using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace LabSuite;
public partial class Game2048Window : Window
{
    static readonly string BF = Path.Combine(AppContext.BaseDirectory, "2048best.txt");
    static readonly string[] Col = { "#1F2F52", "#EEE4DA", "#EDE0C8", "#F2B179", "#F59563", "#F67C5F", "#F65E3B", "#EDCF72", "#EDCC61", "#EDC850", "#EDC53F", "#EDC22E" };
    readonly int[,] g = new int[4, 4]; readonly Random r = new(); int score, best; bool over;
    public Game2048Window()
    {
        InitializeComponent();
        int.TryParse(File.Exists(BF) ? File.ReadAllText(BF) : "0", out best); NewGame(null, null);
    }
    void NewGame(object s, RoutedEventArgs e) { Array.Clear(g); score = 0; over = false; Status.Text = "Грайте стрілками"; Spawn(); Spawn(); Draw(); }
    void Spawn()
    {
        var empty = new List<(int, int)>();
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) if (g[i, j] == 0) empty.Add((i, j));
        if (empty.Count == 0) return;
        var (x, y) = empty[r.Next(empty.Count)]; g[x, y] = r.Next(10) == 0 ? 4 : 2;
    }
    static (int, int) P(int d, int k, int t) => d switch { 0 => (k, t), 1 => (k, 3 - t), 2 => (t, k), _ => (3 - t, k) };
    bool Move(int d)
    {
        bool moved = false;
        for (int k = 0; k < 4; k++)
        {
            var v = new List<int>();
            for (int t = 0; t < 4; t++) { var (x, y) = P(d, k, t); if (g[x, y] != 0) v.Add(g[x, y]); }
            for (int i = 0; i < v.Count - 1; i++)
                if (v[i] == v[i + 1]) { v[i] *= 2; score += v[i]; v.RemoveAt(i + 1); }
            for (int t = 0; t < 4; t++)
            {
                var (x, y) = P(d, k, t); int n = t < v.Count ? v[t] : 0;
                if (g[x, y] != n) { moved = true; g[x, y] = n; }
            }
        }
        return moved;
    }
    bool CanMove()
    {
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
                if (g[i, j] == 0 || (i < 3 && g[i, j] == g[i + 1, j]) || (j < 3 && g[i, j] == g[i, j + 1])) return true;
        return false;
    }
    void Keyed(object s, KeyEventArgs e)
    {
        int d = e.Key switch { Key.Left => 0, Key.Right => 1, Key.Up => 2, Key.Down => 3, _ => -1 };
        if (d < 0) return; e.Handled = true;
        if (over || !Move(d)) return;
        Spawn();
        if (score > best) { best = score; File.WriteAllText(BF, best.ToString()); }
        if (!CanMove()) { over = true; Status.Text = "GAME OVER"; }
        Draw();
    }
    void Draw()
    {
        ScoreText.Text = $"Рахунок: {score}"; BestText.Text = $"Рекорд: {best}"; Board.Children.Clear();
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                int v = g[i, j], idx = v == 0 ? 0 : Math.Min((int)Math.Log2(v), Col.Length - 1);
                Board.Children.Add(new Border
                {
                    Margin = new Thickness(4), CornerRadius = new CornerRadius(6),
                    Background = (Brush)new BrushConverter().ConvertFromString(Col[idx]),
                    Child = new TextBlock
                    {
                        Text = v == 0 ? "" : v.ToString(), FontSize = v < 1000 ? 30 : 22, FontWeight = FontWeights.Bold,
                        Foreground = v > 4 ? Brushes.White : (Brush)FindResource("Ink"),
                        HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center
                    }
                });
            }
    }
}

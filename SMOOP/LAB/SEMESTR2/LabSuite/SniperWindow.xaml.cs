using System.Windows;
using System.Windows.Controls;
namespace LabSuite;
public partial class SniperWindow : Window
{
    readonly List<Button> cells = new(); readonly Random r = new(); int target, lives; bool over;
    public SniperWindow()
    {
        InitializeComponent();
        for (int i = 0; i < 16; i++) { var b = new Button { Style = (Style)FindResource("Cell") }; b.Click += Shoot; cells.Add(b); Field.Children.Add(b); }
        NewGame(null, null);
    }
    void NewGame(object s, RoutedEventArgs e)
    {
        target = r.Next(16); lives = 5; over = false;
        foreach (var c in cells) c.Tag = null;
        Status.Text = "Ціль сховалась у темряві. Постріли: 5";
    }
    void Shoot(object s, RoutedEventArgs e)
    {
        var b = (Button)s; if (over || b.Tag != null) return;
        if (cells.IndexOf(b) == target) { b.Tag = "hit"; over = true; Status.Text = $"Влучання! Витрачено пострілів: {6 - lives}"; return; }
        b.Tag = "miss"; lives--;
        if (lives == 0) { over = true; cells[target].Tag = "hit"; Status.Text = "Зброю зламано. Ціль була тут."; }
        else Status.Text = $"Промах. Лишилось пострілів: {lives}";
    }
}

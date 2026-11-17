using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
namespace LabSuite;
public partial class ClickerWindow : Window
{
    static readonly (string icon, string name, int cost, int inc)[] B =
        { ("🏭", "Завод", 50, 2), ("🏠", "Житловий будинок", 120, 4), ("🌳", "Парк", 300, 10) };
    readonly List<Button> buy = new(); long coins; int income;
    public ClickerWindow()
    {
        InitializeComponent();
        foreach (var b in B)
        {
            var btn = new Button { Content = $"{b.icon} {b.name}\n{b.cost} монет, +{b.inc}/с", Tag = b };
            btn.Click += Buy; Shop.Children.Add(btn); buy.Add(btn);
        }
        var t = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        t.Tick += (_, _) => { coins += income; Refresh(); }; t.Start(); Refresh();
    }
    void Work(object s, RoutedEventArgs e) { coins+=10; Refresh(); }
    void Buy(object s, RoutedEventArgs e)
    {
        var b = ((string icon, string name, int cost, int inc))((Button)s).Tag;
        if (coins < b.cost) return;
        coins -= b.cost; income += b.inc;
        City.Children.Add(new Border
        {
            Width = 64, Height = 64, Margin = new Thickness(4), CornerRadius = new CornerRadius(8),
            Background = (System.Windows.Media.Brush)FindResource("Ink"),
            Child = new TextBlock { Text = b.icon, FontSize = 34, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center }
        });
        Refresh();
    }
    void Refresh()
    {
        CoinsText.Text = $"Монети: {coins}"; IncomeText.Text = $"Дохід: {income} монет за секунду";
        foreach (var btn in buy) btn.IsEnabled = coins >= (((string icon, string name, int cost, int inc))btn.Tag).cost;
    }
}

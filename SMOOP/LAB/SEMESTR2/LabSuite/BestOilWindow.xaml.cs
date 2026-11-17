using System.IO;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
namespace LabSuite;
public partial class BestOilWindow : Window
{
    static readonly (string n, double p)[] Fuels = { ("А-92", 52.5), ("А-95", 55.9), ("Дизель", 54.2), ("Газ", 29.0) };
    static readonly (string n, double p)[] Menu = { ("Хот-дог", 45), ("Бургер", 80), ("Картопля фрі", 40), ("Кола", 30) };
    readonly List<(CheckBox c, TextBox q, double p)> rows = new();
    static readonly string File_ = Path.Combine(AppContext.BaseDirectory, "revenue.txt");
    bool busy; double total;
    double Price => Fuels[Math.Max(Fuel.SelectedIndex, 0)].p;

    public BestOilWindow()
    {
        InitializeComponent();
        Fuel.ItemsSource = Fuels.Select(f => f.n); Fuel.SelectedIndex = 0;
        foreach (var (n, p) in Menu)
        {
            var g = new Grid { Margin = new Thickness(0, 4, 0, 4) };
            g.ColumnDefinitions.Add(new ColumnDefinition()); g.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });
            var c = new CheckBox { Content = n, VerticalAlignment = VerticalAlignment.Center, Foreground = Brushes.White };
            var pr = new TextBlock { Text = $"{p:F2} грн", Margin = new Thickness(8, 0, 8, 0), VerticalAlignment = VerticalAlignment.Center };
            var q = new TextBox { Text = "1", IsEnabled = false };
            c.Checked += (_, _) => q.IsEnabled = true; c.Unchecked += (_, _) => q.IsEnabled = false;
            Grid.SetColumn(pr, 1); Grid.SetColumn(q, 2); g.Children.Add(c); g.Children.Add(pr); g.Children.Add(q);
            CafeList.Children.Add(g); rows.Add((c, q, p));
        }
        ShowRevenue();
    }
    static double? Num(TextBox t)
    {
        if (string.IsNullOrWhiteSpace(t.Text)) return 0;
        return double.TryParse(t.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var v) && v >= 0 ? v : null;
    }
    void FuelChanged(object s, SelectionChangedEventArgs e) { PriceBox.Text = Price.ToString("F2"); FuelCalc(null, null); }
    void Mode(object s, RoutedEventArgs e)
    {
        if (!IsLoaded) return;
        QtyBox.IsEnabled = ByQty.IsChecked == true; SumBox.IsEnabled = !QtyBox.IsEnabled;
    }
    void FuelCalc(object s, TextChangedEventArgs e)
    {
        if (!IsLoaded || busy) return;
        busy = true;
        if (ByQty.IsChecked == true) { if (Num(QtyBox) is double q) SumBox.Text = (q * Price).ToString("F2"); }
        else if (Num(SumBox) is double m) QtyBox.Text = (m / Price).ToString("F2");
        busy = false;
    }
    void Count(object s, RoutedEventArgs e)
    {
        double fuel = (ByQty.IsChecked == true ? Num(QtyBox) * Price : Num(SumBox)) ?? -1;
        if (fuel < 0) { MessageBox.Show("Некоректне значення в блоці АЗС."); return; }
        double cafe = 0;
        foreach (var (c, q, p) in rows)
        {
            if (c.IsChecked != true) continue;
            if (!int.TryParse(q.Text, out int n) || n <= 0) { MessageBox.Show($"Вкажіть кількість (ціле число) для «{c.Content}»."); return; }
            cafe += n * p;
        }
        total = fuel + cafe;
        Total.Text = $"До сплати: {total:F2} грн";
    }
    void Pay(object s, RoutedEventArgs e)
    {
        if (total <= 0) { MessageBox.Show("Спочатку натисніть «Прорахувати»."); return; }
        File.AppendAllText(File_, $"{DateTime.Today:yyyy-MM-dd};{total.ToString(CultureInfo.InvariantCulture)}{Environment.NewLine}");
        total = 0; Total.Text = "До сплати: 0,00 грн"; ShowRevenue();
    }
    void ShowRevenue()
    {
        double sum = 0; string today = DateTime.Today.ToString("yyyy-MM-dd");
        if (File.Exists(File_))
            foreach (var l in File.ReadLines(File_))
            {
                var p = l.Split(';');
                if (p.Length == 2 && p[0] == today && double.TryParse(p[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var v)) sum += v;
            }
        Revenue.Text = $"Виручка за день: {sum:F2} грн";
    }
}

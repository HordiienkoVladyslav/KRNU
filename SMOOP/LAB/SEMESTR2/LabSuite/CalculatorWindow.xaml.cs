using System.Globalization;
using System.Windows;
using System.Windows.Controls;
namespace LabSuite;
public partial class CalculatorWindow : Window
{
    double acc; string op = ""; bool fresh = true;
    public CalculatorWindow()
    {
        InitializeComponent();
        string[] k = { "CE", "C", "<", "/", "7", "8", "9", "*", "4", "5", "6", "-", "1", "2", "3", "+", "±", "0", ",", "=" };
        for (int i = 0; i < k.Length; i++)
        {
            var b = new Button { Content = k[i], FontSize = 22, Tag = k[i] };
            if ("/*-+=".Contains(k[i])) b.Style = (Style)FindResource("Accent");
            b.Click += Key; Grid.SetRow(b, i / 4); Grid.SetColumn(b, i % 4); Keys.Children.Add(b);
        }
    }
    double Parse() => double.TryParse(Cur.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var v) ? v : 0;
    static string Fmt(double v) => v.ToString("0.###############", CultureInfo.InvariantCulture).Replace('.', ',');
    void Key(object s, RoutedEventArgs e)
    {
        string k = (string)((Button)s).Tag;
        if (char.IsDigit(k[0]))
        {
            if (fresh || Cur.Text == "0") { Cur.Text = k; fresh = false; }
            else if (Cur.Text.Length < 16) Cur.Text += k;
            return;
        }
        switch (k)
        {
            case ",": if (fresh) { Cur.Text = "0,"; fresh = false; } else if (!Cur.Text.Contains(',')) Cur.Text += ","; break;
            case "<": if (!fresh) { var t = Cur.Text[..^1]; Cur.Text = t is "" or "-" ? "0" : t; } break;
            case "CE": Cur.Text = "0"; fresh = true; break;
            case "C": Cur.Text = "0"; Hist.Text = ""; acc = 0; op = ""; fresh = true; break;
            case "±": if (Cur.Text != "0") Cur.Text = Cur.Text.StartsWith("-") ? Cur.Text[1..] : "-" + Cur.Text; break;
            case "=":
                if (op != "") { Hist.Text = $"{Fmt(acc)} {op} {Cur.Text} ="; Calc(); op = ""; fresh = true; }
                break;
            default:
                if (op != "" && !fresh) Calc(); else acc = Parse();
                op = k; Hist.Text = $"{Fmt(acc)} {k}"; fresh = true; break;
        }
    }
    void Calc()
    {
        double b = Parse();
        if (op == "/" && b == 0) { Cur.Text = "Ділення на нуль"; Hist.Text = ""; acc = 0; op = ""; fresh = true; return; }
        acc = op switch { "+" => acc + b, "-" => acc - b, "*" => acc * b, _ => acc / b };
        Cur.Text = Fmt(acc);
    }
}

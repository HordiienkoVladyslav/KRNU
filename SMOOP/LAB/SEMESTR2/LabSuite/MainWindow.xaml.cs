using System.Windows;
using System.Windows.Controls;
namespace LabSuite;
public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();
    void Open(object s, RoutedEventArgs e)
    {
        Window w = ((Button)s).Tag switch
        {
            "Calc" => new CalculatorWindow(), "Oil" => new BestOilWindow(), "City" => new ClickerWindow(),
            "Auth" => new AuthWindow(), "G2048" => new Game2048Window(), _ => new SniperWindow()
        };
        w.Owner = this; w.Show();
    }
}

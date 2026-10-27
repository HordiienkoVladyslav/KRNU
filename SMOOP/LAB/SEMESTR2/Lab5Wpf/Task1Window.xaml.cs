using System.Windows;

namespace Lab5Wpf;

public partial class Task1Window : Window
{
    public Task1Window()
    {
        InitializeComponent();
    }

    private void HelloButton_Click(object sender, RoutedEventArgs e)
    {
        OutputText.Text = "Привіт";
    }

    private void ByeButton_Click(object sender, RoutedEventArgs e)
    {
        OutputText.Text = "До побачення";
    }
}

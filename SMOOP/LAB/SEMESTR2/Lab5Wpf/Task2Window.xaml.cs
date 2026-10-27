using System.Windows;

namespace Lab5Wpf;

public partial class Task2Window : Window
{
    public Task2Window()
    {
        InitializeComponent();
    }

    private void HideButton_Click(object sender, RoutedEventArgs e)
    {
        SampleText.Visibility = Visibility.Collapsed;
    }

    private void ShowButton_Click(object sender, RoutedEventArgs e)
    {
        SampleText.Visibility = Visibility.Visible;
    }
}

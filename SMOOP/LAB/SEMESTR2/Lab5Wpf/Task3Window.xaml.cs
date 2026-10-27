using System.Windows;

namespace Lab5Wpf;

public partial class Task3Window : Window
{
    public Task3Window()
    {
        InitializeComponent();
    }

    private void HideButton_Click(object sender, RoutedEventArgs e)
    {
        InputBox.Visibility = Visibility.Hidden;
    }

    private void ShowButton_Click(object sender, RoutedEventArgs e)
    {
        InputBox.Visibility = Visibility.Visible;
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        InputBox.Clear();
    }
}

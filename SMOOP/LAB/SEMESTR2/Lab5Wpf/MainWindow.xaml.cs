using System.Windows;

namespace Lab5Wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenWindow(Window window)
    {
        window.Owner = this;
        window.Show();
    }

    private void Task1_Click(object sender, RoutedEventArgs e) => OpenWindow(new Task1Window());
    private void Task2_Click(object sender, RoutedEventArgs e) => OpenWindow(new Task2Window());
    private void Task3_Click(object sender, RoutedEventArgs e) => OpenWindow(new Task3Window());
    private void Task4_Click(object sender, RoutedEventArgs e) => OpenWindow(new Task4Window());
    private void Task5_Click(object sender, RoutedEventArgs e) => OpenWindow(new Task5Window());
    private void Task6_Click(object sender, RoutedEventArgs e) => OpenWindow(new Task6Window());
    private void Task7_Click(object sender, RoutedEventArgs e) => OpenWindow(new Task7Window());
}

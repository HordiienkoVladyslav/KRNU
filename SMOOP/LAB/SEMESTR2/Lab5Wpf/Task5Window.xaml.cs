using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab5Wpf;

public partial class Task5Window : Window
{
    private readonly Random _random = new();
    private int _escapes;

    public Task5Window()
    {
        InitializeComponent();
    }

    private void Field_Loaded(object sender, RoutedEventArgs e)
    {
        CatchButton.UpdateLayout();
        Canvas.SetLeft(CatchButton, (Field.ActualWidth - CatchButton.ActualWidth) / 2);
        Canvas.SetTop(CatchButton, (Field.ActualHeight - CatchButton.ActualHeight) / 2);
    }

    private void CatchButton_MouseEnter(object sender, MouseEventArgs e)
    {
        double maxX = Math.Max(0, Field.ActualWidth - CatchButton.ActualWidth);
        double maxY = Math.Max(0, Field.ActualHeight - CatchButton.ActualHeight);

        Canvas.SetLeft(CatchButton, _random.NextDouble() * maxX);
        Canvas.SetTop(CatchButton, _random.NextDouble() * maxY);

        _escapes++;
        EscapesText.Text = $"Кнопка втекла: {_escapes} разів";
    }

    private void CatchButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Перемога! Ви спіймали кнопку!");
    }
}

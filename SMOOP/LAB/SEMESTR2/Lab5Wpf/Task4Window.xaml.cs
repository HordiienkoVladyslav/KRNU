using System.Windows;
using System.Windows.Controls;

namespace Lab5Wpf;

public partial class Task4Window : Window
{
    private readonly Random _random = new();
    private readonly List<Button> _buttons;
    private int _moves;

    public Task4Window()
    {
        InitializeComponent();

        _buttons = GamePanel.Children.OfType<Button>().ToList();
        foreach (var button in _buttons)
        {
            button.Click += GameButton_Click;
        }

        NewGame();
    }

    private void NewGame()
    {
        foreach (var button in _buttons)
        {
            button.Visibility = Visibility.Visible;
        }

        _moves = 0;
        UpdateMoves();
    }

    private void UpdateMoves()
    {
        MovesText.Text = $"Ходів: {_moves}";
    }

    private void GameButton_Click(object sender, RoutedEventArgs e)
    {
        var clicked = (Button)sender;

        // Натиснута кнопка завжди ховається.
        clicked.Visibility = Visibility.Hidden;

        // Кожна інша кнопка з імовірністю 40% перемикає свою видимість.
        foreach (var other in _buttons)
        {
            if (other == clicked) continue;

            if (_random.NextDouble() < 0.4)
            {
                other.Visibility = other.Visibility == Visibility.Visible
                    ? Visibility.Hidden
                    : Visibility.Visible;
            }
        }

        _moves++;
        UpdateMoves();

        if (_buttons.All(b => b.Visibility != Visibility.Visible))
        {
            MessageBox.Show("Вітаємо! Усі кнопки приховано!");
            NewGame();
        }
    }

    private void NewGameButton_Click(object sender, RoutedEventArgs e)
    {
        NewGame();
    }
}

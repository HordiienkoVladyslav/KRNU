using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab5Wpf;

public partial class Task7Window : Window
{
    private const int MaxAttempts = 7;

    private readonly Random _random = new();

    private int _secret;
    private int _attemptsLeft;
    private int _attemptNumber;
    private int? _previousGuess;
    private bool _xrayPaid;
    private bool _revealFirstDigit;
    private bool _gameOver;
    private bool _suppressXrayEvents;

    public Task7Window()
    {
        InitializeComponent();
        NewGame();
    }

    private void NewGame()
    {
        _secret = _random.Next(1000, 10000);
        _attemptsLeft = MaxAttempts;
        _attemptNumber = 0;
        _previousGuess = null;
        _xrayPaid = false;
        _gameOver = false;
        _revealFirstDigit = _random.Next(2) == 0;

        _suppressXrayEvents = true;
        XrayCheck.IsChecked = false;
        _suppressXrayEvents = false;

        XrayLabel.Content = "Рентген вимкнено";
        HistoryList.Items.Clear();
        GuessBox.Clear();
        GuessBox.IsEnabled = true;
        CheckButton.IsEnabled = true;
        XrayCheck.IsEnabled = true;

        SetHint("Введіть код і натисніть «Перевірити».", Brushes.Black);
        UpdateAttempts();
        GuessBox.Focus();
    }

    private void UpdateAttempts()
    {
        AttemptsText.Text = $"Залишилось спроб: {_attemptsLeft} з {MaxAttempts}";
    }

    private void SetHint(string text, Brush brush)
    {
        HintText.Text = text;
        HintText.Foreground = brush;
    }

    private void EndGame()
    {
        _gameOver = true;
        GuessBox.IsEnabled = false;
        CheckButton.IsEnabled = false;
        XrayCheck.IsEnabled = false;
    }

    private void AddHistory(string line)
    {
        HistoryList.Items.Add(line);
        HistoryList.ScrollIntoView(line);
    }

    private void GuessBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Дозволяємо вводити лише цифри.
        e.Handled = !e.Text.All(char.IsDigit);
    }

    private void CheckButton_Click(object sender, RoutedEventArgs e)
    {
        if (_gameOver) return;

        string text = GuessBox.Text.Trim();

        if (text.Length != 4 || !int.TryParse(text, out int guess))
        {
            SetHint("Введіть рівно 4 цифри.", Brushes.DarkOrange);
            return;
        }

        if (guess < 1000)
        {
            SetHint("Код починається з цифри від 1 до 9.", Brushes.DarkOrange);
            return;
        }

        _attemptsLeft--;
        _attemptNumber++;
        UpdateAttempts();

        if (guess == _secret)
        {
            AddHistory($"#{_attemptNumber}: {guess} — ВІРНО!");
            SetHint($"Сейф відкрито! Код: {_secret}", Brushes.ForestGreen);
            EndGame();
            MessageBox.Show($"Вітаємо! Ви зламали сейф за {_attemptNumber} спроб(и).");
            return;
        }

        string direction = guess < _secret ? "Більше" : "Менше";

        string temperature = "—";
        if (_previousGuess is int previous)
        {
            int oldDistance = Math.Abs(previous - _secret);
            int newDistance = Math.Abs(guess - _secret);

            temperature = newDistance < oldDistance ? "Гаряче"
                        : newDistance > oldDistance ? "Холодно"
                        : "Без змін";
        }

        int exactDigits = CountExactDigits(guess, _secret);
        _previousGuess = guess;

        string hint = _previousGuess is not null && temperature != "—"
            ? $"{direction}. {temperature}!"
            : $"{direction}.";

        SetHint($"{hint}\nТочних цифр на своїх місцях: {exactDigits}", Brushes.Black);
        AddHistory($"#{_attemptNumber}: {guess} — {direction}, {temperature} (точних цифр: {exactDigits})");

        GuessBox.Clear();
        GuessBox.Focus();

        if (_attemptsLeft <= 0)
        {
            AddHistory($"Спроби вичерпано. Код був {_secret}.");
            SetHint("Сейф заблоковано! Спроби закінчилися.", Brushes.Firebrick);
            EndGame();
            MessageBox.Show($"Сейф заблоковано! Правильний код: {_secret}");
        }
    }

    private static int CountExactDigits(int guess, int secret)
    {
        string g = guess.ToString();
        string s = secret.ToString();
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            if (g[i] == s[i]) count++;
        }

        return count;
    }

    private void XrayCheck_Checked(object sender, RoutedEventArgs e)
    {
        if (_suppressXrayEvents || _gameOver) return;

        if (!_xrayPaid)
        {
            if (_attemptsLeft <= 1)
            {
                MessageBox.Show("Недостатньо спроб для використання рентгена.");
                _suppressXrayEvents = true;
                XrayCheck.IsChecked = false;
                _suppressXrayEvents = false;
                return;
            }

            _attemptsLeft--;
            _xrayPaid = true;
            UpdateAttempts();
            AddHistory("Рентген увімкнено (−1 спроба)");
        }

        string code = _secret.ToString();
        XrayLabel.Content = _revealFirstDigit
            ? $"Перша цифра коду: {code[0]}"
            : $"Остання цифра коду: {code[3]}";
    }

    private void XrayCheck_Unchecked(object sender, RoutedEventArgs e)
    {
        if (_suppressXrayEvents) return;
        XrayLabel.Content = "Рентген вимкнено";
    }

    private void NewGameButton_Click(object sender, RoutedEventArgs e)
    {
        NewGame();
    }
}

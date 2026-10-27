using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab5Wpf;

public partial class Task6Window : Window
{
    private const int Tolerance = 5;

    private readonly Random _random = new();
    private byte _targetR, _targetG, _targetB;
    private bool _ready;

    public Task6Window()
    {
        InitializeComponent();
        _ready = true;
        NewRecipe();
    }

    private void NewRecipe()
    {
        _targetR = (byte)_random.Next(0, 256);
        _targetG = (byte)_random.Next(0, 256);
        _targetB = (byte)_random.Next(0, 256);

        RecipeText.Text = $"Рецепт зілля: R = {_targetR}, G = {_targetG}, B = {_targetB}\n" +
                          $"(допустима похибка ±{Tolerance})";
        TargetRect.Fill = new SolidColorBrush(Color.FromRgb(_targetR, _targetG, _targetB));

        RedSlider.Value = 0;
        GreenSlider.Value = 0;
        BlueSlider.Value = 0;

        UpdateMix();
    }

    private void UpdateMix()
    {
        byte r = (byte)Math.Round(RedSlider.Value);
        byte g = (byte)Math.Round(GreenSlider.Value);
        byte b = (byte)Math.Round(BlueSlider.Value);

        MixRect.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));

        RedLabel.Text = $"Red: {r}";
        GreenLabel.Text = $"Green: {g}";
        BlueLabel.Text = $"Blue: {b}";

        bool match = Math.Abs(r - _targetR) <= Tolerance &&
                     Math.Abs(g - _targetG) <= Tolerance &&
                     Math.Abs(b - _targetB) <= Tolerance;

        if (match)
        {
            StatusText.Text = "Зілля готове!";
            StatusText.Foreground = Brushes.ForestGreen;
        }
        else
        {
            StatusText.Text = "Продовжуйте змішувати...";
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(0x6B, 0x77, 0x90));
        }
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // Подія спрацьовує ще під час InitializeComponent, коли не всі елементи створені.
        if (!_ready) return;
        UpdateMix();
    }

    private void NewRecipeButton_Click(object sender, RoutedEventArgs e)
    {
        NewRecipe();
    }
}

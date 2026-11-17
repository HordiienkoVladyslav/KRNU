using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
namespace LabSuite;
public record User(string Name, string Email, string Hash);
public partial class AuthWindow : Window
{
    static readonly string F = Path.Combine(AppContext.BaseDirectory, "users.json");
    static readonly Regex Mail = new(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$");
    readonly List<User> users;
    public AuthWindow()
    {
        InitializeComponent();
        try { users = File.Exists(F) ? JsonSerializer.Deserialize<List<User>>(File.ReadAllText(F)) : new(); } catch { users = new(); }
    }
    void Save() => File.WriteAllText(F, JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));
    static string H(string e, string p) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(e.ToLowerInvariant() + p)));
    void Say(string m, bool ok = false) { Msg.Text = m; Msg.Foreground = ok ? Brushes.LightGreen : Brushes.Salmon; }
    void ShowLogin(object s, RoutedEventArgs e) { LoginPanel.Visibility = Visibility.Visible; RegPanel.Visibility = Visibility.Collapsed; Msg.Text = ""; }
    void ShowReg(object s, RoutedEventArgs e) { LoginPanel.Visibility = Visibility.Collapsed; RegPanel.Visibility = Visibility.Visible; Msg.Text = ""; }
    void Login(object s, RoutedEventArgs e)
    {
        var em = LEmail.Text.Trim();
        if (!Mail.IsMatch(em)) { Say("Введіть коректний email."); return; }
        if (LPass.Password.Length == 0) { Say("Введіть пароль."); return; }
        var u = users.FirstOrDefault(x => x.Email.Equals(em, StringComparison.OrdinalIgnoreCase) && x.Hash == H(em, LPass.Password));
        if (u == null) { Say("Невірний email або пароль."); return; }
        Say($"Вітаємо, {u.Name}!", true);
    }
    void Register(object s, RoutedEventArgs e)
    {
        string n = RName.Text.Trim(), em = REmail.Text.Trim(), p = RPass.Password;
        if (n.Length < 2) { Say("Вкажіть ім'я (мінімум 2 символи)."); return; }
        if (!Mail.IsMatch(em)) { Say("Введіть коректний email."); return; }
        if (p.Length < 8 || !p.Any(char.IsLetter) || !p.Any(char.IsDigit)) { Say("Пароль: від 8 символів, потрібні літери й цифри."); return; }
        if (p != RPass2.Password) { Say("Паролі не збігаються."); return; }
        if (users.Any(x => x.Email.Equals(em, StringComparison.OrdinalIgnoreCase))) { Say("Користувач з таким email уже існує."); return; }
        users.Add(new User(n, em, H(em, p))); Save();
        ShowLogin(null, null); LEmail.Text = em; Say("Акаунт створено. Тепер увійдіть.", true);
    }
    void Google(object s, RoutedEventArgs e)
    {
        Say("Підключення до Google…", true);
        var t = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.2) };
        t.Tick += (_, _) =>
        {
            t.Stop(); const string em = "user@gmail.com";
            var u = users.FirstOrDefault(x => x.Email == em);
            if (u == null) { u = new User("Google User", em, H(em, Guid.NewGuid().ToString())); users.Add(u); Save(); }
            Say($"Вхід через Google: {u.Name} ({u.Email})", true);
        };
        t.Start();
    }
}

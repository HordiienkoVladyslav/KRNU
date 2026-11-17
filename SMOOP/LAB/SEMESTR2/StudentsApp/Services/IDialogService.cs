namespace StudentsApp.Services;

/// <summary>Абстракція діалогів, щоб ViewModel не залежала від MessageBox напряму.</summary>
public interface IDialogService
{
    bool Confirm(string message, string title);

    void ShowError(string message, string title);
}

using System.Windows;

namespace StudentsApp.Services;

public class MessageBoxDialogService : IDialogService
{
    public bool Confirm(string message, string title)
        => MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)
           == MessageBoxResult.Yes;

    public void ShowError(string message, string title)
        => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
}

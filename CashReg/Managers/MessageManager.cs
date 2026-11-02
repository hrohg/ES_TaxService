using System.Windows;

namespace CashReg.Managers
{
    public class MessageManager
    {
        public static void ShowMessage(string message, string title, MessageBoxButton messageBoxButton = MessageBoxButton.OK, MessageBoxImage messageBoxImage = MessageBoxImage.Error)
        {
            MessageBox.Show(message, title, messageBoxButton, messageBoxImage);
        }
    }
}

using Ecr.Manager.ViewModels;
using System.Windows;

namespace Ecr.Manager.Views
{
    /// <summary>
    /// Interaction logic for ReturnTicketWindow.xaml
    /// </summary>

    public partial class ReturnTicketWindow : Window
    {        
        public ReturnTicketWindow(ReturnTicketViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

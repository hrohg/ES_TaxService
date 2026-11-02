using ECR.Controls.ViewModels;
using System;
using System.Windows;

namespace ECR.Controls.Views
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

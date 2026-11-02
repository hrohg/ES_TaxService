using Ecr.Manager.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace Ecr.Manager.Views
{
    /// <summary>
    /// Interaction logic for SelectItemsWindow.xaml
    /// </summary>
    public partial class SelectItemsWindow : Window
    {
        public SelectItemsViewModel ViewModel { get { return DataContext as SelectItemsViewModel; } }

        public SelectItemsWindow(IEnumerable<PaymentSystemModel> models, bool allowMultipleSelection = false, string title = "Select Items")
        {
            InitializeComponent();
            DataContext = new SelectItemsViewModel(models, allowMultipleSelection, title);

            ViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.DialogResult) && ViewModel.DialogResult.HasValue)
                {
                    DialogResult = ViewModel.DialogResult.Value;
                    Close();
                }
            };
        }
    }
}

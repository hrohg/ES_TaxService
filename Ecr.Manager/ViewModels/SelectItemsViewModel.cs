using Ecr.Manager.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Ecr.Manager.ViewModels
{
    public class SelectItemsViewModel : ViewModelBase
    {
        public string Title { get; private set; }
        public ObservableCollection<PaymentSystemModel> Items { get; }
        public bool IsSingleSelection { get; private set; }
        public bool? DialogResult { get; private set; }

        public ICommand ItemDoubleClickCommand { get; }
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public SelectItemsViewModel(IEnumerable<PaymentSystemModel> models, bool allowMultipleSelection, string title)
        {
            Title = title;
            Items = new ObservableCollection<PaymentSystemModel>(models);
            IsSingleSelection = !allowMultipleSelection;

            ItemDoubleClickCommand = new RelayCommand<object>(OnItemDoubleClick);
            OkCommand = new RelayCommand(ExecuteOk);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void OnItemDoubleClick(object item)
        {
            ExecuteOk(null);
        }

        private void ExecuteOk(object obj)
        {
            DialogResult = true;
            RaisePropertyChanged(() => DialogResult);
        }

        private void ExecuteCancel(object obj)
        {
            DialogResult = false;
            RaisePropertyChanged(() => DialogResult);
        }

        public IEnumerable<PaymentSystemModel> GetSelectedItems() => Items.Where(i => i.IsSelected).Select(i => i);

    }

    public class PaymentSystemModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }

        public bool IsSelected { get; set; }

        public PaymentSystemModel(int code, string name, string logoPath = null)
        {
            Code = code;
            Name = name;
            LogoPath = logoPath;
        }

    }

}

using System.Windows.Input;
using ECR.Controls.Helper;

namespace ECR.Controls.ViewModels
{
    public class ReturnTicketViewModel : ViewModelBase
    {
        #region Internal property
        private bool isReturnPartly;
        private string crn;
        private string receiptId;
        #endregion Internal property

        #region Expernal properties
        public string ReceiptId { get => receiptId; set { receiptId = value; RaisePropertyChanged(() => IsActive); } }
        public string Crn { get => crn; set { crn = value; RaisePropertyChanged(() => IsActive); } }

        public bool IsReturnPartly { get => isReturnPartly; set => isReturnPartly = value; }
        public bool IsActive { get => !string.IsNullOrEmpty(Crn) && !string.IsNullOrEmpty(ReceiptId); }
        public bool HasTicketData { get => TicketData != null; }
        public object TicketData { get; set; }
        #endregion External properties

        public ReturnTicketViewModel(string crn, string ticketNumber = null)
        {
            Crn = crn;
            GetTicketDataCommand = new RelayCommand(OnGetTicketData, o => IsActive);
        }

        #region Internal methods
        private void GetTicketData() { }

        private void OnGetTicketData(object obj)
        {
            //new ReturnReceiptManager().GetReturnReceipt();
        }
        #endregion Internal methods

        #region External methods
        #endregion External methods

        #region Commands
        public ICommand CloseCommand { get; set; }
        public ICommand GetTicketDataCommand { get; set; }
        public ICommand ReturnTicketDataCommand { get; set; }
        #endregion Commands
    }
}

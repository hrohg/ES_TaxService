using System;
using TaxService.Manager.Interfaces;

namespace TaxService.Manager.Models
{
    [Serializable]
    public class BankAccount : IBankAccount
    {
        private string _bankName;
        private string _bankAccountNumber;

        public string BankName
        {
            get { return _bankName ?? string.Empty; }
            set { _bankName = value; }
        }

        public string BankAccountNumber
        {
            get { return _bankAccountNumber ?? string.Empty; }
            set { _bankAccountNumber = value; }
        }
    }
}

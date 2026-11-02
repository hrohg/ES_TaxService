using System;
using TaxService.Manager.Interfaces;

namespace TaxService.Manager.Models
{
    [Serializable]
    public class InvoiceNumber : IInvoiceNumber
    {
        private string _number;
        private string _service;

        public string Number
        {
            get { return _number ?? string.Empty; }
            set { _number = value; }
        }

        public string Series
        {
            get { return _service ?? string.Empty; }
            set { _service = value; }
        }
    }
}

using System;
using System.Xml.Serialization;
using TaxService.Manager.Interfaces;

namespace TaxService.Manager.Models
{
    public class Taxpayer : ITaxpayer
    {
        private string _tin;
        private string _name;
        private string _address;

        [XmlElement("TIN")]
        public string TIN
        {
            get { return _tin ?? string.Empty; }
            set { _tin = value; }
        }

        [XmlElement("Name")]
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        [XmlElement("Address")]
        public string Address
        {
            get { return _address ?? string.Empty; }
            set { _address = value; }
        }
    }

    [Serializable]
    public class SupplierTaxpayer : Taxpayer
    {
        private BankAccount _bankAccount;

        [XmlElement("BankAccount")]
        public BankAccount BankAccount
        {
            get { return _bankAccount; }
            set { _bankAccount = value; }
        }

        public SupplierTaxpayer()
        {
            BankAccount = new BankAccount();
        }
    }

    [Serializable]
    public class BuyerTaxpayer : Taxpayer
    {
        private bool _tinNotRequired;

        [XmlElement("TinNotRequired")]
        public bool TinNotRequired
        {
            get { return _tinNotRequired; }
            set { _tinNotRequired = value; }
        }

        [XmlElement("IsNatural")]
        public bool IsNatural { get; set; }

        public BuyerTaxpayer()
        {

        }
    }    
}

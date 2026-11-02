using System;
using TaxService.Manager.Interfaces;

namespace TaxService.Manager.Models
{
    [Serializable]
    public class BuyerInfo : IBuyerInfo
    {
        private BuyerTaxpayer _taxpayer;
        private string _deliveryLocation;

        public string VATNumber { get; set; }

        public BuyerTaxpayer Taxpayer
        {
            get { return _taxpayer; }
            set { _taxpayer = value; }
        }

        public string DeliveryMethod { get; set; }

        public string DeliveryLocation
        {
            get { return _deliveryLocation ?? string.Empty; }
            set { _deliveryLocation = value; }
        }

        public BuyerInfo()
        {
            Taxpayer = new BuyerTaxpayer();
        }
    }
}

using System;
using System.Xml.Serialization;

namespace TaxService.Manager.Models
{
    public class SupplierInfo
    {
        [XmlElement("SupplyLocation")]
        public string SupplyLocation { get; set; } = string.Empty;

        [XmlElement("SourceIsCar")]
        public bool SourceIsCar { get; set; }
    }

    [Serializable]
    public class SaleSupplierInfo : SupplierInfo
    {
        public string VatNumber { get; set; }

        [XmlElement("Taxpayer", typeof(SupplierTaxpayer))]
        public SupplierTaxpayer Taxpayer { get; set; } = new SupplierTaxpayer();       
        
        public SaleSupplierInfo()
        {

        }
    }

    public class StockTransferSupplierInfo : SupplierInfo
    {
        [XmlElement("Taxpayer")]
        public Taxpayer Taxpayer { get; set; } = new Taxpayer();

        public string ConveyMethod { get; set; }

        public string ConveyLocation { get; set; }        

        public bool DestinationIsCar { get; set; }

        public StockTransferSupplierInfo()
        {

        }
    }
}

using System;
using System.Xml.Serialization;

namespace TaxService.Manager.Models
{
    
    public class GeneralInfo
    {
        [XmlElement("DeliveryDate")]
        public string DeliveryDate { get; set; }
    }

    [Serializable]
    public class SaleGeneralInfo : GeneralInfo
    {
        private InvoiceNumber _invoiceNumber;

        [XmlElement("InvoiceNumber")]
        public InvoiceNumber InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { _invoiceNumber = value; }
        }

        [XmlElement("Procedure")]
        public int Procedure { get; set; }

        [XmlElement("AdjustmentAccount")]
        public bool AdjustmentAccount { get; set; }

        public SaleGeneralInfo(DateTime deliveryDate, int procedure)
        {
            DeliveryDate = deliveryDate.ToString("yyyy-MM-ddK");
            Procedure = procedure;
        }

        public SaleGeneralInfo() { }
    }

    [Serializable]
    public class StockTransferGeneralInfo : GeneralInfo
    {
        public bool CommissionAgreement { get; set; }

        public StockTransferGeneralInfo()
        {

        }

        public StockTransferGeneralInfo(DateTime deliveryDate, bool commissionAgreement = false)
        {
            DeliveryDate = deliveryDate.ToString("yyyy-MM-ddK");
            CommissionAgreement = commissionAgreement;
        }
    }
}

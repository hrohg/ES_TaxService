using System;
using System.Xml.Serialization;

namespace TaxService.Manager.Models
{    
    public abstract class AccountingDocument
    {
        [XmlIgnore]
        public string InvoiceNumber { get; set; }

        [XmlAttribute("Version")]
        public string Version { get; set; }

        [XmlElement("Type", Order = 1)]
        public int Type { get; set; }        
        
        [XmlIgnore]
        public virtual GoodsInfo GoodsInfo { get; set; }
    }

    public class SaleAccountingDocument : AccountingDocument
    {
        [XmlElement("Traceable", Order = 2)]
        public bool Traceable { get; set; }

        [XmlElement("GeneralInfo", typeof(SaleGeneralInfo), Order = 3)]
        public GeneralInfo GeneralInfo { get; set; }

        [XmlElement("SupplierInfo", typeof(SaleSupplierInfo), Order = 4)]
        public SupplierInfo SupplierInfo { get; set; }

        [XmlElement("BuyerInfo", Order = 5)]
        public BuyerInfo BuyerInfo { get; set; } = new BuyerInfo();

        [XmlElement("GoodsInfo", Order = 6)]
        public override GoodsInfo GoodsInfo { get; set; }

        public SaleAccountingDocument()
        {
            Version = "1.0";
            Type = 1;
        }
    }

    public class StockTransferAccountingDocument : AccountingDocument
    {
        [XmlElement("GeneralInfo", typeof(StockTransferGeneralInfo), Order = 3)]
        public StockTransferGeneralInfo GeneralInfo { get; set; }

        [XmlElement("SupplierInfo", typeof(StockTransferSupplierInfo), Order = 4)]
        public StockTransferSupplierInfo SupplierInfo { get; set; }

        [XmlElement("GoodsInfo", Order = 6)]
        public override GoodsInfo GoodsInfo { get; set; }

        public StockTransferAccountingDocument()
        {
            Version = "1.0";
            Type = 2;
        }
    }

    public class ExportedAccDocData
    {
        [XmlIgnore]
        public AccountingDocument AccountingDocument { get; set; }
    }

    [Serializable]
    [XmlRoot(Namespace = "http://www.taxservice.am/tp3/invoice/definitions", ElementName = "ExportedAccDocData")]
    public class ExportedSaleAccDocData : ExportedAccDocData
    {
        [XmlElement("AccountingDocument", typeof(SaleAccountingDocument))]
        public new SaleAccountingDocument AccountingDocument
        {
            get { return (SaleAccountingDocument)base.AccountingDocument; }
            set { base.AccountingDocument = value; }
        }

        public ExportedSaleAccDocData()
        {

        }
    }

    [Serializable]
    [XmlRoot(Namespace = "http://www.taxservice.am/tp3/invoice/definitions", ElementName = "ExportedAccDocData")]
    public class ExportedStockTransferAccDocData : ExportedAccDocData
    {
        [XmlElement("AccountingDocument", typeof(StockTransferAccountingDocument))]
        public new StockTransferAccountingDocument AccountingDocument
        {
            get { return (StockTransferAccountingDocument)base.AccountingDocument; }
            set { base.AccountingDocument = value; }
        }

        public ExportedStockTransferAccDocData()
        {

        }
    }
}

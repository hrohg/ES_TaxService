using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TaxServiceInvoices.Helper
{
    public class XmlSettingsItem
    {
        public string KeyProperty = "Key";
        public string DataProperty = "Data";
        public string ValueProperty = "Value";
        public string MemberProperty = "Member";
        public string Key { get; set; }
        public object Data { get; set; }
        public object Value { get; set; }
        public object Member { get; set; }
        public List<XElement> Elements;
    }
    public class TaxServiceInvoices_XmlTagItems
    {
        /// <summary>
        /// Root
        /// </summary>
        public string ExportedAccDocData = "ExportedAccDocData";
        public string AccountingDocument = "AccountingDocument";
        public string Type = "Type";
        /// <summary>
        /// GeneralInfo
        /// </summary>
        public string GeneralInfo = "GeneralInfo";
        public string DeliveryDate = "DeliveryDate";
        public string DealInfo = "DealInfo";
        public string AdditionalData = "AdditionalData";
        /// <summary>
        /// SupplierInfo
        /// </summary>
        public string SupplierInfo = "SupplierInfo";
        /// <summary>
        /// SupplierInfo Taxpayer
        /// </summary>
        public string Taxpayer = "Taxpayer";
        public string TIN = "TIN";
        public string Name = "Name";
        public string Address = "Address";
        /// <summary>
        /// SupplierInfo SupplyLocation
        /// </summary>
        public string SupplyLocation = "SupplyLocation";
        public string ConveyMethod = "ConveyMethod";
        public string ConveyLocation = "ConveyLocation";
        /// <summary>
        /// GoodsInfo
        /// </summary>
        public string GoodsInfo = "GoodsInfo";
        //GoodsInfo Good
        public string Good = "Good";
        //GoodsInfo Good Description
        public string Description = "Description";
        public string Unit = "Unit";
        public string Amount = "Amount";
        public string PricePerUnit = "PricePerUnit";
        public string Price = "Price";
        public string TotalPrice = "TotalPrice";
        /// <summary>
        /// Total
        /// </summary>
        public string Total = "Total";
    }
    public class XmlConstants
    {
        public static TaxServiceInvoices_XmlTagItems TaxServiceInvoices;
        public static XmlSettingsItem XmlTag;
    }

    public static class ConfigFileConstants
    {
        public static string FileName = @"ECRConfig.Xml";
        public static string FilePath = Application.StartupPath;
    }

    public static class Constants
    {
        public enum AccountingDocumentType
        {
            HarkayinAccountingDocument = 1,
            TexapoxmanAccountingDocument = 2
        }
    }
}

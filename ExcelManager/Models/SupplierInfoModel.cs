using System.Xml.Serialization;

namespace TaxServiceInvoices.Models
{
    public class SupplierInfoModel
    {
        public Taxpayer Taxpayer { get; set; }
        [XmlElement(IsNullable = false)]
        public string SupplyLocation { get; set; }
        [XmlElementAttribute(IsNullable = false)]
        public string ConveyMethod { get; set; }
        [XmlElementAttribute(IsNullable = false)]
        public string ConveyLocation { get; set; }
    }
    public class Taxpayer
    {
        [XmlElementAttribute(IsNullable = false)]
        public string TIN { get; set; }
        [XmlElementAttribute(IsNullable = false)]
        public string Name { get; set; }
        [XmlElementAttribute(IsNullable = false)]
        public string Address { get; set; }
    }
}

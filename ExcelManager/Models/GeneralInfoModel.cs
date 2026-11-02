using System;
using System.Xml.Serialization;

namespace TaxServiceInvoices.Models
{
    public class GeneralInfoModel
    {
        [XmlElement(DataType = "date")]  
        public DateTime? DeliveryDate { get; set; }
        [XmlElement]
        public string DealInfo { get; set; }
        [XmlElement]
        public string AdditionalData { get; set; }
    }
}

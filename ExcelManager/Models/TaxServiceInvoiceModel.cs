using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TaxServiceInvoices.Models
{
    [Serializable]
    public class AccountingDocument : INotifyPropertyChanged
    {
        public AccountingDocument(long type)
        {
            _type = type;
        }

        public AccountingDocument()
        {
            
        }

        #region Invoice model properties
        private const string TypeProperty = "Type";
        private const string CreateDateProperty = "CreateDate";
        private const string DiscountProperty = "Discount";
        private const string SummProperty = "Summ";
        private const string AmountProperty = "Amount";
        private const string PaidProperty = "Paid";
        private const string OddMoneyProperty = "OddMoney";
        private const string ApproveDateProperty = "ApproveDate";
        private const string GeneralInfoProperty = "GeneralInfo";
        private const string SupplierInfoProperty = "SupplierInfo";
        #endregion

        #region ImvoiceModel Internal properties
        private long _type;
        private long _creatorId;
        private string _creator;
        private DateTime _createDate = DateTime.Now;
        private long _memberId;
        private Guid? _partnerId;
        private string _providerName;
        private string _recipientName;
        private decimal? _discount;
        private decimal _summ = 0;
        private decimal _amount = 0;
        private decimal? _paid;
        private DateTime? _approveDate;
        private GeneralInfoModel _generalInfo = new GeneralInfoModel();
        private SupplierInfoModel _supplierInfo = new SupplierInfoModel();

        #endregion

        #region Invoice model External properties
        [XmlElementAttribute(IsNullable = false)]
        public long Type { get { return _type; } set { _type = value;  OnPropertyChanged(TypeProperty);} }
        [XmlIgnore]
        public DateTime CreateDate { get { return _createDate; } set { _createDate = value; OnPropertyChanged(CreateDateProperty); }}
        public GeneralInfoModel GeneralInfo
        {
            get { return _generalInfo; }
            set { if (Equals(value, _generalInfo)) { return;}
                _generalInfo = value; OnPropertyChanged(GeneralInfoProperty);
            }
        }
        public SupplierInfoModel SupplierInfo { get { return _supplierInfo; } set{if(Equals(value,_supplierInfo)){return;}
            _supplierInfo = value; OnPropertyChanged(SupplierInfoProperty);
        } }
        [XmlIgnore]
        public decimal Total { get { return _amount; } set { _amount = value; OnPropertyChanged(AmountProperty); OnPropertyChanged(OddMoneyProperty); } }
        public List<GoodInfos> GoodsInfo { get; set; }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

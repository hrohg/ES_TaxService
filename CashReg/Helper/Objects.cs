using CashReg.Enums;
using CashReg.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TaxServiceInvoices.Helper;

namespace CashReg.Helper
{
    #region Objects

    /// <summary>
    /// 
    /// </summary>
    public class Session
    {
        private string _key;
        /// <summary>
        /// 
        /// </summary>
        public string key { get { return _key; } set { _key = value; } }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EcrCashier : IEcrCashier
    {
        private int _cashier;
        private string _pin;
        public int Cashier { get { return _cashier; } set { _cashier = value; } }
        public string Pin { get { return _pin; } set { _pin = value; } }
        public EcrCashier()
        {

        }
        public EcrCashier(short cashier, string pin)
            : this()
        {
            _cashier = cashier;
            _pin = pin;
        }
    }

    /// <summary>
    /// Գործող օպերատոր
    /// </summary>
    [Serializable]
    public class Client
    {
        /// <summary>
        /// Օպերատորի ունիկալ համարը, որը նաև մուտքանունն է
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Օպերատորի անունը
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Օպերատորին վերագրած բաժինների ունիկալ համարները
        /// </summary>
        public int[] Deps { get; set; }
    }

    /// <summary>
    /// ՀԴՄ օպերատորների և բաժիններ
    /// </summary>
    [Serializable]
    public class UsersAndDepartments
    {
        /// <summary>
        /// Գործող օպերատորների ցանկը
        /// </summary>
        public List<Client> c;

        /// <summary>
        /// Բաժինների ցանկը:
        /// </summary>
        public List<Department> d;
    }

    /// <summary>
    /// Վճարային համակարգեր
    /// </summary>
    [Serializable]
    public class PaymentSystemsModel
    {
        //Վճարային համակարգեր
        public List<PaymentSystem> PaymentSystems;
    }

    /// <summary>
    /// Վճարային համակարգ
    /// </summary>
    [Serializable]
    public class PaymentSystem
    {
        /// <summary>
        /// Կոդ։ Առկա կոդերը ներկայացված են ստորև բերված աղյուսակում։
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// Անվանում
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// Բաժին
    /// </summary>
    [Serializable]
    public class Department : IEcrDepartment
    {
        /// <summary>
        /// Բաժնի ունիկալ համարը
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Բաժնի անվանումը
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Բաժնի հարկման տեսակ
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Բաժնի հարկման անվանումը
        /// </summary>
        [XmlIgnore]
        public string Description { get { return string.Format("{0} ({1})", Name, EnumHelper.GetEnumDescription((TypeOfTaxes)Type)); } }
    }


    public class ReceiptHeader
    {
        public int align = 1;
        public bool bold = false;
        public int fsize = 2;
        public string text = "Բարի գալուստ";
    }

    [Serializable]
    public class EcrProduct : IEcrProduct
    {
        #region Internal property
        private decimal _qty;
        private decimal _price;
        private decimal? _discount;
        private decimal? _additionalDiscount;
        private string _productName;
        private string _unit;
        private string _productCode;

        #endregion

        #region Extrnal properties
        public int dep { get; set; }
        public decimal qty { get { return _qty; } set { _qty = Math.Round(value, 3); } }
        public decimal price { get { return _price; } set { _price = Math.Round(value, 2); } }

        public decimal? discount
        {
            get { return _discount; }
            set { _discount = value!=null? Math.Round(value.Value, 2):(decimal?) null; }
        }

        public int? discountType { get; set; }

        public decimal? additionalDiscount
        {
            get { return _additionalDiscount; }
            set { _additionalDiscount = value != null ? Math.Round(value.Value, 2) : (decimal?)null; }
        }

        public int? additionalDiscountType { get; set; }
        
        public string productCode
        {
            get { return !string.IsNullOrEmpty(_productCode) && _productCode.Length > 50 ? _productCode.Substring(0, 50) : _productCode; }
            set { _productCode = value; }
        }

        public string productName
        {
            get { return !string.IsNullOrEmpty(_productName) && _productName.Length > 25 ? _productName.Substring(0, 25) : _productName; }
            set { _productName = value; }
        }
        public string adgCode { get; set; }
        public string unit
        {
            get { return !string.IsNullOrEmpty(_unit) && _unit.Length > 50 ? _unit.Substring(0, 50) : _unit; }
            set { _unit = value; }
        }

        #endregion

        #region Constructors
        public EcrProduct(string code, string product, string mu)
        {
            adgCode = null;
            productCode = code;
            productName = product;
            unit = mu;
        }
        #endregion
    }

    [Serializable]
    public class EcrPaid : IEcrPaid
    {
        #region Internal properties

        private double? _paidAmount;
        private double? _paidAmountCard;
        private double? _partialAmount = 0;
        private double? _prePaymentAmount = 0;
        #endregion

        #region External properties
        public double? PaidAmount
        {
            get { return _paidAmount; }
            set { _paidAmount = value.HasValue ? Math.Round(value.Value, 2) : (double?)null; }
        }
        public double? PaidAmountCard
        {
            get { return _paidAmountCard; }
            set { _paidAmountCard = value.HasValue? Math.Round(value.Value, 2): (double?)null;}
        }
        public double? PartialAmount
        {
            get { return _partialAmount; }
            set { _partialAmount = value.HasValue? Math.Round(value.Value, 2): (double?)null; }
        }
        public double? PrePaymentAmount
        {
            get { return _prePaymentAmount; }
            set { _prePaymentAmount = value.HasValue? Math.Round(value.Value, 2): (double?)null; ; }
        }
        public bool UseExtPos { get; set; }
        public int? PaymentSystem { get; set; }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EcrTickedAdditionalData : IEcrTickedAdditonalData
    {
        /// <summary>
        /// Գնորդի ՀՎՀՀ 8 նիշանոց թիվ կամ null
        /// </summary>
        public string PartnerTin { get; set; }

        /// <summary>
        /// Կտրոնում առկա ապրանքների emark կոդերը string-երի զանգվածի տեսքով
        /// </summary>
        public string[] EMarks { get; set; }
    }
    #endregion
}

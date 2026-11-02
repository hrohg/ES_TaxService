using CashReg.Helper;
using CashReg.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using TaxServiceInvoices.Helper;

namespace CashReg.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestPrintReceiptModel : IPrintTicketModel
    {
        #region Private properties
        private readonly int _seq;
        private readonly List<IEcrProduct> _products = new List<IEcrProduct>();
        #endregion
        #region public properties
        /// <summary>
        /// Հարցման հերթական համարը
        /// </summary>
        public int seq { get { return _seq; } }
        /// <summary>
        /// Առձեռն վճարված գումար
        /// </summary>
        public double paidAmount { get; set; }
        /// <summary>
        /// Անկանխիկ վճարված գումար
        /// </summary>
        public double paidAmountCard { get; set; }
        /// <summary>
        /// Մասնակի վճարման գումար
        /// </summary>
        public double partialAmount { get; set; }

        /// <summary>
        /// Կանխավճարի օգտագործման գումար
        /// </summary>
        public double prePaymentAmount { get; set; }

        /// <summary>
        /// 2 – Ապրանքներ ռեժիմ
        /// 3 - Կանխավճար
        /// </summary>
        public int mode { get; set; }

        /// <summary>
        /// Գնորդի ՀՎՀՀ 8 նիշանոց թիվ կամ null
        /// </summary>
        public string partnerTin { get; set; }

        /// <summary>
        /// Պարզ կտրոնի բաժին։ Ուղարկվում է միայն պարզ կտրոնի տպման դեպքում
        /// </summary>
        public int? dep { get; set; }

        /// <summary>
        /// Այլ վճարային տերմինալի օգտագործում
        /// Վերաբերվում է անկանխիկ վճարմանը(եթե արժեքը սխալ է, ՀԴՄ-ն միացնում է իր ներքին անկանխիկ վճարման ծրագիրը)
        /// </summary>
        public bool useExtPOS { get; set; }

        /// <summary>
        /// Վճարային համակարգի կոդը useExtPOS -ը false արժեքի դեպքում։
        ///Կարող է լինել null եթե ՀԴՄ սարքում առկա է միային մեկ վճարային համակարգ կամ անհրաժեշտ է ընտրել վճարային համակարգը ՀԴՄ սարքում։
        ///Կոդերը ներկայացված են «Վճարային համակարգերիկոդեր» աղյուսակում։
        /// </summary>
        public int? PaymentSystem { get; set; }

        /// <summary>
        /// Կտրոնում առկա ապրանքների emark կոդերը string-երի զանգվածի տեսքով
        /// </summary>
        public string[] eMarks { get; set; }

        /// <summary>
        /// Կտրոնի պարունակությունը (ապրանքներ) 
        /// </summary>
        public IList<IEcrProduct> items { get { return _products; } }

        #endregion
        public void Add(IEcrProduct item)
        {
            _products.Add(item);
        }
        public void AddRange(List<EcrProduct> productItems)
        {
            foreach (var productItem in productItems)
            {
                items.Add(productItem);
            }
        }
        public RequestPrintReceiptModel(int value)
        {
            _seq = value;
            partialAmount = 0;
            prePaymentAmount = 0;
        }

        public string GetRequest()
        {
            return EcrJsonConverters.ConvertToJson(this);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ResponseReceiptModel : IResponseReceiptModel
    {
        /// <summary>
        /// Կտրոնի հերթական համարը
        /// </summary>  
        [JsonProperty("rseq")]
        public long Rseq { get; set; }
        /// <summary>
        /// ՀԴՄ-ի գրանցման գամարը
        /// </summary>
        [JsonProperty("crn")]
        public string Crn { get; set; }
        /// <summary>
        /// ՀԴՄ գործարանային համարը
        /// </summary>
        [JsonProperty("sn")]
        public string Sn { get; set; }
        /// <summary>
        /// Կազմակերպության ՀՎՀՀ-ն
        /// </summary>
        [JsonProperty("tin")]
        public string Tin { get; set; }
        /// <summary>
        /// Կազմակերպության անվանումը
        /// </summary>
        [JsonProperty("taxpayer")]
        public string Taxpayer { get; set; }
        /// <summary>
        /// Կազմակերպության հասցեն
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Կտրոնի գրանցման/տպման ամսաթիվ ու ժամ Գրինվիչի ժամանակով
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// Վերադարձվող կտրոնի գրանցման/տպման ամսաթիվ ու ժամ Գրինվիչի ժամանակով
        /// </summary>
        [JsonProperty("rtime")]
        public long ReturnTime { get; set; }

        /// <summary>
        /// Ֆիսկալ համար
        /// </summary>
        [JsonProperty("fiscal")]
        public string Fiscal { get; set; }
        /// <summary>
        /// Վիճակահանության համարը
        /// </summary>
        [JsonProperty("lottery")]
        public string Lottery { get; set; }
        /// <summary>
        /// Շահում - Կտրոնների շահումը այլևս չի գործում
        /// 0 – շահում չկա - Այս դեպքում կտրոնի վրա պետք է տպել «Կտրոնը չի շահել»
        /// 1 – շահում կա - Այս դեպքում կտրոնի վրա պետք է տպել «Դուք շահել եք»
        /// </summary>
        [JsonProperty("prize")]
        public int Prize { get; set; }
        /// <summary>
        /// Ընդհանուր գումարը
        /// </summary>
        [JsonProperty("total")]
        public double Total { get; set; }
        /// <summary>
        /// Մանրադրամ
        /// </summary>
        [JsonProperty("change")]
        public double Change { get; set; }

        /// <summary>
        /// Անհրաժեշտ է տպել QR կոդի տեսքով
        /// </summary>
        [JsonProperty("qr")]
        public string Qr { get; set; }

        /// <summary>
        /// Հսկիչ նշանների քանակ
        /// </summary>
        [JsonProperty("emarksCount")]
        public string EmarksCount { get; set; }

        /// Կտրոնի ստուգիչ համար։
        /// Առավելագույնը 13 նիշ(բաղկացած է թվերից)
        [JsonProperty("verificationNumber")]
        public string VerificationNumber { get; set; }

        #region Constructors
        public ResponseReceiptModel()
        { }
        #endregion
    }

    public class ReceiptTicketViewModel
    {
        #region External properties
        public EcrModel EcrModel { get; set; }
        public ResponseReceiptModel ResponseReceipt { get; private set; }
        public string PrizeDescription { get { return ResponseReceipt.Prize == (int)Prize.Prize ? EnumHelper.GetEnumDescription(Prize.Prize) :  EnumHelper.GetEnumDescription(Prize.NoPrize); } }
        #endregion
        public ReceiptTicketViewModel(ResponseReceiptModel responseReceipt)
        {
            ResponseReceipt = responseReceipt;
        }
    }

}

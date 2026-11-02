using CashReg.Interfaces;
using Newtonsoft.Json;

namespace CashReg.Models
{
    /// <summary>
    /// ՀԴՄ վերադարձվող կտրոնի ստացում
    /// Հարցում
    /// </summary>
    public class ReturnReceiptRequestModel
    {
        #region External properties
        //Հարցման հերթական համարը
        public int seq { get; private set; }
        //Վերադարձվող կտրոնի համար
        public string receiptID { get; private set; }
        //Վերադարձվող կտրոն տպած սարքի գրանցման համար
        public string crn { get; private set; }
        #endregion
        public ReturnReceiptRequestModel(int seqValue, string receiptIdValue, string crnValue)
        {
            seq = seqValue;
            receiptID = receiptIdValue;
            crn = crnValue;
        }
    }

    /// <summary>
    /// ՀԴՄ վերադարձվող կտրոնի ստացում
    /// Պատասխան
    /// </summary>
    public class ReturnReceiptResponseModel : IReturnReceiptResponseModel
    {
        #region External properties
        //Վերադարձի կտրոնի հերթական համար
        public long rseq { get; set; }
        //Գանձապահի ID
        public string cid { get; set; }
        //Կտրոնի գրամցման / տպման ամսաթիվ ու ժամ Գրինվիչի ժամանակով
        public string time { get; set; }
        //Ընդմանեը գումար
        public string ta { get; set; }
        //Առձեռն մուտքագրված գումար
        public string cash { get; set; }
        //Անկանխիկ մուծված գումար
        public string card { get; set; }
        //Մասնակի վճարված գումար
        public string ppa { get; set; }
        //Օգտագործված կանխավճար
        public string ppu { get; set; }
        
        /// <summary>
        /// Գործարքի տիպ
        /// 0 – Վաճառք
        /// 2 – Վերադարձ
        /// 3 – Կանխավճար
        /// </summary>
        public string saleType { get; set; }

        /// <summary>
        /// Վաճառքի կտրոնի տիպը՝ 
        /// 1 – Պարզ 
        /// 2 - Ապրանքներով
        /// </summary>
        public int subType { get; set; }

        /// <summary>
        /// Պարզ կտրոնի բաժնի համար
        /// </summary>
        public int did { get; set; }

        /// <summary>
        /// Գնորդի ՀՎՀՀ 8 նիշանոց թիվ կամ null
        /// </summary>
        public string pTin { get; set; }

        //Վերադարձվող կտրոնի համար
        public string Ref { get; set; }

        //Վերադարձվող կտրոնի տպած ՀԴՄ-ի գրանցման համար
        public string refcrn { get; set; }

        /// <summary>
        /// Կտրոնում առկա ապրանքների emark կոդերը string-երի զանգվածի տեսքով
        /// </summary>
        public string[] eMarks { get; set; }

        //Կտրոնի պարունակությունը (ապրանքներ)
        public TotalModel[] totals { get; set; }
        #endregion
    }

    /// <summary>
    /// Կտրոնի պարունակությունը (ապրանքներ):
    /// </summary>
    public class TotalModel : ITotalModel
    {
        #region External properties
        //Բաժնի համար
        public string did { get; set; }
        //Բաժնի ԱԱՀ
        public string dt { get; set; }
        //Բաժնի հարկման ռեժիմ
        public string Dtm { get; set; }
        //Ընդամենը գումար առանց ԱԱՀ
        public string t { get; set; }
        //Ընդամենը գւմար ԱԱՀ-ով
        public string tt { get; set; }
        //Ապրանցի կոդ
        public string gc { get; set; }
        //Ապրանքի անուն
        public string gn { get; set; }
        //Ապրանքի քանակ
        public string qty { get; set; }
        //Ապրանքի գին
        public string p { get; set; }
        //ԱԴԳՏ կոդ
        public string adg { get; set; }
        //Ապրանքի չափման միավոր
        public string mu { get; set; }
        //Զեղչ
        public string dsc { get; set; }
        //Համամասնորեն զեղչ
        public string adsc { get; set; }
        //Զեղչի տեսակ
        public string dsct { get; set; }
        //Ապրանքներով տեղերի ID
        public string rpid { get; set; }
        
        public double Quantity { get => string.IsNullOrEmpty(qty) ? 0 : double.Parse(qty); }
        #endregion
    }

    /// <summary>
    /// ՀԴՄ վերադարձի կտրոնի տպում
    /// Կոդավորումը՝ սեսիայի բանալիով
    /// Հարցում՝
    /// </summary>
    public class PrintReturnReceiptRequestModel
    {
        #region External properties
        //Հարցման հերթական համար
        public int seq { get; private set; }

        //Սարքի գրանցման համար
        public string crn { get; private set; }
        //Վերադարձվող կտրոնի համար
        public int returnTicketId { get; private set; }

        /// <summary>
        /// //Վերադարձվող ապրանքների տվյալներ
        /// Տվյալներն ուղարկվում է միայն եթե տեղի է ունեցել ապրանքներով կտրոնի մասնակի վերադարձ
        /// </summary>
        
        public double? cashAmountForReturn { get; private set; }
        /// <summary>
        /// //Անկանխիկ վերադարձվող գումար
        /// Տվյալն ուղարկվում է եթե կատարվել է մասնակի վերադարձ
        /// </summary>
        public double? cardAmountForReturn { get; private set; }

        /// <summary>
        /// Օգտագործված կանխավճարի վերադարձվող գումար:
        /// Տվյալն ուղարկվում է եթե կատարվել է մասնակի վերադարձ
        /// </summary>
        public decimal? prePaymentAmountForReturn { get; private set; }

        /// <summary>
        /// Գործարքի նույնականացուցիչ:
        /// 12 նիշ
        /// </summary>
        public string rrn { get; set; }

        /// <summary>
        /// Վճարային տերմինալի նույնականացման համար
        /// 8 նիշ
        /// </summary>
        public string terminalId { get; set; }
        
        /// <summary>
        /// Կտրոնում առկա ապրանքների emark կոդերը string-երի զանգվածի տեսքով
        /// </summary>
        public string[] eMarks { get; set; }

        /// <summary>
        /// Վերադարձվող ապրանքների տվյալներ: Տվյալը ուղարկվում է միայն եթե տեղի է ունեցել ապրանքներով կտրոնի մասնակի վերադարձ:        
        /// </summary>
        public IEcrReturnItem[] returnItemList { get; private set; }

        /// <summary>
        /// //Առձեռն վերադարձվող գումար
        /// Տվյալն ուղարկվում է եթե կատարվել է մասնակի վերադարձ
        /// </summary>
        #endregion
        public PrintReturnReceiptRequestModel(int seqValue, string crnValue, int ticketId)
        {
            seq = seqValue;
            crn = crnValue;
            returnTicketId = ticketId;
        }
        public PrintReturnReceiptRequestModel(int seq, string crn, int returnTicketId, IEcrReturnItem[] returnItemList, double? cashAmountForReturn, double? cardAmountForReturn)
            : this(seq, crn, returnTicketId)
        {
            SetReturnItems(returnItemList, cashAmountForReturn, cardAmountForReturn);
        }
        #region External methods
        public void SetReturnItems(IEcrReturnItem[] itemList, double? cashAmount, double? cardAmount)
        {
            returnItemList = itemList;
            cashAmountForReturn = cashAmount;
            cardAmountForReturn = cardAmount;
        }
        #endregion
    }

    /// <summary>
    /// Վերադարձվող ապրանքների տվյալներ: Տվյալը
    /// ուղարկվում է միայն եթե տեղի է ունեցել
    /// ապրանքներով կտրոնի մասնակի վերադարձ:
    /// </summary>
    public class ReturnItem : IEcrReturnItem
    {
        #region External properties
        //Ապրանքի տեղի հերթական համար
        [JsonProperty("rpid")]
        public long RpId { get; private set; }
        //Տվյալ տողի ապրանքի քանակը
        [JsonProperty("quantity")]
        public double Quantity { get; private set; }
        #endregion
        public ReturnItem(int rpId, double qty)
        {
            RpId = rpId;
            Quantity = qty;
        }

        [JsonIgnore]
        public string[] Emarks { get; set; }
    }
}

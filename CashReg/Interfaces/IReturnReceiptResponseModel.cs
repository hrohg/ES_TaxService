using CashReg.Models;

namespace CashReg.Interfaces
{
    /// <summary>
    /// ՀԴՄ վերադարձվող կտրոն
    /// </summary>
    public interface IReturnReceiptResponseModel
    {
        /// <summary>
        /// Վերադարձի կտրոնի հերթական համար
        /// </summary>
        long rseq { get; set; }

        /// <summary>
        /// Գանձապահի ID
        /// </summary>
        string cid { get; set; }

        /// <summary>
        /// Կտրոնի գրանցման/տպման ամսաթիվ ու ժամ
        /// </summary>
        string time { get; set; }

        /// <summary>
        /// Ընդհամենը գումար
        /// </summary>
        string ta { get; set; }

        /// <summary>
        /// Առձեռն մուծված գումար
        /// </summary>
        string cash { get; set; }

        /// <summary>
        /// Անկանխիկ մուծված գումար
        /// </summary>
        string card { get; set; }

        /// <summary>
        /// Մասնակի վճարված գումար
        /// </summary>
        string ppa { get; set; }

        /// <summary>
        /// Օգտագործված կանխավճար
        /// </summary>
        string ppu { get; set; }

        /// <summary>
        /// Գործարքի տիպ՝ 0 – Վաճառք, 2 – Վերադարձ, 3 – Կանխավճար
        /// </summary>
        string saleType { get; set; }

        /// <summary>
        /// Գնորդի ՀՎՀՀ 8 նիշանոց թիվ կամ null
        /// </summary>
        string pTin { get; set; }

        /// <summary>
        /// Վերադարդարձվող կտրոնի համար
        /// </summary>
        string Ref { get; set; }

        /// <summary>
        /// Վերադարձվող կտրոն տպած ՀԴՄ-ի գրանցման համար
        /// </summary>
        string refcrn { get; set; }

        /// <summary>
        /// Կտրոնի պարունակությունը (ապրանքներ): Եթե կանխավճար է, ապա null: Ստորև նկարագրված է յուրաքանչյուր totals-ի նկարագրությունը:
        /// </summary>
        TotalModel[] totals { get; set; }
    }
}
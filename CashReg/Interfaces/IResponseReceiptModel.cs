namespace CashReg.Interfaces
{
    /// <summary>
    /// Պատասխան
    /// </summary>
    public interface IResponseReceiptModel
    {
        /// <summary>
        /// Կտրոնի հերթական համար
        /// </summary>
        long Rseq { get; set; }

        /// <summary>
        /// ՀԴՄ-ի գրանցման համարը
        /// </summary>
        string Crn { get; set; }

        /// <summary>
        /// ՀԴՄ գործարանային համարը
        /// </summary>
        string Sn { get; set; }

        /// <summary>
        /// Կազմակերպության ՀՎՀՀ-ն
        /// </summary>
        string Tin { get; set; }

        /// <summary>
        /// Կազմակերպության անվանումը
        /// </summary>
        string Taxpayer { get; set; }

        /// <summary>
        /// Կազմակերպության հասցեն
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Կտրոնի գրանցման/տպման ամսաթիվ ու ժամ
        /// </summary>
        long Time { get; set; }

        /// <summary>
        /// Ֆիսկալ համար
        /// </summary>
        string Fiscal { get; set; }

        /// <summary>
        /// Վիճակահանության համարը
        /// </summary>
        string Lottery { get; set; }

        /// <summary>
        /// 0 – շահում չկա (Այս դեպքում կտրոնի վրա պետք է տպել «Կտրոնը չի շահել»)
        /// 1 – շահում կա (Այս դեպքում կտրոնի վրա պետք է տպել «Դուք շահել եք»)
        /// </summary>
        int Prize { get; set; }

        /// <summary>
        /// Ընդհանուր գումար
        /// </summary>
        double Total { get; set; }

        /// <summary>
        /// Մանրադրամ
        /// </summary>
        double Change { get; set; }

        /// <summary>
        /// Անհրաժեշտ է տպել QR կոդի տեսքով
        /// </summary>
        string Qr { get; set; }

        /// <summary>
        /// Հսկիչ նշանների քանակ
        /// </summary>
        string EmarksCount { get; set; }

        /// Կտրոնի ստուգիչ համար։
        /// Առավելագույնը 13 նիշ(բաղկացած է թվերից)
        string VerificationNumber { get; set; }
    }
}
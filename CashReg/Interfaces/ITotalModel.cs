namespace CashReg.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITotalModel
    {
        /// <summary>
        /// Բաժնի համար
        /// </summary>
        string did { get; set; }

        /// <summary>
        /// Բաժնի ԱԱՀ
        /// </summary>
        string dt { get; set; }

        /// <summary>
        /// Բաժնի հարկման ռեժիմ
        /// </summary>
        string Dtm { get; set; }

        /// <summary>
        /// Ընդամենը գումար առանց ԱԱՀ
        /// </summary>
        string t { get; set; }

        /// <summary>
        /// Ընդամենը գումար ԱԱՀ-ով
        /// </summary>
        string tt { get; set; }

        /// <summary>
        /// Ապրանքի կոդ
        /// </summary>
        string gc { get; set; }

        /// <summary>
        /// Ապրանքի անուն
        /// </summary>
        string gn { get; set; }

        /// <summary>
        /// Ապրանքի քանակ
        /// </summary>
        string qty { get; set; }

        /// <summary>
        /// Ապրանքի գին
        /// </summary>
        string p { get; set; }

        /// <summary>
        /// ԱՏԳ կոդ
        /// </summary>
        string adg { get; set; }

        /// <summary>
        /// Ապրանքի չափման միավոր
        /// </summary>
        string mu { get; set; }

        /// <summary>
        /// Զեղչ
        /// </summary>
        string dsc { get; set; }

        /// <summary>
        /// Համամասնորեն զեղչ
        /// </summary>
        string adsc { get; set; }

        /// <summary>
        /// Զեղչի տեսակ
        /// </summary>
        string dsct { get; set; }

        /// <summary>
        /// Ապրանքներով տողերի ID
        /// </summary>
        string rpid { get; set; }
    }
}
namespace CashReg.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    //[ComVisible(true)]
    public interface IEcrCashier
    {
        /// <summary>
        /// Օպերատորի կոդ
        /// </summary>
        int Cashier { get; set; }
        /// <summary>
        /// Օպերատորի գաղտնաբառ
        /// </summary>
        string Pin { get; set; }
    }
}
namespace CashReg.Models
{
    /// <summary>
    /// 4.5.5 Կանխիկի մուտք, կանխիկի ելք
    /// </summary>
    public class CashModel
    {
        #region Internal properties
        #endregion

        #region External properties
        /// <summary>
        /// Հարցման հերթական համար
        /// </summary>
        public long seq { get; set; }

        /// <summary>
        /// Գումարի չափ
        /// </summary>
        public double amount { get; set; }

        /// <summary>
        /// True-ի դեպքում կանխիկի մուտք է False-ի դեպքում կանխիկի ելք
        /// </summary>
        public bool isCashIn { get; set; }

        /// <summary>
        /// Գանձապահի համար
        /// </summary>
        public int cashierId { get; set; }

        /// <summary>
        /// Մեկնաբանություն
        /// </summary>
        public string description { get; set; }
        #endregion

        #region Constructors
        public CashModel(long seqValue, double amountValue, bool isCashInValue, int cashierId, string desc)
        {
            seq = seqValue;
            amount = amountValue;
            isCashIn = isCashInValue;
            description = desc;
        }
        #endregion
    }
}

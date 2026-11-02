using Newtonsoft.Json;

namespace CashReg.Interfaces
{
    /// <summary>
    /// Վերադարձվող ապրանքների տվյալներ:
    /// </summary>
    //[ComVisible(true)]
    public interface IEcrReturnItem
    {
        /// <summary>
        /// Ապրանքներով տողերի ID
        /// </summary>
        [JsonProperty("rpid")]
        long RpId { get; }
        /// <summary>
        /// Ապրանքի քանակ
        /// </summary>
        [JsonProperty("quantity")]
        double Quantity { get; }

        [JsonIgnore]
        string[] Emarks { get; set; }
    }
}
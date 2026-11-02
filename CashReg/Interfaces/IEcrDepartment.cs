using System.Runtime.InteropServices;

namespace CashReg.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    //[ComVisible(true)]
    public interface IEcrDepartment
    {
        /// <summary>
        /// Բաժնի եզակի համար
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Բաժնի անվանում
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Բաժնի հարկման տեսակ
        /// </summary>
        int Type { get; set; }
    }

    
}
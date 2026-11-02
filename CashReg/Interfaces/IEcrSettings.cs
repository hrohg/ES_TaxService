using System.Collections.Generic;
using System.Runtime.InteropServices;
using CashReg.Helper;

namespace CashReg.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    //[ComVisible(true)]
    public interface IEcrSettings
    {
        /// <summary>
        /// ՀԴՄ IP հասցե
        /// </summary>
        string Ip { get; set; }
        /// <summary>
        /// ՀԴՄ port
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// ՀԴՄ գաղտնաբառ
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// ՀԴՄ գրանցման համար
        /// </summary>
        string Crn { get; set; }
        /// <summary>
        /// ՀԴՄ օպերատոր
        /// </summary>
        EcrCashier EcrCashier { get; set; }
        /// <summary>
        /// Օպերատորնորին կցված բաժիններ
        /// </summary>
        List<Department> TypeOfOperatorDeps { get; set; }
        /// <summary>
        /// Օպերատորնորին կցված բաժին
        /// </summary>
        Department CashierDepartment { get; set; }
        /// <summary>
        /// Տպել կտրոնը արտաքին տպիչով
        /// </summary>
        bool UseExternalPrinter { get; set; }
        /// <summary>
        /// Ավտոմատ համակարգի IP հասցեն
        /// </summary>
        string ApplicationIp { get; set; }
    }
}

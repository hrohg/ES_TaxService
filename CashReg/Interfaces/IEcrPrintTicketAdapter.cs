using System.Collections.Generic;
using CashReg.Helper;
using CashReg.Models;

namespace CashReg.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    //[ComVisible(true)]
    public interface IEcrPrintTicketAdapter
    {
        #region Connections

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        //[ComVisible(true)]
        bool CreateConnection(IEcrSettings settings);
        
        /// <summary>
        /// ՀԴՄ օպերատորորի մուտք
        /// </summary>
        /// <returns></returns>
        bool TryOperatorLogin();       

        /// <summary>
        /// ՀԴՄ օպերատորորի ելք
        /// </summary>
        /// <returns></returns>
        bool OperatorLogout();
        #endregion

        //#region Print
        ///// <summary>
        ///// ՀԴՄ կտրոնի տպում
        ///// </summary>
        ///// <param name="productItems"></param>
        ///// <param name="paid"></param>
        ///// <returns></returns>
        //ResponseReceiptModel PrintReceiptTicket(List<IEcrProduct> productItems, IEcrPaid paid, IEcrTickedAdditonalData tickedAdditonalData);

        ///// <summary>
        ///// ՀԴՄ վերջին կտրոնի կրկնօրինակի տպում
        ///// </summary>
        ///// <param name="getResponse"></param>
        ///// <returns></returns>
        //bool PrintReceiptLatestCopy(bool getResponse = true);

        ///// <summary>
        ///// ՀԴՄ վերադարձվող կտրոնի ստացում
        ///// </summary>
        ///// <param name="returnTicketValue"></param>
        ///// <returns></returns>
        //ReturnReceiptResponseModel GetReceiptReturnTicket(string returnTicketValue, string crn);
        ///// <summary>
        ///// ՀԴՄ վերադարձի կտրոնի ստացում, տպում
        ///// </summary>
        ///// <param name="returnAll"></param>
        ///// <returns></returns>
        //bool PrintReceiptReturnTicket(string crn, bool? returnAll = null);

        ///// <summary>
        ///// ՀԴՄ վերադարձի կտրոնի տպում
        ///// </summary>
        ///// <param name="returnTicketValue"></param>
        ///// <param name="returnItemsList"></param>
        ///// <param name="paid"></param>
        ///// <returns></returns>
        //ResponseReceiptModel PrintReceiptReturnTicket(int returnTicketValue, IEcrReturnItem[] returnItemsList, EcrPaid paid);

        ///// <summary>
        ///// ՀԴՄ վերադարձի կտրոնի տպում
        ///// </summary>
        ///// <param name="returnItemsList"></param>
        ///// <param name="paid"></param>
        ///// <returns></returns>
        //ResponseReceiptModel PrintReceiptReturnTicket(List<TotalModel> returnItemsList, EcrPaid paid);

        ///// <summary>
        ///// ՀԴՄ կտրոնի տպում Excel ֆայլից
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <param name="addVat"></param>
        ///// <returns></returns>
        //bool PrintReceiptFromExcelFile(string filePath = null, bool addVat = true, IEcrTickedAdditonalData tickedAdditonalData = null);
        //#endregion

        //#region Report
        ///// <summary>
        ///// ՀԴՄ հաշվետվության տպում
        ///// </summary>
        ///// <param name="report"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //bool GetReport(ReportType report, long startDate, long endDate);
        ///// <summary>
        ///// ՀԴՄ հաշվետվության տպում
        ///// </summary>
        ///// <param name="report"></param>
        ///// <returns></returns>
        //bool GetReport(ReportType report);

        ///// <summary>
        ///// ՀԴՄ X հաշվետվության տպում
        ///// </summary>
        ///// <returns></returns>
        //bool GetReportX();

        ///// <summary>
        ///// ՀԴՄ X հաշվետվության տպում
        ///// </summary>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //bool GetReportX(int startDate, int endDate);

        ///// <summary>
        ///// ՀԴՄ Z հաշվետվության տպում
        ///// </summary>
        ///// <returns></returns>
        //bool GetReportZ();

        ///// <summary>
        ///// ՀԴՄ Z հաշվետվության տպում
        ///// </summary>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //bool GetReportZ(int startDate, int endDate);
        //#endregion

        //#region Cash receipts or withdrawal
        ///// <summary>
        ///// Կանխիկի մուտք
        ///// </summary>
        ///// <param name="amount"></param>
        ///// <param name="description"></param>
        ///// <returns></returns>
        //bool SetCashReceipt(decimal amount, string description);
        ///// <summary>
        ///// Կանխիկի ելք
        ///// </summary>
        ///// <param name="amount"></param>
        ///// <param name="description"></param>
        ///// <returns></returns>
        //bool SetCashWithdrawal(decimal amount, string description);
        //#endregion

        //#region Evens
        ///// <summary>
        ///// On Error event
        ///// </summary>
        //event Classes.EcrAcapterBase.OnErrorDelegate OnErrorEvent;
        //#endregion Events
    }
}
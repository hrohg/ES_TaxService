using System;
using System.Collections.Generic;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Models;

namespace Ecr.Manager.Interfaces
{
    public delegate void OnMessageDelegate(string message);
    public delegate void OnErrorDelegate(Exception ex);
    /// <summary>
    /// 
    /// </summary>
    //[ComVisible(true)]
    public interface IEcrManager
    {
        #region Evens
        /// <summary>
        /// On Error event
        /// </summary>
        event OnMessageDelegate OnMessageEvent;
        event OnErrorDelegate OnErrorEvent;
        #endregion Events

        EcrResponse Result { get; }
        #region Connections

        /// <summary>
        /// Կապի ստուգում
        /// </summary>
        /// <returns></returns>
        //[ComVisible(true)]
        bool TryConnection();

        /// <summary>
        /// ՀԴՄ կապի ստուգում
        /// </summary>
        /// <returns></returns>
        bool TryEcrConnection();

        /// <summary>
        /// ՀԴՄ օպերատորորի մուտք
        /// </summary>
        /// <returns></returns>
        bool TryOperatorLogin();

        #endregion

        #region Print
        /// <summary>
        /// ՀԴՄ կտրոնի տպում
        /// </summary>
        /// <param name="productItems"></param>
        /// <param name="paid"></param>
        /// <returns></returns>
        ResponseReceiptModel PrintReceiptTicket(List<IEcrProduct> productItems, IEcrPaid paid, IEcrTickedAdditonalData tickedAdditonalData);
        /// <summary>
        /// ՀԴՄ վերջին կտրոնի կրկնօրինակի տպում
        /// </summary>
        /// <param name="getResponse"></param>
        /// <returns></returns>
        bool PrintReceiptLatestCopy(bool getResponse = true);
        #endregion

        #region Return recipt ticket

        /// <summary>
        /// ՀԴՄ վերադարձվող կտրոնի ստացում
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <returns></returns>
        ReturnReceiptResponseModel GetReceiptReturnTicket(string returnTicketValue, string crn = null);

        /// <summary>
        /// ՀԴՄ վերադարձի կտրոնի ստացում, տպում
        /// </summary>
        /// <param name="returnAll"></param>
        /// <returns></returns>
        void PrintReceiptReturnTicket(Action<string> callback = null);

        /// <summary>
        /// 4.5.4 ՀԴՄ վերադարձի կտրոնի տպում
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <param name="returnItemsList"></param>
        /// <param name="paid"></param>
        /// <returns></returns>
        ResponseReceiptModel PrintReceiptReturnTicket(int returnTicketValue, IEcrReturnItem[] returnItemsList, EcrPaid paid);

        /// <summary>
        /// ՀԴՄ կտրոնի տպում Excel ֆայլից
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="addVat"></param>
        /// <returns></returns>
        bool PrintReceiptFromExcelFile(string filePath = null, bool addVat = true, IEcrTickedAdditonalData tickedAdditonalData = null);
        #endregion Return receipt ticket

        #region Report
        /// <summary>
        /// ՀԴՄ հաշվետվության տպում
        /// </summary>
        /// <param name="report"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        bool GetReport(ReportType report, long startDate, long endDate);
        /// <summary>
        /// ՀԴՄ հաշվետվության տպում
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        bool GetReport(ReportType report);

        /// <summary>
        /// ՀԴՄ X հաշվետվության տպում
        /// </summary>
        /// <returns></returns>
        bool GetReportX();

        /// <summary>
        /// ՀԴՄ X հաշվետվության տպում
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        bool GetReportX(int startDate, int endDate);

        /// <summary>
        /// ՀԴՄ Z հաշվետվության տպում
        /// </summary>
        /// <returns></returns>
        bool GetReportZ();

        /// <summary>
        /// ՀԴՄ Z հաշվետվության տպում
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        bool GetReportZ(int startDate, int endDate);
        #endregion

        #region Cash receipts or withdrawal
        /// <summary>
        /// Կանխիկի մուտք
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        bool SetCashReceipt(double amount, string description);
        /// <summary>
        /// Կանխիկի ելք
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        bool SetCashWithdrawal(double amount, string description);
        #endregion

        #region Ecr data

        /// <summary>
        /// ՀԴՄ օպերատորների և բաժինների ստացում
        /// </summary>
        /// <returns></returns>
        UsersAndDepartments GetUsersDepsList();

        /// <summary>
        /// 4.6 Սարքի ամսաթվի և ժամի ստացում
        /// </summary>
        /// <returns></returns>
        string GetEcrDateTime();

        /// <summary>
        /// 4.6.1 Կտրոնի ձևանմուշի տպում
        /// </summary>
        void PrintBlankTicket();

        /// <summary>
        /// 4.7 ՀԴՄ համաժամանակեցում
        /// </summary>
        bool EcrSyncronization();

        /// <summary>
        /// 4.8 ՀԴՄ սարքում առկա վճարային համակարգերի ցանկի ստացում
        /// (պահանջվում է ընթացիկ սեսիա)։
        /// </summary>
        /// <returns></returns>
        List<PaymentSystem> GetPaymentSystems();

        #endregion Ecr data
    }
}
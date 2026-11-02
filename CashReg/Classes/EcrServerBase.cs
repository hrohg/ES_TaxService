using System;
using System.Collections.Generic;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Managers;
using CashReg.Models;

namespace CashReg.Classes
{
    /// <summary>
    /// Ecr Server Base
    /// </summary>
    //[ComVisible(true)]
    public abstract class EcrServerBase : IEcrServer
    {
        protected IEcrSettings EcrSettings;
        protected readonly string ServerLogPath;
        protected readonly string ErrorLogPath;

        protected EcrException.ResponseCodes ErrorCode;
        protected RequestsManager Manager;
        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        protected EcrServerBase()
        {
            //ServerLogPath = serverLogPath;
            ErrorLogPath = AppDomain.CurrentDomain.BaseDirectory + @"EcrErrorLog.txt";
        }

        protected void Initialize()
        {
            CreateConnection(EcrSettings);
            if (Manager == null) return;
            Manager.OnErrorReceived += OnError;
            Manager.OnException += OnException;
        }

        protected RequestsManager GetRequestsManager(IEcrSettings ecrSettings)
        {
            return new RequestsManager(ecrSettings);
        }

        #region Connections
        /// <summary>
        /// Initializing server and connecting
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public bool CreateConnection(IEcrSettings settings)
        {
            if (settings == null) return false;
            EcrSettings = settings;
            Manager = GetRequestsManager(settings);
            return (Manager != null);
        }

        /// <summary>
        /// Կապի ստուգում
        /// </summary>
        /// <returns></returns>
        public abstract bool TryConnection();
        /// <summary>
        /// ՀԴՄ կապի ստուգում
        /// </summary>
        /// <returns></returns>
        public abstract bool TryEcrConnection();
        /// <summary>
        /// ՀԴՄ օպերատորորի մուտք
        /// </summary>
        /// <returns></returns>
        public abstract bool TryOperatorLogin();
        /// <summary>
        /// ՀԴՄ օպերատորների և բաժինների ստացում
        /// </summary>
        /// <returns></returns>
        public abstract UsersAndDepartments GetUsersDepsList();
        /// <summary>
        /// ՀԴՄ օպերատորորի ելք
        /// </summary>
        /// <returns></returns>
        public abstract bool OperatorLogout();
        #endregion

        #region Print

        /// <summary>
        /// ՀԴՄ կտրոնի տպում
        /// </summary>
        /// <param name="productItems"></param>
        /// <param name="paid"></param>
        /// <param name="partnerTin"></param>
        /// <returns></returns>
        //[ComVisible(true)]
        public abstract ResponseReceiptModel PrintReceipt(List<IEcrProduct> productItems, IEcrPaid paid, string partnerTin);
        /// <summary>
        /// ՀԴՄ վերջին կտրոնի կրկնօրինակի տպում
        /// </summary>
        /// <param name="getResponse"></param>
        /// <returns></returns>
        //[ComVisible(true)]
        public abstract bool PrintReceiptLatestCopy(bool getResponse = true);
        /// <summary>
        /// ՀԴՄ վերադարձվող կտրոնի ստացում
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <returns></returns>
        public abstract ReturnReceiptResponseModel GetReceiptReturnTicket(string returnTicketValue);
        /// <summary>
        /// ՀԴՄ վերադարձի կտրոնի ստացում, տպում
        /// </summary>
        /// <param name="returnAll"></param>
        /// <returns></returns>
        public abstract bool PrintReceiptReturnTicket(bool? returnAll = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnItemsList"></param>
        /// <param name="paid"></param>
        /// <returns></returns>
        public abstract ResponseReceiptModel PrintReceiptReturnTicket(IEcrReturnItem[] returnItemsList, EcrPaid paid);
        /// <summary>
        /// ՀԴՄ վերադարձի կտրոնի տպում
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <param name="returnItemsList"></param>
        /// <param name="paid"></param>
        /// <returns></returns>
        public abstract ResponseReceiptModel PrintReceiptReturnTicket(int returnTicketValue, IEcrReturnItem[] returnItemsList, EcrPaid paid);

        /// <summary>
        /// ՀԴՄ վերադարձի կտրոնի տպում
        /// </summary>
        /// <param name="returnItemsList"></param>
        /// <param name="paid"></param>
        /// <returns></returns>
        public abstract ResponseReceiptModel PrintReceiptReturnTicket(List<TotalModel> returnItemsList, EcrPaid paid);

        /// <summary>
        /// ՀԴՄ կտրոնի տպում Excel ֆայլից
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="addVat"></param>
        /// <param name="partnerTin"></param>
        /// <returns></returns>
        public abstract bool PrintReceiptFromExcelFile(string filePath = null, bool addVat = true, string partnerTin=null);
        #endregion

        #region Report
        /// <summary>
        /// ՀԴՄ հաշվետվության տպում
        /// </summary>
        /// <param name="report"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public abstract bool GetReport(ReportType report, long startDate, long endDate);
        /// <summary>
        /// ՀԴՄ հաշվետվության տպում
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public abstract bool GetReport(ReportType report);
        /// <summary>
        /// ՀԴՄ X հաշվետվության տպում
        /// </summary>
        /// <returns></returns>
        public bool GetReportX()
        {
            return GetReport(ReportType.X);
        }
        /// <summary>
        /// ՀԴՄ X հաշվետվության տպում
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GetReportX(int startDate, int endDate)
        {
            return GetReport(ReportType.X, startDate, endDate);
        }
        /// <summary>
        /// ՀԴՄ Z հաշվետվության տպում
        /// </summary>
        /// <returns></returns>
        public bool GetReportZ()
        {
            return GetReport(ReportType.Z);
        }
        /// <summary>
        /// ՀԴՄ Z հաշվետվության տպում
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GetReportZ(int startDate, int endDate)
        {
            return GetReport(ReportType.Z, startDate, endDate);
        }
        #endregion

        #region Cash receipts or withdrawal
        /// <summary>
        /// Կանխիկի մուտք
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public abstract bool SetCashReceipt(decimal amount, string description);
        /// <summary>
        /// Կանխիկի ելք
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public abstract bool SetCashWithdrawal(decimal amount, string description);
        #endregion

        protected abstract void OnError(string message);
        protected abstract void OnException(string message);

        #region Evens

        /// <summary>
        /// On Error Method
        /// </summary>
        /// <param name="ex"></param>
        public delegate void OnErrorDelegate(Exception ex);
        /// <summary>
        /// On Error event
        /// </summary>
        public abstract event OnErrorDelegate OnErrorEvent;
        #endregion Events
    }
}

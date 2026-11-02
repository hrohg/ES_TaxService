using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Managers;
using System;

namespace CashReg.Adapters
{
    public class EcrReportAdapter : EcrAdapterBase
    {
        public EcrReportAdapter(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback) : base(ecrSettings, messageCallback, exceptionCallback) { }

        #region Ecternal methds
        /// <summary>
        /// 4.6.2 ՀԴՄ հաշվետվության տպում
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// </summary>
        /// <param name="report"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GetReport(ReportType report, long startDate, long endDate)
        {
            Manager.OperatorLogin();
            var reportManager = new EcrReportManager(report, startDate, endDate, Manager);
            reportManager.GetReport();
            Result = Manager.Response;
            Manager.Dispose();
            return (Result.Code == EcrResponse.ResponseCodes.Ok);
        }
        #endregion External methods
    }
}

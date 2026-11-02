using CashReg.Helper;
using CashReg.Models;
using System;

namespace CashReg.Managers
{
    public class EcrReportManager
    {
        #region Internal properies
        private ReportType _reportType;
        private long _startDate;
        private long _endDate;
        private RequestsManager _manager;
        #endregion //Internal proerpties

        #region Constructors
        public EcrReportManager(ReportType reportType, long startDate, long endDate, RequestsManager manager)
        {
            _reportType = reportType;
            _startDate = startDate;
            _endDate = endDate;
            _manager = manager;
            //base.OperatorLogin();
        }
        #endregion //Constructors

        #region External methods
        public bool GetReport()
        {
            if (_manager==null || !_manager.IsOperatorLogin) {return false;}
            var requestStream = GetRequestStream();
            try
            {
                _manager.Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                _manager.Socket.Receive(receave);
                var response = new EcrResponseModel(receave, _manager.PasswordKey);
                _manager.Response = new EcrResponse(response.ResponseCode);
                return true;
            }
            catch (Exception ex)
            {
                _manager.Response = new EcrResponse(EcrResponse.ResponseCodes.EcrRequestException);
                MessageManager.ShowMessage(ex.Message, "Ecr report manager");
                return false;
            }
        }

        public byte[] GetRequestStream()
        {
            var requestObject = new EcrReportRequestModel(++_manager.Seq, _reportType, _startDate, _endDate);
            var request = new EcrRequestModel(ManagerBase.OP_PRINT_REPORT, requestObject, _manager.SessionKey);
            return request.GetRequest();
        }
        #endregion
       
    }
}

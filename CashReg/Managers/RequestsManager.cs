using System;
using System.Collections.Generic;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Models;

namespace CashReg.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestsManager : ManagerBase
    {
        #region protected ObjectMapper mapper;

        protected readonly IEcrCashier _ecrCashier;
        protected readonly IEcrDepartment _cashierDepartment;
        #endregion

        #region External properties
        public bool IsOperatorLogin { get; private set; }
        #endregion

        #region Constructors
        public RequestsManager()
            : base()
        { }
        public RequestsManager(IEcrSettings ecrSettings, Action<string> errorCallBack, Action<string> exceptionCallBack)
            : base(ecrSettings, errorCallBack, exceptionCallBack)
        {
            if (ecrSettings == null)
            {
                Response = new EcrResponse(EcrResponse.ResponseCodes.EcrSettingsError);
                return;
            }
            _ecrCashier = ecrSettings.EcrCashier;
            _cashierDepartment = ecrSettings.CashierDepartment;
            if (_ecrCashier == null)
            {
                Response = new EcrResponse(EcrResponse.ResponseCodes.EcrCashierSettingsError);
            }
        }
        #endregion //Constructors

        #region Destructors
        ~RequestsManager()
        {
            Dispose();
        }
        public override void Dispose()
        {
            if (IsOperatorLogin)
            {
                OperatorLogout();
            }
            if (IsConnected)
            {
                Disconnect();
            }
        }
        #endregion //Destructors

        // Protected implementation of Dispose pattern.

        //Methods requests and responses

        #region Internal methods

        private bool TryGetOperatorLogin()
        {
            if (!IsConnected)
            {
                Response = new EcrResponse(EcrResponse.ResponseCodes.NetworkConnectionError);
                return false;
            }

            var requestObject = new OperatorLoginModel.RequestOperatorLogin(_ecrCashier, Password);
            var requestModel = new EcrRequestModel(OP_OPS_LOGIN, requestObject, PasswordKey);
            var request = requestModel.GetRequest();
            try
            {
                Socket.Send(request, request.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, PasswordKey);
                if (!response.HasResponse)
                {
                    Response = new EcrResponse(response.ResponseCode);
                    SendError(Response.Code, "", response.GetResponseString, requestModel.GetRequestByJson);
                    return false;
                }
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                var session = response.GetResponse<Session>();
                SetSessionKey(Convert.FromBase64String(session.key));
                Response = new EcrResponse(response.ResponseCode);
                return IsOperatorLogin = SessionKey != null;
            }
            catch (Exception ex)
            {
                SendException("Login: " + ex.Message);
                Response = new EcrResponse(EcrResponse.ResponseCodes.UnknownError);
                return false;
            }
        }
        #endregion

        #region External methods

        #region Socket send receive method
        public bool TrySendRequest(byte[] requestStream)
        {
            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public byte[] TryReceiveResponse(long len = 11)
        {
            try
            {
                var receave = new byte[len];
                Socket.Receive(receave);
                return receave;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public byte[] GetCheckEcrConnectionRequest()
        {
            var request = new EcrRequestModel((byte)EcrExecuiteActions.CheckEcrConnection, null, PasswordKey);
            return request.GetRequest();
        }
        //public T ParseResponce<T>(byte[] responseStream)
        //{
        //    if (responseStream == null) return default(T);
        //    var response = new EcrResponseModel(responseStream, PasswordKey);
        //    var value = response.GetResponse<T>();
        //    return value;
        //}
        public EcrResponse GetResponce(byte[] responseStream)
        {
            var responceCode = EcrResponse.ResponseCodes.UnknownError;
            if (responseStream == null)
            {
                responceCode = EcrResponse.ResponseCodes.UNKNOWN_RESPONSE_CODE;
            }
            else
            { responceCode = new EcrResponseModel(responseStream, PasswordKey).ResponseCode; }

            return new EcrResponse(responceCode);
        }
        #endregion Request method

        public void TryCheckEcrConnection()
        {
            if (!IsConnected)
            {
                Response = new EcrResponse(EcrResponse.ResponseCodes.NetworkConnectionError);
                return;
            }
            if (TrySendRequest(GetCheckEcrConnectionRequest()))
            {
                var response = TryReceiveResponse();
                Response = GetResponce(response);
            }
        }

        /// <summary>
        /// 4.5.1 ՀԴՄ օպերատորների և բաժինների ստացում
        /// Այս հրահանգով հնարավոր է ստանալ ՀԴՄ-ի գործող և ակտիվ բոլոր օպերատորների և
        /// բաժինների ցանկը, ինչպես նաև օպերատորնորին կցված բաժինների ցանկ
        /// Կոդավորումը՝ գաղտնաբառով
        /// </summary>
        /// <returns></returns>
        public UsersAndDepartments GetOperatorsAndDeps()
        {
            var requestObject = new RequestGetOperatorsAndDeps(Password);
            var requestModel = new EcrRequestModel(OP_LIST_OPS_AND_DEPS, requestObject, PasswordKey);
            //OnLog(requestModel.GetRequestByJson);
            var request = requestModel.GetRequest();
            try
            {
                //OnLog(requestModel.GetRequestString);
                Socket.Send(request, request.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                //OnLog(EcrRequestModel.ConvertByteToString(receave));
                var response = new EcrResponseModel(receave, PasswordKey);
                Response = new EcrResponse(response.ResponseCode);
                //OnLog(ResponseCode.ToString());

                if (!response.HasResponse)
                {
                    SendError(Response.Code, "", response.GetResponseString, requestModel.GetRequestByJson);
                    return null;
                }
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                var responce = response.GetResponse<UsersAndDepartments>();
                return responce;
            }
            catch (Exception ex)
            {
                SendException("GetOperatorsAndDeps: " + ex.Message);
                Response = new EcrResponse(EcrResponse.ResponseCodes.UnknownError);
                return null;
            }

        }

        /// <summary>
        /// Օպերատորի մուտքի ստուգում
        /// </summary>
        /// <returns></returns>
        public bool TryOperatorLogin()
        {
            if (OperatorLogin()) return false;
            OperatorLogout();
            return true;
        }

        /// <summary>
        /// 4.5.2 ՀԴՄ օպերատորորի մուտք
        /// Կոդավորումը՝ գաղտնաբառով
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public bool OperatorLogin()
        {
            if (!IsConnected)
            {
                Connect();
            }
            return TryGetOperatorLogin();
        }

        /// <summary>
        /// 4.5.3 Օպերատորի ելք (սեսիայի ընդհատում)
        /// Կոդավորումը՝ սեսիայի բանա լիով
        /// </summary>
        /// <param></param>
        /// <returns></returns>        
        public bool OperatorLogout()
        {
            if (!IsOperatorLogin) return true;
            var request = new EcrRequestModel(OP_OPS_LOGOUT, new RequestLogoff(++Seq), SessionKey).GetRequest();
            IsOperatorLogin = false;
            try
            {
                Socket.Send(request, request.Length, 0);
                var responce = new byte[11];
                Socket.Receive(responce);
                var response = new EcrResponseModel(responce, SessionKey);
                //Response = response.ResponseCode;

                return response.HasResponse;
            }
            catch (Exception ex)
            {
                SendError(EcrResponse.ResponseCodes.UnknownError, "OperatorLogout: " + ex.Message);
                //Response = EcrRecponce.ResponseCodes.UnknownError;
                return false;
            }
        }

        /// <summary>
        /// 4.5.4 Կտրոնի տպում (պահանջվում է ընթացիկ սեսիա)
        /// </summary>
        /// <param name="products"></param>
        /// <param name="paid"></param>
        /// <param name="ticketData"></param>
        /// <param name="getResponse"></param>
        /// <param name="partnerTin"></param>
        /// <returns></returns>
        public ResponseReceiptModel PrintReceiptTicket(IList<IEcrProduct> products, IEcrPaid paid, IEcrTickedAdditonalData ticketData)
        {
            if (products == null || paid == null)
            {
                return null;
            }
            var requestObject = new RequestPrintReceiptModel(++Seq)
            {
                paidAmount = paid.PaidAmount ?? 0,
                paidAmountCard = paid.PaidAmountCard ?? 0,
                partialAmount = paid.PartialAmount ?? 0,
                prePaymentAmount = paid.PrePaymentAmount ?? 0,
                mode = (int)InvoiceMode.ProductMode,
                useExtPOS = paid.UseExtPos,
                PaymentSystem = paid.PaymentSystem,
                partnerTin = ticketData.PartnerTin,
                eMarks = ticketData.EMarks,
            };
            foreach (var product in products)
            {
                //product.Dep = _cashierDepartment.Id;
                requestObject.Add(product);
            }
            int errorPos = 0;
            try
            {
                var request = new EcrRequestModel(OP_PRINT_RECEIPT, requestObject, SessionKey);
                var requestStream = request.GetRequest();
                errorPos++;
                Socket.Send(requestStream, requestStream.Length, 0);
                errorPos++;
                var receave = new byte[11];
                Socket.Receive(receave);
                errorPos++;
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                errorPos++;
                if (!response.HasResponse)
                {
                    errorPos++;
                    SendError(response.ResponseCode, "Hasn't response.", request.GetRequestByJson, string.Empty);
                    Response = new EcrResponse(EcrResponse.ResponseCodes.EcrResponceException);
                    return null;
                }
                errorPos += 2;
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                errorPos++;
                response.Response = receave;
                var value = response.GetResponse<ResponseReceiptModel>();
                return value;
            }
            catch (Exception ex)
            {
                SendError(Response.Code, string.Format("PrintReceipt:{2} Exception on printing ticket:{1}. \n error message:{0} \n", ex, Response.Description, errorPos));
                SendException(requestObject.GetRequest());
                Response = new EcrResponse(EcrResponse.ResponseCodes.EcrRequestException);
                return null;
            }
        }

        /// <summary>
        /// 4.5.5 ՀԴՄ վերջին կտրոնի կրկնօրինակի տպում
        /// Այս հրահանգը տպում է վերջին կտրոնի կրկնօրինակը ներառյալ էջագլուխն ու էջատակը:
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public bool PrintReceiptLatestCopy()
        {
            var requestObject = new RequestPrintReceiptLatestCopy(++Seq);
            var request = new EcrRequestModel(OP_PRINT_LAST_RECEIPT, requestObject, SessionKey);
            var requestStream = request.GetRequest();
            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                return Response.Code == EcrResponse.ResponseCodes.Ok;
            }
            catch (Exception ex)
            {
                SendException("PrintReceiptLatestCopy: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 4.5.6 ՀԴՄ վերադարձվող կտրոնի ստացում
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <param name="crn"></param>
        /// <returns></returns>
        public ReturnReceiptResponseModel GetReturnReceipt(string returnTicketValue, string crn)
        {
            if (SessionKey == null)
            {
                return null;
            }
            try
            {
                var requestObject = new ReturnReceiptRequestModel(++Seq, returnTicketValue, crn);
                var request = new EcrRequestModel(OP_GET_RECEIPT_DATA, requestObject, SessionKey);
                var requestStream = request.GetRequest();
                var receave = new byte[11];
                Socket.Send(requestStream, requestStream.Length, 0);
                Socket.Receive(receave);

                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                if (!response.HasResponse)
                {
                    return null;
                }
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;

                var value = response.GetResponse<ReturnReceiptResponseModel>();
                return value;
            }
            catch (TimeoutException ex)
            {
                Response = new EcrResponse(EcrResponse.ResponseCodes.NetworkConnectionError);
                SendError(Response.Code, ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Response = new EcrResponse(EcrResponse.ResponseCodes.EcrResponceException);
                SendException(string.Format("Receiving ticket {0}", ex.ToString()));

                return null;
            }
        }
        //public ReturnReceiptResponseModel GetReturnTicket(string returnTicketValue, string crn)
        //{
        //    if (SessionKey == null) { return null; }
        //    var requestObject = new GetReturnReceiptRequestModel(++Seq, returnTicketValue, crn);
        //    var request = new EcrRequestModel(OP_PRINT_RECEIPT, requestObject, SessionKey);
        //    MessageBox.Show(request.RequestToHex, "", MessageBoxButtons.YesNoCancel);
        //    var requestStream = request.GetRequest();
        //    try
        //    {
        //        Socket.Send(requestStream, requestStream.Length, 0);
        //        var receave = new byte[11];
        //        Socket.Receive(receave);
        //        var response = new EcrResponseModel(receave, SessionKey);
        //        Response = response.ResponseCode;

        //        if (!response.HasResponse)
        //        {
        //            SendError(response.ResponseCode, "", request.GetRequestByJson, response.GetResponseJson.ToString());
        //            return null;
        //        }

        //        if (!response.HasResponse) { return null; }
        //        receave = new byte[response.ResponseLength];
        //        Socket.Receive(receave);
        //        response.Response = receave;
        //        var value = response.GetResponse<ReturnReceiptResponseModel>();
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        SendException("PrintReturnTicket: " + ex.Message);
        //        return null;
        //    }
        //}

        /// <summary>
        /// 4.5.7 ՀԴՄ վերադարձի կտրոնի տպում
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <param name="crn"></param>
        /// <param name="paid"></param>
        /// <param name="ticketData"></param>
        /// <returns></returns>
        public ResponseReceiptModel PrintReturnReceipt(int returnTicketValue, IEcrReturnItem[] returnItemsList, EcrPaid paid)
        {
            if (!IsConnected)
            {
                return null;
            }

            double? cashAmountForReturn = null, cardAmountForReturn = null;
            if (returnItemsList != null)
            {
                cashAmountForReturn = paid != null ? paid.PaidAmount : 0;
                cardAmountForReturn = paid != null ? paid.PaidAmountCard : 0;
            }

            var requestObject = new PrintReturnReceiptRequestModel(++Seq, Crn, returnTicketValue, returnItemsList, cashAmountForReturn, cardAmountForReturn);
            var request = new EcrRequestModel(OP_PRINT_RETURN_RECEIPT, requestObject, SessionKey);
            var requestStream = request.GetRequest();

            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                if (!response.HasResponse) return null;
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                var value = response.GetResponse<ResponseReceiptModel>();
                return value;
            }
            catch (Exception ex)
            {
                SendError(Response.Code, ex.ToString());
                return null;
            }
        }

        //public ReturnReceiptResponseModel PrintReturnTicket(string returnTicketValue, string crn, IEcrPaid paid, IEcrTickedAdditonalData ticketData, IEcrReturnItem[] returnItemList)
        //{
        //    if (SessionKey == null) { return null; }
        //    var requestObject = new GetReturnReceiptRequestModel(++Seq, returnTicketValue, crn);
        //    var request = new EcrRequestModel(OP_PRINT_RECEIPT, requestObject, SessionKey);
        //    MessageBox.Show(request.RequestToHex, "", MessageBoxButtons.YesNoCancel);
        //    var requestStream = request.GetRequest();
        //    try
        //    {
        //        Socket.Send(requestStream, requestStream.Length, 0);
        //        var receave = new byte[11];
        //        Socket.Receive(receave);
        //        var response = new EcrResponseModel(receave, SessionKey);
        //        Response = response.ResponseCode;

        //        if (!response.HasResponse)
        //        {
        //            SendError(response.ResponseCode, "", request.GetRequestByJson, response.GetResponseJson.ToString());
        //            return null;
        //        }

        //        if (!response.HasResponse || !getResponse) { return null; }
        //        receave = new byte[response.ResponseLength];
        //        Socket.Receive(receave);
        //        response.Response = receave;
        //        var value = response.GetResponse<ReturnReceiptResponseModel>();
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        SendException("PrintReturnTicket: " + ex.Message);
        //        return null;
        //    }
        //}

        /// <summary>
        /// 4.6 Սարքի ամսաթվի և ժամի ստացում
        /// պահանջվում է ընթացիկ սեսիա։
        /// Կոդավորումը սեսիայի բանալիով
        /// Հարցում՝
        /// </summary>
        /// <returns></returns>
        public string GetEcrDateTime()
        {
            var requestObject = new RequestGetOperatorsAndDeps(Password);
            var requestModel = new EcrRequestModel(OP_GET_ECR_DATETIME, requestObject, PasswordKey);
            var request = requestModel.GetRequest();
            try
            {
                Socket.Send(request, request.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, PasswordKey);
                Response = new EcrResponse(response.ResponseCode);

                if (!response.HasResponse)
                {
                    SendError(Response.Code, "", response.GetResponseString, requestModel.GetRequestByJson);
                    return null;
                }
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                var responce = response.GetResponse<string>();
                return responce;
            }
            catch (Exception ex)
            {
                SendException("Get ECR date time: " + ex.Message);
                Response = new EcrResponse(EcrResponse.ResponseCodes.UnknownError);
                return null;
            }
        }

        /// <summary>
        /// 4.6.1 Կտրոնի ձևանմուշի տպում
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// Հարցում՝
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public bool PrintBlankTicket()
        {
            var requestObject = new RequestPrintReceiptLatestCopy(++Seq);
            var request = new EcrRequestModel(OP_PRINT_BLANK_TICKET, requestObject, SessionKey);
            var requestStream = request.GetRequest();
            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                return Response.Code == EcrResponse.ResponseCodes.Ok;
            }
            catch (Exception ex)
            {
                SendException("Print blank ticket: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 4.6.3 Կտրոնի էջագլխի եւ էջատակի կարգավորում (պահանջվում է ընթացիկ սեսիա)
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// </summary>
        /// <returns></returns>
        public bool ConfigureReceiptHeader()
        {
            var requestObject = new RequestReceiptHeader(++Seq);
            requestObject.AddHeader(new ReceiptHeader { text = "Company" });
            requestObject.AddFooter(new ReceiptHeader { text = "Շնորհակալություն", align = 2 });
            var request = new EcrRequestModel(OP_PRINT_RECEIPT, requestObject, SessionKey);
            var requestStream = request.GetRequest();
            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                if (!response.HasResponse) return false;
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                return true;
            }
            catch (Exception ex)
            {
                SendException("ConfigureReceiptHeader: " + ex.Message);
                return false;
            }
        }
        public void ConfigureReceiptFooter()
        { }

        /// <summary>
        /// 4.6.4 Կտրոնի էջագլխի Լոգո -ի կարգավորում (պահանջվում է ընթացիկ սեսիա)
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// </summary>
        public void ConfigureReceiptLogo()
        { }

        /// <summary>
        /// 4.7 ՀԴՄ համաժամանակեցում
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// Հարցում՝
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public bool SyncEcr()
        {
            var requestObject = new RequestPrintReceiptLatestCopy(++Seq);
            var request = new EcrRequestModel(OP_SYNC_ECR, requestObject, SessionKey);
            var requestStream = request.GetRequest();
            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                return Response.Code == EcrResponse.ResponseCodes.Ok;
            }
            catch (Exception ex)
            {
                SendException("Print blank ticket: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 4.8 ՀԴՄ սարքում առկա վճարային համակարգերի ցանկի ստացում
        /// պահանջվում է ընթացիկ սեսիա։
        /// Կոդավորումը սեսիայի բանալիով
        /// Հարցում՝
        /// </summary>
        /// <returns></returns>
        public PaymentSystemsModel GetPaymentSystemsList()
        {
            var requestObject = new RequestPrintReceiptLatestCopy(++Seq);
            var requestModel = new EcrRequestModel(OP_GET_PAYMENT_Systems_LIST, requestObject, SessionKey);
            var request = requestModel.GetRequest();
            try
            {
                Socket.Send(request, request.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);

                if (!response.HasResponse)
                {
                    SendError(Response.Code, "", response.GetResponseString, requestModel.GetRequestByJson);
                    return null;
                }
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                var responce = response.GetResponse<PaymentSystemsModel>();
                return responce;
            }
            catch (Exception ex)
            {
                SendException("GetPaymentSystemsList: " + ex.Message);
                Response = new EcrResponse(EcrResponse.ResponseCodes.UnknownError);
                return null;
            }
        }

        /// <summary>
        /// 4.9 Մեկ eMark կոդի ստուգում
        /// պահանջվում է ընթացիկ սեսիա։
        /// Կոդավորումը սեսիայի բանալիով
        /// Հարցում՝
        /// </summary>
        /// <returns></returns>
        public bool CheckEmark(string emark)
        {
            var requestObject = new RequestCheckEmark(++Seq, emark);
            var requestModel = new EcrRequestModel(OP_CHECK_EMARK, requestObject, PasswordKey);
            var request = requestModel.GetRequest();
            try
            {
                Socket.Send(request, request.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, PasswordKey);
                Response = new EcrResponse(response.ResponseCode);

                return response.HasResponse;
            }
            catch (Exception ex)
            {
                SendException("GetOperatorsAndDeps: " + ex.Message);
                Response = new EcrResponse(EcrResponse.ResponseCodes.UnknownError);
                return false;
            }
        }

        #endregion
    }
}

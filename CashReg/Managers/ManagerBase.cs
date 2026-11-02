using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using CashReg.Helper;
using CashReg.Interfaces;

namespace CashReg.Managers
{
    public class ManagerBase : IDisposable
    {
        public delegate void OnErrorMessage(string message);
        public event OnErrorMessage OnErrorReceived;

        public delegate void OnExceptionDelegate(string message);
        public event OnExceptionDelegate OnException;
        #region Internal properties
        private int _timeout = 60000;
        private int _bufferSize = 16384;
        private string _password;
        private byte[] _passwordKey = new byte[24];
        private int _seq = 1;
        private EcrResponse _response = new EcrResponse(EcrResponse.ResponseCodes.UNKNOWN_RESPONSE_CODE);
        #endregion

        #region Constants

        public const byte CheckEcrConnection = (byte)EcrExecuiteActions.CheckEcrConnection;
        public const byte ZERO = 0;

        public const byte OP_LIST_OPS_AND_DEPS = 1;
        public const byte OP_OPS_LOGIN = 2;
        public const byte OP_OPS_LOGOUT = 3;
        public const byte OP_PRINT_RECEIPT = 4;
        public const byte OP_PRINT_LAST_RECEIPT = 5;
        public const byte OP_PRINT_RETURN_RECEIPT = 6;
        public const byte OP_SETUP_HEADERFOOTER = 7;
        public const byte OP_SETUP_HEADERLOGO = 8;
        public const byte OP_PRINT_REPORT = 9;
        public const byte OP_GET_RECEIPT_DATA = 10;
        public const byte OP_GET_CASH_RECEIPT_WITHDRAWAL = 11;
        public const byte OP_GET_ECR_DATETIME = 12;
        public const byte OP_PRINT_BLANK_TICKET = 13;
        public const byte OP_SYNC_ECR = 14;
        public const byte OP_GET_PAYMENT_Systems_LIST = 15;
        public const byte OP_CHECK_EMARK = 16;

        #endregion //Constants

        #region Internal properties
        protected Action<string> ErrorCallBack;
        protected Action<string> ExceptionCallBack;

        public Socket Socket { get; private set; }
        protected string Ip { get; private set; }
        protected int Port { get; private set; }
        protected string Crn { get; private set; }
        protected int Timeout { get { return _timeout; } private set { _timeout = value; } }
        protected int BufferSize { get { return _bufferSize; } private set { _bufferSize = value; } }
        public byte[] PasswordKey { get { return _passwordKey; } private set { _passwordKey = value; } }
        public byte[] SessionKey { get; protected set; }
        public int Seq { get { return _seq; } set { _seq = value; } }
        protected string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                _passwordKey = Encryption.GetSHA256Hash(value);
            }
        }
        #endregion

        #region External properties
        public bool IsConnected { get; private set; }
        public EcrResponse Response { get { return _response; } set { _response = value; } }

        protected void SendError(EcrResponse.ResponseCodes errorCode, string errorMessage, string request = null, string responce = null)
        {
            var message = string.Format("ՀԴՄ սխալ\nԿոդ: {0}\nՍխալ: {1}\n{2}", (int)errorCode, EcrResponse.GetExceptionDescription(errorCode), errorMessage);
            if (errorCode == EcrResponse.ResponseCodes.UNKNOWN_RESPONSE_CODE || errorCode == EcrResponse.ResponseCodes.UnknownError)
                message = string.Format("{0}\nՍխալ: {1}\nՀարցում: {2}\nՊատասխան: {3}", message, errorMessage, request, responce);

            var handler = OnErrorReceived;
            if (handler != null) handler(message);
            if (ErrorCallBack != null) ErrorCallBack(message);
        }
        protected void SendException(string message, string title = null)
        {
            var handler = OnException;
            if (handler != null)
            {
                handler(message);
            }
            OnLog(message);
            if (ExceptionCallBack != null) ExceptionCallBack(message);
        }
        public static void OnLog(string message)
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EcrLog.txt");
            File.AppendAllLines(logPath, new[] { DateTime.Now + " " + message });
        }
        #endregion

        #region Constructors
        protected ManagerBase()
        {

        }
        protected ManagerBase(IEcrSettings ecrSettings, Action<string> errorCallBack, Action<string> exceptionCallBack)
        {
            if (ecrSettings == null) { return; }
            Ip = ecrSettings.Ip;
            Port = ecrSettings.Port;
            Password = ecrSettings.Password;
            Crn = ecrSettings.Crn;

            ErrorCallBack = errorCallBack;
            ExceptionCallBack = exceptionCallBack;
        }
        #endregion //Constructors

        #region Internal methods
        /// <summary>
        /// Socket connetion
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private bool SocketConnect(string server, int port)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(server);
                IPEndPoint ipe = new IPEndPoint(ipAddress, port);
                var socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.ReceiveTimeout = socket.SendTimeout = Timeout;
                socket.ReceiveBufferSize = socket.SendBufferSize = BufferSize;
                socket.Connect(ipe);
                Socket = socket.Connected ? socket : null;
                return socket.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected void SetSessionKey(byte[] sessionKey)
        {
            SessionKey = sessionKey;
        }
        public bool Connect()
        {
            try
            {
                if (IsConnected) return true;
                IsConnected = SocketConnect(Ip, Port);
                Response = new EcrResponse(IsConnected ? EcrResponse.ResponseCodes.Ok : EcrResponse.ResponseCodes.NetworkConnectionError);
                return IsConnected;
            }
            catch (Exception ex)
            {
                OnException(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Close connections and disconnect.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (IsConnected && Socket != null)
                {
                    if (Socket.Connected)
                        Socket.Disconnect(false);

                    Socket.Close();
                    IsConnected = false;
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                Socket = null;
                IsConnected = false;
            }

        }
        #endregion

        #region External methods

        /// <summary>
        /// Disconnect.
        /// </summary>
        public virtual void Dispose()
        {
            Disconnect();
        }
        #endregion //External methosd
    }
}


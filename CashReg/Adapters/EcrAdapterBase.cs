using System;
using System.Drawing;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Managers;

namespace CashReg.Adapters
{
    /// <summary>
    /// Ecr Server Base
    /// </summary>
    //[ComVisible(true)]
    public class EcrAdapterBase : IEcrPrintTicketAdapter, IDisposable
    {
        #region Evens      
        /// <summary>
        /// On Error Method
        /// </summary>
        /// <param name="ex"></param>
        public delegate void OnErrorDelegate(Exception ex);
        /// <summary>
        /// On Error event
        /// </summary>
        public virtual event OnErrorDelegate OnErrorEvent;
        #endregion Events

        #region Internal properties       
        /// <summary>
        /// Author detail
        /// </summary>
        protected const string _version = "FV25.1118.337.073";
        protected const string _aboutVersion = "This is free version of the Electric Cash Register. All rights reserved.";
        protected const string _author = "Hrayr Gishyan";
        protected const string _authorContacts = "e-mail:HrayrGishyan@gmail.com \n Moble:+374 94 88 19 83";

        /// <summary>
        /// Error code detail
        /// </summary>
        protected IEcrSettings EcrSettings;
        protected readonly string ServerLogPath;
        protected readonly string ErrorLogPath;
        protected Action<string> MessageCallback;
        protected Action<Exception> ErrorCallback;
        protected EcrResponse.ResponseCodes ErrorCode;
        protected RequestsManager Manager;

        #endregion

        #region External properties
        public string Version
        {
            get { return _version; }
        }
        public string About
        {
            get { return _aboutVersion; }
        }
        public string Author
        {
            get { return _author; }
        }
        public string Contacts
        {
            get { return _authorContacts; }
        }
        public EcrResponse Result { get; protected set; }
        #endregion

        #region Contructors
        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        protected EcrAdapterBase(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback)
        {
            EcrSettings = ecrSettings;
            //ServerLogPath = serverLogPath;
            ErrorLogPath = AppDomain.CurrentDomain.BaseDirectory + @"EcrErrorLog.txt";
            MessageCallback = messageCallback;
            ErrorCallback = exceptionCallback;
            Initialize();
        }
        #endregion Constructors

        #region Internal methods
        protected void Initialize()
        {
            CreateConnection(EcrSettings);
            if (Manager == null) return;
            //Manager.OnErrorReceived += OnErrorReceived;
            //Manager.OnException += OnException;
        }

        protected RequestsManager GetRequestsManager(IEcrSettings ecrSettings)
        {
            return new RequestsManager(ecrSettings, MessageCallback, OnException);
        }

        protected virtual void OnMessage(string message)
        {
            if (MessageCallback != null) MessageCallback(message);
        }

        protected virtual void OnErrorReceived(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = string.Format("{0} ({1}) ", Result.Description, Result.Code);
            }

            Exception ex = new TaxServiceException(message);
            try
            {
                var handler = OnErrorEvent;
                if (handler != null) handler(ex);

                //if (Manager != null) Manager.Dispose();
            }
            catch (Exception e)
            {
                var handler = OnErrorEvent;
                if (handler != null) handler(ex);
                message = ex.Message;
            }
            finally
            {
                if (ErrorCallback != null) ErrorCallback(ex);
            }
        }

        protected virtual void OnException(string message)
        {
            EcrResponse.OnException(message);
            Manager.Dispose();
        }

        private Bitmap GetLogo()
        {
            try
            {
                string path = "../ecr-integration/src/main/resources/logo.bmp";
                return new Bitmap(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private bool SetLogo()
        {
            Bitmap image = GetLogo();
            byte[] bitmapBytes = image != null ? EcrConvert.ImageToByteArray(image) : null;
            return false;
        }
        #endregion Internal methods        

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
        /// ՀԴՄ օպերատորորի մուտք
        /// </summary>
        /// <returns></returns>
        public bool TryOperatorLogin()
        {
            var isOperatorLogin = Manager.TryOperatorLogin();
            Result = Manager.Response;
            Manager.Dispose();
            return isOperatorLogin;
        }

        /// <summary>
        /// ՀԴՄ օպերատորորի ելք
        /// </summary>
        /// <returns></returns>
        public bool OperatorLogout()
        {
            return Manager.IsOperatorLogin && Manager.OperatorLogout();
        }
        public void Dispose()
        {
            if (Manager != null)
                Manager.Dispose();
        }
        #endregion Connections
    }
}

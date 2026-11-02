using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CashReg.Helper;
using CashReg.Managers;
using CashReg.Models;
using ECR.Services.Helper;

namespace ECR.Services.Server
{
    public class SocketServer
    {
        #region Internal properties
        private string _logPath;
        private Socket _clientSocket = null;
        private bool _stopClient;
        private Thread _clientListenerThread = null;
        private TcpListener _tcpListener;
        private Thread _listenThread;
        private string _ip;
        private static int _port;
        private IPAddress _address;
        private bool _isStarted;
        private string _password;
        private byte[] _passwordKey = new byte[24];
        private EcrConfig _config;
        private string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                _passwordKey = Encryption.GetSHA256Hash(value);
            }
        }
        #endregion //Internal properties

        #region External properties
        public byte[] PasswordKey { get { return _passwordKey; } private set { _passwordKey = value; } }

        public bool IsStarted
        {
            get { return _isStarted; }
            set { _isStarted = value; }
        }

        #endregion //External properies

        #region Constructors
        public SocketServer(EcrConfig config)
        {
            InitializeComponent(config);
        }
        #endregion //Constructors

        #region Internal methods
        private void InitializeComponent(EcrConfig config)
        {
            _logPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\EcrServerLog.txt";
            _config = config;
            if (_config == null || !_config.EcrServiceSettings.IsActive)
            {
                OnLog("Config" + (config == null ? "is Null" : (config.EcrServiceSettings.IsActive ? "true" : "false")));
                return;
            }
            _config = config;
            _ip = _config.EcrServiceSettings.Ip;
            _port = _config.EcrServiceSettings.Port;
            _address = IPAddress.Parse(_ip);
            Password = _config.Password;
            IPEndPoint ipe = new IPEndPoint(_address, _port);
            _clientSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            OnLog(string.Format("ServerIP:{0} \n Server port:{1}", _ip, _port));
        }
        private void StartSocketListener()
        {
            if (_clientSocket == null) return;
            _isStarted = true;
            _tcpListener = new TcpListener(_address, _port);
            _clientListenerThread = new Thread(new ThreadStart(SocketListenerThreadStart));
            _clientListenerThread.Start();
        }
        private void SocketListenerThreadStart()
        {
            OnLog(string.Format("{0} ECR service started.", DateTime.Now));
            _tcpListener.Start();

            while (!_stopClient)
            {
                try
                {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
                catch (SocketException se)
                {
                    OnLog("Listening " + DateTime.Now);
                    OnLog(se.Message);
                    _stopClient = true;
                }
            }
            if (IsStarted) Stop();
        }
        private void HandleClientComm(object o)
        {
            var client = o as TcpClient;
            if (client == null) return;

            NetworkStream clientStream = client.GetStream();
            byte[] request = new byte[client.ReceiveBufferSize];
            byte[] response;
            var ecrRequest = new EcrRequestModel(request, PasswordKey);
            RequestsManager ecrRequestManager = null;

            bool isListening = true;
            while (isListening)
            {
                try
                {
                    //blocks until a client sends a message
                    request = new byte[client.ReceiveBufferSize];
                    clientStream.Read(request, 0, client.ReceiveBufferSize);
                    request = ecrRequest.ParseRequest(request);
                    switch (ecrRequest.Code)
                    {
                        case EcrExecuiteActions.GetOperatorsAndDepList:
                        case EcrExecuiteActions.OperatorLogin:
                            ecrRequestManager = ecrRequestManager ?? new RequestsManager(_config.Convert(), OnError, OnError);
                            if (!ecrRequestManager.IsConnected)
                            {
                                response = new RequestBase(_passwordKey).GetResponse(EcrResponse.ResponseCodes.NetworkConnectionError);
                                Send(client, response);
                                isListening = false;
                                break;
                            }
                            ecrRequestManager.TrySendRequest(request);
                            response = ecrRequestManager.TryReceiveResponse();
                            Send(client, response);
                            var responseModel = new EcrResponseModel(response, PasswordKey);
                            if (responseModel.HasResponse)
                            {
                                response = ecrRequestManager.TryReceiveResponse(responseModel.ResponseLength);
                                Send(client, response);
                            }
                            isListening = ecrRequest.Code == EcrExecuiteActions.OperatorLogin;
                            break;

                        case EcrExecuiteActions.LogoutOperator:
                        case EcrExecuiteActions.PrintLatestTicket:
                        case EcrExecuiteActions.PrintEcrReport:
                        case EcrExecuiteActions.PrintReportX:
                        case EcrExecuiteActions.PrintReportZ:
                            if (ecrRequestManager == null || !ecrRequestManager.IsConnected)
                            {
                                response = new RequestBase(_passwordKey).GetResponse(EcrResponse.ResponseCodes.NetworkConnectionError);
                                Send(client, response);
                                isListening = false;
                                break;
                            }
                            ecrRequestManager.TrySendRequest(request);
                            response = ecrRequestManager.TryReceiveResponse();
                            Send(client, response);
                            isListening = false;
                            break;

                        case EcrExecuiteActions.CheckEcrConnection:
                            ecrRequestManager = new RequestsManager(_config.Convert(), OnError, OnError);
                            response = new RequestBase(_passwordKey).GetResponse(ecrRequestManager.IsConnected ? EcrResponse.ResponseCodes.Ok : EcrResponse.ResponseCodes.NetworkConnectionError);
                            Send(client, response);
                            isListening = false;
                            break;

                        case EcrExecuiteActions.CheckConnection:
                            response = new RequestBase(_passwordKey).GetResponse(EcrResponse.ResponseCodes.Ok);
                            Send(client, response);
                            isListening = false;
                            break;

                        case EcrExecuiteActions.Zero:
                            isListening = false;
                            break;

                        case EcrExecuiteActions.PrintReceiptTicket:

                        case EcrExecuiteActions.PrintReturnTicket:

                        case EcrExecuiteActions.GetReceiptData:
                        case EcrExecuiteActions.PrintCash:

                            if (ecrRequestManager == null || !ecrRequestManager.IsConnected)
                            {
                                response = new RequestBase(_passwordKey).GetResponse(EcrResponse.ResponseCodes.EcrNetworkConnectionError);
                                Send(client, response);
                                isListening = false;
                                break;
                            }
                            ecrRequestManager.TrySendRequest(request);
                            response = ecrRequestManager.TryReceiveResponse();
                            Send(client, response);
                            responseModel = new EcrResponseModel(response, PasswordKey);
                            if (responseModel.HasResponse)
                            {
                                response = ecrRequestManager.TryReceiveResponse(responseModel.ResponseLength);
                                Send(client, response);
                            }
                            isListening = false;
                            break;

                        case EcrExecuiteActions.ManageHeaderAndFooter:
                            break;
                        case EcrExecuiteActions.ManageLogo:
                            break;


                            //T = typeof(RequestPrintReceiptLatestCopy);
                            break;

                            //T = typeof(ReturnReceiptResponseModel);
                            break;
                        case EcrExecuiteActions.GetOperatorsList:
                            break;
                        case EcrExecuiteActions.GetDepsList:
                            break;
                        case EcrExecuiteActions.GetEcrDateTime:
                            break;
                        case EcrExecuiteActions.PrintBlankTicket:
                            break;
                        case EcrExecuiteActions.SyncEcr:
                            break;
                        case EcrExecuiteActions.GetPaymentSystems:
                            break;
                        case EcrExecuiteActions.CashIn:
                            break;
                        case EcrExecuiteActions.CashWithdrawal:
                            break;
                        default:
                            isListening = false;
                            break;
                        case EcrExecuiteActions.CheckEmark:

                            break;
                    }
                }
                catch
                {
                    break;
                    //a socket error has occured
                }
            }
            if (ecrRequestManager != null)
                ecrRequestManager.Dispose();
            client.Close();
        }

        private void OnError(string message)
        {
            MessageManager.ShowMessage(message, "Ecr error");
        }

        private void Send(TcpClient client, byte[] response)
        {
            File.AppendAllLines(_logPath, new[] { response + " " + DateTime.Now });
            if (!client.Connected) return;
            client.Client.SendBufferSize = response.Length;
            client.Client.BeginSend(response, 0, response.Length, SocketFlags.None, null, null);
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                //sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void StopSocketListener()
        {
            if (_clientSocket != null)
            {
                _stopClient = true;
                _clientSocket.Disconnect(true);
                _clientSocket.Close();

                // Wait for one second for the the thread to stop.
                _clientListenerThread.Join(1000);

                // If still alive; Get rid of the thread.
                if (_clientListenerThread.IsAlive)
                {
                    _clientListenerThread.Abort();
                }
                _clientListenerThread = null;
                _clientSocket = null;
            }
        }

        private void OnLog(string message)
        {
            File.AppendAllLines(_logPath, new[] { message });
        }
        #endregion //Internal methods

        #region External methods

        public void Start()
        {
            if (_clientSocket == null)
            {
                OnLog(string.Format("{0} ECR service client socket was null.", DateTime.Now));
                Stop();
                return;
            }
            StartSocketListener();
        }

        public void Stop()
        {
            StopSocketListener();
            IsStarted = false;
            OnLog(string.Format("{0} ECR service stopped.", DateTime.Now));
        }

        #endregion //External methods















        //public void StartServer()
        //{
        //    File.Create(@"D:\MyProjects\ES\ES-TaxService\ECR.Services\bin\Debug\text.txt");
        //    if (m_server != null)
        //    {
        //        // Create a ArrayList for storing SocketListeners before
        //        // starting the server.
        //        m_socketListenersList = new ArrayList();

        //        // Start the Server and start the thread to listen client 
        //        // requests.
        //        m_server.Start();
        //        m_serverThread = new Thread(new ThreadStart(ServerThreadStart));
        //        m_serverThread.Start();

        //        // Create a low priority thread that checks and deletes client
        //        // SocktConnection objcts that are marked for deletion.
        //        m_purgingThread = new Thread(new ThreadStart(PurgingThreadStart));
        //        m_purgingThread.Priority = ThreadPriority.Lowest;
        //        m_purgingThread.Start();
        //    }
        //}

        /// <summary>
        /// Method that stops the TCP/IP Server.
        /// </summary>
        //public void StopServer()
        //{
        //    if (m_server != null)
        //    {
        //        // It is important to Stop the server first before doing
        //        // any cleanup. If not so, clients might being added as
        //        // server is running, but supporting data structures
        //        // (such as m_socketListenersList) are cleared. This might
        //        // cause exceptions.

        //        // Stop the TCP/IP Server.
        //        m_stopServer = true;
        //        m_server.Stop();

        //        // Wait for one second for the the thread to stop.
        //        m_serverThread.Join(1000);

        //        // If still alive; Get rid of the thread.
        //        if (m_serverThread.IsAlive)
        //        {
        //            m_serverThread.Abort();
        //        }
        //        m_serverThread = null;

        //        m_stopPurging = true;
        //        m_purgingThread.Join(1000);
        //        if (m_purgingThread.IsAlive)
        //        {
        //            m_purgingThread.Abort();
        //        }
        //        m_purgingThread = null;

        //        // Free Server Object.
        //        m_server = null;

        //        // Stop All clients.
        //        StopAllSocketListers();
        //    }
        //}


        /// <summary>
        /// Method that stops all clients and clears the list.
        /// </summary>
        //private void StopAllSocketListers()
        //{
        //    foreach (ECRTCPSocketListener socketListener
        //                 in m_socketListenersList)
        //    {
        //        socketListener.StopSocketListener();
        //    }
        //    // Remove all elements from the list.
        //    m_socketListenersList.Clear();
        //    m_socketListenersList = null;
        //}

        /// <summary>
        /// TCP/IP Server Thread that is listening for clients.
        /// </summary>
        //private void ServerThreadStart()
        //{
        //    // Client Socket variable;
        //    Socket clientSocket = null;
        //    ECRTCPSocketListener socketListener = null;
        //    while (!m_stopServer)
        //    {
        //        try
        //        {
        //            // Wait for any client requests and if there is any 
        //            // request from any client accept it (Wait indefinitely).
        //            clientSocket = m_server.AcceptSocket();

        //            // Create a SocketListener object for the client.
        //            socketListener = new ECRTCPSocketListener(clientSocket);

        //            // Add the socket listener to an array list in a thread 
        //            // safe fashon.
        //            //Monitor.Enter(m_socketListenersList);
        //            lock (m_socketListenersList)
        //            {
        //                m_socketListenersList.Add(socketListener);
        //            }
        //            //Monitor.Exit(m_socketListenersList);

        //            // Start a communicating with the client in a different
        //            // thread.
        //            socketListener.StartSocketListener();
        //        }
        //        catch (SocketException se)
        //        {
        //            m_stopServer = true;
        //        }
        //    }
        //}

        /// <summary>
        /// Thread method for purging Client Listeneres that are marked for
        /// deletion (i.e. clients with socket connection closed). This thead
        /// is a low priority thread and sleeps for 10 seconds and then check
        /// for any client SocketConnection obects which are obselete and 
        /// marked for deletion.
        /// </summary>
        //private void PurgingThreadStart()
        //{
        //    while (!m_stopPurging)
        //    {
        //        ArrayList deleteList = new ArrayList();

        //        // Check for any clients SocketListeners that are to be
        //        // deleted and put them in a separate list in a thread sage
        //        // fashon.
        //        //Monitor.Enter(m_socketListenersList);
        //        lock (m_socketListenersList)
        //        {
        //            foreach (ECRTCPSocketListener socketListener
        //                         in m_socketListenersList)
        //            {
        //                if (socketListener.IsMarkedForDeletion())
        //                {
        //                    deleteList.Add(socketListener);
        //                    socketListener.StopSocketListener();
        //                }
        //            }

        //            // Delete all the client SocketConnection ojects which are
        //            // in marked for deletion and are in the delete list.
        //            for (int i = 0; i < deleteList.Count; ++i)
        //            {
        //                m_socketListenersList.Remove(deleteList[i]);
        //            }
        //        }
        //        //Monitor.Exit(m_socketListenersList);

        //        deleteList = null;
        //        Thread.Sleep(10000);
        //    }
        //}
    }
}

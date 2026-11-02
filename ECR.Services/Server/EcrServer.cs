using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using ECR.Services.Helper;

namespace ECR.Services.Server
{
    partial class EcrServer : ServiceBase
    {
        #region Internal properties
        private readonly string _logPath;
        private const string EsMarketAppName = "EsMarket";
        private Socket _clientSocket = null;
        private bool _stopClient;
        private Thread _clientListenerThread = null;
        private TcpListener _tcpListener;
        private Thread _listenThread;
        private EcrConfig _config;
        private SocketServer _server;
        #endregion //Internal properties

        #region External properties

        #endregion //External properies

        #region Constructors
        public EcrServer()
        {
            _logPath = GetLocalAppDataPath() + @"\EcrServerLog.txt";
            File.AppendAllLines(_logPath, new[] { "Starting ECR service " + DateTime.Now });
            var config = ConfigSettings.GetEcrConfig();
            InitializeComponent(config);
        }
        #endregion //Constructors

        #region Internal methods

        protected override void OnStart(string[] args)
        {
            _server = new SocketServer(_config);
            _server.Start();
            
        }

        protected override void OnStop()
        {
            if (_server != null) _server.Stop();
            File.AppendAllLines(_logPath, new[] { "Stopped " + DateTime.Now });
            //StopSocketListener();
        }
        private void StartSocketListener()
        {
            if (_clientSocket != null)
            {
                _tcpListener = new TcpListener(_address, _port);
                _clientListenerThread = new Thread(new ThreadStart(SocketListenerThreadStart));
                _clientListenerThread.Start();
            }
        }
        private void SocketListenerThreadStart()
        {
            int size = 0;
            Byte[] byteBuffer = new Byte[1024];

            //m_lastReceiveDateTime = DateTime.Now;
            //m_currentReceiveDateTime = DateTime.Now;

            //Timer t = new Timer(new TimerCallback(CheckClientCommInterval), null, 15000, 15000);
            _tcpListener.Start();
            while (!_stopClient)
            {
                try
                {
                    //blocks until a client has connected to the server
                    TcpClient client = this._tcpListener.AcceptTcpClient();

                    //create a thread to handle communication 
                    //with connected client
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                    size = _clientSocket.Receive(byteBuffer);
                    //m_currentReceiveDateTime = DateTime.Now;
                    //ParseReceiveBuffer(byteBuffer, size);
                }
                catch (SocketException se)
                {
                    File.AppendAllLines(_logPath, new[] { "Listening " + DateTime.Now });
                    File.AppendAllLines(_logPath, new[] { se.Message });
                    _stopClient = true;

                }
            }
            //t.Change(Timeout.Infinite, Timeout.Infinite);
            //t = null;
        }
        private void HandleClientComm(object o)
        {
            var client = o as TcpClient;
            if (client == null) return;
            //Socket.Send(request, request.Length, 0);
            //var receave = new byte[11];
            //Socket.Receive(receave);
            //var response = new EcrResponseModel(receave, PasswordKey);
            //ResponseCode = response.ResponseCode;
            //if (!response.IsResponse) return null;
            //receave = new byte[response.ResponseLength];
            //Socket.Receive(receave);
            //response.Response = receave;

            NetworkStream clientStream = client.GetStream();
            File.AppendAllLines(_logPath, new[] { client.ReceiveBufferSize.ToString() });
            byte[] message = new byte[client.ReceiveBufferSize];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, client.ReceiveBufferSize);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
            }
            client.Close();
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

        private string GetLocalAppDataPath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), EsMarketAppName);
            path = Path.Combine(path, "EcrServices");
            if (Directory.Exists(path)) return path;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return path;
        }
        #endregion //Internal methods

        #region External methods
        #endregion //External methods
    }
}

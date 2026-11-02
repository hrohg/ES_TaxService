using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ECR.Services.Helper;

namespace ECR.Services.Server
{
    partial class EcrServer
    {
        #region Internal fields
        private string _ip;
        private static int _port;
        private IPAddress _address;
        #endregion //Internal fields
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(EcrConfig config)
        {
            _config = config;
            if (_config == null || _config.EcrServiceSettings == null || !config.EcrServiceSettings.IsActive)
            {
                return;
            }
            _ip = config.EcrServiceSettings.Ip;
            _address = IPAddress.Parse(_ip);
            _port = _config.EcrServiceSettings.Port;
            IPEndPoint ipe = new IPEndPoint(_address, _port);
            _clientSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.ServiceName = "EsEcrServer";
        }

        #endregion
    }
}

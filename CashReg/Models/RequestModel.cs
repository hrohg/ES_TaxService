using System;
using System.Linq;
using System.Web.Script.Serialization;
using CashReg.Helper;

namespace CashReg.Models
{
    public class EcrRequestModel : RequestBase
    {
        #region Constants
        private readonly byte[] _requestHeader = new byte[6] { (byte)0xD5, (byte)0x80, (byte)0xD4, (byte)0xB4, (byte)0xD5, (byte)0x84 };
        private readonly byte[] _protocolType = new byte[2] { (byte)0x00, (byte)0x05 };
        #endregion

        #region private properties
        private byte[] _key;
        private byte[] _requestCode = new byte[1];
        private byte[] _requestLength = new byte[2];
        private EcrExecuiteActions _code;
        private byte[] _req;
        private string _request;
        private byte[] Request { get { return _request != null && _key != null ? Encryption.EncryptString3Des(_request, _key) : null; } }
        #endregion

        #region External properties
        public EcrExecuiteActions Code { get { return _code; } }

        public string GetRequestByJson
        {
            get
            {
                return _req != null && _key != null ? Encryption.DecryptString3Des(_req, _key) : _request;
            }
        }
        
        #endregion

        #region Constructors

        public EcrRequestModel(byte requestCode, object request, byte[] key)
            : base(key)
        {
            _key = key;
            _requestCode = EcrConvert.Int16ToByte(requestCode);
            _request = EcrJsonConverters.SerializeObect(request);
            _requestLength = EcrConvert.Int16ToBigEndianByte((short)(Request != null ? Request.Length : 0));
        }

        public EcrRequestModel(byte[] request, byte[] key)
            : base(key)
        {
            _key = key;
            if (request == null) return;
            var code = request[8];
            _code = (EcrExecuiteActions)code;
            var len = EcrConvert.BeByteToInt16(request.Skip(10).Take(2).ToArray());
            if (len > 12)
            {
                _req = new byte[len];
                _req = request.Skip(12).Take(len).ToArray();
            }
        }

        public EcrRequestModel(byte requestCode, object request)
            : base(null)
        {
            _requestCode = EcrConvert.Int16ToByte(requestCode);
            _request = EcrJsonConverters.SerializeObect(request);
            _requestLength = EcrConvert.Int16ToBigEndianByte((short)(Request != null ? Request.Length : 0));
        }

        #endregion //Constructors

        #region External methods

        public byte[] GetRequest()
        {
            var request = new byte[12 + (Request != null ? Request.Length : 0)];
            Buffer.BlockCopy(_requestHeader, 0, request, 0, 6);
            Buffer.BlockCopy(_protocolType, 0, request, 6, 2);
            Buffer.BlockCopy(_requestCode, 0, request, 8, 1);
            //Buffer.BlockCopy(_requestReserve, 0, request, 9, 1);
            Buffer.BlockCopy(_requestLength, 0, request, 10, 2);
            if (Request != null)
                Buffer.BlockCopy(Request, 0, request, 12, Request.Length);
            return request;
        }

        public string GetRequestString()
        {   
                var request = GetRequest();
                string requestString = request.Aggregate<byte, string>(null, (current, t) => current + (t + "-"));
                return requestString.Substring(0, requestString.Length - 1);         
        }

        public static string ConvertByteToString(byte[] value)
        {
            string requestString = value.Aggregate<byte, string>(null, (current, t) => current + (t + "-"));
            return requestString.Substring(0, requestString.Length - 1);
        }
        public string RequestToHex
        {
            get
            {
                //return ECRConvert.ByteArrayToString(GetRequest());
                return string.Concat(GetRequest().Select(b => b.ToString("X2")).ToArray());
            }
        }

        public byte[] ParseRequest(byte[] request)
        {
            if (request == null) return null;
            var code = request[8];
            _code = (EcrExecuiteActions)code;
            var len = EcrConvert.BeByteToInt16(request.Skip(10).Take(2).ToArray());
            if (len > 12)
            {
                _req = new byte[len];
                _req = request.Skip(12).Take(len).ToArray();
            }
            return request.Take(12+len).ToArray();
        }


        #endregion
    }
}

using System;
using System.Linq;
using System.Text;
using CashReg.Helper;

namespace CashReg.Models
{
    public class EcrResponseModel : RequestBase
    {
        #region private properties

        private byte[] _key;
        private byte[] _protocolVersion = new byte[2];
        private byte[] _softwareVersion = new byte[3];
        private byte[] _responseCode = new byte[2];
        private byte[] _responseLength = new byte[2];
        private byte[] _responseReserve = new byte[2];
        private byte[] _responseBegin;
        private byte[] _response;
        #endregion

        #region public properties
        public string ProtocolVersion { get { return _protocolVersion[0] + "." + _protocolVersion[1]; } }
        public string SoftwareVersion { get { return _softwareVersion[0] + "." + _softwareVersion[1] + "." + _softwareVersion[2]; } }
        public EcrResponse.ResponseCodes ResponseCode { get { return _responseCode != null ? (EcrResponse.ResponseCodes)EcrConvert.BeByteToInt16(_responseCode) : EcrResponse.ResponseCodes.ResponseIsNull; } }
        public long ResponseLength { get { return EcrConvert.BeByteToInt16(_responseLength); } }
        #endregion
        public EcrResponseModel(byte[] key): base(key)
        {
            _key = key;
        }
        public EcrResponseModel(byte[] response, byte[] key): this(key)
        {
            if(response==null) return;
            _responseBegin = response;
            _key = key;
            _protocolVersion = response.Take(2).ToArray();
            _softwareVersion = response.Skip(2).Take(3).ToArray();
            _responseCode = response.Skip(5).Take(2).ToArray();
            _responseLength = response.Skip(7).Take(2).ToArray();
            _responseReserve = response.Skip(9).Take(2).ToArray();
        }
        public byte[] GenerateResponse(short responseCode)
        {
            var response = new byte[11];

            return response;
        }
        public string GetResponseString
        {
            get
            {
                string responseString = _responseBegin.Aggregate<byte, string>(null, (current, t) => current + (t + "-"));
                return responseString.Substring(0, responseString.Length - 1);
            }
        }
        public string GetResponseHexString
        {
            get
            {
                return string.Format("{0:##-##-##}", EcrConvert.ByteArrayToString(_responseBegin));
            }
        }
        public string ResponseBeginToHex { get { return string.Concat(_responseBegin.Select(b => b.ToString("X2")).ToArray()); } }
        public string ResponseToHex
        {
            get
            {
                string hex = string.Empty;
                if (_response == null) return string.Empty;
                foreach (var responce in _response)
                {
                    hex = string.Format("{0}{1}{2}", hex, !string.IsNullOrEmpty(hex) ? "-" : "", responce);
                }
                return hex;
            }
        }

        #region methods

        public byte[] Response
        {
            get { return _response; }
            set
            {
                _response = Encryption.DecryptBytes3Des(value, _key);
            }
        }
        public T GetResponse<T>()
        {
            try
            {
                return EcrJsonConverters.DeserializeObect<T>(Response);
            }
            catch (Exception ex)
            {
                var message = string.Format("JavaScriptSerializer deserialize exception:{0} responce:{1}", ex.Message, EcrConverter.ConvertByteArrayToString(Response));
                EcrResponse.OnException(message);
                throw new Exception(message);
            }
        }

        
        public Object GetResponseJson
        {
            get
            {
                return _response == null ? null : Encoding.UTF8.GetString(_response);
            }

        }
        public string GetResponseDescription { get { return EcrResponse.GetExceptionDescription(ResponseCode); } }
        public bool HasResponse { get { return ResponseCode == EcrResponse.ResponseCodes.Ok && ResponseLength > 0; } }
        public byte[] ResponseBegin
        {
            get
            {
                return _responseBegin;
            }
            set
            {
                _responseBegin = Encryption.DecryptBytes3Des(value, _key);
                _protocolVersion = _responseBegin.Take(2).ToArray();
                _softwareVersion = _responseBegin.Skip(2).Take(3).ToArray();
                _responseCode = _responseBegin.Skip(5).Take(2).ToArray();
                _responseLength = _responseBegin.Skip(7).Take(2).ToArray();
                _responseReserve = _responseBegin.Skip(9).Take(2).ToArray();
            }
        }

        #endregion
    }
}

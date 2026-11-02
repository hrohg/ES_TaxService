using System;
using System.Linq;
using CashReg.Helper;

namespace CashReg.Models
{
    public class RequestBase
    {
        #region Internal properties
        private readonly byte[] _requestHeader = new byte[6] { (byte)0xD5, (byte)0x80, (byte)0xD4, (byte)0xB4, (byte)0xD5, (byte)0x84 };
        private readonly byte[] _protocolType = new byte[2] { (byte)0x00, (byte)0x05 };
        private readonly byte[] _key;
        private readonly byte[] _requestCode = new byte[1];
        private readonly byte[] _requestLength = new byte[2];

        private byte[] _responseBegin;
        private byte[] _protocolVersion = new byte[2];
        private byte[] _softwareVersion = new byte[3];
        private byte[] _responseCode = new byte[2];
        private byte[] _responseLength = new byte[2];
        private byte[] _responseReserve = new byte[2];

        private Int16 _code;
        private byte[] _req;
        #endregion

        #region External properties

        #endregion

        #region Constructors

        public RequestBase(byte[] key)
        {
            _key = key;
        }
        #endregion

        #region Interanla methods
        private void TryParseResponse(byte[] response)
        {
            _responseBegin = response;
            _protocolVersion = response.Take(2).ToArray();
            _softwareVersion = response.Skip(2).Take(3).ToArray();
            _responseCode = response.Skip(5).Take(2).ToArray();
            _responseLength = response.Skip(7).Take(2).ToArray();
            _responseReserve = response.Skip(9).Take(2).ToArray();
        }
        private byte[] TryGetResponse(byte[] response = null)
        {
            var req = new byte[response!=null?response.Length:0 + 12];
            var responseLength = (short) (response != null ? response.Length : 0);
            Buffer.BlockCopy(_protocolType, 0, req, 0, 2);
            Buffer.BlockCopy(_softwareVersion, 0, req, 2, 3);
            Buffer.BlockCopy(_responseCode, 0, req, 5, 2);
            Buffer.BlockCopy(EcrConvert.Int16ToLittleEndianByte(responseLength), 0, req, 7, 2);
            Buffer.BlockCopy(_responseReserve, 0, req, 9, 2);
            if (response!=null)
            Buffer.BlockCopy(response, 0, req, 11, responseLength);
            return req;
        }
        private byte[] TryGetRequest(byte[] request=null)
        {
            var req = new byte[12 + (request != null ? request.Length : 0)];
            Buffer.BlockCopy(_requestHeader, 0, req, 0, 6);
            Buffer.BlockCopy(_protocolType, 0, req, 6, 2);
            Buffer.BlockCopy(_requestCode, 0, req, 8, 1);
            //Buffer.BlockCopy(_requestReserve, 0, request, 9, 1);
            Buffer.BlockCopy(_requestLength, 0, req, 10, 2);
            if (request != null)
                Buffer.BlockCopy(request, 0, req, 12, request.Length);
            return req;
        }

        private byte[] TryGetRequest(string request)
        {
            return TryGetRequest(request != null && _key != null ? Encryption.EncryptString3Des(request, _key) : null);
        }
        #endregion

        #region External methods

        public byte[] GetResponse(EcrResponse.ResponseCodes responseCode)
        {
            _responseCode = EcrConvert.Int16ToBigEndianByte((short)responseCode);
            return TryGetResponse();
        }
        #endregion
    }
}

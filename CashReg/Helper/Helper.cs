using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CashReg.Helper
{
    public static class Encryption
    {
        #region for Win XP SP3
        public static byte[] GetSHA256Hash(string value)
        {
            byte[] result = new byte[24];
            if (string.IsNullOrEmpty(value))
            {
                return result;
            }
            SHA256 shaM = new SHA256Managed();
            byte[] pasHash = shaM.ComputeHash(Encoding.UTF8.GetBytes(value));
            Buffer.BlockCopy(pasHash, 0, result, 0, 24);
            return result;
        }
        public static byte[] EncryptString3Des(string data, byte[] key)
        {
            try
            {
                byte[] toEncrypt = UTF8Encoding.UTF8.GetBytes(data);
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();
                tDESalg.Key = key;
                tDESalg.Mode = CipherMode.ECB;
                tDESalg.Padding = PaddingMode.PKCS7;
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream, tDESalg.CreateEncryptor(key, tDESalg.IV), CryptoStreamMode.Write);
                // Write the byte array to the crypto stream and flush it.
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (Exception ex)
            {
                EcrResponse.OnException(string.Format("3Des encriptor exception:{0} data:{1}", ex.Message, data));
                return null;
            }
        }

        public static byte[] DecryptBytes3Des(byte[] data, byte[] key)
        {
            var sData = EcrConverter.ConvertByteArrayToString(data);
            var sKey = EcrConverter.ConvertByteArrayToString(key);
            try
            {
                TripleDESCryptoServiceProvider tDesAlg = new TripleDESCryptoServiceProvider();
                tDesAlg.Key = key;
                tDesAlg.Mode = CipherMode.ECB;
                tDesAlg.Padding = PaddingMode.PKCS7;
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(data);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tDesAlg.CreateDecryptor(key, tDesAlg.IV),
                    CryptoStreamMode.Read);
                string val;
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    val = srDecrypt.ReadToEnd();
                }
                //Convert the buffer into a string and return it.
                byte[] fromEncrypt = Encoding.UTF8.GetBytes(val);
                return fromEncrypt;
            }
            catch (CryptographicException ex)
            {
                EcrResponse.OnException(string.Format("3Des decriptor exception:{0} data:{1}", ex.Message, sData));
                return null;
            }
        }
        public static string DecryptString3Des(byte[] data, byte[] key)
        {
            try
            {
                TripleDESCryptoServiceProvider tDesAlg = new TripleDESCryptoServiceProvider();
                tDesAlg.Key = key;
                tDesAlg.Mode = CipherMode.ECB;
                tDesAlg.Padding = PaddingMode.PKCS7;
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(data);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tDesAlg.CreateDecryptor(key, tDesAlg.IV),
                    CryptoStreamMode.Read);
                string val;
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    val = srDecrypt.ReadToEnd();
                }

                //Convert the buffer into a string and return it.
                return val;
            }
            catch (CryptographicException ex)
            {
                EcrResponse.OnException(string.Format("3Des decriptor exception:{0} data:{1}", ex.Message,
                    EcrConverter.ConvertByteArrayToString(data)));
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Win 7 or higher, dont work for Win XP
        public static byte[] GetSHA256CryptoHash(string value)
        {
            byte[] result = new byte[24];
            HashAlgorithm hasher = new SHA256CryptoServiceProvider();
            byte[] pasHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            Buffer.BlockCopy(pasHash, 0, result, 0, 24);
            return result;
        }
        public static byte[] TripleDesEncrypt(string input, byte[] key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = key;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return resultArray;
        }
        public static byte[] TripleDesDecrypt(byte[] inputArray, byte[] key)
        {
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = key;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return resultArray;
        }
        #endregion
    }

    public static class EcrConverter
    {
        public static string ConvertByteArrayToString(byte[] data)
        {
            return data.Aggregate(String.Empty, (current, b) => String.Format("{0}{1}{2}", current, String.IsNullOrEmpty(current) ? "" : "-", b));
        }
    }



}

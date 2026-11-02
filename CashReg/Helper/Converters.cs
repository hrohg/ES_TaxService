using System;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CashReg.Helper
{
   
    /// <summary>
    /// 
    /// </summary>
    public static class EcrJsonConverters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string SerializeObect(object value)
        {
            try
            {
                return value == null ? string.Empty : JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeObect<T>(byte[] value)
        {
            return DeserializeObect<T>(Encoding.UTF8.GetString(value));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeObect<T>(string value)
        {
            try
            {
                return value == null
                    ? default(T)
                    :
                    //new JavaScriptSerializer().Deserialize<T>(Encoding.UTF8.GetString(Response));
                    JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ConvertToJson<T>(T obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}

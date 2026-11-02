using System;
using System.Collections.Generic;
using System.Linq;
using CashReg.Enums;
using CashReg.Interfaces;

namespace CashReg.Helper
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EcrSettings : IEcrSettings
    {
        #region External properties
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Crn { get; set; }
        public EcrCashier EcrCashier { get; set; }
        public List<Department> TypeOfOperatorDeps { get; set; }
        public Department CashierDepartment { get; set; }
        public bool UseExternalPrinter { get; set; }
        public string ApplicationIp { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public EcrSettings()
        {EcrCashier = new EcrCashier();}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="serverPort"></param>
        /// <param name="password"></param>
        /// <param name="crn"></param>
        /// <param name="cashier"></param>
        /// <param name="pin"></param>
        /// <param name="cashierDepartment"></param>
        public EcrSettings(string serverUrl, int serverPort, string password, string crn, int? cashier, string pin, Department cashierDepartment = null):this()
        {
            Ip = serverUrl;
            Port = serverPort;
            Password = password;
            Crn = crn;
            if (cashier != null)
            {
                EcrCashier = new EcrCashier((short)cashier, pin);
            }
            CashierDepartment = cashierDepartment;
        }
        #endregion

        #region External methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TypeOfTaxes> GetTypeOfTaxes()
        {
            return Enum.GetValues(typeof(TypeOfTaxes)).Cast<TypeOfTaxes>();

        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class XmlTaggingAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the Xml node or node attribute
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value whether the XmlTaggingAttribute.Name is an attribute name of a Xml node
        /// </summary>
        public bool IsAttribute { get; private set; }

        /// <summary>
        /// Gets the name of the node with the attribute name of XmlTaggingAttribute.Name
        /// </summary>
        public string Node { get; private set; }

        /// <summary>
        /// Initializes a new instance of XmlTaggingAttribute with the name of the Xml node
        /// </summary>
        /// <param name="name">The name of the Xml node</param>
        public XmlTaggingAttribute(string name)
        {
            Name = name;
            Node = "";
            IsAttribute = false;
        }

        /// <summary>
        /// Initializes a new instance of XmlTaggingAttribute with the attribute of a previously declared XmlTaggingAttribute.Name node name
        /// </summary>
        /// <param name="attributeName">The name of the attribute</param>
        /// <param name="nodeName">The name of the node to be attributed. Must be declared in previous class property XmlTaggingAttribute</param>
        public XmlTaggingAttribute(string attributeName, string nodeName)
            : this(attributeName)
        {
            IsAttribute = true;
            Node = nodeName;
        }
    }
}

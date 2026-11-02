using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashReg.Models
{
    public class EcrModel
    {
        #region External properties
        public string CompanyName { get; private set; }
        public string CompanyAddress { get; set; }
        public string Tin { get; private set; }
        #endregion

        public EcrModel(string comapanyName, string tin)
        {
            CompanyName = CompanyName;
            Tin = tin;
        }
    }
}

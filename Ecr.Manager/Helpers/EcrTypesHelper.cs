using System.Collections.Generic;
using TaxServiceInvoices.Models;

namespace Ecr.Manager.Helpers
{
    public static class EcrTypesHelper
    {
        public static List<EnumModel> GetDealTypes()
        {
            return TaxServiceInvoices.Helper.EnumHelper.GetDealTypes();
        }
    }
}

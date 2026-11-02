using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TaxServiceInvoices.Enums;
using TaxServiceInvoices.Models;

namespace TaxServiceInvoices.Helper
{
    public static class EnumHelper
    {
        public static List<EnumModel> GetDealTypes()
        {
            return Enum.GetValues(typeof(DealTypes)).Cast<DealTypes>().Select(s =>
            new EnumModel
            {
                Id = Convert.ToInt32(s),
                Name = EnumHelper.GetEnumName(s),
                Description = GetEnumDescription(s)
            }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute?.Description ?? value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumName(Enum enumValue)
        {
            var member = enumValue.GetType()
                          .GetMember(enumValue.ToString())
                          .FirstOrDefault();

            if (member != null)
            {
                var attr = member.GetCustomAttribute<DisplayAttribute>();
                return attr?.Name ?? enumValue.ToString();
            }

            return enumValue.ToString();
        }
    }
}

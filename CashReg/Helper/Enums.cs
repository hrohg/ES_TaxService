using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using CashReg.Enums;
using CashReg.Interfaces;

namespace CashReg.Helper
{
    public static class Enumerations
    {        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<IEcrDepartment> GetEcrDepartments()
        {
            return new List<IEcrDepartment>((((TypeOfTaxes[])Enum.GetValues(typeof(TypeOfTaxes))).Select(s => new Department { Type = (int)s, Id = (int)s, Name = GetEcrDepartmentDescription(s) }).ToList()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeOfTaxes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetEcrDepartmentDescription(TypeOfTaxes typeOfTaxes)
        {
            string description;
            switch (typeOfTaxes)
            {
                case TypeOfTaxes.None:
                    description = "0 Անհայտ հարկման տեսակ";
                    break;
                case TypeOfTaxes.WithVat:
                    description = "1 ԱԱՀ ով հարկվող";
                    break;
                case TypeOfTaxes.WithoutVat:
                    description = "2 ԱԱՀ ով չհարկվող";
                    break;
                case TypeOfTaxes.TurnoverTax:
                    description = "3 Շրջանառության հարկ";
                    break;
                case TypeOfTaxes.PatentFee:
                    description = "4 Արտոնագրային վճար";
                    break;
                case TypeOfTaxes.FixedFee:
                    description = "5 Հաստատագրված վճար";
                    break;
                case TypeOfTaxes.FamilyBusiness:
                    description = "6 Ընտանեկան ձեռնարկատիրություն";
                    break;
                case TypeOfTaxes.MicroBusiness:
                    description = "7 Միկրո ձեռնարկատիրություն";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("typeOfTaxes", typeOfTaxes, null);
            }
            return description;
        }        
    }

    /// <summary>
    /// Enums
    /// </summary>

    #region Enums

    /// <summary>
    /// Գործարքի տիպ
    /// </summary>
    public enum ActionType
    {
        //0 Վաճառք
        [Description("0")]
        Sale,
        //2 Վերադարձ 
        [Description("2")]
        Return,
        //3 Կանխավճար
        [Description("3")]
        Deposit
    }

    /// <summary>
    /// Շահում
    /// </summary>
    public enum Prize
    {
        [Description("Դուք շահել եք")]
        Prize = 1,
        [Description("Կտրոնը չի շահել")]
        NoPrize = 0
    }

    public enum ReportType
    {
        X = 1,
        Z = 2
    }

    #endregion    

    internal enum EcrActions : byte
    {
        [Description("ՀԴՄ օպերատորների և բաժինների ցանկի ստացում")]
        GetOperatorsAndDeps = 11,
        [Description("Օպերատորի մուտք:")]
        OperatorLogin = 12
    }

    internal enum EcrBaseActions : byte
    {
        [Description("Օպերատորի ելք")]
        LogoutOperator = 21,
        [Description("Կտրոնի տպում")]
        PrintReceiptTicket = 22,
        [Description("Վերջին կտրոնի կրկնօրինակի տպում")]
        PrintLatestTicket = 23,
        [Description("Վերադարձի կտրոնի տպում")]
        PrintReturnTicket = 24,
        [Description("ՀԴՄ հաշվետվությունների տպում")]
        PrintEcrReport = 25,
        [Description("Կտրոնի տվյալների ստացում")]
        GetTicketData = 26,
        [Description("Կտրոնի էջագլխի և էջատակի կարգավորում")]
        ManageHeaderAndFooter = 27,
        [Description("Կտրոնի էջագլխի Լոգո -ի կարգավորում")]
        ManageLogo = 28
    }

    public enum EcrExecuiteActions : byte
    {
        CheckEcrConnection = 202,
        CheckConnection = 201,
        Zero = 0,
        GetOperatorsList = 101,
        GetDepsList = 102,

        [Description("ՀԴՄ օպերատորների և բաժինների ցանկի ստացում")]
        GetOperatorsAndDepList = 1,
        [Description("Օպերատորի մուտք (սեսիայի հաստատում)")]
        OperatorLogin = 2,
        [Description("Օպերատորի ելք (սեսիայի ընդհատում)")]
        LogoutOperator = 3,
        [Description("Կտրոնի տպում (պահանջվում է ընթացիկ սեսիա)")]
        PrintReceiptTicket = 4,
        [Description("Վերջին կտրոնի կրկնօրինակի տպում (պահանջվում է ընթացիկ սեսիա)")]
        PrintLatestTicket = 5,
        [Description("Վերադարձի կտրոնի տպում (պահանջվում է ընթացիկ սեսիա)")]
        PrintReturnTicket = 6,
        [Description("Կտրոնի էջագլխի և էջատակի կարգավորում (պահանջվում է ընթացիկ սեսիա)")]
        ManageHeaderAndFooter = 7,
        [Description("Կտրոնի էջագլխի Լոգո -ի կարգավորում (պահանջվում է ընթացիկ սեսիա)")]
        ManageLogo = 8,
        [Description("ՀԴՄ հաշվետվությունների տպում (պահանջվում է ընթացիկ սեսիա)")]
        PrintEcrReport = 9,
        [Description("Կտրոնի տվյալների ստացում (պահանջվում է ընթացիկ սեսիա)")]
        GetReceiptData = 10,
        [Description("Կանխիկի մուտք կանխիկի ելք (պահանջվում է ընթացիկ սեսիա)")]
        PrintCash = 11,
        [Description("Սարքի ամսաթվի և ժամի ստացում (պահանջվում է ընթացիկ սեսիա)")]
        GetEcrDateTime = 12,
        [Description("Կտրոնի ձևանմուշի տպում (պահանջվում է ընթացիկ սեսիա)")]
        PrintBlankTicket = 13,
        [Description("ՀԴՄ սարքի համաժամանակեցում (պահանջվում է ընթացիկ սեսիա)")]
        SyncEcr = 14,
        [Description("ՀԴՄ սարքում առկա վճարային համակարգերի ցանկի ստացում (պահանջվում է ընթացիկ սեսիա)")]
        GetPaymentSystems = 15,
        [Description("eMark կոդի ստուգում (պահանջվում է ընթացիկ սեսիա)")]
        CheckEmark = 16,

        PrintReportX = 203,
        PrintReportZ = 204,
        CashIn = 211,
        CashWithdrawal = 212
    }

    public enum IsCashInEnum
    {
        CashWithdrawal = 0,
        CashInReceipt = 1
    }

    public enum DiscountType
    {
        Persentage = 1,
        Amountal = 2,
        Total = 4,
        AdditionalPersentage = 8,
        AdditionalAmount = 16
    }

    public enum InvoiceMode
    {
        ProductMode = 2,
        PrepaymentMode = 3
    }
}

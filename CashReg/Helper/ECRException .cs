using System;
using System.IO;
using System.Linq;

namespace CashReg.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class EcrResponse
    {
        /// <summary>
        /// 4.9 Սխալների կոդերի ցանկ
        /// </summary>
        public enum ResponseCodes
        {
            ResponseIsNull = 1,
            UnknownError = 0,
            /// <summary>
            /// Լրացուցիչ սխալներ
            /// </summary>

            /// <summary>
            /// ՀԴՄ հարցման սխալ (Timeout)
            /// </summary>
            RequestTimeoutException = 11,
            /// <summary>
            /// Կապի խնդիր
            /// </summary>
            NetworkConnectionError = 12,
            /// <summary>
            /// ՀԴՄ -ի կապի խնդիր
            /// </summary>
            EcrNetworkConnectionError = 13,
            /// <summary>
            /// ՀԴՄ կարգավորումների սխալ: Կարգավորումները թերի են:
            /// </summary>
            EcrSettingsError = 14,
            /// <summary>
            /// ՀԴՄ կարգավորումների սխալ: ՀԴՄ օպերատորի տվյալները թերի են:
            /// </summary>
            EcrCashierSettingsError = 15,
            /// <summary>
            /// Գործողությունն ընդհատվել է օգտագործողի կողմից:
            /// </summary>
            CanceledByUser = 21,
            /// <summary>
            /// ՀԴՄ հարցման սխալ:
            /// </summary>
            EcrRequestException = 41,
            /// <summary>
            /// ՀԴՄ պատասխանի սխալ:
            /// </summary>
            EcrResponceException = 42,
            /// <summary>
            /// Ընդհանուր սխալներ
            /// </summary>

            /// <summary>
            /// Ընդհանուր
            /// </summary>
            #region Ընդհանուր
            /// <summary>
            /// Գործողության բարեհաջող ավարտ
            /// </summary>
            Ok = 200,
            /// <summary>
            /// ՀԴՄ ներքին սխալ
            /// Ընդհանուր տիպի չդասակարգված սխալ
            /// </summary>
            INTERNAL_ERROR = 500,
            /// <summary>
            /// Հարցման սխալ
            /// Վերադարձվում է, երբ հարցումը չի վերծանվում
            /// </summary>
            ERROR_BAD_REQUEST = 400,
            /// <summary>
            /// Սխալ արձանագրության տարբերակ
            /// </summary>
            ERROR_WRONG_PROTOCOL_VERSION = 402,
            /// <summary>
            /// Չարտոնագրված միացում
            /// Վերադարձվում է, երբ ՀԴՄ- ում կարագավորված IP հասցեն 
            /// չի համընկնում կապ հաստատած սարքի IP հասցեի հետ
            /// </summary>
            ERROR_UNAUTHORIZED_CONNECTION = 403,
            /// <summary>
            /// Սխալ գործողության կոդ
            /// Վերադարձվում է, երբ Գլխագրում նշված
            /// գործողության կոդը սխալ է
            /// </summary>
            ERROR_BAD_OP_CODE = 404,
            /// <summary>
            /// Գաղտնաբառով կոդավորման սխալ
            /// </summary>
            LOGIN_ERROR_WRONG_PASS = 101,
            /// <summary>
            /// Սեսիայի բանալիով կոդավորման սխալ
            /// </summary>
            ERROR_WRONG_SESSION = 102,
            /// <summary>
            /// Գլխագրի ֆորմատի սխալ
            /// </summary>
            ERROR_WRONG_REQ_HEADER = 103,
            /// <summary>
            /// Հարցման հերթական համարի սխալ
            /// </summary>
            ERROR_WRONG_RSEQ = 104,
            /// <summary>
            /// JSON ֆորմատավորման սխալ
            /// </summary>
            ERROR_WRONG_JSON = 105,
            /// <summary>
            /// Վերջին կտրոնի գրառումը բացակայում է
            /// </summary>
            LAST_RECEIPT_NO_FILE = 141,
            /// <summary>
            /// Վերջին կտրոնը պատկանում է այլ օգտատիրոջը
            /// </summary>
            LAST_RECEIPT_ANOTHER_CASHIER = 142,
            /// <summary>
            /// Տպիչի ընդհանուր սխալ
            /// </summary>
            LAST_RECEIPT_PRN_FAIL = 143,
            /// <summary>
            /// Տպիչի ինիցիալիզացիայի սխալ
            /// </summary>
            LAST_RECEIPT_PRN_INIT = 144,
            /// <summary>
            /// Տպիչում վերջացել է թուղթը
            /// </summary>
            LAST_RECEIPT_PRN_NO_PAPER = 145,
            #endregion

            /// <summary>
            /// Մուտքի գործառույթի սխալներ
            /// </summary>

            #region Մուտքի գործառույթի սխալներ
            /// <summary>
            /// Օպերատորի գաղտնաբառի սխալ
            /// </summary>
            LOGIN_ERROR_WRONG_USER = 111,
            /// <summary>
            /// Այդպիսի օպերատոր գոյություն չունի
            /// Հնարավոր է երեք դեպք՝
            /// 1. Տվյալ օգտվողի դերը օպերատոր չէ
            /// 2. Տվյալ օգտվողը ակտիվ չէ
            /// 3. Այդպիսի օգտվող գրանցված չէ
            /// </summary>
            LOGIN_ERROR_NO_SUCH_USER = 112,
            /// <summary>
            /// Օպերատորը ակտիվ չէ
            /// </summary>
            LOGIN_ERROR_INACTIVE_USER = 113,
            /// <summary>
            /// Սխալ օգտվող
            /// </summary>
            WRONG_USER = 121,
            #endregion
            /// <summary>
            /// Կտրոնի տպման գործառույթի սխալներ
            /// </summary>

            #region Կտրոնի տպման գործառույթի սխալներ
            /// <summary>
            /// Այդպիսի բաժին գոյություն չունի
            /// Այս սխալը կվերադարձվի նաև այն
            /// դեպքում, երբ օպերատորին տվյալ
            /// բաժինը կցված չէ
            /// </summary>
            PRINT_RECEIPT_NO_DEPT = 151,
            /// <summary>
            /// Մուծված գումարը ընդհանուր գումարից պակաս է
            /// </summary>
            PRINT_RECEIPT_PAID_LESS = 152,
            /// <summary>
            /// Կտրոնի գումարը գերազանզում է սահմանված շեմը
            /// </summary>
            PRINT_RECEIPT_LIMIT = 153,
            /// <summary>
            /// Կտրոնի գումարը պետք է լինի դրական թիվ
            /// </summary>
            PRINT_RECEIPT_AMOUNT_POS = 154,
            /// <summary>
            /// Անհրաժեշտ է համաժամանակեցնել ՀԴՄ-ն
            /// </summary>
            PRINT_RECEIPT_SYNC_NEEDED = 155,
            /// <summary>
            /// Համաժամանակեցումը չհաջողվեց
            /// </summary>
            PRINT_RECEIPT_SYNC_FAILED = 156,
            /// <summary>
            /// Սխալ վերադարձի կտրոնի համար
            /// </summary>
            RECEIPT_RETURN_INVALID_ID = 157,
            /// <summary>
            /// Կտրոնը արդեն վերադարձված է
            /// </summary>
            RECEIPT_RETURN_ALREADY_RETURNED = 158,
            /// <summary>
            /// Ապրանքի գինը և քանակը չի կարող լինել ոչ դրական
            /// </summary>
            RECEIPT_PRICE_QTY_NONZERO = 159,
            /// <summary>
            /// Զեղչի տոկոսը պետք է լինի ոչ բացասական թիվ և լինի 100-ից փոքր
            /// </summary>
            RECEIPT_PRICE_INVALID_DISCOUNT_PERCENT = 160,
            /// <summary>
            /// Ապրանքի կոդը սխալ է
            /// </summary>
            PRINT_RECEIPT_ERROR_EMPTY_CODE = 161,
            /// <summary>
            /// Ապրանքի անվանումը սխալ է
            /// </summary>
            PRINT_RECEIPT_ERROR_EMPTY_NAME = 162,
            /// <summary>
            /// Ապրանքի չափման միավորը չի կարող լինել դատարկ
            /// </summary>
            Product_Mu_Cant_be_Null = 163,
            /// <summary>
            /// Անկանխիկ վճարման խափանում
            /// </summary>
            Cash_Error = 164,
            /// <summary>
            /// Ապրանքի գինը չի կարող լինել 0
            /// </summary>
            Receipt_Amount_Cant_be_Null = 165,
            /// <summary>
            /// Վերջնական գնի հաշվարկի սխալ
            /// </summary>
            Receipt_Wrong_Amount = 166,
            /// <summary>
            /// Անկանխիկ գումարը ավելի մեծ է քան կտրոնի ընդհանուր գումարը
            /// </summary>
            Cashless_More_Than_Amount = 167,
            /// <summary>
            /// Անկանխիկ գումարը ծածկում է ընդհանուր գումարը (Կանխիկ գումարը ավելորդ է)
            /// </summary>
            Cash_Is_Unnecessary = 168,
            /// <summary>
            /// Ֆիսկալ հաշվետվության ֆիլտրների սխալ ընտրություն (մեկից ավել ֆիլտրերի դաշտ է ուղարկվել)
            /// </summary>
            Fiscal_Report_Filters_Issue = 169,
            /// <summary>
            /// Ֆիսկալ հաշվետվության ժամանակ սխալ ամսաթվային միջակայք է ուղարկվել: Միջակայքը չպետք է գերազանցի 2 ամիսը
            /// </summary>
            Fiscal_Report_Date_Issue = 170,
            /// <summary>
            /// Ապրանքի գնի անթույլատրելի արժեք
            /// </summary>
            Product_Price_Unacceptable = 171,
            /// <summary>
            /// Կտրոնը՝ ապրանքներով, պարզ կամ կանխավճարի կտրոն չէ
            /// </summary>
            Ticket_Without_Products = 172,
            /// <summary>
            /// Սխալ զեղչի տեսակ
            /// </summary>
            Wrong_Discount_Type = 173,
            /// <summary>
            /// Վերադարձվող կտրոնը գոյություն չունի
            /// </summary>
            Return_RECEIPT_Does_Not_Exists = 174,
            /// <summary>
            /// Վերադարձվող կտրոնի սխալ գրանցման համար
            /// </summary>
            Wrong_return_RECEIPT_Number = 175,
            /// <summary>
            /// Վերջին կտրոնը գոյություն չունի
            /// </summary>
            Last_RECEIPT_Does_Not_Exsists = 176,
            /// <summary>
            /// Նշված տիպի կտրոնների համար վերադարձ հնարավոր չէ կատարել
            /// </summary>
            Wrong_Return_RECEIPT_Type = 177,
            /// <summary>
            /// Հարցված գումարը հնարավոր չէ վերադարձնել
            /// </summary>
            Requested_Amount_Can_Not_Be_Returned = 178,
            /// <summary>
            /// Մասնակի վճարում կտրոնը պետք է վերադարձվի ամբողջությամբ
            /// </summary>
            Partial_Payment = 179,
            /// <summary>
            /// Ամբողջական վերադարձի ավել գումար
            /// </summary>
            Complete_Return_Money = 180,
            /// <summary>
            /// Վերադարձվող ապրանքի սխալ քանակ
            /// </summary>
            Wrong_Return_Product_Count = 181,
            /// <summary>
            /// Վերադարձվող կտրոնը իրենից վերադարձ տիպի կտրոն է ներկայացնում
            /// </summary>
            Return_RECEIPT_Is_Return_RECEIPT = 182,
            /// <summary>
            /// Սխալ ԱՏԳԱԱ/ԱԴԳՏ կոդ
            /// </summary>
            Wrong_ADGT_Code = 183,
            /// <summary>
            /// Կանխավճարի վերադարձի անթույլատրելի հարցում
            /// </summary>
            Prepayment_Return_Inadmissible = 184,
            /// <summary>
            /// Հնարավոր չէ կատարել տվյալ կտրոնի վերադարձը: Անհրաժեշտ է ՀԴՄ ծրագրի համաժամանակեցում
            /// </summary>
            Return_RECEIPT_Inadmissible = 185,
            /// <summary>
            /// Կանխավճարի դեպքում սխալ գումար
            /// </summary>
            Wrong_Prepayment_Amount = 186,
            /// <summary>
            /// Կանխավճարի դեպքում սխալ ցուցակ
            /// </summary>
            Wrong_Prepaymetn_List = 187,
            /// <summary>
            /// Սխալ գումարներ
            /// </summary>
            Wrong_Amounts = 188,
            /// <summary>
            /// Սխալ կլորացում
            /// </summary>
            Wrong_Rounding = 189,
            /// <summary>
            /// Վճարումը հասանելի չէ
            /// </summary>
            Payment_Unavailable = 190,
            /// <summary>
            /// Կանխիկի մուտքի/ելքի ժամանակ գումարը պետք է լինի մեծ 0-ից
            /// </summary>
            Wrong_Cash_InOut_Amount = 191,
            /// <summary>
            /// ԱՏԳ կոդը պարտադիր է
            /// </summary>
            ATG_AA_IS_MISSING = 192,
            /// <summary>
            /// Գնորդի ՀՎՀՀ-ի ֆորմատը սխալ է
            /// </summary>
            Wrong_Partner_Tax = 193,
            
            /// <summary>
            /// Կանխավճարի դեպքում չպետք է ուղարկվեն emark կոդեր
            /// </summary>
            Emark_Not_Needed = 194,
            
            /// <summary>
            ///  emark կոդի ֆորմատի սխալ
            /// </summary>
            Wrong_Emark_Code = 195,

            /// <summary>
            /// Այլ երկրի ծածկագիր
            /// </summary>
            Other_Country_Code = 196,
            #endregion

            /// <summary>
            /// Անհայտ սխալներ
            /// </summary>

            #region Անհայտ սխալներ
            /// <summary>
            /// Անհայտ սխալ
            /// </summary>
            UNKNOWN_RESPONSE_CODE = -1,
            ERROR_NO_USER = 121,
            SIMPLE_RECEIPT_NOT_ALLOWED = 410,
            #endregion

        }
        public ResponseCodes Code { get; protected set; }
        public string Description { get => GetExceptionDescription(Code); }
        public EcrResponse(ResponseCodes responseCode)
        {
            Code = responseCode;
            //MessageBox.Show("An error occurred. Error code: " + responseCode);
        }
        public static ResponseCodes GetException(ResponseCodes value)
        {
            foreach (var t in ((ResponseCodes[])Enum.GetValues(typeof(ResponseCodes))).Where(t => t == value))
            {
                return t;
            }
            return ResponseCodes.UNKNOWN_RESPONSE_CODE;
        }

        #region Excaption descriptions
        public static string GetExceptionDescription(ResponseCodes value)
        {
            string errorDescription = null;
            switch (value)
            {
                case ResponseCodes.ResponseIsNull:
                    errorDescription = "Null response:";
                    break;
                //Լրացուցիչ
                // Error code = 1;
                case ResponseCodes.RequestTimeoutException:
                    errorDescription = "ՀԴՄ հարցման սխալ (Timeout):";
                    break;
                // Error code = 2;
                case ResponseCodes.NetworkConnectionError:
                    errorDescription = "Կապի խնդիր:";
                    break;
                case ResponseCodes.EcrNetworkConnectionError:
                    errorDescription = "ՀԴՄ -ի կապի խնդիր:";
                    break;
                // Error code = 3;
                case ResponseCodes.EcrSettingsError:
                    errorDescription = "ՀԴՄ կարգավորումների սխալ: Կարգավորումները թերի են:";
                    break;
                // Error code = 4;
                case ResponseCodes.EcrCashierSettingsError:
                    errorDescription = "ՀԴՄ կարգավորումների սխալ: ՀԴՄ օպերատորի տվյալները թերի են:";
                    break;
                // Error code = 21;
                case ResponseCodes.CanceledByUser:
                    errorDescription = "Գործողությունն ընդհատվել է օգտագործողի կողմից:";
                    break;
                case ResponseCodes.EcrRequestException:
                    errorDescription = "ՀԴՄ հարցման սխալ:";
                    break;
                case ResponseCodes.EcrResponceException:
                    errorDescription = "ՀԴՄ պատասխանի սխալ:";
                    break;
                //Ընդհանուր

                // Error code = 200;
                case ResponseCodes.Ok:
                    errorDescription = "Գործողության բարեհաջող ավարտ:";
                    break;
                // Error code = 500;
                case ResponseCodes.INTERNAL_ERROR:
                    errorDescription = "ՀԴՄ ներքին սխալ";
                    break;
                // Error code = 400;
                case ResponseCodes.ERROR_BAD_REQUEST:
                    errorDescription = "Հարցման սխալ";
                    break;
                // Error code = 402;
                case ResponseCodes.ERROR_WRONG_PROTOCOL_VERSION:
                    errorDescription = "Սխալ արձանագրության տարբերակ";
                    break;
                // Error code = 403;
                case ResponseCodes.ERROR_UNAUTHORIZED_CONNECTION:
                    errorDescription = "Չարտոնագրված միացում";
                    break;
                // Error code = 404;
                case ResponseCodes.ERROR_BAD_OP_CODE:
                    errorDescription = "Սխալ գործողության կոդ";
                    break;
                // Error code = 410;
                case ResponseCodes.SIMPLE_RECEIPT_NOT_ALLOWED:
                    errorDescription = "Սխալ 410";
                    break;
                // Error code = 101;
                case ResponseCodes.LOGIN_ERROR_WRONG_PASS:
                    errorDescription = "Գաղտնաբառով կոդավորման սխալ:";
                    break;
                // Error code = 102;
                case ResponseCodes.ERROR_WRONG_SESSION:
                    errorDescription = "Սեսիայի բանալիով կոդավորման սխալ:";
                    break;
                // Error code = 103;
                case ResponseCodes.ERROR_WRONG_REQ_HEADER:
                    errorDescription = "Գլխագրի ֆորմատի սխալ:";
                    break;
                // Error code = 104;
                case ResponseCodes.ERROR_WRONG_RSEQ:
                    errorDescription = "Հարցման հերթական համարի սխալ:";
                    break;
                // Error code = 105;
                case ResponseCodes.ERROR_WRONG_JSON:
                    errorDescription = "JSON ֆորմատավորման սխալ:";
                    break;
                // Error code = 141;
                case ResponseCodes.LAST_RECEIPT_NO_FILE:
                    errorDescription = "Վերջին կտրոնի գրառումը բացակայում է:";
                    break;
                // Error code = 142;
                case ResponseCodes.LAST_RECEIPT_ANOTHER_CASHIER:
                    errorDescription = "Վերջին կտրոնի պատկանում է այլ օգտատիրոջը:";
                    break;
                // Error code = 143;
                case ResponseCodes.LAST_RECEIPT_PRN_FAIL:
                    errorDescription = "Տպիչի ընդհանուր սխալ:";
                    break;
                // Error code = 144;
                case ResponseCodes.LAST_RECEIPT_PRN_INIT:
                    errorDescription = "Տպիչի ինիցիալիզացիայի սխալ:";
                    break;
                // Error code = 145;
                case ResponseCodes.LAST_RECEIPT_PRN_NO_PAPER:
                    errorDescription = "Տպիչում վերջացել է թուղթը:";
                    break;

                //Մուտքի գործառ ույթի սխալներ

                // Error code = 111;
                case ResponseCodes.LOGIN_ERROR_WRONG_USER:
                    errorDescription = "Օպերատորի գաղտնաբառի սխալ:";
                    break;
                // Error code = 112;
                case ResponseCodes.LOGIN_ERROR_NO_SUCH_USER:
                    errorDescription = "Այդպիսի օպերատոր գոյություն չունի:";
                    break;
                // Error code = 113;
                case ResponseCodes.LOGIN_ERROR_INACTIVE_USER:
                    errorDescription = "Օպերատորը ակտիվ չէ:";
                    break;

                // Error code = 121;
                case ResponseCodes.ERROR_NO_USER:
                    errorDescription = "Սխալ օգտվող:";
                    break;

                //Կտրոնի տպման գործառույթի սխալներ

                // Error code = 151;
                case ResponseCodes.PRINT_RECEIPT_NO_DEPT:
                    errorDescription = "Այդպիսի բաժին գոյություն չունի:";
                    break;
                // Error code = 152;
                case ResponseCodes.PRINT_RECEIPT_PAID_LESS:
                    errorDescription = "Մուծված գումարը ընդհանուր գումարից պակաս է:";
                    break;
                // Error code = 153;
                case ResponseCodes.PRINT_RECEIPT_LIMIT:
                    errorDescription = "Կտրոնի գումարը գերազանզում է սահմանված շեմը:";
                    break;
                // Error code = 154;
                case ResponseCodes.PRINT_RECEIPT_AMOUNT_POS:
                    errorDescription = "Կտրոնի գումարը պետք է լինի դրական թիվ:";
                    break;
                // Error code = 155;
                case ResponseCodes.PRINT_RECEIPT_SYNC_NEEDED:
                    errorDescription = "Անհրաժեշտ է համաժամանակեցնել ՀԴՄ-ն:";
                    break;
                // Error code = 156
                case ResponseCodes.PRINT_RECEIPT_SYNC_FAILED:
                    errorDescription = "Համաժամանակեցումը չհաջողվեց";
                    break;
                // Error code = 157;
                case ResponseCodes.RECEIPT_RETURN_INVALID_ID:
                    errorDescription = "Սխալ վերադարձի կտրոնի համար:";
                    break;
                // Error code = 158;
                case ResponseCodes.RECEIPT_RETURN_ALREADY_RETURNED:
                    errorDescription = "Կտրոնը արդեն վերադարձված է:";
                    break;
                // Error code = 159;
                case ResponseCodes.RECEIPT_PRICE_QTY_NONZERO:
                    errorDescription = "Ապրանքի գինը և քանակը չի կարող լինել ոչ դրական:";
                    break;
                // Error code = 160;
                case ResponseCodes.RECEIPT_PRICE_INVALID_DISCOUNT_PERCENT:
                    errorDescription = "Զեղչի տոկոսը պետք է լինի ոչ բացասական թիվ և լինի 100-ից փոքր:";
                    break;
                // Error code = 161;
                case ResponseCodes.PRINT_RECEIPT_ERROR_EMPTY_CODE:
                    errorDescription = "Ապրանքի կոդը չի կարող լինել դատարկ:";
                    break;
                // Error code = 162;
                case ResponseCodes.PRINT_RECEIPT_ERROR_EMPTY_NAME:
                    errorDescription = "Ապրանքի անվանումը չի կարող լինել դատարկ:";
                    break;
                // Error code = 163;
                case ResponseCodes.Product_Mu_Cant_be_Null:
                    errorDescription = "Ապրանքի չափման միավորը չի կարող լինել դատարկ:";
                    break;
                // Error code = 164;
                case ResponseCodes.Cash_Error:
                    errorDescription = "Անկանխիկ վճարման գործարքը չհաջողվեց իրականացնել:";
                    break;
                // Error code = 165;
                case ResponseCodes.Receipt_Amount_Cant_be_Null:
                    errorDescription = "ՀԴՄ կրտրոնում մեկ ապրանքի տող հաշվարկված ընդհանուր գումարը չի կարող լինել դատարկ կամ չի կարող լինել ոչ դրական:";
                    break;
                // Error code = 166;
                case ResponseCodes.Receipt_Wrong_Amount:
                    errorDescription = "ՀԴՄ կրտրոնում մեկ ապրանքի տողի հաշվարկված ընդհանուր գումարի հաշվարկը սխալ է:";
                    break;
                // Error code = 167;
                case ResponseCodes.Cashless_More_Than_Amount:
                    errorDescription = "Անկանխիկ գումարը ավելի մեծ է քան կտրոնի ընդհանուր գումարը:";
                    break;
                // Error code = 168;
                case ResponseCodes.Cash_Is_Unnecessary:
                    errorDescription = "Անկանխիկ գումարը ծածկում է ընդհանուր գումարը (Կանխիկ գումարը ավելորդ է):";
                    break;
                // Error code = 169;
                case ResponseCodes.Fiscal_Report_Filters_Issue:
                    errorDescription = "Ֆիսկալ հաշվետվության ֆիլտերների սխալ ընտրություն (մեկից ավել ֆիլտերի դաշտ է ուղարկվել):";
                    break;
                // Error code = 170;
                case ResponseCodes.Fiscal_Report_Date_Issue:
                    errorDescription = "Ֆիսկալ հաշվետվության ժամանակ սխալ ամսաթվային միջակայք է ուղարկվել:  Միջակայքը չպետք է գերազանցի 2 ամիսը:";
                    break;
                // Error code = 171;
                case ResponseCodes.Product_Price_Unacceptable:
                    errorDescription = "Ապրանքի գնի անթույլատրելի արժեք:";
                    break;
                // Error code = 172;
                case ResponseCodes.Ticket_Without_Products:
                    errorDescription = "Կտրոնը ապրանքներով կտրոն չէ:";
                    break;
                // Error code = 173;
                case ResponseCodes.Wrong_Discount_Type:
                    errorDescription = "Սխալ զեղչի տեսակ:";
                    break;
                // Error code = 174;
                case ResponseCodes.Return_RECEIPT_Does_Not_Exists:
                    errorDescription = "Վերադարձվող կտրոնը գույություն չունի:";
                    break;
                // Error code = 175;
                case ResponseCodes.Wrong_return_RECEIPT_Number:
                    errorDescription = "Վերադարձվող կտրոնի սխալ գրանցման համար:";
                    break;
                // Error code = 176;
                case ResponseCodes.Last_RECEIPT_Does_Not_Exsists:
                    errorDescription = "Վերջին կտրոնը գույություն չունի:";
                    break;
                // Error code = 177;
                case ResponseCodes.Wrong_Return_RECEIPT_Type:
                    errorDescription = "Նշված տիպի կտրոնների համար վերադարձ հնարավոր չէ կատարել:";
                    break;
                // Error code = 178;
                case ResponseCodes.Requested_Amount_Can_Not_Be_Returned:
                    errorDescription = "Հարցված գումարը հնարավոր չէ վերադարձնել:";
                    break;
                // Error code = 179;
                case ResponseCodes.Partial_Payment:
                    errorDescription = "Մասնակի վճարում կտրոնը պետք է վերադարձվի ամբողջությամբ:";
                    break;
                // Error code = 180;
                case ResponseCodes.Complete_Return_Money:
                    errorDescription = "Ամբողջական վերադարձի ավել գումար:";
                    break;
                // Error code = 181;
                case ResponseCodes.Wrong_Return_Product_Count:
                    errorDescription = "Վերադարձվող ապրանքի սխալ քանակ:";
                    break;
                // Error code = 182;
                case ResponseCodes.Return_RECEIPT_Is_Return_RECEIPT:
                    errorDescription = "Վերադարձվող կտրոնը իրենից վերադարձ տիպի կտրոն է ներկայացնում:";
                    break;
                // Error code = 183;
                case ResponseCodes.Wrong_ADGT_Code:
                    errorDescription = "Սխալ ԱԴԳՏ կոդ:";
                    break;
                // Error code = 184;
                case ResponseCodes.Prepayment_Return_Inadmissible:
                    errorDescription = "Կանխավճարի վերադարձի անթույլատրելի հարցում:";
                    break;
                // Error code = 185;
                case ResponseCodes.Return_RECEIPT_Inadmissible:
                    errorDescription = "Հնարավոր չէ կատարել տվյալ կտրոնի վերադարձը: Անհրաժեշտ է ՀԴՄ ծրագրի համաժամանակեցում:";
                    break;
                // Error code = 186;
                case ResponseCodes.Wrong_Prepayment_Amount:
                    errorDescription = "Կանխավճարի դեպքում սխալ գումար:";
                    break;
                // Error code = 187;
                case ResponseCodes.Wrong_Prepaymetn_List:
                    errorDescription = "Կանխավճարի դեպքում սխալ ցուցակ:";
                    break;
                // Error code = 188;
                case ResponseCodes.Wrong_Amounts:
                    errorDescription = "Սխալ գումարներ:";
                    break;
                // Error code = 189;
                case ResponseCodes.Wrong_Rounding:
                    errorDescription = "Սխալ կլորացում:";
                    break;
                // Error code = 190;
                case ResponseCodes.Payment_Unavailable:
                    errorDescription = "Վճարումը հասանելի չէ:";
                    break;
                // Error code = 191;
                case ResponseCodes.Wrong_Cash_InOut_Amount:
                    errorDescription = "Կանխիկի մուտք ելքի ժամանակ գումարը պետք է լինի մեծ 0-ից";
                    break;                
                // Error code = 192;
                case ResponseCodes.ATG_AA_IS_MISSING:
                    errorDescription = "ԱՏԳ ԱԱ/ԱԴԳՏ կոդը բացակայում է";
                    break;
                // Error code = 193;
                case ResponseCodes.Wrong_Partner_Tax:
                    errorDescription = "Գնորդի ՀՎՀՀ-ի ֆորմատը սխալ է";
                    break;
                // Error code = 194
                case ResponseCodes.Emark_Not_Needed:
                    errorDescription = "Կանխավճարի դեպքում չպետք է ուղարկվեն emark կոդեր";
                    break;
                // Error code = 195
                case ResponseCodes.Wrong_Emark_Code:
                    errorDescription = "emark կոդի ֆորմատի սխալ";
                    break;                
                // Error code = 196;
                case ResponseCodes.Other_Country_Code:
                    errorDescription = "Այլ երկրի ծածկագիր";
                    break;
                // Error code = -1;
                case ResponseCodes.UNKNOWN_RESPONSE_CODE:                    
                case ResponseCodes.UnknownError:
                default:
                    errorDescription = "Անհայտ սխալ";
                    break;
            }
            return errorDescription;
        }
        #endregion

        public static void OnException(string message)
        {
            var exLogPath = AppDomain.CurrentDomain.BaseDirectory + @"EcrExLog.txt";
            try
            {
                if (!File.Exists(exLogPath))
                {
                    var file = File.Create(exLogPath);
                    file.Close();
                }

                message = string.Format("Date:{0} message:{1}\n", DateTime.Now, message);
                File.AppendAllLines(exLogPath, new[] { message });
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}

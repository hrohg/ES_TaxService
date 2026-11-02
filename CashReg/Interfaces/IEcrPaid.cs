namespace CashReg.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    //[ComVisible(true)]
    public interface IEcrPaid
    {
        /// <summary>
        /// Առձեռն վճարված գումար
        /// </summary>
        double? PaidAmount { get; set; }

        /// <summary>
        /// Անկանխիկ վճարված գումար
        /// </summary>
        double? PaidAmountCard { get; set; }

        /// <summary>
        /// Մասնակի վճարման գումար
        /// </summary>
        double? PartialAmount { get; set; }

        /// <summary>
        /// Կանխավճարի օգտագործման գումար
        /// </summary>
        double? PrePaymentAmount { get; set; }

        /// <summary>
        /// Այլ վճարային տերմինալի օգտագործում:
        /// </summary>
        bool UseExtPos { get; set; }

        /// <summary>
        /// Վճարային համակարգի կոդը useExtPOS -ը false արժեքի դեպքում։
        /// Կարող է լինել null եթե ՀԴՄ սարքում առկա է միային մեկ վճարային համակարգ կամ անհրաժեշտ է ընտրել վճարային համակարգը ՀԴՄ սարքում։ 
        /// Կոդերը ներկայացված են «Վճարային համակարգերի կոդեր» աղյուսակում։
        /// </summary>
        int? PaymentSystem { get; set; }
    }
}
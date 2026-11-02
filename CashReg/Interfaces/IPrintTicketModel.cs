using System.Collections.Generic;
using CashReg.Helper;

namespace CashReg.Interfaces
{
    /// <summary>
    /// Հարցում
    /// </summary>
    public interface IPrintTicketModel
    {
        /// <summary>
        /// Հարցման հերթական համարը
        /// </summary>
        int seq { get; }
        /// <summary>
        /// Առձեռն վճարված գումարը
        /// </summary>
        double paidAmount { get; set; }
        /// <summary>
        /// Անկանխիկ վճարված գումարը
        /// </summary>
        double paidAmountCard { get; set; }
        /// <summary>
        /// Մասնակի վճարման գումարը
        /// </summary>
        double partialAmount { get; set; }
        /// <summary>
        /// Կանխավճարի օգտագործման գումար
        /// </summary>
        double prePaymentAmount { get; set; }
        
        /// <summary>
        /// Կտրոնի տպման ռեժիմ: 2 – Ապրանքներ ռեժիմ 3 – Կանխավճար (այլ արժեքներ չեն ընդունվում)
        /// </summary>
        int mode { get; set; }
        
        /// <summary>
        /// Գնորդի ՀՎՀՀ 8 նիշանոց թիվ կամ null
        /// </summary>
        string partnerTin { get; set; }

        /// <summary>
        /// Պարզ կտրոնի բաժին։ Ուղարկվում է միայն պարզ կտրոնի տպման դեպքում
        /// </summary>
        int? dep { get; set; }

        /// <summary>
        /// Այլ վճարային տերմինալի օգտագործում: Վերաբերվում է անկանխիկ վճարմանը (եթե արժեքը սխալ է, ՀԴՄ-ն միացնում է իր ներքին անկանխիկ վճարման ծրագիրը)
        /// </summary>
        bool useExtPOS { get; set; }

        /// <summary>
        /// Վճարային համակարգի կոդը useExtPOS -ը false արժեքի դեպքում։
        ///Կարող է լինել null եթե ՀԴՄ սարքում առկա է միային մեկ վճարային համակարգ կամ անհրաժեշտ է ընտրել վճարային համակարգը ՀԴՄ սարքում։
        ///Կոդերը ներկայացված են «Վճարային համակարգերիկոդեր» աղյուսակում։
        /// </summary>
        int? PaymentSystem { get; set; }

        /// <summary>
        /// Կտրոնում առկա ապրանքների emark կոդերը string-երի զանգվածի տեսքով
        /// </summary>
        string[] eMarks { get; set; }

        /// <summary>
        /// Կտրոնի պարունակությունը (ապրանքների ցանկ)
        /// </summary>
        IList<IEcrProduct> items { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void Add(IEcrProduct item);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productItems"></param>
        void AddRange(List<EcrProduct> productItems);
    }
}
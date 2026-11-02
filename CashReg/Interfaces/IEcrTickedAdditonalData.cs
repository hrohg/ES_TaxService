namespace CashReg.Interfaces
{
    public interface IEcrTickedAdditonalData
    {
        /// <summary>
        /// Գնորդի ՀՎՀՀ 8 նիշանոց թիվ կամ null
        /// </summary>
        string PartnerTin { get; set; }

        /// <summary>
        /// Կտրոնում առկա ապրանքների emark կոդերը string-երի զանգվածի տեսքով
        /// </summary>
        string[] EMarks { get; set; }
    }
}

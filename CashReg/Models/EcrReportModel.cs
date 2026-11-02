using CashReg.Helper;

namespace CashReg.Models
{
    public class EcrReportRequestModel
    {
        #region External properties
        public int seq { get; set; }
        public int reportType { get; set; }
        public int? deptId { get; set; }
        public int? cashierId { get; set; }
        public int? transactionTypeId { get; set; }
        public long startDate { get; set; }
        public long endDate { get; set; }
        #endregion
        public EcrReportRequestModel(int seqValue, ReportType type, long startDateValue, long endDateValue)
        {
            seq = seqValue;
            reportType = (int) type;
            startDate = startDateValue;
            endDate = endDateValue;
        }

    }
}

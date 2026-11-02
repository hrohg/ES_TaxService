using System;
using System.Windows.Forms;

namespace Ecr.Manager.WinForm.Controls
{
    public partial class SelectDateIntermediateForm : Form
    {
        public DateTime StartDate { get { return DtpStartDate.Value; } set { DtpStartDate.Value = value; } }
        public DateTime EndDate { get { return DtpEndDate.Value; } set { DtpEndDate.Value = value; } }
        public SelectDateIntermediateForm()
        {
            InitializeComponent();
            DtpStartDate.Value = DateTime.Today;
            DtpEndDate.Value = DateTime.Now;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DtpDate_ValueChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = (DtpStartDate.Value <= DtpEndDate.Value);
        }
    }
}

using System;
using System.Windows.Forms;

namespace Ecr.Manager.WinForm.Controls
{
    public partial class InputForm : Form
    {
        public string TicketValue { get { return TxtReturnTicket.Text; } set { TxtReturnTicket.Text = value; } }
        public InputForm(string title)
        {
            InitializeComponent();
            txtLable.Text = title;
        }
        public InputForm(string value, string title):this(title)
        {
            TicketValue = value;
            TxtReturnTicket.Focus();
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TxtReturnTicket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnAccept_Click(sender, null);
            }
        }
    }
}

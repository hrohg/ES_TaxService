using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CashReg.Models;
using TaxServiceInvoices.Helper;

namespace CashReg.Helper
{
    public partial class SelectProductForm : Form
    {
        public List<TotalModel> Products { get; private set; }
        public SelectProductForm(List<TotalModel> products)
        {
            Products = products;
            var bindingList = new BindingList<TotalModel>(Products);
            var source = new BindingSource(bindingList, null);
            InitializeComponent();
            totalModelBindingSource.DataSource = source.DataSource;

        }

        public List<TotalModel> SelectedProducts
        {
            get
            {
                var products = new List<TotalModel>();
                foreach (DataGridViewRow row in DgvDataGridView.Rows)
                {
                    //var quantity = (decimal)row.Cells[qtyDataGridViewTextBoxColumn.Name].Value;
                    //var price = (decimal)row.Cells[pDataGridViewTextBoxColumn.Name].Value;
                    //var discount = (decimal)row.Cells[dscDataGridViewTextBoxColumn.Name].Value;
                    //var val = (quantity*price*(100 - discount)/100);
                    //row.Cells[tTDataGridViewTextBoxColumn.Name].Value = val.ToString("N");

                    if (!HgConvert.ToBoolean(row.Cells[ColumnCheck.Name].Value)) continue;
                    products.Add(Products.Single(s => s.RpId == (string) row.Cells[rpIdDataGridViewTextBoxColumn.Name].Value));

                }
                return products;
            }
        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            var products = new List<TotalModel>();
            foreach (DataGridViewRow row in DgvDataGridView.Rows)
            {
                if (!HgConvert.ToBoolean(row.Cells[ColumnCheck.Name].Value)) continue;
                products.Add(Products.Single(s => s.RpId == (string) row.Cells[rpIdDataGridViewTextBoxColumn.Name].Value));
            }
            Products = products;
            DialogResult = Products != null && Products.Count > 0 ? DialogResult.OK : DialogResult.Abort;
            Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DgvDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TxtTotal.Text = SelectedProducts.Sum(i => EcrConvert.ConvertToDouble(i.Qty) * EcrConvert.ConvertToDouble(i.P) * (1 - (i.DscT == "1" ? EcrConvert.ConvertToDouble(i.Dsc) / 100 : 0))).ToString("N");
        }

    }
}

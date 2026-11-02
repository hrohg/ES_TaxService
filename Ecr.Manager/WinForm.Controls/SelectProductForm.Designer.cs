namespace Ecr.Manager.WinForm.Controls
{
    partial class SelectProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.DgvDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtTotal = new System.Windows.Forms.TextBox();
            this.totalModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ColumnCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rpIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.muDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dscDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.didDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adgDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adscDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dscTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(520, 356);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "Թողնել";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(439, 356);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "Հաստատել";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // DgvDataGridView
            // 
            this.DgvDataGridView.AllowUserToAddRows = false;
            this.DgvDataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.DgvDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvDataGridView.AutoGenerateColumns = false;
            this.DgvDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.DgvDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.DgvDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCheck,
            this.rpIdDataGridViewTextBoxColumn,
            this.gcDataGridViewTextBoxColumn,
            this.gnDataGridViewTextBoxColumn,
            this.muDataGridViewTextBoxColumn,
            this.qtyDataGridViewTextBoxColumn,
            this.pDataGridViewTextBoxColumn,
            this.dscDataGridViewTextBoxColumn,
            this.tTDataGridViewTextBoxColumn,
            this.didDataGridViewTextBoxColumn,
            this.dtDataGridViewTextBoxColumn,
            this.dtmDataGridViewTextBoxColumn,
            this.tDataGridViewTextBoxColumn,
            this.adgDataGridViewTextBoxColumn,
            this.adscDataGridViewTextBoxColumn,
            this.dscTDataGridViewTextBoxColumn});
            this.DgvDataGridView.DataSource = this.totalModelBindingSource;
            this.DgvDataGridView.Location = new System.Drawing.Point(12, 12);
            this.DgvDataGridView.Name = "DgvDataGridView";
            this.DgvDataGridView.Size = new System.Drawing.Size(583, 312);
            this.DgvDataGridView.TabIndex = 1;
            this.DgvDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDataGridView_CellValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Վերադարձվող գումար";
            // 
            // TxtTotal
            // 
            this.TxtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTotal.Location = new System.Drawing.Point(495, 330);
            this.TxtTotal.Name = "TxtTotal";
            this.TxtTotal.Size = new System.Drawing.Size(100, 20);
            this.TxtTotal.TabIndex = 3;
            this.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // totalModelBindingSource
            // 
            this.totalModelBindingSource.DataSource = typeof(CashReg.Models.TotalModel);
            // 
            // ColumnCheck
            // 
            this.ColumnCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColumnCheck.FalseValue = "False";
            this.ColumnCheck.HeaderText = "Ընտրել";
            this.ColumnCheck.IndeterminateValue = "False";
            this.ColumnCheck.Name = "ColumnCheck";
            this.ColumnCheck.TrueValue = "True";
            this.ColumnCheck.Width = 51;
            // 
            // rpIdDataGridViewTextBoxColumn
            // 
            this.rpIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.rpIdDataGridViewTextBoxColumn.DataPropertyName = "RpId";
            this.rpIdDataGridViewTextBoxColumn.HeaderText = "ՀՀ";
            this.rpIdDataGridViewTextBoxColumn.Name = "rpIdDataGridViewTextBoxColumn";
            this.rpIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.rpIdDataGridViewTextBoxColumn.Visible = false;
            this.rpIdDataGridViewTextBoxColumn.Width = 44;
            // 
            // gcDataGridViewTextBoxColumn
            // 
            this.gcDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.gcDataGridViewTextBoxColumn.DataPropertyName = "Gc";
            this.gcDataGridViewTextBoxColumn.HeaderText = "Կոդ";
            this.gcDataGridViewTextBoxColumn.Name = "gcDataGridViewTextBoxColumn";
            this.gcDataGridViewTextBoxColumn.ReadOnly = true;
            this.gcDataGridViewTextBoxColumn.Width = 51;
            // 
            // gnDataGridViewTextBoxColumn
            // 
            this.gnDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gnDataGridViewTextBoxColumn.DataPropertyName = "Gn";
            this.gnDataGridViewTextBoxColumn.HeaderText = "Անվանում";
            this.gnDataGridViewTextBoxColumn.Name = "gnDataGridViewTextBoxColumn";
            this.gnDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // muDataGridViewTextBoxColumn
            // 
            this.muDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.muDataGridViewTextBoxColumn.DataPropertyName = "Mu";
            this.muDataGridViewTextBoxColumn.HeaderText = "Չմ";
            this.muDataGridViewTextBoxColumn.Name = "muDataGridViewTextBoxColumn";
            this.muDataGridViewTextBoxColumn.ReadOnly = true;
            this.muDataGridViewTextBoxColumn.Width = 45;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            this.qtyDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.qtyDataGridViewTextBoxColumn.DataPropertyName = "Qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = "0";
            this.qtyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.qtyDataGridViewTextBoxColumn.HeaderText = "Քանակ";
            this.qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            this.qtyDataGridViewTextBoxColumn.Width = 68;
            // 
            // pDataGridViewTextBoxColumn
            // 
            this.pDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.pDataGridViewTextBoxColumn.DataPropertyName = "P";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = "0";
            this.pDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.pDataGridViewTextBoxColumn.HeaderText = "Գին";
            this.pDataGridViewTextBoxColumn.Name = "pDataGridViewTextBoxColumn";
            this.pDataGridViewTextBoxColumn.ReadOnly = true;
            this.pDataGridViewTextBoxColumn.Width = 52;
            // 
            // dscDataGridViewTextBoxColumn
            // 
            this.dscDataGridViewTextBoxColumn.DataPropertyName = "Dsc";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.dscDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.dscDataGridViewTextBoxColumn.HeaderText = "Զեղչ";
            this.dscDataGridViewTextBoxColumn.MinimumWidth = 40;
            this.dscDataGridViewTextBoxColumn.Name = "dscDataGridViewTextBoxColumn";
            this.dscDataGridViewTextBoxColumn.ReadOnly = true;
            this.dscDataGridViewTextBoxColumn.Width = 40;
            // 
            // tTDataGridViewTextBoxColumn
            // 
            this.tTDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.tTDataGridViewTextBoxColumn.DataPropertyName = "TT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = "0";
            this.tTDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.tTDataGridViewTextBoxColumn.HeaderText = "Գումար";
            this.tTDataGridViewTextBoxColumn.Name = "tTDataGridViewTextBoxColumn";
            this.tTDataGridViewTextBoxColumn.ReadOnly = true;
            this.tTDataGridViewTextBoxColumn.Width = 69;
            // 
            // didDataGridViewTextBoxColumn
            // 
            this.didDataGridViewTextBoxColumn.DataPropertyName = "Did";
            this.didDataGridViewTextBoxColumn.HeaderText = "Did";
            this.didDataGridViewTextBoxColumn.Name = "didDataGridViewTextBoxColumn";
            this.didDataGridViewTextBoxColumn.Visible = false;
            this.didDataGridViewTextBoxColumn.Width = 5;
            // 
            // dtDataGridViewTextBoxColumn
            // 
            this.dtDataGridViewTextBoxColumn.DataPropertyName = "Dt";
            this.dtDataGridViewTextBoxColumn.HeaderText = "Dt";
            this.dtDataGridViewTextBoxColumn.Name = "dtDataGridViewTextBoxColumn";
            this.dtDataGridViewTextBoxColumn.Visible = false;
            this.dtDataGridViewTextBoxColumn.Width = 5;
            // 
            // dtmDataGridViewTextBoxColumn
            // 
            this.dtmDataGridViewTextBoxColumn.DataPropertyName = "Dtm";
            this.dtmDataGridViewTextBoxColumn.HeaderText = "Dtm";
            this.dtmDataGridViewTextBoxColumn.Name = "dtmDataGridViewTextBoxColumn";
            this.dtmDataGridViewTextBoxColumn.Visible = false;
            this.dtmDataGridViewTextBoxColumn.Width = 5;
            // 
            // tDataGridViewTextBoxColumn
            // 
            this.tDataGridViewTextBoxColumn.DataPropertyName = "T";
            this.tDataGridViewTextBoxColumn.HeaderText = "T";
            this.tDataGridViewTextBoxColumn.Name = "tDataGridViewTextBoxColumn";
            this.tDataGridViewTextBoxColumn.Visible = false;
            this.tDataGridViewTextBoxColumn.Width = 5;
            // 
            // adgDataGridViewTextBoxColumn
            // 
            this.adgDataGridViewTextBoxColumn.DataPropertyName = "Adg";
            this.adgDataGridViewTextBoxColumn.HeaderText = "Adg";
            this.adgDataGridViewTextBoxColumn.Name = "adgDataGridViewTextBoxColumn";
            this.adgDataGridViewTextBoxColumn.Visible = false;
            this.adgDataGridViewTextBoxColumn.Width = 5;
            // 
            // adscDataGridViewTextBoxColumn
            // 
            this.adscDataGridViewTextBoxColumn.DataPropertyName = "Adsc";
            this.adscDataGridViewTextBoxColumn.HeaderText = "Adsc";
            this.adscDataGridViewTextBoxColumn.Name = "adscDataGridViewTextBoxColumn";
            this.adscDataGridViewTextBoxColumn.Visible = false;
            this.adscDataGridViewTextBoxColumn.Width = 5;
            // 
            // dscTDataGridViewTextBoxColumn
            // 
            this.dscTDataGridViewTextBoxColumn.DataPropertyName = "DscT";
            this.dscTDataGridViewTextBoxColumn.HeaderText = "DscT";
            this.dscTDataGridViewTextBoxColumn.Name = "dscTDataGridViewTextBoxColumn";
            this.dscTDataGridViewTextBoxColumn.Visible = false;
            this.dscTDataGridViewTextBoxColumn.Width = 5;
            // 
            // SelectProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 391);
            this.Controls.Add(this.TxtTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DgvDataGridView);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnCancel);
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "SelectProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ընտրել համապատասխան ապրանքները";
            ((System.ComponentModel.ISupportInitialize)(this.DgvDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvDataGridView;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.BindingSource totalModelBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtTotal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn rpIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn muDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dscDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn didDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn adgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn adscDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dscTDataGridViewTextBoxColumn;
    }
}
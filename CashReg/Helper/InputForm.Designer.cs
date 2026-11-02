namespace CashReg.Helper
{
    partial class InputForm
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
            this.BtnAccept = new System.Windows.Forms.Button();
            this.TxtReturnTicket = new System.Windows.Forms.TextBox();
            this.txtLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnAccept
            // 
            this.BtnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAccept.Location = new System.Drawing.Point(62, 85);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(75, 23);
            this.BtnAccept.TabIndex = 1;
            this.BtnAccept.Text = "Հաստատել";
            this.BtnAccept.UseVisualStyleBackColor = true;
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // TxtReturnTicket
            // 
            this.TxtReturnTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtReturnTicket.Location = new System.Drawing.Point(12, 46);
            this.TxtReturnTicket.Name = "TxtReturnTicket";
            this.TxtReturnTicket.Size = new System.Drawing.Size(125, 20);
            this.TxtReturnTicket.TabIndex = 0;
            this.TxtReturnTicket.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtReturnTicket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtReturnTicket_KeyDown);
            // 
            // txtLable
            // 
            this.txtLable.AutoSize = true;
            this.txtLable.Location = new System.Drawing.Point(9, 30);
            this.txtLable.Name = "txtLable";
            this.txtLable.Size = new System.Drawing.Size(0, 13);
            this.txtLable.TabIndex = 2;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(149, 120);
            this.Controls.Add(this.txtLable);
            this.Controls.Add(this.TxtReturnTicket);
            this.Controls.Add(this.BtnAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ՀԴ կտրոն";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAccept;
        private System.Windows.Forms.TextBox TxtReturnTicket;
        private System.Windows.Forms.Label txtLable;
    }
}
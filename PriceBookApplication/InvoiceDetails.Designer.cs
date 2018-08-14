namespace PriceBookApplication
{
    partial class InvoiceDetails
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
            this.lblInvoiceAmount = new System.Windows.Forms.Label();
            this.tbxInvoiceAmount = new System.Windows.Forms.TextBox();
            this.lblStore = new System.Windows.Forms.Label();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.cbxStore = new System.Windows.Forms.ComboBox();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInvoiceAmount
            // 
            this.lblInvoiceAmount.AutoSize = true;
            this.lblInvoiceAmount.Location = new System.Drawing.Point(12, 28);
            this.lblInvoiceAmount.Name = "lblInvoiceAmount";
            this.lblInvoiceAmount.Size = new System.Drawing.Size(81, 13);
            this.lblInvoiceAmount.TabIndex = 0;
            this.lblInvoiceAmount.Text = "Invoice Amount";
            // 
            // tbxInvoiceAmount
            // 
            this.tbxInvoiceAmount.Location = new System.Drawing.Point(99, 25);
            this.tbxInvoiceAmount.Name = "tbxInvoiceAmount";
            this.tbxInvoiceAmount.Size = new System.Drawing.Size(175, 20);
            this.tbxInvoiceAmount.TabIndex = 1;
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Location = new System.Drawing.Point(12, 68);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(32, 13);
            this.lblStore.TabIndex = 0;
            this.lblStore.Text = "Store";
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(12, 112);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(68, 13);
            this.lblInvoiceDate.TabIndex = 0;
            this.lblInvoiceDate.Text = "Invoice Date";
            // 
            // cbxStore
            // 
            this.cbxStore.FormattingEnabled = true;
            this.cbxStore.Location = new System.Drawing.Point(99, 65);
            this.cbxStore.Name = "cbxStore";
            this.cbxStore.Size = new System.Drawing.Size(173, 21);
            this.cbxStore.TabIndex = 2;
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(99, 106);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(173, 20);
            this.dtpInvoiceDate.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(99, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 137);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // InvoiceDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 172);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dtpInvoiceDate);
            this.Controls.Add(this.cbxStore);
            this.Controls.Add(this.tbxInvoiceAmount);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.lblStore);
            this.Controls.Add(this.lblInvoiceAmount);
            this.MaximizeBox = false;
            this.Name = "InvoiceDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInvoiceAmount;
        private System.Windows.Forms.TextBox tbxInvoiceAmount;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.ComboBox cbxStore;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
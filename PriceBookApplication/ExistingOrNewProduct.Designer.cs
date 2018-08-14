namespace PriceBookApplication
{
    partial class ExistingOrNewProduct
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
            this.btnNew = new System.Windows.Forms.Button();
            this.btnExisting = new System.Windows.Forms.Button();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(12, 33);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnExisting
            // 
            this.btnExisting.Location = new System.Drawing.Point(93, 33);
            this.btnExisting.Name = "btnExisting";
            this.btnExisting.Size = new System.Drawing.Size(75, 23);
            this.btnExisting.TabIndex = 0;
            this.btnExisting.Text = "Existing";
            this.btnExisting.UseVisualStyleBackColor = true;
            this.btnExisting.Click += new System.EventHandler(this.btnExisting_Click);
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(12, 9);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(176, 13);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "Is this a new or an existing product?";
            // 
            // ExistingOrNewProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 68);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.btnExisting);
            this.Controls.Add(this.btnNew);
            this.Name = "ExistingOrNewProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New or Existing Product";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnExisting;
        private System.Windows.Forms.Label lblQuestion;
    }
}
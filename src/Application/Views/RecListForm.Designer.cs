namespace BlackSugar.Views
{
    partial class RecListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecListForm));
            this.lstDetail = new System.Windows.Forms.ListView();
            this.pnlTitlrBar = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlColorBar = new System.Windows.Forms.Panel();
            this.pnlError = new System.Windows.Forms.Panel();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlBackSmokeBottom = new BlackSugar.Controls.TranslucentPanel();
            this.pnlBackSmokeTop = new BlackSugar.Controls.TranslucentPanel();
            this.txtFilter = new BlackSugar.Controls.PlaceholderTextBox();
            this.pnlTitlrBar.SuspendLayout();
            this.pnlError.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDetail
            // 
            this.lstDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDetail.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstDetail.HideSelection = false;
            this.lstDetail.Location = new System.Drawing.Point(8, 62);
            this.lstDetail.Name = "lstDetail";
            this.lstDetail.Size = new System.Drawing.Size(784, 567);
            this.lstDetail.TabIndex = 0;
            this.lstDetail.UseCompatibleStateImageBehavior = false;
            // 
            // pnlTitlrBar
            // 
            this.pnlTitlrBar.Controls.Add(this.lblTitle2);
            this.pnlTitlrBar.Controls.Add(this.lblTitle1);
            this.pnlTitlrBar.Controls.Add(this.txtFilter);
            this.pnlTitlrBar.Controls.Add(this.btnClose);
            this.pnlTitlrBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitlrBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitlrBar.Name = "pnlTitlrBar";
            this.pnlTitlrBar.Size = new System.Drawing.Size(800, 40);
            this.pnlTitlrBar.TabIndex = 1;
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle2.Font = new System.Drawing.Font("メイリオ", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle2.Location = new System.Drawing.Point(82, 7);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(63, 25);
            this.lblTitle2.TabIndex = 3;
            this.lblTitle2.Text = "●REC";
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("メイリオ", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle1.Location = new System.Drawing.Point(3, 6);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(84, 27);
            this.lblTitle1.TabIndex = 2;
            this.lblTitle1.Text = "Explorer";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(760, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 40);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 11);
            this.pnlHeader.TabIndex = 2;
            // 
            // pnlColorBar
            // 
            this.pnlColorBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColorBar.Location = new System.Drawing.Point(0, 51);
            this.pnlColorBar.Name = "pnlColorBar";
            this.pnlColorBar.Size = new System.Drawing.Size(800, 5);
            this.pnlColorBar.TabIndex = 3;
            // 
            // pnlError
            // 
            this.pnlError.BackColor = System.Drawing.Color.DeepPink;
            this.pnlError.Controls.Add(this.lblErrorMessage);
            this.pnlError.Controls.Add(this.btnOK);
            this.pnlError.Location = new System.Drawing.Point(0, 210);
            this.pnlError.Name = "pnlError";
            this.pnlError.Size = new System.Drawing.Size(800, 190);
            this.pnlError.TabIndex = 4;
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.Font = new System.Drawing.Font("メイリオ", 11F, System.Drawing.FontStyle.Bold);
            this.lblErrorMessage.ForeColor = System.Drawing.Color.White;
            this.lblErrorMessage.Location = new System.Drawing.Point(56, 63);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(503, 23);
            this.lblErrorMessage.TabIndex = 1;
            this.lblErrorMessage.Text = "Error : Not Exists Selected Folder Path. フォルダが存在しません。";
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("メイリオ", 13F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(648, 137);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(125, 35);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "O K";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // pnlBackSmokeBottom
            // 
            this.pnlBackSmokeBottom.BackColor = System.Drawing.Color.Black;
            this.pnlBackSmokeBottom.Location = new System.Drawing.Point(0, 400);
            this.pnlBackSmokeBottom.Name = "pnlBackSmokeBottom";
            this.pnlBackSmokeBottom.Size = new System.Drawing.Size(800, 235);
            this.pnlBackSmokeBottom.TabIndex = 6;
            // 
            // pnlBackSmokeTop
            // 
            this.pnlBackSmokeTop.BackColor = System.Drawing.Color.Black;
            this.pnlBackSmokeTop.Location = new System.Drawing.Point(0, 0);
            this.pnlBackSmokeTop.Name = "pnlBackSmokeTop";
            this.pnlBackSmokeTop.Size = new System.Drawing.Size(800, 210);
            this.pnlBackSmokeTop.TabIndex = 5;
            // 
            // txtFilter
            // 
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFilter.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFilter.Location = new System.Drawing.Point(547, 0);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Placeholder = "Quick Search";
            this.txtFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtFilter.Size = new System.Drawing.Size(210, 20);
            this.txtFilter.TabIndex = 1;
            // 
            // RecListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(800, 635);
            this.Controls.Add(this.pnlBackSmokeBottom);
            this.Controls.Add(this.pnlError);
            this.Controls.Add(this.pnlBackSmokeTop);
            this.Controls.Add(this.pnlColorBar);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlTitlrBar);
            this.Controls.Add(this.lstDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RecListForm";
            this.ShowInTaskbar = false;
            this.Text = "ExplorerREC";
            this.pnlTitlrBar.ResumeLayout(false);
            this.pnlTitlrBar.PerformLayout();
            this.pnlError.ResumeLayout(false);
            this.pnlError.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstDetail;
        private System.Windows.Forms.Panel pnlTitlrBar;
        private System.Windows.Forms.Panel pnlHeader;
        //private System.Windows.Forms.TextBox txtFilter;
        private BlackSugar.Controls.PlaceholderTextBox txtFilter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlColorBar;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Panel pnlError;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Button btnOK;
        //private System.Windows.Forms.Panel pnlBackSmoke;
        private BlackSugar.Controls.TranslucentPanel pnlBackSmokeTop;
        private Controls.TranslucentPanel pnlBackSmokeBottom;
    }
}
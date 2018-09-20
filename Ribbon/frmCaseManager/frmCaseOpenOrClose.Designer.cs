namespace Ischool.Equip_Repair
{
    partial class frmCaseOpenOrClose
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
            this.lbCaseID = new DevComponents.DotNetBar.LabelX();
            this.lbCloseTime = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbxCloser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnCloseCase = new DevComponents.DotNetBar.ButtonX();
            this.btnLeave = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // lbCaseID
            // 
            this.lbCaseID.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbCaseID.BackgroundStyle.Class = "";
            this.lbCaseID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCaseID.Location = new System.Drawing.Point(12, 16);
            this.lbCaseID.Name = "lbCaseID";
            this.lbCaseID.Size = new System.Drawing.Size(255, 23);
            this.lbCaseID.TabIndex = 0;
            this.lbCaseID.Text = "工單編號";
            // 
            // lbCloseTime
            // 
            this.lbCloseTime.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbCloseTime.BackgroundStyle.Class = "";
            this.lbCloseTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCloseTime.Location = new System.Drawing.Point(12, 49);
            this.lbCloseTime.Name = "lbCloseTime";
            this.lbCloseTime.Size = new System.Drawing.Size(255, 23);
            this.lbCloseTime.TabIndex = 1;
            this.lbCloseTime.Text = "結案日期";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 83);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "結案者";
            // 
            // tbxCloser
            // 
            // 
            // 
            // 
            this.tbxCloser.Border.Class = "TextBoxBorder";
            this.tbxCloser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbxCloser.Enabled = false;
            this.tbxCloser.Location = new System.Drawing.Point(93, 82);
            this.tbxCloser.Name = "tbxCloser";
            this.tbxCloser.ReadOnly = true;
            this.tbxCloser.Size = new System.Drawing.Size(187, 25);
            this.tbxCloser.TabIndex = 3;
            // 
            // btnCloseCase
            // 
            this.btnCloseCase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCloseCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseCase.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseCase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCloseCase.Location = new System.Drawing.Point(146, 131);
            this.btnCloseCase.Name = "btnCloseCase";
            this.btnCloseCase.Size = new System.Drawing.Size(75, 23);
            this.btnCloseCase.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCloseCase.TabIndex = 4;
            this.btnCloseCase.Text = "結案";
            this.btnCloseCase.Click += new System.EventHandler(this.btnCloseCase_Click);
            // 
            // btnLeave
            // 
            this.btnLeave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeave.BackColor = System.Drawing.Color.Transparent;
            this.btnLeave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLeave.Location = new System.Drawing.Point(227, 131);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(75, 23);
            this.btnLeave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLeave.TabIndex = 5;
            this.btnLeave.Text = "離開";
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // frmCaseClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 166);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnCloseCase);
            this.Controls.Add(this.tbxCloser);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.lbCloseTime);
            this.Controls.Add(this.lbCaseID);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(330, 205);
            this.MinimumSize = new System.Drawing.Size(330, 205);
            this.Name = "frmCaseClose";
            this.Text = "工單結案";
            this.Load += new System.EventHandler(this.frmCaseClose_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lbCaseID;
        private DevComponents.DotNetBar.LabelX lbCloseTime;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbxCloser;
        private DevComponents.DotNetBar.ButtonX btnCloseCase;
        private DevComponents.DotNetBar.ButtonX btnLeave;
    }
}
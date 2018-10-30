namespace Ischool.Equip_Repair
{
    partial class frmExportPlaceEquip
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("位置名稱(階層1)");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("位置名稱(階層2)");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("位置名稱(階層3)");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("設施名稱");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("設施財產編號");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("設施損壞原因");
            this.wizard1 = new DevComponents.DotNetBar.Wizard();
            this.wizardPage1 = new DevComponents.DotNetBar.WizardPage();
            this.ckbxSelectAll = new System.Windows.Forms.CheckBox();
            this.listViewEx1 = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.wizard1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.BackgroundImage = global::Ischool.Equip_Repair.Properties.Resources.wizard1_BackgroundImage;
            this.wizard1.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
            this.wizard1.CancelButtonText = "關閉";
            this.wizard1.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.FinishButtonTabIndex = 3;
            this.wizard1.FinishButtonText = "匯出";
            // 
            // 
            // 
            this.wizard1.FooterStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.wizard1.FooterStyle.BackColorGradientAngle = 90;
            this.wizard1.FooterStyle.BorderBottomWidth = 1;
            this.wizard1.FooterStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard1.FooterStyle.BorderLeftWidth = 1;
            this.wizard1.FooterStyle.BorderRightWidth = 1;
            this.wizard1.FooterStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Etched;
            this.wizard1.FooterStyle.BorderTopColor = System.Drawing.SystemColors.Control;
            this.wizard1.FooterStyle.BorderTopWidth = 1;
            this.wizard1.FooterStyle.Class = "";
            this.wizard1.FooterStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard1.FooterStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard1.FooterStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
            this.wizard1.HeaderCaptionFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.wizard1.HeaderDescriptionFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.wizard1.HeaderDescriptionIndent = 16;
            this.wizard1.HeaderImage = global::Ischool.Equip_Repair.Properties.Resources.Export_Image;
            // 
            // 
            // 
            this.wizard1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.wizard1.HeaderStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.wizard1.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(157)))), ((int)(((byte)(182)))));
            this.wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.wizard1.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard1.HeaderStyle.BorderLeftWidth = 1;
            this.wizard1.HeaderStyle.BorderRightWidth = 1;
            this.wizard1.HeaderStyle.BorderTopWidth = 1;
            this.wizard1.HeaderStyle.Class = "";
            this.wizard1.HeaderStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard1.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard1.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.Size = new System.Drawing.Size(504, 426);
            this.wizard1.TabIndex = 0;
            this.wizard1.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wizardPage1});
            // 
            // wizardPage1
            // 
            this.wizardPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage1.AntiAlias = false;
            this.wizardPage1.BackButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wizardPage1.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage1.Controls.Add(this.ckbxSelectAll);
            this.wizardPage1.Controls.Add(this.listViewEx1);
            this.wizardPage1.HelpButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wizardPage1.Location = new System.Drawing.Point(7, 72);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wizardPage1.PageDescription = "選擇匯出欄位";
            this.wizardPage1.PageTitle = "匯出位置與設施資料";
            this.wizardPage1.Size = new System.Drawing.Size(490, 296);
            // 
            // 
            // 
            this.wizardPage1.Style.Class = "";
            this.wizardPage1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.wizardPage1.StyleMouseDown.Class = "";
            this.wizardPage1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.wizardPage1.StyleMouseOver.Class = "";
            this.wizardPage1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizardPage1.TabIndex = 7;
            this.wizardPage1.FinishButtonClick += new System.ComponentModel.CancelEventHandler(this.wizardPage1_FinishButtonClick);
            this.wizardPage1.CancelButtonClick += new System.ComponentModel.CancelEventHandler(this.wizardPage1_CancelButtonClick);
            // 
            // ckbxSelectAll
            // 
            this.ckbxSelectAll.AutoSize = true;
            this.ckbxSelectAll.Checked = true;
            this.ckbxSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbxSelectAll.Location = new System.Drawing.Point(12, 11);
            this.ckbxSelectAll.Name = "ckbxSelectAll";
            this.ckbxSelectAll.Size = new System.Drawing.Size(79, 21);
            this.ckbxSelectAll.TabIndex = 1;
            this.ckbxSelectAll.Text = "選擇全部";
            this.ckbxSelectAll.UseVisualStyleBackColor = true;
            this.ckbxSelectAll.CheckedChanged += new System.EventHandler(this.ckbxSelectAll_CheckedChanged);
            // 
            // listViewEx1
            // 
            this.listViewEx1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listViewEx1.Border.Class = "ListViewBorder";
            this.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listViewEx1.CheckBoxes = true;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            listViewItem3.Checked = true;
            listViewItem3.StateImageIndex = 1;
            listViewItem4.Checked = true;
            listViewItem4.StateImageIndex = 1;
            listViewItem5.Checked = true;
            listViewItem5.StateImageIndex = 1;
            listViewItem6.Checked = true;
            listViewItem6.StateImageIndex = 1;
            this.listViewEx1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.listViewEx1.Location = new System.Drawing.Point(12, 38);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(460, 248);
            this.listViewEx1.TabIndex = 0;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.List;
            // 
            // frmExportPlaceEquip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 426);
            this.Controls.Add(this.wizard1);
            this.DoubleBuffered = true;
            this.Name = "frmExportPlaceEquip";
            this.Text = "匯出位置與設施資料";
            this.wizard1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard wizard1;
        private DevComponents.DotNetBar.WizardPage wizardPage1;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewEx1;
        private System.Windows.Forms.CheckBox ckbxSelectAll;
    }
}
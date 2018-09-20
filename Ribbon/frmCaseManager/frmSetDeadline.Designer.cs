namespace Ischool.Equip_Repair
{
    partial class frmSetDeadline
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbxCaseID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dtDeadline = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnLeave = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dtDeadline)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(36, 32);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "工單編號";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(36, 79);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "完工期限";
            // 
            // tbxCaseID
            // 
            // 
            // 
            // 
            this.tbxCaseID.Border.Class = "TextBoxBorder";
            this.tbxCaseID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbxCaseID.Enabled = false;
            this.tbxCaseID.Location = new System.Drawing.Point(117, 31);
            this.tbxCaseID.Name = "tbxCaseID";
            this.tbxCaseID.ReadOnly = true;
            this.tbxCaseID.Size = new System.Drawing.Size(140, 25);
            this.tbxCaseID.TabIndex = 2;
            // 
            // dtDeadline
            // 
            this.dtDeadline.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtDeadline.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtDeadline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDeadline.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtDeadline.ButtonDropDown.Visible = true;
            this.dtDeadline.IsPopupCalendarOpen = false;
            this.dtDeadline.Location = new System.Drawing.Point(117, 78);
            // 
            // 
            // 
            this.dtDeadline.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtDeadline.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtDeadline.MonthCalendar.BackgroundStyle.Class = "";
            this.dtDeadline.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDeadline.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtDeadline.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDeadline.MonthCalendar.DisplayMonth = new System.DateTime(2018, 9, 1, 0, 0, 0, 0);
            this.dtDeadline.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtDeadline.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtDeadline.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtDeadline.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDeadline.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtDeadline.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtDeadline.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDeadline.MonthCalendar.TodayButtonVisible = true;
            this.dtDeadline.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtDeadline.Name = "dtDeadline";
            this.dtDeadline.Size = new System.Drawing.Size(140, 25);
            this.dtDeadline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtDeadline.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(131, 131);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLeave
            // 
            this.btnLeave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeave.BackColor = System.Drawing.Color.Transparent;
            this.btnLeave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLeave.Location = new System.Drawing.Point(212, 131);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(75, 23);
            this.btnLeave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLeave.TabIndex = 5;
            this.btnLeave.Text = "離開";
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // frmSetDeadline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 166);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtDeadline);
            this.Controls.Add(this.tbxCaseID);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(315, 205);
            this.MinimumSize = new System.Drawing.Size(315, 205);
            this.Name = "frmSetDeadline";
            this.Text = "設定完工期限";
            this.Load += new System.EventHandler(this.frmSetDeadline_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtDeadline)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbxCaseID;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtDeadline;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnLeave;
    }
}
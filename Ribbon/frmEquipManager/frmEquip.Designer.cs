namespace Ischool.Equip_Repair
{
    partial class frmEquip
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbxPlaceName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbxEquipName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbxPropertyNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnLeave = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.labelX1.Location = new System.Drawing.Point(27, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "位置名稱";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(27, 64);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "設施名稱";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(27, 105);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "財產編號";
            // 
            // tbxPlaceName
            // 
            // 
            // 
            // 
            this.tbxPlaceName.Border.Class = "TextBoxBorder";
            this.tbxPlaceName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbxPlaceName.Enabled = false;
            this.tbxPlaceName.Location = new System.Drawing.Point(108, 22);
            this.tbxPlaceName.Name = "tbxPlaceName";
            this.tbxPlaceName.ReadOnly = true;
            this.tbxPlaceName.Size = new System.Drawing.Size(150, 25);
            this.tbxPlaceName.TabIndex = 3;
            // 
            // tbxEquipName
            // 
            // 
            // 
            // 
            this.tbxEquipName.Border.Class = "TextBoxBorder";
            this.tbxEquipName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbxEquipName.Location = new System.Drawing.Point(108, 63);
            this.tbxEquipName.Name = "tbxEquipName";
            this.tbxEquipName.Size = new System.Drawing.Size(150, 25);
            this.tbxEquipName.TabIndex = 4;
            this.tbxEquipName.TextChanged += new System.EventHandler(this.tbxEquipName_TextChanged);
            // 
            // tbxPropertyNo
            // 
            // 
            // 
            // 
            this.tbxPropertyNo.Border.Class = "TextBoxBorder";
            this.tbxPropertyNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbxPropertyNo.Location = new System.Drawing.Point(108, 104);
            this.tbxPropertyNo.Name = "tbxPropertyNo";
            this.tbxPropertyNo.Size = new System.Drawing.Size(150, 25);
            this.tbxPropertyNo.TabIndex = 5;
            this.tbxPropertyNo.TextChanged += new System.EventHandler(this.tbxPropertyNo_TextChanged);
            // 
            // btnLeave
            // 
            this.btnLeave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLeave.BackColor = System.Drawing.Color.Transparent;
            this.btnLeave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLeave.Location = new System.Drawing.Point(208, 157);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(75, 23);
            this.btnLeave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLeave.TabIndex = 6;
            this.btnLeave.Text = "離開";
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(127, 157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmEquip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 191);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.tbxPropertyNo);
            this.Controls.Add(this.tbxEquipName);
            this.Controls.Add(this.tbxPlaceName);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(310, 230);
            this.MinimumSize = new System.Drawing.Size(310, 230);
            this.Name = "frmEquip";
            this.Text = "frmEquip";
            this.Load += new System.EventHandler(this.frmEquip_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbxPlaceName;
        private DevComponents.DotNetBar.Controls.TextBoxX tbxEquipName;
        private DevComponents.DotNetBar.Controls.TextBoxX tbxPropertyNo;
        private DevComponents.DotNetBar.ButtonX btnLeave;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
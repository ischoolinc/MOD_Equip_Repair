using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace Ischool.Equip_Repair
{
    public partial class frmBrokenReason : BaseForm
    {
        private FormMode _mode;
        private string _equipID;
        private string _equipName;
        private string _reasonID;
        private string _brokenReason;

        public frmBrokenReason(FormMode mode,string equipID,string equipName,string reasonID ,string brokenReason)
        {
            InitializeComponent();

            this._mode = mode;
            this._equipID = equipID;
            this._equipName = equipName;
            this._reasonID = reasonID;
            this._brokenReason = brokenReason;
        }

        private void frmBrokenReason_Load(object sender, EventArgs e)
        {
            // 取得此設施的損壞原因
            //DataTable dt = DAO.BrokenReason.GetReasonByEquipID(this._equipID);

            if (this._mode == FormMode.Add)
            {
                tbxEquipName.Text = this._equipName;
            }
            if (this._mode == FormMode.Update)
            {
                tbxEquipName.Text = this._equipName;
                tbxReason.Text = this._brokenReason;
            }
        }

        private bool tbxReason_Validate()
        {
            if (string.IsNullOrEmpty(tbxReason.Text.Trim()))
            {
                errorProvider1.SetError(tbxReason,"損壞原因不可空白!");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbxReason, null);
                return true;
            }
        }

        private void tbxReason_TextChanged(object sender, EventArgs e)
        {
            tbxReason_Validate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxReason_Validate())
            {
                if (this._mode == FormMode.Add)
                {
                    try
                    {
                        DAO.BrokenReason.InsertBrokenReason(this._equipID, tbxReason.Text.Trim());
                        MsgBox.Show("資料新增成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
                if (this._mode == FormMode.Update)
                {
                    try
                    {
                        DAO.BrokenReason.UpdateBrokenReason(this._reasonID, tbxReason.Text.Trim());
                        MsgBox.Show("資料更新成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

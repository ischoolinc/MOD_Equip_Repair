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
    public partial class frmSetRepairStatus : BaseForm
    {
        private DataRow _row;

        public frmSetRepairStatus(DataRow row)
        {
            InitializeComponent();

            this._row = row;
        }

        private void frmSetRepairStatus_Load(object sender, EventArgs e)
        {
            groupPanel1.Text = "工單編號: " + this._row["uid"];
            tbxPlace.Text = "" + this._row["place_name"];
            tbxEquipName.Text = "" + this._row["equip_name"];
            tbxReason.Text = "" + this._row["apply_reason"];
            tbxReporter.Text = DAO.Actor.Instance.GetUserAccount();

            cbxReportStatus.Text = "" + this._row["fix_status"];
            cbxReportStatus.Items.Add("未處理");
            cbxReportStatus.Items.Add("已維修");
            cbxReportStatus.Items.Add("待料中");
            cbxReportStatus.Items.Add("待廠商維修中");
            cbxReportStatus.Items.Add("校內自行處理");
            
        }

        private bool cbxReportStatus_Validate()
        {
            if (string.IsNullOrEmpty(cbxReportStatus.Text.Trim()))
            {
                errorProvider1.SetError(cbxReportStatus,"維修進度不可空白!");
                return false;
            }
            else
            {
                errorProvider1.SetError(cbxReportStatus, null);
                return true;
            }
        }

        private void cbxReportStatus_TextChanged(object sender, EventArgs e)
        {
            cbxReportStatus_Validate();
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            if (cbxReportStatus_Validate())
            {
                try
                {
                    string caseID = "" + this._row["uid"];
                    string fixStatus = cbxReportStatus.Text.Trim();
                    DAO.Case.UpdateFixStatus(caseID, fixStatus);
                    MsgBox.Show("維修進度更新成功!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}

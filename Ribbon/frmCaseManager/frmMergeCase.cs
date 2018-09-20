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
    public partial class frmMergeCase : BaseForm
    {
        private DataRow _row;
        private List<string> _listCaseID = new List<string>();

        public frmMergeCase(DataRow row)
        {
            InitializeComponent();

            this._row = row;
        }

        private void frmMergeCase_Load(object sender, EventArgs e)
        {
            groupPanel1.Text = "工單編號: " + this._row["uid"];
            lbApplyDate.Text = DateTime.Parse("" + this._row["apply_date"]).ToString("yyyy年MM月dd日");
            tbxPlace.Text = "" + this._row["place_name"];
            tbxEquip.Text = "" + this._row["equip_name"];
            tbxReason.Text = "" + this._row["apply_reason"];

            // 取得ref_case_id IS NULL 的案件資料
            DataTable dt = DAO.Case.GetCanMergeCaseData();
            foreach (DataRow row in dt.Rows)
            {
                this._listCaseID.Add("" + row["uid"]);   
            }
        }

        private bool tbxCaseID_Validate()
        {
            if (string.IsNullOrEmpty(tbxCaseID.Text.Trim()))
            {
                errorProvider1.SetError(tbxCaseID,"工單編號不可空白!");
                return false;
            }
            else
            {
                if (this._listCaseID.Contains(tbxCaseID.Text.Trim()))
                {
                    if (tbxCaseID.Text.Trim() == "" + this._row["uid"]) // 要合併到的案件 不包含案件自己本身
                    {
                        errorProvider1.SetError(tbxCaseID, "無法合併工單至其自身!");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(tbxCaseID, null);
                        return true;
                    }
                }
                else
                {
                    errorProvider1.SetError(tbxCaseID, "工單編號不存在!");
                    return false;
                }
            }
        }

        private void tbxCaseID_TextChanged(object sender, EventArgs e)
        {
            tbxCaseID_Validate();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (tbxCaseID_Validate())
            {
                try
                {
                    string caseID = "" + this._row["uid"];
                    string refCaseID = tbxCaseID.Text.Trim();
                    DAO.Case.UpdateRefCaseID(caseID, refCaseID);
                    MsgBox.Show("工單合併成功!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void btnleave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

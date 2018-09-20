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
    public partial class frmCaseDetail : BaseForm
    {
        private DataRow _row;
        private string _workers;
        private string _cases;

        public frmCaseDetail(DataRow row,string workers,string cases)
        {
            InitializeComponent();

            this.AutoScroll = true;

            this._row = row;
            this._workers = workers;
            this._cases = cases;
        }

        private void frmDetailCase_Load(object sender, EventArgs e)
        {
            #region 工單基本資料
            {
                groupPanel1.Text = "工單編號: " + this._row["uid"];
                lbApplyTime.Text = "申請時間: " + DateTime.Parse("" + this._row["apply_date"]).ToString("yyyy年MM月dd日");
                tbxApplicant.Text = "" + this._row["applicant_name"];
                tbxApplyAccount.Text = "" + this._row["applicant_account"];
                tbxPlace.Text = "" + this._row["place_name"];
                tbxEquip.Text = "" + this._row["equip_name"];
                tbxApplyReason.Text = "" + this._row["apply_reason"];
                pictureBox1.ImageLocation = "" + this._row["picture1"];
                tbxPic1Detail.Text = "" + this._row["pic1_comment"];
                pictureBox2.ImageLocation = "" + this._row["picture2"];
                tbxPic2Detail.Text = "" + this._row["pic2_comment"];
            }
            #endregion

            #region 管理員設定
            {
                string time = ("" + this._row["deadline"]) == "" ? "" : DateTime.Parse("" + this._row["deadline"]).ToString("yyyy年MM月dd日");
                lbDeadline.Text = "完工期限: " + time;
                tbxWorkers.Text = this._workers;
                tbxCases.Text = this._cases;
                tbxRemark.Text = "" + this._row["remark"];
            }
            #endregion

            #region 維修進度
            {
                string time = ("" + this._row["repair_time"]) == "" ? "" : DateTime.Parse("" + this._row["repair_time"]).ToString("yyyy年MM月dd日");
                lbReportTime.Text = "回報時間: " + time;
                tbxRepoter.Text = "" + this._row["repair_account"];
                tbxFixStatus.Text = "" + this._row["fix_status"];
            }
            #endregion

            #region 結案
            {
                string time = ("" + this._row["close_time"]) == "" ? "" : DateTime.Parse("" + this._row["close_time"]).ToString("yyyy年MM月dd日");
                lbCloseTime.Text = "結案時間: " + time;
                tbxCloser.Text = "" + this._row["close_by"];
            }
            #endregion
        }

        private void btnCloase_Click(object sender, EventArgs e)
        {
            string caseID = "" + this._row["uid"];
            DialogResult result = MsgBox.Show(string.Format("確定將工單編號:{0} 進行結案?",caseID),"提醒",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DAO.Case.UpdateCaseIsClose(caseID, "true", DateTime.Now.ToString("yyyy/MM/dd"), DAO.Actor.Instance.GetUserAccount());
                    MsgBox.Show("工單結案成功!");
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
}

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
    public partial class frmCaseOpenOrClose : BaseForm
    {
        private string _caseID;
        private CaseStatus _mode;

        public frmCaseOpenOrClose(CaseStatus mode,string caseID)
        {
            InitializeComponent();

            this._caseID = caseID;
            this._mode = mode;
        }

        private void frmCaseClose_Load(object sender, EventArgs e)
        {
            lbCaseID.Text = "工單編號: " + this._caseID;
            lbCloseTime.Text = "結案日期: " + DateTime.Now.ToString("yyyy年MM月dd日");
            tbxCloser.Text = DAO.Actor.Instance.GetUserAccount();

            if (this._mode == CaseStatus.IsClose)
            {
                btnCloseCase.Text = "結案";
            }
            else
            {
                btnCloseCase.Text = "開啟";
            }
        }

        private void btnCloseCase_Click(object sender, EventArgs e)
        {
            if (this._mode == CaseStatus.IsClose)
            {
                DialogResult result = MsgBox.Show("確定結案此工單?", "提醒", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DAO.Case.UpdateCaseIsClose(this._caseID, "true", DateTime.Now.ToString("yyyy/MM/dd"), tbxCloser.Text);
                        MsgBox.Show("工單結案成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                DialogResult result = MsgBox.Show("確定開啟此工單? \n結案時間與結案者資料將會被清除。", "提醒", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DAO.Case.UpdateCaseIsClose(this._caseID, "false", null, null);
                        MsgBox.Show("工單開啟成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch (Exception ex)
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

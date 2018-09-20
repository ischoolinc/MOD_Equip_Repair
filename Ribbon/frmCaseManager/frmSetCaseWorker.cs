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
    public partial class frmSetCaseWorker : BaseForm
    {
        private string _caseID;

        public frmSetCaseWorker(string caseID)
        {
            InitializeComponent();

            this._caseID = caseID;
        }

        private void frmSetCaseWorker_Load(object sender, EventArgs e)
        {
            tbxCaseID.Text = this._caseID;

            this.SuspendLayout();
            {
                // 取得案件維修人員資料
                DataTable dt = DAO.Worker.GetCaseWorker(this._caseID);
                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgvrow = new DataGridViewRow();
                    dgvrow.CreateCells(dataGridViewX1);

                    int col = 0;
                    dgvrow.Cells[col++].Value = bool.Parse("" + row["指定"]);
                    dgvrow.Cells[col++].Value = "" + row["teacher_name"];
                    dgvrow.Cells[col++].Value = "" + row["account"];
                    dgvrow.Tag = "" + row["uid"]; // 維修員系統編號

                    dataGridViewX1.Rows.Add(dgvrow);
                }
            }
            this.ResumeLayout();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> listWorkerID = new List<string>();
            foreach (DataGridViewRow dgvrow in dataGridViewX1.Rows)
            {
                if (bool.Parse("" + dgvrow.Cells[0].Value))
                {
                    listWorkerID.Add("" + dgvrow.Tag);
                }
            }

            try
            {
                if (listWorkerID.Count > 0)
                {
                    DAO.Case.UpdateCaseWorkers(this._caseID, listWorkerID);
                    MsgBox.Show("資料更新成功!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

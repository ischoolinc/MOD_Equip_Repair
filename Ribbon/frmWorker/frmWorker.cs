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
    public partial class frmWorker : BaseForm
    {
        public frmWorker()
        {
            InitializeComponent();
        }

        private void frmWorker_Load(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        private void ReloadDataGridView()
        {
            this.SuspendLayout();

            dataGridViewX1.Rows.Clear();
            DataTable dt = DAO.Worker.GetWorkerData();
            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow dgvrow = new DataGridViewRow();
                dgvrow.CreateCells(dataGridViewX1);

                int col = 0;
                dgvrow.Cells[col++].Value = "" + row["teacher_name"];
                dgvrow.Cells[col++].Value = "" + row["account"];
                dgvrow.Cells[col++].Value = "刪除";
                dgvrow.Tag = "" + row["uid"]; // 維修員系統編號

                dataGridViewX1.Rows.Add(dgvrow);
            }

            this.ResumeLayout();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 2)
                {
                    string teacherName = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                    string workerID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;

                    DialogResult result = MsgBox.Show(string.Format("確定刪除{0}教師維修員身分?",teacherName),"提醒",MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            DAO.Worker.DeleteWorkerByID(workerID, Program._workerRoleID);
                            MsgBox.Show("資料刪除成功!");
                            ReloadDataGridView();
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddWorker form = new frmAddWorker();
            form.FormClosed += delegate
            {
                if (form.DialogResult == DialogResult.Yes)
                {
                    ReloadDataGridView();
                }
            };
            form.ShowDialog();
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

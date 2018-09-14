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
    public partial class frmAddAdmin : BaseForm
    {
        public frmAddAdmin()
        {
            InitializeComponent();
        }

        private void frmAddAdmin_Load(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        private void ReloadDataGridView()
        {
            this.SuspendLayout();

            // 取得老師資料
            DataTable dt = DAO.Admin.GetTeacherData();
            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow dgvrow = new DataGridViewRow();
                dgvrow.CreateCells(dataGridViewX1);

                int col = 0;
                dgvrow.Cells[col++].Value = "" + row["teacher_name"];
                dgvrow.Cells[col++].Value = "" + row["nickname"];
                dgvrow.Cells[col++].Value = ParseGender("" + row["gender"]);
                dgvrow.Cells[col++].Value = "" + row["st_login_name"];
                dgvrow.Cells[col++].Value = "" + row["dept"];
                dgvrow.Cells[col++].Value = "指定";
                dgvrow.Tag = "" + row["id"];
                
                dataGridViewX1.Rows.Add(dgvrow);
            }

            this.ResumeLayout();
        }

        private string ParseGender(string gender)
        {
            switch (gender)
            {
                case "0":
                    return "女";
                case "1":
                    return "男";
                default:
                    return null;
            }
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxSearch.Text))
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        row.Visible = (row.Cells[0].Value.ToString().IndexOf(tbxSearch.Text) > -1);
                    }
                }
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 5)
                {
                    string teacherID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;
                    string teacherName = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                    string account = "" + dataGridViewX1.Rows[e.RowIndex].Cells[3].Value;
                    string loginID = DAO.Actor.Instance.GetLoginIDByAccount(account);
                    string roleID = Program._adminRoleID;
                    string userAccount = DAO.Actor.Instance.GetUserAccount();

                    if (string.IsNullOrEmpty(account))
                    {
                        MsgBox.Show(string.Format("{0}教師沒有登入帳號，無法指定為設施報修管理員!", teacherName));
                        return;
                    }
                    else
                    {
                        DialogResult result = MsgBox.Show(string.Format("確定將{0}教師指定為設施報修管理員?", teacherName), "提醒",MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                DAO.Admin.InsertAdmin(teacherID, account, loginID, roleID,DateTime.Now.ToString("yyyy/MM/dd"), userAccount);
                                MsgBox.Show("資料新增成功!");
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
        }

    }
}

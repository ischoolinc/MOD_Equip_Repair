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
    public partial class frmCancelMergeCase : BaseForm
    {
        private string _caseID;
        private List<DataRow> _listData = new List<DataRow>();

        public frmCancelMergeCase(string caseID,List<DataRow>listData)
        {
            InitializeComponent();

            this._listData = listData;
            this._caseID = caseID;
        }

        private void frmCancelMergeCase_Load(object sender, EventArgs e)
        {
            tbxCaseID.Text = this._caseID;
            ReloadDataGridView();
        }

        private void ReloadDataGridView()
        {
            this.SuspendLayout();
            {
                foreach (DataRow row in this._listData)
                {
                    DataGridViewRow dgvrow = new DataGridViewRow();
                    dgvrow.CreateCells(dataGridViewX1);

                    int col = 0;
                    dgvrow.Cells[col++].Value = "" + row["uid"];
                    dgvrow.Cells[col++].Value = "" + row["equip_name"];
                    dgvrow.Cells[col++].Value = "" + row["apply_reason"];
                    dgvrow.Cells[col++].Value = "移除";
                    dgvrow.Tag = row;

                    dataGridViewX1.Rows.Add(dgvrow);
                }
            }
            this.ResumeLayout();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 3)
            {
                string caseID = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;

                try
                {
                    DAO.Case.UpdateRefCaseID(caseID, null);
                    MsgBox.Show("工單移除成功!");
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

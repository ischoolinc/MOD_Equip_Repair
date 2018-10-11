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
    public partial class frmCaseManager : BaseForm
    {
        public frmCaseManager()
        {
            InitializeComponent();
        }

        private void frmCaseManager_Load(object sender, EventArgs e)
        {

            #region Init dateTime
            {
                dtStart.Value = DateTime.Now.AddDays(-7);
                dtEnd.Value = DateTime.Now;
            }
            #endregion

            #region Init cbxStatus
            {
                Status ob1 = new Status();
                ob1.Mode = CaseStatus.IsClose;
                ob1.Name = EnumDescription.Get(typeof(CaseStatus), CaseStatus.IsClose.ToString());
                ob1.IsClose = true;
                cbxStatus.Items.Add(ob1);

                Status ob2 = new Status();
                ob2.Mode = CaseStatus.UnClose;
                ob2.Name = EnumDescription.Get(typeof(CaseStatus), CaseStatus.UnClose.ToString());
                ob2.IsClose = false;
                cbxStatus.Items.Add(ob2);

                cbxStatus.SelectedIndex = 1;
                cbxStatus.DisplayMember = "Name";
                cbxStatus.ValueMember = "IsClose";
            }
            #endregion

            #region Init cbxProgress
            {
                cbxProgress.Items.Add("--全部--");
                cbxProgress.Items.Add("未處理");
                cbxProgress.Items.Add("已維修");
                cbxProgress.Items.Add("代料中");
                cbxProgress.Items.Add("待廠商維修中");
                cbxProgress.Items.Add("校內自行處理");
                

                cbxProgress.SelectedIndex = 0;
            }
            #endregion

            ReloadDataGridView();

            if (!DAO.Actor.Instance.IsAdmin())
            {
                btnSetCase.Enabled = false;
                btnMerge.Enabled = false;
                btnDoCase.Enabled = false;
            }
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            dtStart.Enabled = checkBoxX1.Checked;
            dtEnd.Enabled = checkBoxX1.Checked;
        }

        private void cbxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.Clear();

            if (((Status)cbxStatus.SelectedItem).IsClose)
            {
                btnSetCase.Enabled = false;
                btnMerge.Enabled = false;
                btnCaseClose.Enabled = false;
                btnCaseOpen.Enabled = true;
                btnRepairStatus.Enabled = false;
            }
            else
            {
                btnSetCase.Enabled = true;
                btnMerge.Enabled = true;
                btnCaseClose.Enabled = true;
                btnCaseOpen.Enabled = false;
                btnRepairStatus.Enabled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        private void ReloadDataGridView()
        {
            this.SuspendLayout();
            {
                dataGridViewX1.Rows.Clear();
                Dictionary<string, List<string>> dicWorkerNameByCaseID = new Dictionary<string, List<string>>();
                Dictionary<string, List<string>> dicMergeCaseIDByCaseID = new Dictionary<string, List<string>>();
                Dictionary<string, List<DataRow>> dicMergeCaseDataByCaseID = new Dictionary<string, List<DataRow>>();
                DataTable dt;
                if (checkBoxX1.Checked)
                {
                    dt = DAO.Case.GetCaseDataByCondition(dtStart.Value.ToString("yyyy/MM/dd"), dtEnd.Value.ToString("yyyy/MM/dd"), ((Status)cbxStatus.SelectedItem).IsClose,cbxProgress.SelectedItem.ToString());
                }
                else
                {
                    dt = DAO.Case.GetCaseDataByCondition(null, null, ((Status)cbxStatus.SelectedItem).IsClose, cbxProgress.SelectedItem.ToString());
                }

                List<string> listCaseIDs = new List<string>();
                #region 取得target CaseIDs
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        listCaseIDs.Add("" + row["uid"]);
                    }
                }
                #endregion

                if (listCaseIDs.Count > 0)
                {
                    DataTable dtWorker = DAO.Case.GetCaseWorkerByCaseID(listCaseIDs);
                    DataTable dtMergeCase = DAO.Case.GetMergeCaseByCaseID(listCaseIDs);

                    #region 資料整理
                    {
                        foreach (DataRow row in dtWorker.Rows)
                        {
                            string caseID = "" + row["ref_case_id"];

                            if (!dicWorkerNameByCaseID.ContainsKey(caseID))
                            {
                                dicWorkerNameByCaseID.Add(caseID, new List<string>());
                            }
                            dicWorkerNameByCaseID[caseID].Add("" + row["teacher_name"]);
                        }
                        foreach (DataRow row in dtMergeCase.Rows)
                        {
                            string caseID = "" + row["ref_case_id"];

                            if (!dicMergeCaseDataByCaseID.ContainsKey(caseID))
                            {
                                dicMergeCaseDataByCaseID.Add(caseID,new List<DataRow>());
                            }
                            dicMergeCaseDataByCaseID[caseID].Add(row);

                            if (!dicMergeCaseIDByCaseID.ContainsKey(caseID))
                            {
                                dicMergeCaseIDByCaseID.Add(caseID, new List<string>());
                            }
                            dicMergeCaseIDByCaseID[caseID].Add("" + row["uid"]);
                        }
                    }
                    #endregion
                }

                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgvrow = new DataGridViewRow();
                    dgvrow.CreateCells(dataGridViewX1);

                    string caseID = "" + row["uid"];
                    string workerNames = "";
                    string mergeCaseIDs = "";
                    List<DataRow> listMergeData = new List<DataRow>();
                    #region 取得案件維修員資料
                    {
                        if (dicWorkerNameByCaseID.ContainsKey(caseID))
                        {
                            workerNames = string.Join(" ,", dicWorkerNameByCaseID[caseID]);
                        }
                    }
                    #endregion

                    #region 取得案件合併案件資料
                    {
                        if (dicMergeCaseIDByCaseID.ContainsKey(caseID))
                        {
                            if (dicMergeCaseIDByCaseID.ContainsKey(caseID))
                            {
                                mergeCaseIDs = string.Join(" ,", dicMergeCaseIDByCaseID[caseID]);
                                listMergeData = dicMergeCaseDataByCaseID[caseID];
                            }
                        }
                    }
                    #endregion

                    int col = 0;
                    dgvrow.Cells[col++].Value = "" + row["uid"];
                    dgvrow.Cells[col++].Value = "" + row["is_close"] == "true" ? "結案" : "未結案";
                    dgvrow.Cells[col++].Value = "" + row["fix_status"];
                    dgvrow.Cells[col++].Value = workerNames;
                    dgvrow.Cells[col].Tag = listMergeData;
                    dgvrow.Cells[col++].Value = mergeCaseIDs;
                    dgvrow.Cells[col++].Value = "" + row["deadline"] == "" ? "" : DateTime.Parse("" + row["deadline"]).ToString("yyyy/MM/dd");
                    dgvrow.Cells[col++].Value = "" + row["place_name"];
                    dgvrow.Cells[col++].Value = "" + row["equip_name"];
                    dgvrow.Cells[col++].Value = "" + row["applicant_name"];
                    dgvrow.Cells[col++].Value = DateTime.Parse("" + row["apply_date"]).ToString("yyyy/MM/dd");
                    dgvrow.Tag = row;

                    dataGridViewX1.Rows.Add(dgvrow);
                }
            }
            this.ResumeLayout();
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataRow row = (DataRow)dataGridViewX1.SelectedRows[0].Tag;
                string workers = "" + dataGridViewX1.SelectedRows[0].Cells[2].Value;
                string cases = "" + dataGridViewX1.SelectedRows[0].Cells[3].Value;
                frmCaseDetail form = new frmCaseDetail(row, workers, cases);
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }
        }

        #region 設定完工期限
        private void btnSetDeadline_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.SelectedRows[0].Index > -1)
            {
                string caseID = "" + dataGridViewX1.SelectedRows[0].Cells[0].Value;
                string deadline = "" + dataGridViewX1.SelectedRows[0].Cells[4].Value;

                frmSetDeadline form = new frmSetDeadline(caseID, deadline);
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }
        }
        #endregion

        #region 設定案件維修人員
        private void btnSetCaseWorker_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.SelectedRows[0].Index > -1)
            {
                string caseID = "" + dataGridViewX1.SelectedRows[0].Cells[0].Value;

                frmSetCaseWorker form = new frmSetCaseWorker(caseID);
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }
        }
        #endregion

        #region 合併工單
        private void btnMergeCase_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.Rows[0].Index > -1)
            {
                DataRow row = (DataRow)dataGridViewX1.SelectedRows[0].Tag;
                frmMergeCase form = new frmMergeCase(row);
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }
        }
        #endregion

        #region 取消合併工單
        private void btnCancelMerge_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.Rows[0].Index > -1)
            {
                string caseID = "" + ((DataRow)dataGridViewX1.SelectedRows[0].Tag)["uid"];
                List<DataRow> listData = (List<DataRow>)dataGridViewX1.SelectedRows[0].Cells[3].Tag;

                frmCancelMergeCase form = new frmCancelMergeCase(caseID, listData);
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }
        }
        #endregion

        #region 工單結案
        private void btnCaseClose_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.Rows[0].Index > -1)
            {
                string caseID = "" + dataGridViewX1.SelectedRows[0].Cells[0].Value;
                frmCaseOpenOrClose form = new frmCaseOpenOrClose(CaseStatus.IsClose, caseID);
                form.Text = "工單結案";
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }  
        }
        #endregion

        #region 工單開啟
        private void btnCaseOpen_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.Rows[0].Index > -1)
            {
                string caseID = "" + dataGridViewX1.SelectedRows[0].Cells[0].Value;
                frmCaseOpenOrClose form = new frmCaseOpenOrClose(CaseStatus.UnClose, caseID);
                form.Text = "工單開啟";
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }
        }
        #endregion

        #region 更新維修進度
        private void btnRepairStatus_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.Rows[0].Index > -1)
            {
                DataRow row = (DataRow)dataGridViewX1.SelectedRows[0].Tag;

                frmSetRepairStatus form = new frmSetRepairStatus(row);
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView();
                    }
                };
                form.ShowDialog();
            }
        } 
        #endregion

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

public class Status
{
    public CaseStatus Mode { get; set; }
    public string Name { get; set; }
    public bool IsClose { get; set; }
}

public enum CaseStatus
{
    [Description("結案")]
    IsClose,
    [Description("未結案")]
    UnClose
}





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
using FISCA.UDT;
using System.Collections;

namespace Ischool.Equip_Repair
{
    public partial class frmEquipManager : BaseForm
    {
        public frmEquipManager()
        {
            InitializeComponent();
        }

        private void frmEquipManager_Load(object sender, EventArgs e)
        {
            ReloadTreeView();
        }

        #region 位置
        private void ReloadTreeView()
        {
            this.SuspendLayout();
            {
                treeView1.Nodes.Clear();
                // 取得位置資料
                DataTable dt = DAO.Place.GetPlaceData();

                TreeNode topNode = new TreeNode();
                topNode.Name = null;
                topNode.Text = string.Format("位置({0})", dt.Rows.Count);
                //topNode.Expand();
                treeView1.Nodes.Add(topNode);

                TreeRootExist(dt);
                CreateRootNode(topNode, dt);
            }
            this.ResumeLayout();
        }

        private void TreeRootExist(DataTable dt)
        {
            EnumerableRowCollection<DataRow> result = dt.AsEnumerable().Where(r => r.Field<string>("parent_id") == "");

            if (result.Any() == false)
            {
            }
        }

        private void CreateRootNode(TreeNode topNode, DataTable dt)
        {
            List<DataRow> listDataRow = GetTreeRoot(dt);

            foreach (DataRow row in listDataRow)
            {
                TreeNode node = new TreeNode();
                node.Text = row.Field<string>("name");
                node.Name = row.Field<string>("uid");
                topNode.Nodes.Add(node);

                CreateNode(dt, node);
            }

            topNode.Expand();
        }

        private void CreateNode(DataTable dt, TreeNode node)
        {
            IEnumerable<DataRow> Rows = GetTreeNodes(dt, node);

            TreeNode newNode;
            foreach (DataRow row in Rows)
            {
                newNode = new TreeNode();
                newNode.Text = row.Field<string>("name");
                newNode.Name = row.Field<string>("uid");
                node.Nodes.Add(newNode);

                CreateNode(dt, newNode);
            }
        }

        private IEnumerable<DataRow> GetTreeNodes(DataTable dt, TreeNode node)
        {
            return dt.AsEnumerable()
                .Where(r => r.Field<string>("parent_id") == node.Name)
                .OrderBy(r => r.Field<string>("uid"));
        }

        private List<DataRow> GetTreeRoot(DataTable dt)
        {
            return dt.AsEnumerable().Where(r => r.Field<string>("parent_id") == "").ToList();
        }

        private void btnAddPlace_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string parentID = treeView1.SelectedNode.Name;
                int level = treeView1.SelectedNode.Level;

                if (treeView1.SelectedNode.Level == 3)
                {
                    MsgBox.Show("階層已達上限，無法再往下新增所屬位置!");
                }
                else
                {
                    frmPlace form = new frmPlace(FormMode.Add, null, parentID, null, level);
                    form.Text = string.Format("{0}位置", EnumDescription.Get(typeof(FormMode), FormMode.Add.ToString()));
                    form.FormClosed += delegate
                    {
                        if (form.DialogResult == DialogResult.Yes)
                        {
                            ReloadTreeView();
                        }
                    };
                    form.ShowDialog();
                }
            }
        }

        private void btnUpdatePlace_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Name != "")
            {
                string placeID = treeView1.SelectedNode.Name;
                string parentID = "" + treeView1.SelectedNode.Parent.Name;
                int level = treeView1.SelectedNode.Level;
                string placeName = treeView1.SelectedNode.Text;

                frmPlace form = new frmPlace(FormMode.Update, placeName, parentID, placeID, level);
                form.Text = string.Format("{0}位置", EnumDescription.Get(typeof(FormMode), FormMode.Update.ToString()));
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadTreeView();
                    }
                };
                form.ShowDialog();
            }
        }

        private void btnDeletePlace_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Name != "")
            {
                string placeName = treeView1.SelectedNode.Text;
                string placeID = treeView1.SelectedNode.Name;

                DialogResult result = MsgBox.Show(string.Format("確定刪除{0}位置資料? ", placeName), "提醒", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DAO.Place.DeletePlaceDataByID(placeID);
                        MsgBox.Show("資料刪除成功!");
                        ReloadTreeView();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                ReloadDataGridView1(treeView1.SelectedNode.Name);
            }
        }

        #endregion

        #region 設施

        private void ReloadDataGridView1(string placeID)
        {
            if (!string.IsNullOrEmpty(placeID))
            {
                this.SuspendLayout();
                {
                    dataGridViewX1.Rows.Clear();
                    // 取得設施資料
                    DataTable dt = DAO.Equip.GetEquipDataByPlaceID(placeID);
                    foreach (DataRow row in dt.Rows)
                    {
                        DataGridViewRow dgvrow = new DataGridViewRow();
                        dgvrow.CreateCells(dataGridViewX1);

                        int col = 0;
                        dgvrow.Cells[col++].Value = "" + row["place_name"];
                        dgvrow.Cells[col++].Value = "" + row["equip_name"];
                        dgvrow.Cells[col++].Value = "" + row["property_no"];
                        dgvrow.Tag = "" + row["uid"]; // 設施系統編號

                        dataGridViewX1.Rows.Add(dgvrow);
                    }
                }
                this.ResumeLayout();
            }
            if (dataGridViewX1.SelectedRows.Count > 0 )
            {
                ReloadDataGridView2();
            }
            else
            {
                dataGridViewX2.Rows.Clear();
            }
        }

        private void btnAddEquip_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && !string.IsNullOrEmpty(treeView1.SelectedNode.Name))
            {
                string placeID = treeView1.SelectedNode.Name;
                string placeName = treeView1.SelectedNode.Text;

                frmEquip form = new frmEquip(FormMode.Add, placeID, placeName, null, null, null);
                form.Text = string.Format("{0}設施", EnumDescription.Get(typeof(FormMode), FormMode.Add.ToString()));
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView1(placeID);
                    }
                };
                form.ShowDialog();
            }
        }

        private void btnUpdateEquip_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (dataGridViewX1.SelectedRows.Count > 0)
                {
                    string placeID = treeView1.SelectedNode.Name;
                    string placeName = treeView1.SelectedNode.Text;
                    string equipID = "" + dataGridViewX1.SelectedRows[0].Tag;
                    string equipname = "" + dataGridViewX1.SelectedRows[0].Cells[1].Value;
                    string propertyNo = "" + dataGridViewX1.SelectedRows[0].Cells[2].Value;

                    frmEquip form = new frmEquip(FormMode.Update, placeID, placeName, equipID, equipname, propertyNo);
                    form.Text = string.Format("{0}設施", EnumDescription.Get(typeof(FormMode), FormMode.Update.ToString()));
                    form.FormClosed += delegate
                    {
                        if (form.DialogResult == DialogResult.Yes)
                        {
                            ReloadDataGridView1(placeID);
                        }
                    };
                    form.ShowDialog();
                }
            }
        }

        private void btnDeleteEquip_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (dataGridViewX1.SelectedRows.Count > 0)
                {
                    string equipID = "" + dataGridViewX1.SelectedRows[0].Tag;
                    string equipname = "" + dataGridViewX1.SelectedRows[0].Cells[1].Value;

                    DialogResult result = MsgBox.Show(string.Format("確定刪除{0}設施資料?", equipname), "提醒", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            DAO.Equip.DeleteEquipData(equipID);
                            MsgBox.Show("資料刪除成功!");
                            ReloadDataGridView1(treeView1.SelectedNode.Name);
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ReloadDataGridView2();
        }

        #endregion

        #region 原因

        private void ReloadDataGridView2()
        {
            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                this.SuspendLayout();
                {
                    string equipID = "" + dataGridViewX1.SelectedRows[0].Tag;
                    dataGridViewX2.Rows.Clear();

                    // 取得設施損壞原因資料
                    DataTable dt = DAO.BrokenReason.GetReasonByEquipID(equipID);

                    foreach (DataRow row in dt.Rows)
                    {
                        DataGridViewRow dgvrow = new DataGridViewRow();
                        dgvrow.CreateCells(dataGridViewX2);

                        int col = 0;
                        dgvrow.Cells[col++].Value = "" + row["equip_name"];
                        dgvrow.Cells[col++].Value = "" + row["reason"];
                        dgvrow.Tag = "" + row["uid"]; // 損壞原因系統編號

                        dataGridViewX2.Rows.Add(dgvrow);
                    }

                }
                this.ResumeLayout();
            }
        }

        private void btnAddReason_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.SelectedRows[0].Index > -1)
            {
                string equipID = "" + dataGridViewX1.SelectedRows[0].Tag;
                string equipName = "" + dataGridViewX1.SelectedRows[0].Cells[1].Value;
                frmBrokenReason form = new frmBrokenReason(FormMode.Add, equipID, equipName,null,null);
                form.Text = string.Format("{0}損壞原因",EnumDescription.Get(typeof(FormMode),FormMode.Add.ToString()));
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView2();
                    }
                };
                form.ShowDialog();
            }
        }

        private void btnUpdateReason_Click(object sender, EventArgs e)
        {
            if (dataGridViewX2.SelectedRows.Count > 0 && dataGridViewX2.SelectedRows[0].Index > -1)
            {
                string equipID = "" + dataGridViewX1.SelectedRows[0].Tag;
                string equipName = "" + dataGridViewX1.SelectedRows[0].Cells[1].Value;
                string reasonID = "" + dataGridViewX2.SelectedRows[0].Tag;
                string reason = "" + dataGridViewX2.SelectedRows[0].Cells[1].Value;
                frmBrokenReason form = new frmBrokenReason(FormMode.Update, equipID, equipName, reasonID, reason);
                form.Text = string.Format("{0}損壞原因", EnumDescription.Get(typeof(FormMode), FormMode.Update.ToString()));
                form.FormClosed += delegate
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridView2();
                    }
                };
                form.ShowDialog();
            }
        }

        private void btnDeleteReason_Click(object sender, EventArgs e)
        {
            if (dataGridViewX2.SelectedRows.Count > 0 && dataGridViewX2.SelectedRows[0].Index > -1)
            {
                string reason = "" + dataGridViewX2.SelectedRows[0].Cells[1].Value;

                DialogResult result = MsgBox.Show(string.Format("確定刪除「{0}」損壞原因?", reason),"提醒",MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string reasonID = "" + dataGridViewX2.SelectedRows[0].Tag;
                        DAO.BrokenReason.DeleteBrokenReason(reasonID);
                        MsgBox.Show("資料刪除成功!");
                        ReloadDataGridView2();
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        #endregion

    }
}

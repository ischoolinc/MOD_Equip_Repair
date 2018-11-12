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
using Aspose.Cells;
using FISCA.Data;

namespace Ischool.Equip_Repair
{
    public partial class frmExportPlaceEquip : BaseForm
    {
        public frmExportPlaceEquip()
        {
            InitializeComponent();
        }

        private void ckbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewEx1.Items)
            {
                item.Checked = ckbxSelectAll.Checked;
            }
        }

        private void wizardPage1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            List<string> listExportField = new List<string>();
            Workbook report = new Workbook();
            report.Worksheets[0].Name = "位置與設施資料";

            #region 1.取得選取欄位
            {
                foreach (ListViewItem item in listViewEx1.Items)
                {
                    if (item.Checked)
                    {
                        listExportField.Add(item.Text);
                    }
                }
            }
            #endregion

            #region 2.取得資料、寫入資料
            {
                string sql = @"
SELECT
	target_place.uid
	, target_place.level AS target_place_level
	, target_place.name
	, place2.name AS father
	, place2.level AS father_level
	, place3.name AS grandfather
	, place3.level AS grandfather_level
	, equip.name AS equip_name
    , equip.property_no
FROM
    $ischool.equip_repair.place AS target_place
    LEFT OUTER JOIN $ischool.equip_repair.place AS place2
        ON place2.uid = target_place.parent_id
    LEFT OUTER JOIN $ischool.equip_repair.place AS place3
        ON place3.uid = place2.parent_id
    LEFT OUTER JOIN $ischool.equip_repair.equip AS equip
        ON equip.ref_place_id = target_place.uid
ORDER BY
    target_place.level
    , target_place.name
    , equip.name
";
                QueryHelper qh = new QueryHelper();
                DataTable dt = qh.Select(sql);

                int rowIndex = 0;

                #region 填表頭
                {
                    int col = 0;
                    foreach (string tital in listExportField)
                    {
                        report.Worksheets[0].Cells[rowIndex, col++].PutValue(tital);
                    }
                    rowIndex++;
                }
                #endregion

                #region 填資料
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (rowIndex < 65536)
                        {
                            for (int col = 0; col < listExportField.Count(); col++)
                            {
                                switch (listExportField[col])
                                {
                                    case "位置名稱(階層1)":
                                        if ("" + row["target_place_level"] == "0")
                                        {
                                            report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["name"]);
                                        }
                                        else if ("" + row["father_level"] == "0")
                                        {
                                            report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["father"]);
                                        }
                                        else
                                        {
                                            report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["grandfather"]);
                                        }
                                        break;
                                    case "位置名稱(階層2)":
                                        if ("" + row["target_place_level"] == "1")
                                        {
                                            report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["name"]);
                                        }
                                        else
                                        {
                                            report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["father"]);
                                        }
                                        break;
                                    case "位置名稱(階層3)":
                                        if ("" + row["target_place_level"] == "2")
                                        {
                                            report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["name"]);
                                        }
                                        break;
                                    case "設施名稱":
                                        report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["equip_name"]);
                                        break;
                                    case "設施財產編號":
                                        report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["property_no"]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        rowIndex++;
                    }
                }
                #endregion
            }
            #endregion

            #region 3.儲存資料
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "匯出位置與設施資料";
                saveFileDialog.FileName = "匯出位置與設施資料.xls";
                saveFileDialog.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DialogResult result = new DialogResult();
                    try
                    {
                        report.Save(saveFileDialog.FileName);
                        result = MsgBox.Show("檔案儲存完成，是否開啟檔案?", "是否開啟", MessageBoxButtons.YesNo);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                        return;
                    }

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(saveFileDialog.FileName);
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show("開啟檔案發生失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    this.Close();
                }
            }
            #endregion
        }

        private void wizardPage1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
    }
}

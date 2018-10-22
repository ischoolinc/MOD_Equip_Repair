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
using System.IO;

namespace Ischool.Equip_Repair
{
    public partial class frmSumCase : BaseForm
    {
        public frmSumCase()
        {
            InitializeComponent();
        }

        private void frmSumCase_Load(object sender, EventArgs e)
        {
            dtStart.Value = DateTime.Now.AddDays(-7);
            dtEnd.Value = DateTime.Now;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // 根據日期區間取得申報案件資料
            DataTable dt = DAO.Case.GetCaseDataByDateRange(dtStart.Value.ToString("yyyy/MM/dd"), dtEnd.Value.ToString("yyyy / MM / dd"));

            Dictionary<string, List<DataRow>> dicCaseDataByKey = new Dictionary<string, List<DataRow>>();
            // 資料整理
            foreach (DataRow row in dt.Rows)
            {
                string key = "" + row["place_name"] + row["equip_name"];

                if (!dicCaseDataByKey.ContainsKey(key))
                {
                    dicCaseDataByKey.Add(key,new List<DataRow>());
                }
                dicCaseDataByKey[key].Add(row);
            }
            // 產出報表
            Workbook wb = new Workbook(new MemoryStream(Properties.Resources.統計申報案件樣板));

            //wb.Worksheets[0].Cells[0].PutValue("位置");
            //wb.Worksheets[0].Cells[1].PutValue("設施");
            //wb.Worksheets[0].Cells[2].PutValue("申報次數");

            int rowIndex = 1;
            foreach (string key in dicCaseDataByKey.Keys)
            {    
                int colIndex = 0;
                string placeName = "" + dicCaseDataByKey[key][0]["place_name"];
                string equipName = "" + dicCaseDataByKey[key][0]["equip_name"];
                int count = dicCaseDataByKey[key].Count();

                // 複製樣板格式
                wb.Worksheets[0].Cells.CopyRow(wb.Worksheets[0].Cells, 1, rowIndex);

                wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue(placeName);
                wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue(equipName);
                wb.Worksheets[0].Cells[rowIndex, colIndex++].PutValue(count);

                rowIndex++;
            }

            #region 儲存資料
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                string fileName = string.Format("{0}_{1}統計申報案件報表",dtStart.Value.ToString("yyyy_MM_dd"), dtEnd.Value.ToString("yyyy_MM_dd"));
                saveFileDialog.Title = fileName;
                saveFileDialog.FileName = string.Format("{0}.xlsx", fileName);
                saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|所有檔案 (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DialogResult result = new DialogResult();
                    try
                    {
                        wb.Save(saveFileDialog.FileName);
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

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

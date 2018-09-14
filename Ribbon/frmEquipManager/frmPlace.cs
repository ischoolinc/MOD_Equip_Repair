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
using FISCA.Data;

namespace Ischool.Equip_Repair
{
    public partial class frmPlace : BaseForm
    {
        private Dictionary<string, DataRow> _dicPlaceByName = new Dictionary<string, DataRow>();
        private FormMode _mode;
        private string _parentID;
        private string _placeName;
        private string _placeID;
        private int _level;

        public frmPlace(FormMode mode,string placeName,string parentID,string placeID,int level)
        {
            InitializeComponent();

            this._mode = mode;
            this._parentID = parentID;
            this._placeName = placeName;
            this._placeID = placeID;
            this._level = level;
        }

        private void frmPlace_Load(object sender, EventArgs e)
        {
            // 取得該階層所有位置資料
            DataTable dt = DAO.Place.GetPlaceDataByParentID(this._parentID);
            foreach (DataRow row in dt.Rows)
            {
                this._dicPlaceByName.Add("" + row["name"],row);
            }

            if (this._mode == FormMode.Add)
            {

            }
            if (this._mode == FormMode.Update)
            {
                if (this._dicPlaceByName.ContainsKey(this._placeName))
                {
                    this._dicPlaceByName.Remove(this._placeName);
                }

                tbxPlaceName.Text = this._placeName;
            }
        }

        private bool tbxPlaceName_Validate()
        {
            if (string.IsNullOrEmpty(tbxPlaceName.Text.Trim()))
            {
                errorProvider1.SetError(tbxPlaceName,"位置名稱不可空白!");
                return false;
            }
            else
            {
                // 判斷在同一個父節點下是否位置名稱重複
                if (this._dicPlaceByName.ContainsKey(tbxPlaceName.Text.Trim()))
                {
                    errorProvider1.SetError(tbxPlaceName, "位置名稱不可重複!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxPlaceName, null);
                    return true;
                }
            }

        }

        private void tbxPlaceName_TextChanged(object sender, EventArgs e)
        {
            tbxPlaceName_Validate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxPlaceName_Validate())
            {
                if (this._mode == FormMode.Add)
                {
                    try
                    {
                        DAO.Place.InsertPlaceData(tbxPlaceName.Text.Trim(), this._parentID, this._level);
                        MsgBox.Show("資料新增成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
                if (this._mode == FormMode.Update)
                {
                    try
                    {
                        DAO.Place.UpdatePlaceData(tbxPlaceName.Text.Trim(), this._placeID);
                        MsgBox.Show("資料修改成功!");
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

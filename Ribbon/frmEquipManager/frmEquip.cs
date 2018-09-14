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
    public partial class frmEquip : BaseForm
    {
        private FormMode _mode;
        private string _placeID;
        private string _placeName;
        private string _equipID;
        private string _equipName;
        private string _propertyNo;
        private Dictionary<string, DataRow> _dicEquipDataByName = new Dictionary<string, DataRow>();
        private List<string> _listPropertyNo = new List<string>();

        public frmEquip(FormMode mode,string placeID,string placeName,string equipID ,string equipName,string propertyNo)
        {
            InitializeComponent();

            this._mode = mode;
            this._placeID = placeID;
            this._placeName = placeName;
            this._equipID = equipID;
            this._equipName = equipName;
            this._propertyNo = propertyNo;
        }

        private void frmEquip_Load(object sender, EventArgs e)
        {
            #region 取得位置所屬的設備
            {
                DataTable dt = DAO.Equip.GetEquipDataByPlaceID(this._placeID);
                foreach (DataRow row in dt.Rows)
                {
                    this._dicEquipDataByName.Add("" + row["equip_name"], row);
                }
            }
            #endregion

            #region 取得所有設備資料
            {
                DataTable dt = DAO.Equip.GetAllEquipData();
                foreach (DataRow row in dt.Rows)
                {
                    this._listPropertyNo.Add("" + row["property_no"]);
                }
            }
            #endregion

            if (this._mode == FormMode.Add)
            {
                tbxPlaceName.Text = this._placeName;
            }
            if (this._mode == FormMode.Update)
            {
                if (this._dicEquipDataByName.ContainsKey(this._equipName))
                {
                    this._dicEquipDataByName.Remove(this._equipName);
                }
                if (this._listPropertyNo.Contains(this._propertyNo))
                {
                    this._listPropertyNo.Remove(this._propertyNo);
                }

                tbxPlaceName.Text = this._placeName;
                tbxEquipName.Text = this._equipName;
                tbxPropertyNo.Text = this._propertyNo;
            }
        }

        private bool tbxEquipName_Validate()
        {
            if (string.IsNullOrEmpty(tbxEquipName.Text.Trim()))
            {
                errorProvider1.SetError(tbxEquipName,"設施名稱不可空白!");
                return false;
            }
            else
            {
                if (this._dicEquipDataByName.ContainsKey(tbxEquipName.Text.Trim()))
                {
                    errorProvider1.SetError(tbxEquipName, "設施名稱不可重複!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxEquipName, null);
                    return true;
                }
            }
        }

        private bool tbxPropertyNo_Validate()
        {
            if (string.IsNullOrEmpty(tbxPropertyNo.Text.Trim()))
            {
                errorProvider1.SetError(tbxPropertyNo,"財產編號不可空白!");
                return false;
            }
            else
            {
                if (this._listPropertyNo.Contains(tbxPropertyNo.Text.Trim()))
                {
                    errorProvider1.SetError(tbxPropertyNo, "財產編號不可重複!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxPropertyNo, null);
                    return true;
                }
            }
        }

        private void tbxEquipName_TextChanged(object sender, EventArgs e)
        {
            tbxEquipName_Validate();
        }

        private void tbxPropertyNo_TextChanged(object sender, EventArgs e)
        {
            tbxPropertyNo_Validate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxEquipName_Validate() && tbxPropertyNo_Validate())
            {
                if (this._mode == FormMode.Add)
                {
                    try
                    {
                        DAO.Equip.InsertEquipData(tbxEquipName.Text.Trim(), tbxPropertyNo.Text.Trim(), this._placeID);
                        MsgBox.Show("資料新增成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
                if (this._mode == FormMode.Update)
                {
                    try
                    {
                        DAO.Equip.UpdateEquipData(tbxEquipName.Text.Trim(), tbxPropertyNo.Text.Trim(), this._equipID);
                        MsgBox.Show("資料更新成功!");
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

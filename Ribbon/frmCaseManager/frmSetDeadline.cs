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
    public partial class frmSetDeadline : BaseForm
    {
        private string _caseID;
        private string _deadline;

        public frmSetDeadline(string caseID,string deadline)
        {
            InitializeComponent();
            this._caseID = caseID;
            this._deadline = deadline;
        }

        private void frmSetDeadline_Load(object sender, EventArgs e)
        {
            tbxCaseID.Text = this._caseID;
            dtDeadline.Text = this._deadline;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DAO.Case.UpdateCaseDeadline(this._caseID, dtDeadline.Value.ToString("yyyy/MM/dd"));
                MsgBox.Show("資料更新成功!");
                this.DialogResult = DialogResult.Yes;
                this.Close();
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

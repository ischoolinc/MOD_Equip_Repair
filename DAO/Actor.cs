using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FISCA.Data;
using System.Data;

namespace Ischool.Equip_Repair.DAO
{
    class Actor
    {
        private static string _userAccount;
        private static bool _isAdmin;

        private static QueryHelper _qh = new QueryHelper();

        private static Actor _actor;

        public static Actor Instance
        {
            get
            {
                if (_actor == null)
                {
                    _actor = new Actor();
                }
                return _actor;
            }
        }

        private Actor()
        {
            _userAccount = FISCA.Authentication.DSAServices.UserAccount.Replace("'", "''");

            string sql = string.Format(@"
SELECT
    _login.*
FROM
    _login
    LEFT OUTER JOIN _lr_belong
        ON _login.id = _lr_belong._login_id
WHERE
    _login.login_name = '{0}'
    AND _lr_belong._role_id = {1}
            ", _userAccount,Program._adminRoleID);

            _isAdmin = _qh.Select(sql).Rows.Count > 0;
        }

        public string GetUserAccount()
        {
            return _userAccount;
        }

        public bool IsAdmin()
        {
            return _isAdmin;
        }

        public string GetLoginIDByAccount(string account)
        {
            string sql = string.Format(@"
SELECT
    *
FROM
    _login
WHERE
    login_name = '{0}'
            ", account);

            DataTable dt = _qh.Select(sql);

            if (dt.Rows.Count > 0)
            {
                return "" + dt.Rows[0]["id"];
            }
            else
            {
                return  "";
            }
        }
    }
}

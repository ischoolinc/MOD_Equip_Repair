using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FISCA.Data;
using K12.Data;

namespace Ischool.Equip_Repair.DAO
{
    class Role
    {
        private static QueryHelper _qh = new QueryHelper();
        private static UpdateHelper _up = new UpdateHelper();

        /// <summary>
        /// 取得角色資料
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static DataTable GetRoleDataByRoleName(string roleName)
        {
            string sql = string.Format(@"
SELECT
    *
FROM
    _role
WHERE
    role_name = '{0}'
            ",roleName);

            return _qh.Select(sql);
        }

        /// <summary>
        /// 更新角色權限
        /// </summary>
        public static void UpdateRole(string roleID,string permission)
        {
            string sql = string.Format(@"
UPDATE _role SET
    permission = '{0}'
WHERE
    id = {1}
            ", permission, roleID);

            _up.Execute(sql);
        }

        /// <summary>
        /// 建立角色
        /// </summary>
        public static void InsertRole(string roleName,string permisssion)
        {
            string sql = string.Format(@"
INSERT INTO _role(
    role_name
    , description
    , permission
) VALUES(
    '{0}'
    , ''
    , '{1}'
)
            ", roleName, permisssion);
            _up.Execute(sql);
        }
    }
}

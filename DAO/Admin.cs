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
    class Admin
    {
        private static QueryHelper _qh = new QueryHelper();
        private static UpdateHelper _up = new UpdateHelper();

        /// <summary>
        /// 取得管理員資料
        /// </summary>
        public static DataTable GetAdminData()
        {
            string sql = @"
SELECT
    admin.uid
    , teacher.teacher_name
    , admin.account
FROM
    $ischool.equip_repair.admin AS admin
    LEFT OUTER JOIN teacher
        ON admin.ref_teacher_id = teacher.id
";
            return _qh.Select(sql);
        }

        /// <summary>
        /// 取得尚未被指定為管理員或維修員的老師資料
        /// </summary>
        public static DataTable GetTeacherData()
        {
            string sql = @"
SELECT
    teacher.*
FROM
    teacher
    LEFT OUTER JOIN $ischool.equip_repair.admin AS admin
        ON admin.ref_teacher_id = teacher.id
    LEFT OUTER JOIN $ischool.equip_repair.worker AS worker
        ON worker.ref_teacher_id = teacher.id
WHERE
    admin.uid IS NULL
    AND worker.uid IS NULL
    AND teacher.status IN (1,2)
";
            return _qh.Select(sql);
        }

        /// <summary>
        /// 新增管理員
        /// </summary>
        public static void InsertAdmin(string teacherID,string account,string loginID,string roleID,string createTime,string createdBy)
        {
            string sql = "";
            if (string.IsNullOrEmpty(loginID))
            {
                #region SQL
                sql = string.Format(@"
WITH data_row AS(
    SELECT
        '{0}'::TEXT AS account
        , {1}::BIGINT AS ref_teacher_id
        , '{2}'::TIMESTAMP AS create_time
        , '{3}'::TEXT AS created_by
) , insert_admin AS(
    INSERT INTO $ischool.equip_repair.admin(
        account
        , ref_teacher_id
        , create_time
        , created_by
    )
    SELECT
        account
        , ref_teacher_id
        , create_time
        , created_by
    FROM
        data_row
) , insert_login AS(
    INSERT INTO _login(
        login_name
        , sys_admin
        , account_type
        , password
    )
    SELECT
        account
        , '0'
        , 'greening'
        , '1234'
    FROM
        data_row
    RETURNING *
) 
    INSERT INTO _lr_belong(
        _login_id
        , _role_id
    )
    SELECT
        insert_login.id
        , {4}
    FROM
        insert_login

                ", account, teacherID, createTime, createdBy, roleID);
                #endregion
            }
            else
            {
                #region SQL
                sql = string.Format(@"
WITH insert_unit_admin AS(
    INSERT INTO $ischool.equip_repair.admin(
        account
        , ref_teacher_id
        , create_time
        , created_by
    )
    VALUES(
        '{0}'
        , {1}
        , '{2}'
        , '{3}'
    )
) 
INSERT INTO _lr_belong(
    _login_id
    , _role_id
)
SELECT
    {4}
    , {5}
                    ", account, teacherID, createTime, createdBy, loginID, roleID);

                #endregion
            }
            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        /// <summary>
        /// 刪除管理員資料，以及該管理員角色權限
        /// </summary>
        public static void DeleteAdmin(string adminID, string roleID)
        {
            string sql = string.Format(@"
WITH data_row AS(
    SELECT
        _lr_belong.*
    FROM
        $ischool.equip_repair.admin AS admin
        LEFT OUTER JOIN _login
            ON _login.login_name = admin.account
        LEFT OUTER JOIN _lr_belong
            ON _lr_belong._login_id = _login.id
    WHERE
        admin.uid = {0}
        AND _lr_belong._role_id = {1}
) ,delete_admin AS(
    DELETE
    FROM
        $ischool.equip_repair.admin
    WHERE
        uid = {0}
) 
DELETE FROM
    _lr_belong
WHERE
    id IN (
    SELECT
        id
    FROM
        data_row
)
            ", adminID, roleID);

            _up.Execute(sql);
        }
    }
}

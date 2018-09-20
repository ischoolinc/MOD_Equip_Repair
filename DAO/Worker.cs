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
    class Worker
    {
        private static QueryHelper _qh = new QueryHelper();
        private static UpdateHelper _up = new UpdateHelper();

        public static DataTable GetCaseWorker(string caseID)
        {
            string sql = string.Format(@"
SELECT
    worker.uid
    , CASE WHEN case_belong.uid IS NOT NULL THEN true
        ELSE false
        END AS 指定
    , teacher.teacher_name
    , worker.account
FROM
    $ischool.equip_repair.worker AS worker
    LEFT OUTER JOIN (
        SELECT 
            * 
        FROM
            $ischool.equip_repair.case_belong
        WHERE
            ref_case_id = {0}
    ) AS case_belong
        ON case_belong.ref_worker_id = worker.uid
    LEFT OUTER JOIN teacher
        ON teacher.id = worker.ref_teacher_id
            ", caseID);

            return _qh.Select(sql);
        }

        /// <summary>
        /// 取得維修員資料
        /// </summary>
        public static DataTable GetWorkerData()
        {
            string sql = @"
SELECT
    worker.uid
    , teacher.teacher_name
    , worker.account
FROM
    $ischool.equip_repair.worker AS worker
    LEFT OUTER JOIN teacher
        ON teacher.id = worker.ref_teacher_id
";
            return _qh.Select(sql);
        }

        /// <summary>
        /// 新增維修員
        /// </summary>
        public static void InsertWorker(string teacherID, string account, string loginID, string roleID, string createTime, string createdBy)
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
) , insert_worker AS(
    INSERT INTO $ischool.equip_repair.worker(
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
        , ''
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
    INSERT INTO $ischool.equip_repair.worker(
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
            _up.Execute(sql);
        }

        /// <summary>
        /// 刪除維修員資料
        /// </summary>
        public static void DeleteWorkerByID(string workerID,string roleID)
        {
            string sql = string.Format(@"
WITH data_row AS(
    SELECT
        _lr_belong.*
    FROM
        $ischool.equip_repair.worker AS worker
        LEFT OUTER JOIN _login
            ON _login.login_name = worker.account
        LEFT OUTER JOIN _lr_belong
            ON _lr_belong._login_id = _login.id
    WHERE
        worker.uid = {0}
        AND _lr_belong._role_id = {1}
) ,delete_worker AS(
    DELETE
    FROM
        $ischool.equip_repair.worker
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
            ", workerID, roleID);

            _up.Execute(sql);
        }
    }
}

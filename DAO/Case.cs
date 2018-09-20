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
    class Case
    {
        private static QueryHelper _qh = new QueryHelper();
        private static UpdateHelper _up = new UpdateHelper();

        /// <summary>
        /// 取得可被合併到的案件
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCanMergeCaseData()
        {
            string sql = @"
SELECT
    *
FROM
    $ischool.equip_repair.case
WHERE
    ref_case_id IS NULL
";

            return _qh.Select(sql);
        }

        public static DataTable GetCaseDataByCondition(string startDate,string endDate,bool isClose)
        {
            string sql = "";
            if (string.IsNullOrEmpty(startDate))
            {
                sql = string.Format(@"
SELECT
    rp_case.*
    , equip.name AS equip_name
    , place.name AS place_name
FROM
    $ischool.equip_repair.case AS rp_case
    LEFT OUTER JOIN $ischool.equip_repair.equip AS equip
        ON equip.uid = rp_case.ref_equip_id
    LEFT OUTER JOIN $ischool.equip_repair.place AS place
        ON place.uid = equip.ref_place_id
WHERE
    rp_case.ref_case_id IS NULL
    AND rp_case.is_close = {0}::BOOLEAN
                ", isClose);
            }
            else
            {
                sql = string.Format(@"
SELECT
    rp_case.*
    , equip.name AS equip_name
    , place.name AS place_name
FROM
    $ischool.equip_repair.case AS rp_case
    LEFT OUTER JOIN $ischool.equip_repair.equip AS equip
        ON equip.uid = rp_case.ref_equip_id
    LEFT OUTER JOIN $ischool.equip_repair.place AS place
        ON place.uid = equip.ref_place_id
WHERE
    rp_case.ref_case_id IS NULL
    AND DATE_TRUNC('day',rp_case.apply_date) >= '{0}'::TIMESTAMP
    AND DATE_TRUNC('day',rp_case.apply_date) <= '{1}'::TIMESTAMP
    AND rp_case.is_close = {2}::BOOLEAN
                ", startDate, endDate, isClose);
            }

            return _qh.Select(sql);
        }

        public static DataTable GetCaseDataByDateRange(string startDate,string endDate)
        {
            string sql = string.Format(@"
SELECT
    rp_case.uid
    , place.name AS place_name
    , equip.name AS equip_name
FROM
    $ischool.equip_repair.case AS rp_case
    LEFT OUTER JOIN $ischool.equip_repair.equip AS equip
        ON equip.uid = rp_case.ref_equip_id
    LEFT OUTER JOIN $ischool.equip_repair.place AS place
        ON place.uid = equip.ref_place_id
WHERE
    rp_case.ref_case_id IS NULL
    AND DATE_TRUNC('day',rp_case.apply_date) >= '{0}'::TIMESTAMP
    AND DATE_TRUNC('day',rp_case.apply_date) <= '{1}'::TIMESTAMP
            ", startDate, endDate);

            return _qh.Select(sql);
        }

        public static DataTable GetCaseWorkerByCaseID(List<string>listIDs)
        {
            string sql = string.Format(@"
SELECT
    case_belong.ref_case_id
    , worker.*
    , teacher.teacher_name
FROM
    $ischool.equip_repair.case_belong AS case_belong
    LEFT OUTER JOIN $ischool.equip_repair.worker AS worker
        ON worker.uid = case_belong.ref_worker_id
    LEFT OUTER JOIN teacher 
        ON teacher.id = worker.ref_teacher_id
WHERE
    ref_case_id IN( {0} )
            ", string.Join(",", listIDs));

            return _qh.Select(sql);
        }

        public static DataTable GetMergeCaseByCaseID(List<string> listIDs)
        {
            string sql = string.Format(@"
SELECT 
    rp_case.*
    , equip.name AS equip_name
FROM
    $ischool.equip_repair.case AS rp_case
    LEFT OUTER JOIN $ischool.equip_repair.equip AS equip
        ON equip.uid = rp_case.ref_equip_id
WHERE
    rp_case.ref_case_id IN( {0} )
            ", string.Join(",", listIDs));

            return _qh.Select(sql);
        }

        public static void UpdateCaseDeadline(string caseID,string deadline)
        {
            string sql = string.Format(@"
UPDATE $ischool.equip_repair.case SET
    deadline = '{0}'::TIMESTAMP
WHERE
    uid = {1}
            ", deadline, caseID);

            _up.Execute(sql);
        }

        public static void UpdateCaseWorkers(string caseID,List<string>listWorkerIDs)
        {
            List<string> listData = new List<string>();

            foreach (string workerID in listWorkerIDs)
            {
                string data = string.Format(@"
SELECT
    {0}::BIGINT AS ref_case_id
    , {1}::BIGINT AS ref_worker_id
                ",caseID,workerID);

                listData.Add(data);
            }

            string dataRow = string.Join("UNION ALL",listData);

            string sql = string.Format(@"
WITH data_row AS(
    {0}
) , delete_case_belong AS(
    DELETE
    FROM
        $ischool.equip_repair.case_belong
    WHERE
        ref_case_id = {1}
) , insert_case_belong AS(
    INSERT INTO $ischool.equip_repair.case_belong(
        ref_case_id
        , ref_worker_id
    )
    SELECT
        ref_case_id
        , ref_worker_id
    FROM
        data_row
)

SELECT 0
            ",dataRow,caseID);

            _qh.Select(sql);
        }

        public static void UpdateRefCaseID(string caseID,string refCaseID)
        {
            string sql = "";
            if (string.IsNullOrEmpty(refCaseID))
            {
                sql = string.Format(@"
UPDATE $ischool.equip_repair.case SET
    ref_case_id = {0}
WHERE
    uid = {1}
            ", "null",caseID);
            }
            else
            {
                sql = string.Format(@"
UPDATE $ischool.equip_repair.case SET
    ref_case_id = {0}
WHERE
    uid = {1}
            ", refCaseID, caseID);
            }
            

            _up.Execute(sql);
        }

        public static void UpdateFixStatus(string caseID,string fixStatus)
        {
            string sql = string.Format(@"
UPDATE $ischool.equip_repair.case SET
    fix_status = '{0}'
    , repair_account = '{1}'
    , repair_time = '{2}'::TIMESTAMP
WHERE
    uid = {3}
            ",fixStatus,DAO.Actor.Instance.GetUserAccount(),DateTime.Now.ToString("yyyy/MM/dd"),caseID);

            _up.Execute(sql);
        }

        public static void UpdateCaseIsClose(string caseID,string isClose,string closeTime,string closeBy)
        {
            string sql = "";
            if (string.IsNullOrEmpty(closeTime) && string.IsNullOrEmpty(closeBy))
            {
                sql = string.Format(@"
UPDATE $ischool.equip_repair.case SET
    is_close = {0}
    , close_time = null
    , close_by = null
WHERE
    uid = {1}
            ", isClose, caseID);
            }
            else
            {
                sql = string.Format(@"
UPDATE $ischool.equip_repair.case SET
    is_close = {0}
    , close_time = '{1}'::TIMESTAMP
    , close_by = '{2}'
WHERE
    uid = {3}
            ", isClose, closeTime, closeBy, caseID);
            }

            _up.Execute(sql);
        }
    }
}

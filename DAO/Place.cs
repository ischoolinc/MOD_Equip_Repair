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
    class Place
    {
        private static QueryHelper _qh = new QueryHelper();

        private static UpdateHelper _up = new UpdateHelper();

        public static DataTable GetPlaceData()
        {
            string sql = @"
SELECT
    *
FROM
    $ischool.equip_repair.place
ORDER BY
    uid
";
            return _qh.Select(sql);
        }

        public static DataTable GetPlaceDataByParentID(string parentID)
        {
            string sql = string.Format(@"
SELECT
    *
FROM
    $ischool.equip_repair.place
WHERE
    parent_id {0}
            ",parentID == "" ? "IS NULL" : " = " + parentID);

            return _qh.Select(sql);
        }

        public static void InsertPlaceData(string placeName,string parentID,int level)
        {
            string sql = string.Format(@"
WITH data_row AS(
    SELECT
        '{0}'::TEXT AS name
        , {1}::BIGINT AS parent_id
        , {2}::BIGINT AS level
        , '{3}'::TIMESTAMP AS create_time
        , '{4}'::TEXT AS created_by
) 
INSERT INTO $ischool.equip_repair.place(
    name
    , parent_id
    , level
    , create_time
    , created_by
)
SELECT
     name
    , parent_id
    , level
    , create_time
    , created_by
FROM
    data_row
            ", placeName, parentID == "" ? "null" : parentID, level,DateTime.Now.ToString("yyyy/MM/dd"),Actor.Instance.GetUserAccount());

            _up.Execute(sql);
        }

        public static void UpdatePlaceData(string placeName,string placeID)
        {
            string sql = string.Format(@"
UPDATE $ischool.equip_repair.place SET
    name = '{0}'
WHERE
    uid = {1}
            ",placeName, placeID);

            _up.Execute(sql);
        }

        public static void DeletePlaceDataByID(string placeID)
        {
            string sql = string.Format(@"
WITH target_data AS(
    SELECT 
	n1.uid AS n1_uid
	, n2.uid AS n2_uid
	, n3.uid As n3_uid
FROM
	$ischool.equip_repair.place AS n1
	LEFT OUTER JOIN $ischool.equip_repair.place AS n2
		ON n2.parent_id = n1.uid
	LEFT OUTER JOIN $ischool.equip_repair.place AS n3
		ON n3.parent_id = n2.uid
WHERE
	n1.uid = {0}
)
DELETE
FROM
    $ischool.equip_repair.place
WHERE
    uid = (SELECT n1_uid FROM target_data)
    OR uid = (SELECT n2_uid FROM target_data)
    OR uid = (SELECT n3_uid FROM target_data)
            ", placeID);

            _up.Execute(sql);
        }

    }
}

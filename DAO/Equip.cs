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
    class Equip
    {
        private static QueryHelper _qh = new QueryHelper();
        private static UpdateHelper _up = new UpdateHelper();

        public static DataTable GetAllEquipData()
        {
            string sql = @"
SELECT
    *
FROM
    $ischool.equip_repair.equip
            ";

            return _qh.Select(sql);
        }

        public static DataTable GetEquipDataByPlaceID(string placeID)
        {
            string sql = string.Format(@"
SELECT
    place.name AS place_name
    , equip.name AS equip_name
    , equip.uid
    , equip.property_no
FROM
    $ischool.equip_repair.equip AS equip
    LEFT OUTER JOIN $ischool.equip_repair.place AS place
        ON place.uid = equip.ref_place_id
WHERE
    ref_place_id = {0}
            ", placeID);

            return _qh.Select(sql);
        }

        public static void InsertEquipData(string equipName,string propertyNo,string placeID)
        {
            string sql = string.Format(@"
WITH data_row AS(
    SELECT
        '{0}'::TEXT AS name
        , '{1}'::TEXT AS property_no
        , {2}::BIGINT AS ref_place_id
        , '{3}'::TIMESTAMP AS create_time
        , '{4}'::TEXT AS created_by
)
INSERT INTO $ischool.equip_repair.equip(
    name
    , property_no
    , ref_place_id
    , create_time
    , created_by
)
SELECT
    name
    , property_no
    , ref_place_id
    , create_time
    , created_by
FROM
    data_row
            ", equipName, propertyNo, placeID,DateTime.Now.ToString("yyyy/MM/dd"),DAO.Actor.Instance.GetUserAccount());

            _up.Execute(sql);
        }

        public static void UpdateEquipData(string equipName, string propertyNo, string equipID)
        {
            string sql = string.Format(@"
UPDATE $ischool.equip_repair.equip SET
    name = '{0}'
    , property_no = '{1}'
WHERE
    uid = {2}
            ", equipName, propertyNo, equipID);

            _up.Execute(sql);
        }

        public static void DeleteEquipData(string equipID)
        {
            string sql = string.Format(@"
DELETE FROM
    $ischool.equip_repair.equip
WHERE
    uid = {0}
            ", equipID);

            _up.Execute(sql);
        }
    }
}

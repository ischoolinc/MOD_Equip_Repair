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
    class BrokenReason
    {
        private static QueryHelper _qh = new QueryHelper();
        private static UpdateHelper _up = new UpdateHelper();

        public static DataTable GetReasonByEquipID(string equipID)
        {
            string sql = string.Format(@"
SELECT
    equip.name AS equip_name
    , reason.*
FROM
    $ischool.equip_repair.broken_reason AS reason
    LEFT OUTER JOIN $ischool.equip_repair.equip AS equip
        ON reason.ref_equip_id = equip.uid
WHERE
    ref_equip_id = {0}
            ", equipID);

            return _qh.Select(sql);
        }

        public static void InsertBrokenReason(string equipID,string reason)
        {
            string sql = string.Format(@"
INSERT INTO $ischool.equip_repair.broken_reason(
    ref_equip_id
    , reason
)
VALUES(
    {0}
    , '{1}'
)
            ", equipID, reason);

            _up.Execute(sql);
        }

        public static void UpdateBrokenReason(string reasonID,string reason)
        {
            string sql = string.Format(@"
UPDATE $ischool.equip_repair.broken_reason SET
    reason = '{0}'
WHERE
    uid = {1}
            ", reason, reasonID);

            _up.Execute(sql);
        }

        public static void DeleteBrokenReason(string reasonID)
        {
            string sql = string.Format(@"
DELETE FROM
    $ischool.equip_repair.broken_reason
WHERE
    uid = {0}
            ", reasonID);

            _up.Execute(sql);
        }
    }
}

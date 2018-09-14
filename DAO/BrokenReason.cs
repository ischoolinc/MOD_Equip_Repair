using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FISCA.Data;

namespace Ischool.Equip_Repair.DAO
{
    class BrokenReason
    {
        private static QueryHelper _qh = new QueryHelper();

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
    }
}

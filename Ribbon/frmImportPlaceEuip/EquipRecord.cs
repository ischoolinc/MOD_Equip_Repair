using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ischool.Equip_Repair
{
    public class EquipRecord
    {
        /// <summary>
        /// 設施系統編號
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 設施名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 財產編號
        /// </summary>
        public string PropertyNo { get; set; }
        /// <summary>
        /// 位置系統編號
        /// </summary>
        public string RefPlaceID { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 建立者帳號
        /// </summary>
        public string CreateBy { get; set; }
    }
}

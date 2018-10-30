using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ischool.Equip_Repair
{
    public class PlaceRecord
    {
        /// <summary>
        /// 位置系統編號
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 位置名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 位置系統編號
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 階層
        /// </summary>
        public int Level { get; set; }
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

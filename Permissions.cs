using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ischool.Equip_Repair
{
    class Permissions
    {
        public static string 設定管理員 { get { return "71126486-17F8-42F4-8AD9-1ABC5E538389"; } }
        public static bool 設定管理員權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[設定管理員].Executable;
            }
        }

        public static string 設定維修人員 { get { return "971BC2FA-F2DB-427C-91FF-35B4A570ADC5"; } }
        public static bool 設定維修人員權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[設定維修人員].Executable;
            }
        }

        public static string 管理位置與設施 { get { return "76809C59-7F04-4C79-BD32-55878FAB5534"; } }
        public static bool 管理位置與設施權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[管理位置與設施].Executable;
            }
        }

        public static string 統計申報案件 { get { return "A70B65AE-E5AD-4AA0-B5A8-EFFB2B2EBB22"; } }
        public static bool 統計申報案件權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[統計申報案件].Executable;
            }
        }

        public static string 管理申報案件 { get { return "4D8E9592-9599-47D9-879B-B59E0D852408"; } }
        public static bool 管理申報案件權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[管理申報案件].Executable;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FISCA;
using FISCA.UDT;
using K12.Data.Configuration;
using FISCA.Presentation;
using FISCA.Permission;
using FISCA.Presentation.Controls;

namespace Ischool.Equip_Repair
{
    public class Program
    {
        #region 管理員角色
        /// <summary>
        /// 設施報修管理員角色名稱
        /// </summary>
        public static string _adminRoleName = "設施報修管理員";

        public static string _adminRoleID;

        /// <summary>
        /// 設施報修管理員角色功能權限
        /// </summary>
        public static string _adminPermission = @"
<Permissions>
<Feature Code=""71126486-17F8-42F4-8AD9-1ABC5E538389"" Permission=""Execute""/>
<Feature Code=""971BC2FA-F2DB-427C-91FF-35B4A570ADC5"" Permission=""Execute""/>
<Feature Code=""76809C59-7F04-4C79-BD32-55878FAB5534"" Permission=""Execute""/>
<Feature Code=""76809C59-7F04-4C79-BD32-55878FAB5534"" Permission=""Execute""/>
<Feature Code=""A70B65AE-E5AD-4AA0-B5A8-EFFB2B2EBB22"" Permission=""Execute""/>
<Feature Code=""4D8E9592-9599-47D9-879B-B59E0D852408"" Permission=""Execute""/>
</Permissions>
";
        #endregion

        #region 維修員角色
        /// <summary>
        /// 設施報修維修員角色名稱
        /// </summary>
        public static string _workerRoleName = "設施報修維修員";

        public static string _workerRoleID;

        /// <summary>
        /// 設施報修維修員角色功能權限
        /// </summary>
        public static string _workerPermission = @"
<Permissions>
</Permissions>
"; 
        #endregion

        [MainMethod("設施報修")]
        public static void Main()
        {
            #region 設施報修
            {
                // Init 模組專用角色
                ModuleRole role = ModuleRole.Instance;

                // 設施報修模組
                MotherForm.AddPanel(EquipRepairPanel.Instance);

                MotherForm.RibbonBarItems["設施報修", "基本設定"]["人員設定"].Image = Properties.Resources.foreign_language_config_64;
                MotherForm.RibbonBarItems["設施報修", "基本設定"]["人員設定"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設施報修", "基本設定"]["管理位置與設施"].Image = Properties.Resources.flux_diagram_config_128;
                MotherForm.RibbonBarItems["設施報修", "基本設定"]["管理位置與設施"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["報表"].Image = Properties.Resources.Report;
                MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["管理申報案件"].Image = Properties.Resources.invoice_zoom_128;
                MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["管理申報案件"].Size = RibbonBarButton.MenuButtonSize.Large;

                #region 設定管理員
                {
                    MotherForm.RibbonBarItems["設施報修", "基本設定"]["人員設定"]["設定管理員"].Enable = Permissions.設定管理員權限;
                    MotherForm.RibbonBarItems["設施報修", "基本設定"]["人員設定"]["設定管理員"].Click += delegate
                    {
                        (new frmAdmin()).ShowDialog();
                    };
                }
                #endregion

                #region 設定維修人員
                {
                    MotherForm.RibbonBarItems["設施報修", "基本設定"]["人員設定"]["設定維修人員"].Enable = Permissions.設定維修人員權限;
                    MotherForm.RibbonBarItems["設施報修", "基本設定"]["人員設定"]["設定維修人員"].Click += delegate
                    {
                        (new frmWorker()).ShowDialog();
                    };
                }
                #endregion

                #region 管理位置
                {
                    MotherForm.RibbonBarItems["設施報修", "基本設定"]["管理位置與設施"].Enable = Permissions.管理位置與設施權限;
                    MotherForm.RibbonBarItems["設施報修", "基本設定"]["管理位置與設施"].Click += delegate
                    {
                        (new frmEquipManager()).ShowDialog();
                    };
                }
                #endregion

                #region 統計申報案件
                {
                    MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["報表"]["統計申報案件"].Enable = Permissions.統計申報案件權限;
                    MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["報表"]["統計申報案件"].Click += delegate
                    {

                    };
                }
                #endregion

                #region 管理申報案件
                {
                    MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["管理申報案件"].Enable = Permissions.管理申報案件權限;
                    MotherForm.RibbonBarItems["設施報修", "案件管理/案件統計"]["管理申報案件"].Click += delegate
                    {

                    };
                }
                #endregion


                #region 管理權限
                {
                    Catalog detail = new Catalog();
                    detail = RoleAclSource.Instance["設施報修"]["功能按鈕"];
                    detail.Add(new RibbonFeature(Permissions.設定管理員, "設定管理員"));
                    detail.Add(new RibbonFeature(Permissions.設定維修人員, "設定維修人員"));
                    detail.Add(new RibbonFeature(Permissions.管理位置與設施, "管理位置與設施"));
                    detail.Add(new RibbonFeature(Permissions.統計申報案件, "統計申報案件"));
                    detail.Add(new RibbonFeature(Permissions.管理申報案件, "管理申報案件"));
                }
                #endregion
            }
            #endregion
        }
    }
}

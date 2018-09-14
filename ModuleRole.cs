using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FISCA.Presentation.Controls;

namespace Ischool.Equip_Repair
{
    /// <summary>
    /// 管理模組專用角色
    /// </summary>
    class ModuleRole
    {

        private static ModuleRole _moduleRole;

        public static ModuleRole Instance
        {
            get
            {
                if (_moduleRole == null)
                {
                    _moduleRole = new ModuleRole();
                }
                return _moduleRole;
            }
        }

        private ModuleRole()
        {

            #region 管理員
            {
                DataTable dt = DAO.Role.GetRoleDataByRoleName(Program._adminRoleName);
                if (dt.Rows.Count > 0) // 角色存在
                {
                    Program._adminRoleID = "" + dt.Rows[0]["id"];
                    // 更新權限
                    DAO.Role.UpdateRole(Program._adminRoleID, Program._adminPermission);
                }
                else 
                {
                    // 建立角色
                    DAO.Role.InsertRole(Program._adminRoleName, Program._adminPermission);
                }
            }
            #endregion

            #region 維修員
            {
                DataTable dt = DAO.Role.GetRoleDataByRoleName(Program._workerRoleName);
                if (dt.Rows.Count > 0) // 角色存在
                {
                    Program._workerRoleID = "" + dt.Rows[0]["id"];
                    // 更新權限
                    DAO.Role.UpdateRole(Program._workerRoleID, Program._workerPermission);
                    
                }
                else 
                {
                    // 建立角色
                    DAO.Role.InsertRole(Program._workerRoleName, Program._workerPermission);
                }
            }
            #endregion

        }
    }
}

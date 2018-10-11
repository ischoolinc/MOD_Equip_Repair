using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation;
using CefSharp.WinForms;

namespace Ischool.Equip_Repair
{
    public partial class EquipRepairPanel : BlankPanel
    {
        private ChromiumWebBrowser _browser;

        private EquipRepairPanel()
        {
            this.Group = "設施報修";

            this._browser = new ChromiumWebBrowser("https://sites.google.com/ischool.com.tw/equipment-repair/%E9%A6%96%E9%A0%81");
            this._browser.Dock = DockStyle.Fill;
            ContentPanePanel.Controls.Add(this._browser);

        }

        private static EquipRepairPanel _panel;

        public static EquipRepairPanel Instance
        {
            get
            {
                if (_panel == null)
                {
                    _panel = new EquipRepairPanel();
                }
                return _panel;
            }
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ContentPanePanel
            // 
            this.ContentPanePanel.Location = new System.Drawing.Point(0, 163);
            this.ContentPanePanel.Size = new System.Drawing.Size(870, 421);
            // 
            // TidyCompetitionPanel
            // 
            this.Name = "EquipRepairPanel";
            this.ResumeLayout(false);
        }
    }
}
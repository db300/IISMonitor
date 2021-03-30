using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IISMonitor.ReleaseManagement
{
    public partial class ReleaseManagerPanel : UserControl
    {
        #region constructor

        public ReleaseManagerPanel()
        {
            InitializeComponent();

            InitForm();
            InitContent();
        }

        #endregion

        #region 界面初始化

        private void InitForm()
        {
            DoubleBuffered = true;
        }

        private void InitContent()
        {
            #region 应用程序池操作

            btnCreateApplicationPool.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtApplicationPool.Text))
                {
                    MessageBox.Show("应用程序池名称为空");
                    return;
                }
                using (var appPoolsManager = new iHawkIISLibrary.ApplicationPoolsManager())
                {
                    txtLog.AppendText($"{txtApplicationPool.Text}: {appPoolsManager.CreateApplicationPool(txtApplicationPool.Text)}\r\n");
                }
            };

            #endregion

            #region 网站应用程序操作

            btnRefreshWebsiteList.Click += (sender, e) =>
            {
                cmbWebsiteList.Items.Clear();
                using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
                {
                    cmbWebsiteList.Items.AddRange(websitesManager.GetWebsiteList().Cast<object>().ToArray());
                }
            };
            btnBrowser.Click += (sender, e) =>
            {
                var dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;
                txtPhysicalPath.Text = dlg.SelectedPath;
            };
            btnRefreshAppPoolList.Click += (sender, e) =>
            {
                cmbAppPoolList.Items.Clear();
                using (var applicationPoolsManager = new iHawkIISLibrary.ApplicationPoolsManager())
                {
                    cmbAppPoolList.Items.AddRange(applicationPoolsManager.GetApplicationPoolList().Cast<object>().ToArray());
                }
            };
            btnCreateWebsiteApplication.Click += (sender, e) =>
            {
                if (cmbWebsiteList.SelectedIndex < 0)
                {
                    MessageBox.Show("未选择网站");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtVirtualPath.Text))
                {
                    MessageBox.Show("应用程序虚拟路径为空");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPhysicalPath.Text))
                {
                    MessageBox.Show("应用程物理路径为空");
                    return;
                }
                if (!Directory.Exists(txtPhysicalPath.Text))
                {
                    MessageBox.Show("应用程序物理路径不存在");
                    return;
                }
                if (cmbAppPoolList.SelectedIndex < 0)
                {
                    MessageBox.Show("未选择应用程序池");
                    return;
                }
                using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
                {
                    txtLog.AppendText($"{txtVirtualPath.Text}: {websitesManager.CreateWebsiteApplication(cmbWebsiteList.Text, txtVirtualPath.Text, txtPhysicalPath.Text, cmbAppPoolList.Text)}\r\n");
                }
            };

            #endregion
        }

        #endregion
    }
}

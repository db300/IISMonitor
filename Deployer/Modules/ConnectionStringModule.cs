using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deployer.Modules
{
    /// <summary>
    /// 数据库连接串
    /// </summary>
    public partial class ConnectionStringModule : UserControl
    {
        #region constructor
        public ConnectionStringModule()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region property
        private iHawkAppControl.iTreeView.AdSelectTreeView _tvVirtualPath;
        private TextBox _txtConnectString;
        #endregion

        #region event handler
        private void BtnView_Click(object sender, EventArgs e)
        {
            View();
        }

        private void BtnSetConnectionString_Click(object sender, EventArgs e)
        {
            var dlg = new ConnectionStringsForm();
            if (dlg.ShowDialog() != DialogResult.OK) return;
            var dict = dlg.ConnectionStringDict;
            if (!(dict?.Count > 0)) return;
            WriteConnectionStrings(dict);
        }

        private void TvVirtualPath_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == SystemColors.GrayText) e.Cancel = true;
        }

        private void TvVirtualPath_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == SystemColors.GrayText) e.Cancel = true;
        }
        #endregion

        #region method
        private void View()
        {
            _tvVirtualPath.Nodes.Clear();
            string website = "";
            List<string> virtualPathList = null;
            var sb = new StringBuilder();
            using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
            {
                var websites = websitesManager.GetWebsiteList();
                if (websites.Count == 0)
                {
                    _txtConnectString.Text = "ERROR: 默认网站不存在";
                    return;
                }

                website = websites[0];
                virtualPathList = websitesManager.GetWebsiteApplicationList(website);
                virtualPathList.Remove("/");
            }
            using (var webConfigManager = new iHawkIISLibrary.WebConfigManager())
            {
                foreach (var path in virtualPathList)
                {
                    var dict = webConfigManager.GetConnectionStrings(website, path);

                    var treeNode = new TreeNode(path);
                    _tvVirtualPath.Nodes.Add(treeNode);
                    sb.AppendLine("==============================");
                    sb.AppendLine(path);
                    if (dict == null)
                    {
                        treeNode.ForeColor = SystemColors.GrayText;
                        sb.AppendLine("暂时无法获取");
                    }
                    else
                    {
                        treeNode.Checked = true;
                        foreach (var item in dict) sb.AppendLine($"{item.Key}: {item.Value}");
                    }
                    sb.AppendLine("------------------------------");
                }
            }
            _txtConnectString.Text = sb.ToString();
        }

        private void WriteConnectionStrings(Dictionary<string, string> connectStringDict)
        {
            string website = "";
            using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
            {
                var websites = websitesManager.GetWebsiteList();
                if (websites.Count == 0)
                {
                    _txtConnectString.Text = "ERROR: 默认网站不存在";
                    return;
                }

                website = websites[0];
            }
            using (var webConfigManager = new iHawkIISLibrary.WebConfigManager())
            {
                foreach (TreeNode node in _tvVirtualPath.Nodes)
                {
                    if (!node.Checked) continue;
                    var s = webConfigManager.AddConnectionStrings(website, node.Text, connectStringDict, true);
                    _txtConnectString.AppendText($"INFO: {node.Text} set connectionStrings {s}\r\n");
                }
                _txtConnectString.AppendText("INFO: connectionStrings set done.\r\n");
            }
        }
        #endregion

        #region ui
        private void InitUi()
        {
            var btnView = new Button
            {
                AutoSize = true,
                Location = new Point(20, 20),
                Parent = this,
                Text = "快速查看"
            };
            btnView.Click += BtnView_Click;
            var btnSetConnectionString = new Button
            {
                AutoSize = true,
                Location = new Point(btnView.Right + 12, btnView.Top),
                Parent = this,
                Text = "批量设置"
            };
            btnSetConnectionString.Click += BtnSetConnectionString_Click;
            _tvVirtualPath = new iHawkAppControl.iTreeView.AdSelectTreeView
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                CheckBoxes = true,
                Font = new Font(Font.FontFamily, 12),
                FullRowSelect = true,
                HideSelection = false,
                ItemHeight = 25,
                Location = new Point(btnView.Left, btnView.Bottom + 12),
                Parent = this,
                ShowLines = false,
                Size = new Size(300, ClientSize.Height - 20 - btnView.Bottom - 12)
            };
            _tvVirtualPath.BeforeCheck += TvVirtualPath_BeforeCheck;
            _tvVirtualPath.BeforeSelect += TvVirtualPath_BeforeSelect;
            _txtConnectString = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.White,
                Font = new Font(Font.FontFamily, 12),
                Location = new Point(_tvVirtualPath.Right + 12, btnView.Bottom + 12),
                Multiline = true,
                Parent = this,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                Size = new Size(ClientSize.Width - 20 - _tvVirtualPath.Right - 12, ClientSize.Height - 20 - btnView.Bottom - 12),
                WordWrap = false
            };
        }
        #endregion
    }
}

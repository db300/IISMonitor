using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deployer.Modules
{
    /// <summary>
    /// 应用程序设置
    /// </summary>
    public partial class AppSettingModule : UserControl
    {
        #region constructor
        public AppSettingModule()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region property
        private iHawkAppControl.iTreeView.AdSelectTreeView _tvVirtualPath;
        private TextBox _txtAppSetting;
        #endregion

        #region event handler
        private void BtnView_Click(object sender, EventArgs e)
        {
            View();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var virtualPath = _tvVirtualPath.SelectedNode?.Text;
            if (string.IsNullOrWhiteSpace(virtualPath)) return;
            string website = "";
            using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
            {
                var websites = websitesManager.GetWebsiteList();
                if (websites.Count == 0)
                {
                    _txtAppSetting.Text = "ERROR: 默认网站不存在";
                    return;
                }

                website = websites[0];
            }
            using (var webConfigManager = new iHawkIISLibrary.WebConfigManager())
            {
                var s = _txtAppSetting.Text.Trim();
                if (s.StartsWith("{"))
                {
                    var jsonFile = webConfigManager.GetAppSettingsJsonFileName(website, virtualPath);
                    using (var sw = new StreamWriter(jsonFile, false, Encoding.Default))
                    {
                        sw.Write(s);
                        sw.Flush();
                    }
                    iHawkAppLibrary.MessageBoxes.ShowInfo("修改完成");
                }
                else
                {
                    var dict = new Dictionary<string, string>();
                    foreach (var line in _txtAppSetting.Lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        var key = line.Substring(0, line.IndexOf(':'));
                        var value = line.Substring(line.IndexOf(':') + 1).Trim();
                        dict.Add(key, value);
                    }
                    s = webConfigManager.AddAppSettings(website, virtualPath, dict, true);
                    iHawkAppLibrary.MessageBoxes.ShowInfo(s);
                }
            }
        }

        private void TvVirtualPath_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == SystemColors.GrayText) e.Cancel = true;
        }

        private void TvVirtualPath_AfterSelect(object sender, TreeViewEventArgs e)
        {
            View(e.Node.Text);
        }
        #endregion

        #region method
        private void View()
        {
            _tvVirtualPath.Nodes.Clear();
            _txtAppSetting.ReadOnly = true;
            string website = "";
            List<string> virtualPathList = null;
            var sb = new StringBuilder();
            using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
            {
                var websites = websitesManager.GetWebsiteList();
                if (websites.Count == 0)
                {
                    _txtAppSetting.Text = "ERROR: 默认网站不存在";
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
                    var dict = webConfigManager.GetAppSettings(website, path);

                    var treeNode = new TreeNode(path);
                    _tvVirtualPath.Nodes.Add(treeNode);
                    sb.AppendLine("==============================");
                    sb.AppendLine(path);
                    if (dict == null)
                    {
                        var jsonFile = webConfigManager.GetAppSettingsJsonFileName(website, path);
                        if (jsonFile.StartsWith("fail"))
                        {
                            treeNode.ForeColor = SystemColors.GrayText;
                            sb.AppendLine($"暂时无法获取: {jsonFile}");
                        }
                        else
                        {
                            using (var sr = new StreamReader(jsonFile, Encoding.Default))
                            {
                                sb.Append(sr.ReadToEnd());
                            }
                        }
                    }
                    else
                    {
                        treeNode.Checked = true;
                        foreach (var item in dict) sb.AppendLine($"{item.Key}: {item.Value}");
                    }
                    sb.AppendLine("------------------------------");
                }
            }
            _txtAppSetting.Text = sb.ToString();
        }

        private void View(string virtualPath)
        {
            _txtAppSetting.Clear();
            _txtAppSetting.ReadOnly = false;
            string website = "";
            using (var websitesManager = new iHawkIISLibrary.WebsitesManager())
            {
                var websites = websitesManager.GetWebsiteList();
                if (websites.Count == 0)
                {
                    _txtAppSetting.Text = "ERROR: 默认网站不存在";
                    return;
                }

                website = websites[0];
            }
            using (var webConfigManager = new iHawkIISLibrary.WebConfigManager())
            {
                var dict = webConfigManager.GetAppSettings(website, virtualPath);
                if (dict == null)
                {
                    var jsonFile = webConfigManager.GetAppSettingsJsonFileName(website, virtualPath);
                    if (jsonFile.StartsWith("fail"))
                    {
                        _txtAppSetting.Text = $"ERROR: 暂时无法获取 {jsonFile}";
                    }
                    else
                    {
                        using (var sr = new StreamReader(jsonFile, Encoding.Default))
                        {
                            _txtAppSetting.Text = sr.ReadToEnd();
                        }
                    }
                }
                else
                {
                    foreach (var item in dict) _txtAppSetting.AppendText($"{item.Key}: {item.Value}\r\n");
                }
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
            _tvVirtualPath = new iHawkAppControl.iTreeView.AdSelectTreeView
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                CheckBoxes = false,
                Font = new Font(Font.FontFamily, 12),
                FullRowSelect = true,
                HideSelection = false,
                ItemHeight = 25,
                Location = new Point(btnView.Left, btnView.Bottom + 12),
                Parent = this,
                ShowLines = false,
                Size = new Size(300, ClientSize.Height - 20 - btnView.Bottom - 12)
            };
            _tvVirtualPath.BeforeSelect += TvVirtualPath_BeforeSelect;
            _tvVirtualPath.AfterSelect += TvVirtualPath_AfterSelect;
            _txtAppSetting = new TextBox
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
            var btnSave = new Button
            {
                AutoSize = true,
                Location = new Point(_txtAppSetting.Left, btnView.Top),
                Parent = this,
                Text = "保存修改"
            };
            btnSave.Click += BtnSave_Click;
        }
        #endregion
    }
}

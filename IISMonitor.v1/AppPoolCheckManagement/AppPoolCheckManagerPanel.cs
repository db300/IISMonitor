using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iHawkIISLibrary;
using Microsoft.Web.Administration;

namespace IISMonitor.AppPoolCheckManagement
{
    public partial class AppPoolCheckManagerPanel : UserControl
    {
        #region constructor

        public AppPoolCheckManagerPanel()
        {
            InitializeComponent();

            InitForm();
            InitContent();
        }

        #endregion

        #region property

        private readonly ApplicationPoolsManager _apm = new ApplicationPoolsManager();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion

        #region 界面初始化

        private void InitForm()
        {
            DoubleBuffered = true;
        }

        private void InitContent()
        {
            // view button & tree
            var btnView = new Button {Parent = this, Location = new Point(15, 15), Size = new Size(200, 25), Text = @"查看", Anchor = AnchorStyles.Left | AnchorStyles.Top};
            var tree = new iHawkAppControl.iTreeView.AdSelectTreeView
            {
                Parent = this,
                Location = new Point(15, btnView.Bottom + 5),
                ShowLines = false,
                FullRowSelect = true,
                HideSelection = false,
                ItemHeight = 25,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom
            };
            tree.Size = new Size(250, ClientSize.Height - 15 - tree.Top);
            btnView.Click += (sender, e) =>
            {
                tree.Nodes.Clear();
                foreach (var appPoolName in _apm.GetApplicationPoolList())
                {
                    tree.Nodes.Add(appPoolName, $"{appPoolName}: {_apm.GetApplicationPoolState(appPoolName)}");
                }
            };
            // temp monitor button and temp monitor log
            var btnMonitor = new Button {Parent = this, Location = new Point(btnView.Right + 10, 15), Size = new Size(100, 25), Text = @"监测", Anchor = AnchorStyles.Left | AnchorStyles.Top};
            var txtAppPool = new TextBox
            {
                Parent = this,
                Location = new Point(btnMonitor.Right + 10, 15),
                Size = new Size(100, 25),
                Text = @"ziyouAppPool",
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };
            var txtTimer = new TextBox {Parent = this, Location = new Point(txtAppPool.Right + 10, 15), Size = new Size(100, 25), Text = @"1000", Anchor = AnchorStyles.Left | AnchorStyles.Top};
            var txtLog = new TextBox
            {
                Parent = this,
                Location = new Point(tree.Right + 10, tree.Top),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };
            txtLog.Size = new Size(ClientSize.Width - 15 - txtLog.Left, ClientSize.Height - 15 - txtLog.Top);
            btnMonitor.Click += (sender, e) =>
            {
                var timer = new Timer {Interval = int.Parse(txtTimer.Text), Enabled = false};
                timer.Tick += (tt, ee) =>
                {
                    if (_apm.GetApplicationPoolState(txtAppPool.Text) == ObjectState.Started) return;
                    try
                    {
                        txtLog.AppendText($"【{DateTime.Now:yyyyMMddHHmmssfff}】{_apm.StartApplicationPool(txtAppPool.Text)}\r\n");
                    }
                    catch (Exception ex)
                    {
                        txtLog.AppendText($"【{DateTime.Now:yyyyMMddHHmmssfff}】{ex}\r\n");
                    }
                };
                timer.Enabled = true;
                btnMonitor.Enabled = false;
            };
        }

        #endregion
    }
}

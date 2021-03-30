using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IISMonitor.WebServiceCheckManagement
{
    public partial class WebServiceCheckManagerPanel : UserControl
    {
        #region constructor

        public WebServiceCheckManagerPanel()
        {
            InitializeComponent();

            InitForm();
            InitContent();
        }

        #endregion

        #region property

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion

        #region control

        private TextBox _txtLog;

        #endregion

        #region internal method

        private void PrintLog(string log)
        {
            if (_txtLog.Lines.Length > 50) _txtLog.Clear();
            _txtLog.AppendText($"【{DateTime.Now:yyyyMMddHHmmssfff}】{log}\r\n");
        }

        #endregion

        #region 界面初始化

        private void InitForm()
        {
            DoubleBuffered = true;
        }

        private void InitContent()
        {
            var btnMonitor = new Button
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Location = new Point(15, 15),
                Parent = this,
                Size = new Size(100, 25),
                Text = @"监测"
            };
            var txtCategory = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Parent = this,
                Text = @"Web Service",
                Width = 120
            };
            txtCategory.Location = new Point(btnMonitor.Right + 10, btnMonitor.Top + (btnMonitor.Height - txtCategory.Height) / 2);
            var txtCounter = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Location = new Point(txtCategory.Right + 10, txtCategory.Top),
                Parent = this,
                Text = @"Current Connections",
                Width = 120
            };
            var txtInstance = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Location = new Point(txtCounter.Right + 10, txtCounter.Top),
                Parent = this,
                Text = @"_Total",
                Width = 120
            };
            var txtMachine = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Location = new Point(txtInstance.Right + 10, txtInstance.Top),
                Parent = this,
                Text = @"localhost",
                Width = 120
            };
            _txtLog = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                Location = new Point(15, btnMonitor.Bottom + 5),
                Multiline = true,
                Parent = this,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            _txtLog.Size = new Size(ClientSize.Width - 30, ClientSize.Height - 15 - _txtLog.Top);
            btnMonitor.Click += (sender, e) =>
            {
                var performanceCounter = new PerformanceCounter(txtCategory.Text, txtCounter.Text, txtInstance.Text, txtMachine.Text);
                var timer = new Timer {Interval = 3000, Enabled = false};
                timer.Tick += (tt, ee) =>
                {
                    var value = (int) performanceCounter.NextValue();
                    PrintLog($"{value}");
                    if (value > 1000)
                    {
                        Logger.Warn($"{value}");
                        MainForm.MessageList.Add($"{value}");
                    }
                    else
                    {
                        Logger.Info($"{value}");
                    }
                };
                timer.Enabled = true;
                btnMonitor.Enabled = false;
                txtCategory.ReadOnly = true;
                txtCounter.ReadOnly = true;
                txtInstance.ReadOnly = true;
                txtMachine.ReadOnly = true;
            };
        }

        #endregion
    }
}

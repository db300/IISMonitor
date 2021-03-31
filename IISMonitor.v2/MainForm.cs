using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IISMonitor
{
    public partial class MainForm : Form
    {
        #region constructor

        public MainForm()
        {
            InitializeComponent();

            InitUi();
        }

        #endregion

        #region property

        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private FlowLayoutPanel _panel;
        private TextBox _txtLog4Opera;
        private TextBox _txtLog4Monitor;

        #endregion

        #region event handler

        private void BtnList_Click(object sender, EventArgs e)
        {
            ((Button) sender).Enabled = false;
            var poolList = AppSingleton.Apm.GetApplicationPoolList();
            poolList.Sort();
            foreach (var pool in poolList)
            {
                var appPoolPanel = new AppPoolPanel
                {
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                    Parent = _panel,
                    Width = 330
                };
                appPoolPanel.Notification += (ap, tt, ss) =>
                {
                    switch (tt)
                    {
                        case "Monitor":
                            _txtLog4Monitor.AppendText($"【{DateTime.Now}】{ss}\r\n");
                            break;
                        case "Opera":
                            _txtLog4Opera.AppendText($"【{DateTime.Now}】{ss}\r\n");
                            break;
                    }
                };
                appPoolPanel.Update(pool);
                _panel.Controls.Add(appPoolPanel);
            }
        }

        #endregion

        #region ui

        private void InitUi()
        {
            Text = @"IISMonitor";

            var panel0 = new Panel
            {
                Dock = DockStyle.Left,
                Parent = this,
                Width = 350
            };
            var btnList = new Button
            {
                Dock = DockStyle.Bottom,
                Parent = panel0,
                Text = @"刷新列表"
            };
            btnList.Click += BtnList_Click;
            _panel = new FlowLayoutPanel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(0),
                Parent = panel0
            };
            _panel.BringToFront();

            var panel1 = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                Parent = this
            };
            panel1.BringToFront();
            _txtLog4Opera = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Parent = panel1.Panel1,
                ReadOnly = true
            };
            _txtLog4Monitor = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Parent = panel1.Panel2,
                ReadOnly = true
            };
        }

        #endregion
    }
}

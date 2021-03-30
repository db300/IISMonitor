using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IISMonitor.AppPoolCheckManagement;
using IISMonitor.HttpCheckManagement;
using IISMonitor.ReleaseManagement;
using IISMonitor.WebServiceCheckManagement;

namespace IISMonitor
{
    public partial class MainForm : Form
    {
        #region constructor

        public MainForm()
        {
            InitializeComponent();

            InitForm();
            InitContent();
        }

        #endregion

        #region property

        public static readonly List<string> MessageList = new List<string>();

        #endregion

        #region 界面初始化

        private void InitForm()
        {
            DoubleBuffered = true;

            //窗口属性
            Size = new Size(750, 500);
            Text = @"IIS监测器";
        }

        private void InitContent()
        {
            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Parent = this,
                TabPages =
                {
                    new TabPage {Name = "tpHttpCheck", Text = @"Http Check", BorderStyle = BorderStyle.None},
                    new TabPage {Name = "tpAppPoolCheck", Text = @"Application Pool Check", BorderStyle = BorderStyle.None},
                    new TabPage {Name = "tpWebServiceCheck", Text = @"WebService Check", BorderStyle = BorderStyle.None},
                    new TabPage {Name = "tpReleaseManager", Text = @"Release Manager", BorderStyle = BorderStyle.None}
                }
            };
            tabControl.TabPages["tpHttpCheck"].Controls.Add(new HttpCheckManagerPanel {Dock = DockStyle.Fill, DockPadding = {All = 0}});
            tabControl.TabPages["tpAppPoolCheck"].Controls.Add(new AppPoolCheckManagerPanel {Dock = DockStyle.Fill, DockPadding = {All = 0}});
            tabControl.TabPages["tpWebServiceCheck"].Controls.Add(new WebServiceCheckManagerPanel {Dock = DockStyle.Fill, DockPadding = {All = 0}});
            tabControl.TabPages["tpReleaseManager"].Controls.Add(new ReleaseManagerPanel {Dock = DockStyle.Fill, DockPadding = {All = 0}});

            var timer = new Timer {Interval = 1000, Enabled = false};
            timer.Tick += (tt, ee) =>
            {
                while (MessageList.Count > 0)
                {
                    new WarnForm {Info = MessageList[0]}.Show();
                    MessageList.RemoveAt(0);
                }
            };
            timer.Enabled = true;
        }

        #endregion
    }
}

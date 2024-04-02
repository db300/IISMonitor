using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessProtector
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
        private TabControl _tabControl;
        #endregion

        #region event handler
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var processPanel = new ProcessPanel { Dock = DockStyle.Fill };
            processPanel.Notification += ProcessPanel_Notification;
            processPanel.Close += ProcessPanel_Close;
            var tabPage = new TabPage { Text = "进程" };
            tabPage.Controls.Add(processPanel);
            _tabControl.TabPages.Add(tabPage);
            _tabControl.SelectedTab = tabPage;
        }

        private void ProcessPanel_Notification(object sender, ProcessProtectItem item)
        {
            if (sender is ProcessPanel processPanel && processPanel.Parent is TabPage tabPage)
            {
                tabPage.Text = $"进程：{item.Name} - {item.Status}";
            }
        }

        private void ProcessPanel_Close(object sender)
        {
            if (sender is ProcessPanel processPanel && processPanel.Parent is TabPage tabPage)
            {
                processPanel.Dispose();
                tabPage.Dispose();
                //_tabControl.TabPages.Remove(tabPage);
            }
        }
        #endregion

        #region ui
        private void InitUi()
        {
            StartPosition = FormStartPosition.CenterScreen;
            Text = $"进程守护器 {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Parent = this
            };
            InitUi4Tool(panel);

            _tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Parent = this
            };
            _tabControl.BringToFront();
        }

        private void InitUi4Tool(Panel panel)
        {
            var btnAdd = new Button
            {
                AutoSize = true,
                Location = new Point(Config.ControlMargin, Config.ControlMargin),
                Parent = panel,
                Text = "添加"
            };
            btnAdd.Click += BtnAdd_Click;
        }
        #endregion
    }
}

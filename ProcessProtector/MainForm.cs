using System;
using System.Drawing;
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
        private const string Url4Readme = "https://www.yuque.com/lengda/eq8cm6/sfdxx2so95vy7wnv";
        private TabControl _tabControl;

        private string _exitPassword = "";
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

        private void BtnLock_Click(object sender, EventArgs e)
        {
            var txt = ((Button)sender).Text;
            switch (txt)
            {
                case "退出加锁":
                    {
                        var passwordForm = new PasswordForm { Text = "锁定" };
                        if (passwordForm.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(passwordForm.Password)) return;
                        _exitPassword = passwordForm.Password;
                        ((Button)sender).Text = "退出解锁";
                    }
                    break;
                case "退出解锁":
                    {
                        var passwordForm = new PasswordForm { Text = "解锁" };
                        if (passwordForm.ShowDialog(this) != DialogResult.OK) return;
                        if (_exitPassword == passwordForm.Password)
                        {
                            _exitPassword = "";
                            ((Button)sender).Text = "退出加锁";
                        }
                        else
                        {
                            MessageBox.Show("密码错误，解锁失败", "提示");
                        }
                    }
                    break;
            }
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

        private void Link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Url4Readme);
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var tipForm = new ExitTipForm();
            if (tipForm.ShowDialog() != DialogResult.OK) e.Cancel = true;
            else if (tipForm.MinToNotify)
            {
                e.Cancel = true;
                Hide();
            }
            else if (!string.IsNullOrWhiteSpace(_exitPassword))
            {
                MessageBox.Show("退出已加锁，请先解锁", "提示");
                e.Cancel = true;
            }
        }
        #endregion

        #region ui
        private void InitUi()
        {
            Icon = SystemIcons.Shield;
            StartPosition = FormStartPosition.CenterScreen;
            Text = $"进程守护器 {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
            FormClosing += MainForm_FormClosing;

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

            var notifyIcon = new NotifyIcon
            {
                Icon = Icon,
                Text = Text,
                Visible = true
            };
            notifyIcon.Click += NotifyIcon_Click;

            /*
            var contextMenuStrip = new ContextMenuStrip();
            var item1 = new ToolStripMenuItem("设置退出密码...");
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { item1 });
            item1.Click += NotifyIconMenuItem1_Click;
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            */
        }

        private void InitUi4Tool(Panel panel)
        {
            var btnAdd = new Button
            {
                AutoSize = true,
                Parent = panel,
                Text = "添加"
            };
            btnAdd.Location = new Point(Config.ControlMargin, (panel.ClientSize.Height - btnAdd.Height) / 2);
            btnAdd.Click += BtnAdd_Click;

            var link = new LinkLabel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Parent = panel,
                Text = "点击查看使用说明"
            };
            link.Location = new Point(panel.ClientSize.Width - Config.ControlMargin - link.Width, (panel.ClientSize.Height - link.Height) / 2);
            link.Click += Link_Click;

            var btnLock = new Button
            {
                AutoSize = true,
                Parent = panel,
                Text = "退出加锁"
            };
            btnLock.Location = new Point(link.Left - Config.ControlPadding - btnLock.Width, (panel.ClientSize.Height - btnLock.Height) / 2);
            btnLock.Click += BtnLock_Click;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessProtector
{
    public partial class ProcessPanel : UserControl
    {
        #region constructor
        public ProcessPanel()
        {
            InitializeComponent();

            InitUi();

            Load += ProcessPanel_Load;
        }
        #endregion

        #region property
        private ComboBox _cmbProcess;
        private Button _btnStart;
        private Button _btnStop;
        private TextBox _txtLog;

        private readonly Timer _timer4Protect = new Timer { Interval = 1000, Enabled = false };
        private readonly ProcessProtectItem _processProtectItem = new ProcessProtectItem();
        #endregion

        #region event handler
        private void ProcessPanel_Load(object sender, EventArgs e)
        {
            var timer = new Timer { Enabled = true, Interval = 10 };
            timer.Tick += (tt, ee) =>
            {
                timer.Enabled = false;
                _cmbProcess.Items.Clear();
                _cmbProcess.Items.AddRange(Process.GetProcesses().Select(a => a.ProcessName).ToArray());
                _cmbProcess.Sorted = true;
            };

            _timer4Protect.Tick += Timer4Protect_Tick;
        }

        private void Timer4Protect_Tick(object sender, EventArgs e)
        {
            // 检查进程是否存在
            var isProcessRunning = Process.GetProcesses().Any(p => p.ProcessName == _processProtectItem.Name);
            // 如果进程不存在，启动它
            if (!isProcessRunning)
            {
                _txtLog.AppendText($"【{DateTime.Now}】进程【{_processProtectItem.Name}】不存在，启动进程\r\n");
                Process.Start(_processProtectItem.Path);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_processProtectItem.Name)) return;
            ((Button)sender).Enabled = false;
            _cmbProcess.Enabled = false;
            _timer4Protect.Enabled = true;
            _btnStop.Enabled = true;
            _processProtectItem.Status = "已启动";
            Notification?.Invoke(this, _processProtectItem);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            _cmbProcess.Enabled = true;
            _timer4Protect.Enabled = false;
            _btnStart.Enabled = true;
            _processProtectItem.Status = "已停止";
            Notification?.Invoke(this, _processProtectItem);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            _timer4Protect.Enabled = false;
            Close?.Invoke(this);
        }
        #endregion

        #region ui
        private void InitUi()
        {
            _cmbProcess = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Parent = this
            };
            _cmbProcess.SelectedIndexChanged += (sender, e) =>
            {
                try
                {
                    _processProtectItem.Name = _cmbProcess.Text;
                    _processProtectItem.Path = Process.GetProcessesByName(_processProtectItem.Name).FirstOrDefault()?.MainModule.FileName;
                    Notification?.Invoke(this, _processProtectItem);
                    _btnStart.Enabled = true;
                }
                catch (Exception ex)
                {
                    _txtLog.AppendText($"{_cmbProcess.Text} {ex.Message}\r\n");
                    _btnStart.Enabled = false;
                }
            };
            _btnStart = new Button
            {
                AutoSize = true,
                Enabled = false,
                Location = new Point(Config.ControlMargin, _cmbProcess.Bottom + Config.ControlPadding),
                Parent = this,
                Text = "启动"
            };
            _btnStart.Click += BtnStart_Click;
            _btnStop = new Button
            {
                AutoSize = true,
                Enabled = false,
                Location = new Point(_btnStart.Right + Config.ControlPadding, _btnStart.Top),
                Parent = this,
                Text = "停止"
            };
            _btnStop.Click += BtnStop_Click;

            var btnClose = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Parent = this,
                Text = "关闭"
            };
            btnClose.Location = new Point(ClientSize.Width - Config.ControlMargin - btnClose.Width, _btnStart.Top);
            btnClose.Click += BtnClose_Click;

            _txtLog = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                Location = new Point(Config.ControlMargin, _btnStart.Bottom + Config.ControlPadding),
                Multiline = true,
                Parent = this,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                Size = new Size(ClientSize.Width - 2 * Config.ControlMargin, ClientSize.Height - _btnStart.Bottom - Config.ControlPadding - Config.ControlMargin)
            };
        }
        #endregion

        #region custom event
        internal delegate void ProcessProtectEventHandler(object sender, ProcessProtectItem item);
        internal event ProcessProtectEventHandler Notification;

        internal delegate void ProcessProtectCloseEventHandler(object sender);
        internal event ProcessProtectCloseEventHandler Close;
        #endregion
    }
}

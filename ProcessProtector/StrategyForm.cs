using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessProtector
{
    /// <summary>
    /// 策略设置窗口
    /// </summary>
    public partial class StrategyForm : Form
    {
        #region constructor
        public StrategyForm()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region property
        private RadioButton _rbImmediateRestart;
        private RadioButton _rbDelayedRestart;
        private NumericUpDown _numDelaySeconds;
        private RadioButton _rbExecuteScript;
        private TextBox _txtScriptFileName;

        internal StrategyItem StrategyItem
        {
            get
            {
                var item = new StrategyItem();
                if (_rbImmediateRestart.Checked) item.Method = 0;
                else if (_rbDelayedRestart.Checked) item.Method = 1;
                else if (_rbExecuteScript.Checked) item.Method = 2;
                item.DelaySeconds = (int)_numDelaySeconds.Value;
                item.ScriptFileName = _txtScriptFileName.Text.Trim();
                return item;
            }
            set
            {
                switch (value.Method)
                {
                    case 0:
                        _rbImmediateRestart.Checked = true;
                        break;
                    case 1:
                        _rbDelayedRestart.Checked = true;
                        break;
                    case 2:
                        _rbExecuteScript.Checked = true;
                        break;
                }
                _numDelaySeconds.Value = value.DelaySeconds;
                _txtScriptFileName.Text = value.ScriptFileName;
            }
        }
        #endregion

        #region event handler
        private void TxtScriptFileName_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "脚本文件|*.bat;*.cmd|所有文件|*.*",
                Title = "选择脚本文件"
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                _txtScriptFileName.Text = ofd.FileName;
            }
        }
        #endregion

        #region ui
        private void InitUi()
        {
            ClientSize = new Size(400, 180);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "设置守护策略";

            _rbImmediateRestart = new RadioButton
            {
                AutoSize = true,
                Checked = true,
                Location = new Point(20, 20),
                Parent = this,
                Text = "立即重启",
            };
            _rbDelayedRestart = new RadioButton
            {
                AutoSize = true,
                Location = new Point(20, _rbImmediateRestart.Bottom + 12),
                Parent = this,
                Text = "延迟重启"
            };
            _numDelaySeconds = new NumericUpDown
            {
                AutoSize = true,
                Location = new Point(_rbDelayedRestart.Right + 12, _rbDelayedRestart.Top),
                Maximum = 3600,
                Minimum = 1,
                Parent = this,
                TextAlign = HorizontalAlignment.Right,
                Value = 1,
                Width = 80
            };
            var lbl = new Label
            {
                AutoSize = true,
                Location = new Point(_numDelaySeconds.Right + Config.ControlPadding, _rbDelayedRestart.Top + 3),
                Parent = this,
                Text = "秒"
            };
            _rbExecuteScript = new RadioButton
            {
                AutoSize = true,
                Location = new Point(20, _rbDelayedRestart.Bottom + 12),
                Parent = this,
                Text = "执行脚本"
            };
            _txtScriptFileName = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(_rbExecuteScript.Right + 12, _rbExecuteScript.Top),
                Parent = this,
                ReadOnly = true,
                Width = ClientSize.Width - 20 - _rbExecuteScript.Right - 12
            };
            _txtScriptFileName.Click += TxtScriptFileName_Click;

            var btnOk = new Button
            {
                AutoSize = true,
                DialogResult = DialogResult.OK,
                Parent = this,
                Text = "确定"
            };
            btnOk.Location = new Point(ClientSize.Width - 20 - btnOk.Width, ClientSize.Height - 20 - btnOk.Height);
        }
        #endregion
    }
}

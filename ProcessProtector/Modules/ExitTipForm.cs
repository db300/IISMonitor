using System.Drawing;
using System.Windows.Forms;

namespace ProcessProtector.Modules
{
    /// <summary>
    /// 退出提示窗口
    /// </summary>
    public partial class ExitTipForm : Form
    {
        #region constructor
        public ExitTipForm()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region property
        private RadioButton _rbMinToNotify;
        private RadioButton _rbExit;

        public bool MinToNotify => _rbMinToNotify.Checked;
        #endregion

        #region ui
        private void InitUi()
        {
            ClientSize = new Size(200, 120);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "提示";

            _rbMinToNotify = new RadioButton
            {
                AutoSize = true,
                Checked = true,
                Location = new Point(20, 20),
                Parent = this,
                Text = "最小化到系统托盘",
            };
            _rbExit = new RadioButton
            {
                AutoSize = true,
                Location = new Point(20, _rbMinToNotify.Bottom + 12),
                Parent = this,
                Text = "直接退出"
            };

            var btnOk = new Button
            {
                AutoSize = true,
                DialogResult = DialogResult.OK,
                Parent = this,
                Text = "确定",
            };
            btnOk.Location = new Point(ClientSize.Width - 20 - btnOk.Width, ClientSize.Height - 20 - btnOk.Height);
        }
        #endregion
    }
}

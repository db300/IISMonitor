using System.Drawing;
using System.Windows.Forms;

namespace ProcessProtector.Modules
{
    /// <summary>
    /// 密码窗口
    /// </summary>
    public partial class PasswordForm : Form
    {
        #region constructor
        public PasswordForm()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region property
        private TextBox _txtPassord;

        public string Password => _txtPassord.Text.Trim();
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
            Text = "密码";

            _txtPassord = new TextBox
            {
                Location = new Point(20, 30),
                Parent = this,
                Width = ClientSize.Width - 2 * 20,
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

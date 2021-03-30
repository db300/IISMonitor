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
    public partial class WarnForm : Form
    {
        #region constructor

        public WarnForm()
        {
            InitializeComponent();

            InitForm();
            InitContent();

            Load += (sender, e) => { Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Size.Width, Screen.PrimaryScreen.WorkingArea.Height - Size.Height); };
        }

        #endregion

        #region property

        public string Info
        {
            set
            {
                if (_txtInfo != null) _txtInfo.Text = value;
            }
        }

        #endregion

        #region control

        private TextBox _txtInfo;

        #endregion

        #region 界面初始化

        private void InitForm()
        {
            DoubleBuffered = true;

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Size = new Size(300, 200);
            Text = @"IISMonitorWarn";
            TopMost = true;
        }

        private void InitContent()
        {
            _txtInfo = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                BorderStyle = BorderStyle.None,
                Location = new Point(10, 10),
                Multiline = true,
                Parent = this,
                ReadOnly = true,
                Size = new Size(ClientSize.Width - 20, ClientSize.Height - 20),
                WordWrap = true
            };
        }

        #endregion
    }
}

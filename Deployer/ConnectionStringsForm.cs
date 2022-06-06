using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deployer
{
    public partial class ConnectionStringsForm : Form
    {
        #region constructor
        public ConnectionStringsForm()
        {
            InitializeComponent();

            InitUi();
        }
        #endregion

        #region property
        private iHawkAppControl.iDataGridView.AdDataGridView _dgConnectionString;

        public Dictionary<string, string> ConnectionStringDict
        {
            get
            {
                var dict = new Dictionary<string, string>();
                foreach (DataGridViewRow row in _dgConnectionString.Rows)
                {
                    var name = row.Cells["name"].Value;
                    var connectionString = row.Cells["connectionString"].Value;
                    if (name != null && connectionString != null) dict.Add(name.ToString().Trim(), connectionString.ToString().Trim());
                }
                return dict;
            }
        }
        #endregion

        #region ui
        private void InitUi()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "数据库连接串";

            var btnOk = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                AutoSize = true,
                Parent = this,
                Text = "确定"
            };
            btnOk.Location = new Point(ClientSize.Width - 20 - btnOk.Width, ClientSize.Height - 20 - btnOk.Height);
            btnOk.Click += (sender, e) => { DialogResult = DialogResult.OK; };

            _dgConnectionString = new iHawkAppControl.iDataGridView.AdDataGridView
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                IndexVisible = false,
                Location = new Point(20, 20),
                Parent = this,
                Size = new Size(ClientSize.Width - 40, btnOk.Top - 12 - 20)
            };
            _dgConnectionString.Headers = new Dictionary<string, string> { { "name", "name" }, { "connectionString", "connectionString" } };
        }
        #endregion
    }
}

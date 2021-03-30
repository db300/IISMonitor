using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IISMonitor.HttpCheckManagement
{
    public partial class HttpCheckManagerPanel : UserControl
    {
        #region constructor

        public HttpCheckManagerPanel()
        {
            InitializeComponent();

            InitForm();
            InitContent();
        }

        #endregion

        #region property

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion

        #region control

        private TextBox _txtLog;

        #endregion

        #region internal method

        private void PrintLog(string log)
        {
            if (_txtLog.Lines.Length > 50) _txtLog.Clear();
            _txtLog.AppendText($"【{DateTime.Now:yyyyMMddHHmmssfff}】{log}\r\n");
        }

        #endregion

        #region 界面初始化

        private void InitForm()
        {
            DoubleBuffered = true;
        }

        private void InitContent()
        {
            var btnMonitor = new Button
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Location = new Point(15, 15),
                Parent = this,
                Size = new Size(100, 25),
                Text = @"监测"
            };
            var txtUrl = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                Parent = this,
                Text = @"http://www.ziyou.studio/ziyou/testmanagement/testmanagerhandler.ashx",
                Width = ClientSize.Width - btnMonitor.Right - 10 - 15
            };
            txtUrl.Location = new Point(btnMonitor.Right + 10, btnMonitor.Top + (btnMonitor.Height - txtUrl.Height) / 2);
            _txtLog = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                Location = new Point(15, btnMonitor.Bottom + 5),
                Multiline = true,
                Parent = this,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            _txtLog.Size = new Size(ClientSize.Width - 30, ClientSize.Height - 15 - _txtLog.Top);
            btnMonitor.Click += (sender, e) =>
            {
                var timer = new Timer {Interval = 3000, Enabled = false};
                timer.Tick += (tt, ee) =>
                {
                    var dt = DateTime.Now;
                    var request = WebRequest.CreateHttp(txtUrl.Text);
                    request.Method = WebRequestMethods.Http.Head;
                    try
                    {
                        using (var response = request.GetResponse() as HttpWebResponse)
                        {
                            var diff = (DateTime.Now - dt).Milliseconds;
                            PrintLog($"{response?.StatusCode}, {diff}ms");
                            if (diff > 1000)
                            {
                                Logger.Warn($"{response?.StatusCode}, {diff}ms");
                                MainForm.MessageList.Add($"{response?.StatusCode}, {diff}ms");
                            }
                            else
                            {
                                Logger.Info($"{response?.StatusCode}, {diff}ms");
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        PrintLog(ex.Message);
                        Logger.Error(ex.Message);
                        MainForm.MessageList.Add(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        PrintLog(ex.Message);
                        Logger.Error(ex.Message);
                        MainForm.MessageList.Add(ex.Message);
                    }
                };
                timer.Enabled = true;
                btnMonitor.Enabled = false;
                txtUrl.ReadOnly = true;
            };
        }

        #endregion
    }
}

using System;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace IISMonitor
{
    static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Logger.Info("IISMonitor 启动");

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "应用程序发生致命错误");
                MessageBox.Show($"应用程序发生致命错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Logger.Info("IISMonitor 退出");
                LogManager.Shutdown();
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.Error(e.Exception, "UI线程未处理异常");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal(e.ExceptionObject as Exception, "应用程序域未处理异常");
        }
    }
}

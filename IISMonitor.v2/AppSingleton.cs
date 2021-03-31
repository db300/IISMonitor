using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISMonitor
{
    internal static class AppSingleton
    {
        internal static readonly iHawkIISLibrary.ApplicationPoolsManager Apm = new iHawkIISLibrary.ApplicationPoolsManager();
    }
}

using System;
using System.Diagnostics;
using System.IO;

namespace WindowsServiceUtil
{
    internal static class NetHelper
    {
        private static string NetFilename = Path.Combine(Environment.SystemDirectory, "net.exe");

        internal static bool StopService(string serviceName, int timeout)
        {
            // send net stop servicename
            Process process = Process.Start(NetFilename, $"stop {serviceName}");

            if (process.WaitForExit(timeout))
            {
                // process stopped, return true if service was actually stopped
                return process.ExitCode == 0;
            }
            else
            {
                // exeeded timelimit, thus return false
                return false;
            }
        }
    }
}

namespace WindowsServiceUtil
{
    using System;
    using System.Diagnostics;
    using System.IO;

    internal static class NetHelper
    {
        private static string NetFilename = Path.Combine(Environment.SystemDirectory, "net.exe");

        internal static bool StopService(string serviceName, int timeout)
        {
            // use sc.exe to request service info
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                FileName = NetFilename,
                Arguments = $"stop {serviceName}"
            };
            Process process = Process.Start(startInfo);

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

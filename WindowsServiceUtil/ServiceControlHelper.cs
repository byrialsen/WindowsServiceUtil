namespace WindowsServiceUtil
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Service Control (SC) helper class
    /// Uses c:\Windows\System32\sc.exe file to 
    /// </summary>
    internal static class ServiceControlHelper
    {
        private static string ServiceControlFilename = Path.Combine(Environment.SystemDirectory, "sc.exe");

        /// <summary>
        /// Query extended Windows Service information
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        internal static Tuple<int, int> QueryServiceInfo(string serviceName)
        {
            // init
            int state = -1;
            int pid = 0;

            // use sc.exe to request service info
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = ServiceControlFilename,
                Arguments = $"queryex {serviceName}"
            };

            Process process = Process.Start(startInfo);

            // Do not wait for the process to exit before
            // reading to the end of its redirected stream.
            // process.WaitForExit();
            // Read the output stream first and then wait.
            string[] lines = process.StandardOutput
                .ReadToEnd()
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                var keyvalue = line.Split(new char[] { ':' }, 2);

                if (keyvalue.Length == 2)
                {
                    string key = keyvalue[0].Trim().ToLower();
                    string value = keyvalue[1].Trim();

                    if (key.CompareTo("state") == 0)
                    {
                        if (!int.TryParse(value.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries)[0], out state))
                        {
                            state = -1;
                        }
                    }

                    if (key.CompareTo("pid") == 0)
                    {
                        if (!int.TryParse(value, out pid))
                        {
                            pid = 0;
                        }
                    }
                }
            }

            process.WaitForExit();

            return new Tuple<int, int>(pid, state);
        }
    }
}

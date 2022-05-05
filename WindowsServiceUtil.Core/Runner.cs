
namespace WindowsServiceUtil.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;

    public static class Runner
    {
        public static int Execute(string[] args)
        {
            // init
            args = args ?? new string[0];
            int exitcode = 0;

            if (args.Length < 1)
            {
                throw new ArgumentException("Arguments can not be empty");
            }

            // handle command
            string command = args[0].ToLower().Trim();
            List<string> commandArgs = args.ToList();
            commandArgs.RemoveAt(0);

            switch (command)
            {
                case "start":
                    exitcode = ExecuteStart(args);
                    break;

                case "stop":
                    exitcode = ExecuteStop(args);
                    break;

                default:
                    exitcode = ExitCodes.UnknownCommand;
                    break;
            }

            return exitcode;
        }

        private static int ExecuteStop(string[] args)
        {
            //var myservice = ServiceController.GetServices()
            //    .FirstOrDefault(x => string.Compare(x.ServiceName, "IsWindowsService", StringComparison.OrdinalIgnoreCase) == 0);
            /*

            var sc = Path.Combine(Environment.SystemDirectory, "sc.exe");
            string name = "ISWindowsService";
            string path = @"C:\Program Files (x86)\ISWindowService\InnoSetupWindowsService.exe";

            var services = Process.GetProcesses();
            var service = Process.GetProcessesByName(name);

            var process = Process.Start(sc, $"query {name}");
            process.WaitForExit();
            */

            return 0;
        }

        private static int ExecuteStart(string[] args)
        {
            return 0;
        }
    }
}

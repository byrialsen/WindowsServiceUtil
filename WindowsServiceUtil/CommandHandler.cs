namespace WindowsServiceUtil
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Command line handler
    /// </summary>
    // TODO: Maybe refactor to use https://www.youtube.com/watch?v=kM0pWAwo_FQ&t=894s
    internal static class CommandHandler
    {
        internal static int Execute(string[] args)
        {
            // init
            int exitcode = 0;
            args = args ?? new string[0];

            // check number of parameters 
            if (args.Length < 1)
            {
                return ExitCodes.MissingParameters;
            }

            // determine command and arguments and run
            try
            {
                // parse arguments
                Arguments arguments = Arguments.Parse(args);

                switch (arguments.Command)
                {
                    // handle stop and wait
                    case Command.Stop:
                        exitcode = StopAndWait(arguments.ServiceName, arguments.Timeout);
                        break;

                    // unknow
                    case Command.Unknown:
                        exitcode = ExitCodes.UnknownCommand;
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                exitcode = ExitCodes.MissingParameters;
            }
            catch (Exception)
            {
                // unknown failure
                exitcode = ExitCodes.FailedUnknown;
            }

            return exitcode;
        }

        /// <summary>
        /// Stop service if it is running and wait for windows service process (files) to be released
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static int StopAndWait(string serviceName, int timeout)
        {
            var scRes = ServiceControlHelper.QueryServiceInfo(serviceName);
            int pid = scRes.Item1;
            int state = scRes.Item2;

            // stop service if running
            if (state == ServiceStatus.Running)
            {
                if (!NetHelper.StopService(serviceName, timeout))
                {
                    // service stop failed
                    return ExitCodes.ServiceStopFailed;
                }
            }

            // wait for service process to exit if process is running (pid <>  0).
            if (pid > 0)
            {
                Process.GetProcessById(pid)?.WaitForExit(timeout);
            }

            // we succeeded but return exitcode in respect to restart
            return state == ServiceStatus.Running
                ? ExitCodes.SuccessNeedsRestart
                : ExitCodes.Success;
        }
    }
}

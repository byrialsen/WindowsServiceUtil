namespace WindowsServiceUtil
{
    using System;
    
    /// <summary>
    /// Windows Service Util main class
    /// </summary>
    internal class Program
    {

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Examples:
            // WindowsServiceUtil Stop name=ISWindowsService timeout=30000
            // WindowsServiceUtil Stop "name=IS Windows Service" timeout=30000

            int exitCode = CommandHandler.Execute(args);

            Environment.Exit(exitCode);
        }
    }
}

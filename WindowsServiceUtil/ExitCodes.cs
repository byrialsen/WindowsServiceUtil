using System;

namespace WindowsServiceUtil
{
    /// <summary>
    /// <see cref="ExitCodes"/>
    /// </summary>
    internal static class ExitCodes
    {
        /// <summary>
        /// Failed with unknown error
        /// </summary>
        internal const int FailedUnknown = -1;

        /// <summary>
        /// Successfully 
        /// </summary>
        internal const int Success = 0;

        /// <summary>
        /// Sucessfully and was initially running
        /// </summary>
        internal const int SuccessNeedsRestart = 1;

        /// <summary>
        /// Command is unknown error
        /// </summary>
        internal const int UnknownCommand = 2;

        /// <summary>
        /// Missing parameters error
        /// </summary>
        internal const int MissingParameters = 3;

        /// <summary>
        /// Stopping Windows Service failed
        /// </summary>
        internal const int ServiceStopFailed = 4;
    }
}

namespace WindowsServiceUtil
{
    /// <summary>
    /// Windows Service Status's
    /// </summary>
    internal static class ServiceStatus
    {
        /// <summary>
        /// The service is not running. This corresponds to the Win32 SERVICE_STOPPED constant, which is defined as 0x00000001.
        /// </summary>
        internal const int Stopped = 1;

        /// <summary>
        /// The service is starting. This corresponds to the Win32 SERVICE_START_PENDING constant, which is defined as 0x00000002.
        /// </summary>
        internal const int Startpending = 2;

        /// <summary>
        /// The service is stopping. This corresponds to the Win32 SERVICE_STOP_PENDING constant, which is defined as 0x00000003.
        /// </summary>
        internal const int StopPending = 3;

        /// <summary>
        /// The service is running. This corresponds to the Win32 SERVICE_RUNNING constant, which is defined as 0x00000004.
        /// </summary>
        internal const int Running = 4;

        /// <summary>
        /// The service continue is pending. This corresponds to the Win32 SERVICE_CONTINUE_PENDING constant, which is defined as 0x00000005
        /// </summary>
        internal const int ContinuePending = 5;

        /// <summary>
        /// The service pause is pending. This corresponds to the Win32 SERVICE_PAUSE_PENDING constant, which is defined as 0x00000006.
        /// </summary>
        internal const int PausePending = 6;

        /// <summary>
        /// The service is paused. This corresponds to the Win32 SERVICE_PAUSED constant, which is defined as 0x00000007.
        /// </summary>
        internal const int Paused = 7;
    }
}

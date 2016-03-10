namespace SvgSoft.WinService.Constants
{
    public enum ServiceType
    {
        /// <summary>
        /// Reserved.
        /// </summary>
        SERVICE_ADAPTER = 0x00000004, 
        /// <summary>
        /// File system driver service.
        /// </summary>
        SERVICE_FILE_SYSTEM_DRIVER = 0x00000002, 
        /// <summary>
        /// Driver service.
        /// </summary>
        SERVICE_KERNEL_DRIVER = 0x00000001, 
        /// <summary>
        /// Reserved.
        /// </summary>
        SERVICE_RECOGNIZER_DRIVER = 0x00000008, 
        /// <summary>
        /// Service that runs in its own process.
        /// </summary>
        SERVICE_WIN32_OWN_PROCESS = 0x00000010, 
        /// <summary>
        /// Service that shares a process with one or more other services. For more information, see Service Programs.
        /// <see cref="https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms685967(v=vs.85).aspx"/>
        /// </summary>
        SERVICE_WIN32_SHARE_PROCESS = 0x00000020, 
        /// <summary>
        /// The service can interact with the desktop. 
        /// (If you specify either SERVICE_WIN32_OWN_PROCESS or SERVICE_WIN32_SHARE_PROCESS, and the service is running in the context of the LocalSystem account)
        /// For more information, see Interactive Services.
        /// <see cref="https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms683502(v=vs.85).aspx"/>
        /// </summary>
        SERVICE_INTERACTIVE_PROCESS = 0x00000100
    }

}

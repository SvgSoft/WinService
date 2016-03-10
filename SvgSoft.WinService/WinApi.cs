using SvgSoft.WinService.Constants;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SvgSoft.WinService
{
    class WinApi
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms684323%28v=vs.85%29.aspx
        public static extern IntPtr OpenSCManager(string lpMachineName, string lpSCDB, int scParameter);
        public static IntPtr OpenSCManager(SCManagerAccess access) { return OpenSCManager(null, access); }
        public static IntPtr OpenSCManager(string computerName, SCManagerAccess access)
        {
            return OpenSCManager(computerName, null, (int)access);
        }

        [DllImport("Advapi32.dll", SetLastError=true)]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms682450%28v=vs.85%29.aspx
        public static extern IntPtr CreateService(IntPtr SC_HANDLE, string lpSvcName, string lpDisplayName,
            int dwDesiredAccess, int dwServiceType, int dwStartType, int dwErrorControl, string lpPathName,
            string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);

        [DllImport("advapi32.dll")]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms682028%28v=vs.85%29.aspx
        public static extern void CloseServiceHandle(IntPtr SCHANDLE);

        [DllImport("advapi32.dll", SetLastError = true)]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms686321%28v=vs.85%29.aspx
        public static extern int StartService(IntPtr SVHANDLE, int dwNumServiceArgs, string lpServiceArgVectors);

        [DllImport("advapi32.dll", SetLastError = true)]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms684330%28v=vs.85%29.aspx
        public static extern IntPtr OpenService(IntPtr SCHANDLE, string lpSvcName, int dwNumServiceArgs);

        [DllImport("advapi32.dll", SetLastError = true)]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms682562%28v=vs.85%29.aspx
        public static extern int DeleteService(IntPtr SVHANDLE);

        [DllImport("kernel32.dll")]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms679360%28v=vs.85%29.aspx
        public static extern int GetLastError();

        [DllImport("advapi32.dll", SetLastError = true)]
        // https://msdn.microsoft.com/ru-ru/library/windows/desktop/ms681988%28v=vs.85%29.aspx
        public static extern bool ChangeServiceConfig2(IntPtr ScHandle, int dwInfoLevel, IntPtr lpInfo);

        public static bool Success(IntPtr result, out string error)
        {
            return Success(result.ToInt32(), out error);
        }
        public static bool Success(int result, out string error)
        {
            if (result == 0)
            {
                error = (new Win32Exception(Marshal.GetLastWin32Error())).Message;
                return false;
            }
            else
            {
                error = null;
                return true;
            }
        }
    }
}

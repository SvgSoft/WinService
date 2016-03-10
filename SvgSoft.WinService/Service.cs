using System;
using System.Collections.Generic;
using SvgSoft.WinService.Constants;
using SvgSoft.WinService.Accounts;
using ServiceType = SvgSoft.WinService.Constants.ServiceType;
using System.ServiceProcess;

namespace SvgSoft.WinService
{
    public class Service
    {
        #region Install
        public static bool Install(string servicePath, string serviceName, ServiceAccounts account, string serviceDisplayName = null, bool uninstallIfExist = false)
        {
            string err;
            return Install(servicePath, serviceName, account, out err, serviceDisplayName: serviceDisplayName, uninstallIfExist: uninstallIfExist);
        }
        public static bool Install(string servicePath, string serviceName, ServiceAccounts account, out string error, string serviceDisplayName = null, bool uninstallIfExist = false)
        {
            return Install(servicePath, serviceName, out error, serviceDisplayName: serviceDisplayName, account: account.GetStringValue(), password: null, uninstallIfExist: uninstallIfExist);
        }
        public static bool Install(string servicePath, string serviceName, string serviceDisplayName = null, string account = null, string password = null, bool uninstallIfExist = false)
        {
            string err;
            return Install(servicePath, serviceName, out err, serviceDisplayName: serviceDisplayName, account: account, password: password, uninstallIfExist: uninstallIfExist);
        }
        public static bool Install(string servicePath, string serviceName, out string error, string serviceDisplayName = null, string account = null, string password = null, bool uninstallIfExist = false)
        {
            if (uninstallIfExist && Exist(serviceName)) Uninstall(serviceName, out error);

            IntPtr scHandle = IntPtr.Zero;
            IntPtr serviceHandle = IntPtr.Zero;
            error = null;
            bool result = false;
            try
            {
                scHandle = WinApi.OpenSCManager(SCManagerAccess.SC_MANAGER_CREATE_SERVICE);
                if (WinApi.Success(scHandle, out error))
                {
                    serviceHandle = WinApi.CreateService(scHandle, serviceName, serviceDisplayName, (int)ServiceAccess.SERVICE_ALL_ACCESS,
                        (int)ServiceType.SERVICE_WIN32_OWN_PROCESS, (int)ServiceStartType.SERVICE_AUTO_START,
                        (int)ServiceErrorControl.SERVICE_ERROR_NORMAL, servicePath, null, 0, null, account, password);
                    result = WinApi.Success(serviceHandle, out error);
                }
            }
            finally
            {
                try
                {
                    if (serviceHandle != IntPtr.Zero) WinApi.CloseServiceHandle(serviceHandle);
                    if (scHandle != IntPtr.Zero) WinApi.CloseServiceHandle(scHandle);
                }
                catch { }
            }
            return result;
        }
        #endregion

        #region Uninstall
        public static bool Uninstall(string serviceName)
        {
            string err; return Uninstall(serviceName, out err);
        }
        public static bool Uninstall(string serviceName, out string error)
        {
            IntPtr scHandle = IntPtr.Zero;
            IntPtr serviceHandle = IntPtr.Zero;
            try
            {
                scHandle = WinApi.OpenSCManager(SCManagerAccess.GENERIC_WRITE);
                if (!WinApi.Success(scHandle, out error)) return false;
                serviceHandle = WinApi.OpenService(scHandle, serviceName, (int)ServiceAccess.DELETE);
                if (!WinApi.Success(serviceHandle, out error)) return false;
                int result = WinApi.DeleteService(serviceHandle);
                return WinApi.Success(result, out error);
            }
            finally
            {
                try
                {
                    if (serviceHandle != IntPtr.Zero) WinApi.CloseServiceHandle(serviceHandle);
                    if (scHandle != IntPtr.Zero) WinApi.CloseServiceHandle(scHandle);
                }
                catch { }
            }
        }
        #endregion

        #region Start
        public static bool Start(string serviceName)
        {
            string err;
            return Start(serviceName, out err);
        }
        public static bool Start(string serviceName, out string error)
        {
            error = null;
            try
            {
                changeServiceStatus(serviceName, System.ServiceProcess.ServiceControllerStatus.Running);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        #endregion

        #region Stop
        public static bool Stop(string serviceName) { string err; return Stop(serviceName, out err); }
        public static bool Stop(string serviceName, out string error)
        {
            error = null;
            try
            {
                changeServiceStatus(serviceName, System.ServiceProcess.ServiceControllerStatus.Stopped);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        #endregion

        public static bool Exist(string serviceName)
        {
            foreach (System.ServiceProcess.ServiceController sc in System.ServiceProcess.ServiceController.GetServices())
            {
                if (sc.ServiceName.Equals(serviceName, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static bool IsRunning(string serviceName)
        {
            using (System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController(serviceName))
            {
                if (sc == null) return false;
                return sc.Status == System.ServiceProcess.ServiceControllerStatus.Running;
            }
        }

        public static bool SetRestartOnFailure(string serviceName, int delay)
        {
            try
            {
                List<SC_ACTION> actions = new List<SC_ACTION>();
                actions.Add(new SC_ACTION() { Delay = delay, Type = (int)SC_ACTION_TYPE.RestartService });
                RecoveryProperty.ChangeRecoveryProperty(serviceName, actions, 0, null, true, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void changeServiceStatus(string serviceName, ServiceControllerStatus status)
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                if (sc == null) throw new Exception("Service not found");
                switch (status)
                {
                    case ServiceControllerStatus.Running:
                        sc.Start();
                        break;
                    case ServiceControllerStatus.Stopped:
                        sc.Stop();
                        break;
                    default:
                        throw new NotImplementedException("Status not supported");
                }
                sc.Refresh();
                sc.WaitForStatus(status);
            }
        }
    }
}

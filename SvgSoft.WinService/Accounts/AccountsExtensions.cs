using SvgSoft.WinService.Constants;
using System;

namespace SvgSoft.WinService.Accounts
{
    static class AccountsExtensions
    {
        public static string GetStringValue(this ServiceAccounts account)
        {
            switch (account)
            {
                case ServiceAccounts.Default:
                    return null;
                case ServiceAccounts.LocalService:
                    return @"NT AUTHORITY\LocalService";
                case ServiceAccounts.LocalSystem:
                    return null;
                case ServiceAccounts.NetworkService:
                    return @"NT AUTHORITY\NetworkService";
                default:
                    throw new NotImplementedException(string.Format("Unrecognized account type: {0}", account));
            }
        }
        public static SCManagerAccess GetPermitedManagerAccess(this AccountType account)
        {
            switch (account)
            {
                case AccountType.Administrator:
                    return SCManagerAccess.SC_MANAGER_ALL_ACCESS;
                case AccountType.Local:
                    return SCManagerAccess.SC_MANAGER_CONNECT | SCManagerAccess.SC_MANAGER_ENUMERATE_SERVICE | SCManagerAccess.SC_MANAGER_QUERY_LOCK_STATUS;
                case AccountType.LocalSystem:
                    return SCManagerAccess.SC_MANAGER_CONNECT | SCManagerAccess.SC_MANAGER_ENUMERATE_SERVICE | SCManagerAccess.SC_MANAGER_MODIFY_BOOT_CONFIG | SCManagerAccess.SC_MANAGER_QUERY_LOCK_STATUS;
                case AccountType.Remote:
                    return SCManagerAccess.SC_MANAGER_CONNECT;
                default:
                    throw new NotImplementedException(string.Format("Account type {0} not supported", account));
            }
        }
    }
}

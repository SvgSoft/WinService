namespace SvgSoft.WinService.Constants
{
    public enum SCManagerAccess
    {
        /// <summary>
        /// Includes STANDARD_RIGHTS_REQUIRED, in addition to all access rights in this table.
        /// </summary>
        SC_MANAGER_ALL_ACCESS = 0xF003F,
        /// <summary>
        /// Required to call the CreateService function to create a service object and add it to the database.
        /// </summary>
        SC_MANAGER_CREATE_SERVICE = 0x0002, 
        /// <summary>
        /// Required to connect to the service control manager.
        /// </summary>
        SC_MANAGER_CONNECT = 0x0001, 
        /// <summary>
        /// Required to call the EnumServicesStatus or EnumServicesStatusEx function to list the services that are in the database.
        /// Required to call the NotifyServiceStatusChange function to receive notification when any service is created or deleted.
        /// </summary>
        SC_MANAGER_ENUMERATE_SERVICE = 0x0004, 
        /// <summary>
        /// Required to call the LockServiceDatabase function to acquire a lock on the database.
        /// </summary>
        SC_MANAGER_LOCK = 0x0008, 
        /// <summary>
        /// Required to call the NotifyBootConfigStatus function.
        /// </summary>
        SC_MANAGER_MODIFY_BOOT_CONFIG = 0x0020, 
        /// <summary>
        /// Required to call the QueryServiceLockStatus function to retrieve the lock status information for the database.
        /// </summary>
        SC_MANAGER_QUERY_LOCK_STATUS = 0x0010,
        /// <summary>
        /// SC_MANAGER_ALL_ACCESS
        /// </summary>
        GENERIC_ALL = SC_MANAGER_ALL_ACCESS,
        /// <summary>
        /// STANDARD_RIGHTS_READ
        /// SC_MANAGER_ENUMERATE_SERVICE
        /// SC_MANAGER_QUERY_LOCK_STATUS 
        /// </summary>
        GENERIC_READ = StandartAccessRights.STANDARD_RIGHTS_READ | SC_MANAGER_ENUMERATE_SERVICE | SC_MANAGER_QUERY_LOCK_STATUS,
        /// <summary>
        /// STANDARD_RIGHTS_WRITE
        /// SC_MANAGER_CREATE_SERVICE
        /// SC_MANAGER_MODIFY_BOOT_CONFIG
        /// </summary>
        GENERIC_WRITE = StandartAccessRights.STANDARD_RIGHTS_WRITE | SC_MANAGER_CREATE_SERVICE | SC_MANAGER_MODIFY_BOOT_CONFIG,
        /// <summary>
        /// STANDARD_RIGHTS_EXECUTE
        /// SC_MANAGER_CONNECT
        /// SC_MANAGER_LOCK
        /// </summary>
        GENERIC_EXECUTE = StandartAccessRights.STANDARD_RIGHTS_EXECUTE | SC_MANAGER_CONNECT | SC_MANAGER_LOCK
    }

}

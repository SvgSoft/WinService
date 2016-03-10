namespace SvgSoft.WinService.Constants
{
    public enum StandartAccessRights
    {
        /// <summary>
        /// The right to delete the object.
        /// </summary>
        DELETE = 0x00010000,
        /// <summary>
        /// The right to read the information in the object's security descriptor, not including the information in the system access control list (SACL).
        /// </summary>
        READ_CONTROL = 0x00020000,
        /*
        /// <summary>
        /// The right to use the object for synchronization. This enables a thread to wait until the object is in the signaled state. Some object types do not support this access right.
        /// </summary>
        SYNCHRONIZE,*/
        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the object's security descriptor.
        /// </summary>
        WRITE_DAC = 0x00040000,
        /// <summary>
        /// The right to change the owner in the object's security descriptor.
        /// </summary>
        WRITE_OWNER = 0x00080000,
        /// <summary>
        /// Combines DELETE, READ_CONTROL, WRITE_DAC, WRITE_OWNER, and SYNCHRONIZE access.
        /// </summary>
        STANDARD_RIGHTS_ALL = 0x001F0000,
        /// <summary>
        /// Currently defined to equal READ_CONTROL.
        /// </summary>
        STANDARD_RIGHTS_EXECUTE = READ_CONTROL,
        /// <summary>
        /// Currently defined to equal READ_CONTROL.
        /// </summary>
        STANDARD_RIGHTS_READ = READ_CONTROL,
        /// <summary>
        /// Combines DELETE, READ_CONTROL, WRITE_DAC, and WRITE_OWNER access.
        /// </summary>
        STANDARD_RIGHTS_REQUIRED = 0x000F0000,
        /// <summary>
        /// Currently defined to equal READ_CONTROL.
        /// </summary>
        STANDARD_RIGHTS_WRITE = READ_CONTROL
    }
}

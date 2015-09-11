namespace ZoomLa.ZLEnum
{
    using System;

    [Flags]
    public enum UserStatus
    {
        Locked = 1,
        None = 0,
        WaitValidateByAdmin = 4,
        WaitValidateByEmail = 2
    }
}
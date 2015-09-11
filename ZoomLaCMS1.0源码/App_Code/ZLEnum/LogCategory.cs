namespace ZoomLa.ZLEnum
{
    using System;

    /// <summary>
    /// 日志类型枚举
    /// </summary>
    public enum LogCategory
    {
        //无
        None,
        //登录成功
        LoginOk,
        //登录失败
        LoginFailure,
        //登出
        LogOut,
        //异常
        Exception        
    }
}
namespace ZoomLa.IDAL
{
    using System;
    using ZoomLa.Model;

    /// <summary>
    /// 日志逻辑层接口
    /// </summary>
    public interface ID_Log
    {
        //添加日志
        bool Add(M_LogInfo info);
        //删除日志
        bool Delete(int id);
    }
}
namespace ZoomLa.IDAL
{
    using System;
    using System.Data;
    using ZoomLa.Model;

    /// <summary>
    /// ID_Correct 的摘要说明
    /// </summary>
    public interface ID_Correct
    {
        void Add(M_Correct info);
        void Delete(int CorrectID);
        DataTable GetList(string title);        
    }
}
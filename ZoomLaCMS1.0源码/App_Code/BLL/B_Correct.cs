namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;

    /// <summary>
    /// B_Correct 的摘要说明
    /// </summary>
    public class B_Correct
    {
        private static readonly ID_Correct dal = IDal.CreateCorrect();

        public B_Correct()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public void Add(M_Correct info)
        {
            dal.Add(info);
        }
        public void Delete(int CorrectID)
        {
            dal.Delete(CorrectID);
        }
        public DataTable GetList(string title)
        {
            return dal.GetList(title);
        }
    }
}
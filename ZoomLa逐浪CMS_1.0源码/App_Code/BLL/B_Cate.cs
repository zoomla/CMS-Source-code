namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;

    /// <summary>
    /// B_Dic 的摘要说明
    /// </summary>
    public class B_Cate
    {
        public B_Cate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private static readonly ID_Cate cate = IDal.CreateCate();
        public static bool Add(M_Cate cateInfo)
        {
            return cate.Add(cateInfo);
        }

        public static bool Update(M_Cate cateInfo)
        {
            return cate.Update(cateInfo);
        }
        public static bool DeleteByID(int cateID)
        {
            return cate.DeleteByID(cateID);
        }
        public static DataView GetCateInfo()
        {
            return cate.SeachCateAll().DefaultView;
        }
        public static M_Cate GetCateByid(int cateId)
        {
            return cate.GetCateByid(cateId);
        }

    }
}
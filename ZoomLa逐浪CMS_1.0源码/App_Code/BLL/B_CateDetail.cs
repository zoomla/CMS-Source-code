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
    /// B_Dic1 的摘要说明
    /// </summary>
    public class B_CateDetail
    {


        public B_CateDetail()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private static readonly ID_CateDetail catedetail = IDal.CreateCateDetail();
        public static bool Add(M_CateDetail catedetailInfo)
        {
            return catedetail.Add(catedetailInfo);
        }
        public static bool Update(M_CateDetail catedetailInfo)
        {
            return catedetail.Update(catedetailInfo);
        }
        public static bool DeleteByID(int catedetailID)
        {
            return catedetail.DeleteByID(catedetailID);
        }
        public static DataTable GetcatedetailAll(int cateID)
        {
            return catedetail.SeachCateDetailAll(cateID);
        }
        public static M_CateDetail  GetcatedetailById(int catedetailid)
        {
            return catedetail.GetcatedetailById(catedetailid);
        }
            


    }
}
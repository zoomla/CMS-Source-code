using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Model.MIS;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.MIS
{
    //公文借阅
    public class B_OA_Borrow : ZoomLa.BLL.ZL_Bll_InterFace<M_OA_Borrow>
    {
        private string TbName = "";
        private string PK = "";
        private M_OA_Borrow model = new M_OA_Borrow();
        private B_OA_Document oaBll = new B_OA_Document();
        public B_OA_Borrow()
        {
            TbName = model.TbName;
            PK = model.PK;
        }
        public int Insert(M_OA_Borrow model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_OA_Borrow model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_OA_Borrow SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 获取用户借阅,并且在有效期内的文档
        /// </summary>
        public DataTable SelByUid(int uid)
        {
            string ids = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("EDate", DateTime.Now), new SqlParameter("uid", "%," + uid + ",%") };
            string sql = "SELECT * FROM " + TbName + " WHERE EDate>@EDate AND Uids Like @uid";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql,sp);
            if (dt == null || dt.Rows.Count < 1) return null;
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr["docids"].ToString() + ",";
            }
            return oaBll.SelByIDS(ids);
        }
        /// <summary>
        /// 是否有该文档的借阅权限,True:有
        /// </summary>
        public bool HasAuth(int uid, int appid)
        {
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("EDate", DateTime.Now), 
                new SqlParameter("uid", "%," + uid + ",%"),
                new SqlParameter("appid","%," + appid + ",%")
            };
            string sql = "SELECT * FROM " + TbName + " WHERE EDate>@EDate AND Uids Like @uid AND Appid LIKE @appid";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            return dt.Rows.Count > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;
using System.Data;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Exam
{
    public class B_EDU_AutoPK : ZL_Bll_InterFace<M_EDU_AutoPK>
    {

        private string TbName, PK;
        private M_EDU_AutoPK initMod = new M_EDU_AutoPK();
        public B_EDU_AutoPK()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_EDU_AutoPK model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_EDU_AutoPK model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public DataTable SelByUid(int uid)
        {
            return SqlHelper.JoinQuery("A.*,B.Regulationame", TbName, "ZL_ExClassgroup", "A.Ownclass=B.GroupID", "UserID=" + uid, "CDate DESC");
        }
        public M_EDU_AutoPK SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable SelByDate(int cid, DateTime sdate, DateTime edate)
        {
            return SelByDate(cid, -1, sdate, edate);
        }
        public DataTable SelByDate(int cid, int uid, DateTime sdate, DateTime edate)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE UserID=" + uid + " AND Ownclass=" + cid + " AND ((SDate BETWEEN '" + sdate + "' AND '" + edate + "' AND  EDate BETWEEN '" + sdate + "' AND '" + edate + "') OR ('"
                        + sdate + "' BETWEEN SDate AND EDate AND '" + edate + "' BETWEEN SDate AND EDate))";
            return SqlHelper.ExecuteTable(System.Data.CommandType.Text, sql);
        }
        public M_EDU_AutoPK SelModelByCid(int classID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE OwnClass=" + classID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable SelByClassID(int cid, int uid)
        {
            string sql = "SELECT A.*,B.Regulationame FROM " + TbName + " A LEFT JOIN ZL_ExClassgroup B ON A.Ownclass=B.GroupID WHERE Ownclass=" + cid + " AND A.UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public System.Data.DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize, int cid = -100, int uid = -100)
        {
            string where = " 1=1";
            if (cid != -100) { where += " AND OwenClass=" + cid; }
            if (uid != -100) { where += " AND UserID"; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        //代课操作
        public bool InsertTeach(string tname, string dtname, int cid, string numinfo, DateTime cdate)
        {
            string sql = string.Format("INSERT INTO ZL_EDU_Substitute(TName,ClassID,NumInfo,DTName,CDate) VALUES('{0}',{1},'{2}','{3}','{4}')", tname, cid, numinfo, dtname, cdate);
            return SqlHelper.ExecuteSql(sql);
        }
        //查询代课信息
        public DataTable SelTeachConfig()
        {
            return SqlHelper.JoinQuery("A.*,B.Regulationame", "ZL_EDU_Substitute", "ZL_ExClassgroup", "A.ClassID=B.GroupID");
        }
    }
}

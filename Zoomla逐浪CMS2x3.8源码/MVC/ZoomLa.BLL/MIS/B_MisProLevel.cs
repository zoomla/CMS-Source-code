using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_MisProLevel
    {
        public string strTableName, PK;
        private M_MisProLevel model = new M_MisProLevel();
        public DataTable dt = null;
        public B_MisProLevel()
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public DataTable SelByProID(string proID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ProID", proID) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ProID=@ProID Order By StepNum", sp);
        }
        public M_MisProLevel SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
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
        public M_MisProLevel SelByProIDAndStepNum(int proID, int StepNum)
        {
            M_MisProLevel mod = null;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("proID", proID), new SqlParameter("StepNum", StepNum) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ProID=@proID And StepNum=@StepNum", sp);
            if (dt != null && dt.Rows.Count > 0)
                mod = model.GetModelFromDR(dt.Rows[0]);
            return mod;
        }
        public List<M_MisProLevel> GetAllLastestMemos()
        {
            string sql = String.Format("select * from {0} where [Type] = 4 order by {1} desc", strTableName, PK);
            return GetMemos(sql, CommandType.Text);
        }
        public List<M_MisProLevel> GetUserMemos(string uname)
        {
            string sql = String.Format("select * from {0} where [Type] = 4 and Inputer = @name order by {1} desc", strTableName, PK);
            SqlParameter pram = new SqlParameter("@name", uname);
            return GetMemos(sql, CommandType.Text, pram);
        }
        private List<M_MisProLevel> GetMemos(string sql, CommandType stype, params SqlParameter[] prams)
        {
            List<M_MisProLevel> lstMemos = new List<M_MisProLevel>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(stype, sql, prams))
            {
                while (reader.Read())
                {
                    lstMemos.Add(model.GetModelFromReader(reader));
                }
            }
            return lstMemos;
        }


        public List<M_MisProLevel> GetSharedMemos(string uname)
        {
            return null;
        }
        private M_MisProLevel SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize, int proid = -100)
        {
            string where = " 1=1";
            string order = "";
            if (proid != -100) { where += " AND ProID=" + proid; order = "StepNum"; }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, "A." + PK, where, order);
            setting.fields = "A.*,B.ProcedureName";
            setting.t2 = "ZL_MisProcedure";
            setting.on = "A.ProID=B.ID";
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByProID(int proID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("proID", proID) };
            return SqlHelper.JoinQuery("A.*,B.ProcedureName", strTableName, "ZL_MisProcedure", "A.ProID=B.ID", "ProID=@proID", "StepNum", sp);
        }
        /// <summary>
        /// 获取指定步骤之前或之后的步骤信息,用于回退和转交等,为防SQL注入,请勿让用户接触第三个参数
        /// </summary>
        public DataTable SelByStep(int proID, int stepNum, string op = "<=")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("proID", proID), new SqlParameter("StepNum", stepNum) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ProID=@proID And StepNum" + op + "@StepNum Order By StepNum", sp);
        }
        public bool UpdateByID(M_MisProLevel model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool UpdateUid(int uid, int id)
        {
            return Sql.Update(strTableName, "UserId=" + uid, "Id=" + id, null);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_MisProLevel model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //--------------
        public DataTable SelByRefer(int userID)
        {
            B_User userBll = new B_User();
            M_UserInfo userModel = new M_UserInfo();
            userModel = userBll.GetUserByUserID(userID);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserID", "%," + userID + ",%"), new SqlParameter("GroupID", "%," + userModel.GroupID + ",%") };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ReferUser Like @UserID OR ReferGroup Like @GroupID", sp);
        }
        public DataTable SelByCC(int userID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserID", "%," + userID + ",%") };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where CCUser Like @UserID");
        }
        public DataTable SelByEmail(int userID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserID", "%," + userID + ",%") };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where EmailAlert Like @UserID");
        }
        public DataTable SelBySMS(int userID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserID", "%," + userID + ",%") };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where SMSAlert Like @UserID");
        }
        /// <summary>
        /// 是否最后一步
        /// </summary>
        public bool IsLastStep(M_MisProLevel currentModel)
        {
            bool flag = false;
            DataTable dt = SelByProID(currentModel.ProID);
            model = model.GetModelFromDR(dt.Rows[(dt.Rows.Count - 1)]);
            if (model.stepNum == currentModel.stepNum)
                flag = true;
            return flag;
        }
        public bool UpdateStep(int proID)
        {
            bool flag = false;
            DataTable dt = SelByProID(proID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                model = SelReturnModel(DataConvert.CLng(dt.Rows[i]["ID"]));
                model.stepNum = i + 1;
                if (UpdateByID(model))
                    flag = true;
                else
                    flag = false;
            }
            return flag;
        }
        //判断权限用户和用户组
    }
}

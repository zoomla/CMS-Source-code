using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.SQLDAL.SQL;

/*
 * OA自由流程,后期与B_MisProcedure(固定流程)合并
 * 
 */
namespace ZoomLa.BLL
{
    public class B_OA_FreePro : ZL_Bll_InterFace<M_MisProLevel>
    {
        public string strTableName, PK;
        public DataTable dt = null;
        public M_MisProLevel model = new M_MisProLevel();
        public B_OA_FreePro()
        {
            strTableName = "ZL_OA_FreePro";
            PK = model.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
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
        public M_MisProLevel SelByDocID(int id)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "Where BackOption="+id+" And StepNum=1"))
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_MisProLevel model)
        {
            if (!string.IsNullOrEmpty(model.CCUser) && !string.IsNullOrEmpty(model.ReferUser))
                model.CCUser = StringHelper.RemoveRepeat(model.CCUser.Split(','), model.ReferUser.Split(','));
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int Insert(M_MisProLevel model)
        {
            if (!string.IsNullOrEmpty(model.CCUser) && !string.IsNullOrEmpty(model.ReferUser))
                model.CCUser = StringHelper.RemoveRepeat(model.CCUser.Split(','), model.ReferUser.Split(','));
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //---------Tools
        /// <summary>
        /// 获取ProID指定步骤的实例化模型
        /// </summary>
        /// <param name="docID">公文ID</param>
        /// <param name="StepNum">需要的步骤</param>
        /// <returns></returns>
        public M_MisProLevel SelByProIDAndStepNum(int docID, int StepNum)
        {
            M_MisProLevel mod = null;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("DocID", docID), new SqlParameter("StepNum", StepNum) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where BackOption=@DocID And StepNum=@StepNum", sp);
            if (dt != null && dt.Rows.Count > 0)
                mod = model.GetModelFromDR(dt.Rows[0]);
            return mod;
        }
        /// <summary>
        /// 查出公文ID下所指定的全部步骤
        /// </summary>
        public DataTable SelDTByDocID(int docID)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where BackOption=" + docID + " Order By StepNum");
        }
        /// <summary>
        /// FreePro中是否进行到了最后一步,用于创建新的流程
        /// </summary>
        public bool IsLastFreeStep(M_MisProLevel currentModel)
        {
            DataTable dt = new DataTable();
            dt = SelDTByDocID(currentModel.BackOption);
            model = model.GetModelFromDR(dt.Rows[(dt.Rows.Count - 1)]);
            bool flag = (model.stepNum == currentModel.stepNum);
            return flag;
        }
        /// <summary>
        /// 流程是否最后一步(公文,限定和自由判断方式不同)
        /// </summary>
        public bool IsLastStep(M_OA_Document oaMod, M_MisProLevel currentModel)
        {
            B_MisProLevel stepBll = new B_MisProLevel();
            DataTable dt = new DataTable();
            bool flag = false;
            switch (oaMod.ProType)
            {
                case (int)M_MisProcedure.ProTypes.Admin:
                case (int)M_MisProcedure.ProTypes.AdminFree:
                    dt = stepBll.SelByProID(oaMod.ProID);
                    break;
                case (int)M_MisProcedure.ProTypes.Free://1:0
                default:
                    dt = SelDTByDocID(currentModel.BackOption);
                    break;
            }
            if (dt.Rows.Count < 1) { throw new Exception("流程下未定义步骤"); }
            model = model.GetModelFromDR(dt.Rows[(dt.Rows.Count - 1)]);
            flag = (model.stepNum == currentModel.stepNum);
            return flag;
        }
        /// <summary>
        /// 获取下一步骤序号
        /// </summary>
        public int GetStep(int docID) 
        {
            int step=1;
            string sql = "Select max(StepNum) From "+strTableName+" Where backoption="+docID;
            dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            if (dt == null)
            {

            }
            else
            {
                step = (Convert.ToInt32(dt.Rows[0][0])+1);
            }
            return step;
        }
        /// <summary>
        /// 获取最后一步
        /// </summary>
        public M_MisProLevel GetLastStep(int docID) 
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, "Select Top 1 * From "+strTableName+" Where BackOption = "+docID+" Order By StepNum Desc"))
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
        /// <summary>
        /// 获取指定步骤之前或之后的步骤信息,用于回退和转交等,为防SQL注入,请勿让用户接触第三个参数
        /// </summary>
        public DataTable SelByStep(int proID, int stepNum, string op = "<=")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("proID", proID), new SqlParameter("StepNum", stepNum) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where BackOption=@proID And StepNum" + op + "@StepNum Order By StepNum", sp);
        }
        /// <summary>
        /// 删除指定步骤,主用于回退等时候
        /// </summary>
        /// <param name="docID">公文ID</param>
        /// <param name="stepNum">步骤号</param>
        /// <param name="op">SQL操作符</param>
        public void DelByStep(int docID, int stepNum, string op = ">")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("docID", docID), new SqlParameter("StepNum", stepNum) };
            op = op.Replace(" ", "");
            switch (op)
            {
                case ">":
                case "<":
                case "=":
                case ">=":
                case "<=":
                    break;
                default:
                    throw new Exception("运算符不正确[" + op + "]");
            }
            SqlHelper.ExecuteSql("Delete From " + strTableName + " Where BackOption=@docID And StepNum" + op + "@StepNum", sp);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
    public class B_Mis_AppProg
    {
        public string TbName = "";
        public string PK = "";
        public DataTable dt = null;
        private M_Mis_AppProg model = new M_Mis_AppProg();
        public B_Mis_AppProg()
        {
            TbName = model.TbName;
            PK = model.PK;
        }
        //-----------------Insert
        /// <summary>
        /// 添加一条新的数据
        /// </summary>
        /// <param name="appID">申请ID</param>
        /// <param name="proID">使用的流程ID</param>
        /// <param name="userID">审批人</param>
        /// <param name="result">审批结果:0未审,2未通过,99成功</param>
        /// <param name="remind">备注</param>
        /// <param name="createDate">创建日期</param>
        /// <returns></returns>
        public int Insert(int appID, int proID, int userID, string result, string remind, DateTime createDate)
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("appID",appID),
                new SqlParameter("proID",proID),
                new SqlParameter("userID",userID),
                new SqlParameter("result",result),
                new SqlParameter("Remind",remind),
                new SqlParameter("CreateDate",createDate),
            };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, "Insert Into " + TbName + " (appID,proID,userID,result,Remind,CreateDate)Values(@appID,@proID,@userID,@result,@Remind,@CreateDate);select @@IDENTITY;", sp);
        }
        public int Insert(M_Mis_AppProg model)
        {
            return Sql.insert(TbName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
        //-----------------Retrieve
        public DataTable SelAll()
        {
            dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Order By CreateDate Desc");
            return dt;
        }
        /// <summary>
        /// 根据申请单ID,查看结果,含用户名
        /// </summary>
        public DataTable SelByAppID(string appID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("appid", appID) };
            return DBCenter.JoinQuery("A.*,B.UserName,B.HoneyName", TbName, "ZL_User", "a.ApproveID=b.UserID", "A.appid=@appid", "A.ID", sp);
        }
        /// <summary>
        /// 获取指定步骤的会签信息
        /// </summary>
        /// <param name="appid">文档ID</param>
        /// <param name="stepnum">步骤序号</param>
        /// <returns>字符串,有需要可以返回数组等</returns>
        public string SelHQInfo(int appid,int stepnum) 
        {
            DataTable dt = SelHQDT(appid,stepnum);
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["UserName"] + ",";
            }
            return result.TrimEnd(',');
        }
        public DataTable SelHQDT(int appid, int stepnum)
        {
            return SqlHelper.JoinQuery("A.*,B.HoneyName,B.UserName", TbName, "ZL_User", "a.ApproveID=b.UserID", "AppID="+appid+" AND ProLevel="+stepnum);
        }
        //根据申请单ID,会员ID查看结果,含用户名 ---By ZC
        public DataTable SelByAppID(string appID, string userID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("appID", appID), new SqlParameter("userID", userID) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select a.*,b.UserName From " + TbName + " as a Left Join ZL_User as b ON a.ApproveID=b.UserID Where AppID=@appID And ApproveID=@userID Order By ID",sp);
        }
        public DataTable SelHasSign(int appID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("appID", appID) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select a.*,b.VPath From " + TbName + " as a Left Join ZL_OA_Sign as b ON a.SignID=b.ID Where AppID=@appID And a.[Sign] is not null And a.[Sign]!='' Order By ID", sp);
        }
        /// <summary>
        /// 根据appid,stepnum获取表信息,用于会签
        /// </summary>
        public DataTable SelDTByStep(int appID, int stepNum)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where AppID=" + appID + " And ProLevel=" + stepNum, null);
        }
        /// <summary>
        /// 用于会签,根据Appid与StepNum获取到目标模型
        /// </summary>
        /// <returns></returns>
        public M_Mis_AppProg SelByStep(int appID, int stepNum)
        {
            return SelReturnModel("Where AppID=" + appID + " And ProLevel=" + stepNum);
        }
        private M_Mis_AppProg SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
        /// 根据Appid找到已会签的用户
        /// </summary>
        public string SelHQUserID(int appID, int stepNum)
        {
            DataTable dt = SelDTByStep(appID, stepNum);
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["HQUserID"].ToString() + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }

        public bool IsHQComplete(int appID, M_MisProLevel stepMod)
        {
            bool flag = true;
            string ids = SelHQUserID(appID, stepMod.stepNum);
            if (string.IsNullOrEmpty(ids))
                return false;//无会签信息,则直接返回
            string[] progArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] stepArr = stepMod.ReferUser.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < stepArr.Length; i++)
            {
                if (!progArr.Select(p => p).Contains(stepArr[i]))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// 判断会签是否完成
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="userID">当前签字的经办人ID</param>
        /// <param name="stepMod">当前步骤模型</param>
        /// <returns></returns>
        public bool IsHQComplete(int appID, int userID, M_MisProLevel stepMod)
        {
            //B_Group groupBll=new B_Group();
            bool flag = true;
            string ids = SelHQUserID(appID, stepMod.stepNum);
            //if (string.IsNullOrEmpty(ids)) return false;//无会签信息,则直接返回
            ids += "," + userID;
            string[] progArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //if (stepMod.ReferGroup != "")
            //    stepMod.ReferUser += groupBll.GetUserIDByGroupIDS(stepMod.ReferGroup);
            string[] stepArr = stepMod.ReferUser.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < stepArr.Length; i++)
            {
                if (!progArr.Select(p => p).Contains(stepArr[i]))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        /// <summary>
        /// 根据审批，获取已被此用户审批过的文档ids字符串
        /// </summary>
        public string GetAppidByApprove(int userID)
        {
            string ids = "";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select AppID From " + TbName + " Where ApproveID = " + userID + " Group By AppID");
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr["AppID"].ToString() + ",";
            }
            ids = ids.TrimEnd(',');
            return ids;
        }
        /// <summary>
        /// 清除会签信息,用于回退时，将回退到的那步之后所有的会签信息清除.
        /// </summary>
        public void ClearHQ(int appID, int stepNum)
        {
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + TbName + " Set HQUserID='' Where AppID=" + appID + " And ProLevel>" + stepNum);
        }
        /// <summary>
        /// 清除签章信息,用于回退
        /// </summary>
        public void ClearSign(int appID, int stepNum)
        {
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + TbName + " Set SignID=null,Sign=null Where AppID=" + appID + " And ProLevel>" + stepNum);
        }
        //查看用户是否已对流程审批，是则返回true
        public bool CheckApproval(int userid, int stepnum, int appid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userid", userid), new SqlParameter("stepnum", stepnum), new SqlParameter("appid", appid) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where AppID=@appid And ApproveID=@userid And ProLevel=@stepnum", sp);
            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// 更新提交时间,用于公文审批等
        /// </summary>
        public bool UpdateDate(int uid, int id, DateTime date)
        {
            string sql = "Update " + TbName + " Set CreateTime='" + date + "' Where ApproveID=" + uid + " And ID=" + id;
            return SqlHelper.ExecuteSql(sql);
        }
    }
}

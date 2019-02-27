using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.BLL.Helper;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_OA_Document
    {
        public M_OA_Document model = new M_OA_Document();
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        public B_MisProLevel stepBll = new B_MisProLevel();
        public B_Mis_AppProg progBll = new B_Mis_AppProg();
        public string NormalDoc = " (Status =0 OR Status=2) ";
        public B_OA_Document() 
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_OA_Document SelReturnModel(int ID)
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
        private M_OA_Document SelReturnModel(string strWhere)
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
        public int insert(M_OA_Document model)
        {
            return Sql.insertID(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_OA_Document model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
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
        //流程是否已经开始(即是否已有处理记录)
        public bool HasBegin(int appid)
        {
            string sql = "SELECT Top 1 ID FROM ZL_Mis_Appprog WHERE AppID="+appid;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            return dt.Rows.Count > 0;
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        /// <summary>
        /// 更新状态，默认将其放入回收站
        /// </summary>
        public void UpdateStatus(int id,int status=-99) 
        {
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + strTableName + " Set Status="+status+" Where ID="+id);
        }
        /// <summary>
        /// 用户有权经办的文档(已执行到的当前步,或已处理过)如果Pro上还需要限制权限,则再另外在外面套:0:全部，1:待办,2:已办
        /// </summary>
        public DataTable SelByRefer(int userID, int flag=0) 
        {
            //OA步骤是0步开始,表示当前步骤，获取下一步需要加1,用于待办
            //获取用户有权限的目标表
            DataTable resultDT = new DataTable();
            DataTable referDT = stepBll.SelByRefer(userID);//获取到用户有权限的步骤
            string proID ="";
            foreach(DataRow dr in referDT.Rows)
            {
               proID+=dr["ProID"].ToString()+",";
            }
            proID= proID.Trim(',');
            if (!string.IsNullOrEmpty(proID))//没有固定任何经办权限
            {
                //获取到公文表中，拥有对应ProID权限的文档,这是初步筛选
                DataTable docDT = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ProID in(" + proID + ") Order By CreateTime Desc");
                //还需要筛选一步，看当前执行到的步骤是否有经办权限(当前步无其权限的,不显示)
                proID = "";
                foreach (DataRow dr in referDT.Rows)
                {
                    docDT.DefaultView.RowFilter = "ProID=" + dr["ProID"] + "And ((CurStepNum+1) =" + dr["StepNum"].ToString() + " Or CurStepNum=" + dr["StepNum"].ToString() + ")";
                    resultDT.Merge(docDT.DefaultView.ToTable());
                    docDT.DefaultView.RowFilter = "";
                }
            }
            //自由流程筛选(上面已完成固定流程筛选)

            //文档为自由流程,且仍在进行或回退,步数+1=自由流程中进度,且用户有权限的文档表
            string freeSql = "Select a.*,b.stepNum,b.ReferUser,b.BackOption From " + strTableName + " as a Left Join ZL_OA_FreePro as b on a.id=b.BackOption and (a.CurStepNum+1)=b.stepNum Where (a.[Status] =0 OR a.[Status]=2) And a.ProID=0 And (b.ReferUser Like @UserID OR b.CCUser Like @UserID)";
            SqlParameter[] freeSp = new SqlParameter[] { new SqlParameter("UserID","%,"+userID+",%")};
            DataTable freeDocDT = SqlHelper.ExecuteTable(CommandType.Text, freeSql,freeSp);
            resultDT.Merge(freeDocDT);//合并
            resultDT.DefaultView.Sort = "CreateTime Desc";
            resultDT = resultDT.DefaultView.ToTable();

            switch (flag)
            {
                case 0://全部
                    break;
                case 1://待办
                    resultDT.DefaultView.RowFilter = "Status >-1 And Status < 99";//进行中，回退
                    break;
                case 2://显示自己有权限，且已归档案的
                    resultDT.DefaultView.RowFilter = "Status = '-1' OR Status = '99'";
                    break;
                default:
                    break;
            }
           resultDT.DefaultView.RowFilter = "DocType is Null OR DocType=0";
           resultDT = resultDT.DefaultView.ToTable(true);
           return resultDT;
        }
        /// <summary>
        /// 待办公文筛选,只筛选自由流程,其原本的流程不筛选或分开筛,以降低代码复杂度
        /// </summary>
        /// <param name="uid">登录的用户ID</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public DataTable SelDocByUid(int uid)
        {
            /*
             * --ZL_OA_FreePro中最大的一步(如果CurStepNum=0的时候则为1也可)
             * --非完成或已拒绝状态,且已经有自由流程存在
             */
            DataTable resultDT = new DataTable();
            string suid = "'%," + uid + ",%'";
            string fields = "A.*,B.stepNum,b.stepName,B.ReferUser,B.CCUser";
            //string on = "A.ID=B.BackOption AND (case when A.CurStepNum =0 then 1 else A.CurStepNum end)=B.stepNum";
            string on = "A.ID=B.BackOption AND (A.CurStepNum+1)=B.stepNum";
            string where = "A.[Status]!=99 AND A.[Status]!=-1 AND stepNum IS NOT NULL AND ReferUser LIKE " + suid;
            resultDT = SqlHelper.JoinQuery(fields, "ZL_OA_Document", "ZL_OA_FreePro", on, where, "A.SendDate DESC");
            return resultDT;
        }
        /// <summary>
        /// CC给用户的文档
        /// </summary>
        public DataTable SelByCC(int userID) 
        {
            DataTable dt = new DataTable();
            return dt;
        }
        /// <summary>
        /// 已发公文,不包含回收站和已归档
        /// </summary>
        /// <param name="status">公文状态,为0取全部,除已删除</param>
        public DataTable SelByUserID(int uid, int status = 0)
        {
            string sql = "Select * From " + strTableName + " Where UserID=" + uid;
            if (status == 0)
            {
                sql += " And Status!=-99 And Status<100";
            }
            else
            {
                sql += " And Status=" + status + " And Status<100";
            }
            sql += " Order By CreateTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 自己已办的流程,不显示归档
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="docType">流程ID,为0获取全部</param>
        public DataTable SelByApproveID(int uid, int proid = 0)
        {
            string ids = progBll.GetAppidByApprove(uid);
            if (string.IsNullOrEmpty(ids))
                return new DataTable();
            string sql = "Select * From " + strTableName + " Where ID In(" + ids + ") AND [Status]<100";
            if (proid > 0)
            {
                sql += " AND ProID=" + proid;
            }
            sql += " Order By CreateTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelByItemID(string TableName, int itemID)
        {
            string sql = "Select * From " + TableName + " Where ID=" + itemID;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 是否已经开始审批了,如果mis_prog中该ID下已有记录时,则返回true
        /// </summary>
        /// <returns></returns>
        public bool IsApproving(int appID)
        {
            bool flag = false;
            DataTable dt = progBll.SelByAppID(appID.ToString());
            if (dt != null && dt.Rows.Count > 0)
                flag = true;
            return flag;
        }
        /// <summary>
        /// 根据状态获取文件
        /// </summary>
        public DataTable SelDocByStatus(ZoomLa.Model.ZLEnum.ConStatus status)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE [Status]=" + (int)status;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelByIDS(string ids)
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids)) return null;
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteTable(CommandType.Text,"SELECT * FROM "+strTableName+" WHERE ID IN ("+ids+")");
        }
        public string SelTitleByIDS(string ids)
        {
            string result = "";
            DataTable dt = SelByIDS(ids);
            if (dt == null || dt.Rows.Count < 1) { return result; }
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["Title"] as string + ",";
            }
            return result.TrimEnd(',');
        }
        //public DataTable SelByDocType(int userID,int doctype, int flag = 0)
        //{
        //    //OA步骤是0步开始,表示当前步骤，获取下一步需要加1,用于待办
        //    //获取用户有权限的目标表
        //    DataTable resultDT = new DataTable();
        //    DataTable referDT = stepBll.SelByRefer(userID);//获取到用户有权限的步骤
        //    string proID = "";
        //    foreach (DataRow dr in referDT.Rows)
        //    {
        //        proID += dr["ProID"].ToString() + ",";
        //    }
        //    proID = proID.Trim(',');
        //    if (!string.IsNullOrEmpty(proID))//没有固定任何经办权限
        //    {
        //        //获取到公文表中，拥有对应ProID权限的文档,这是初步筛选
        //        DataTable docDT = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ProID in(" + proID + ") Order By CreateTime Desc");
        //        //还需要筛选一步，看当前执行到的步骤是否有经办权限(当前步无其权限的,不显示)
        //        proID = "";
        //        foreach (DataRow dr in referDT.Rows)
        //        {
        //            docDT.DefaultView.RowFilter = "ProID=" + dr["ProID"] + "And ((CurStepNum+1) =" + dr["StepNum"].ToString() + " Or CurStepNum=" + dr["StepNum"].ToString() + ")";
        //            resultDT.Merge(docDT.DefaultView.ToTable());
        //            docDT.DefaultView.RowFilter = "";
        //        }
        //    }
        //    //自由流程筛选(上面已完成固定流程筛选)

        //    //文档为自由流程,且仍在进行或回退,步数+1=自由流程中进度,且用户有权限的文档表
        //    string freeSql = "Select a.*,b.stepNum,b.ReferUser,b.BackOption From " + strTableName + " as a Left Join ZL_OA_FreePro as b on a.id=b.BackOption and (a.CurStepNum+1)=b.stepNum Where (a.[Status] =0 OR a.[Status]=2) And a.ProID=0 And (b.ReferUser Like @UserID OR b.CCUser Like @UserID)";
        //    SqlParameter[] freeSp = new SqlParameter[] { new SqlParameter("UserID", "%," + userID + ",%") };
        //    DataTable freeDocDT = SqlHelper.ExecuteTable(CommandType.Text, freeSql, freeSp);
        //    resultDT.Merge(freeDocDT);//合并
        //    resultDT.DefaultView.Sort = "CreateTime Desc";
        //    resultDT = resultDT.DefaultView.ToTable();

        //    switch (flag)
        //    {
        //        case 0://全部
        //            break;
        //        case 1://待办
        //            resultDT.DefaultView.RowFilter = "Status >-1 And Status < 99";//进行中，回退
        //            break;
        //        case 2://显示自己有权限，且已归档案的
        //            resultDT.DefaultView.RowFilter = "Status = '-1' OR Status = '99'";
        //            break;
        //        default:
        //            break;
        //    }
        //    if (doctype == 0)//筛选公文
        //    {
        //        resultDT.DefaultView.RowFilter = "DocType is Null OR DocType=" + doctype;
        //    }
        //    else if (doctype == 1)//筛选事务
        //    {
        //        resultDT.DefaultView.RowFilter = "DocType=" + doctype;
        //    }
        //    resultDT = resultDT.DefaultView.ToTable(true);
        //    return resultDT;
        //}
    }
}

namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Net.Mail;
    using System.Collections.Generic;
    using ZoomLa.Components;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_Message
    {
        public B_Message()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_Message initmod = new M_Message();
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable Sel(string type, string skey)
        {
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            switch (type)
            {
                case "all":
                    break;
                case "sys":
                    where += " AND MsgType = 1";
                    break;
                case "mb":
                    where += " AND MsgType = 2";
                    break;
                case "code":
                    where += "AND MsgType = 3";
                    break;
            }
            if (!string.IsNullOrEmpty(skey)) { where += " AND Title LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.Sel(strTableName, where, " PostDate DESC", sp);
        }
        public M_Message SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_Message SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return new M_Message().GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public static bool Update(M_Message model)
        {
            return Sql.UpdateByIDs(model.TbName, model.PK, model.MsgID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DelByID(int id)
        {
            string sqlStr = "Delete From " + strTableName + " Where MsgID=" + id;
            return SqlHelper.ExecuteSql(sqlStr);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        //增加信息
        public int GetInsert(M_Message model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public static int Add(M_Message model) { return new B_Message().GetInsert(model); }
        public static M_Message GetMessByID(int id) { return new B_Message().SelReturnModel(id); }
        /// <summary>
        /// 放入垃圾箱-1删除,0收件删除,1发件删除,2:标为已读
        /// </summary>
        public bool DropByids(string ids, int flag = 0)
        {
            string sqlStr = "";
            switch (flag)
            {
                case 0:
                    sqlStr = "Update " + strTableName + " Set Status=1,IsDelInBox=1 Where MsgID in({0})";
                    break;
                case 1:
                    sqlStr = "Update " + strTableName + " Set IsDelSendbox=1 Where MsgID in({0})";
                    break;
                case -1:
                    sqlStr = "Update " + strTableName + " Set Status=-1 Where MsgID in({0})";
                    break;
                default:
                    break;
            }
            SafeSC.CheckIDSEx(ids);
            sqlStr = string.Format(sqlStr, ids);
            return SqlHelper.ExecuteSql(sqlStr);
        }
        /// <summary>
        /// 如自己仅是收件或抄送人,则从收件人中移除自己信息
        /// </summary>
        public void RemoveByUserID(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string[] idArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            M_Message model = new M_Message();
            for (int i = 0; i < idArr.Length; i++)
            {
                int mid = Convert.ToInt32(idArr[i]);
                model = SelReturnModel(mid);
                model.Incept = model.Receipt.Replace("," + uid + ",", ",,").Replace(",,", ",");//从收件人和抄送人中移除自己
                model.CCUser = model.CCUser.Replace("," + uid + ",", ",,").Replace(",,", ",");
                Update(model);
            }
        }
        /// <summary>
        /// 恢复收件箱,发件箱,草稿等的被删除状态
        /// </summary>
        public bool ReCoverByIDS(string ids, int uid)
        {
            //SafeSC.CheckIDSEx(ids);
            string sql = "Update " + strTableName + " Set IsDelInbox=0 Where MsgID in ({0})";
            string sql2 = "Update " + strTableName + " Set Status=1,IsDelSendBox=0 Where MsgID in({0}) And Sender=" + uid;
            SafeSC.CheckIDSEx(ids);
            sql = string.Format(sql, ids);
            sql2 = string.Format(sql2, ids);
            SqlHelper.ExecuteSql(sql);
            return SqlHelper.ExecuteSql(sql2);
        }
        /// <summary>
        /// 查询收件数据
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="isdel">是否删除</param>
        /// <returns></returns>
        public static DataTable SeachByUserName(string UserName, int isdel, int Savedata)
        {
            string sqlStr = "select * from ZL_Message where Incept=@UserName and IsDelInbox=@IsDelInbox and Savedata=@Savedata Order by PostDate desc";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar),
                new SqlParameter("@IsDelInbox", SqlDbType.Int),
                new SqlParameter("@Savedata",SqlDbType.Int)
            };
            cmdParams[0].Value = UserName;
            cmdParams[1].Value = isdel;
            cmdParams[2].Value = Savedata;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public static DataTable SelByUserName(string UserID, int isdel, int Savedata)
        {
            string sqlStr = "select * from ZL_Message where (Incept like @UserID or CCUser like @UserID)  and IsDelInbox=@isdel and Savedata=@Savedata Order by PostDate desc";
            SqlParameter[] sp = new SqlParameter[]{ new SqlParameter("UserID","%,"+UserID+",%"),
            new SqlParameter("isdel",isdel),new SqlParameter("Savedata",Savedata)};
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        public static void DeleteByDate(int datediff)
        {
            string sqlStr = "";
            if (datediff > 0)
                sqlStr = "delete from ZL_Message where datediff(day,PostDate,getdate())>@diff and status=1";
            else
                sqlStr = "delete from ZL_Message where status=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@diff", SqlDbType.Int) };
            cmdParams[0].Value = datediff;
            SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public static void DeleteByUser(string UserName)
        {
            string sqlStr = "delete from ZL_Message where Sender=@Sender and status=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@Sender", SqlDbType.NVarChar) };
            cmdParams[0].Value = UserName;
            SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public static int UserMessCount(int userID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userID", "%," + userID + ",%") };
            string sqlStr = "select Count(MsgID) from ZL_Message Where Incept like @userID and readuser not like @userID and IsDelSendbox=0 and IsDelInbox=0";
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, sp));
        }
        /// <summary>
        /// 查找该用户的发送信息
        /// </summary>
        /// <param name="senderName"></param>
        /// <returns></returns>
        public static DataTable SeachSendMessage(string UserID, int IsDelSendbox, int saveData)
        {
            string sqlStr = "select * from ZL_Message where Sender=@UserName and IsDelSendbox=@IsDelSendbox and saveData=@saveData  Order by PostDate desc";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar),
                new SqlParameter("@IsDelSendbox", SqlDbType.Int),
                new SqlParameter("@Savedata",SqlDbType.Int)
            };
            cmdParams[0].Value = UserID;
            cmdParams[1].Value = IsDelSendbox;
            cmdParams[2].Value = saveData;
            //System.Web.HttpContext.Current.Response.Write(sqlStr);
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public static bool IsExit(string msgID)
        {
            string sqlStr = "SELECT count(*) FROM ZL_Message WHERE MsgID=" + msgID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr)) > 0;
        }
        /// <summary>
        /// 是否允许其读这条信息
        /// </summary>
        public bool AllowRead(int msgID, string userID)
        {
            bool flag = false;
            string str = "," + userID + ",";
            M_Message model = SelReturnModel(msgID);
            if (model.Sender.Equals(userID) || model.Incept.IndexOf(str) > -1 || model.CCUser.IndexOf(str) > -1)//发件,收件,抄送人可阅读
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 获取用户未读的短信信息
        /// </summary>
        public DataTable SelUnreadMsgByUserID(int userID)
        {
            //string uid = "," + userID + ",";
            //dt = SqlHelper.ExecuteTable("Select MsgID From "+strTableName);
            //return dt;
            return null;
        }

        //----------------------New Logical
        /// <summary>
        /// 0:草稿,1:我的发件(未删除),2:我的收件(未删除),3:我的回收站,默认为收件箱
        /// </summary>
        public DataTable SelMyMail(int uid, int flag = 2, string skey = "")
        {
            //Savedata:0:正常,1:草稿
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "", suid = "'%," + uid + ",%'";
            switch (flag)
            {
                case 0://草稿(草稿删中删除,则删除数据库记录)
                    where += " Sender=" + uid + " And Savedata =1";
                    break;
                case 1://发件箱
                    where += " Sender=" + uid + NormalMail(uid);
                    break;
                case 2://收件箱
                    where += " (Incept Like " + suid + " OR CCUser Like " + suid + ")" + NormalMail(uid);
                    break;
                case 3://回收站
                    where += " Savedata=0 And DelIDS Like " + suid + " And (RealDelIDS Is Null OR RealDelIDS Not Like " + suid + ")";
                    break;
                case 4://我的所有带附件的发件,正常,草稿,回收站(用于邮箱容量统计),彻底删除的不算
                    where += " Sender='" + uid + "' And AttachMent !='' And (RealDelIDS Is Null OR RealDelIDS Not Like " + suid + ")";
                    break;
                default:
                    throw new Exception("错误的参数Flag:" + flag);
            }
            if (!string.IsNullOrEmpty(skey)) { where += " AND Title LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.Sel(strTableName, where, "MsgID DESC", sp);
        }
        //非草稿,非回收站,未删除的邮件
        private string NormalMail(int uid)
        {
            string suid = "'%," + uid + ",%'";
            string sql = " And Savedata=0 And (DelIDS Is Null OR DelIDS Not Like " + suid + ") And (RealDelIDS Is Null OR RealDelIDS Not Like " + suid + ")";
            return sql;
        }
        /// <summary>
        /// 恢复收件箱,发件箱
        /// </summary>
        public bool ReFromRecycle(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string suid = "'," + uid + ",'";
            string sql = "Update " + strTableName + " Set DelIDS=REPLACE(REPLACE(DelIDS," + suid + ",','),',,',',') Where MsgID in(" + ids + ")";//将用户的ID从DelIDS中移除,并且将,,替换为,保持格式正常
            return SqlHelper.ExecuteSql(sql);
        }
        public bool ReFromDraft(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update " + strTableName + " Set SaveData=0 Where MsgID in(" + ids + ") Where Sender=" + uid;
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 将其置入DelIDS中
        /// </summary>
        public void DelByID(int msgid, int uid)
        {
            DelByIDS(msgid.ToString(), uid);
        }
        /// <summary>
        /// 批量删除, 如用户量大，则改为存储过程
        /// </summary>
        public void DelByIDS(string ids, int uid)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            string suid = "'," + uid + ",'";
            string sql = "Update " + strTableName + " Set DelIDS = ISNULL(DelIDS,'')+" + suid + " Where MsgID in(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public void RealDelByID(int msgid, int uid)
        {
            RealDelByIDS(msgid.ToString(), uid);
        }
        /// <summary>
        /// 彻底删除,草稿(删记录)
        /// </summary>
        public void RealDelByIDS(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string suid = "'," + uid + ",'";
            string sql = "Update " + strTableName + " Set RealDelIDS =ISNULL(RealDelIDS,'') +" + suid + " Where " + PK + " in(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
            DelAttachAndRecord(ids, uid);
        }
        //删除数据库记录和附件(用于草稿)
        private void DelAttachAndRecord(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Select MsgID,AttachMent From " + strTableName + " Where MsgID in(" + ids + ") And SaveData=1 And Sender =" + uid;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            string needDel = DelAttach(dt);
            if (!string.IsNullOrEmpty(needDel))
            {
                needDel = needDel.TrimEnd(',');
                string delSql = "Delete From " + strTableName + " Where MsgID in(" + needDel + ")";
                SqlHelper.ExecuteSql(delSql);
            }
        }
        private string DelAttach(DataTable dt)
        {
            string needDel = "";
            foreach (DataRow dr in dt.Rows)
            {
                needDel += dr["MsgID"] + ",";
                if (string.IsNullOrEmpty(dr["AttachMent"].ToString())) continue;
                foreach (string vpath in dr["AttachMent"].ToString().Split(','))
                {
                    SafeSC.DelFile(vpath);
                }
            }
            return needDel;
        }
        //-----------------Tool
        /// <summary>
        /// 获取指定用户,是否有未读的邮件,如果有的话,则提示
        /// </summary>
        public DataTable GetUnReadMail(int uid)
        {
            string u = "," + uid + ",";
            string suid = "'%," + uid + ",%'";
            string sql = "Select MsgID,Title From " + strTableName + " Where (CCUser Like '%" + u + "%' OR Incept Like '%" + u + "%') And ReadUser Not Like " + suid + NormalMail(uid);
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 批量将邮件设为已阅读
        /// </summary>
        public void UnreadToRead(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update ZL_Message Set ReadUser=ReadUser+'," + uid + ",' Where msgid in (" + ids + ") And ReadUser not like '%," + uid + ",%'";
            SqlHelper.ExecuteSql(sql);
        }
        //-----后台使用
        /// <summary>
        /// 彻底删除数据库记录与相关附件
        /// </summary>
        public void DelByAdmin(string ids) //删除数据库记录与相关附件
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Select MsgID,AttachMent From " + strTableName + " Where MsgID in(" + ids + ")";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            DelAttach(dt);
            string delSql = "Delete From " + strTableName + " Where MsgID in(" + ids + ")";
            SqlHelper.ExecuteSql(delSql);
        }
    }
}
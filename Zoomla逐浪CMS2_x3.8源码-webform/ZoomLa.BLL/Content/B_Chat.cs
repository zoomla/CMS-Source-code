namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_Chat
    {
        public B_Chat()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_Chat initmod = new M_Chat();
        public M_Chat GetSelect(int ID)
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
        private M_Chat SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_Chat model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int AddChat(M_Chat model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsertAllMessage(M_Chat chat)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Chat] ([Content],[SendDatetime],[SendUserID],[AdmitUserId],[type],[All_People]) VALUES (@Content,@SendDatetime,@SendUserID,@AdmitUserId,2,',');select @@IDENTITY";
            SqlParameter[] cmdParams = chat.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

        public DataTable Select_AllMessage(string userid, int lastid)
        {
            string sqlStr = "SELECT  c.[ID],c.[Content],c.[SendDatetime],c.[SendUserID],c.[AdmitUserId],c.[type], (select u.username from zl_user as u where  c.sendUserid=u.userid),C.All_People,[roomid]   FROM [dbo].[ZL_Chat] as c where c.type=2   and c.all_people not like @userid and  ID >" + lastid + "  order by c.SendDatetime desc ";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userid", "%" + userid + "%") };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        public bool GetUpdate(M_Chat chat)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Chat] SET [Content] = @Content,[SendDatetime] = @SendDatetime,[SendUserID] = @SendUserID,[AdmitUserId] = @AdmitUserId,[type] = 1 WHERE [ID] = @ID";
            SqlParameter[] cmdParams = chat.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        public bool GetUpdates(M_Chat chat, int id, int type)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Chat] SET [Content] = @Content,[SendDatetime] = @SendDatetime,[SendUserID] = @SendUserID,[AdmitUserId] = @AdmitUserId,[type] = " + type + ", All_People=All_People+ '" + id + ",'   WHERE [ID] = @ID";
            SqlParameter[] cmdParams = chat.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public int UpdateState(int cid)
        {
            string sql = "update [Zl_chat] set state=1 where id =@Id";
            SqlParameter sp = new SqlParameter("@Id", SqlDbType.Int);
            sp.Value = cid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
        }
        public int UpdateState(string cids)
        {
            string sql = "update [Zl_chat] set state=1 where id =@Id";
            SqlParameter sp = new SqlParameter("@Id", SqlDbType.Int);
            sp.Value = cids;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
        }

        public DataTable SelectTopMessages()
        {
            string sqlStr = "SELECT top 10 c.[ID],c.[Content],c.[SendDatetime],c.[SendUserID],c.[AdmitUserId],c.[type],(select u.username from zl_user as u where c.sendUserID=u.userid) ,[roomid]  FROM [dbo].[ZL_Chat] as c  where c.type=2  order by SendDatetime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public int GetInsertRoomMessage(M_Chat chat)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Chat] ([Content],[SendDatetime],[SendUserID],[AdmitUserId],[type],[All_People],[Roomid],[state]) VALUES (@Content,@SendDatetime,@SendUserID,@AdmitUserId,@type,',',@roomid,0);select @@IDENTITY";
            SqlParameter[] cmdParams = new SqlParameter[6];
            //cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            //cmdParams[0].Value = chat.ID;
            cmdParams[0] = new SqlParameter("@Content", SqlDbType.NVarChar, 1000);
            cmdParams[0].Value = chat.Content;
            cmdParams[1] = new SqlParameter("@SendDatetime", SqlDbType.DateTime, 8);
            cmdParams[1].Value = chat.SendDatetime;
            cmdParams[2] = new SqlParameter("@SendUserID", SqlDbType.Int, 4);
            cmdParams[2].Value = chat.SendUserID;
            cmdParams[3] = new SqlParameter("@AdmitUserId", SqlDbType.Int, 4);
            cmdParams[3].Value = chat.AdmitUserId;
            cmdParams[4] = new SqlParameter("@type", SqlDbType.Int, 4);
            cmdParams[4].Value = chat.type;
            cmdParams[5] = new SqlParameter("@roomid", SqlDbType.Int);
            cmdParams[5].Value = chat.RoomId;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public DataTable SelectAllRoomM(int roomid)
        {
            string sqlStr = "SELECT  c.[ID],c.[Content],c.[SendDatetime],c.[SendUserID],c.[AdmitUserId],c.[type], (select u.username from zl_user as u where  c.sendUserid=u.userid),C.All_People,[roomid]   FROM [dbo].[ZL_Chat] as c where c.type=3   and roomid=" + roomid + "  order by c.SendDatetime desc ";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public DataTable SelectAllRoomM(int roomid, int lastid)
        {
            string sqlStr = "SELECT  c.[ID],c.[Content],c.[SendDatetime],c.[SendUserID],c.[AdmitUserId],c.[type], (select u.username from zl_user as u where  c.sendUserid=u.userid),C.All_People,[roomid]   FROM [dbo].[ZL_Chat] as c where c.type=3   and roomid=" + roomid + " and  ID >" + lastid + "  order by c.SendDatetime desc ";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 返回房间信息(1周);
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoomsm(int roomId, int lastID, int type, int uid)
        {
            string sql = "select [ID],[content],[sendDatetime],[username],c.roomId  from [Zl_chat] as c inner join [ZL_user] as u on c.sendUserID =u.userId where    c.roomId=@roomId and type=@type  and SendUserID<>@uid and  id>@lastID";
            SqlParameter[] sp = new SqlParameter[4];
            sp[0] = new SqlParameter("@roomId", SqlDbType.Int);
            sp[0].Value = roomId;
            sp[1] = new SqlParameter("@type", SqlDbType.Int);
            sp[1].Value = type;
            sp[2] = new SqlParameter("@uid", SqlDbType.Int);
            sp[2].Value = uid;
            sp[3] = new SqlParameter("@lastID", SqlDbType.Int);
            sp[3].Value = lastID;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            //  UpdateState(getcids(dt));
            updatState(dt);
            return dt;
        }
        /// <summary>
        /// 用户私聊(1周)
        /// </summary>
        public DataTable GetUsersm(int sendUid, int admitUid, int state)
        {
            string sql = "select [ID],[content],[sendDatetime],[username],[admitUserId] from [Zl_chat] as c inner join [ZL_user] as u on c.sendUserID =u.userId where c.sendUserID=@sendUid and c.admitUserId=@admitUid and c.state=@state ";
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@sendUid", SqlDbType.Int);
            sp[0].Value = sendUid;
            sp[1] = new SqlParameter("@admitUid", SqlDbType.Int);
            sp[1].Value = admitUid;
            sp[2] = new SqlParameter("@state", SqlDbType.Int);
            sp[2].Value = state;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            updatState(dt);
            return dt;
        }
        public DataTable GetAlls(int type, int lastID, int uid)
        {
            string sql = "select [ID],[content],[sendDatetime],[username] from [Zl_chat] as c inner join [ZL_user] as u on c.sendUserID =u.userId where c.type=@type and  SendUserID<>@uid and  id>@lastID";
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@type", SqlDbType.Int);
            sp[0].Value = type;
            sp[1] = new SqlParameter("@uid", SqlDbType.Int);
            sp[1].Value = uid;
            sp[2] = new SqlParameter("@lastID", SqlDbType.Int);
            sp[2].Value = lastID;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            // UpdateState(getcids(dt));
            updatState(dt);
            return dt;
        }
        public DataTable GetRoomsm(int roomId, int state, int type, string wheretime)
        {
            string sql = "select [ID],[content],[sendDatetime],[username],c.roomId  from [Zl_chat] as c inner join [ZL_user] as u on c.sendUserID =u.userId where c.roomId=@roomId and type=@type and c.state=@state and datediff(" + wheretime + ",getDate(),senddatetime)=0";
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@roomId", SqlDbType.Int);
            sp[0].Value = roomId;
            sp[1] = new SqlParameter("@type", SqlDbType.Int);
            sp[1].Value = type;
            sp[2] = new SqlParameter("@state", SqlDbType.Int);
            sp[2].Value = state;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            //  UpdateState(getcids(dt));
            return dt;
        }
        public DataTable GetUsersm(int sendUid, int admitUid, int state, string wheretime)
        {
            string sql = "select [ID],[content],[sendDatetime],[username],[admitUserId] from [Zl_chat] as c inner join [ZL_user] as u on c.sendUserID =u.userId where c.sendUserID=@sendUid and c.admitUserId=@admitUid and c.state=@state and datediff(" + wheretime + ",getDate(),senddatetime)=0";
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@sendUid", SqlDbType.Int);
            sp[0].Value = sendUid;
            sp[1] = new SqlParameter("@admitUid", SqlDbType.Int);
            sp[1].Value = admitUid;
            sp[2] = new SqlParameter("@state", SqlDbType.Int);
            sp[2].Value = state;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            return dt;
        }
        //大家()
        public DataTable GetAlls(int type, int state, string wheretime)
        {
            string sql = "select [ID],[content],[sendDatetime],[username] from [Zl_chat] as c inner join [ZL_user] as u on c.sendUserID =u.userId where c.type=@type and c.state=@state and datediff(" + wheretime + ",getDate(),senddatetime)=0";
            SqlParameter[] sp = new SqlParameter[2];
            sp[0] = new SqlParameter("@type", SqlDbType.Int);
            sp[0].Value = type;
            sp[1] = new SqlParameter("@state", SqlDbType.Int);
            sp[1].Value = state;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            return dt;
        }

        /// <summary>
        /// 删除上一周数据
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public bool DeleteChat()
        {
            string sqlStr = "delete ZL_Chat where datediff(wk,getDate(),senddatetime)=-1";
            return SqlHelper.ExecuteSql(sqlStr);
        }

        public DataTable Select_Alls(int id, int fd)
        {
            string sqlStr = "SELECT c.[ID],c.[Content],c.[SendDatetime],c.[SendUserID],c.[AdmitUserId],c.[type],(select u.username from zl_user as u  where c.SendUserID=u.userid) as 'username' ,[roomid]  FROM [dbo].[ZL_Chat] as c where c.type=0 and c.SendUserID=" + fd + " and c.AdmitUserId=" + id + "order by c.SendDatetime desc ";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public int lastId()
        {
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, "select MAX(ID) from [ZL_Chat]  "));
        }

        public DataTable SelectTopMessage(int id, int fd)
        {
            string sqlStr = "SELECT top 10 c.[ID],c.[Content],c.[SendDatetime],c.[SendUserID],c.[AdmitUserId],c.[type],(select u.username from zl_user as u where c.sendUserID=u.userid) ,[roomid]  FROM [dbo].[ZL_Chat] as c  where c.state=1 and (SendUserID=" + fd + " and AdmitUserId=" + id + ") or (SendUserID=" + id + " and AdmitUserId=" + fd + ") order by SendDatetime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        public void updatState(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    UpdateStates(Convert.ToInt32(row[0]));
                }
            }
        }

        public int UpdateStates(int cid)
        {
            string sql = "update [Zl_chat] set state=1 where id =@Id";
            SqlParameter sp = new SqlParameter("@Id", SqlDbType.Int);
            sp.Value = cid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
        }

    }
}
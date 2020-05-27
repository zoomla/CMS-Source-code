namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    public class B_ClassRoom
    {
        public string TbName, PK;
        public M_ClassRoom initMod = new M_ClassRoom();
        public DataTable dt = null;
        public B_ClassRoom()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_ClassRoom SelReturnModel(int ID)
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
       
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public bool UpdateByID(M_ClassRoom model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.RoomID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(TbName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            string sql = "DELETE FROM "+TbName+" WHERE RoomID="+ID;
            SqlHelper.ExecuteSql(sql);
            //删除关联的班级成员表
            sql = "DELETE FROM ZL_Exam_Student WHERE RoomID="+ID;
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_ClassRoom model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_ClassRoom model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetUpdate(M_ClassRoom model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.RoomID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool InsertUpdate(M_ClassRoom model)
        {
            if (model.RoomID > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        public M_ClassRoom GetSelect(int ID)
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
    
        /// <summary>
        /// 查询全部班级(用于班级管理)
        /// </summary>
        /// <param name="userid">按成员查询</param>
        /// <param name="audit">审核条件</param>
        /// <param name="name">按班级名搜索</param>
        /// <returns></returns>
        public DataTable Select_All(int userid = 0,int audit=-1,string name="")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@name", "%"+name+"%") };
            string classwhere = "";//班级查询条件
            string fields = "";//查询字段
            if (!string.IsNullOrEmpty(name))//按班级名搜索
            {
                classwhere += " AND RoomName LIKE @name";
            }
            if (userid > 0)
            {
                classwhere += " AND CreateUser=" + userid;
            }
            if (audit > -1)//查询
            {
                classwhere += " AND A.IsTrue=" + audit;
            }
            //该班级成员人数字段（按成员类别统计）
            string stufiled = ",(SELECT COUNT(*) FROM ZL_Exam_Student E WHERE A.RoomID=E.RoomID AND E.StudentType={0}) {1} ";
            string sql = "SELECT A.*"+string.Format(stufiled,1,"StuCount")+string.Format(stufiled,2,"TeachCount")+string.Format(stufiled,3,"FamilyCount") + fields + " FROM ZL_Exam_ClassView A "
                        +"WHERE 1=1 "+ classwhere + " ORDER BY A.Creation DESC";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        /// <summary>
        /// 成员检索班级
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="audit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable Select_Stu_All(int userid=0,int audit=-1,int stuaudit=-1, string name = "")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@name", "%" + name + "%") };
            string classwhere = "";//班级查询条件
            string fields = "";//查询字段
            if (!string.IsNullOrEmpty(name))//按班级名搜索
            {
                classwhere += " AND RoomName LIKE @name";
            }
            if (audit > -1)//班级审核状态
            {
                classwhere += " AND A.IsTrue="+audit;
            }
            if (userid > 0)//按成员检索
            {
                string stuwhere = "";//成员条件
                fields += ",(SELECT Auditing FROM ZL_Exam_Student WHERE userid=" + userid + " AND RoomID=A.RoomID) StuAuditing";
                if (stuaudit > -1)//成员查询条件
                {
                    stuwhere += " AND (Auditing=" + stuaudit+" OR (A.IsTrue="+ stuaudit + " AND A.CreateUser=" + userid + " ) )";//包括自己添加的班级
                }
                classwhere += " AND " + userid + " IN (SELECT UserID FROM ZL_Exam_Student WHERE RoomID=A.RoomID AND (Auditing!=-1 OR A.CreateUser="+userid+") " + stuwhere + ")";
            }
            //该班级成员人数字段（按成员类别统计）
            string stufiled = ",(SELECT COUNT(*) FROM ZL_Exam_Student E WHERE A.RoomID=E.RoomID AND E.StudentType={0}) {1} ";
            string sql = "SELECT A.*"+string.Format(stufiled,1,"StuCount")+string.Format(stufiled,2,"TeachCount")+string.Format(stufiled,3,"FamilyCount") + fields + " FROM ZL_Exam_ClassView A "
                        + "WHERE 1=1 " + classwhere + " ORDER BY A.Creation DESC";
            return SqlHelper.ExecuteTable(sql, sp);

        }
        /// <summary>
        /// 查询全部班级包括学生审核信息(用于班级申请)
        /// </summary>
        /// <param name="userid">学生id</param>
        /// <returns></returns>
        public DataTable Select_U_All(int userid, string searchkey = "")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + searchkey + "%") };
            string wherestr = "";
            if (!string.IsNullOrEmpty(searchkey))
            {
                wherestr += " AND (A.SchoolName LIKE @key OR A.GradeName LIKE @key OR A.RoomName LIKE @key)";
            }
            string sql = "SELECT A.*,(SELECT COUNT(*) FROM ZL_Exam_Student B WHERE UserID=" + userid + " AND B.RoomID=A.RoomID) AS StuCount FROM ZL_Exam_ClassView A "
                        + " WHERE A.CreateUser!=" + userid + " AND A.IsTrue=1 " + wherestr + " ORDER BY A.Creation DESC";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        /// <summary>
        /// 根据老师id或学生id查询班级
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="auditing">审核状态，-1表示不限制</param>
        /// <param name="roomid">班级id(用于班级查询条件)</param>
        /// <returns></returns>
        public DataTable SelByUid(int uid, int auditing = 1, int roomid = 0)
        {
            string classwhere = "";//班级查询条件
            string stuwhere = "";//查询学生记录条件
            if (auditing > -1)
            {
                stuwhere += " AND  B.Auditing=" + auditing;
            }
            if (roomid > 0)
            {
                classwhere += " AND A.RoomID=" + roomid;
            }
            string fields = "A.*,B.Auditing,(SELECT COUNT(*) FROM ZL_Exam_Student WHERE RoomID=A.RoomID " + stuwhere + " ) AS StuCount";
            //string sql = "SELECT " + fields + " FROM " + TbName + " A LEFT JOIN ZL_Exam_Student B ON A.RoomID=B.RoomID WHERE (B.UserID=" + userid + " OR A.CreateUser=" + userid + ")" + classwhere;
            return SqlHelper.JoinQuery(fields, TbName, "ZL_Exam_Student", "A.RoomID=B.RoomID", "B.UserID=" + uid + classwhere);
        }
        public DataTable SelectStudentRoom(string Userid)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("UserID",Userid) };
            string strSql = "SELECT * FROM " + TbName + " WHERE RoomID in (select RoomID from ZL_Student where Auditing=1 and UserID=@UserID) and IsTrue=1 ORDER BY RoomID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp); 
        }
        public DataTable SelBySchID(int id)
        {
            string strSql = "SELECT * FROM " + TbName + " WHERE  IsTrue=1 and SchoolID=" + id + " ORDER BY Creation DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);

        }
        public bool UpdateByState(string ids,int value)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE "+TbName+" SET IsTrue="+ value+" WHERE RoomID IN ("+ids+")";
            return SqlHelper.ExecuteSql(sql);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM "+TbName+" WHERE RoomID IN ("+ids+")";
            return SqlHelper.ExecuteSql(sql);
        }
    }
}

namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    /// <summary>
    /// B_Student 的摘要说明
    /// </summary>
    public class B_Student
    {
        public string TbName, PK;
        public M_Student initMod = new M_Student();
        public DataTable dt = null;
        public B_Student()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_Student SelReturnModel(int ID)
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
        /// 根据条件查询一条记录
        /// </summary>
        public M_Student SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
        public PageSetting SelPage(int cpage, int psize, int roomid = -100, int audit = -100, int type = -100, int uid = -100)
        {
            string where = " 1=1";
            if (roomid != -100) { where += " AND RoomID=" + roomid; }
            if (audit != -100) { where += " AND Auditing=" + audit; }
            if (type != -100) { where += " AND StudentType=" + type; }
            if (uid != -100) { where += " AND UserID=" + uid; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 查询班级成员列表
        /// </summary>
        /// <param name="roomid">班级id</param>
        /// <param name="Auditing">按审核状态查询</param>
        /// <param name="stutype">按成员类型查询 1:学生、2:教师</param>
        /// <param name="userid">按用户id查询</param>
        /// <returns></returns>
        public DataTable SelByURid(int roomid, int Auditing = -1, int stutype = 0, int userid = 0)
        {
            string strwhere = "";
            if (userid > 0)
            {
                strwhere += " AND UserID=" + userid;
            }
            if (Auditing > -1)//按状态
            {
                strwhere += " AND Auditing=" + Auditing;
            }
            if (stutype > 0)
            {
                strwhere += " AND StudentType=" + stutype;
            }
            string sql = "SELECT * FROM " + TbName + " WHERE RoomID=" + roomid + strwhere;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 审核操作
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public bool AuditStatus(string ids, bool audit)
        {
            string wherestr = " AND Auditing!=-1";//班级创建者(班主任)无需审核
            int status = audit ? 1 : 0;
            return UpDateStatus(ids, status, wherestr);
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpDateStatus(string ids, int value)
        {
            return UpDateStatus(ids, value, "");
        }
        /// <summary>
        /// 修改状态（带条件）
        /// </summary>
        /// <returns></returns>
        private bool UpDateStatus(string ids, int value, string wherestr = "")
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + TbName + " SET Auditing=" + value + " WHERE Noteid IN (" + ids + ")" + wherestr;
            return SqlHelper.ExecuteSql(sql);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE Noteid IN (" + ids + ") AND Auditing!=-1";
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }

        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_Student model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Noteid.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(TbName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(TbName, strWhere);
        }

        public int insert(M_Student model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public bool GetInsert(M_Student model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Student model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Noteid.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_Student model)
        {
            if (model.Noteid > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public M_Student GetSelect(int ID)
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
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(TbName, "", "");
        }
        public DataTable SelByValue(string key)
        {
            // DataTable dt = st.Select_ByValue(" * ", " UserID in (select UserID from ZL_UserBase where TrueName like '%" + txtName.Text + "%') ", "");
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + key + "%") };
            string sql = "Select * From " + TbName + " Where UserID in (select UserID from ZL_UserBase where TrueName like @key)";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

    }
}

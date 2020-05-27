namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;    /// <summary>
                         /// 用户角色权限
                         /// </summary>
    public class B_Permission
    {
        private string TbName, PK;
        private M_Permission initMod = new M_Permission();
        public B_Permission()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Permission SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool GetUpdate(M_Permission model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int GetInsert(M_Permission model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool InsertUpdate(M_Permission model)
        {
            if (model.ID > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        public M_Permission GetSelect(int ID)
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
        public DataTable SelByRole(string rname)
        {
            string sql = "Select * From " + TbName + " Where RoleName Like @rname";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("rname","%"+rname+"%") };
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }
        public DataTable Select_All()
        {
            return Sql.Sel(TbName);
        }
        public DataTable SelByUserGrop(string usergrop)
        {
            string sql = "SELECT Perlist,Precedence FROM " + TbName + " WHERE UserGroup LIKE @usergroup AND IsTrue=1";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@usergroup", "%"+usergrop+"%") };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        /// <summary>
        /// 节点权限
        /// </summary>
        /// <param name="Nodeid"></param>
        /// <param name="Usergroup"></param>
        /// <returns></returns>
        private string Nodeinfo(int Usergroup, int type, string str)
        {
            DataTable dtNode = new DataTable();

            try
            {
                DataTable ptable = SelByUserGrop(Usergroup.ToString());
                if (ptable != null && ptable.Rows.Count > 0)
                {
                    DataRow[] dr;
                    DataSet ds = new DataSet();
                    DataTable dts = new DataTable();

                    for (int i = 0; i < ptable.Rows.Count; i++)
                    {
                        ds = function.XmlToTable(ptable.Rows[i]["Perlist"].ToString());
                        DataTable ddt = ds.Tables[0];
                        ddt.Columns.Add("Precedence");
                        dts = ddt.Clone();
                        if (ds.Tables.Count > 0)
                        {
                            for (int j = 0; j < ddt.Rows.Count; j++)
                            {
                                ddt.Rows[j]["Precedence"] = ptable.Rows[i]["Precedence"];
                                dts.Rows.Add(ddt.Rows[j].ItemArray);
                            }
                        }
                    }
                    switch (type)
                    {
                        case 1:
                            dr = dts.Select("Nodelist like '%," + str + "'");
                            dtNode = GetDt(dr);
                            break;
                        case 2:
                            dr = dts.Select("DataList like '%," + str + "'");
                            dtNode = GetDt(dr);
                            break;
                        case 3:
                            dr = dts.Select("Fieldlist like '%," + str + "'");
                            dtNode = GetDt(dr);
                            break;
                    }
                }
                return GetPerByTable(dtNode);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将DataRow[]转成DataTable
        /// </summary>
        private DataTable GetDt(DataRow[] dr)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < dr.Length; i++)
            {
                //dt.Rows.Add(dr[i]);
                //dt.Rows.Add(dr[i].ItemArray);
                dt.ImportRow(dr[i]);
            }
            dt.Columns.Add("Precedence");
            return dt;
        }
        /// <summary>
        /// 字段权限
        /// </summary>
        /// <param name="ModelId">模型ID</param>
        /// <param name="fieldsname">字段名称</param>
        /// <param name="Usergroup">用户组</param>
        private string Fieldsinfo(int ModelId, string fieldsname, int Usergroup)
        {
            B_Permission pll = new B_Permission();
            int Fieldid = 0;

            #region 从模型表中找到字段的ID
            B_ModelField mfll = new B_ModelField();
            DataTable mftable = mfll.SelectTableName("ZL_ModelField", "FieldName = '" + fieldsname + "' and ModelId=" + ModelId + "");

            if (mftable.Rows.Count > 0)
            {
                Fieldid = DataConverter.CLng(mftable.Rows[0]["FieldID"]);
            }
            else
            {
                Fieldid = 0;
            }
            #endregion

            return Nodeinfo(Usergroup, 3, Fieldid.ToString());
        }
        /// <summary>
        /// 返回模型字段权限实体
        /// </summary>
        /// <param name="ModelId"></param>
        /// <param name="fieldsname"></param>
        /// <param name="Usergroup"></param>
        /// <returns></returns>
        public M_Permission Get_Field(int ModelId, string fieldsname, int Usergroup)
        {
            string Fieldstr = Fieldsinfo(ModelId, fieldsname, Usergroup);
            return CheckStr(Fieldstr);
        }

        /// <summary>
        /// 返回用户模型权限实体
        /// </summary>
        /// <param name="ModelID"></param>
        /// <param name="Usergroup"></param>
        /// <returns></returns>
        public M_Permission Get_Data(int ModelID, int Usergroup)
        {
            string Datastr = Nodeinfo(Usergroup, 2, ModelID.ToString());
            return CheckStr(Datastr);
        }

        /// <summary>
        /// 返回节点权限实体
        /// </summary>
        /// <param name="Nodeid"></param>
        /// <param name="Usergroup"></param>
        /// <returns></returns>
        public M_Permission Get_Node(int Nodeid, int Usergroup)
        {
            try
            {
                string nodestr = Nodeinfo(Usergroup, 1, Nodeid.ToString());
                return CheckStr(nodestr);
            }
            catch
            {
                return new M_Permission();
            }
        }

        private M_Permission CheckStr(string str)
        {
            return null;
        }

        /// <summary>
        /// 获得权限方法重构
        /// </summary>
        /// <param name="ptable"></param>
        /// <returns></returns>
        private static string GetPerByTable(DataTable ptable)
        {
            string Nodepermisser = string.Empty;
            if (ptable.Rows.Count > 0)
            {
                #region 权限判断
                int Maxvalue = 0;//优先初始值
                DataRow[] maxtable = ptable.Select("", "Precedence desc");//查询优先级降幂排序
                Maxvalue = DataConverter.CLng(maxtable[0]["Precedence"].ToString());//取第一个优先级为最大优先
                DataRow[] newtable = ptable.Select("Precedence=" + Maxvalue + "");
                bool[] basepermiss = { true, true, true, true, true, true };//权限初始值

                //遍历所有最大优先级的角色权限
                for (int i = 0; i < newtable.Length; i++)
                {
                    string Pagelist = ptable.Rows[i]["Pagelist"].ToString();//取得权限
                    Pagelist = Pagelist.TrimEnd(new char[] { ',' });
                    Pagelist = Pagelist.TrimStart(new char[] { ',' });
                    if (!string.IsNullOrEmpty(Pagelist))
                    {
                        if (Pagelist.IndexOf(",") > -1)
                        {
                            string[] pagelistarr = Pagelist.Split(new string[] { "," }, StringSplitOptions.None);

                            //比较权限
                            for (int ii = 0; ii < pagelistarr.Length; ii++)
                            {
                                basepermiss[ii] = DataConverter.CBool(pagelistarr[ii]) && basepermiss[ii];
                            }
                        }
                    }
                }
                #region 重组权限
                for (int ie = 0; ie < basepermiss.Length; ie++)
                {
                    if (ie < basepermiss.Length - 1)
                    {
                        if (basepermiss[ie] == true)
                        {
                            Nodepermisser = Nodepermisser + "1" + ",";
                        }
                        else
                        {
                            Nodepermisser = Nodepermisser + "0" + ",";
                        }
                    }
                    else
                    {
                        if (basepermiss[ie] == true)
                        {
                            Nodepermisser = Nodepermisser + "1";
                        }
                        else
                        {
                            Nodepermisser = Nodepermisser + "0";
                        }
                    }
                }
                #endregion
            }
            return Nodepermisser == string.Empty ? "1,1,1,1,1,1" : Nodepermisser;
                #endregion
        }

        /// <summary>
        /// 传入1,2,3，返回角色名
        /// </summary>
        public string GetRoleNameByIDs(string ids)
        {
            ids = ids ?? "";
            ids = ids.Trim(',');
            string result = "";
            if (string.IsNullOrEmpty(ids))return result;
            SafeSC.CheckIDSEx(ids);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select RoleName From " + TbName + " Where ID in(" + ids + ")");
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["RoleName"] as string + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }
        //----------------------OA
        /// <summary>
        /// 根据用户权限datatable,判断权限
        /// </summary>
        /// <param name="mu"></param>
        /// <param name="authdt">用权限限DT</param>
        /// <returns>True:有权限</returns>
        public bool CheckAuth(DataTable authdt,string auth)
        {
            if (authdt == null || authdt.Rows.Count < 1 || string.IsNullOrEmpty(auth)) return false;
            foreach (DataRow row in authdt.Rows)
            {
                string[] rolestr = row["Auth_OA"].ToString().Split(',');
                foreach (var str in rolestr)
                {
                    if (str.ToLower().Equals(auth.ToLower()))
                        return true;
                }
                
            }
            return false;
        }
        public bool CheckAuth(string rids, string auth)
        {
            DataTable authdt = SelAuthByRoles(rids);
            return CheckAuth(authdt, auth);
        }
        /// <summary>
        /// 是否包含允许角色
        /// </summary>
        /// <param name="authrids">已授权的用户角色IDS</param>
        /// <param name="myrids">用户所拥有的角色IDS</param>
        /// <returns>True通过</returns>
        public bool ContainRole(string authrids, string myrids)
        {
            if (string.IsNullOrEmpty(authrids)) return true;
            if (string.IsNullOrEmpty(myrids)) return false;
            string[] ridArr = myrids.Trim(',').Split(',');
            foreach (string rid in ridArr)
            {
                if (authrids.Contains("," + rid + ",")) { return true; }
            }
            return false;
        }
        /// <summary>
        /// 根据用户角色IDS,返回用户的权限表
        /// </summary>
        /// <param name="rids"></param>
        /// <returns></returns>
        public DataTable SelAuthByRoles(string rids) 
        {
            rids = rids.Trim(',').Replace(",,", ",");
            if (string.IsNullOrEmpty(rids)) return null;
            SafeSC.CheckDataEx(rids);
            string sql = "SELECT * FROM "+TbName+" WHERE ID IN ("+rids+")";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        //-------------------用户权限验证
        /// <summary>
        /// 对当前登录用户验证权限
        /// </summary>
        /// <param name="auth">需要验证的权限</param>
        /// <returns>True:通过</returns>
        public static void CheckAuthEx(string auth)
        {
            M_UserInfo mu = new B_User().GetLogin();
            if (!new B_Permission().CheckAuth(mu.UserRole, auth)) 
            {
                function.WriteErrMsg("你当前没有访问该页面的权限");
            }
        }
        /// <summary>
        /// 检测当前登录用户是否有指定权限
        /// </summary>
        /// <returns>True:拥有</returns>
        public static bool CheckAuth(string auth)
        {
            M_UserInfo mu = new B_User().GetLogin();
            return new B_Permission().CheckAuth(mu.UserRole, auth);
        }
    }
}
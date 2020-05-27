namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Web.UI.WebControls;
    using SQLDAL.SQL;
    public class B_Spec
    {
        private string strTableName,PK;
        private M_Spec initMod = new M_Spec();
        public B_Spec() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelAllByListBox()
        {
            DataTable dt = Sel();
            DataTable result = dt.Clone();
            AddToDT(dt, result, 0, 0);
            return result;
        }
        private void AddToDT(DataTable dt,DataTable result,int pid,int detp)
        {
            DataRow[] drs = dt.Select("Pid=" + pid);
            for (int i = 0; i < drs.Length; i++)
            {
                drs[i]["SpecName"] = GetLevelStr("　", "|-", detp+1) + drs[i]["SpecName"];
                result.ImportRow(drs[i]);
                AddToDT(dt, result,DataConvert.CLng(drs[i]["SpecID"]), detp+1);
            }
        }
        public bool UpdatePidByIDS(string ids,int id)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + strTableName + " SET Pid=" + id + " WHERE SpecID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        private string GetLevelStr(string pre, string preChar, int level)
        {
            string str = "";
            for (int i = 1; i < level; i++)
            {
                str += pre;
            }
            return (str += preChar);
        }
        public M_Spec SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
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
        public void InitTreeNode(TreeNodeCollection Nds,int pid=0,string PDesc="")
        {
            TreeNode tmpNd;
            DataTable dtcate = GetSpecList(pid);
            PDesc += pid > 0 ? GetSpec(pid).SpecName + ">>" : "";
            foreach (DataRow dr in dtcate.Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["SpecID"].ToString();
                tmpNd.Text = dr["SpecName"].ToString();
                tmpNd.NavigateUrl = "javascript:SetSpec('" + PDesc + dr["SpecName"]+"','"+ dr["SpecID"]  +"');";
                tmpNd.Target = "";
                Nds.Add(tmpNd);
                InitTreeNode(tmpNd.ChildNodes, DataConvert.CLng(dr["SpecID"]), PDesc);
                //InitSpecTree(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value));
            }
        }
        private M_Spec SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
            return Sql.Sel(strTableName,"","OrderID");
        }
        public bool UpdateByID(M_Spec model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.SpecID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool DelBySpecID(int id)
        {
            string sql = "Delete From "+strTableName+" Where Specid="+id;
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_Spec model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public void AddSpec(M_Spec model)
        {
            if (model.SpecID > 0)
                UpdateByID(model);
            else
                insert(model);
        }
        public void UpdateSpec(M_Spec model)
        {
            if (model.SpecID > 0)
                UpdateByID(model);
            else
                insert(model);
        }
        public void DelSpec(int ID)
        {
            Sql.Del(strTableName, PK + "=" + ID);
        }
        public int GetFirstID()
        {
            string strSql = "Select Top 1 SpecID from ZL_Special Order by OrderID ASC";
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
        }
        public M_Spec GetSpec(int SpecID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@SpecID", SqlDbType.Int, 4) };
            cmdParams[0].Value = SpecID;
            string strSql = "select * from ZL_Special where SpecID=@SpecID";
            using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (drd.Read())
                {
                    return initMod.GetModelFromReader(drd);
                }
                else
                {
                    return new M_Spec(true);
                }
            }
        }
        public DataTable GetSpecList(int Pid=0)
        {
            string strSql = "select A.*,(Select Count(*) From ZL_Special Where Pid=A.SpecID) as ChildCount,(SELECT COUNT(*) FROM ZL_CommonModel B WHERE (','+B.SpecialID+',') LIKE ('%,'+CAST(A.SpecID AS varchar)+',%')) AS ContentCount From ZL_Special A Where Pid=" + Pid+" ORDER BY OrderID";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        public DataTable GetSpecAll(string skey="")
        {
            int specID = DataConvert.CLng(skey);
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("@skey","%"+skey+"%") };
            string wherestr = specID<=0 ? " AND SpecName LIKE @skey" : " AND SpecID="+specID;
            string strSql = "select * from ZL_Special WHERE 1=1 "+wherestr+" Order by OrderID ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 主用于生成发布
        /// </summary>
        public DataTable SelAsNode()
        {
            string sql = "SELECT (SELECT Count(SpecID) From " + strTableName + " WHERE A.SpecID=Pid) ChildCount,SpecID AS NodeID,SpecName AS NodeName,SpecDir AS NodeDir,NodeType=10 FROM " + strTableName + " A ORDER BY OrderID";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 查询专题名是否存在
        /// </summary>
        /// <param name="SpecName"></param>
        /// <returns></returns>
        public DataTable GetIsSpecName(string SpecName)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("SpecName", SpecName) };
            string strSql = "select * from ZL_Special where SpecName=@SpecName";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable GetIsSpecDir(string specdir)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("specdir",specdir) };
            string sql = "SELECT * FROM ZL_Special WHERE SpecDir=@specdir";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable GetSpecContent(int SpecID, string Order, string Conditions)
        {
            SafeSC.CheckDataEx(Order, Conditions);
            string strSql = string.Format("select a.* from ZL_CommonModel a where a.SpecialID Like @SpecID order by {0} {1}", Order, Conditions);
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SpecID","%,"+SpecID+",%")
            };
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable SelByIDS(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                SafeSC.CheckIDSEx(ids);
                ids = ids.TrimEnd(new char[] { ',' });
                string sql = "Select * From " + strTableName + " Where SpecID IN (" + ids + ")";
                return SqlHelper.ExecuteTable(CommandType.Text, sql);
            }
            return null;
        }
        //-------------------------------------------
        public void AddToSpec(string ids, int specid)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("specid",","+specid+",") };
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE ZL_CommonModel SET SpecialID = REPLACE(REPLACE(SpecialID,@specid,','),',,',',')+@specid WHERE GeneralID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql,sp);
        }
        public int GetNextOrderID(int pid)
        {
            string sql = "SELECT TOP 1 OrderID FROM "+strTableName+" WHERE Pid="+pid+" ORDER BY OrderID DESC";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            if (dt.Rows.Count > 0) { int orderid = DataConvert.CLng(dt.Rows[0]["OrderID"]);return orderid + 1; }
            return 1;
        }
    }
}
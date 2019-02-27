namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_PayPlat
    {
        private string strTableName,PK;
        private M_PayPlat initMod = new M_PayPlat();
        public B_PayPlat() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_PayPlat SelReturnModel(int ID)
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
        public M_PayPlat SelModelByClass(M_PayPlat.Plat type)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, " WHERE PayClass=" + (int)type))
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
        /// 获取微信支付配置(payclass=21)
        /// </summary>
        public static M_PayPlat GetModelForWx()
        {
            return new B_PayPlat().SelModelByClass(M_PayPlat.Plat.WXPay);
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
        public bool UpdateByID(M_PayPlat model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.PayPlatID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_PayPlat model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        /// <summary>
        /// 添加平台
        /// </summary>
        public bool Add(M_PayPlat model)
        {
            if (model.PayPlatID > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        /// <summary>
        /// 修改平台
        /// </summary>
        public bool Update(M_PayPlat model)
        {
            if (model.PayPlatID > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        /// <summary>
        /// 删除平台
        /// </summary>
        public bool DeleteByID(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        /// <summary>
        /// 读取平台实例
        /// </summary>
        public M_PayPlat GetPayPlatByid(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable GetNodeChildList(int ParentID)
        {
            string strSql = "select * from ZL_PayPlat where PayPlatID=@PayPlatID Order by PayTime";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@PayPlatID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            return SqlHelper.ExecuteDataSet(CommandType.Text, strSql, cmdParams).Tables[0];
        }
        /// <summary>
        /// 读取所有启用的平台
        /// </summary>
        public DataTable GetPayPlatAll()
        {
            return Sel();
        }
        /// <summary>
        /// 获取系统配置的支付平台
        /// </summary>
        public DataTable GetSysPayPlat()
        {
            string cmdText = "SELECT * FROM [ZL_PayPlat] WHERE [IsDisabled]=0 AND [UID]=0 ORDER BY [IsDefault] DESC,[OrderID] ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, cmdText, null);
        }
        /// <summary>
        /// cha ID
        /// </summary>
        public M_PayPlat GetPayID()
        {
            M_PayPlat pp = new M_PayPlat();
            string strSql = "select * from ZL_PayPlat";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text,strSql))
            {
                if (reader.Read())
                {
                    pp.UID = Convert.ToInt32(reader["UID"]);
                    pp.PayPlatName = Convert.ToString(reader["PayPlatName"]);
                }
            }
            return pp;
        }
        /// <summary>
        /// 读取所有平台列表
        /// </summary>
        public DataTable GetPayPlatListAll(int flag = -1)//All,Enable,Disabled
        {
            string wherestr = "";
            if (flag > -1) { wherestr = "AND IsDisabled=" + flag; }
            string strSql = "SELECT * FROM ZL_PayPlat where UID=0 " + wherestr + " ORDER BY IsDefault DESC,OrderID ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 最大排序序号
        /// </summary>
        public int GetMaxOrder()
        {
            string strSql = "select Max(OrderID) from ZL_PayPlat";
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
        }
        /// <summary>
        /// 最小排序序号
        /// </summary>
        public int GetMinOrder()
        {
            string strSql = "select Min(OrderID) from ZL_PayPlat";
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
        }
        /// <summary>
        /// 当前平台排序前一个平台实例
        /// </summary>
        public M_PayPlat GetPrePayPlat(int OrderID)
        {
            string strSql = "select Top 1 PayPlatID from ZL_PayPlat where OrderID<@CurrentID order by OrderID desc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@CurrentID",SqlDbType.Int,4)
            };
            cmdParam[0].Value = OrderID;
            int Pid = DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
            return GetPayPlatByids(Pid);
        }

        /// <summary>
        /// 读取平台实例
        /// </summary>
        M_PayPlat GetPayPlatByids(int ID)
        {
            return SelReturnModel(ID);
        }
        /// <summary>
        /// 当前平台排序后一个平台实例
        /// </summary>
        public M_PayPlat GetNextPayPlat(int OrderID)
        {
            string strSql = "select top 1 PayPlatID from ZL_PayPlat where OrderID>@CurrentID order by OrderID asc";
            //string strSql = "select Top 1 PayPlatID from ZL_PayPlat where OrderID<@CurrentID order by OrderID desc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@CurrentID",SqlDbType.Int,4)
            };
            cmdParam[0].Value = OrderID;
            int nextid = DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
            return GetPayPlatByids(nextid);
        }
        /// <summary>
        /// 将当前平台前移
        /// </summary>
        public void MovePre(int PayPlatID)
        {
            M_PayPlat curr = GetPayPlatByid(PayPlatID);
            if (curr.OrderID > GetMinOrder())
            {
                M_PayPlat pre = GetPrePayPlat(curr.OrderID);
                int temp = pre.OrderID;
                pre.OrderID = curr.OrderID;
                curr.OrderID = temp;
                if (Update(curr))
                {
                    Update(pre);
                }
            }
        }
        /// <summary>
        /// 将当前平台后移
        /// </summary>
        public void MoveNext(int PayPlatID)
        {
            M_PayPlat curr = GetPayPlatByid(PayPlatID);
            if (curr.OrderID < GetMaxOrder())
            {
                M_PayPlat next = GetNextPayPlat(curr.OrderID);
                int temp = next.OrderID;
                next.OrderID = curr.OrderID;
                curr.OrderID = temp;
                if (Update(curr))
                {
                    Update(next);
                }
            }
        }
        public bool SetDefault(int ID)
        {
            DBCenter.UpdateSQL(strTableName, "IsDefault=0", "");
            return DBCenter.UpdateSQL(strTableName, "IsDefault=1,IsDisabled=0", "PayPlatID=" + ID);
        }
        /// <summary>
        /// 根据登录的UID读取所有平台列表
        /// </summary>
        public DataTable GetPayPlatID(int uid)
        {
            string strSql = "select * from ZL_PayPlat where UID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable GetPayPlatByClassid(int payclassid)
        {
            DataTable dt = new DataTable();
            string cmd = "select * from ZL_PayPlat where Payclass=@payclassid";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@payclassid", SqlDbType.Int) };
            para[0].Value = payclassid;
            dt = SqlHelper.ExecuteTable(CommandType.Text, cmd, para);
            return dt;
        }
        /// <summary>
        /// 用于支付宝购网银
        /// </summary>
        public M_PayPlat GetPayPlatByClassID(string payClass)
        {
            //只返回第一个
            string strSql = "Select Top 1 * From " + strTableName + " Where PayClass = @PayClass";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("payClass",SqlDbType.Int,4)};
            cmdParam[0].Value = payClass;
            
            int nextid = DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
            return GetPayPlatByids(nextid);
        }
    }
}
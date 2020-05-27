using System;
using System.Collections.Generic;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Xml;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_UserStoreTable
    {
        public string strTableName = "";
        public string PK = "";
        private M_UserStoreTable initMod = new M_UserStoreTable();
        public B_UserStoreTable()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_UserStoreTable GetStoreByID(int ID)
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
        /// <summary>
        /// 查询所有记录
        /// </summary>
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
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateStoreTable(M_UserStoreTable model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_UserStoreTable model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        #region 申请开通店铺
        /// <summary>
        /// 申请开通店铺(成功返回true)
        /// </summary>
        /// <param name="ust"></param>
        /// <returns></returns>
        public bool InsertStoreTable(M_UserStoreTable ust)
        {
            try
            {
                string sql = @"INSERT INTO [ZL_UserStoreTable] (
	[UserID],
	[StoreName],
	[StoreContent],
	[StoreCredit],
	[StoreVip],
	[StoreType],
	[StoreCash],
	[StoreState],
	[AddTime],
	[StoreProvince],
	[StoreCity]
) VALUES (
	@UserID,
	@StoreName,
	@StoreContent,
	@StoreCredit,
	@StoreVip,
	@StoreType,
	@StoreCash,
	@StoreState,
	@AddTime,
	@StoreProvince,
	@StoreCity
)";
                SqlParameter[] parameter ={ 
                    new SqlParameter("UserID",ust.UserID),
                    new SqlParameter("StoreName",ust.StoreName),
                    new SqlParameter("StoreContent",ust.StoreContent),
                    new SqlParameter("StoreCredit",ust.StoreCredit),
                    new SqlParameter("StoreVip",ust.StoreVip),
                    new SqlParameter("StoreType",ust.StoreType),
                    new SqlParameter("StoreCash",ust.StoreCash),
                    new SqlParameter("StoreState",ust.StoreState),
                    new SqlParameter("StoreProvince",ust.StoreProvince),
                    new SqlParameter("StoreCity",ust.StoreCity),
                    new SqlParameter("AddTime",DateTime.Now)
                };
                if (SqlHelper.ExecuteScalar(CommandType.Text, sql, parameter) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region 根据用户ID得到店铺信息
        /// <summary>
        /// 根据用户ID得到店铺信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public M_UserStoreTable GetStoreTableByUserID(int userid)
        {
            try
            {
                string sql = @"select * from ZL_UserStoreTable where UserID=@userid";
                SqlParameter[] parameter = { new SqlParameter("userid", userid) };
                M_UserStoreTable ust = new M_UserStoreTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadStoreTable(dr, ust);
                    }
                    dr.Close();
                    dr.Dispose();
                }
                return ust;
            }
            catch
            {
                throw;
            }
        }
        public static void ReadStoreTable(SqlDataReader dr, M_UserStoreTable ust)
        {
            ust.ID = int.Parse(dr["ID"].ToString());
            ust.UserID = int.Parse(dr["UserID"].ToString());
            ust.StoreName = dr["StoreName"].ToString();
            ust.StoreContent = dr["StoreContent"].ToString();
            ust.StoreProvince = dr["StoreProvince"].ToString();
            ust.StoreCity = dr["StoreCity"].ToString();
            ust.StoreType = dr["StoreType"].ToString();
            ust.StoreVip = int.Parse(dr["StoreVip"].ToString());
            ust.StoreCredit = int.Parse(dr["StoreCredit"].ToString());
            ust.StoreCash = decimal.Parse(dr["StoreCash"].ToString());
            ust.AddTime = DateTime.Parse(dr["AddTime"].ToString());
            ust.StoreState = int.Parse(dr["StoreState"].ToString());
            ust.ID = int.Parse(dr["ID"].ToString());
            //dr.Close();
        }
        #endregion

        #region 读取店铺类型
        /// <summary>
        /// 读取店铺类型
        /// </summary>
        /// <returns></returns>
        public List<M_UserStoreTypeTable> GetStoreType()
        {
            try
            {
                string sql = @"select * from ZL_UserStoreTypeTable";
                //return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
                List<M_UserStoreTypeTable> list = new List<M_UserStoreTypeTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
                {
                    while (dr.Read())
                    {
                        M_UserStoreTypeTable ustt = new M_UserStoreTypeTable();
                        ustt.ID = int.Parse(dr["ID"].ToString());
                        ustt.TypeName = dr["TypeName"].ToString();
                        list.Add(ustt);
                    }
                    dr.Close();
                    dr.Dispose();
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据ID读取类型
        /// <summary>
        /// 根据ID读取类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public M_UserStoreTypeTable GetStoreTypeByID(int id)
        {
            try
            {
                string sql = @"select * from ZL_UserStoreTypeTable where ID=@id";
                SqlParameter[] parameter = { new SqlParameter("id", id) };
                M_UserStoreTypeTable ustt = new M_UserStoreTypeTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ustt.ID = int.Parse(dr["ID"].ToString());
                        ustt.TypeName = dr["TypeName"].ToString();
                    }
                    dr.Close();
                    dr.Dispose();
                }
                return ustt;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 选择城市


        public List<Pcity> ReadCity(string xmlurl, string fcode)
        {
            List<Pcity> provinceList = new List<Pcity>();
            XmlDocument xdd = new XmlDocument();
            xdd.Load(xmlurl);
            XmlReader dr = new XmlNodeReader(xdd);
            while (dr.Read())
            {
                if (dr.NodeType == XmlNodeType.Element)
                {
                    if (dr.Name == "city")
                    {
                        Pcity pr = new Pcity();
                        if (dr.MoveToAttribute("fcode"))
                        {
                            if (dr.Value == fcode)
                            {
                                if (dr.MoveToAttribute("name"))
                                {
                                    pr.Name = dr.Value;
                                }
                                if (dr.MoveToAttribute("code"))
                                {
                                    pr.Fcode = fcode;
                                    pr.Code = dr.Value;
                                    provinceList.Add(pr);
                                }
                            }
                        }

                    }
                }
            }
            return provinceList;

        }

        public Pcity GetCityByCode(string xmlurl, string fcode, string code)
        {
            Pcity pc = new Pcity();
            XmlDocument xdd = new XmlDocument();
            xdd.Load(xmlurl);
            XmlReader dr = new XmlNodeReader(xdd);
            while (dr.Read())
            {
                if (dr.NodeType == XmlNodeType.Element)
                {
                    if (dr.Name == "city")
                    {

                        if (dr.MoveToAttribute("fcode"))
                        {
                            if (dr.Value == fcode)
                            {
                                pc.Fcode = dr.Value;
                                if (dr.MoveToAttribute("code"))
                                {
                                    if (dr.Value == code)
                                    {
                                        pc.Code = dr.Value;
                                        if (dr.MoveToAttribute("name"))
                                        {
                                            pc.Name = dr.Value;
                                            return pc;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return pc;
        }


        #endregion

        #region 所有店铺分类型
        /// <summary>
        /// 所有店铺分类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public DataTable GetStoreByState(int state)
        {
            try
            {
                string sql = "select * from ZL_UserStoreTable where StoreState=" + state + " order by AddTime desc";
                return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据搜索查询店铺
        /// <summary>
        /// 根据搜索查询店铺
        /// </summary>
        /// <param name="state"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public DataTable GetStoreBySearch(int state, int type, string key)
        {
            try
            {
                string sql = null;
                switch (type)
                {
                    case 2: sql = "SELECT ZL_User.UserName, ZL_UserStoreTable.* FROM ZL_User INNER JOIN ZL_UserStoreTable ON ZL_User.UserID = ZL_UserStoreTable.UserID where ZL_UserStoreTable.StoreState=" + state + " and ZL_User.StoreName like %'" + key + "'% order by ZL_UserStoreTable.AddTime desc"; break;

                    case 3: sql = "SELECT ZL_User.UserName, ZL_UserStoreTable.* FROM ZL_User INNER JOIN ZL_UserStoreTable ON ZL_User.UserID = ZL_UserStoreTable.UserID where ZL_UserStoreTable.StoreState=" + state + " and ZL_User.UserName like %'" + key + "'% order by ZL_UserStoreTable.AddTime desc"; break;
                }
                return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改店铺状态
        /// <summary>
        /// 修改店铺状态
        /// </summary>
        /// <param name="state"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateStoreInManage(int state, string list)
        {
            try
            {
                string sql = "update ZL_UserStoreTable set StoreState=" + state + " where (ID in (" + list + "))";
                if (SqlHelper.ExecuteScalar(CommandType.Text, sql, null) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除店铺
        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="id"></param>
        public void DelStoreByID(int id)
        {
            try
            {
                string sql = "delect from ZL_UserStoreTypeTable where id=" + id;
                SqlHelper.ExecuteScalar(CommandType.Text, sql, null);
            }
            catch { }
        }
        #endregion


        #region 批量删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        public void DelStoreInManage(string list)
        {
            try
            {
                string sql = "Delete ZL_UserStoreTable  where (ID in (" + list + "))";
                SqlHelper.ExecuteScalar(CommandType.Text, sql, null);

            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
    #region 省份实体


    /// <summary>
    /// 城市
    /// </summary>
    public class Pcity
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string fcode;

        public string Fcode
        {
            get { return fcode; }
            set { fcode = value; }
        }
    }
    #endregion
}
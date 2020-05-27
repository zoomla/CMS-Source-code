
namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using Newtonsoft.Json;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    using ZoomLa.Model.Shop;
    using ZoomLa.BLL.Shop;
    using ZoomLa.Components;
    public class B_Product
    {
        private string TbName, PK;
        private M_Product initMod = new M_Product();
        public B_Product()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        ///  改为返回ZL_Commidites表中数据
        /// </summary>
        /// <param name="table">扩展字段</param>
        /// <param name="m_Product">商品模型</param>
        /// <param name="CCate">CommonData</param>
        public int Add(DataTable table, M_Product m_Product, M_CommonData CCate)
        {
            B_Content conBll = new B_Content();
            CCate.Status = 99;
            string FieldList = "";//key
            string Valuelist = "";
            SqlParameter[] ps = new SqlParameter[table.Rows.Count];
            int si = 0;
            if (table != null)
            {
                foreach (DataRow dr in table.Rows)
                {

                    if (string.IsNullOrEmpty(FieldList))
                    {
                        FieldList = "[" + dr["FieldName"].ToString() + "]";
                        Valuelist = "@" + dr["FieldName"].ToString();
                        ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), Sql.GetFieldValue(dr));
                    }
                    else
                    {
                        FieldList = FieldList + ",[" + dr["FieldName"].ToString() + "]";
                        Valuelist = Valuelist + ",@" + dr["FieldName"].ToString();
                        ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), Sql.GetFieldValue(dr));
                    }

                    if (dr["FieldType"].ToString() == "DateType")
                    {
                        if (Sql.GetFieldValue(dr) == "")
                        {
                            ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlDateTime.Null);
                        }
                        else
                        {
                            if (!IsDate(Sql.GetFieldValue(dr)))
                            {
                                ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlDateTime.Null);
                            }
                            else
                            {
                                ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), Sql.GetFieldValue(dr));
                            }
                        }
                    }
                    if (dr["FieldType"].ToString() == "int" || dr["FieldType"].ToString() == "NumType" || dr["FieldType"].ToString() == "float" || dr["FieldType"].ToString() == "money")
                    {
                        if (Sql.GetFieldValue(dr) == "")
                        {
                            ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlInt32.Null);
                        }
                    }

                    si = si + 1;
                }
            }
            //插入扩展与CommonModel
            int ItemID = 0;
            if (!string.IsNullOrEmpty(FieldList) && !string.IsNullOrEmpty(Valuelist) && !string.IsNullOrEmpty(m_Product.TableName))
            {
                string strSql = "Insert Into " + m_Product.TableName + " (" + FieldList + ") values (" + Valuelist + ");select @@IDENTITY AS newID";
                ItemID = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, ps));
            }
            int ComModelID = 0;
            if (CCate != null)
            {
                string sql = "select max(OrderID) from ZL_CommonModel";
                int OrderID = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, null)) + 1;
                CCate.ItemID = ItemID;
                CCate.OrderID = OrderID;
                ComModelID = conBll.insert(CCate);
            }
            m_Product.ItemID = ItemID;
            m_Product.ComModelID = ComModelID;
            return Insert(m_Product);
        }
        public int AddCommodities(M_Product model)
        {
            return Insert(model);
        }
        public int Insert(M_Product model)
        {
            if (model.OrderID == 0) { model.OrderID = (DataConvert.CLng(DBCenter.ExecuteScala(TbName, "MAX(OrderID)", "1=1")) + 1); }
            if (string.IsNullOrEmpty(model.ProCode)) { model.ProCode = GetProCode(SiteConfig.ShopConfig.ItemRegular); }
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool DeleteByID(int ProductId, M_Product m_Product)
        {
            string sqlStr = "update " + TbName + " set Recycler=1 Where [ID]=" + ProductId;
            return SqlHelper.ExecuteSql(sqlStr);
        }
        /// <summary>
        /// 还原商品
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public bool UpDeleteByID(int ProductId)
        {
            string sqlStr = "update ZL_Commodities set Recycler=0 Where [ID]=@ID";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            sp[0].Value = ProductId;
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        public bool RealDeleteByID(int ProductId, M_Product m_Product)
        {
            return RealDelByIDS(ProductId.ToString());
        }
        /// <summary>
        /// 从数据库中移动商品相关信息
        /// </summary>
        public bool RealDelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "SELECT ID,ItemID FROM " + TbName + " WHERE ID IN (" + ids + ")");
            string items = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items += dt.Rows[i]["ItemID"] + ",";
            }
            items = items.TrimEnd(',');
            return RealDelByIDS(ids, items);
        }
        public bool RealDelByIDS(string ids, string items)
        {
            SafeSC.CheckDataEx(items);
            SafeSC.CheckIDSEx(ids);
            SqlHelper.ExecuteSql("DELETE FROM ZL_CommonModel WHERE ItemID IN(" + items + ") AND TableName='ZL_P_Shop'");
            SqlHelper.ExecuteSql("DELETE FROM ZL_P_Shop WHERE ID IN(" + items + ")");
            return SqlHelper.ExecuteSql("DELETE FROM " + TbName + " WHERE ID IN (" + ids + ")");
        }
        public bool updateinfo(M_Product model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public DataTable ProductUserName(string username, int top, int type)
        {
            string sqlStr = "";
            if (type == 1)
            {
                if (top < 1)
                    sqlStr = "select * from ZL_Commodities where Recycler=0 and ItemID!=0 and AddUser=@username order by OrderId desc, id desc";
                else
                    sqlStr = "select top " + top + " * from ZL_Commodities where AddUser=@username and ItemID!=0 order by OrderId desc, id desc";
            }
            else
            {
                if (top < 1)
                    sqlStr = "select * from ZL_Commodities where AddUser=@username and ItemID!=0 and Istrue=1 order by OrderId desc, id desc";
                else
                    sqlStr = "select top " + top + " * from ZL_Commodities where AddUser=@username and ItemID!=0 and Istrue=1 order by OrderId desc, id desc";
            }
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@username", SqlDbType.NVarChar);
            parameter[0].Value = username;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, parameter);
        }
        public DataTable GetProducts(string keyWord)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("keyWord", "%" + keyWord + "%") };
            return Sql.Sel(TbName, " keyWord like @keyWord and ItemID!=0", "", sp);
        }
        public DataTable U_GetProductAll(int uid, int nodeid)
        {
            string where = "UserID=" + uid;
            if (nodeid > 0) { where += " AND NodeID=" + nodeid; }
            return DBCenter.Sel(TbName, where, PK + " DESC");
        }
        public DataTable GetProductAll(int NodeID, int storeid = 0, int type = 1, int pid = 0)
        {
           return GetProductAll(NodeID, storeid, type, pid, "");
        }
        public DataTable GetProductAll(int NodeID, int storeid, int type, int pid, string proclass)
        {
            string wherestr = "";
            switch (type)
            {
                case 1://所有商品
                    wherestr = "";
                    break;
                case 2://正常销售
                    wherestr = "And Sales=1";
                    break;
                case 3://未销售
                    wherestr = "And Sales=0";
                    break;
                case 4://正常销售商品
                    wherestr = "and ProClass=1";
                    break;
                case 5://特价处理
                    wherestr = "and istrue=1 and ProClass=2 and Sales=1";
                    break;
                case 6://所有热销
                    wherestr = "and ishot=1";
                    break;
                case 7://所有新品
                    wherestr = "and isnew=1";
                    break;
                case 8://所有精品
                    wherestr = "and isbest=1";
                    break;
                case 9://有促销活动
                    wherestr = "and istrue=1 and ID NOT in (select ID from ZL_Commodities where  istrue=1 and Preset = '1|0|0|{}') and Sales=1";
                    break;
                case 10://实际库存报警的商品
                    wherestr = "and istrue=1 and Stock<=StockDown and StockDown<>-1 and Sales=1 and JisuanFs=0";
                    break;
                case 11://预定库存报警的商品
                    wherestr = "and istrue=1 and Stock<=StockDown and StockDown<>-1 and Sales=1 and JisuanFs=1";
                    break;
                case 12://已售完的商品
                    wherestr = "and istrue=1 and Stock=0 and Sales=1 and Sold>0";
                    break;
                case 13:
                    wherestr = "and istrue=1 and Wholesales=1 and Sales=1";
                    break;
                case 14://所有捆绑销售的商品
                    wherestr = "and istrue=1 and Priority=1 and id<>0";
                    break;
                case 15://所有礼品
                    wherestr = "and istrue=1 and ItemID!=0 and Largess=1";
                    break;
                case 16://已审核商品
                    wherestr = "and istrue=1";
                    break;
                case 17://待审核商品
                    wherestr = "and istrue=0";
                    break;
                case 18://用户商品
                    wherestr = "and UserID>0";
                    break;
                case 19:
                    wherestr = "and ShiPrice>0";
                    break;
                case 20:
                    wherestr = " and istrue=1";
                    break;
                case 21:
                    wherestr = " and istrue=0";
                    break;
            }
            string strSql = "select * from ZL_Commodities A where  (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 " + wherestr + " and ParentID=" + pid;
            if (NodeID == 0)
            {
                strSql = "select A.*,B.NodeName from ZL_Commodities A Left Join ZL_Node B ON A.NodeID=B.NodeID where A.Recycler=0 " + wherestr + " and A.ParentID=" + pid;
            }
            if (storeid == 0)
            {
                strSql += " AND (UserShopID=0 OR UserShopID IS NULL)";
            }
            else if (storeid > 0)
            {
                strSql += " AND UserShopID=" + storeid;
            }
            else { strSql += " AND UserShopID >0"; }//所有店铺商品
            if (!string.IsNullOrEmpty(proclass)) { SafeSC.CheckIDSEx(proclass); strSql += " AND ProClass IN (" + proclass + ")"; }
            strSql += " order by A.OrderId desc,ID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable GetProByProClass(M_Product.ClassType proClass)
        {
            string strSql = "Select * from ZL_Commodities where ProClass=" + (int)proClass + " Order By OrderId desc,ID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable GetProductAllRecycler()
        {
            return Sql.Sel(TbName, " Recycler=1 and ItemID!=0 ", "OrderId desc,ID desc");
        }
        public DataTable GetProductAllscv(string tablename, int pid = 0)
        {
            SafeSC.CheckDataEx(tablename);
            string wherestr = pid > 0 ? "ItemID=" + pid : "1=1";
            string strSql = "select * from  ZL_Commodities Inner join " + tablename + " on " + tablename + ".ID=ZL_Commodities.ItemID where ItemID!=0 AND " + wherestr + " order by OrderId desc, ZL_Commodities.ID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 读取促销商品
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductbybind(int type, int id, int NodeID)
        {
            string strSql = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and istrue=1 and Priority=" + type + " and id<>" + id + " order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                strSql = "select * from ZL_Commodities where istrue=1 and ItemID!=0 and Priority=" + type + " and id<>" + id + " order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable SelProductByStore(int type, int id, int NodeID, int storeid)
        {
            string nodefied = NodeID == 0 ? "1=1" : "(NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ")";
            string strSql = "select * from ZL_Commodities where " + nodefied + " and Recycler=0 and ItemID!=0 and istrue=1 and Priority=" + type + " and id<>" + id + " AND UserShopID=" + storeid + " order by OrderId desc, ID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        /// <summary>
        /// 读取审核商品
        /// </summary>
        /// <param name="type">审核类型 1-已审核 0-待审核</param>
        /// <returns></returns>
        public DataTable GetReview(int type, int NodeID)
        {
            string strSql = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and ItemID!=0  and istrue=" + type + " order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                strSql = "select * from ZL_Commodities where istrue=" + type + " and ItemID!=0 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable GetProductbyLargess(int type)
        {
            return Sql.Sel(TbName, "  istrue=1 and ItemID!=0 and Largess=" + type, "OrderId desc, ID desc");
        }
        /// <summary>
        /// 读取分类商品
        /// </summary>
        /// <param name="nodid"></param>
        /// <returns></returns>
        public DataTable GetProductnodid(int nodeid)
        {
            string strSql = "select * from ZL_Commodities where Recycler=0 and ItemID!=0 and istrue=1 and (NodeID=" + nodeid + " or FirstNodeID=" + nodeid + ") order by OrderId desc, ID desc";
            if (nodeid == 0)
            {
                strSql = "select * from ZL_Commodities where Recycler=0 and ItemID!=0 and istrue=1 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 读取进回收站分类商品
        /// </summary>
        /// <param name="nodid"></param>
        /// <returns></returns>
        public DataTable GetProductnodidRecycler(int nodeid)
        {
            string strSql = "select * from ZL_Commodities where Recycler=1 and ItemID!=0 and (NodeID=" + nodeid + " or FirstNodeID=" + nodeid + ") order by OrderId desc, ID desc";
            if (nodeid == 0)
            {
                strSql = "select * from ZL_Commodities where Recycler=1 and ItemID!=0  order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable GetProductnodidscv(int nodeid, string tablename)
        {
            SafeSC.CheckDataEx(tablename);
            string strSql = "select * from  ZL_Commodities Inner join " + tablename + " on " + tablename + ".ID=ZL_Commodities.ItemID where Recycler=0 and ItemID!=0 and ZL_Commodities.istrue=1 and ZL_Commodities.Nodeid=" + nodeid + " order by OrderId desc, ZL_Commodities.ID desc";
            if (nodeid == 0)
            {
                strSql = "select * from  ZL_Commodities Inner join " + tablename + " on " + tablename + ".ID=ZL_Commodities.ItemID where Recycler=0 and ItemID!=0 and ZL_Commodities.istrue=1 order by OrderId desc, ZL_Commodities.ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public M_Product GetproductByid(int ID)
        {
            if (ID < 1) { return null; }
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
        /// //后台与前台共用的查询 
        /// </summary>
        /// <param name="Type">0,商品名称 1, 商品简介 2,商品介绍 3,厂商 4,品牌/商标 5,关键字</param>
        public DataTable ProductSearch(int Type, string KeyWord, int storeid = 0)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + KeyWord + "%") };
            string sqlstr = "SELECT  A.*,B.NodeName FROM ZL_Commodities A Left Join ZL_Node B ON A.NodeID=B.NodeID WHERE A.Recycler=0 ";
            switch (Type)
            {
                case 0:
                    sqlstr += " AND ProName Like @key";
                    break;
                case 2:
                    sqlstr += " AND Proinfo Like @key";
                    break;
                case 3:
                    sqlstr += " AND Procontent Like @key";
                    break;
                case 4:
                    sqlstr += " AND Producer Like @key";
                    break;
                case 5:
                    sqlstr += " AND Brand Like @key";
                    break;
                case 6:
                    sqlstr += " AND BarCode Like @key";
                    break;
                case 7:
                    sqlstr += " AND ComModelID Like @key";
                    break;
                case 8:
                    sqlstr += " AND Com_Abbreviation Like @key";
                    break;
                case 9:
                    sqlstr += " AND AddUser Like @key";
                    break;
                case 10:
                    sqlstr += " AND ID =" + DataConvert.CLng(KeyWord);
                    break;
                default:
                    sqlstr += " AND ProName Like @key";
                    break;
            }
            //if (Type == 8)
            //{
            //    sqlstr = "select ZL_Commodities.*,ZL_P_Productku.* from ZL_Commodities left join ZL_P_Productku on ZL_Commodities.ItemID=ZL_P_Productku.ID where and ItemID!=0 ZL_P_Productku.Com_Abbreviation like @key ";
            //}

            if (storeid == 0)
            {
                sqlstr += " AND UserShopID=0 OR UserShopID IS NULL";
            }
            else if (storeid > 0)
            {
                sqlstr += " AND UserShopID=" + storeid;
            }
            else { sqlstr += " AND UserShopID>" + storeid; }
            sqlstr += " ORDER BY OrderID DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlstr, sp);
        }
        /// <summary>
        /// //后台与前台共用的查询城市 
        /// </summary>
        /// <param name="Type">0,商品名称 1, 商品简介 2,商品介绍 3,厂商 4,品牌/商标 5,关键字</param>
        /// <param name="KayWord"></param>
        /// <returns></returns>
        public DataTable ProductSearchCity(int Type, string KayWord, string user)
        {
            //user已在上层处理
            string tablename;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("KayWord", "%" + KayWord + "%") };
            switch (Type)
            {
                case 0:
                    tablename = "Proname";
                    break;
                case 1:
                    tablename = "Proname";
                    break;
                case 2:
                    tablename = "Proinfo";
                    break;
                case 3:
                    tablename = "Procontent";
                    break;
                case 4:
                    tablename = "Producer";
                    break;
                case 5:
                    tablename = "Brand";
                    break;
                case 6:
                    tablename = "BarCode";
                    break;
                case 7:
                    tablename = "ComModelID";
                    break;
                case 8:
                    tablename = "Com_Abbreviation";
                    break;
                default:
                    tablename = "Proname";
                    break;
            }
            string sqlstr = "select * from ZL_Commodities where Recycler=0 and " + tablename + " like @KayWord and AddUser in" + user;

            if (Type == 8)
            {
                sqlstr = "select ZL_Commodities.*,ZL_P_Productku.* from ZL_Commodities left join ZL_P_Productku on ZL_Commodities.ItemID=ZL_P_Productku.ID where and ItemID!=0 ZL_P_Productku.Com_Abbreviation like @KayWord and ZL_Commodities.AddUser in" + user;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlstr, sp);
        }

        /// <summary>
        ///  正在销售的商品
        /// </summary>
        /// <param name="cateid">0--不允许销售，1--允许销售</param>
        /// <returns></returns>
        public DataTable ProductSalesare(int cateid, int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 Sales=@cateid order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where ItemID!=0 and Sales=@cateid order by OrderId desc, ID desc";
            }
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@cateid", SqlDbType.Int, 4);
            parameter[0].Value = cateid;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, parameter);
        }
        /// <summary>
        /// 未销售的商品
        /// </summary>
        public DataTable ProductNotsale(int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and Sales=0 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where ItemID!=0 and Sales=0 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 正常销售的商品
        /// </summary>
        public DataTable Normalsales(int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and ProClass=1 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where ProClass=1 and ItemID!=0 order by OrderId desc, ID desc";
            }

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 特价处理的商品
        /// </summary>
        public DataTable Special(int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and istrue=1 and ProClass=2 and Sales=1 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where istrue=1 and ProClass=2 and ItemID!=0 and Sales=1 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 所有热销的商品
        /// </summary>
        /// <param name="Soldnum">热销数量范围</param>
        public DataTable ProductSold(int Soldnum, int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and ishot=1 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where ishot=1 and ItemID!=0 order by OrderId desc, ID desc";
            }
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@Soldnum", SqlDbType.Int, 4);
            parameter[0].Value = Soldnum;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 所有新品
        /// </summary>
        public DataTable ProductNew(int diff, int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and isnew=1 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where isnew=1 and ItemID!=0 order by OrderId desc, ID desc";
            }

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@diff", SqlDbType.Int, 4);
            parameter[0].Value = diff;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 所有精品商品
        /// </summary>

        public DataTable ProductFine(int Dengjinum, int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and isbest=1 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where ItemID!=0 and isbest=1 order by OrderId desc, ID desc";
            }
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@Dengjinum", SqlDbType.Int, 4);
            parameter[0].Value = Dengjinum;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public DataTable Productjian()
        {
            return Sql.Sel(TbName, "Recycler=0 and istrue=1 and ItemID!=0 and dengji>3 and Sales=1", "OrderId desc, Priority desc");
        }
        /// <summary>
        /// 所有促销活动的商品
        /// </summary>
        public DataTable ProductPreset(int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and ItemID!=0 and Recycler=0 and istrue=1 and ID NOT in (select ID from ZL_Commodities where  istrue=1 and Preset = '1|0|0|{}') and Sales=1 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where istrue=1 and ItemID!=0 and ID NOT in (select ID from ZL_Commodities where  istrue=1 and ItemID!=0 and Preset = '1|0|0|{}') and Sales=1 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 实际库存报警的商品
        /// </summary>
        public DataTable ProductAlarm(int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and istrue=1 and Stock<=StockDown and StockDown<>-1 and Sales=1 and JisuanFs=0 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where istrue=1 and ItemID!=0 and Stock<=StockDown and StockDown<>-1 and Sales=1 and JisuanFs=0 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 预定库存报警的商品
        /// </summary>
        public DataTable ProductCategory(int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and istrue=1 and Stock<=StockDown and StockDown<>-1 and Sales=1 and JisuanFs=1 order  by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where istrue=1 and Stock<=StockDown and StockDown<>-1 and Sales=1 and ItemID!=0 and JisuanFs=1 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 已售完的商品
        /// </summary>
        public DataTable Productsoldout(int cateid, int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and istrue=1 and Stock=0 and Sales=1 and Sold>0 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where istrue=1 and Stock=0 and ItemID!=0 and Sales=1 and Sold>0 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 所有批发的商品
        /// </summary>
        public DataTable ProductWholesale(int NodeID)
        {
            string sqlStr = "select * from ZL_Commodities where (NodeID=" + NodeID + " or FirstNodeID=" + NodeID + ") and Recycler=0 and ItemID!=0 and istrue=1 and Wholesales=1 and Sales=1 order by OrderId desc, ID desc";
            if (NodeID == 0)
            {
                sqlStr = "select * from ZL_Commodities where  istrue=1 and Wholesales=1 and ItemID!=0 and Sales=1 order by OrderId desc, ID desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public bool setproduct(int type, string list)
        {
            SafeSC.CheckIDSEx(list);
            string sqlStr = string.Empty;
            switch (type)
            {
                case 1:
                    sqlStr = "update ZL_Commodities set Sales=1 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 2:
                    sqlStr = "update ZL_Commodities set ishot=1 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 3:
                    sqlStr = "update ZL_Commodities set isbest=1 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 4:
                    sqlStr = "update ZL_Commodities set isnew=1 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 5:
                    try
                    {
                        string tempsql = "select Tablename,ItemID from ZL_Commodities where (ID in (" + list + ")) and ItemID!=0";
                        using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, tempsql, null))
                        {
                            string tebname = "";
                            if (reader.Read())
                            {
                                int tempid = Convert.ToInt32(reader["ItemID"]);
                                tebname = reader["TableName"].ToString();
                                string ssql = "delete from " + tebname + " where ID =" + tempid + "";
                                SqlHelper.ExecuteSql(ssql, null);
                            }
                            reader.Close();
                            reader.Dispose();
                        }
                    }
                    catch (Exception) { }
                    sqlStr = "delete from ZL_Commodities where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 6:
                    sqlStr = "update ZL_Commodities set Sales=0 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 7:
                    sqlStr = "update ZL_Commodities set ishot=0 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 8:
                    sqlStr = "update ZL_Commodities set isbest=0 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 9:
                    sqlStr = "update ZL_Commodities set isnew=0 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 10://批量审核
                    sqlStr = "update ZL_Commodities set istrue=1 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 11://取消审核
                    sqlStr = "update ZL_Commodities set istrue=0 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 12://还原
                    sqlStr = "update ZL_Commodities set Recycler=0 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 13://进回收站
                    sqlStr = "update ZL_Commodities set Recycler=1 where (ID in (" + list + ")) and ItemID!=0";
                    break;
                case 14://全部还原
                    sqlStr = "update ZL_Commodities set Recycler=0 where Recycler=1 and ItemID!=0";
                    break;
                case 15://清空回收站
                    DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM " + TbName + " WHERE Recycler=1 AND ItemID!=0");
                    string ids = "", items = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        items += dt.Rows[i]["ItemID"] + ",";
                        ids += dt.Rows[i]["ID"] + ",";
                    }
                    items = items.TrimEnd(','); ids = ids.TrimEnd(',');
                    RealDelByIDS(ids, items);
                    return true;
                default:
                    sqlStr = "update ZL_Commodities set isnew=isnew where (ID in (" + list + ")) and ItemID!=0";
                    break;
            }
            return SqlHelper.ExecuteSql(sqlStr, null);
        }
        public DataTable Getmodetable(string tablename, int Itemid)
        {
            SafeSC.CheckDataEx(tablename);
            return Sql.Sel(tablename, PK, Itemid);
        }
        public bool ProUpStock(int id, int kucun)
        {

            string sqlStr = "update ZL_Commodities set Stock=" + kucun + " where ID=" + id + " and ItemID!=0";
            return SqlHelper.ExecuteSql(sqlStr);

            //SqlParameter[] cmdParams = new SqlParameter[1];
            //return Sql.UpdateByID(strTableName, "ID=", id, "Stock = '" + kucun + "'", cmdParams);
        }
        public DataTable Souchprolist(string list)
        {
            SafeSC.CheckIDSEx(list);
            return Sql.Sel(TbName, "Recycler=0 and ItemID!=0 and istrue=1 and Sales=0 and (ID in (" + list + ")", "");
        }
        public DataTable GetProWhere(string str)
        {
            SafeSC.CheckIDSEx(str);
            return GetWhere(" * ", " ID in (" + str + ")", " ID desc");
        }
        private DataTable GetWhere(string sqlstr, string sqlwhere, string order)
        {
            if (!string.IsNullOrEmpty(order))
            {
                order = " order by " + order;
            }
            string sql = "select " + sqlstr + " from  ZL_Commodities where Recycler=0 and ItemID!=0 and " + sqlwhere + order;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public DataTable SelectProByCmdID(int ContentID)
        {
            return Sql.Sel(TbName, " ComModelID = " + ContentID + " and ItemID!=0", "");
        }
        //用于旅游,机票页
        public DataTable SelByIDS(string ids, string field = "*")
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Select " + field + " From " + TbName + " Where ID IN(" + ids + ")";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductTAll()
        {
            return Sql.Sel(TbName, "Recycler=0 and ItemID!=0", "OrderId desc, ID desc");
        }
        public int GetOrder(int NodeID, int size)
        {
            string strSql = "";
            if (size == 0)
                strSql = "select Min(OrderID) from ZL_Commodities where ItemID!=0";
            else
                strSql = "select Max(OrderID) from ZL_Commodities where ItemID!=0";
            strSql += " and NodeID = " + NodeID;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql));
        }
        /// <summary>
        ///获取前一或后一商品数据,用于前端和后端商品排序,
        ///0:下移(前一商品),1:上移(后一商品)
        /// </summary>
        public M_Product GetNearID(int NodeID, int CurrentID, int UporDown, int uid = 0)
        {
            string where = " 1=1 ";
            string order = "";
            if (NodeID > 0) { where += " AND NodeID=" + NodeID; }
            if (uid > 0) { where += " AND UserID=" + uid; }
            if (UporDown == 0)
            {
                where += " AND OrderId<" + CurrentID;
                order = "OrderId DESC";
            }
            else// if (UporDown == 1)
            {
                where += " AND OrderId>" + CurrentID;
                order = "OrderId ASC";
            }
            int id = DataConvert.CLng(DBCenter.ExecuteScala(TbName, "ID", where, order));
            return GetproductByid(id);
        }
        public bool UpdateOrder(M_Product Product)
        {
            string strSql = "Update ZL_Commodities Set OrderId=@OrderId Where ID=@ID and ItemID!=0";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@OrderID", SqlDbType.Int),
                new SqlParameter("@ID", SqlDbType.Int)
            };
            cmdParams[0].Value = Product.OrderID;
            cmdParams[1].Value = Product.ID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        /// <summary>
        /// 修改是否设为标准商品
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="isSend">是否设为标准商品:0为否,1为是</param>
        /// <param name="UserShopID">标准商品ID</param>
        /// <returns></returns>
        public bool GetUpdateInsend(int id, int isSend, int UserShopID)
        {
            string sqlStr = "update ZL_Commodities set isStand=@isStand , UserShopID=@UserShopID WHERE ID=@ID and ItemID!=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@isStand",isSend),
                new SqlParameter("@UserShopID",UserShopID),
                new SqlParameter("@ID",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, para);
        }
        /// <summary>
        /// 更新购买商品人数
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="sold">人数</param>
        /// <returns></returns>
        public bool GetUpdateSold(int id, int sold)
        {
            string sqlStr = "update ZL_Commodities set Sold=@Sold WHERE ID=@ID and ItemID!=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Sold",sold),
                new SqlParameter("@ID",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, para);
        }
        //数据导入
        public bool ImportProducts(DataTable dt, int ModelID, int NodeID)
        {
            B_ModelField b_ModelField = new B_ModelField();
            DataTable dtModelField = b_ModelField.GetModelFieldList(ModelID);
            //建立从表存放当前信息
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            table.Columns.Add(new DataColumn("ShopmodelID", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dtModelField != null && dtModelField.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtModelField.Rows)
                    {
                        DataRow row = table.NewRow();
                        row[0] = dr["FieldName"].ToString();
                        row[1] = dr["FieldType"].ToString();
                        row[4] = dr["FieldAlias"].ToString();
                        try
                        {
                            row[2] = dt.Rows[i][row[4].ToString()];
                        }
                        catch
                        {
                            return false;
                        }
                        table.Rows.Add(row);
                    }
                }
                //基础表内容
                M_CommonData m_CommonData = new M_CommonData();
                m_CommonData.NodeID = NodeID;
                m_CommonData.ModelID = ModelID;
                B_Model b_Model = new B_Model();
                m_CommonData.TableName = b_Model.GetModelById(ModelID).TableName;
                m_CommonData.Title = "";
                m_CommonData.Status = 99;//0或99
                m_CommonData.Inputer = "";
                m_CommonData.PdfLink = "";
                m_CommonData.FirstNodeID = GetFriestNode(NodeID);
                m_CommonData.EliteLevel = 0;
                m_CommonData.InfoID = "";
                m_CommonData.SpecialID = "";
                m_CommonData.Template = "";
                m_CommonData.DefaultSkins = 0;

                //主表内容
                M_Product m_Product = new M_Product();
                m_Product.ModelID = ModelID;
                m_Product.Nodeid = NodeID;
                m_Product.Categoryid = 0;
                m_Product.ProCode = dt.Rows[i]["商品编号"].ToString();
                m_Product.Propeid = 0;
                m_Product.BarCode = dt.Rows[i]["条形码"].ToString();
                m_Product.Proname = dt.Rows[i]["商品名称"].ToString();
                m_Product.Kayword = dt.Rows[i]["关键字"].ToString();
                m_Product.ProUnit = dt.Rows[i]["商品单位"].ToString();
                try
                {
                    m_Product.Weight = Convert.ToInt32(dt.Rows[i]["商品重量"].ToString() == "" ? 0 : dt.Rows[i]["商品重量"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[商品重量]格式不正确");
                }
                try
                {
                    m_Product.ServerPeriod = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[服务期限]格式不正确");
                }
                m_Product.ProClass = 1;
                m_Product.Sales = Convert.ToInt32(dt.Rows[i]["销售状态(1)"].ToString() == "1" ? 1 : 0);
                m_Product.Istrue = Convert.ToInt32(dt.Rows[i]["属性设置(1)"].ToString() == "1" ? 1 : 0);
                try
                {
                    m_Product.AllClickNum = Convert.ToInt32(dt.Rows[i]["点击数"].ToString() == "" ? 0 : dt.Rows[i]["点击数"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[点击数]格式不正确");
                }
                try
                {
                    m_Product.UpdateTime = Convert.ToDateTime(dt.Rows[i]["更新时间"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[更新时间]格式不正确");
                }
                m_Product.ModeTemplate = "";

                ///22222222222222222222
                m_Product.Proinfo = dt.Rows[i]["商品简介"].ToString();
                m_Product.Procontent = dt.Rows[i]["详细介绍"].ToString();
                m_Product.Clearimg = dt.Rows[i]["商品清晰图"].ToString();
                m_Product.Thumbnails = dt.Rows[i]["商品缩略图"].ToString();

                ///33333333333333333333
                m_Product.Producer = dt.Rows[i]["生产商"].ToString();
                m_Product.Brand = dt.Rows[i]["品牌/商标"].ToString();
                m_Product.Allowed = Convert.ToInt32(dt.Rows[i]["缺货时允许购买(0)"].ToString() == "0" ? 0 : 1);
                try
                {
                    m_Product.Quota = Convert.ToInt32(dt.Rows[i]["限购数量"].ToString() == "" ? 0 : dt.Rows[i]["限购数量"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[限购数量]格式不正确");
                }
                try
                {
                    m_Product.DownQuota = Convert.ToInt32(dt.Rows[i]["最低购买数量"].ToString() == "" ? 0 : dt.Rows[i]["最低购买数量"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[最低购买数量]格式不正确");
                }
                m_Product.Stock = 0;//库存数量

                try
                {
                    m_Product.ShiPrice = Convert.ToDouble(dt.Rows[i]["市场参考价"].ToString() == "" ? 0 : dt.Rows[i]["市场参考价"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[市场参考价]格式不正确");
                }
                try
                {
                    m_Product.LinPrice = Convert.ToDouble(dt.Rows[i]["当前零售价"].ToString() == "" ? 0 : dt.Rows[i]["当前零售价"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[当前零售价]格式不正确");
                }
                try
                {
                    m_Product.AddTime = DateTime.Parse(dt.Rows[i]["创建时间"].ToString());
                }
                catch (Exception)
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[创建时间]格式不正确");
                }

                try
                {
                    m_Product.StockDown = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[库存报警下限]格式不正确");
                }
                try
                {
                    m_Product.JisuanFs = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[前台库存计算方式]格式不正确");
                }
                try
                {
                    m_Product.Rateset = 1;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[税率设置]格式不正确");
                }
                try
                {
                    m_Product.Rate = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[商品税率]格式不正确");
                }
                try
                {
                    m_Product.Dengji = 3;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[商品推荐等级]格式不正确");
                }

                ///44444444444444444444
                try
                {
                    m_Product.ShiPrice = Convert.ToDouble(dt.Rows[i]["市场参考价"].ToString() == "" ? 0 : dt.Rows[i]["市场参考价"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[市场参考价]格式不正确");
                }
                try
                {
                    m_Product.LinPrice = Convert.ToDouble(dt.Rows[i]["当前零售价"].ToString() == "" ? 0 : dt.Rows[i]["当前零售价"]);
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[当前零售价]格式不正确");
                }
                //m_Product.UserPrice = dt.Rows[i]["会员组价格"].ToString();
                m_Product.UserPrice = "0";
                try
                {
                    m_Product.FestlPrice = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[节日价格]格式不正确");
                }
                m_Product.FestPeriod = "";//节日时间
                try
                {
                    m_Product.BookPrice = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[预订价格]格式不正确");
                }
                try
                {
                    m_Product.bookDay = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[预订时间]格式不正确");
                }
                try
                {
                    m_Product.Recommend = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[打折优惠率]格式不正确");
                }
                try
                {
                    m_Product.PointVal = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[积分价格]格式不正确");
                }
                m_Product.Wholesales = 0;//允许批发
                try
                {
                    m_Product.Wholesaleone = 1;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[允许单独销售]格式不正确");
                }
                try
                {
                    m_Product.Largess = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[设置为礼品]格式不正确");
                }
                try
                {
                    m_Product.Largesspirx = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[礼品价格]格式不正确");
                }

                ///55555555555555555555
                try
                {
                    m_Product.ProjectType = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[促销方案]格式不正确");
                }
                try
                {
                    m_Product.IntegralNum = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[购物积分]格式不正确");
                }

                ///其它
                try
                {
                    m_Product.GuessType = 0;
                }
                catch
                {
                    function.WriteErrMsg("第" + (i + 1) + "行，字段[秒杀时间段]格式不正确");
                }
                m_Product.Preset = "";
                m_Product.TableName = m_CommonData.TableName;

                Add(table, m_Product, m_CommonData);
                table.Rows.Clear();//清除子表中的数据
            }
            return true;
        }
        public bool Update(DataTable table, M_Product m_Product, M_CommonData CCate)
        {
            if (m_Product.ItemID == 0 || m_Product.ItemID < 1)
            {
                string FieldList = "";
                string Valuelist = "";
                int si = 0;
                SqlParameter[] ps = new SqlParameter[table.Rows.Count];
                foreach (DataRow dr in table.Rows)
                {
                    if (string.IsNullOrEmpty(FieldList))
                    {
                        FieldList = "[" + dr["FieldName"].ToString() + "]";
                        Valuelist = "@" + dr["FieldName"].ToString();
                        ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), this.GetFieldValue(dr));
                    }
                    else
                    {
                        FieldList = FieldList + ",[" + dr["FieldName"].ToString() + "]";
                        Valuelist = Valuelist + ",@" + dr["FieldName"].ToString();
                        ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), this.GetFieldValue(dr));
                    }
                    if (dr["FieldType"].ToString() == "DateType")
                    {
                        if (this.GetFieldValue(dr) == "")
                        {
                            ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlDateTime.Null);
                        }
                        else
                        {
                            if (!IsDate(this.GetFieldValue(dr)))
                            {
                                ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlDateTime.Null);
                            }
                            else
                            {
                                ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), this.GetFieldValue(dr));
                            }
                        }
                    }
                    if (dr["FieldType"].ToString() == "int" || dr["FieldType"].ToString() == "NumType" || dr["FieldType"].ToString() == "float" || dr["FieldType"].ToString() == "money")
                    {
                        if (this.GetFieldValue(dr) == "")
                        {
                            ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlInt32.Null);
                        }
                    }
                    si = si + 1;
                }
                if ((FieldList != "") && (Valuelist != ""))
                {
                    string strSql = "Insert Into " + m_Product.TableName + " (" + FieldList + ") values(" + Valuelist + ");select @@IDENTITY AS newID";
                    int ItemID = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, ps));
                    m_Product.ItemID = ItemID;
                    SqlParameter[] sp2 = new SqlParameter[] {
                        new SqlParameter("Title", CCate.Title),
                        new SqlParameter("InfoID",CCate.InfoID),
                        new SqlParameter("SpecialID",CCate.SpecialID),
                        new SqlParameter("Template",CCate.Template)

                    };
                    //string strSql2 = "Update ZL_CommonModel Set Title='" + CCate.Title + "',ItemID=" + ItemID + ",EliteLevel=" + CCate.EliteLevel + ",InfoID='" + CCate.InfoID + "',SpecialID='" + CCate.SpecialID + "',Template='" + CCate.Template + "',OrederClass=" + CCate.OrederClass + ",FirstNodeID=" + CCate.FirstNodeID + " Where GeneralID=" + m_Product.ComModelID;
                    string strSql2 = "Update ZL_CommonModel Set Title=@Title,ItemID=" + ItemID + ",EliteLevel=" + CCate.EliteLevel + ",InfoID=@InfoID,SpecialID=@SpecialID,Template=@Template,OrederClass=" + CCate.OrederClass + ",FirstNodeID=" + CCate.FirstNodeID + " Where GeneralID=" + m_Product.ComModelID;
                    SqlHelper.ExecuteSql(strSql2, sp2);
                }
            }
            else
            {
                string FieldSet = "";
                string Valuelist = "";
                int si = 0;
                SqlParameter[] ps = new SqlParameter[table.Rows.Count];

                foreach (DataRow dr in table.Rows)
                {
                    string str = this.GetFieldSet(dr);
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (string.IsNullOrEmpty(FieldSet))
                        {
                            FieldSet = str;
                            Valuelist = "[" + dr["FieldName"].ToString() + "]" + "=@" + dr["FieldName"].ToString();
                            ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), this.GetFieldValue(dr));
                        }
                        else
                        {
                            FieldSet = FieldSet + "," + str;
                            Valuelist = Valuelist + "," + "[" + dr["FieldName"].ToString() + "]" + "=@" + dr["FieldName"].ToString();
                            ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), this.GetFieldValue(dr));
                        }
                        if (dr["FieldType"].ToString() == "DateType")
                        {
                            if (this.GetFieldValue(dr) == "")
                            {
                                ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlDateTime.Null);
                            }
                            else
                            {
                                if (!IsDate(this.GetFieldValue(dr)))
                                {
                                    ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlDateTime.Null);
                                }
                                else
                                {
                                    ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), this.GetFieldValue(dr));
                                }
                            }
                        }
                        if (dr["FieldType"].ToString() == "int" || dr["FieldType"].ToString() == "NumType" || dr["FieldType"].ToString() == "float" || dr["FieldType"].ToString() == "money")
                        {
                            if (this.GetFieldValue(dr) == "")
                            {
                                ps[si] = new SqlParameter("@" + dr["FieldName"].ToString(), System.Data.SqlTypes.SqlInt32.Null);
                            }
                        }
                    }
                    si = si + 1;
                }
                if (!string.IsNullOrEmpty(FieldSet))
                {
                    string strSql = "Update " + m_Product.TableName + " Set " + Valuelist + " Where [ID]=" + m_Product.ItemID;
                    SqlHelper.ExecuteScalar(CommandType.Text, strSql, ps);
                }
                int ItemID = m_Product.ItemID;
                SqlParameter[] sp2 = new SqlParameter[] {
                        new SqlParameter("Title", CCate.Title),
                        new SqlParameter("InfoID",CCate.InfoID),
                        new SqlParameter("SpecialID",CCate.SpecialID),
                        new SqlParameter("Template",CCate.Template)

                    };
                string strSql2 = "Update ZL_CommonModel Set Title=@Title,ItemID=" + ItemID + ",EliteLevel=" + CCate.EliteLevel + ",InfoID=@InfoID,SpecialID=@SpecialID,Template=@Template,FirstNodeID=" + CCate.FirstNodeID + ",OrederClass=" + CCate.OrederClass + " Where GeneralID=" + m_Product.ComModelID;
                SqlHelper.ExecuteSql(strSql2, sp2);
            }
            return Sql.UpdateByIDs(TbName, PK, m_Product.ID.ToString(), BLLCommon.GetFieldAndPara(m_Product), m_Product.GetParameters());
        }
        //-------------Tools
        private bool IsDate(string s)
        {
            try
            {
                DateTime dt = DateTime.Parse(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GetFieldValue(DataRow dr)
        {
            string FieldType = dr["FieldType"].ToString();
            string result = "";
            switch (FieldType)
            {
                case "TextType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "OptionType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "ListBoxType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "DateType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MultipleHtmlType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MultipleTextType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "FileType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "PicType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "FileSize":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "ThumbField":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MultiPicType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "OperatingType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "SmallFileType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "SuperLinkType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MoneyType":
                    result = dr["FieldValue"].ToString();
                    break;
                case "BoolType":
                    result = dr["FieldValue"].ToString();
                    break;
                case "NumType":
                    if (dr["FieldValue"].ToString() == "")
                    {
                        result = "";
                    }
                    else
                    {
                        result = dr["FieldValue"].ToString();
                    }
                    break;
                case "GradeOptionType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                default:
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
            }
            return result;
        }
        public string GetFieldSet(DataRow dr)
        {
            string FieldType = dr["FieldType"].ToString();
            string result = "";
            switch (FieldType)
            {
                case "TextType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "OptionType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "DateType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultipleHtmlType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultipleTextType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "FileType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "PicType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "ListBoxType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "FileSize":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "ThumbField":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MultiPicType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "OperatingType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "SuperLinkType":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                case "MoneyType":
                    result = dr["FieldName"].ToString() + "=" + dr["FieldValue"].ToString();
                    break;
                case "BoolType":
                    result = dr["FieldName"].ToString() + "=" + dr["FieldValue"].ToString();
                    break;
                case "NumType":
                    result = dr["FieldName"].ToString() + "=" + dr["FieldValue"].ToString();
                    break;
                case "SwfFileUpload":
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
                default:
                    result = dr["FieldName"].ToString() + "='" + dr["FieldValue"].ToString() + "'";
                    break;
            }
            return result;
        }
        public int FNodeID = 0;
        public int GetFriestNode(int NodeID)
        {
            GetNo(NodeID);
            return FNodeID;
        }
        public void GetNo(int NodeID)
        {
            B_Node b_Node = new B_Node();
            M_Node nodeinfo = b_Node.GetNodeXML(NodeID);
            int ParentID = nodeinfo.ParentID;
            if (DataConverter.CLng(nodeinfo.ParentID) > 0)
            {
                GetNo(nodeinfo.ParentID);
            }
            else
            {
                FNodeID = nodeinfo.NodeID;
            }
        }
        //需处理,多人同时添加问题,是否改为从内存中取
        public string GetProCode(string regular)
        {
            string stime = DateTime.Now.ToString("yyyy/MM/dd 00:00");
            string etime = DateTime.Now.ToString("yyyy/MM/dd 23:59:59");
            string itemCode = DateTime.Now.ToString(regular);
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT Count(ID) From " + TbName + " WHERE AddTime BETWEEN '" + stime + "' AND '" + etime + "'"));
            count++;
            if (count < 10) { itemCode += "0000" + count; }
            else if (count >= 10) { itemCode += "000" + count; }
            else if (count >= 100) { itemCode += "00" + count; }
            else if (count >= 1000) { itemCode += "0" + count; }
            else if (count >= 10000) { itemCode += count; }
            else { itemCode += count; }
            return itemCode;
        }
        /// <summary>
        /// 根据多价格编号,返回价格信息
        /// </summary>
        /// <param name="codeobj">价格编号</param>
        /// <param name="json">价格Json</param>
        /// <param name="price">价格</param>
        public DataRow GetPriceByCode(object codeobj, string json, ref double price)
        {
            if (codeobj == null || codeobj == DBNull.Value || string.IsNullOrEmpty(codeobj.ToString())) { return null; }
            string code = codeobj.ToString();
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            if (dt.Rows.Count < 1 || dt.Select("code='" + code + "'").Length < 1) { return null; }
            DataRow dr = dt.Select("code='" + code + "'")[0];
            price = Convert.ToDouble(dr["LinPrice"]);
            return dr;
        }
        public DataTable SelPage(int psize, int cpage, int uid = 0, string proname = "", int nodeID = 0, string addUser = "")
        {
            string where = "1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (uid > 0) { where += " AND UserId=" + uid; }
            if (!string.IsNullOrEmpty(proname))
            {
                where += " AND Proname like @proname";
                sp.Add(new SqlParameter("proname", "%" + proname + "%"));
            }
            if (nodeID > 0) { where += " AND Nodeid=" + nodeID; }
            if (!string.IsNullOrEmpty(addUser))
            {
                where += " AND AddUser like @addUser";
                sp.Add(new SqlParameter("addUser", "%" + addUser + "%"));
            }
            PageSetting config = new PageSetting()
            {
                fields = "*",
                pk = PK,
                t1 = TbName,
                cpage = cpage,
                psize = psize,
                where = where,
                order = "ID ASC",
                sp = sp.ToArray()
            };
            return DBCenter.SelPage(config);
        }
        /// <summary>
        /// IDC商品等处使用
        /// </summary>
        public DataTable Sel(string proclass = "", string skey = "")
        {
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(proclass)) { SafeSC.CheckIDSEx(proclass); where += " AND ProClass IN (" + proclass + ")"; }
            if (!string.IsNullOrEmpty(skey)) { where += " AND Proname LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.Sel(TbName, where, PK + " DESC", sp);
        }
        //-------------------------------会员价逻辑
        /// <summary>
        /// 根据配置,使用零售价|会员价|会员组价,用于购物车与订单
        /// </summary>
        public double P_GetByUserType(M_Product model, M_UserInfo mu)
        {
            if (model == null) { throw new Exception("商品不存在"); }
            if (mu.IsNull) { return model.LinPrice; }//购物车界面,用户尚未登录
            double price = model.LinPrice;
            if (!string.IsNullOrEmpty(model.UserPrice) && model.UserType > 0)
            {
                switch (model.UserType)
                {
                    case 1:
                        //未确定何为会员
                        //price = Convert.ToDouble(model.UserPrice);
                        break;
                    case 2://会员组价格,如未匹配,或填写不正确,则仍按零售价
                        {
                            DataTable updt = JsonConvert.DeserializeObject<DataTable>(model.UserPrice);
                            DataRow[] drs = updt.Select("gid='" + mu.GroupID + "'");
                            if (drs.Length > 0) { price = Convert.ToDouble(drs[0]["price"]); }
                        }
                        break;
                }
            }
            return price;
        }
    }
}
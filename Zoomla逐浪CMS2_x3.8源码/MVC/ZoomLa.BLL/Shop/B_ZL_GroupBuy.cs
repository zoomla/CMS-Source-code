using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using System.Globalization;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_ZL_GroupBuy
    {
        public string strTableName ="";
        public string PK = "";
        public DataTable dt = null;
        private M_ZL_GroupBuy initMod = new M_ZL_GroupBuy();
        public B_ZL_GroupBuy()
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
        public M_ZL_GroupBuy GetSelect(int ID)
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
        public DataTable GetAll()
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
        public bool GetUpdate(M_ZL_GroupBuy model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool DelGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInserts(M_ZL_GroupBuy model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="GroupBuy"></param>
        /// <returns></returns>
        public bool GetInsert(M_ZL_GroupBuy GroupBuy)
        {
            if (GetInserts(GroupBuy) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 根据商品ID找到团购ID
        /// <summary>
        ///  根据商品ID找到团购商品
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public DataTable GetGroupBuyByShopID(int shopid)
        {
            string sql = "select * from ZL_GroupBuy where ShopID=@shopid ORDER BY number ";
            SqlParameter[] parameter = { new SqlParameter("ShopID", shopid) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
        }
        #endregion


        /// <summary>
        /// 根据当前人数获取价格
        /// </summary>
        /// <param name="shopid">商品ID</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public object GetCurrentPrice(int shopid, int num)
        {
            string sqlStr = "select price from ZL_GroupBuy where  number in (select top 1 number from ZL_GroupBuy where number <=@number order by number desc) and shopid=@shopid";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@number",num),
                new SqlParameter("@shopid",shopid)
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para);
        }

        /// <summary>
        /// 根据团购查询所有商品信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectProdecutForGroup()
        {
            string sql = "select * from ZL_Commodities where ID in (select shopid from zl_groupbuy)";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }

        public DataTable SelectGroup(int proid)
        {
            string sql = "select * from zl_groupbuy where shopid = " + proid + "";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using FHModel;
using System.Data;
using System.Data.SqlClient;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class ProductTableLogic
    {
        #region 读取商品类型
        /// <summary>
        /// 读取商品类型
        /// </summary>
        /// <returns></returns>
        public new static List<ProductTypetable> GetType()
        {
            try
            {
                string sql = @"select * from ZL_Sns_ProductTypetable";
                SqlParameter[] parameter ={ };
                List<ProductTypetable> list = new List<ProductTypetable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        ProductTypetable ptt = new ProductTypetable();
                        ReadType(dr, ptt);
                        list.Add(ptt);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取类型表
        /// <summary>
        /// 读取类型表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="ptt"></param>
        public static void ReadType(SqlDataReader dr, ProductTypetable ptt)
        {
            ptt.ID = new Guid(dr["ID"].ToString());
            ptt.Name = dr["Name"].ToString();
        }
        #endregion

        #region 添加商品
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static Guid InsertProduct(ProductTable pt)
        {
            try
            {
                pt.ID = Guid.NewGuid();
                string sql = @"INSERT INTO [ZL_Sns_ProductTable] (
	[ID],
	[ProductName],
	[TypeID],
	[ProductContent],
	[Price],
	[VipPrice],
	[ProductPic],
	[Addtime]
) VALUES (
	@ID,
	@ProductName,
	@TypeID,
	@ProductContent,
	@Price,
	@VipPrice,
	@ProductPic,
	@Addtime
)";
                SqlParameter[] parameter ={ 
                    new SqlParameter("ID",pt.ID),
                    new SqlParameter("ProductName",pt.ProductName),
                    new SqlParameter("TypeID",pt.TypeID),
                    new SqlParameter("ProductContent",pt.ProductContent),
                    new SqlParameter("Price",pt.Price),
                    new SqlParameter("VipPrice",pt.VipPrice),
                    new SqlParameter("ProductPic",pt.ProductPic),
                    new SqlParameter("Addtime",DateTime.Now)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return pt.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取商品
        /// <summary>
        /// 读取商品
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<ProductTable> GetProductTable(PagePagination page)
        {
            try
            {
                string sql = @"SELECT * from ZL_Sns_ProductTable ";
                if (page != null)
                {
                    sql = page.PaginationSql(sql);
                }
                SqlParameter[] parameter ={ };
                List<ProductTable> list = new List<ProductTable>();
                using(SqlDataReader dr=SqlHelper.ExecuteReader(CommandType.Text,sql,parameter))
                {
                    while (dr.Read())
                    {
                        ProductTable pt = new ProductTable();
                        ReadProduct(dr, pt);
                        list.Add(pt);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取商品表
        /// <summary>
        /// 读取商品表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="pt"></param>
        public static void ReadProduct(SqlDataReader dr, ProductTable pt)
        {
            pt.ID=new Guid(dr["ID"].ToString());
            pt.ProductName=dr["ProductName"].ToString();
            pt.TypeID=new Guid(dr["TypeID"].ToString());
            pt.ProductContent=dr["ProductContent"].ToString();
            pt.Price = int.Parse(dr["Price"].ToString());
            pt.VipPrice = int.Parse(dr["VipPrice"].ToString());
            pt.ProductPic=dr["ProductPic"].ToString();
            pt.Addtime= DateTime.Parse(dr["Addtime"].ToString());
        }
        #endregion

        #region 删除商品
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="delid"></param>
        public static void DelProduct(string delid)
        {
            //try
            //{
            //    delid = delid.Replace(",", "','");
            //    string sql = "delete from ZL_Sns_ProductTable where (ID in ('" + delid + "'))";
            //    // SqlParameter[] parameter ={ new SqlParameter("delid", delid) };
            //    SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            //}
            //catch { }
        }
        #endregion

        #region 根据ID读取数据
        /// <summary>
        /// 根据ID读取数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static ProductTable GetPtByID(Guid ID)
        {
            try
            {
                string sql = @"SELECT  ZL_Sns_ProductTable.*
FROM 
      ZL_Sns_ProductTable where ZL_Sns_ProductTable.ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
                ProductTable pt = new ProductTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadProduct(dr, pt);
                    }
                }
                return pt;
            }
            catch 
            {
                throw;
            }
        }
        #endregion

        #region 修改商品
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="pt"></param>
        public static void UpdateProductByID(ProductTable pt)
        {
            string sql = @"UPDATE [ZL_Sns_ProductTable] SET
	[ProductName] = @ProductName,
	[TypeID] = @TypeID,
	[ProductContent] = @ProductContent,
	[Price] = @Price,
	[VipPrice] = @VipPrice,
	[ProductPic] = @ProductPic
WHERE
	[ID] = @ID";
            SqlParameter[] parameter ={ 
                    new SqlParameter("ID",pt.ID),
                    new SqlParameter("ProductName",pt.ProductName),
                    new SqlParameter("TypeID",pt.TypeID),
                    new SqlParameter("ProductContent",pt.ProductContent),
                    new SqlParameter("Price",pt.Price),
                    new SqlParameter("VipPrice",pt.VipPrice),
                    new SqlParameter("ProductPic",pt.ProductPic)
                };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }
        #endregion

        /// <summary>
        /// 读取商品
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<ProductTable> GetProductTableBytID(Guid TypeID,PagePagination page)
        {
            try
            {
                string sql = @"SELECT  ZL_Sns_ProductTable.* from
      ZL_Sns_ProductTable ";
                if (TypeID != Guid.Empty)
                {
                    sql += @" and ZL_Sns_ProductTable.TypeID=@TypeID";
                }
                if (page != null)
                {
                    sql = page.PaginationSql(sql);
                }
                SqlParameter[] parameter ={ new SqlParameter("TypeID", TypeID) };
                List<ProductTable> list = new List<ProductTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        ProductTable pt = new ProductTable();
                        
                        ReadProduct(dr, pt);
                        list.Add(pt);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        #region 购买商品
        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="usp"></param>
        /// <returns></returns>
        public static Guid InsertShop(UserShopProduct usp)
        {
            try
            {
                string sql = @"INSERT INTO [ZL_Sns_UserShopProduct] (
	[ID],
	[UserID],
	[ProductID],
	[ShopDay],
	[Addtime],
	[LargessID],
	[LargessContent]
) VALUES (
	@ID,
	@UserID,
	@ProductID,
	@ShopDay,
	@Addtime,
	@LargessID,
	@LargessContent
)";
                usp.ID = Guid.NewGuid();
                //if (usp.LargessID == null)
                //{
                //    usp.LargessID = 0;
                //}
                SqlParameter[] parameter ={ 
                    new SqlParameter("ID",usp.ID),
                    new SqlParameter("UserID",usp.UserID),
                    new SqlParameter("ProductID",usp.ProductID),
                    new SqlParameter("ShopDay",usp.ShopDay),
                    new SqlParameter("LargessID",usp.LargessID),
                    new SqlParameter("LargessContent",usp.LargessContent),
                    new SqlParameter("Addtime",DateTime.Now)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return usp.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 读取用户购买商品
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<UserShopProduct> GetUserShop(int UserID)
        {
            try
            {
                string sql = @"SELECT ZL_Sns_UserShopProduct.*, ZL_Sns_ProductTable.ProductName, ZL_Sns_ProductTable.ProductPic
FROM ZL_Sns_UserShopProduct INNER JOIN
      ZL_Sns_ProductTable ON ZL_Sns_UserShopProduct.ProductID = ZL_Sns_ProductTable.ID where ZL_Sns_UserShopProduct.UserID=@UserID";
                SqlParameter[] parameter ={ new SqlParameter("UserID", UserID) };
                List<UserShopProduct> list = new List<UserShopProduct>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        UserShopProduct usp = new UserShopProduct();
                        usp.ProductName = dr["ProductName"].ToString();
                        usp.ProductPic = dr["ProductPic"].ToString();
                        ReadUserShop(dr, usp);
                        list.Add(usp);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 读取商品表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="pt"></param>
        public static void ReadUserShop(SqlDataReader dr, UserShopProduct usp)
        {
            usp.ID = new Guid(dr["ID"].ToString());
            usp.UserID = int.Parse(dr["UserID"].ToString());
            usp.ProductID = new Guid(dr["ProductID"].ToString());
            usp.ShopDay = int.Parse(dr["ShopDay"].ToString());
            usp.LargessID = int.Parse(dr["LargessID"].ToString());
            usp.LargessContent = dr["LargessContent"].ToString();
            usp.Addtime = DateTime.Parse(dr["Addtime"].ToString());
        }
    }
}

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
   public class HomeSetLogic
   {
       #region 添加用户放置商品
       /// <summary>
       /// 添加用户放置商品
       /// </summary>
       /// <param name="hc"></param>
       /// <returns></returns>
       public static Guid InsertHomeSet(HomeCollocate hc)
       {
           try
           {
               string sql = @"INSERT INTO [ZL_Sns_HomeCollocate] (
	[ID],
	[UserID],
	[ShopID],
	[CLeft],
	[CIndexZ],
	[CTop],
	[Addtime]
) VALUES (
	@ID,
	@UserID,
	@ShopID,
	@CLeft,
	@CIndexZ,
	@CTop,
	@Addtime
)";
               hc.CIndexZ = SqlHelper.GetMaxId(@"ZL_Sns_HomeCollocate where UserID='" + hc.UserID + "'", "CIndexZ") == 0 ? 11 : SqlHelper.GetMaxId("ZL_Sns_HomeCollocate where UserID='" + hc.UserID + "'", "CIndexZ");
               hc.CIndexZ = hc.CIndexZ + 1;
               hc.ID = Guid.NewGuid();
               SqlParameter[] parameter ={ 
                new SqlParameter("ID",hc.ID),
                   new SqlParameter("UserID",hc.UserID),
                   new SqlParameter("ShopID",hc.ShopID),
                   new SqlParameter("CLeft",hc.CLeft),
                   new SqlParameter("CIndexZ",hc.CIndexZ),
                   new SqlParameter("CTop",hc.CTop),
                   new SqlParameter("Addtime",DateTime.Now)
               };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
               return hc.ID;
           }
           catch
           {
               throw;
           }
       }
       #endregion

       #region 读取用户放置商品
       /// <summary>
       /// 读取用户放置商品
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public static List<HomeCollocate> GetHomeProductByUserID(int UserID)
       {
           try
           {
               string sql = @"SELECT ZL_Sns_ProductTable.ProductName, ZL_Sns_ProductTable.ProductPic, ZL_Sns_HomeCollocate.*
FROM ZL_Sns_UserShopProduct INNER JOIN
      ZL_Sns_HomeCollocate ON ZL_Sns_UserShopProduct.ID = ZL_Sns_HomeCollocate.ShopID INNER JOIN
      ZL_Sns_ProductTable ON ZL_Sns_UserShopProduct.ProductID = ZL_Sns_ProductTable.ID where ZL_Sns_HomeCollocate.UserID=@UserID";
               SqlParameter[] parameter ={ new SqlParameter("UserID", UserID) };
               List<HomeCollocate> list = new List<HomeCollocate>();
               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
               {
                   while (dr.Read())
                   {
                       HomeCollocate hc = new HomeCollocate();
                       hc.ProductName = dr["ProductName"].ToString();
                       hc.ProductPic = dr["ProductPic"].ToString();
                       ReadHomeSet(dr, hc);
                       list.Add(hc);
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

       #region 读取用户放置商品表
       /// <summary>
       /// 读取用户放置商品表
       /// </summary>
       /// <param name="dr"></param>
       /// <param name="hc"></param>
       public static void ReadHomeSet(SqlDataReader dr, HomeCollocate hc)
       {
           hc.ID = new Guid(dr["ID"].ToString());
           hc.UserID =int.Parse(dr["UserID"].ToString());
           hc.ShopID = new Guid(dr["ShopID"].ToString());
           hc.CLeft = int.Parse(dr["CLeft"].ToString());
           hc.CIndexZ = int.Parse(dr["CIndexZ"].ToString());
           hc.CTop = int.Parse(dr["CTop"].ToString());
           hc.Addtime = DateTime.Parse(dr["Addtime"].ToString());
       }
       #endregion

       #region 读取用户头像放置
       /// <summary>
       /// 读取用户头像放置
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public static HomeHeadCollocate GetHomeSetHeadByUserID(int UserID)
       {
           try
           {
               string sql = @"select * from ZL_Sns_HomeHeadCollocate where UserID=@UserID";
               SqlParameter[] parameter ={ new SqlParameter("UserID", UserID) };
               HomeHeadCollocate hhc = new HomeHeadCollocate();
               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
               {
                   if (dr.Read())
                   {
                       hhc.UserID = int.Parse(dr["UserID"].ToString());
                       hhc.UserHeadPic = dr["UserHeadPic"].ToString();
                       hhc.UserLeft = int.Parse(dr["UserLeft"].ToString());
                       hhc.UserIndexZ = int.Parse(dr["UserIndexZ"].ToString());
                       hhc.UserTop = int.Parse(dr["UserTop"].ToString());
                       hhc.CohabitID =dr["CohabitID"] ==null?new Guid(): new Guid(dr["CohabitID"].ToString());
                       hhc.CohabitHeadPic = dr["CohabitHeadPic"].ToString();

                       hhc.CohabitLeft = int.Parse(dr["CohabitLeft"].ToString());
                       hhc.CohabitIndexZ = int.Parse(dr["CohabitIndexZ"].ToString());
                       hhc.CohabitTop = int.Parse(dr["CohabitTop"].ToString());

                   }
               }
               return hhc;
           }
           catch
           {
               throw;
           }

       }
       #endregion

       #region 初始化用户小屋头像
       /// <summary>
       /// 初始化用户小屋头像
       /// </summary>
       /// <param name="hhc"></param>
       public static void InsertUserHead(HomeHeadCollocate hhc)
       {
           try
           {
               string sql = @"INSERT INTO [ZL_Sns_HomeHeadCollocate] (
	[UserID],
	[UserHeadPic],
	[UserLeft],
	[UserIndexZ],
	[UserTop],
	[CohabitID],
	[CohabitHeadPic],
	[CohabitLeft],
	[CohabitIndexZ],
	[CohabitTop]
) VALUES (
	@UserID,
	@UserHeadPic,
	@UserLeft,
	@UserIndexZ,
	@UserTop,
	@CohabitID,
	@CohabitHeadPic,
	@CohabitLeft,
	@CohabitIndexZ,
	@CohabitTop
)";
               SqlParameter[] parameter =
                   { 
                   new SqlParameter("UserID",hhc.UserID),
                   new SqlParameter("UserHeadPic",hhc.UserHeadPic),
                   new SqlParameter("UserLeft",hhc.UserLeft),
                   new SqlParameter("UserIndexZ",hhc.UserIndexZ),
                   new SqlParameter("UserTop",hhc.UserTop),
                   new SqlParameter("CohabitHeadPic",hhc.CohabitHeadPic),
                   new SqlParameter("CohabitLeft",hhc.CohabitLeft),
                   new SqlParameter("CohabitIndexZ",hhc.CohabitIndexZ),
                   new SqlParameter("CohabitTop",hhc.CohabitTop),
                   new SqlParameter("CohabitID",hhc.CohabitID)
                   };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
               
           }
           catch
           {
               throw;
           }
       }
       #endregion

       #region 保存用户头像位置
       /// <summary>
       /// 保存用户头像位置
       /// </summary>
       /// <param name="hcc"></param>
       public static void UpdateHead(HomeHeadCollocate hcc)
       {
           try
           {
               string sql = @"UPDATE [ZL_Sns_HomeHeadCollocate] SET
	[UserHeadPic] = @UserHeadPic,
	[UserLeft] = @UserLeft,
	[UserIndexZ] = @UserIndexZ,
	[UserTop] = @UserTop,
	[CohabitID] = @CohabitID,
	[CohabitHeadPic] = @CohabitHeadPic,
	[CohabitLeft] = @CohabitLeft,
	[CohabitIndexZ] = @CohabitIndexZ,
	[CohabitTop] = @CohabitTop
WHERE
	[UserID] = @UserID";
               SqlParameter[] parameter ={ 
                   new SqlParameter("UserID",hcc.UserID),
                   new SqlParameter("UserHeadPic",hcc.UserHeadPic),
                   new SqlParameter("UserLeft",hcc.UserLeft),
                   new SqlParameter("UserIndexZ",hcc.UserIndexZ),
                   new SqlParameter("UserTop",hcc.UserTop),
                   new SqlParameter("CohabitID",hcc.CohabitID),
                   new SqlParameter("CohabitHeadPic",hcc.CohabitHeadPic),
                   new SqlParameter("CohabitLeft",hcc.CohabitLeft),
                   new SqlParameter("CohabitIndexZ",hcc.CohabitIndexZ),
                   new SqlParameter("CohabitTop",hcc.CohabitTop)
               };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
           }
           catch { }
       }
       #endregion

       #region 保存商品位置
       /// <summary>
       /// 保存商品位置
       /// </summary>
       /// <param name="left"></param>
       /// <param name="top"></param>
       /// <param name="ID"></param>
       public static void UpdateProduct(int left,int top,Guid ID)
       {
           try
           {
               string sql = @"UPDATE [ZL_Sns_HomeCollocate] SET
	[CLeft] = @CLeft,
	[CTop] = @CTop,
    [Addtime]=@Addtime
WHERE
	[ID] = @ID";
               SqlParameter[] parameter ={ 
                   new SqlParameter("CLeft",left),
                   new SqlParameter("CTop",top),
                   new SqlParameter("ID",ID),
                   new SqlParameter("Addtime",DateTime.Now)
               };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
           }
           catch
           {
           }
       }
       #endregion

       #region 检测商品是否使用
       /// <summary>
       /// 检测商品是否使用(使用中返回true)
       /// </summary>
       /// <param name="ShopID"></param>
       /// <returns></returns>
       public static bool CheckShopid(Guid ShopID)
       {
           try
           {
               string sql = @"select * from ZL_Sns_HomeCollocate where ShopID=@ShopID";
               SqlParameter[] parameter ={ new SqlParameter("ShopID", ShopID) };
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

       #region 删除放置商品
       /// <summary>
       /// 删除放置商品
       /// </summary>
       /// <param name="ShopID"></param>
       public static void DelHomeSet(Guid ShopID)
       {
           string sql = @"Delete from ZL_Sns_HomeCollocate where ShopID=@ShopID";
           SqlParameter[] parameter ={ new SqlParameter("ShopID", ShopID) };
           SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
       }
       #endregion

       #region 最新布置
       /// <summary>
       /// 最新布置
       /// </summary>
       /// <returns></returns>
       public static List<HomeCollocate> GetNewUserSet(int i,PagePagination page)
       {
           try
           {
               string sql = @"SELECT  DISTINCT";
               if(i!=100)
               {
                   sql += " TOP "+i;

               }
               sql += @" (userid),addtime  FROM [ZL_Sns_HomeCollocate] 
";
               if (page != null)
               {
                   sql = page.PaginationSql(sql);
               }
               List<HomeCollocate> list = new List<HomeCollocate>();
               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
               {
                   while (dr.Read())
                   {
                       HomeCollocate hc = new HomeCollocate();
                       hc.UserID =int.Parse(dr["userid"].ToString());
                       list.Add(hc);
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

//       #region 得到大户人家
//       /// <summary>
//       /// 得到大户人家
//       /// </summary>
//       /// <param name="i"></param>
//       /// <returns></returns>
//       public static List<UserTable> GetMostPeopel(int i,PagePagination page)
//       {
//           try
//           {
//               string sql = @"SELECT";
//               if(i!=100)
//               {
//                   sql += " TOP "+i;

//               }
//               sql += @" UserTable.*,UserAccount.AccMoney 
//FROM UserAccount INNER JOIN
//      UserTable ON UserAccount.UserID = UserTable.UserID";
//               if (page != null)
//               {
//                   sql = page.PaginationSql(sql);
//               }
//               List<UserTable> list = new List<UserTable>();
//               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
//               {
//                   while (dr.Read())
//                   {
//                       UserTable ut=new UserTable();
//                       ut.UserID = new Guid(dr["UserID"].ToString());
//                       ut.UserName = dr["UserName"].ToString();
//                       list.Add(ut);
//                   }
//               }
//               return list;
//           }
//           catch
//           {
//               throw;
//           }
//       }
//       #endregion
   }
}

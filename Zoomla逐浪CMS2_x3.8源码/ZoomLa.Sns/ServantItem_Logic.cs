using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class ServantItem_Logic
    {
        #region 添加仆人项目
        /// <summary>
        /// 添加仆人项目
        /// </summary>
        /// <param name="sitem"></param>
        public static void Add(ServantItem sitem)
        {
            sitem.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO ServantItem([ID],[ItemName],[ItemSynopsis],[ItemMoney],[ItemImage],[ItemType])
     VALUES (@ID,@ItemName,@ItemSynopsis,@ItemMoney,@ItemImage,@ItemType)";

            SqlParameter[] sp ={
                new SqlParameter("@ID",sitem.ID),
                new SqlParameter("@ItemName",sitem.ItemName),
                new SqlParameter("@ItemSynopsis",sitem.ItemSynopsis),
                new SqlParameter("@ItemMoney",sitem.ItemMoney),
                new SqlParameter("@ItemImage",sitem.ItemImage)
                //new SqlParameter("@ItemType",sitem.ItemType)
            
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 修改仆人项目
        /// <summary>
        /// 修改仆人项目
        /// </summary>
        /// <param name="sitem"></param>
        public static void Update(ServantItem sitem)
        {
            string SQLstr = @"UPDATE ServantItem SET [ItemName] = @ItemName,[ItemSynopsis] = @ItemSynopsis,[ItemMoney] = @ItemMoney,[ItemImage] = @ItemImage,[ItemType] = @ItemType
 WHERE [ID] = @ID";

            SqlParameter[] sp ={
                new SqlParameter("@ID",sitem.ID),
                new SqlParameter("@ItemName",sitem.ItemName),
                new SqlParameter("@ItemSynopsis",sitem.ItemSynopsis),
                new SqlParameter("@ItemMoney",sitem.ItemMoney),
                new SqlParameter("@ItemImage",sitem.ItemImage)
                //new SqlParameter("@ItemType",sitem.ItemType)
            
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 删除仆人项目
        /// <summary>
        /// 删除仆人项目
        /// </summary>
        /// <param name="id"></param>
        public static void Del(Guid id)
        {
            string SQLstr = @"delete from ServantItem where ID=@ID ";
            SqlParameter[] sp ={ new SqlParameter("@ID", id) };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        
        }

        #endregion

        #region 根据项目ID查询单个项目信息
        /// <summary>
        /// 根据项目ID查询单个项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServantItem GetServantItem(Guid id)
        {
            try
            {
                ServantItem sitem = new ServantItem();
                string SQLstr = @"select * from ServantItem where ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadServantItem(dr, sitem);
                    }
                }
                return sitem;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询所有项目信息
        /// <summary>
        /// 查询所有项目信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<ServantItem> GetAllServantItem(PagePagination page)
        {
            try
            {
                string SQLstr = @"SELECT *  FROM ServantItem";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                List<ServantItem> list = new List<ServantItem>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr))
                {
                    while (dr.Read())
                    {
                        ServantItem svtitem = new ServantItem();
                        ReadServantItem(dr, svtitem);
                        list.Add(svtitem);
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


        #region 读取仆人项目信息
        /// <summary>
        /// 读取仆人项目信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccritique"></param>
        public static void ReadServantItem(SqlDataReader dr, ServantItem sitem)
        {
            sitem.ID = (Guid)dr["ID"];
            sitem.ItemImage = dr["ItemImage"].ToString();
            sitem.ItemMoney = Convert.ToInt32(dr["ItemMoney"]);
            sitem.ItemName = dr["ItemName"].ToString();
            sitem.ItemSynopsis = dr["ItemSynopsis"].ToString();
            //sitem.ItemType = Convert.ToInt32(dr["ItemType"]);
        }
        #endregion
    }
}

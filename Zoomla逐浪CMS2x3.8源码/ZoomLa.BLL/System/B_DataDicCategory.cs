using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_DataDicCategory
    {
        public M_DicCategory initMod = new M_DicCategory();
        public string PK, TbName;
        public B_DataDicCategory()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        /// <summary>
        /// 添加数据字典分类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool AddCate(M_DicCategory model)
        {
            return Sql.insertID(model.TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model))>0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(M_DicCategory model)
        {
            return Sql.UpdateByIDs(model.TbName, model.PK, model.DicCateID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public static bool DelCate(string ids)
        {
            M_DicCategory model = new M_DicCategory();
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteSql("DELETE FROM " + model.TbName + " WHERE " + model.PK+" IN("+ids+")");
        }
        public static bool DelCate(int ID)
        {
            M_DicCategory model = new M_DicCategory();
            return Sql.Del(model.TbName, model.PK + "=" + ID);
        }
        public static DataTable GetDicCateList()
        {
            string strSql = "select * from ZL_DataDicCategory";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 根据分类名查询分类列表
        /// </summary>
        /// <param name="NameKey"></param>
        /// <returns></returns>
        public static DataTable SearchDicCateList(string NameKey)
        {
            string strSql = "select * from ZL_DataDicCategory where IsUsed='1'";
            if (!string.IsNullOrEmpty(NameKey))
                strSql = strSql + " and CategoryName like @key";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("key","%"+NameKey+"%") };
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 设定启用
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static bool SetUsed(int ID)
        {
            M_DicCategory model = new M_DicCategory();
            string sql = " update " + model.TbName + " set IsUsed=1 where DicCateID=" + ID;
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 设定不启用
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static bool SetUnUsed(int ID)
        {
            M_DicCategory model = new M_DicCategory();
            string sql = " update " + model.TbName + " set IsUsed=0 where DicCateID=" + ID;
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 读取分类实例
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static M_DicCategory GetDicCate(int DicCateID)
        {
            string strSql = "select * from ZL_DataDicCategory where DicCateID=@DicCateID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@DicCateID", SqlDbType.Int)
            };
            sp[0].Value = DicCateID;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                if (rdr.Read())
                {
                    return new M_DicCategory().GetModelFromReader(rdr);
                }
                else
                {
                    return new M_DicCategory(true);
                }
            }
        }
        /// <summary>
        /// 将分类ID包含在ID数组字符串中的分类设定启用状态
        /// </summary>
        /// <param name="DicCateIDArr"></param>
        /// <param name="IsUsed"></param>
        /// <returns></returns>
        public static bool SetUsedByArr(string DicCateIDArr, bool IsUsed)
        {
            string strSql = "update ZL_DataDicCategory set IsUsed=@IsUsed where DicCateID in (" + DicCateIDArr + ")";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@IsUsed", SqlDbType.Char,1)
            };
            sp[0].Value = IsUsed ? "1" : "0";
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 将所有分类设定启用
        /// </summary>
        /// <returns></returns>
        public static bool SetUsedAll()
        {
            string strSql = "update ZL_DataDicCategory set IsUsed='1' where 1=1";
            return SqlHelper.ExecuteSql(strSql);
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using System.Text;

namespace ZoomLa.BLL
{
    public class B_DataDictionary
    {
        M_Dictionary initMod = new M_Dictionary();
        string PK, TbName;
        public B_DataDictionary()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }

        /// <summary>
        /// 添加数据字典项目
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool AddDic(M_Dictionary model)
        {
            return Sql.insertID(model.TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model))>0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(M_Dictionary model)
        {
            return Sql.UpdateByIDs(model.TbName, model.PK, model.DicID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        /// <summary>
        /// 删除字典项目
        /// </summary>
        /// <param name="DicID"></param>
        /// <returns></returns>
        public bool Del(int ID)
        {
            return Sql.Del(TbName,PK + "=" + ID);
        }
        /// <summary>
        /// 读取字典项目实例
        /// </summary>
        /// <param name="DicID"></param>
        /// <returns></returns>
        public static M_Dictionary GetModel(int DicID)
        {
            string strSql = "select * from ZL_DataDic where DicID=@DicID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@DicID", SqlDbType.Int)
            };
            sp[0].Value = DicID;
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                if (rdr.Read())
                {
                    return new M_Dictionary().GetModelFromReader(rdr);
                }
                else
                {
                    return new M_Dictionary(true);
                }
            }
        }
        /// <summary>
        /// 读取某分类下的字典项目，项目之间用回车换行分隔
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static string GetDicString(int DicCateID)
        {
            string strSql = "select DicName from ZL_DataDic where DicCate=@DicCateID and IsUsed='1'and IsUsed='2'";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@DicCateID", SqlDbType.Int)
            };
            sp[0].Value = DicCateID;
            StringBuilder sb = new StringBuilder();
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                while (rdr.Read())
                {
                   StringHelper.AppendString(sb, rdr.GetString(0), ",");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 读取某分类下字典项目列表
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static DataTable GetDicListbyCate(int DicCateID)
        {

            string strSql = "select * from ZL_DataDic where DicCate=@DicCateID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@DicCateID", SqlDbType.Int)
            };
            sp[0].Value = DicCateID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 设定启用
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static bool SetUsed(int DicID)
        {
            string sql = "update ZL_DataDic set IsUsed=1 where DicID="+DicID;
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 设定不启用
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static bool SetUnUsed(int DicID)
        {
            string sql = "update ZL_DataDic set IsUsed=0 where DicID=" + DicID;
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 将分类的字典项目全部设为启用
        /// </summary>
        /// <param name="DicCateID"></param>
        /// <returns></returns>
        public static bool SetAllUsed(int DicCateID)
        {
            string strSql = "update ZL_DataDic set IsUsed='1' where DicCate=@DicCateID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@DicCateID", SqlDbType.Int)
            };
            sp[0].Value = DicCateID;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 将项目ID包含在项目ID数组字符串中的项目的启用状态设为指定状态
        /// </summary>
        /// <param name="DicIDArr"></param>
        /// <param name="IsUsed"></param>
        /// <returns></returns>
        public static bool SetUsedByArr(string ids, bool IsUsed)
        {
            string[] idsArr = ids.Split(',');
            string sql = "";
            SqlParameter[] sp = new SqlParameter[idsArr.Length + 1];

            for (int i = 0; i < idsArr.Length; i++)
            {
                sp[i] = new SqlParameter("ids" + i, idsArr[i]);
                sql += "@" + sp[i].ParameterName + ",";
            }
            sql = sql.TrimEnd(',');
            sp[idsArr.Length] = new SqlParameter("@IsUsed", SqlDbType.Char,1);
            sp[idsArr.Length].Value=IsUsed ? "1" : "0";
            string strSql = "update ZL_DataDic set IsUsed=@IsUsed where DicID in (" + sql + ")";
            return SqlHelper.ExecuteSql(strSql, sp);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_GradeOption
    {
        private string strTableName, PK;
        private M_GradeCate initMod = new M_GradeCate();
        private M_Grade gradeMod = new M_Grade();
        public B_GradeOption()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_GradeCate SelReturnModel(int ID)
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_GradeCate model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.CateID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_GradeCate model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 添加分级选项分类
        /// </summary>
        /// <param name="Cate">分类实例</param>
        /// <returns>成功状态</returns>
        public static bool AddCate(M_GradeCate model)
        {
            B_GradeOption bll = new B_GradeOption();
            if (model.CateID > 0)
                bll.UpdateByID(model);
            else
                bll.insert(model);
            return true;
        }
        /// <summary>
        /// 更新分级选项分类
        /// </summary>
        /// <param name="Cate">分类实例</param>
        /// <returns>成功状态</returns>
        public static bool UpdateCate(M_GradeCate model)
        {
            B_GradeOption bll = new B_GradeOption();
            if (model.CateID > 0)
                bll.UpdateByID(model);
            else
                bll.insert(model);
            return true;
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="CateID">分类ID</param>
        /// <returns>成功状态</returns>
        public bool DelCate(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + strTableName + " WHERE " + PK + " IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 添加分级选项
        /// </summary>
        /// <param name="GradeOption">分级选项实例</param>
        /// <returns>成功状态</returns>
        public static bool AddGradeOption(M_Grade model)
        {
            if (model.GradeID > 0)
                return Sql.UpdateByIDs(model.TbName, model.PK, model.GradeID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
            else
                Sql.insertID(model.TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        /// <summary>
        /// 更新分级选项
        /// </summary>
        /// <param name="GradeOption">分级选项实例</param>
        /// <returns>成功状态</returns>
        public static bool UpdateDic(M_Grade model)
        {
            if (model.GradeID > 0)
                return Sql.UpdateByIDs(model.TbName, model.PK, model.GradeID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
            else
                Sql.insertID(model.TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        /// <summary>
        /// 删除分级选项
        /// </summary>
        /// <param name="GradeID">选项ID</param>
        /// <returns></returns>
        public static bool DelGradeOption(int ID)
        {
            M_Grade model = new M_Grade();
            return Sql.Del(model.TbName, model.PK + "=" + ID);
        }
        public void DelOptioinsByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            SqlHelper.ExecuteSql("DELETE FROM " + strTableName + " WHERE " + PK + " IN(" + ids + ")");
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCateList()
        {
            string strSql = "select * from ZL_GradeCate Order by CateID Asc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 获取分类实例
        /// </summary>
        /// <param name="CateID">分类ID</param>
        /// <returns></returns>
        public M_GradeCate GetCate(int CateID)
        {
            string strSql = "select * from ZL_GradeCate where CateID=@CateID";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@CateID",CateID)
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (sdr.Read())
                {
                    return initMod.GetModelFromReader(sdr);
                }
                else
                {
                    return new M_GradeCate();
                }
            }
        }
        /// <summary>
        /// 分级选项列表
        /// </summary>
        /// <param name="CateID">分类ID</param>
        /// <param name="ParentID">父选项ID</param>
        /// <returns></returns>
        public static DataTable GetGradeList(int CateID, int ParentID = -1)
        {
            string strwhere = "";
            if (ParentID > -1)
            {
                strwhere += " AND ParentID=@ParentID";
            }
            string strSql = "select * from ZL_Grade where Cate=@CateID " + strwhere + " Order by GradeID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CateID",SqlDbType.Int),
                new SqlParameter("@ParentID",SqlDbType.Int)
            };
            sp[0].Value = CateID;
            sp[1].Value = ParentID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public static bool DelByIds(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string strsql = "DELETE FROM ZL_Grade WHERE GradeID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(strsql);
        }
        /// <summary>
        /// 分级选项列表
        /// </summary>
        /// <param name="CateID">分类ID</param>
        /// <param name="ParentID">父选项ID</param>
        /// <returns></returns>
        public static DataTable GetGradeListTop(int CateID, int ParentID, int num)
        {
            string strSql = "select top " + num + " * from ZL_Grade where Cate=@CateID and ParentID=@ParentID Order by GradeID Asc";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CateID",SqlDbType.Int),
                new SqlParameter("@ParentID",SqlDbType.Int)
            };
            sp[0].Value = CateID;
            sp[1].Value = ParentID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 获取分级选项实例
        /// </summary>
        /// <param name="GradeID">选项ID</param>
        /// <returns></returns>
        public static M_Grade GetGradeOption(int GradeID)
        {
            string strSql = "select * from ZL_Grade where GradeID=@GradeID";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@GradeID",GradeID)
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (sdr.Read())
                {
                    return new M_Grade().GetModelFromReader(sdr);
                }
                else
                {
                    return new M_Grade();
                }
            }
        }
        /// <summary>
        /// 同级选项是否重名
        /// </summary>
        /// <param name="CateID"></param>
        /// <param name="ParentID"></param>
        /// <param name="GradeName"></param>
        /// <returns></returns>
        public static bool IsExsitGrade(int CateID, int ParentID, string GradeName)
        {
            string strSql = "select GradeID from ZL_Grade where Cate=@CateID and ParentID=@ParentID and GradeName=@GradeName";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CateID",SqlDbType.Int),
                new SqlParameter("@ParentID",SqlDbType.Int),
                new SqlParameter("@GradeName",SqlDbType.NVarChar,50)
            };
            sp[0].Value = CateID;
            sp[1].Value = ParentID;
            sp[2].Value = GradeName;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp)) > 0;
        }
        /// <summary>
        /// 某级选项名称是否存在，存在则返回选项ID，否咋返回0
        /// </summary>
        /// <param name="CateID">分类ID</param>
        /// <param name="ParentID">父选项ID</param>
        /// <param name="GradeName">选项名称</param>
        /// <returns>返回存在的选项ID</returns>
        public static int GradeIDByName(int CateID, int ParentID, string GradeName)
        {
            string strSql = "select GradeID from ZL_Grade where Cate=@CateID and ParentID=@ParentID and GradeName=@GradeName";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CateID",SqlDbType.Int),
                new SqlParameter("@ParentID",SqlDbType.Int),
                new SqlParameter("@GradeName",SqlDbType.NVarChar,50)
            };
            sp[0].Value = CateID;
            sp[1].Value = ParentID;
            sp[2].Value = GradeName;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// 获取下级选项组合字符串 各选项由","隔开
        /// </summary>
        /// <param name="CateID"></param>
        /// <param name="GradeID"></param>
        /// <returns></returns>
        public static string GetNextGrade(int CateID, int GradeID)
        {
            string strSql = "select GradeName from ZL_Grade where Cate=@CateID and ParentID=@ParentID Order by GradeID Asc";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CateID",SqlDbType.Int),
                new SqlParameter("@ParentID",SqlDbType.Int)
            };
            sp[0].Value = CateID;
            sp[1].Value = GradeID;
            string gradestr = "";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                while (sdr.Read())
                {
                    if (string.IsNullOrEmpty(gradestr))
                        gradestr = sdr[0].ToString();
                    else
                        gradestr = gradestr + "," + sdr[0].ToString();
                }
                sdr.Close();
                sdr.Dispose();
            }
            return gradestr;
        }
    }
}

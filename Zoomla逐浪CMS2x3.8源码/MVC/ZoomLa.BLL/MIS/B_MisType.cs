using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
   public class B_MisType
    {
        public M_MisType model = new M_MisType();
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        public B_MisType() 
        {
            strTableName = model.TbName;
            PK = model.PK;
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName,PK,ID);
        }
        
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_MisType SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
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
        public DataTable Sels()
        {
            return Sql.Sel(strTableName);
        }


        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby);
        }
        /// <summary>
        /// 排序
        /// </summary>
        public DataTable Sel(string strWhere, string strOrderby,SqlParameter[] sp)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby,sp);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public DataTable SelPage(string strWhere,string strOrderby,int pageNum,int pageSize)
        {
            return Sql.SelPage(strTableName, PK,strWhere, strOrderby, pageNum, pageSize);
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
        public bool UpdateByID(M_MisType model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), model.GetFieldAndPara(), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        /// <summary>
        /// 根据ID删除
        /// </summary>
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        /// <summary>
        /// 添加
        /// </summary>
        public int insert(M_MisType model)
        {
            return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_Exam_Sys_Papers:ZL_Bll_InterFace<M_Exam_Sys_Papers>
    {
        private string strTableName, TbName, PK;
        private M_Exam_Sys_Papers initMod = new M_Exam_Sys_Papers();
        public B_Exam_Sys_Papers() 
        {
            strTableName = initMod.TbName;
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        #region old(disuse)
        public M_Exam_Sys_Papers GetSelectByM_Ps(M_Exam_Sys_Papers mps)
        {
            string sql = "SELECT * FROM [ZL_Exam_Sys_Papers] WHERE 1=1";
            if (mps != null)  //判断条件是否为空
            {
                if (mps.id > 0)
                {
                    sql += " AND [id]=" + mps.id;
                }
                if (!string.IsNullOrEmpty(mps.p_name))
                {
                    sql += " AND [p_name]='" + mps.p_name + "'";
                }
                if (mps.p_class > 0)
                {
                    sql += " AND [p_class]=" + mps.p_class;
                }
            }
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_Exam_Sys_Papers> mpss = GetSelByDataset(ds);
            if (mpss != null && mpss.Count > 0)
            {
                return mpss[0];
            }
            else
            {
                return new M_Exam_Sys_Papers();
            }
        }
        private List<M_Exam_Sys_Papers> GetSelByDataset(DataSet ds)
        {
            return null;
        }
        /// <summary>
        /// 查询所有试卷列表
        /// </summary>
        /// <returns></returns>
        public List<M_Exam_Sys_Papers> GetSelect_All()
        {
            string sql = "SELECT * FROM [ZL_Exam_Sys_Papers]";
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_Exam_Sys_Papers> mpss = GetSelByDataset(ds);
            if (mpss != null && mpss.Count > 0)
            {
                return mpss;
            }
            else
            {
                return new List<M_Exam_Sys_Papers>();
            }
        }
        public int GetInsert(M_Exam_Sys_Papers mps)
        {
            B_KeyWord keybll = new B_KeyWord();
            keybll.AddKeyWord(mps.TagKey, 3);
            return Insert(mps);
        }
        public bool GetDelete(int id)
        {
            return Sql.Del(strTableName, id);
        }
        public M_Exam_Sys_Papers GetSelect(int id)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Exam_Sys_Papers] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = id;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Exam_Sys_Papers();
                }
            }
        }

        /// <summary>
        /// 通过试卷分类ID查询
        /// </summary>
        /// <param name="classid">分类ID</param>
        /// <returns></returns>
        public DataTable Selelct_Classid(int classid)
        {
            string sqlStr = "SELECT * FROM ZL_Exam_Sys_Papers WHERE p_class=@p_class OR p_class IN ( SELECT C_id FROM ZL_Exam_Class WHERE C_Classid=@p_class )";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@p_class",classid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
        #endregion
        public int Insert(M_Exam_Sys_Papers model)
        {
            return Sql.insertID(TbName, initMod.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Exam_Sys_Papers model)
        {
            B_KeyWord keybll = new B_KeyWord();
            keybll.AddKeyWord(model.TagKey, 3);
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Exam_Sys_Papers SelReturnModel(int ID)
        {
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName,"","ID DESC");
        }
        public DataTable SelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteTable("SELECT * FROM " + strTableName + " WHERE ID IN(" + ids + ")");
        }
        /// <summary>
        /// 查询所有试卷(可按类型)
        /// </summary>
        /// <returns></returns>
        public DataTable SelAll(int type = 0)
        {
            string wherestr = type > 0 ? " AND p_class=" + type : "";
            string sql = "SELECT A.*,B.TypeName FROM " + TbName + " A LEFT JOIN ZL_Exam_PaperNode B ON A.p_class=B.ID WHERE 1=1" + wherestr;
            return SqlHelper.ExecuteTable(sql);
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM "+TbName+" WHERE ID IN("+ids+")";
            SqlHelper.ExecuteSql(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Exam
{
    public class B_Content_Publish:ZL_Bll_InterFace<M_Content_Publish>
    {
        public string TbName, PK;
        public M_Content_Publish initMod = new M_Content_Publish();
        public DataTable dt = null;
        public B_Content_Publish() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Content_Publish model)
        {
            return Sql.insert(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Content_Publish model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Content_Publish SelReturnModel(int ID)
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
        /// <summary>
        /// 返回前一个，或后一篇文章
        /// </summary>
        public M_Content_Publish SelModel(int curid,int pid,string flag)
        {
            string sql = "";
            switch (flag)
            {
                case "p"://上一个
                    sql = " Where ID<" + curid + " And Pid=" + pid + " Order By ID Desc";
                    break;
                default:
                    sql = " Where ID>" + curid + " And Pid=" + pid + " Order By ID";
                    break;
            }
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, sql))
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
        /// 获取上一篇或下一篇报纸的ID
        /// </summary>
        public int GetPid(int pid, string flag)
        {
            string sql = "Select ID From " + TbName + " a Where Pid=0 And (Select COUNT(ID) From "+TbName+" Where Pid=a.ID)>0 And";
            switch (flag)
            {
                case "p":
                    sql += " ID<=" + pid + " Order By ID ASC";
                    break;
                default:
                    sql += " ID>=" + pid + " Order By ID Desc";
                    break;
            }
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                pid = Convert.ToInt32(dt.Rows[0]["ID"]);
            }
            return pid;
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public DataTable SelByNid(int nid=0)
        {
            string sql = "Select *,(Select COUNT(ID) From " + TbName + " Where Pid=a.ID) as TitleNum From " + TbName + " a Where Pid=0";
            if (nid > 0)
                sql += " And Nid=" + nid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelByPid(int pid = 0)
        {
            string sql = "Select *,(Select COUNT(ID) From " + TbName + " Where Pid=a.ID) as TitleNum From " + TbName + " a Where Pid=" + pid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
    }
}

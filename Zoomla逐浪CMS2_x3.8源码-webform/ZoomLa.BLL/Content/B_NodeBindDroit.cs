using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
   public class B_NodeBindDroit
    {
        private string PK, strTableName;
        private M_NodeBindDroit initmod = new M_NodeBindDroit();
        public B_NodeBindDroit() 
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_NodeBindDroit SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_NodeBindDroit SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
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
        public DataTable Sel()
        {
            return SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where Fid > 0");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据NodeID返回模型
        /// </summary>
        public M_NodeBindDroit SelByNodeID(int nodeID) 
        {
           return  SelReturnModel(" Where NodeID ="+nodeID);
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_NodeBindDroit model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_NodeBindDroit model)
        {
            return Sql.insert(strTableName, model.GetParameters(),BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 根据审核码查询节点绑定权限
        /// </summary>
       public DataTable GetNodeBindDroitByStateCode(int stateCode)
        {
            return null;
        }
        /// <summary>
        /// 增加节点绑定权限
        /// </summary>
        public bool AddNodeBinDroit(M_NodeBindDroit mn)
        {
            if (AddNodeBinDroits(mn) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int AddNodeBinDroits(M_NodeBindDroit mn)
        {
            string strSql = "insert into ZL_NodeBindDroit(NodeID,FID) values(@NodeID,@FID)";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@NodeID", mn.NodeId), new SqlParameter("@FID", mn.FID) };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, sp);
        }
    }
}

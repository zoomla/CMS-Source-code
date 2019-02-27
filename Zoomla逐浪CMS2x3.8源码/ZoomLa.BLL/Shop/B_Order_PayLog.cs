using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_Order_PayLog
    {
        private string strTableName, PK;
        private M_Order_PayLog initMod = new M_Order_PayLog();
        public B_Order_PayLog()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_Order_PayLog SelReturnModel(int ID)
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

        public M_Order_PayLog SelModelByOrderID(int orderID) 
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, " where OrderID="+orderID))
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
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        /// <summary>
        /// 插入数据返回新ID
        /// </summary>
        public int insert(M_Order_PayLog model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Order_PayLog model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
    }
}
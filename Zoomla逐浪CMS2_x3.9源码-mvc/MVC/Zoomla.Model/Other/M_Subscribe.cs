using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Subscribe:M_Base
    {
        #region 构造函数
        public M_Subscribe()
        {
        }

        public M_Subscribe
        (
            int ID,
            string SubscribeName
        )
        {
            this.ID = ID;
            this.SubscribeName = SubscribeName;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] SubscribeList()
        {
            string[] Tablelist = { "ID", "SubscribeName" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 订阅类别
        /// </summary>
        public string SubscribeName { get; set; }
        #endregion

        public override string TbName { get { return "Subscribe"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"SubscribeName","NVarChar","100"}
                                 };
            return Tablelist;
        }
        public  SqlParameter[] GetParameters(M_Subscribe model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SubscribeName;
            return sp;
        }

        public  M_Subscribe GetModelFromReader(SqlDataReader rdr)
        {
            M_Subscribe model = new M_Subscribe();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SubscribeName = rdr["SubscribeName"].ToString();
            rdr.Close();
            return model;
        }
    }
}



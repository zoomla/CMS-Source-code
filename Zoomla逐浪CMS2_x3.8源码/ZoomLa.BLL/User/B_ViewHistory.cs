using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL.User
{
    public class B_ViewHistory
    {
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        private M_ViewHistory initMod = new M_ViewHistory();
        public B_ViewHistory()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }

        public bool Add(M_ViewHistory model)
        {
            string strSql = "INSERT INTO [ZL_ViewHistory] ([InfoId],[type],[UserID],[addtime]) VALUES (@InfoId,@type,@UserID,@addtime)";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName); 
        }
 
        /// <summary>
        /// 统计会员在此栏目下查看内容时,该节点共浏览的篇数
        /// </summary>
        /// <param name="generalid">内容id</param>
        /// <param name="nodeid">节点id</param>
        /// <returns></returns>
        public int GetViewHistoryInnerJoinCommonModel(int generalid,int nodeid)
        {
            string sql = "select count(v.infoid) from ZL_CommonModel as c inner join ZL_ViewHistory as v on c.generalid=v.infoid where v.infoid=" + generalid + " and c.nodeid=" + nodeid + "";
            return Convert.ToInt32((SqlHelper.ExecuteScalar(CommandType.Text, sql)));
        }
    }
}
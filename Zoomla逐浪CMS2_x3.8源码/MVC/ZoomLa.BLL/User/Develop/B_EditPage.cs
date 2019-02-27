using System.Data;
using ZoomLa.Model.User.Develop;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
namespace ZoomLa.BLL.User.Develop
{
    public class B_EditPage
    {

        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        private M_Zone_Node initMod = new M_Zone_Node();
        public B_EditPage()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public int AddLabel(M_Zone_Node model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model)); 
        }
        public DataTable SelLabel(M_Zone_Node model)
        {
            return Sql.Sel(strTableName, "[UserID]=" + model.UserID + " AND [NodeID]=" + model.NodeID, "");
        }
        public DataTable SelLabelByLabelID(M_Zone_Node model)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserID", model.UserID), new SqlParameter("NodeID", model.NodeID), new SqlParameter("LabelID", model.LabelID) };
            return Sql.Sel(strTableName, "[UserID]=@UserID AND [NodeID]=@NodeID AND [LabelID]=LabelID","",sp);
        }
        public bool UpLabel(M_Zone_Node model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
         }
        public bool UpLabelContent(M_Zone_Node model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
            //return Sql.UpLabel(strTableName, "[Content]=" + model.Content+"[Overflow]=" + model.Overflow+"[Display]=" + model.Display+"[Background]=" + model.Background, "[UserID]=" + model.UserID + " AND [NodeID]=" + model.NodeID + " AND [LabelID]=" + model.LabelID, M_Zone_Node.GetParameters(model));
        }
        public void UpLabelContent(string content, string background, string labelID, int userid, int nodeid, string overflow, string display)
        {
            SqlParameter[] sp = new SqlParameter[]{ 
                new SqlParameter("content",content),
                new SqlParameter("background",background),
                new SqlParameter("labelID",labelID),
                new SqlParameter("overflow",overflow),
                new SqlParameter("display",display)
            };
            string strsql = "update " + strTableName + " set content=@content,background=@background,labelID=@labelID,overflow=@overflow,display=@display where userid=" + userid + " and nodeid=" + nodeid + " and labelID=@labelID";
            SqlHelper.ExecuteNonQuery(CommandType.Text, strsql,sp);
        }
        //public int DelLabel(M_Zone_Node model)
        //{
        //    return Sql.DelLabel(strTableName,  "[UserID]=" + model.UserID + " AND [NodeID]=" + model.NodeID + " AND [LabelID]=" + model.LabelID, M_Zone_Node.GetParameters(model));  
        //}
        public void DelLabel(int userid, int nodeid, string labelid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userid", userid), new SqlParameter("nodeid", nodeid), new SqlParameter("labelid", labelid) };
            string str = "delete from " + strTableName + " where userid=@userid and nodeid=@nodeid and labelid=@labelid";
            SqlHelper.ExecuteNonQuery(CommandType.Text, str, sp);
        }
    }
}
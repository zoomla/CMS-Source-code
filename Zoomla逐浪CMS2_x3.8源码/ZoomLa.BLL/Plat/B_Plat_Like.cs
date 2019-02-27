using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.BLL.Helper;
using System.Data.Common;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_Like : ZL_Bll_InterFace<M_Plat_Like>
    {
        public string TbName, PK;
        public M_Plat_Like initMod = new M_Plat_Like(); 
        public B_Plat_Like()
        {
            TbName = initMod.TbName;
            PK = initMod.GetPK();
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName,PK,ID);
        }
        public int Insert(M_Plat_Like model)
        {
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 点赞操作
        /// </summary>
        public bool AddLike(int uid, int infoID, string source)
        {
            source = source.ToLower();
            int taguser = 0;//受赞人
            switch (source)
            {
                case "plat":
                    taguser = new B_Blog_Msg().SelReturnModel(infoID).CUser;
                    break;
                case "bar":
                    taguser = new B_Guest_Bar().SelReturnModel(infoID).CUser;
                    break;
                case "baike":
                case "guest":
                default:
                    break;
            }
            List<SqlParameter> sp = new List<SqlParameter> { new SqlParameter("@source", source) };
            string sql = "SELECT * FROM " + TbName + " WHERE CUser=" + uid + " AND MsgID=" + infoID + " AND Source=@source";
            DataTable dt = DBCenter.Sel(TbName, "CUser=" + uid + " AND MsgID=" + infoID + " AND Source=@source", "", sp);
            if (dt.Rows.Count <= 0)
            {
                return Insert(new M_Plat_Like() { CUser = uid, MsgID = infoID, TagUser = taguser, Source = source }) > 0;
            }
            return false;
        }
        /// <summary>
        /// 消赞操作
        /// </summary>
        public bool DelLike(int uid, int infoID, string source)
        {
            List<SqlParameter> splist = new List<SqlParameter> { new SqlParameter("source", source) };
            return DBCenter.DelByWhere(TbName, "CUser=" + uid + " AND MsgID=" + infoID + " AND Source=@source", splist);
        }
        /// <summary>
        /// 按内容id获取点赞用户
        /// </summary>
        public DataTable SelLikeUsers(int infoID, string source)
        {
            return SelByMsgIDS(infoID.ToString(), source);
        }
        /// <summary>
        /// 是否点赞过了
        /// </summary>
        public bool HasLiked(int uid,int infoID, string source)
        {
            List<SqlParameter> sp = new List<SqlParameter> { new SqlParameter("source", source) };
            string where = "CUser=" + uid + " AND MsgID=" + infoID + " AND Source=@source";
            return DBCenter.Count(TbName, where, sp) > 0;
        }
        public DataTable SelByMsgIDS(string ids, string source)
        {
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("source", source) };
            return DBCenter.JoinQuery("A.*,B.salt,B.HoneyName,B.UserName", TbName, "ZL_User", "A.CUser=B.UserID AND Source=@source", "MsgID IN (" + ids + ")","",sp.ToArray());
        }
        /// <summary>
        /// 获取该信息的点赞数
        /// </summary>
        public int Count(int infoID, string source)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("source", source) };
            string where = "MsgID =" + infoID + " AND Source=@source";
            return DBCenter.Count(TbName, where, sp);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", "CDate Desc");
        }
        public M_Plat_Like SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
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
        public bool UpdateByID(M_Plat_Like model)
        {
            return DBCenter.UpdateByID(model,model.ID);
        }
    }
}

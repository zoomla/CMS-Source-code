using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Content;
using ZoomLa.Model;
using ZoomLa.BLL.HtmlLabel;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{
    public class B_Content_FileBuy : ZL_Bll_InterFace<M_Content_FileBuy>
    {
        public string TbName, PK;
        M_Content_FileBuy initMod = null;
        public B_Content_FileBuy()
        {
            initMod = new M_Content_FileBuy();
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Content_FileBuy model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Content_FileBuy model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_Content_FileBuy SelReturnModel(int ID)
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
        //插入一条文件下载记录
        public int InsertLog(int gid, string Ranstr, M_UserInfo mu, M_Field_Down model, string field)
        {
            M_Content_FileBuy buymod = new M_Content_FileBuy();
            buymod.Gid = gid;
            buymod.Ranstr = Ranstr;
            buymod.UserID = mu.UserID;
            buymod.UserName = mu.UserName;
            buymod.BuyPrice = model.price.ToString();
            buymod.FName = model.fname;
            buymod.CDate = DateTime.Now;
            buymod.Url = model.url;
            buymod.Field = field;
            buymod.EndDate = model.hour > 0 ? DateTime.Now.AddMinutes(model.hour) : DateTime.MaxValue;
            return Insert(buymod);
        }
        public bool UpdateLog(int uid,int gid, string Ranstr, M_Field_Down model)
        {
            M_Content_FileBuy buymod = SelByGid(uid,gid, Ranstr);
            buymod.EndDate = model.hour > 0 ? DateTime.Now.AddMinutes(model.hour) : DateTime.MaxValue;
            return UpdateByID(buymod);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CreateTime Desc");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "","CreateTime DESC");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT A.*,B.Title FROM " + TbName + " A LEFT JOIN ZL_CommonModel B ON A.Gid=B.GeneralID WHERE A.UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public M_Content_FileBuy SelByGid(int uid,int gid, string ranstr)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE Gid=@gid AND Ranstr=@ranstr AND UserID="+uid;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@gid", gid), new SqlParameter("@ranstr", ranstr) };
            SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, sp);
            if (reader.Read())
                return initMod.GetModelFromReader(reader);
            else
                return null;
        }
    }
}

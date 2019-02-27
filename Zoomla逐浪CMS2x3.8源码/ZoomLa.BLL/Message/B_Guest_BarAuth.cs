using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Message
{
    public class B_Guest_BarAuth : ZL_Bll_InterFace<M_Guest_BarAuth>
    {
        public string TbName, PK;
        public M_Guest_BarAuth initMod = new M_Guest_BarAuth();
        public DataTable dt = null;
        public B_Guest_BarAuth()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Guest_BarAuth model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Guest_BarAuth model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Guest_BarAuth SelReturnModel(int ID)
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
        public M_Guest_BarAuth SelModelByUid(int cateid, int uid)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "BarID=" + cateid + " And Uid=" + uid))
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
            return DBCenter.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool AuthCheckByName(int cateid, int uid, string name)
        {
            if (uid < 1 || cateid < 1 || string.IsNullOrEmpty(name))
                return false;
            M_Guest_BarAuth authMod = SelModelByUid(cateid, uid);
            if (authMod == null) return false;
            switch (name)
            {
                case "look":
                    return authMod.Look == 1;
                case "send":
                    return authMod.Send == 1;
                case "reply":
                    return authMod.Reply == 1;
                default:
                    return false;
            }
        }
        /// <summary>
        /// needlog,send
        /// </summary>
        public bool AuthCheck(M_GuestBookCate cateMod, M_UserInfo mu, string authname = "needlog")
        {
            bool flag = false;
            switch (authname)
            {
                case "needlog"://登录权限
                    if (cateMod.NeedLog == 0)//匿名,登录,指定用户
                        flag = true;
                    else if (cateMod.NeedLog == 1 && mu.UserID > 0)
                        flag = true;
                    else if (cateMod.NeedLog == 2 && mu.UserID > 0)
                        flag = AuthCheckByName(cateMod.CateID, mu.UserID, "look");
                    break;
                case "send"://发贴权限
                    if (cateMod.PostAuth == 0)
                        flag = true;
                    else if (cateMod.PostAuth == 1 && mu.UserID > 0)
                        flag = true;
                    else if (cateMod.PostAuth == 2)
                    {
                        flag = AuthCheckByName(cateMod.CateID, mu.UserID, "send");
                    }
                    break;
            }
            return flag;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Project;
using ZoomLa.Model;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Plat.Note
{
    public partial class common : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Pro_Msg msgBll = new B_Pro_Msg();
        B_Pro_Progress progBll = new B_Pro_Progress();
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            M_APIResult result = new M_APIResult()
            {
                retcode = M_APIResult.Success,
                action = action
            };
            M_UserInfo mu = buser.GetLogin();
            switch (action)
            {
                case "msg_list":
                    {
                        int stepid = DataConvert.CLng(Request["ID"]);
                        int psize = DataConvert.CLng(Request["psize"]); if (psize < 1) { psize = 10; }
                        PageSetting setting = SelPage(PageCommon.GetCPage(), psize, stepid, 0);
                        result.result = JsonConvert.SerializeObject(setting.dt);
                        result.addon = setting.itemCount.ToString();
                    }
                    break;
                case "msg_add":
                    {
                        M_Pro_Msg msgMod = new M_Pro_Msg();
                        msgMod.CUser = mu.UserID;
                        msgMod.CUName = mu.UserName;
                        msgMod.Content = Request["Content"];
                        msgMod.StepID = DataConvert.CLng(Request["ID"]);
                        msgMod.ReplyMsgID = DataConvert.CLng(Request["rid"]);
                        if (msgMod.ReplyMsgID > 0)
                        {
                            M_Pro_Msg rpyMod = msgBll.SelReturnModel(msgMod.ReplyMsgID);
                            msgMod.RCUser = rpyMod.CUser;
                            msgMod.RCUName = rpyMod.CUName;
                        }
                        msgMod.ID = msgBll.Insert(msgMod);
                        PageSetting setting = SelPage(1, 1, 0, msgMod.ID);
                        result.result = JsonConvert.SerializeObject(setting.dt);
                    }
                    break;
                case "msg_del":
                    {

                    }
                    break;
                case "complete"://获取完成度
                    {
                        int proid = DataConvert.CLng(Request["proid"]);
                        result.result = progBll.GetComplete(proid).ToString();
                    }
                    break;
                default:
                    throw new Exception(action + ",不在请求列表");
            }
            RepToClient(result);
        }
        private PageSetting SelPage(int cpage, int psize, int stepid, int id)
        {
            string where = "";
            if (id > 0) { where += " A.ID=" + id; }
            if (stepid > 0) { where += " A.StepID=" + stepid; }
            PageSetting setting = new PageSetting()
            {
                fields = "A.*,B.UserName,B.UserFace,B.GroupName",
                cpage = cpage,
                psize = psize,
                t1 = "ZL_Pro_Msg",
                t2 = "ZL_User_PlatView",
                on = "A.CUser=B.UserID",
                where = where
            };
            setting.dt = DBCenter.SelPage(setting);
            return setting;
        }
        public void RepToClient(M_APIResult result) { Response.Clear(); Response.Write(JsonConvert.SerializeObject(result)); Response.Flush(); Response.End(); }
    }
}
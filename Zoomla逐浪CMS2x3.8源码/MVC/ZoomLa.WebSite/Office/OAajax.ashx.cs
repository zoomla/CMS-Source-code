using System;
using System.Web;
using ZoomLa.BLL;
using System.Data;

namespace ZoomLaCMS.MIS.OA
{
    /// <summary>
    /// OAajax 的摘要说明
    /// </summary>
    public class OAajax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //弹出提示存Cookies中吧,用于标记是否弹出过
            B_User buser = new B_User();
            string result = "";
            DataTable dt = new DataTable();
            string action = context.Request.Form["action"];
            switch (action)
            {
                case "GetUnreadMail"://Default.aspx
                    B_Message msgBll = new B_Message();
                    dt = msgBll.GetUnReadMail(buser.GetLogin().UserID);
                    if (dt.Rows.Count > 0)
                    {
                        result = "{\"num\":\"" + dt.Rows.Count + "\"}";
                    }
                    else
                    {
                        result = "0";
                    }
                    break;
                case "GetAffir"://待办公文和事务
                                //B_OA_Document oaBll = new B_OA_Document();
                                //dt = oaBll.SelByRefer(buser.GetLogin().UserID, 1);
                                //DataTable dt2 = oaBll.SelByDocType(buser.GetLogin().UserID, 1,1);
                                //    result = "{\"affair\":\"" + dt.Rows.Count + "\",\"affair2\":\""+dt2.Rows.Count+"\"}";
                    break;
                default:
                    result = "-1";
                    break;
            }
            context.Response.Write(result);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
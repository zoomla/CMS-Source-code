using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Edit;

namespace ZoomLaCMS.Edit.Doc
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                M_Doc_DataPack packMod = new M_Doc_DataPack(Request);
                if (packMod.version < DocCache.verMod.version)
                {
                    Response.Write(JsonConvert.SerializeObject(DocCache.verMod));
                }
                else { Response.Write("0"); }
                Response.Flush(); Response.End();
            }
            if (!IsPostBack)
            {
                if (!new B_User().CheckLogin())
                {
                    function.Script(this, "AjaxLogin();");
                }
            }
        }
        private void LongConnection()
        {
            if (function.isAjax())
            {

                M_Doc_DataPack packMod = new M_Doc_DataPack(Request);
                DateTime date1 = DateTime.Now.AddSeconds(packMod.time);//过期时间
                                                                       //bool ready = false;
                while (Response.IsClientConnected)
                {
                    Thread.Sleep(500);
                    if (DateTime.Compare(date1, DateTime.Now) < 0)//主动让客户端过期
                    {
                        Response.Write("0");
                        Response.End();
                        break;
                    }

                    //如有结果,则更新,忽略掉本人
                    //ZLLog.L("sesionID" + (!Session.SessionID.Equals(DocCache.verMod.sessionID)));
                    //ZLLog.L("version" + (packMod.version < DocCache.verMod.version));
                    //!Session.SessionID.Equals(DocCache.verMod.sessionID) && 
                    if (packMod.version < DocCache.verMod.version)
                    {
                        Response.Write(JsonConvert.SerializeObject(DocCache.verMod));
                        Response.End();
                        break;
                    }
                }
            }
        }
    }
}
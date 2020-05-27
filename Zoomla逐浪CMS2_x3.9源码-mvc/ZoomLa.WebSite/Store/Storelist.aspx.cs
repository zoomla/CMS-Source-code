using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Sns;

namespace ZoomLaCMS.Store
{
    public partial class Storelist : FrontPage
    {
        protected B_CreateShopHtml shll = new B_CreateShopHtml();
        protected B_CreateHtml bll = new B_CreateHtml();

        protected B_UserStoreTable ustbll = new B_UserStoreTable();
        protected B_User bubll = new B_User();
        protected B_Content cbll = new B_Content();

        public new int Cpage { get { int page = Page.RouteData.Values["CPage"] == null ? 1 : DataConverter.CLng(Page.RouteData.Values["CPage"].ToString()); page = page < 1 ? 1 : page; return page; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int sid = DataConverter.CLng(base.Request.QueryString["id"]);
                if (sid == 0)
                {
                    ErrToClient("店铺ID错误,Storelist.aspx?id=店铺ID");
                }
                else
                {
                    M_CommonData cdate = cbll.GetCommonData(sid);
                    DataTable dt = null;

                    try
                    {
                        dt = cbll.GetContent(sid);
                    }
                    catch
                    {
                        ErrToClient("该店铺不存在");
                    }
                    if (dt.Rows.Count < 1)
                    {
                        ErrToClient("该店铺不存在");
                    }
                    else
                    {
                        try
                        {
                            if (cdate.Status != 99)
                            {
                                ErrToClient("该店铺还在审核中");
                            }
                            else if (cdate.Status != 99)
                            {
                                ErrToClient("该店铺没有通过");
                            }
                            else
                            {
                                if (cdate.Status == 0)
                                {
                                    ErrToClient("该店铺被关闭了");
                                }
                                else
                                {
                                    B_Content cll = new B_Content();

                                    string StoreStyleID = cll.GetContent(sid).Rows[0]["StoreStyleID"].ToString();

                                    B_User ull = new B_User();
                                    string username = cll.GetCommonData(sid).Inputer;
                                    int userid = ull.GetUserByName(username).UserID;


                                    B_StoreStyleTable stll = new B_StoreStyleTable();
                                    M_StoreStyleTable stinfo = stll.GetStyleByID(DataConverter.CLng(StoreStyleID));

                                    string ListStyle = stinfo.ListStyle;

                                    string TemplateDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/" + ListStyle;

                                    TemplateDir = TemplateDir.Replace("/", @"\");
                                    string ContentHtml = FileSystemObject.ReadFile(TemplateDir);

                                    ContentHtml = this.bll.CreateHtml(ContentHtml, Cpage, sid, "1");

                                    if (ContentHtml.IndexOf("$Zone_") > -1)
                                    {
                                        ContentHtml = ZoneFun.MessageReplace(ContentHtml, 0, Guid.Empty, Guid.Empty);
                                    }
                                    Response.Write(ContentHtml);
                                }
                            }

                        }
                        catch
                        {
                            ErrToClient("店铺信息读取失败!ID错误!");
                        }
                    }
                }



            }
            else
            {
                ErrToClient("[产生错误的可能原因：没有指定店铺ID]");
            }
        }
    }
}
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

namespace ZoomLaCMS.Store
{
    public partial class StoreIndex : System.Web.UI.Page
    {
        B_UserStoreTable ustbll = new B_UserStoreTable();
        B_User bubll = new B_User();
        B_CreateHtml bll = new B_CreateHtml();
        B_CreateShopHtml sll = new B_CreateShopHtml();
        B_Content conBll = new B_Content();
        B_User buser = new B_User();
        B_StoreStyleTable styleBll = new B_StoreStyleTable();
        public int Item { get { return DataConverter.CLng(Request.QueryString["ID"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetInit();
        }

        //初始化
        private void GetInit()
        {
            if (Item < 1) { function.WriteErrMsg("店铺ID错误,StoreIndex.aspx?id=店铺ID"); }
            M_CommonData cdata = conBll.GetCommonData(Item);
            if (!cdata.IsStore) { function.WriteErrMsg("错误,指定的ID并非店铺"); }
            DataTable dt = conBll.GetContent(Item);
            if (dt != null && dt.Rows.Count < 1)
            {
                function.WriteErrMsg("该店铺不存在");
            }
            else if (cdata.Status == 0)
            {
                function.WriteErrMsg("该店铺被关闭了");
            }
            else if (cdata.Status != 99)
            {
                function.WriteErrMsg("该店铺还在审核中");
            }
            else
            {
                string username = cdata.Inputer;
                int userid = buser.GetUserByName(username).UserID;
                int StoreStyleID = DataConverter.CLng(dt.Rows[0]["StoreStyleID"]);//店铺风格
                M_StoreStyleTable stinfo = styleBll.GetStyleByID(StoreStyleID);
                string ContentHtml = SafeSC.ReadFileStr(SiteConfig.SiteOption.TemplateDir + "/" + stinfo.StyleUrl);
                //ContentHtml = sll.CreateShopHtml(ContentHtml, Item, userid);
                ContentHtml = this.bll.CreateHtml(ContentHtml, 0, Item, 0);
                Response.Write(ContentHtml);
            }
        }
    }
}
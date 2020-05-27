using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class SetBid : System.Web.UI.Page
{
    B_User ull = new B_User();
    B_Content cll = new B_Content();
    protected void Page_Load(object sender, EventArgs e)
    {
        function.SetPageNoCache();
        int ContentID = DataConverter.CLng(Request.QueryString["ContentID"]);//内容ID
        string menu = Request.QueryString["menu"];//操作命令
        int UserID = DataConverter.CLng(Request.QueryString["UserID"]);//用户ID
        //double BidMoney = DataConverter.CDouble(Request.QueryString["BidMoney"]);
        M_CommonData ctable = cll.GetCommonData(ContentID);

        if (ctable.GeneralID > 0)
        {
            M_CommonData cdata = cll.GetCommonData(ContentID);

            if (UserID > 0)
            {
               
            }
        }
        else
        {
            Response.Write("<script>alert('提交失败！没有找到数据！');</script>");
        }
    }
}

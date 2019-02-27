using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Xml;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;

public partial class manage_User_ServiceCode : System.Web.UI.Page
{
    B_Temp tempBll = new B_Temp();
    private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FName_T.Text = Call.SiteName;
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='ServiceSeat.aspx'>客服管理</a></li><li><a href='CodeList.aspx'>引用管理</a></li><li class='active'>在线生成</li>");
            if (Mid > 0)
            {
                MyBind();
            }
        }
    }
    private void MyBind() 
    {
        M_Temp tempMod = tempBll.SelReturnModel(Mid);
        FName_T.Text = tempMod.Str1;
        Content_T.Text = tempMod.Str2;
        txtCode.Text = "<script id='zlchat' data-option='{\"theme\":\"def\",\"id\":\"" + tempMod.ID + "\"}' src='/JS/Plugs/Chat/chat_def.js'></script>";
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Temp tempMod = new M_Temp();
        if (Mid > 0)
        {
            tempMod = tempBll.SelReturnModel(Mid);
        }
        tempMod.Str1 = FName_T.Text;
        tempMod.Str2 = Content_T.Text;
        tempMod.Str4 = Request.Form["theme_rad"];
        if (Mid > 0)
        {
            tempBll.UpdateByID(tempMod);
        }
        else
        {
            tempMod.UseType = 12;
            tempMod.Str3 = Guid.NewGuid().ToString();
            tempMod.ID = tempBll.Insert(tempMod);
        }
        function.WriteSuccessMsg("操作成功","ServiceCode.aspx?ID="+tempMod.ID);
    }
}

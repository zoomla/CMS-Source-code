using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
public partial class Manage_Pub_FormDesign : System.Web.UI.Page
{
    M_Pub pubMod = new M_Pub();
    B_Pub pubBll = new B_Pub();
    public int PubID { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            switch (action)
            {
                case "PubName":
                    break;
            }
            Response.Write(1); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            if (PubID > 0)
            {
                pubMod = pubBll.SelReturnModel(PubID);
                if (pubMod.PubType != 8) function.WriteErrMsg("该互动模块,并非互动表单!!");
                Json_Hid.Value = pubMod.Pubinfo;
                Title_T.Text = pubMod.PubName;
                PubName_T.Text = pubMod.PubTableName;
                Intro_T.Text = pubMod.PubTemplate;
                function.Script(this, "InitForm();");
            }
            else
            {
                function.Script(this, "DisDefault();");
            }
        }
    }
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        bool isnew = true; int newid = 0;
        if (PubID > 0)
        {
            pubMod = pubBll.SelReturnModel(PubID);
            newid = pubMod.Pubid;
            isnew = false;
        }
        pubMod.PubName = Title_T.Text;
        pubMod.PubTemplate = Intro_T.Text;
        pubMod.Pubinfo = Json_Hid.Value;
        if (isnew)
        {
            pubMod.PubTableName = "ZL_Pub_" + PubName_T.Text.Replace(" ", "");
            pubMod.PubType = 8;
            pubMod.PubCreateTime = DateTime.Now;
            pubMod.PubEndTime = DateTime.MaxValue;
            if (ZoomLa.SQLDAL.DBHelper.Table_IsExist(pubMod.PubTableName))
            {
                pubMod.PubTableName += "_" + function.GetRandomString(4);
                //function.WriteErrMsg(pubMod.PubTableName + "表已存在,请更换表名", "javascript:history.go(-1);");
            }
            pubBll.CreateModelInfo(pubMod);
            newid = pubBll.insert(pubMod);
        }
        else
        {
            pubBll.UpdateByID(pubMod);
        }
        Response.Redirect("/Rss/FormView.aspx?pid=" + newid);
    }
}
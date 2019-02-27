using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class MIS_ZLOA_ViewContent : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected M_UserInfo minfo = new M_UserInfo();
    protected B_Content bcon = new B_Content();
    protected M_CommonData mcon = new M_CommonData();
    protected B_ModelField bfield = new B_ModelField();
    protected B_Group bgro = new B_Group();
    protected B_OA_Document boa = new B_OA_Document();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Gid"]))
            {
                DataBind(Request.QueryString["Gid"]);
            }
            else 
            {
                function.WriteErrMsg("请先选择文档!!!");
            }
        }
    }
    protected void DataBind(string ID)
    {
        int generealID = Convert.ToInt32(ID);
        mcon = bcon.GetCommonData(generealID);
        minfo = buser.GetUserByName(mcon.Inputer);
        Title.Text = mcon.Title;
        LbCreatTime.Text = mcon.CreateTime.ToString("yyyy年MM月dd日 HH:mm:ss");
        userNameL.Text = mcon.Inputer;
        //function.WriteErrMsg(mcon.TableName + ":" + mcon.ItemID + ":" + generealID);
        DataRow dr = boa.SelByItemID(mcon.TableName, mcon.ItemID).Rows[0];
        SecretL.Text = dr["Secret"].ToString();
        UrgencyL.Text = dr["Urgency"].ToString();
        ImportanceL.Text = dr["Importance"].ToString();
        userGroupL.Text = dr["userGroupT"].ToString();
        ContentHtml.Text = dr["content"].ToString();
        if (!string.IsNullOrEmpty(dr["attach"].ToString()))
        {
            string[] af = dr["attach"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string h = "";
            for (int i = 0; i < af.Length; i++)
            {
                h += "<span class='disupFile'>";
                h += GroupPic.GetShowExtension(GroupPic.GetExtName(af[i]));
                h += "<a href='" + af[i] + "' title='点击下载'>" + af[i].Split('/')[(af[i].Split('/').Length - 1)] + "</span>";
            }
            publicAttachTD.InnerHtml = h;
        }
    }
}
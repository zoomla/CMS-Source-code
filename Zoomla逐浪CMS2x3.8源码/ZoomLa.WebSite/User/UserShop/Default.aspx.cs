using System;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_UserShop_Default : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_ModelField mfbll = new B_ModelField();
    B_Content conBll = new B_Content();
    B_Model bmll = new B_Model();
    B_StoreStyleTable sstbll = new B_StoreStyleTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetInit();
        }
    }
    //初始化
    private void GetInit()
    {
        M_UserInfo mu = buser.GetLogin();
        M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
        if (storeMod == null)
        {
            Response.Redirect("StoreApply.aspx");//申请店铺
        }
        else if (storeMod != null)
        {
            StoreUrl_L.Text = "<a href='/Store/StoreIndex.aspx?id=" + storeMod.GeneralID + "' target='_blank'> [浏览店铺]</a>";
        }
        if (storeMod.Status != 99)
        {
            Response.Redirect("StoreEdit.aspx");
        }
        else
        {
            StoreName_T.Text = storeMod.Title;
            DataTable cmdinfo = conBll.GetContent(storeMod.GeneralID);
            if (cmdinfo.Rows.Count > 0)
            {
                DataRow dr = cmdinfo.Rows[0];
                Label3.Text = dr["StoreCredit"].ToString();
                Label4.Text = GetState(dr["StoreCommendState"].ToString());
                M_ModelInfo mi = bmll.GetModelById(Convert.ToInt32(dr["StoreModelID"]));
                Label1.Text = mi.ModelName;
                DataTable slist = sstbll.GetStyleByModel(Convert.ToInt32(dr["StoreModelID"]), 1);
                //HiddenField1.Value = dr["StoreModelID"].ToString();
                SSTDownList.DataSource = slist;
                SSTDownList.DataTextField = "StyleName";
                SSTDownList.DataValueField = "ID";
                SSTDownList.DataBind();
                SSTDownList.SelectedValue = dr["StoreStyleID"].ToString();
                M_StoreStyleTable sst = sstbll.GetStyleByID(int.Parse(dr["StoreStyleID"].ToString()));
                Image1.ImageUrl = function.GetImgUrl(sst.StylePic);
                ModelHtml.Text = mfbll.InputallHtml(DataConvert.CLng(dr["StoreModelID"]), 0, new ModelConfig()
                {
                    ValueDT = cmdinfo
                });
            }
        }
    }
    private string GetState(string state)
    {
        switch (state)
        {
            case "0": return "普通"; 
            case "1": return "推荐"; 
            case "2": return "关闭"; 
            default: return ""; 
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
        DataTable cmdinfo = conBll.GetContent(storeMod.GeneralID);
        //----------
        DataTable dt = mfbll.GetModelFieldList(Convert.ToInt32(cmdinfo.Rows[0]["StoreModelID"]));
        Call commonCall = new Call();
        DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
        DataRow rs4 = table.NewRow();
        rs4[0] = "StoreName";
        rs4[1] = "TextType";
        rs4[2] = StoreName_T.Text;
        table.Rows.Add(rs4);
        DataRow rs5 = table.NewRow();
        rs5[0] = "StoreStyleID";
        rs5[1] = "int";
        rs5[2] = DataConverter.CLng(SSTDownList.Text);
        table.Rows.Add(rs5);
        M_StoreStyleTable sst = sstbll.GetStyleByID(int.Parse(SSTDownList.Text));
        M_CommonData CData = conBll.GetCommonData(storeMod.GeneralID);
        CData.Template = sst.StyleUrl;
        CData.Title = StoreName_T.Text;
        CData.IP = IPScaner.GetUserIP();
        conBll.UpdateContent(table, CData);
        function.WriteSuccessMsg("提交成功");
    }
    protected void SSTDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        M_StoreStyleTable sst = sstbll.GetStyleByID(int.Parse(SSTDownList.Text));
        Image1.ImageUrl = "/UploadFiles/" + sst.StylePic;
    }
}

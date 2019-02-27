using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
public partial class manage_Boss_AddBoss : CustomerPageAction
{
    B_BossInfo boll = new B_BossInfo();
    public int BossID { get { return DataConverter.CLng(Request.QueryString["bid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nodeid = string.IsNullOrEmpty(Request.QueryString["nodeid"]) ? "-1" : Request.QueryString["nodeid"].ToString();
            if (nodeid == "-1")
                // function.WriteErrMsg("参数错误");
                this.HdnParentId.Value = nodeid;
            GetPage();
            if (BossID > 0) { MyBind(); }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='Bosstree.aspx'>加盟商管理</a></li><li>添加加盟商</li>");
        }

    }
    public void MyBind()
    {
        M_BossInfo bossMod = boll.SelReturnModel(BossID);
        ddlProvince.SelectedValue = bossMod.Province;
        ddlCity.SelectedValue = bossMod.city;
        tx_ShopType.SelectedValue = bossMod.shoptype.ToString();
        tx_cname.Text = bossMod.CName;
        CMoney.Text = bossMod.CMoney.ToString();
        tx_adderss.Text = bossMod.Address;
        cx_Agent.Text = bossMod.Agent;
        cx_license.Text = bossMod.license;
        tx_CTel.Text = bossMod.CTel;
        tx_CInfo.Text = bossMod.CInfo;
        ContractNum.Text = bossMod.ContractNum;
        AccountPeople.Text = bossMod.AccountPeople;
        Bank.Text = bossMod.Bank;
        BankNum.Text = bossMod.BankNum;
        AccountPeople2.Text = bossMod.AccountPeople2;
        Bank2.Text = bossMod.Bank2;
        BankNum2.Text = bossMod.BankNum2;
        linkname.Text = bossMod.linkname;
        linksex.SelectedValue = bossMod.linksex;
        linkPositions.Text = bossMod.linkPositions;
        linktel.Text = bossMod.linktel;
        fax.Text = bossMod.fax;
        PostCode.Text = bossMod.PostCode;
        email.Text = bossMod.email;
        Documents.SelectedValue = bossMod.Documents;
        DocumentsNUm.Text = bossMod.DocumentsNUm;
    }
    private void GetPage()
    {
    
  
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int nodeid = DataConverter.CLng(this.HdnParentId.Value);
        M_BossInfo BInfo = new M_BossInfo();
        BInfo.parentid = nodeid;
        if (nodeid != 0)
        {
            boll.GetUpdateChild(nodeid);
        }

        BInfo.AccountPeople = this.AccountPeople.Text.Trim();
        BInfo.AccountPeople2 = this.AccountPeople2.Text.Trim();
        BInfo.Address = this.tx_adderss.Text.Trim();
        BInfo.Agent = this.cx_Agent.Text.Trim();
        BInfo.Bank = this.Bank.Text.Trim();

        BInfo.Bank2 = this.Bank2.Text.Trim();
        BInfo.BankNum = this.BankNum.Text.Trim();
        BInfo.BankNum2 = this.BankNum2.Text.Trim();
        BInfo.CInfo = this.tx_CInfo.Text.Trim();
        BInfo.city = this.ddlCity.SelectedValue;
        // Response.Write("<script language=javascript>alert('" + BInfo.city + "');location.href='Bosstree.aspx';</script>");
        BInfo.Province = this.ddlProvince.SelectedValue;
        BInfo.CName = this.tx_cname.Text.Trim();
        BInfo.ContractNum = this.ContractNum.Text.Trim();
        BInfo.CTel = this.tx_CTel.Text.Trim();
        BInfo.Documents = this.Documents.Text.Trim();

        BInfo.DocumentsNUm = this.DocumentsNUm.Text.Trim();
        BInfo.email = this.email.Text.Trim();
        BInfo.fax = this.fax.Text.Trim();
        BInfo.license = this.cx_license.Text.Trim();
        BInfo.linkname = this.linkname.Text.Trim();

        BInfo.linkPositions = this.linkPositions.Text.Trim();
        BInfo.linksex = this.linksex.SelectedValue;
        BInfo.linktel = this.linktel.Text.Trim();
        BInfo.PostCode = this.PostCode.Text.Trim();

        BInfo.shoptype = DataConverter.CLng(this.tx_ShopType.SelectedValue);
        BInfo.CMoney = DataConverter.CDouble(this.CMoney.Text);


        if (boll.GetInsert(BInfo) > 0)
        {
            function.WriteSuccessMsg("添加成功!", "Bosstree.aspx");
        }
        else
        {
            function.WriteErrMsg("添加失败!", "Bosstree.aspx");
        }
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  Response.Write("<script language=javascript>alert('" + ddlCity.SelectedValue + "');location.href='Bosstree.aspx';</script>");
    }
}

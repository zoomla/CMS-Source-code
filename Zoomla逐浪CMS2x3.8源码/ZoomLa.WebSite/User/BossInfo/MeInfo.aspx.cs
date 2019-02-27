using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;

public partial class User_BossInfo_MeInfo : System.Web.UI.Page
{
    B_BossInfo boll = new B_BossInfo();
    private B_User bll = new B_User();
    private B_Node bnll = new B_Node();
    private B_Card bc = new B_Card();
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
            M_UserInfo uinfo = buser.GetLogin();
            M_BossInfo MBoss = boll.SelReturnModel(uinfo.UserID);
            if (MBoss==null)
            {
                function.WriteErrMsg("未能找到您的加盟信息！");
            }
            if (MBoss.CName=="")
                //function.WriteErrMsg("你不是加盟商");
            this.tx_cname.Text = MBoss.CName;

            if (MBoss.shoptype == 1)
                this.Label4.Text = "服务中心";
            else
                this.Label4.Text = "服务E店";

                this.Label5.Text=MBoss.CName;
                this.Label6.Text=MBoss.Address;
                this.Label9.Text = MBoss.Agent;
                this.Label10.Text = MBoss.license;
                this.Label11.Text = MBoss.CTel;
                this.Label12.Text = MBoss.CInfo;
                this.tx_money.Text=MBoss.ContractNum;
                this.tx_zong.Text=MBoss.AccountPeople;
                this.tx_num.Text = MBoss.Bank;
                this.Label13.Text = MBoss.BankNum;
                this.Label14.Text = MBoss.AccountPeople2;
                this.Label15.Text = MBoss.Bank;
                this.Label16.Text = MBoss.BankNum2;
                this.Label1.Text = MBoss.linkname;
                this.Label2.Text = MBoss.linksex;
                this.Label3.Text = MBoss.linkPositions;
                this.Label17.Text = MBoss.linktel;
                this.Label18.Text = MBoss.fax;
                this.Label19.Text = MBoss.PostCode;
                this.Label20.Text = MBoss.email;
                this.Label21.Text = MBoss.Documents;
                this.Label22.Text = MBoss.DocumentsNUm;
       
                //province pp = ct.GetprovinceByCode(Server.MapPath(@"../Command/SystemData.xml"), MBoss.Province);
                //Pcity cc = ct.GetCityByCode(Server.MapPath(@"../Command/SystemData.xml"), MBoss.city, MBoss.Province);
                //this.tx_cname.Text = pp.Name;
            #region 上级代理商
                if (MBoss.parentid == 0)
                {
                    Label7.Text = "你为最高级代理商";
                }
                else
                {
                    M_BossInfo PBoss = boll.GetSelect(DataConverter.CLng(MBoss.parentid));
                    Label7.Text = PBoss.CName;
                    Label8.Text = PBoss.CTel;
                }
            #endregion
        }
    }

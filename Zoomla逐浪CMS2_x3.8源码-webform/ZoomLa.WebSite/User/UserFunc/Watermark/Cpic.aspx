<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cpic.aspx.cs" Inherits="Manage_I_Plus_Cpic" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>图片背景水印</title>
</head>
<body>
    <form id="form1" runat="server">
        <%
            //ZoomLa.BLL.B_User buser = new ZoomLa.BLL.B_User();
            //int uid = Convert.ToInt32(Request.QueryString["uid"]);
            //ZoomLa.Model.M_UserInfo mu = buser.GetSelect(uid);
            UName_L.Text = Request.QueryString["Name"];
            UName_L2.Text = Request.QueryString["GiveMan"];
            VName_L.Text = Request.QueryString["VName"];
            authcode_L.Text = Request.QueryString["Auth_Code"];
            CardName_L.Text =Request.QueryString["CardName"];
            Auth_Name_L.Text = Request.QueryString["Auth_Name"];
            //authcode_L.Text = Request.QueryString["AuthCode"];
            stime.InnerText = string.IsNullOrEmpty(Request.QueryString["StartDate"]) ? DateTime.Now.ToString("yyyy年01月01日") : DateTime.Parse(Request.QueryString["StartDate"]).ToString("yyyy年MM月dd日");
            etime.InnerText = string.IsNullOrEmpty(Request.QueryString["EndDate"]) ? DateTime.Now.AddYears(1).ToString("yyyy年01月01日") : DateTime.Parse(Request.QueryString["EndDate"]).ToString("yyyy年MM月dd日");
            CreateImage("微信号：" + Request.QueryString["VName"]);
        %>
        <div>
            <div runat="server" style="padding-left: 0px;">
                <asp:Image runat="server" ID="bgimg" Style="width: 1024px; height: 723px;font-family:'Microsoft MHei';" />
                <div style="position: absolute; top: 0px; left: 0px;width:100%;padding-left:80px;">
                    <div id="row1" style="margin-top: 205px; text-align: right; padding-right: 180px;">
                        <asp:Label runat="server" ID="authcode_L" Text="1401459842002690" Font-Size="16" />
                    </div>
                    <div id="row2" style="margin-top: 80px; ">
                        <div style="float: left;padding-right:5px;height: 80px;line-height:80px;font-size: 26px;width:100px;">兹授权</div>
                        <div style="float: left;font-size: 20px;line-height:30px;width:300px;">
                            <div style="border-bottom: 1px solid black;"><span style="padding-left:15px;">姓名：</span><span style="padding-left:30px;"><asp:Label runat="server" ID="UName_L" /></span></div>
                            <div style="border-bottom: 1px solid black;"><span style="padding-left:15px;">微信号：</span><span style="padding-left:30px;"><asp:Label runat="server" ID="VName_L" /></span></div>
                        </div>
                        <div style="float:left;padding-left:5px;">
                            <div style="float:left;line-height:80px;padding-right:10px;font-size:26px;">于</div>
                            <div style="border-bottom:1px solid black;float:left;padding-top:23px;width:460px;text-align:right;font-size:20px;">
                                <asp:Label runat="server" ID="UName_L2" style="float:left;padding-left:10px;" /> 
                                (选填:公司/个体户/个人)
                            </div>
                        </div> 
                        <div style="clear: both;"></div>
                    </div>
                    <div id="row3" style="font-size:26px;">
                        <div style="width:auto;float:left;">取得我司旗下</div>
                        <div style="font-size:20px;float:left; border-bottom:1px solid black;width:500px;text-align:center;"><asp:Label runat="server" ID="CardName_L" /></div>
                        <div style="float:left;">品牌的经营销售权。</div>
                        <div style="clear:both;"></div>
                    </div>
                    <div id="row4"><!--授权时间,备注,授权企业-->
                        <div id="row4_left">
                            <div>
                                <span style="display:block;"> 备注：本授权书以正本为有效文本,不得影印,涂改,转让。</span>
                                <span style="display:block;margin-top:5px;"><%:Call.SiteName
%>拥有此授权证书的最终解释权。</span>
                            </div>
                        </div>
                        <div id="row4_right" style="float:right;position:relative;top:-100px;width:500px;font-size:20px;">
                            <div>
                                <div style="float:left;width:20%;">授权时间：</div>
                                <div style="float: left; width: 79%;">
                                    <span runat="server" id="stime"></span><br />
                                    <span runat="server" id="etime"></span>
                                </div>
                                <div style="clear:both;"></div>
                            </div>
                            <div style="padding-top:30px;">授权公司：<asp:Label runat="server" ID="Auth_Name_L" /></div>
                            <div style="position:absolute;top:40px;left:160px;"><img src="sign.gif" style="width:120px;height:120px;" /></div>
                            <div style="padding-top:10px;">代表人(签名)：</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

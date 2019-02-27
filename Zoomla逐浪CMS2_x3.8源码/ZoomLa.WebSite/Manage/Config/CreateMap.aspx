<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateMap.aspx.cs" Inherits="test_test" MasterPageFile="~/Manage/I/Default.master" Debug="true" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>后台管理索引</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mid_txtbox text-center">
    <div class="mid_txtcnt">
    <h1>快速生成系统应用查询索引</h1>
    <h4>一般维护时发布一次即可</h4>
     </div>
    <div class="creat_btn">
    <asp:LinkButton runat="server" ID="Sure_Btn" OnClick="Sure_Btn_Click" CssClass="btn btn-primary" OnClientClick="return confirm('确定要重新生成SiteMap.config吗?');" ><i class="fa fa-th-large"></i><br/>生成后台索引</asp:LinkButton>
<asp:LinkButton ID="SureUser_Btn" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('确定要生成UserMap.config吗?');" OnClick="SureUser_Btn_Click" ><i class="fa fa-user"></i><br />生成会员索引</asp:LinkButton>
    </div>
    </div>
</asp:Content>
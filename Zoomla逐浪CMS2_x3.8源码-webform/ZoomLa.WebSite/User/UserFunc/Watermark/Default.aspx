<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_UserFunc_Watermark_Default" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>授权证书生成</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="office" data-ban="Watermark"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">授权证书生成</li> 
    </ol>
</div>
<div class="container u_cnt btn_green">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-center" title="姓名">姓名：</td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" runat="server" ID="Name" /></td>
            <td class="text-center" title="微信号码">微信号码：</td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" runat="server" ID="VName" /></td>
            <td class="text-center" title="授权人">授权人：</td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" runat="server" ID="GiveMan" /></td>
        </tr>
        <tr>
            <td class="text-center" title="品牌">品牌：</td>
            <td><asp:TextBox data-state="edit" CssClass="form-control" runat="server" ID="CardName" /></td>
            <td class="text-center" title="授权时间">授权时间：</td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" onclick="WdatePicker();" runat="server" ID="StartDate" /></td>
            <td class="text-center" title="到期时间">到期时间： </td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" onclick="WdatePicker();" runat="server" ID="EndDate" /></td>
        </tr>
        <tr>
            <td class="text-center" title="标题">标题：</td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" runat="server" ID="Title_T" /></td>
            <td class="text-center" title="授权编号">授权编号：</td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" runat="server" ID="Auth_Code_T" /></td>
            <td class="text-center" title="授权企业">授权企业：</td>
            <td><asp:TextBox CssClass="form-control" data-state="edit" runat="server" ID="Auth_Name_T" /></td>
        </tr>
        <tr><td colspan="6" class="text-center">
            <input type="button" class="btn btn-primary" value="确定生成" onclick="EditImage()" />
            <a href="javascript:ClearData()" class="btn btn-primary">重置参数</a>
            </td></tr>
    </table>
    </div>
    <div>
        
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    function EditImage() {
        if ($("#Name").val() != "" && $("#Auth_Code_T").val()!="") {
            var url = "HtmlToJpg.aspx?Name=" + $("#Name").val() + "&VName=" + $("#VName").val() + "&GiveMan=" + $("#GiveMan").val() + "&CardName=" + $("#CardName").val() + "&StartDate=" + $("#StartDate").val() + "&EndDate=" + $("#EndDate").val() + "&Title=" + $("#Title_T").val() + "&Auth_Code=" + $("#Auth_Code_T").val() + "&Auth_Name=" + $("#Auth_Name_T").val();
            window.open(url);
        } else {
            alert("证书名和授权编号不能为空！");
        }
        
    }
    function ClearData() {
        $("[data-state=edit]").val("");
    }
</script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDetail.aspx.cs" Inherits="Plat_Blog_InfoDetail" ClientIDMode="Static" MasterPageFile="~/Plat/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
    <title>个人信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="height: 40px; line-height: 40px; background-color: #f7f7f7;">
            <div style="width:50%;float:left;padding-left:10px;"><span class="fa fa-remove" title="关闭" onclick="CloseSelf();"></span></div>
            <div style="width:50%;float:left;padding-right:10px;text-align:right;">
            <%--    <span class="fa fa-envelope" style="margin-right:5px;" title="站内邮件"></span>
                <span class="fa fa-comment" title="站内短信"></span>--%>
            </div>
        </div>
    <div id="content">
        <div id="head">
            <div id="head_img">
                <asp:Image runat="server" class="imgface" ID="pre_img" Style="width: 100px; height: 100px;" />
            </div>
            <div id="head_name">
                <fieldset disabled="disabled">
                    <div class="form-group">
                        <label>用户名：</label>
                        <input type="text" readonly="readonly" id="username_T" runat="server" class="form-control"  value="zzf993" />
                    </div>
                    <div class="form-group">
                        <label>状<span style="margin-right:13px;"></span>态：</label>
                        <input type="text" readonly="readonly" id="status_T"  runat="server" class="form-control"  value="正常" />
                    </div>
                </fieldset>
            </div>
        </div>
        <div id="body">
            <table  id="body_table" class="table table-striped">
                <tr>
                    <td class="td_name"> <label>真实姓名：</label></td>
                    <td><asp:Label ID="trueName_L" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="td_name"><label>公司职务：</label></td>
                    <td><asp:Label ID="position_L" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="td_name"><label>联系电话：</label></td>
                    <td><asp:Label ID="mobile_L" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="td_name"><label>所属部门：</label></td>
                    <td><asp:Label ID="group_L" runat="server"></asp:Label></td>
                </tr>
                <tr><td colspan="2" style="padding-left:50px;">
                    <a href="/Plat/Blog?Uids=<%:UserID %>" class="btn btn-primary" target="_parent">他的工作流</a>
                    <a href="/Plat/Mail/MessageSend.aspx?uid=<%:UserID %>" class="btn btn-primary" target="_parent">发送邮件</a>
                    </td></tr>
            </table>
        </div>
        <div id="foot"></div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        #content{width:600px; height:800px; margin-left:auto; margin-right:auto; margin-top:10px;}
        #head{ height:100px; border-bottom:1px solid #CCC;}
        #head_img{float:left;}
        #head_name{float:left; margin-left:10px;}
        #body{ margin:10px;}
        .td_name{ width:30%; text-align:center;}
        .fa{cursor:pointer;}
        .fa:hover{color:#0066cc;}
    </style>
    <script type="text/javascript">
        function CloseSelf()
        {
            $(parent.document).find("#ShowUser_Div").hide();
            $(parent.document).find("#ShowUser_if").attr("src","");
        }
    </script>
</asp:Content>

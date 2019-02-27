<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompDetail.aspx.cs" Inherits="ZoomLaCMS.Plat.Group.CompDetail"  MasterPageFile="~/Plat/Main.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUps" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>公司详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container platcontainer">
        <div runat="server" id="compinfo_div" visible="false">
           <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">公司信息</span></div>
            <table class="table table-bordered table-hover table-striped">
                <tr>
                    <td>公司简称：</td>
                    <td>
                        <ZL:TextBox runat="server" ID="CompShort_T" AllowEmpty="false" CssClass="form-control text_300" MaxLength="4" /></td></tr>
                <tr>
                    <td class="td_m">公司名称：</td>
                    <td><ZL:TextBox runat="server" ID="CompName_T" AllowEmpty="false" CssClass="form-control text_300" /></td>
                </tr>
                <tr runat="server" id="admin_logo_tr" visible="false">
                    <td>公司LOGO：</td>
                    <td>
                        <ZL:SFileUps ID="SFiles_Up" runat="server" />
                    </td>
                </tr>
                <tr runat="server" id="view_logo_tr" visible="false"><td>公司LOGO：</td><td><asp:Image runat="server" ID="Logo_Img" CssClass="img_mid" /></td></tr>
                <tr>
                     <td>联系电话：</td>
                     <td><asp:TextBox runat="server" ID="Telephone_T" CssClass="form-control text_300" /></td>
                </tr>
                <tr>
                     <td>联系手机：</td>
                     <td><asp:TextBox runat="server" ID="Mobile_T" CssClass="form-control text_300" /></td>
                </tr>
                <tr>
                     <td>公司网址：</td>
                     <td><asp:TextBox runat="server" ID="CompHref_T" CssClass="form-control text_300" /></td>
                </tr>
                <tr>
                    <td>公司备注：</td>
                    <td><asp:TextBox runat="server" ID="CompDesc_T" onkeydown="return GetEnterCode('focus','TxtCompType');" CssClass="form-control text_300"></asp:TextBox></td>
                </tr>
                <tr style="display:none;">
                    <td>公司状态：</td>
                    <td><asp:TextBox runat="server" ID="CompStatus_T" onkeydown="return GetEnterCode('click','BtnSave');"  CssClass="form-control text_300"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>创建人：</td>
                    <td><asp:TextBox runat="server"  ID="CompUser_T" CssClass="form-control text_300" disabled="disabled"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>创建时间：</td>
                    <td><asp:TextBox runat="server" ID="CreateTime_T" CssClass="form-control text_300" disabled="disabled"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="adminop_tr" visible="false">
                    <td></td>
                    <td>
                        <asp:Button ID="BtnSave" CausesValidation="false" runat="server" ToolTip="保存修改" Text="修改信息" OnClick="BtnSave_Click" CssClass="btn btn-info" OnClientClick="if(!IsSelectedId()){alert('请选择修改项');return false;}else{return confirm('你确定要修改选中内容吗？')}" /></td>
                </tr>
            </table>   
        </div>
        <div runat="server" id="person_div" visible="false">
            <div>做一个人的美男子，静静的也很好^_^||</div>
            <div>您当前是个人办公状态没有加入任何企业</div>
            <div class="text-center">
                <a href="/Plat/Common/JoinToComp.aspx" class="btn btn-info"><i class="fa fa-compress"></i> 加入企业</a>
                <a href="/Plat/Common/CompCert.aspx" class="btn btn-info"><i class="fa fa-file"> 申请认证</i></a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script>
    $("#up").click(function () {
        $("#photo").click();
    });
    $("#photo").click(function () {
        var la = document.getElementById("label");
        if (la.textContent == "") {
            alert("请先上传");
        } else {
            document.getElementById("img_D").style.display = "block";
            var img = document.getElementById("img");
            img.src = la.textContent;
            document.getElementById("label").style.display = "none";
        }
    });
    $("#close").click(function () {
        document.getElementById("img_D").style.display = "none";
    });
    function checkFile() {
        var filename = $("#File_Up").val();
        if (filename != "") {
            var checkex = ["jpg", "png", "gif", "ico"];
            var exname = filename.substr(filename.lastIndexOf(".")+1, filename.length - filename.lastIndexOf(".") + 1);
            for (var i = 0; i < checkex.length; i++) {
                if (checkex[i] == exname)
                    return true;
            }
            alert("图片格式不正确！");
        } else {
            alert("没有选择图片！");
        }
        return false;
    }
    setactive("公司");
</script>
</asp:Content>


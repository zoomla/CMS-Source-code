<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RemoteImg.aspx.cs" Inherits="Plugins_WebUploader_RemoteImg" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>获取远程图片</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel-body">
        <div class="alert alert-info" style="margin-top:0px;">请输入远程图片Http路径,以回车分隔</div>
        <asp:TextBox runat="server" ID="Remote_Url" TextMode="MultiLine" CssClass="form-control" Style="height: 200px; max-width: 100%; width: 100%;"></asp:TextBox>
    </div>
    <div class="panel-footer">
        <asp:Button runat="server" ID="GetPic_Btn" Text="确定" OnClick="GetPic_Btn_Click" OnClientClick="ShowWait();" CssClass="btn btn-primary" />
    </div>
    <asp:HiddenField runat="server" ID="NodeID_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        var json =  JSON.parse('<%=Request["json"]%>');
        $("#NodeID_Hid").val(json.pval.nodeid);
        function AddAttach(str) {
            var ret = { _raw: str };
            setTimeout(function () { parent.AddAttach(null, ret, json.pval); },200);
        }
        function ShowWait()
        {
            $("#GetPic_Btn").val("抓取中");
            setTimeout(function () { $("#GetPic_Btn").addClass("disabled"); }, 50);
        }
    </script>
</asp:Content>

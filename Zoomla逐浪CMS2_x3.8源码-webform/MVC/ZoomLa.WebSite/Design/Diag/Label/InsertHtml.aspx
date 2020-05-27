<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertHtml.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Label.InsertHtml" MasterPageFile="~/Common/Master/Empty.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>插入Html</title>
    <link href="/App_Themes/User.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr><td class="td_l text-right">代码段名称</td>
        <td><ZL:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="String" /></td>
    </tr>
    <tr>
        <td class="text-right">代码内容<br />(支持JS与HTML)</td>
        <td>
            <ZL:TextBox runat="server" TextMode="MultiLine" ID="Html_T" CssClass="form-control" Style="height: 300px;" AllowEmpty="false" />
        </td>
    </tr>
    <tr><td></td><td>
        <asp:Button runat="server" ID="Save_Btn" Visible="false" CssClass="btn btn-primary" Text="插入Html" OnClick="Save_Btn_Click" />
        <asp:Button runat="server" ID="Edit_Btn" Visible="false" CssClass="btn btn-primary" Text="保存修改" OnClick="Edit_Btn_Click" />
                 </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/Design/JS/sea.js"></script>
<script>
    var base64 = null
    var editpage = { id: "<%:Mid%>", dom: null, model: null, scope: null };
    seajs.use(["base64"], function (instance) {
        base64 = instance;
        if (editpage.id && editpage.id != "") {
            editpage.model = top.page.compList.GetByID(editpage.id);
            $("#Html_T").val(base64.decode(editpage.model.dataMod.label));
            $("#Alias_T").val(editpage.model.dataMod.alias);
        }
    })
    function LabelToDesign(label, html) {
        //唯一身份标识,避免标签过长导致无法定位
        model.dataMod.guid = newGuid();
        model.dataMod.name = $("#Alias_T").val();
        model.dataMod.alias = $("#Alias_T").val();
        model.dataMod.label = label;
        model.htmlTlp = html;
        parent.AddComponent(model);
    }
    function SaveEdit(label, html) {
        var compMod = top.page.compList.GetByID("<%:Mid%>");
        compMod.dataMod.alias = $("#Alias_T").val();
        compMod.dataMod.label = label;
        compMod.TempHtml = html;
        setTimeout(function () {
            compMod.Render();
            top.EventBind();
            top.CloseDiag();
        }, 500);
     }
    // var model = { dataMod: { value: "", name: "", label: "", guid: "",alias:"" }, config: { type: "label", css: "candrag", style: 'position:absolute;top:30%;left:40%;' }, htmlTlp: "" };
    var model = {
        dataMod: { value: "", name: "", label: "", guid: "", alias: "" },
        config: { type: "label", compid: "", css: "candrag", style: "position:absolute;top:30%;left:40%;", bodyid: "" },
        htmlTlp: ""
    };
</script>
</asp:Content>


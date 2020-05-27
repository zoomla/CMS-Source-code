<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddScenceTlp.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.AddScenceTlp" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<%@ Register Src="~/Manage/I/ASCX/TreeTlpDP.ascx" TagPrefix="ZL" TagName="TreeTlp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/dist/css/star-rating.min.css" rel="stylesheet" />
    <script src="/dist/js/star-rating.min.js"></script>
    <title>场景模板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
<tr>
    <td class="td_m">模板名称:</td>
    <td>
        <ZL:TextBox runat="server" ID="TlpName_T" CssClass="form-control text_300" AllowEmpty="false"></ZL:TextBox>
    </td>
</tr>
<tr><td>模板分类:</td><td>
    <ZL:TreeTlp ID="NodeTree" Selected="Node_Hid" NodeID="ID" Pid="Pid" Name="Name" EmpyDataText="请选择分类!" runat="server" />
    <asp:HiddenField ID="Node_Hid" runat="server" />
               </td></tr>
<tr <%=Mid>0 ? "":"hidden" %>>
    <td>创建时间:</td>
    <td><asp:Label ID="CDate_L" runat="server" /></td>
</tr>
<tr><td>模板价格:</td><td>
    <ZL:TextBox runat="server" ID="Price_T" CssClass="form-control text_300" AllowEmpty="true" ValidType="FloatZeroPostive" />
               </td></tr>
<tr><td>预览图片:</td><td><ZL:SFileUp runat="server" ID="PreView_UP" FType="Img" /></td></tr>
<tr id="score_tr" runat="server" visible="false">
    <td>模板评分:</td>
    <td runat="server">
        <input id="score_num" name="score_num" type="number" class="rating" min="0" max="5" step="0.5" data-size="xs">
    </td>
</tr>
<tr><td>模板状态:</td><td>
    <label><input type="radio" value="0" name="zstatus_rad" checked="checked" />正常</label>
    <label><input type="radio" value="1" name="zstatus_rad" />推荐</label>
    <label><input type="radio" value="-1" name="zstatus_rad" />停用</label>
</td></tr>
<tr><td>模板用途</td><td>
   <label><input type="checkbox" value="mbh5_fast" name="defby_chk" />场景快速生成</label>
   <label><input type="checkbox" value="404" name="defby_chk" />404模板</label>
</td></tr>
<tr><td>模板备注:</td><td><asp:TextBox runat="server" ID="Remind_T" TextMode="MultiLine" CssClass="form-control m715-50" style="height:50px;"></asp:TextBox></td></tr>
<tr>
    <td></td>
    <td>
        <asp:LinkButton runat="server" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click">保存</asp:LinkButton>
        <a href="TlpList.aspx?type=<%:ZType %>" class="btn btn-default">取消</a>
        <a runat="server" ID="edit_a" target="_blank" class="btn btn-info" Visible="false"><i class="fa fa-pencil"></i> 修改模板</a>
    </td>
</tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/SelectCheckBox.js"></script>
<script>
    function setscore(score) {
        $("#score_num").val(score);
        $(".rating").rating('refresh', {
            showClear: false,
        });
    }
</script>
</asp:Content>

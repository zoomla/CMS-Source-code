<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTlp.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.AddTlp" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<%@ Register Src="~/Manage/I/ASCX/TreeTlpDP.ascx" TagPrefix="ZL" TagName="TreeTlp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/dist/css/star-rating.min.css" rel="stylesheet" />
    <script src="/dist/js/star-rating.min.js"></script>
    <title>模板管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ul class="nav nav-tabs">
    <li class="active"><a href="#tab0" data-toggle="tab">基本参数</a></li>
    <li><a href="#tab1" data-toggle="tab">页面列表</a></li>
</ul>
<div class="tab-content">
<div class="tab-pane active" id="tab0">
<table class="table table-bordered table-striped">
<tr><td class="td_m">模板名</td><td>  <ZL:TextBox runat="server" ID="TlpName_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="String"></ZL:TextBox>
                             </td></tr>
<tr><td>分类</td><td>
    <ZL:TreeTlp ID="NodeTree" Selected="Node_Hid" NodeID="ID" Pid="Pid" Name="Name" EmpyDataText="请选择分类!" runat="server" />
    <asp:HiddenField ID="Node_Hid" runat="server" />
               </td></tr>
<tr><td>价格</td><td>
    <ZL:TextBox runat="server" ID="Price_T" CssClass="form-control text_300" AllowEmpty="true" ValidType="FloatZeroPostive" />
               </td></tr>
<tr><td>预览图</td><td><ZL:SFileUp runat="server" ID="PreView_UP" FType="Img" /><asp:Image runat="server" ID="PreView_Img" Visible="false" CssClass="tlpimg" /></td></tr>
<tr id="score_tr" runat="server" visible="false">
    <td>评分:</td>
    <td runat="server">
        <input id="score_num" name="score_num" type="number" class="rating" min=0 max=5 step=0.5 data-size="xs">
    </td>
</tr>
<tr><td>状态</td><td>
    <label><input type="radio" value="0" name="zstatus_rad" checked="checked" />正常</label>
    <label><input type="radio" value="1" name="zstatus_rad" />推荐</label>
    <label><input type="radio" value="-1" name="zstatus_rad" />停用</label>
</td></tr>
<tr><td>备注</td><td><asp:TextBox runat="server" ID="Remind_T" TextMode="MultiLine" CssClass="form-control m715-50" style="height:50px;"></asp:TextBox></td></tr>
<tr>
    <td></td>
    <td>
        <asp:LinkButton runat="server" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click">保存</asp:LinkButton>
        <a href="TlpList.aspx" class="btn btn-default">取消</a>
        <asp:LinkButton runat="server" ID="Init_Btn" OnClick="Init_Btn_Click" OnClientClick="return confirm('确定要初始化模板吗,数据将会重置');" Visible="false" >   <i class="fa fa-rotate-left"></i> 初始化</asp:LinkButton>
    </td>
</tr>
</table>
</div>
<div class="tab-pane" id="tab1">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="5" IsHoldState="false"
                OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
                CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="你还没有创建页面">
                <Columns>
                    <asp:BoundField HeaderText="标题" DataField="Title" />
                    <asp:BoundField HeaderText="路径" DataField="Path" />
                    <asp:BoundField HeaderText="创建时间" DataField="CDate" DataFormatString="{0:yyyy年MM月dd日 hh:mm}" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a class="option_style" href="/Design/?ID=<%#Eval("Guid") %>&Source=tlp" target="_blank"><i class="fa fa-paint-brush"></i>设计</a>
                            <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
</div>
</div>
<div class="alert alert-info">
    一套模板应该具备,首页|列表页|内容页|全局组件页四个基本模板构成，如果丢失可以点“重置”按钮恢复。
</div>
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

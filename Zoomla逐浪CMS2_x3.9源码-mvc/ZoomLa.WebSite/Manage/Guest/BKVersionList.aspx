<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BKVersionList.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.BKVersionList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>词条版本</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <div style="margin-bottom:10px;">
        <div class="pull-left" style="line-height:32px;">
            按词条名搜索：
        </div>
        <div class="input-group pull-left" style="width:300px;">
            <asp:TextBox CssClass="form-control" ID="SKey_T" runat="server"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="SearchBtn" CssClass="btn btn-default" runat="server" Text="搜索" OnClick="SearchBtn_Click" />
            </span>
        </div>
    </div><div class="clearfix"></div>
    <div class="borders">
        <ul class="nav nav-tabs">
            <li data-index="-100"><a href="BKVersionList.aspx?Status=-100">所有词条</a></li>
            <li data-index="0"><a href="BKVersionList.aspx?Status=0">待审核</a></li>
            <li data-index="1"><a href="BKVersionList.aspx?Status=1">已通过</a></li>
             <li data-index="-1"><a href="BKVersionList.aspx?Status=-1">已拒绝</a></li>
        </ul>
    </div>
    <div class="panel panel-default" style="padding: 0px;">
        <div class="panel panel-body" style="padding: 0px; margin: 0px;">
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="EGV_RowDataBound" OnPageIndexChanging="EGV_PageIndexChanging" 
                IsHoldState="false" OnRowCommand="EGV_RowCommand" AllowPaging="True" AllowSorting="True"  CssClass="table table-striped table-bordered table-hover"
                EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:BoundField HeaderText="流水号" DataField="Flow" />
                    <asp:BoundField HeaderText="版本号" DataField="VerStr" />
                    <asp:BoundField HeaderText="标题" DataField="Tittle" />
                    <asp:BoundField HeaderText="编辑人" DataField="UserName" />
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%#GetStatus() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="/Guest/Baike/Details.aspx?EditID=<%#Eval("ID") %>" target="_blank" class="option_style"><i class="fa fa-eye" title="预览"></i></a>
                            <a href="/Baike/BKEditor.aspx?EditID=<%#Eval("ID") %>&mode=admin" target="_blank" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                            <asp:LinkButton runat="server" OnClientClick="return confirm('确实要删除吗？');" CommandArgument='<%#Eval("ID") %>' CommandName="Del" CausesValidation="false" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                            <a href="javascript:;" class="option_style" onclick="ShowBKList('<%#Eval("Flow") %>');"><i class="fa fa-list"></i>版本管理</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
        </div>
        <div class="panel panel-footer" style="padding: 3px; margin: 0px;">
            <asp:Button runat="server" ID="BatAudit_Btn" CssClass="btn btn-primary" Text="审核并应用" OnClick="BatAudit_Btn_Click" OnClientClick="return confirm('审核后,将会使用该版本词条');" />
            <asp:Button runat="server" ID="BatUnAudit_Btn" CssClass="btn btn-primary" Text="取消审核" OnClick="BatUnAudit_Btn_Click" />
            <asp:Button runat="server" ID="BatDel_Btn" CssClass="btn btn-info" Text="批量删除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗');" />
            <input type="button" class="btn btn-info" value="批量拒绝" onclick="ShowReject();" />
            <asp:Button runat="server" ID="BatReject_Btn" style="display:none;" Text="批量拒绝" OnClick="BatReject_Btn_Click" />
        </div>
    </div>
<div style="width:400px;display:none;" id="reject_div">
    <div class="panel panel-default">
        <div class="panel-body padding0">
             <textarea class="form-control" id="msg_t" name="msg_t" style="border:none;height:140px;resize:none;" placeholder="请输入拒绝原因,200字以内" maxlength="200"></textarea>
        </div>
        <div class="panel-footer">
            <input type="button" value="确定" class="btn btn-primary" onclick="SubReject();" />
            <input type="button" value="关闭" class="btn btn-default" onclick="diag.CloseModal();" />
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    $(function () {
        var status = "<%:ZStatus%>";
        $(".nav-tabs li[data-index=" + status + "]").addClass("active");
    })
    function ShowBKList(flow) {
        ShowComDiag("BKList.aspx?Flow=" + flow, "版本浏览");
    }
    var diag = new ZL_Dialog();
    function ShowReject() {
        diag.maxbtn = false;
        diag.backdrop = false;
        diag.title = "拒绝原因";
        diag.body = $("#reject_div").html();
        diag.ShowModal();
    }
    function SubReject() {
        $("#BatReject_Btn").click();
    }
</script>
</asp:Content>
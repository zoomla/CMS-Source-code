<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TxtLog.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.TxtLog"MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>日志查看</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="egvdiv" runat="server">
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10"
        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="日志为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="文件名">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Name") %>' CommandName="view"><%#Eval("Name") %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="起始时间" DataField="CreateTime" DataFormatString="{0:yyyy年MM月dd日 HH:mm}" />
            <asp:BoundField HeaderText="终止时间" DataField="LastWriteTime" DataFormatString="{0:yyyy年MM月dd日 HH:mm}" />
            <asp:BoundField HeaderText="日志大小" DataField="ExSize" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Name") %>' CommandName="view" CssClass="option_style"><i class="fa fa-eye" title="查看"></i></asp:LinkButton>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Name") %>' CommandName="del2" OnClientClick="return confirm('确定删除?');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Name") %>' CommandName="down" CssClass="option_style"><i class="fa fa-download" title="下载"></i>下载</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    </div>
    <div id="viewdiv" runat="server" visible="false">
        <div class="top_opbar" style="padding-left:10px;">
            <a href="<%#Request.RawUrl %>" class="btn btn-primary">返回列表</a>
            <asp:Button runat="server" ID="DownLog_Btn" Text="下载日志" CssClass="btn btn-primary" OnClick="DownLog_Btn_Click" />
            <asp:HiddenField runat="server" ID="Curfname_Hid" />
        </div>
        <div class="txtlog_div">
            <asp:Literal runat="server" ID="LogTxt_Li" EnableViewState="false"></asp:Literal>
        </div>
        <%--<asp:TextBox runat="server" ID="LogTxt_T" TextMode="MultiLine" CssClass="form-control" style="height:550px;" placeholder="日志内容为空"></asp:TextBox>--%>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
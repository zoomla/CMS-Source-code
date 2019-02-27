<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Plat.Task.Default"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head"><title>公司名称_任务中心</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<a href="javascript:;" data-toggle="modal" data-target="#myModal" id="modal1" style="display: none;"></a>
<div class="platcontainer container">
    <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">任务管理</span></div>
    <div class="input-group nav_searchDiv margin_t10" style="width:682px;">
        <asp:TextBox runat="server" ID="searchText" class="form-control text_300" placeholder="请输入需要搜索的内容" />
        <asp:DropDownList runat="server" CssClass="form-control text_md">
            <asp:ListItem Value="1" Selected="True">全部任务</asp:ListItem>
            <asp:ListItem Value="2">我负责的任务</asp:ListItem>
            <asp:ListItem Value="3">我参与的任务</asp:ListItem>
        </asp:DropDownList>
        <span class="input-group-btn">
            <asp:LinkButton runat="server" CssClass="btn btn-info" ID="searchBtn" OnClick="searchBtn_Click"><i class="fa fa-search"></i> 搜索</asp:LinkButton>
            <a href="AddTask.aspx" class="btn btn-info" ><i class="fa fa-plus"></i> 创建新任务</a>
        </span>
    </div>
<div class="margin_t10">
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
            class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
            OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <input type="checkbox" value="<%#Eval("ID") %>" name="idChk" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="任务名">
                    <ItemTemplate><a href="AddTask.aspx?ID=<%#Eval("ID") %>" title="查看详情"><%#Eval("TaskName") %></a> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到期日">
                    <ItemTemplate><%#Convert.ToDateTime(Eval("EndTime")).ToString("yyyy年MM月dd日") %> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="负责人">
                    <ItemTemplate><%#GetUserName(Eval("LeaderIDS").ToString()) %> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="任务色彩">
                    <ItemTemplate>
                        <span class="colorSpan_F <%#Eval("Color") %>" id="colorSpan_<%#Eval("ID") %>" onclick="ShowColor(this)"><span class="colorSpan"></span></span>
                        <div class="ex_Color" onclick="HideColor()" data-id="<%#Eval("ID") %>"><span class="colorSpan_F White" data-color="White"><span class="colorSpan"></span></span><span class="colorSpan_F SkyBlue" data-color="SkyBlue"><span class="colorSpan"></span></span><span class="colorSpan_F Purple" data-color="Purple"><span class="colorSpan"></span></span><span class="colorSpan_F Pink" data-color="Pink"><span class="colorSpan"></span></span><span class="colorSpan_F StoneYellow" data-color="StoneYellow"><span class="colorSpan"></span></span><span class="colorSpan_F BrightYellow" data-color="BrightYellow"><span class="colorSpan"></span></span></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="Edit_Link" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>' ToolTip="编辑"><i class="fa fa-pencil"></i>修改</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除"><i class="fa fa-trash-o"></i>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        <asp:Button runat="server" ID="DelBtn" OnClientClick="return confirm('你确定要删除吗!');" OnClick="DelBtn_Click" CssClass="btn btn-info" Text="批量删除" />
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>

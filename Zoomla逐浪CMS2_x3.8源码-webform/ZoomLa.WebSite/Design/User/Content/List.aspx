<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Design_User_Content_List" MasterPageFile="~/Design/Master/User.master" %>
<%@ Register Src="~/Manage/I/ASCX/TreeView.ascx" TagPrefix="ZL" TagName="TreeView" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>内容列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container siteinfo">
    <ol class="breadcrumb">
        <li><a href="/Design/User/?SiteID=<%:SiteID %>">动力模块</a></li>
        <li class="active">内容列表</li>
    </ol>
    <div id="nodeNav" class="col-lg-2 col-md-2 col-sm-2 col-xs-12 divBorder" style="padding:0;height:500px;overflow-y:auto;">
        <div>
            <ZL:TreeView ID="MyTree" IsShrink="true" NodeID="NodeID" runat="server" />
            <span style="color:green;" runat="server" id="remind" visible="false" />
        </div>
    </div>
    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
        <div>
           <div class="pull-left">
                <asp:Label ID="AddContent_L" runat="server"></asp:Label>
           </div>
            <div class="input-group" style="width: 404px;">
                <asp:DropDownList ID="DropDownList1" CssClass="form-control text_150" Style="border-right: none;" runat="server">
                    <asp:ListItem Value="0">按标题查找</asp:ListItem>
                    <asp:ListItem Value="1">按ID查找</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TxtSearchTitle" CssClass="form-control text_md" runat="server"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button ID="Btn_Search" runat="server" class="btn btn-primary" Text="搜索" OnClick="Btn_Search_Click" />
                </span>
            </div>
        </div>
        <div class="margin_t5">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" 
                        CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                        OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="td_xs">
                            <ItemTemplate>
                                <input type="checkbox" name="idchk" value="<%#Eval("GeneralID") %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GeneralID" HeaderText="ID" ItemStyle-CssClass="td_xs">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="标题"> 
                            <ItemTemplate>
                                <a href='<%#Domain+"Content?Gid="+Eval("GeneralID") %>' target="_blank"><%# Eval("Title")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <%# GetStatus(Eval("Status", "{0}")) %>
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="已生成" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# GetCteate(Eval("IsCreate", "{0}"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-CssClass="td_l">
                            <ItemTemplate>
                                <a href='<%#Domain+"Content?Gid="+Eval("GeneralID") %>' target="_blank" title="预览" class="option_style"><i class="fa fa-eye"></i></a>
                                <a href="AddContent.aspx?GeneralID=<%#Eval("GeneralID")+"&SiteID="+SiteID %>" class="option_style" title="修改"><i class="fa fa-pencil"></i></a>
                                <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%# Eval("GeneralID") %>' 
                                    OnClientClick="return confirm('你确定将该数据删除到回收站吗？')"><i class="fa fa-trash"></i>删除</asp:LinkButton>
                                <asp:LinkButton runat="server" Visible='<%#GetIsRe(Eval("Status", "{0}")) %>' CommandName="reset" CommandArgument='<%# Eval("GeneralID") %>'
                                    OnClientClick="return confirm('你确定将该数据还原吗？')">还原</asp:LinkButton>
                            </ItemTemplate> 
                        </asp:TemplateField>
                    </Columns>
            </ZL:ExGridView> 
        </div>
        <div class="margin_t5">
            <asp:Button ID="BatDel_Btn" runat="server" Text="批量删除" OnClick="btnDeleteAll_Click" class="btn btn-primary"
                OnClientClick="return confirm('你确定要将所有选择项删除吗?')" UseSubmitBehavior="true" />
            <asp:Button ID="Rel_Btn" Visible="false" runat="server" Text="批量还原" CssClass="btn btn-primary" OnClick="Rel_Btn_Click" />
        </div>
    </div>
</div> 
    </span> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="/design/res/css/user.css" rel="stylesheet" />
<script src="/JS/SelectCheckBox.js"></script>
<script>
$(function () {
    $(".tvNav a.list1").click(function () { showlist(this); });
    if (window.localStorage.Content_tvNav) {
        expand($("#" + window.localStorage.Content_tvNav));
    }
})
function showlist(obj) {
    $(obj).parent().parent().find("a").removeClass("activeLi");//a-->li-->ul
    $(obj).parent().children("a").addClass("activeLi");//li
    $(obj).parent().siblings("li").find("ul").slideUp();
    $(obj).parent().children("ul").slideToggle();
    window.localStorage.Content_tvNav = obj.id;
}
function expand(obj)//超链接,不以动画效果显示
{
    $a = $(obj).parent().parent(".tvNav ul").parent("li").find("a:first");
    if ($a.length > 0) expand($a);
    $(obj).addClass("activeLi");
    $(obj).parent("li").children("ul").show();
}
</script>
</asp:Content>
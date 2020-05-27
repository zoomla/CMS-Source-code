<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MBSiteList.aspx.cs" Inherits="ZoomLaCMS.Manage.MBSiteList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微建站</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li class="active">微建站</li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block; width: 230px;">
            <div class="input-group" style="position: relative; margin-bottom: -12px;">
                <asp:TextBox ID="Skey_T" placeholder="微站名称或创建人" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<div id="template" runat="server">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="20" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="td_s">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:TemplateField HeaderText="站点信息">
                    <ItemTemplate>
                        <img src="<%#Eval("SiteImg") %>" class="img_50" onerror="this.error=null;this.src='/design/mobile/tlp/<%#Eval("TlpID") %>/view.jpg';" />
                        <%#Eval("SiteName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所属用户">
                    <ItemTemplate>
                        <a href="javascript:;" onclick="showUser('<%#Eval("UserID") %>');"><%#Eval("UserName") %>(<%#Eval("UserID") %>)</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CDate" DataFormatString="{0:yyyy年MM月dd日}" HeaderText="创建时间" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="/design/mobile/default.aspx?id=<%#Eval("ID") %>" target="_blank" title="预览"><i class="fa fa-eye"></i></a>
                        <a class="option_style" href="EditMBSite.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                        <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" CssClass="option_style" OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash"></i>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    function showuser(uid) {
        comdiag.ShowModal("../User/UserInfo.aspx?id=" + uid, "用户信息");
    }
    $("#sel_btn").click(function (e) {
        if ($("#sel_box").css("display") == "none") {
            $(this).addClass("active");
            $("#sel_box").slideDown(300);
            $("#template").css("margin-top", "44px");
        }
        else {
            $(this).removeClass("active");
            $("#sel_box").slideUp(200);
            $("#template").css("margin-top", "0px");
        }
    });
</script>
</asp:Content>

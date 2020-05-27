<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArriveManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.Arrive.ArriveManage"  MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="ZL" TagName="UserGuide" Src="~/Manage/I/ASCX/UserGuide.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>优惠劵管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar">
        <asp:TextBox ID="Name_T" placeholder="优惠劵名：" runat="server" class="form-control text_md"></asp:TextBox>
        <asp:TextBox ID="AgainTime_T" placeholder="有效时间：" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd'});" runat="server" class="form-control text_md"></asp:TextBox>
        <asp:TextBox ID="EndTime_T" placeholder="过期时间：" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd'});" runat="server" class="form-control text_md"></asp:TextBox>
        <asp:Button ID="Skey_Btn" runat="server" Text="搜索" class="btn btn-primary" OnClick="Skey_Btn_Click" />
    </div>
    <div class="clearbox"></div>
    <ul class="nav nav-tabs">
        <li class="active"><a href="#cur" onclick="ShowTabs(-100)">全部</a></li>
        <li><a href="#cur0" onclick="ShowTabs(0)">现金卡</a></li>
        <li><a href="#cur1" onclick="ShowTabs(1)">银币卡</a></li>
    </ul>
     <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"  EmptyDataText="当前无数据"
            OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound" OnPageIndexChanging="EGV_PageIndexChanging" class="table table-striped table-bordered table-hover">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%# Eval("ID") %>"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="Id" runat="server" Text='<%# Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="卡别名" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <%# Eval("ArriveName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server" Text='<%#Eval("ArriveNO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="密码" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblPwd" runat="server" Text='<%#Eval("ArrivePwd") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所属用户" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="生效时间" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblAginTime" runat="server" Text='<%# Eval("AgainTime","{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到期时间" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                       <%#Eval("EndTime","{0:yyyy-MM-dd}") %>
                    </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:TemplateField HeaderText="使用时间" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                      <%#Eval("UseTime","{0:yyyy-MM-dd}") %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                     <%#GetState() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <a href="AddArrive.aspx?id=<%#Eval("id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a>
                        <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗?');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        <asp:Button runat="server" ID="batActive" OnClick="batActive_Click" Text="批量激活" class="btn btn-primary" />
        <asp:Button runat="server" ID="batBtn" OnClick="BtnDelete_Click" Text="批量删除" class="btn btn-primary" OnClientClick="return confirm('确定要删除选中吗');" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script>
    $(function () {
        if (getParam("type")) {
            $("li a[href='#cur" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
        }
    });
    function ShowTabs(Type) {
        location.href = 'ArriveManage.aspx?type=' + Type;
    }
    function change(id) {
        location.href = 'AddArrive.aspx?menu=update&id=' + id;
    }
</script>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpSenderManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ExpSenderManage" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发件信息</title>
    <style>
        .enabled a { color: #999; text-decoration: none; }
        .allchk_l { display: none; }
        #EGV{margin-top:5px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb navbar-fixed-top">
        <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'>工作台</a></li>
        <li><a href='ProductManage.aspx'>商城管理</a></li> 
        <li><a href='DeliverType.aspx'>商城设置</a></li>
        <li class='active'>发件信息<a href='AddExpSender.aspx'>[添加发件信息]</a></li>
        <div id="help" class="pull-right text-center" style="margin-right: 8px;"><a href="javascript:;" onclick="selbox.toggle();" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
        <div id="sel_box" runat="server" class="padding5">
            <span class="pull-left" style="line-height: 40px;">收件人：</span>
            <div class="input-group pull-left" style="width: 300px;">
                <asp:TextBox runat="server" ID="Skey_T" class="form-control" placeholder="请输入需要搜索的内容" />
                <span class="input-group-btn">
                    <asp:LinkButton runat="server" CssClass="btn btn-default" ID="souchok" OnClick="souchok_Click"><span class="fa fa-search"></span></asp:LinkButton>
                </span>
            </div>
        </div>
    </ol>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据！！">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="发件人" />
            <asp:BoundField DataField="CompName" HeaderText="公司名称" />
            <asp:BoundField DataField="Address" HeaderText="发件地址" />
            <asp:BoundField DataField="Mobile" HeaderText="发件人手机" />
            <asp:TemplateField HeaderText="默认">
                <ItemTemplate><%#GetIsDefault() %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CDate" HeaderText="创建时间" DataFormatString="{0:yyyy:MM:dd}" />
            <asp:BoundField DataField="Remind" HeaderText="备注" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddExpSender.aspx?id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil"></i>修改</a>
                    <span class="<%#Eval("IsDefault","")=="1" ? "enabled" : "" %>">
                        <asp:LinkButton runat="server" CommandName="setdef" Enabled='<%#Eval("IsDefault","")!="1" %>' CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-check"></i>设为默认</asp:LinkButton></span>
                    <asp:LinkButton runat="server" CommandName="del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定将该数据删除吗？');" CssClass="option_style"><i class="fa fa-trash-o"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>

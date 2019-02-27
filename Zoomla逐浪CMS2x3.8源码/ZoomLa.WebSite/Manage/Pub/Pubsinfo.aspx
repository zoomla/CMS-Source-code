<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="Pubsinfo.aspx.cs" Inherits="Manage_I_Pub_Pubsinfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>信息列表</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:HiddenField ID="HdnModelID" runat="server" />
    <asp:HiddenField ID="HiddenNode" runat="server" />
    <asp:HiddenField ID="HiddenType" runat="server" />
    <div>
        <ul class="nav nav-tabs">
            <li class="active"><a href="#tab1" data-toggle="tab" onclick="ShowTabs(0,1)">所有信息</a></li>
            <li><a href="#tab3" data-toggle="tab" onclick="ShowTabs(1,3)">待审核信息</a></li>
            <li><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2,2)">已审核信息</a></li>
        </ul>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无互动信息！">
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" title="" value='<%#Eval("ID") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID">
                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="标题">
                    <HeaderStyle Width="25%" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="View" CommandArgument='<%# Eval("ID") %>'><%#GetPubTitle(Eval("PubTitle"))%></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IP地址">
                    <ItemTemplate>
                        <%#Eval("PubIP")+"<br/>("+ZoomLa.BLL.Helper.IPScaner.IPLocation(Eval("PubIP",""))+")" %>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Pubnum" HeaderText="参与人数">
                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="发表日期">
                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%# Eval("PubAddTime", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%# shenhe(Eval("Pubstart", "{0}"))%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="回复">
                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                    <ItemTemplate><%#GetCount(Eval("ID","{0}"))%></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                        <a href="AddPub.aspx?Pubid=<%:PubID%>&Parentid=<%#Eval("ID") %><%= this.HiddenNode.Value %>&type=<%=Request["type"] %>" class="option_style"><i class="fa fa-mail-reply" title="回复"></i>回复</a>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Audit" CommandArgument='<%# Eval("ID") %>' CssClass="option_style"><i class="fa fa-legal" title="审核"></i>审核</asp:LinkButton>
                        <a href="ViewSmallPub.aspx?Pubid=<%:PubID %>&ID=<%# Eval("ID", "{0}")%><%= this.HiddenNode.Value %>&type=0 " class="option_style"><i class="fa fa-magic" title="管理"></i>相关信息与回复管理</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="删除信息" UseSubmitBehavior="False" OnClientClick="if(!confirm('确定要批量删除评论吗？')){return false;}" OnClick="Button1_Click" />
        <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="审核通过" UseSubmitBehavior="False" OnClick="Button2_Click" />
        <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="取消审核" UseSubmitBehavior="False" OnClick="Button3_Click" />
        <asp:Button ID="DownExcel_Btn" CssClass="btn btn-primary" runat="server" Text="下载Excel" UseSubmitBehavior="false" OnClick="DownExcel_Btn_Click" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
           
            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");
            }
        })
        function ShowTabs(ID, type) {
            location.href = 'Pubsinfo.aspx?Pubid=<%=Request.QueryString["Pubid"] %>&ID=' + ID + '&type=' + type;
        }
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=idchk]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>

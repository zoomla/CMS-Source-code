<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="CommentManage.aspx.cs" Inherits="Manage_I_Content_CommentManage" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>评论管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div>
        <ul class="nav nav-tabs">
            <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)"><%=Resources.L.所有评论 %></a></li>
            <li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)"><%=Resources.L.已审核评论 %></a></li>
            <li><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2)"><%=Resources.L.待审核评论 %></a></li>
        </ul>
        <div class="panel panel-default" style="padding:0px;">
        <div class="panel panel-body" style="padding:0px; margin:0px;">
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="CommentID" PageSize="10"
             OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand"
             AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:L,选择 %>">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" title="" value='<%#Eval("CommentID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,评论ID %>">
                    <ItemTemplate>
                        <span class="CommentID"><%#Eval("CommentID") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,所属内容标题 %>">
			        <HeaderStyle Width="20%" />
			        <ItemTemplate>
				        <a href="<%# GetUrl()%>" target="_blank"><%# Eval("Title")%></a>
			        </ItemTemplate>
		        </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,评论内容 %>">
                    <ItemTemplate>
                        <%#GetContent() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,发表日期 %>">
			        <ItemTemplate>
				        <%# Eval("CommentTime", "{0:yyyy-MM-dd}")%>
			        </ItemTemplate>
			        <ItemStyle HorizontalAlign="Center" />
		        </asp:TemplateField>
		        <asp:TemplateField HeaderText="<%$Resources:L,发表人 %>">
			        <ItemTemplate>
                    <a onclick="ShowUserDiag('<%# Eval("UserID") %>')" href="javascript:;">
				        <%# GetUserName(Eval("UserID", "{0}"))%></a>
			        </ItemTemplate>
		        </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,状态 %>">
			        <ItemTemplate>
				        <%#getcommend(Eval("Audited"))%>
			        </ItemTemplate>
		        </asp:TemplateField>
                <asp:BoundField HeaderText="<%$Resources:L,支持 %>" DataField="Agree" />
                <asp:BoundField HeaderText="反对" DataField="DisAgree" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" CommandName="show" CommandArgument='<%# Eval("CommentID") %>' runat="server" CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1" CommandName="Del2" CommandArgument='<%# Eval("CommentID") %>' runat="server" CssClass="option_style"><i class="fa fa-trash" title="删除"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" CommandName="Audit" CommandArgument='<%# Eval("CommentID") %>' runat="server" CssClass="option_style"><i class="fa fa-legal" title="审核"></i>审核</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center"  />
		    <RowStyle HorizontalAlign="Center" />
        </ZL:ExGridView>
            </div>
        <div class="panel panel-footer" style="padding:0px; margin:0px;">
            <asp:Button ID="Button1" runat="server" Text="删除评论" OnClick="BtnSubmit1_Click" CssClass="btn btn-primary" OnClientClick="if(!IsSelectedId()){alert('请选择操作项');return false;}" />
            <asp:Button ID="Button2"   runat="server" Text="审核通过" OnClick="BtnSubmit2_Click" OnClientClick="if(!IsSelectedId()){alert('请选择操作项');return false;}" CssClass="btn btn-primary" />
            <asp:Button ID="Button3"  runat="server" Text="取消审核" OnClick="BtnSubmit3_Click" OnClientClick="if(!IsSelectedId()){alert('请选择操作项');return false;}" CssClass="btn btn-primary" />
            </div>
            </div>
    </div>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $().ready(function () {
            if (getParam("type"))
            {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
            $("#Egv tr").dblclick(function () {
                var id = $(this).find(".CommentID").text();
                if (id) {
                    location = "CommentShow.aspx?id="+id;
                }
            });
        })
        function ShowTabs(n)
        {
            location.href = 'CommentManage.aspx?type=' + n;
        }
        function IsSelectedId()
        {
            var checkArr = $("[name=idchk]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
        var userdiag = new ZL_Dialog();
        function ShowUserDiag(id) {
            userdiag.url = "../User/UserInfo.aspx?id=" + id;
            userdiag.title = "用户详情";
            userdiag.ShowModal();
        }
    </script>
</asp:Content>
    


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WxUserList.aspx.cs" Inherits="Manage_WeiXin_WxUserList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>粉丝管理</title>
<style>
table img{width:30px; height:30px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"> 
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"   EnableTheming="False" IsHoldState="false"
                class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"  OnPageIndexChanging="EGV_PageIndexChanging" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("OpenID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户头像">
                <ItemTemplate>
                    <img class="imgurl" src="<%#Eval("HeadImgUrl") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户名">
                <ItemTemplate>
                    <span class="name"><%#Eval("Name") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="性别">
                <ItemTemplate>
                    <%#GetSexIcon() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户组">
                <ItemTemplate>
                    <span class="groupid"><%#GetUserGroup() %></span> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="更新时间">
                <ItemTemplate>
                    <span class="cdate"><%#Eval("CDate") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="javascript:;" title="更新用户信息" data-oid="<%#Eval("OpenID") %>" class="option_style wxoption"><span class="fa fa-refresh"></span></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <script>
        $().ready(function () {
            $(".wxoption").click(function () {
                UpdateUser($(this).data('oid'));
            });
        });
        function UpdateUser(openid) {
            $(".wxoption[data-oid='" + openid + "'] span").addClass("fa-spin");
            var $tr = $(".wxoption[data-oid='" + openid + "']").closest('tr');
            $.ajax({
                type: 'POST',
                data: { action: 'update', openid: openid,appid:'<%=AppId %>' },
                success: function (data) {
                    if (data == '-1') {
                        $tr.remove();
                        return;
                    }
                    var obj = JSON.parse(data);
                    $tr.find('.imgurl').attr('src', obj.HeadImgUrl);
                    $tr.find('.name').text(obj.Name);
                    $tr.find('.sex').attr('class', obj.Sex == 1 ? 'fa fa-male' : 'fa fa-female');
                    $tr.find('.groupid').text(GetGroupName(obj.Groupid));
                    $(".wxoption[data-oid='" + openid + "'] span").removeClass("fa-spin");
                }
            });
        }
        //用户组名(暂时静态处理)
        function GetGroupName(groupid) {
            switch (groupid) {
                case 0:
                    return "未分组";
                case 1:
                    return "黑名单";
                case 2:
                    return "星标组";
                default:
                    return "";
            }
        }
    </script>
</asp:Content> 


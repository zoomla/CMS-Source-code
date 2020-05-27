<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="ZoomLaCMS.Plat.Left.Users" %>
<asp:Repeater runat="server" ID="RPT" OnItemDataBound="RPT_ItemDataBound">
        <ItemTemplate>
        <div>
            <div data-id="<%#Eval("ID") %>" class="gp_info" onclick="leftnav.gp.toggle(this);">
                <i class="fa fa-chevron-right r_gray gp_btn"></i><span style="margin-left:8px;"><%#Eval("GroupName") %></span>
                <span class="r_gray pull-right" runat="server" id="usercount_sp"></span>
            </div>
            <ul class="gp_child listul">
                    <asp:Repeater runat="server" ID="User_RPT">
                        <ItemTemplate>
                            <li onclick="leftnav.showuinfo('<%#Eval("UserID") %>');" title="查看消息">
                                <img class="icon" src="<%#Eval("UserFace") %>" onerror="shownoface(this);" />
                                <span class="icon_txt"><%#Eval("HoneyName") %></span>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
        </div>
        </ItemTemplate>
</asp:Repeater>
<div class="margin_t10">
    <span runat="server" id="Span1"></span><span class="r_gray">个联系人</span>
</div>
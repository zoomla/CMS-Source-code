<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WxTree.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.WxTree" %>
<div class="tvNavDiv">
    <div class="left_ul">
        <ul class="m_left_ulin" id="menu7_9">
            <li class="bg-primary"><span class="fa fa-chevron-down"></span>微信管理</li>
            <li>
                <p class="menu_tit laybtn"><i class="fa fa-arrow-circle-down"></i>基本配置</p>
                <ul id="menu7_9_ul1" class="menu_tit_ch active">
                    <li id="menu7_9_1"><a href="javascript:;" onclick="ShowMain('','{$path}WeiXin/WxAppManage.aspx');">公众号管理</a></li>
                </ul>
            </li>
            <li>
                <p class="menu_tit laybtn"><i class="fa fa-plus-circle"></i>账户中心</p>
                <ul id="menu7_9_ul2" class="menu_tit_ch">
                    <asp:Repeater runat="server" EnableViewState="false" ID="RPT">
                        <ItemTemplate>
                            <li data-id="<%#Eval("ID") %>"><a href="javascript:;" onclick="ShowMain('','{$path}WeiXin/Home.aspx?appid=<%#Eval("ID") %>');"><%#Eval("Alias") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </li>
        </ul>
    </div>
</div>

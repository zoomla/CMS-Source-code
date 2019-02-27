<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VirtualDirectory.aspx.cs" Inherits="Manage_Site_VirtualDirectory" MasterPageFile="~/Manage/Site/OptionMaster.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>虚拟目录</title>
<style type="text/css">	#addUL li{padding-top:10px;	}</style>
</asp:Content>
<asp:Content  runat="server" ContentPlaceHolderID="pageContent">
    <div id="m_site">
        <p style="float: left;">站群中心 >> 虚拟目录</p></div>
    <div id="site_main">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div id="tab3">
            <ZL:ExGridView runat="server" ID="vdEGV" AutoGenerateColumns="false" OnRowCommand="vdEGV_RowCommand" OnPageIndexChanging="vdEGV_PageIndexChanging"
                 AllowPaging="True" RowStyle-CssClass="tdbg"
                MouseOverCssClass="tdbgmouseover" CellPadding="2" CellSpacing="1" ForeColor="Black" CssClass="border" Width="100%"
                GridLines="None" EnableTheming="False" EmptyDataText="没有任何数据！">
                <Columns>
                    <asp:TemplateField HeaderText="序列号">
                        <ItemTemplate>
                            <%#Eval("Index") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="虚拟路径">
                        <ItemTemplate>
                            <%#Eval("VPath") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物理路径">
                        <ItemTemplate>
                            <a href="javascript:openWin('<%# "SiteFileManage.aspx?siteName="+Server.UrlEncode(Eval("SiteName") as string)+"&index="+ 
                                     (Convert.ToInt32(Eval("Index"))-1) %>');void(0);" title="点击浏览目录"><%#Eval("PPath") %></a>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="EditPhySicalPath" Text='<%#Eval("PPath") %>' Width="90%" Style="text-align: center;" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="site_a">
                        <ItemTemplate>
                            <a href="<%#"SiteFileManage.aspx?siteName="+Server.UrlEncode(Eval("SiteName") as string)+"&index="+ 
                                     (Convert.ToInt32(Eval("Index"))-1) %>"title="点击浏览目录">浏览</a>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit2" CommandArgument='<%# Container.DisplayIndex %>'>修改</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del2" CommandArgument='<%#Eval("Index")%>'>删除</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Save" CommandArgument='<%# Container.DisplayIndex+":"+Eval("SiteName")+":"+Eval("Index") %>'>
                                        更新</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel2" CommandArgument='<%# Container.DisplayIndex %>'>取消</asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
                    <ul id="addUL" style="margin-top:20px;width:350px;">
                        <li>虚拟路径：<asp:TextBox runat="server" ID="vdVPath" class="site_input"/><span style="color: red">*例:/文件名</span></li>
                        <li>物理路径：<asp:TextBox runat="server" ID="vdPPath" class="site_input"/><span style="color: red">*例:C:\test\</span></li>
                        <li style="text-align:center;">
                            <asp:Button runat="server" ID="vdBtn" Text="添加" OnClick="vdBtn_Click" class="site_button" />
                            <input type="button" value="返回" onclick="parent.disOptionDiv();" class="site_button" />
                        </li>
                    </ul>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            function openWin(url)
            {
                window.open(url);
            }
        </script>
    </div>
    </asp:Content>
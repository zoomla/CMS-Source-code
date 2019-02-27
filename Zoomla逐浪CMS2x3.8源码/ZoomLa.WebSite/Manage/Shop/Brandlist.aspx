<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Brandlist.aspx.cs" Inherits="manage_Shop_Brandlist" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择品牌</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <div>
            <table  class="table table-striped table-bordered table-hover">
                <tr class="title" style="height: 22">
                    <td align="left">
                        <b>已经选定的品牌：</b></td>
                    <td align="right">
                        <a href="javascript:window.close();">返回&gt;&gt;</a></td>
                </tr>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td align="left">
                        <input type="text" id="UserNameList" class="l_input" size="60" maxlength="200" readonly="readonly"
                            class="inputtext" />
                        <input type="hidden" name="HdnUserName" id="HdnUserName" value="" runat="server" /></td>
                    <td align="center">
                        </td>
                </tr>
            </table>
            <br />                      
            <table class="table table-striped table-bordered table-hover">
                <tr class="title">
                    <td align="left">
                        <b>品牌列表：</b></td>
                    <td align="right">
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td valign="top" colspan="2">
                        <div id="DivUserName" runat="server" visible="false">未找到任何品牌！</div>
                        <asp:Repeater ID="RepUser" runat="server" OnItemDataBound="RepUser_ItemDataBound">
                            <HeaderTemplate>
                                <table width="100%" border="0" cellspacing="1" cellpadding="1" class="border">
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td align="center">
                                    <a href="#" onclick="<%# "add('" + DataBinder.Eval(Container,"DataItem.Trname","{0}") + "')"%>">
                                        <%# Eval("Trname")%>
                                    </a>
                                </td>
                                <% 
                                    i++; %>
                                <% if (i % 8 == 0 && i > 1)
                                   {%>
                                </tr><tr>
                                    <%} %>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tr></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <div id="pager1" runat="server">
            </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function add(obj) {
            if (obj == "") { return false; }
            if (opener.document.getElementById('Brand').value == "") {
                opener.document.getElementById('Brand').value = obj;
                document.getElementById('UserNameList').value = opener.document.getElementById('Brand').value;
                return false;
            }
            var singleUserName = obj.split(",");
            var ignoreUserName = "";
            for (i = 0; i < singleUserName.length; i++) {
                opener.document.getElementById('Brand').value = singleUserName[i];
                document.getElementById('UserNameList').value = opener.document.getElementById('Brand').value;
            }
        }
    </script>
</asp:Content>
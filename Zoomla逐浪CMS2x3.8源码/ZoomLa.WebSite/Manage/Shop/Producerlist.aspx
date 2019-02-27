<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Producerlist.aspx.cs" Inherits="manage_Shop_Producerlist" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>选择厂商</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
                <tr class="title" style="height: 22">
                    <td align="left">
                        <b>已经选定的厂商：</b></td>
                    <td align="right">
                        <a href="javascript:window.close();">返回&gt;&gt;</a></td>
                </tr>
                <tr>
                    <td align="left">
                        <input type="text" id="UserNameList" class="form-control" size="60" maxlength="200" readonly="readonly"/>
                        <input type="hidden" name="HdnUserName" id="HdnUserName" /></td>
                    <td align="center">
                        </td>
                </tr>
            </table>                  
            <table width="100%" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td align="left">
                        <b>厂商列表：</b></td>
                    <td align="right">
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td valign="top" colspan="2">
                        <div id="DivUserName" runat="server" visible="false">
                            未找到任何厂商！</div>
                        <asp:Repeater ID="RepUser" runat="server" OnItemDataBound="RepUser_ItemDataBound">
                            <HeaderTemplate>
                                <table width="100%" border="0" cellspacing="1" cellpadding="1" class="border">
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td align="center">
                                    <a href="#" onclick="<%# "add('" + DataBinder.Eval(Container,"DataItem.Producername","{0}") + "')"%>">
                                        <%# Eval("Producername")%>
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
            <table border="0" align="center" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        
                    </td>
                </tr>
            </table>
            <div id="pager1" runat="server"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function add(obj) {
            if (obj == "") { return false; }
            if (opener.document.getElementById('Producer').value == "") {
                opener.document.getElementById('Producer').value = obj;
                document.getElementById('UserNameList').value = opener.document.getElementById('Producer').value;
                return false;
            }
            var singleUserName = obj.split(",");
            var ignoreUserName = "";
            for (i = 0; i < singleUserName.length; i++) {
                opener.document.getElementById('Producer').value = singleUserName[i];
                document.getElementById('UserNameList').value = opener.document.getElementById('Producer').value;
            }
        }
        </script>
</asp:Content>
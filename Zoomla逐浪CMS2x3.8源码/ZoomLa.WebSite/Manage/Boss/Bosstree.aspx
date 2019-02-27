<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bosstree.aspx.cs" Inherits="manage_Boss_Bosstree" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
   <title>加盟商管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody1">
            <tr class="tdbg">
                <td>
                </td>
                <td >
                    代理商名称
                </td>
                <td>
                  代理商用户名
                </td>
                <td>
                    合同协议号
                </td>
                
                <td>
                    代理商费用
                </td>
                <td>
                   法定代理人 
                </td>
                <td>
                 操作 
                </td>
            </tr>
            <ZL:ExRepeater ID="RPT" runat="server" PageSize="20" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='6'><div class='text-center'>" PageEnd="</div></td></tr>">
                <ItemTemplate>
                    <tr>
                        <td>
                      <input type="checkbox" name="idchk" value="<%#Eval("nodeid") %>" />
                        </td>
                        <td>
                          <a href="BossInfo.aspx?nodeid=<%#Eval("nodeid") %>&parentid=<%#Eval("parentid") %>">  <%#Eval("CName") %> </a>
                        </td>
                        <td>
                       
                          <a href="/manage/User/UserInfo.aspx?id=<%#Eval("UserID") %>"> <%#UserNames(DataBinder.Eval(Container, "DataItem.UserID", "{0}"))%>  </a>  
                        </td>
                        <td>
                              <%#Eval("ContractNum") %> 
                        </td>
                         <td> 
                         <%#formatcs(DataBinder.Eval(Container, "DataItem.CMoney", "{0}"))%>   
                        </td>
                        <td>
                            <%#Eval("Agent") %> 
                        </td>
                        <td>
                         <a href="AddBoss.aspx?bid=<%#Eval("nodeid") %> ">修改</a>
                            <a href="BossInfo.aspx?nodeid=<%#Eval("nodeid") %>&parentid=<%#Eval("parentid") %>">查看</a>
                          <a href="Bosstree.aspx?nodeid=<%#Eval("nodeid") %>&types=del">删除</a>
                    </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
         </tbody>
    </table>
    <asp:Button ID="Button2" Text="删除选中加盟商" class="btn btn-primary" runat="server"
                        OnClick="Button2_Click" />
     <div id="Guide_back" style=" display:none">
        <ul>
            <li id="Guide_top">
                <div id="Guide_toptext">招商管理</div>
            </li>
            <li id="Guide_main">
                <div style="height: 25px; text-indent: 25px; line-height: 25px;">
                    <a href="Bosstree.aspx">显示所有</a>&nbsp;&nbsp;招商名：
                    <table><tr><td> 
                    <asp:TextBox ID="TextBox1" runat="server" BorderColor="#fff" CssClass="l_inpnon"></asp:TextBox></td><td>
                    <asp:Button ID="Button1" CssClass="C_sch" runat="server" Text="" OnClick="Button1_Click" />
                        </td></tr></table>
                </div>
                <div id="Guide_box" style="height: 600px">
                    <asp:TreeView ID="tvNav" runat="server" ExpandDepth="1" ShowLines="True" EnableViewState="False" NodeIndent="10">
                        <NodeStyle BorderStyle="None" ImageUrl="~/Images/TreeLineImages/plus.gif" />
                        <ParentNodeStyle ImageUrl="~/Images/TreeLineImages/plus.gif" />
                        <SelectedNodeStyle ImageUrl="~/Images/TreeLineImages/dashminus.gif" />
                        <RootNodeStyle ImageUrl="~/Images/TreeLineImages/dashminus.gif" />
                    </asp:TreeView>
                </div>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
      <%--  <script type="text/javascript" src="/JS/menu.js"></script>--%>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        function Switch(obj) {
            obj.className = (obj.className == "guideexpand") ? "guidecollapse" : "guideexpand";
            var nextDiv;
            if (obj.nextSibling) {
                if (obj.nextSibling.nodeName == "DIV") {
                    nextDiv = obj.nextSibling;
                }
                else {
                    if (obj.nextSibling.nextSibling) {
                        if (obj.nextSibling.nextSibling.nodeName == "DIV") {
                            nextDiv = obj.nextSibling.nextSibling;
                        }
                    }
                }
                if (nextDiv) {
                    nextDiv.style.display = (nextDiv.style.display != "") ? "" : "none";
                }
            }
        }
        function OpenLink(lefturl, righturl) {
            if (lefturl != "") {
                parent.frames["left"].location = lefturl;
            }
            try {
                parent.MDIOpen(righturl); return false;
            } catch (Error) {
                parent.frames["main_right"].location = righturl;
            }
        }
        function gotourl(url) {
            try {
                parent.MDILoadurl(url); void (0);
            } catch (Error) {
                parent.frames["main_right"].location = "../" + url; void (0);
            }
        }
        function LinkClick(str) {
            if (confirm('确实要删除此加盟商吗？')) {
                //location.href = 'Bosstree.aspx?type=del&id=' + str;
                gotourl("Boss/Bosstree.aspx?type=del&id=" + str);
            }
        }
        $().ready(function () {
            $("#chkAll").click(function () {
                var _all=$(this)[0];
                $("input[name='idchk']").each(function () {
                    $(this)[0].checked = _all.checked;
                });
            });
        });
    </script>
</asp:Content>
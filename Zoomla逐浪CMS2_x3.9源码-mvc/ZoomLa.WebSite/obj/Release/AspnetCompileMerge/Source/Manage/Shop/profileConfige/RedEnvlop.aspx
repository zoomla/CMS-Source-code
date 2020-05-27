<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedEnvlop.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.profileConfige.RedEnvlop" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>红包配置信息</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
    <tr>
        <td class="spacingtitle" align="center"> <asp:Literal ID="LTitle" runat="server" Text="修改红包信息配置"></asp:Literal></td>
    </tr>
    <tr class="tdbg">
        <td valign="top" style="margin-top: 0px; margin-left: 0px;">
         <table width="100%" cellpadding="2" cellspacing="1" style="background-color: white;">
             <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" width="25%"><strong>奖励规则(修改无效)：</strong></td>
                <td style="padding-bottom:10px;">
                 <div id="add" style="display:none;">
                   兑现金额：<asp:TextBox ID="txtHonorMoney" runat="server"  class="form-control" style="margin:3px 0"></asp:TextBox>
                   是否包含兑现金额:<asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem></asp:DropDownList>
                   奖金：<asp:TextBox ID="txtMoney" runat="server"  class="form-control" style="margin:3px 0"></asp:TextBox>
                   <asp:Button ID="btnAdd" runat="server" class="btn btn-primary" style="margin-top:5px;" Text="添加"   onclick="btnAdd_Click" />
                   <input type="button" class="btn btn-primary" style="margin-top:5px;" value="取消" onclick="show('view')" />
                  </div>
                   <div id="updata" style="display:none;">
                     奖励编号:<asp:DropDownList ID="ids" runat="server"></asp:DropDownList>
                     兑现金额：<asp:TextBox ID="TextBox1" runat="server"  class="form-control"></asp:TextBox>
                     是否包含兑现金额:<asp:DropDownList ID="ddCont" runat="server">
                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                        <asp:ListItem Value="1">不包含</asp:ListItem></asp:DropDownList>
                     奖金：<asp:TextBox ID="txtBonus" runat="server"  class="form-control"></asp:TextBox>
                     <asp:Button ID="btnUpdata" runat="server"  class="btn btn-primary" Text="修改" 
                           onclick="btnUpdata_Click"/>
                     <input type="button" class="btn btn-primary" value="取消" onclick="show('view')" />
                   </div>
                   <div id="del" style="display:none;">奖励编号:<asp:DropDownList ID="ddid" runat="server"></asp:DropDownList>
                   <asp:Button ID="btnDel" runat="server"  class="btn btn-primary" Text="删除" 
                           onclick="btnDel_Click"/>
                   <input type="button" class="btn btn-primary" value="取消" onclick="show('view')" /></div>
                   <div id="view" runat="server" >
                   <textarea  id="txtHonorinfo" class="form-control" name="infoeditor" rows="10" style="width:100%;max-width:580px;" runat="server" disabled="disabled"></textarea><br />
                   <input type="button" value="添加规则" class="btn btn-primary" onclick="show('add')" />&nbsp;<input type="button" value="修改规则" class="btn btn-primary" onclick="    show('updata')" />&nbsp;<input type="button" class="btn btn-primary" value="删除规则" onclick="    show('del')" />
                   </div>
                    <%=Call.GetUEditor("txtHonorinfo",4) %></td>
            </tr>
             <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td class="tdbgleft"><strong>奖励规则(页面效果,修改有效)：</strong></td>
                    <td style="padding-bottom:10px;">
                    <textarea  id="txtHonor" class="form-control" name="infoeditor" rows="10" style="width:100%;max-width:580px;" runat="server"></textarea>
                   </td>
                    <%=Call.GetUEditor("txtHonor",4) %>
              </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
               <td class="tdbgleft"><strong>注意事项：</strong></td>
               <td>
                <textarea  id="info" class="form-control" name="infoeditor" rows="10" style="width:100%;max-width:580px;" runat="server"></textarea>
                    <%=Call.GetUEditor("info",4) %>
                </td>
             </tr>
         </table>
        </td>
    </tr>
</table>
<table id="TABLE1">
    <tr>
        <td align="left" style="height: 59px">
            &nbsp; &nbsp;
            <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="修改" runat="server"  onclick="EBtnSubmit_Click" />
            <input type="button" class="btn btn-primary" name="Button2" value="返回列表" onclick="location.href = '../profile/RedEnvlopManage.aspx'" id="Button2" />
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function show(obj) {
            if (obj == "add") {
                document.getElementById("add").style.display = "";
                document.getElementById("updata").style.display = "none";
                document.getElementById("del").style.display = "none";
                document.getElementById("view").style.display = "none";
                document.getElementById("txtHonorMoney").value = "";
                document.getElementById("txtMoney").value = "";
            }
            if (obj == "updata") {
                document.getElementById("add").style.display = "none";
                document.getElementById("updata").style.display = "";
                document.getElementById("del").style.display = "none";
                document.getElementById("view").style.display = "none";
                document.getElementById("txtBonus").value = "";
                document.getElementById("TextBox1").value = "";
            }
            if (obj == "del") {
                document.getElementById("add").style.display = "none";
                document.getElementById("updata").style.display = "none";
                document.getElementById("del").style.display = "";
                document.getElementById("view").style.display = "none";
            }
            if (obj == "view") {
                document.getElementById("add").style.display = "none";
                document.getElementById("updata").style.display = "none";
                document.getElementById("del").style.display = "none";
                document.getElementById("view").style.display = "";
            }
        }
    </script>
</asp:Content>


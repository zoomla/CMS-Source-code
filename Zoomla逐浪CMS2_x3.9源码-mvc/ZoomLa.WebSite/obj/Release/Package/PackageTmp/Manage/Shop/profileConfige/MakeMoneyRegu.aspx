<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MakeMoneyRegu.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.profileConfige.MakeMoneyRegu"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>赚钱计划规则</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">   
<table class="table table-striped table-bordered table-hover">
    <tr>
        <td class="spacingtitle" align="center"> <asp:Literal ID="LTitle" runat="server" Text="修改赚钱计划配置"></asp:Literal></td>
    </tr>
    <tr class="tdbg">
        <td valign="top" style="margin-top: 0px; margin-left: 0px;">
         <table class="table table-striped table-bordered table-hover"	>
             <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" width="25%"><strong>赚钱计划规则(修改无效)：</strong></td>
                <td style="padding-bottom:10px;">
                 <div id="add" style="display:none;padding-bottom:5px; padding-top:5px">
                   <asp:HiddenField ID="hfmenu" runat="server" />
                   <div id="div1">规则编号:<asp:DropDownList ID="ddIds" runat="server" onchange="change()"></asp:DropDownList></div>
                    <div id="div2">会员数量最低值：<asp:TextBox ID="txtMinNum" runat="server"  class="form-control"></asp:TextBox>
                    <div id="div3">会员数量最高值：<asp:TextBox ID="txtMaxNum" runat="server"  class="form-control"></asp:TextBox></div>
                   推荐会员的奖励：<asp:TextBox ID="txtAward" runat="server"  class="form-control"></asp:TextBox></div>
                   <asp:Button ID="btn" runat="server" class="btn btn-primary" style="margin-top:5px;"  Text="添加" 
                         onclick="btn_Click"/>
                   <input type="button" class="btn btn-primary" style="margin-top:5px;" value="取消" onclick="show('view')" />
                  </div>
                   <div id="view" runat="server" >
                   <textarea  id="txtHonorinfo" class="form-control"  name="infoeditor" rows="10" runat="server" disabled="disabled"  style="margin-bottom:10px; max-width:580px;width:100%;"></textarea><br />
                   <input type="button" value="添加规则" class="btn btn-primary"  onclick="show('add')" />&nbsp;<input type="button" value="修改规则" class="btn btn-primary"  onclick="    show('updata')" />&nbsp;<input type="button" class="btn btn-primary"  value="删除规则" onclick="    show('del')" />
                   </div>
                    <%=Call.GetUEditor("MsgContent_T",4) %> 
                </td>
            </tr>
              <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft"><strong>推荐会员购物后,会员所得奖励：</strong></td>
                <td style="padding-bottom:10px;">
                <asp:TextBox ID="txtShopMoney" runat="server" class="form-control"></asp:TextBox>
               </td>
              </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
               <td class="tdbgleft"><strong>注意事项：</strong></td>
               <td>
                <textarea  id="info" class="form-control" style="max-width:580px;width:100%;" rows="10" runat="server"></textarea>
                    <%=Call.GetUEditor("info",4) %> 
                </td>
             </tr>
         </table>
        </td>
    </tr>
</table>
<table id="TABLE1">
    <tr>
        <td align="left"><asp:Button ID="EBtnSubmit" class="btn btn-primary"  Text="修改" runat="server"  onclick="EBtnSubmit_Click" /></td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    function show(obj) {
        document.getElementById("txtMinNum").value = "";
        document.getElementById("txtMaxNum").value = "";
        document.getElementById("txtAward").value = "";
        document.getElementById("hfmenu").value = obj;

        if (obj == "add") {
            document.getElementById("add").style.display = "";
            document.getElementById("view").style.display = "none";
            document.getElementById("div1").style.display = "none";
            document.getElementById("div2").style.display = "";
            document.getElementById("btn").value = "添加";
        }
        if (obj == "updata") {
            document.getElementById("add").style.display = "";
            document.getElementById("view").style.display = "none";
            document.getElementById("div1").style.display = "";
            document.getElementById("div2").style.display = "";
            document.getElementById("btn").value = "修改";

        }
        if (obj == "del") {
            document.getElementById("add").style.display = "";
            document.getElementById("view").style.display = "none";
            document.getElementById("div1").style.display = "";
            document.getElementById("div2").style.display = "none";
            document.getElementById("btn").value = "删除";
        }
        if (obj == "view") {
            document.getElementById("add").style.display = "none";
            document.getElementById("view").style.display = "";
        }
    }

    function change() {
        if (document.getElementById("hfmenu").value == "updata" && document.getElementById("ddIds").value == "-1") {
            document.getElementById("add").style.display = "";
            document.getElementById("view").style.display = "none";
            document.getElementById("div1").style.display = "";
            document.getElementById("div2").style.display = "";
            document.getElementById("div3").style.display = "none";
            document.getElementById("btn").value = "修改";
        }
        if (document.getElementById("hfmenu").value == "updata" && document.getElementById("ddIds").value != "-1") {
            document.getElementById("add").style.display = "";
            document.getElementById("view").style.display = "none";
            document.getElementById("div1").style.display = "";
            document.getElementById("div2").style.display = "";
            document.getElementById("div3").style.display = "";
            document.getElementById("btn").value = "修改";
        }
        if (document.getElementById("hfmenu").value == "del") {
            document.getElementById("add").style.display = "";
            document.getElementById("view").style.display = "none";
            document.getElementById("div1").style.display = "";
            document.getElementById("div2").style.display = "none";
            document.getElementById("btn").value = "删除";
        }
    }
</script>
</asp:Content>



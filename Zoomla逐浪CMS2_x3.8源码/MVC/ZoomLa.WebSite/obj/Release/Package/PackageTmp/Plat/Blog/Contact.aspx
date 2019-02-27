<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.Contact" MasterPageFile="~/Plat/Main.master" %>
<asp:content runat="server" contentplaceholderid="head"><title>通讯录</title></asp:content>
<asp:content runat="server" contentplaceholderid="Content">
<div class="container platcontainer">
    <div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">通讯录</span></div>
    <div class="input-group" style="width: 400px;">
        <input runat="server" id="skey_t" class="form-control" placeholder="请输入搜索内容" type="text" />
        <span class="input-group-btn">
            <asp:Button runat="server" ID="Skey_Btn" Text="搜索" OnClick="Skey_Btn_Click" CssClass="btn btn-default" />
        </span>
    </div>
  <div class="margin_t5">
    <Zl:ExGridview ID="EGV" AutoGenerateColumns="false" AllowPaging="true" EnableTheming="false" Width="98%" CssClass="table table-bordered table-hover" EmptyDataText="当前没有信息！" OnPageIndexChanging="EGV_PageIndexChanging"  OnRowCommand="EGV_RowCommand" PageSize="10" runat="server">
      <columns>
      <asp:templatefield HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
        <itemtemplate>
          <input type="checkbox" name="idChk" value="<%#Eval("UserID") %>" />
        </itemtemplate>
      </asp:templatefield>
      <asp:BoundField HeaderText="用户名" DataField="UserName" />
      <asp:templatefield headertext="真实名称">
        <itemtemplate> <%# GetGN(Eval("TrueName")) %> </itemtemplate>
      </asp:templatefield>
      <asp:BoundField HeaderText="手机号码" DataField="Mobile" />
      <asp:BoundField HeaderText="所属部门" DataField="GroupName" />
      <asp:TemplateField HeaderText="二维码">
        <ItemTemplate> <span class="fa fa-qrcode" style="font-size:1.3em;color:gray;" title="获取二维码" onclick="GetQrCode(<%#Eval("UserID") %>);"></span>
          <div style="position:absolute;display:none;z-index:5;margin-left:-80px;" id="imgdiv_<%#Eval("UserID") %>" name="imgdiv"><img id="img_<%#Eval("UserID") %>" src="#" style="width:120px;height:120px;" /></div>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:templatefield Headertext="操作">
        <itemtemplate> <a href="/Plat/Mail/MessageSend.aspx?uid=<%#Eval("UserID") %>" title="发送邮件">邮件</a> 
          <!--<a href="#" title="发送短信">短信</a>--> 
        </itemtemplate>
      </asp:templatefield>
      </columns>
    </Zl:ExGridview>
    <div class="clearfix"></div>
    <table  class="TableWrap"  border="0" cellpadding="0" cellspacing="0" width="100%" id="sleall">
      <tr>
        <td style="height: 21px"><asp:Button ID="BtnSetTopPosation" runat="server" CssClass="btn btn-info" OnClick="BtnSetTopPosation_Click" Text="批量发邮件"  />
          <asp:HiddenField ID="HdnCateID" runat="server" /></td>
      </tr>
    </table>
  </div>
</div>
</asp:content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/JQueryAjax.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script>
    //生成图片,返回图片链接,显示链接,
    function GetQrCode(uid)
    {
        $("div[name=imgdiv]").hide();
        PostToCS2("/User/Left", "GetQrCode", uid, function (data) { $("#img_" + uid).attr("src", data); $("#imgdiv_" + uid).show(); console.log(data); });
    }
</script>
</asp:Content>
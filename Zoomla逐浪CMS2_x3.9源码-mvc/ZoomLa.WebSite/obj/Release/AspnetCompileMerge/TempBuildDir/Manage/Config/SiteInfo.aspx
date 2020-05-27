<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteInfo.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Config.SiteInfo" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>网站信息配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdbgleft td_l"><strong><%:lang.LF("网站名称") %>：</strong></td>
            <td><asp:TextBox ID="SiteName_T" runat="server"  class=" form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("网站标题") %>：</strong></td>
            <td><asp:TextBox ID="SiteTitle_T" runat="server"  class="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
          <td class="tdbgleft"><strong><%:lang.LF("网站地址") %>：</strong></td>
          <td><asp:TextBox ID="SiteUrl_T" runat="server"  class="form-control text_300" ></asp:TextBox>
             <span Style="color:#1e860b;" >当前网址：<asp:Literal ID="siteurl" runat="server"></asp:Literal></span>
              <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="SiteUrl_T" ErrorMessage="网址不能为空"  Style="float:left;" Display="Dynamic" />
              <asp:RegularExpressionValidator ID="RF2" ValidationExpression="^[a-zA-z]+://[^s]*[^d]*$" runat="server" ErrorMessage="网址格式错误，需加'http://'头" ControlToValidate="SiteUrl_T"  Display="Dynamic" />
            </td>
        </tr>
           <tr>
          <td class="tdbgleft"><strong><%:lang.LF("后台路径") %>：</strong></td>
          <td><asp:TextBox ID="CustomPath" runat="server"  MaxLength="10" class="form-control text_300" onkeyup="value=value.replace(/[^\w\/]/ig,'')"  />
              <span style="color:#1e860b;"> 3-10英文数字组合</span>
        <asp:RequiredFieldValidator ID="CustomPath_R2" SetFocusOnError="true" runat="server" ControlToValidate="CustomPath" Display="Dynamic" ErrorMessage="后台路径不能为空,最小三位!" ForeColor="Red"></asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator ID="CustomPath_R1" ControlToValidate="CustomPath"
			ValidationExpression="^[a-zA-Z0-9_\u4e00-\u9fa5\@\.]{3,10}$" SetFocusOnError="true" runat="server" Display="Dynamic" ErrorMessage="后台路径不能少于三位" ForeColor="Red"></asp:RegularExpressionValidator>
          </td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("LOGO地址") %>：</strong></td>
            <td><asp:TextBox ID="LogoUrl_T" runat="server"  class="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong>后台标志：</strong></td>
            <td>
                <asp:TextBox ID="LogoAdmin_T" placeholder="初始值" runat="server"  class="form-control text_300"></asp:TextBox>
                <span style="color:green;">*选择图标(如选用图片则规格是54px*48px大小)</span>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong>后台名称：</strong></td>
            <td>
                <asp:TextBox ID="LogoPlatName_T" runat="server" placeholder="初始值" class="form-control text_300"></asp:TextBox>
                <input type="button" class="btn btn-default" value="推荐格式" onclick="setPlatName();" />
            </td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong>官方二维码：</strong></td>
            <td><asp:TextBox ID="QRCode_T" runat="server"  class="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
          <td class="tdbgleft"><strong><%:lang.LF("公司名称") %>：</strong></td>
          <td>
          <asp:TextBox ID="ComName_T"  runat="server" CssClass="form-control text_300"></asp:TextBox>
           </td>
          </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("站长姓名") %>：</strong></td>
            <td><asp:TextBox ID="Webmaster_T" runat="server" class="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("站长手机") %>：</strong></td>
            <td><asp:TextBox ID="MasterPhone_T" runat="server" class="form-control text_300" />
                 <asp:RegularExpressionValidator ID="REV1" Display="Dynamic" ControlToValidate="MasterPhone_T" ForeColor="Red" ValidationExpression="^1\d{10}$" runat="server" ErrorMessage="手机号码格式不正确"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("站长信箱") %>：</strong></td>
            <td><asp:TextBox ID="WebmasterEmail_T" runat="server" class="form-control text_300" />
                 <asp:RegularExpressionValidator ID="Email_Reg"  runat="server" ErrorMessage="邮件格式不正确" SetFocusOnError="true" ForeColor="Red" ControlToValidate="WebmasterEmail_T"  Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("网站META关键词") %>：</strong></td>
            <td><asp:TextBox ID="MetaKeywords_T" runat="server" TextMode="MultiLine" class="form-control m715-50" style="height:60px;"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("网站META网页描述") %>：</strong></td>
            <td><asp:TextBox ID="MetaDescription_T" runat="server" TextMode="MultiLine" class="form-control m715-50" style="height:60px;"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("版权信息") %>：</strong></td>
            <td><asp:TextBox ID="Copyright_T" runat="server" TextMode="MultiLine"  class="form-control m715-50" style="height:100px;"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdbgleft"><strong><%:lang.LF("站长统计脚本") %>：</strong></td>
            <td><asp:TextBox ID="allSiteJS" runat="server" TextMode="MultiLine" class="form-control m715-50" style="height:100px;" /></td>
        </tr>
    </table>
    <div style="width:100%;text-align:center;">
       <asp:Button ID="Button1" runat="server" Text="保存设置" style="margin-bottom:20px;" OnClick="Button1_Click" class="btn btn-primary" OnClientClick="disBtn(this,2000);"/>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style></style>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/Plugs/IconSelector.js"></script>
    <script type="text/javascript">
        $(function () {
            var iconsel = new iconSelctor("LogoAdmin_T");
            $("#icon_text_LogoAdmin_T").attr("placeholder", "初始值");
            $("#icon_btn_LogoAdmin_T").attr("class", "btn btn-default");
            $("#icon_btn_LogoAdmin_T").text("选取图标");
        });
        //更新配置前检测
        function CheckInfo() {
            var a = document.getElementById("<%=CustomPath.ClientID%>").value;
            if (a.length < 3) {
                alert("个性化路径最少三位");
                document.getElementById("<%=CustomPath.ClientID%>").focus();
                return false;
            }
            else if (a.toLowerCase() == "user") {
                alert("不允许使用已有目录名user");
                document.getElementById("<%=CustomPath.ClientID%>").focus();
                return false;
            }
        disBtn("Button1", 3000);
        return true;
    }
    function setPlatName() {
        var tlp = "国际互联<span>网站门户管理系统</span>";
        $("#LogoPlatName_T").val(tlp);
    }
    </script>
</asp:Content>
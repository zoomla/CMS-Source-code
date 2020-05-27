<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateCode.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.UpdateCode"  MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>修改二维码</title>
<style>
    #codeTable tr td{line-height:30px;}
    #crt2 td{padding:5px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<div id="HeadNav" runat="server">
<table class="table" >
<tr class="text-center">
<td id="TabTitle1" runat="server" name="TabTitle1" class="titlemouseover">
文本二维码
</td>
<td id="TabTitle2" runat="server" name="TabTitle2" class="titlemouseover">
名片信息
</td>
</tr>
</table>
</div>
    
    <div id="crt1" runat="server">
<table id="codeTable" class="table table-bordered">
    <tr>
        <td>
            纠错等级:<asp:DropDownList id="Level" CssClass="form-control" runat="server" Width="80px">
            <asp:ListItem Selected="True">L</asp:ListItem>
            <asp:ListItem>M</asp:ListItem>
            <asp:ListItem>Q</asp:ListItem>
            <asp:ListItem>H</asp:ListItem>
        </asp:DropDownList>
        版本:<asp:DropDownList id="Version" CssClass="form-control" runat="server" Width="80px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem Selected="True">5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
        大小:<asp:TextBox CssClass="form-control" ID="TxtSize" Text="4"  runat="server" Width="50px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="TxtContent" class="form-control" runat="server" Height="50px" TextMode="MultiLine" Width="435px"></asp:TextBox>
        <br />
        <span style="color:#808000;">注：如需生成网址则须在开头加上http:// 如(http://www.baidu.com)</span>
        <br /> 
       <div id="img1div"></div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="BtnCreate" CssClass="btn btn-primary" Text="保存修改" runat="server" OnClick="BtnCreate_Click" /> 
         &nbsp;
        <asp:Button ID="BtnSave" Text="下载到本地" CssClass="btn btn-primary" runat="server" OnClick="BtnSave_Click" />
        &nbsp;
       <input id="Button2" type="button" value="获取调用代码" class="btn btn-primary"  onclick="copy()"/> 
         &nbsp;
        <input name="Cancel" type="button" class="btn btn-primary"  id="Button1" value="返回列表" onclick="javescript: history.go(-1)" />   
        
        </td>
    </tr>
</table>
    </div>
    <div id="crt2" runat="server">
        <table>
            <tr>
                <td>姓名:</td>
                <td><input  class="form-control" runat="server" id="FN"></td>
                <td>个人主页:</td>
                <td><input class="form-control" id="URL" runat="server"></td>
                <td>邮箱:</td>
                <td><input class="form-control" runat="server" id="EMAIL">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入正确的邮箱格式" Display="Dynamic" ControlToValidate="EMAIL" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>MSN:</td>
                <td><input class="form-control" id="MSN" runat="server"></td>
                <td>QQ:</td>
                <td><input class="form-control" id="QQ" runat="server"></td>
                <td>手机:</td>
                <td>
                    <input class="form-control" runat="server" id="TEL">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="请输入正确的手机号码" ControlToValidate="TEL" Display="Dynamic" ValidationExpression="^1\d{10}$"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
          <div id="img2div"></div>
        <br />
        <asp:Button ID="BtnCreates" OnClick="BtnCreates_Click" CssClass="btn btn-primary" Text="保存修改" runat="server"/> 
         &nbsp;
        <asp:Button ID="BtnSaves" Text="下载到本地" CssClass="btn btn-primary" OnClick="BtnSave_Click" runat="server"/>
         &nbsp;
       <input id="Button3" type="button" value="获取调用代码" class="btn btn-primary"  onclick="copy()"/> 
        &nbsp;
         <input name="Cancel" type="button" class="btn btn-primary"  id="Cancel" value="返回列表" onclick="javescript: history.go(-1)" />   
    </div>
<div id="ShowImgs" runat="server" visible="false" style="display:none;">
<table class="table table-bordered">
<tr>
<td class="text-left">
<asp:Image ID="ImgCode" runat="server"/><br />
</td>	</tr>
<tr>
<td class="text-left" ><asp:TextBox TextMode="MultiLine" ID="TxtZoneCode" Rows="5" runat="server"  class="form-control"></asp:TextBox><br /></td>
</tr>  
<tr>
<td class="text-left" >   
</td>
</tr> 
<tr>
<td class="text-left" >
<span style="color:Grey">调用方法：点击按钮复制代码，粘贴到网页中的指定位置即可。</span></td>
</tr>
</table>
</div>
<script>
    if (document.getElementById("img1div"))
        document.getElementById("img1div").innerHTML = document.getElementById("ShowImgs").innerHTML;
    if (document.getElementById("img2div"))
        document.getElementById("img2div").innerHTML = document.getElementById("ShowImgs").innerHTML;
</script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogSearch.aspx.cs" Inherits="User_UserZone_LogManage_LogSearch" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>搜索日志</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
    function submitForms(num) { 
        if(num==1) {
            if (document.getElementById("radioSex1").checked) {
                document.getElementById("sex").value = "1";
            }
            else {
                document.getElementById("sex").value = "0";
            }
            document.getElementById("age1").value = document.getElementById("txtAge1").value;
            document.getElementById("age2").value = document.getElementById("txtAge2").value;
            document.getElementById("ltype").value = document.getElementById("LogType").value;
            document.forms[1].submit();
        }
        else if (num == 2) {
        if (document.getElementById("radio1").checked) {
            document.getElementById("sType").value = "0";
        }
        else {
            document.getElementById("sType").value = "1";
        }
        document.getElementById("TypeStr").value = document.getElementById("txtSearch").value;
            document.forms[2].submit();
        }
    
    }
</script>
</head>
<body>   
    <div>
    <form id="form1" runat="server" action="LogSearchResult.aspx" method="post">
    <table>
        <tr>
            <td>作者性别：</td><td>
                <input id="radioSex1" type="radio" value="男生" name="authorSex"  checked="checked"/>男生<input id="radioSex2" type="radio"  name="authorSex"  value="女生"/>女生
            </td>
        </tr>
        <tr>
            <td>作者年龄：</td><td>
                <asp:DropDownList ID="txtAge1" runat="server">
                <asp:ListItem Value="18" Text="18"/><asp:ListItem Value="19" Text="19"/><asp:ListItem Value="20" Text="20"/>
                <asp:ListItem Value="21" Text="21"/><asp:ListItem Value="22" Text="22"/><asp:ListItem Value="23" Text="23"/>
                <asp:ListItem Value="24" Text="24"/><asp:ListItem Value="25" Text="25"/><asp:ListItem Value="26" Text="26"/>
                <asp:ListItem Value="27" Text="27"/><asp:ListItem Value="28" Text="28"/><asp:ListItem Value="29" Text="29"/>
                <asp:ListItem Value="30" Text="30"/><asp:ListItem Value="31" Text="31"/><asp:ListItem Value="32" Text="32"/>
                <asp:ListItem Value="33" Text="33"/><asp:ListItem Value="34" Text="34"/><asp:ListItem Value="35" Text="35"/>
                <asp:ListItem Value="36" Text="36"/><asp:ListItem Value="37" Text="37"/><asp:ListItem Value="38" Text="38"/>
                <asp:ListItem Value="39" Text="39"/><asp:ListItem Value="40" Text="40"/><asp:ListItem Value="41" Text="41"/>
                <asp:ListItem Value="42" Text="42"/><asp:ListItem Value="43" Text="43"/><asp:ListItem Value="44" Text="44"/>
                <asp:ListItem Value="45" Text="45"/><asp:ListItem Value="46" Text="46"/><asp:ListItem Value="47" Text="47"/>
                <asp:ListItem Value="48" Text="48"/><asp:ListItem Value="49" Text="49"/><asp:ListItem Value="50" Text="50"/>
                <asp:ListItem Value="51" Text="51"/><asp:ListItem Value="52" Text="52"/><asp:ListItem Value="53" Text="53"/>
                <asp:ListItem Value="54" Text="54"/><asp:ListItem Value="55" Text="55"/><asp:ListItem Value="56" Text="56"/>
                <asp:ListItem Value="57" Text="57"/><asp:ListItem Value="58" Text="58"/><asp:ListItem Value="59" Text="59"/>
                <asp:ListItem Value="60" Text="60"/><asp:ListItem Value="61" Text="61"/><asp:ListItem Value="62" Text="62"/>             
                <asp:ListItem Value="63" Text="63"/><asp:ListItem Value="64" Text="64"/><asp:ListItem Value="65" Text="65"/>
                <asp:ListItem Value="66" Text="66"/><asp:ListItem Value="67" Text="67"/><asp:ListItem Value="68" Text="68"/>
                <asp:ListItem Value="69" Text="69"/><asp:ListItem Value="70" Text="70"/><asp:ListItem Value="71" Text="71"/>
                <asp:ListItem Value="72" Text="72"/><asp:ListItem Value="73" Text="73"/><asp:ListItem Value="74" Text="74"/>
                <asp:ListItem Value="75" Text="75"/><asp:ListItem Value="76" Text="76"/><asp:ListItem Value="77" Text="77"/>
                <asp:ListItem Value="78" Text="78"/><asp:ListItem Value="79" Text="79"/><asp:ListItem Value="80" Text="80"/>               
                </asp:DropDownList>到
                <asp:DropDownList ID="txtAge2" runat="server">
                <asp:ListItem Value="18" Text="18"/><asp:ListItem Value="19" Text="19"/><asp:ListItem Value="20" Text="20"/>
                <asp:ListItem Value="21" Text="21"/><asp:ListItem Value="22" Text="22"/><asp:ListItem Value="23" Text="23"/>
                <asp:ListItem Value="24" Text="24"/><asp:ListItem Value="25" Text="25"/><asp:ListItem Value="26" Text="26"/>
                <asp:ListItem Value="27" Text="27"/><asp:ListItem Value="28" Text="28"/><asp:ListItem Value="29" Text="29"/>
                <asp:ListItem Value="30" Text="30"/><asp:ListItem Value="31" Text="31"/><asp:ListItem Value="32" Text="32"/>
                <asp:ListItem Value="33" Text="33"/><asp:ListItem Value="34" Text="34"/><asp:ListItem Value="35" Text="35"/>
                <asp:ListItem Value="36" Text="36"/><asp:ListItem Value="37" Text="37"/><asp:ListItem Value="38" Text="38"/>
                <asp:ListItem Value="39" Text="39"/><asp:ListItem Value="40" Text="40"/><asp:ListItem Value="41" Text="41"/>
                <asp:ListItem Value="42" Text="42"/><asp:ListItem Value="43" Text="43"/><asp:ListItem Value="44" Text="44"/>
                <asp:ListItem Value="45" Text="45"/><asp:ListItem Value="46" Text="46"/><asp:ListItem Value="47" Text="47"/>
                <asp:ListItem Value="48" Text="48"/><asp:ListItem Value="49" Text="49"/><asp:ListItem Value="50" Text="50"/>
                <asp:ListItem Value="51" Text="51"/><asp:ListItem Value="52" Text="52"/><asp:ListItem Value="53" Text="53"/>
                <asp:ListItem Value="54" Text="54"/><asp:ListItem Value="55" Text="55"/><asp:ListItem Value="56" Text="56"/>
                <asp:ListItem Value="57" Text="57"/><asp:ListItem Value="58" Text="58"/><asp:ListItem Value="59" Text="59"/>
                <asp:ListItem Value="60" Text="60"/><asp:ListItem Value="61" Text="61"/><asp:ListItem Value="62" Text="62"/>             
                <asp:ListItem Value="63" Text="63"/><asp:ListItem Value="64" Text="64"/><asp:ListItem Value="65" Text="65"/>
                <asp:ListItem Value="66" Text="66"/><asp:ListItem Value="67" Text="67"/><asp:ListItem Value="68" Text="68"/>
                <asp:ListItem Value="69" Text="69"/><asp:ListItem Value="70" Text="70"/><asp:ListItem Value="71" Text="71"/>
                <asp:ListItem Value="72" Text="72"/><asp:ListItem Value="73" Text="73"/><asp:ListItem Value="74" Text="74"/>
                <asp:ListItem Value="75" Text="75"/><asp:ListItem Value="76" Text="76"/><asp:ListItem Value="77" Text="77"/>
                <asp:ListItem Value="78" Text="78"/><asp:ListItem Value="79" Text="79"/><asp:ListItem Value="80" Text="80"/>               
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>日记类型：</td><td>
                <asp:DropDownList ID="LogType" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">                
                <input id="Button2" type="button" value="确定" onclick="submitForms(1)" /></td>
        </tr>
        <tr>
            <td align="right">精确搜索:</td>
            <td><input id="radio1" type="radio" value="0" name="searchType"  checked="checked"/>标题<input id="radio2" type="radio"  name="searchType"  value="1"/>作者</td>
        </tr>
        <tr><td colspan="2" align="center"><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <input id="Button1" type="button" value="确定" onclick="submitForms(2)"/></td>
        </tr>
        </table>
        </form>
        <form id="form2" action="LogSearchResult.aspx" method="post">
            <input id="sex" name="sex" type="hidden" />
            <input id="age1" name="age1" type="hidden" />
            <input id="age2" name="age2" type="hidden" />
            <input id="ltype" name="ltype" type="hidden" />
        </form>
        <form id="form3" action="LogSearchResult.aspx" method="post">       
                <input id="sType" name="sType" type="hidden" />
                <input id="TypeStr" name="TypeStr" type="hidden" />         
        </form>
    </div>
</body>
</html>
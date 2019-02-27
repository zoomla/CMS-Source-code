<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chickorder.aspx.cs" Inherits="manage_Shop_Chickorder" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>客户签收单</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <!--startprint-->
<table class="table table-striped table-bordered table-hover">
    <tbody id="Tbody1">
        <tr class="tdbg">
            <td colspan="6" align="center" class="title"><asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="center" class="style15">订单号：</td><td align="left" class="style1"><asp:Label ID="dingdanID" runat="server" Text=""></asp:Label></td>
            <td align="center" class="style2">收货人姓名:</td>
            <td width="20%" align="center" style="height: 23px"><asp:Label ID="Reuser" runat="server" Text=""></asp:Label>&nbsp;</td>
           <td align="center" class="style3">要求配送日期:</td>
            <td align="left" style="width: 13%; height: 23px"><asp:Label ID="addtime" runat="server"></asp:Label></td>              
        </tr>
        <tr>
            <td align="center" class="style15">收货人电话:</td>
            <td align="left" class="style1">                                
            <asp:Label ID="Phone" runat="server" Text=""></asp:Label></td>
                <td align="center" class="style2">收货人手机:</td>
                <td width="20%" align="center" style="height: 23px"><asp:Label ID="Mobile" runat="server" Text=""></asp:Label></td>
                <td align="center" class="style3">
                    送货时段:</td>
            <td align="left" style="width: 13%; height: 23px">
                <asp:Label ID="Deliverytime" runat="server" Text=""></asp:Label>
            </td>      
        </tr>
        <tr>
            <td  align="center" class="style16">
                收货人地址:
            </td>               
            <td colspan="5"><asp:Label ID="Jiedao" runat="server" Text=""></asp:Label></td>               
        </tr>
        <tr>
            <td align="center" class="style16" rowspan="2">配送商品:</td>
            <td align="center" class="title" style="width: 25%;">商品名称</td>
            <td align="center" class="title" style="width: 10%;">售价</td>
            <td align="center" class="title" style="width: 5%;">数量</td>
            <td align="center" class="title">金额</td>
            <td></td>
        </tr>
        <tr>      
           <asp:Repeater ID="procart" runat="server" OnItemDataBound="cartinfo_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td></td>
                        <td><a href='<%#GetShopUrl()%>' target='_blank'><%#Eval("proname")%></a></td>
                        <td><%#GetLinPrice()%></td>
                        <td><%#Eval("pronum") %></td>
                        <td><%#GetPrice()%></td>
                        <td></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <tr>
            <td  align="center" class="style16">总金额:</td><td><asp:Label runat="server" ID="AllMoney_L"></asp:Label></td>   
            <td>邮费:</td>
            <td colspan="3"><asp:Label runat="server" ID="Fare_L"></asp:Label></td>            
        </tr>
        <tr>
            <td align="center" class="style16">
                <br />
                留言:<br />
                <br />
            </td>
            <td colspan="5">
                <asp:Label ID="Ordermessage" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td  align="center" class="style16">署名:</td>               
            <td colspan="5"><asp:Label ID="Reusers" runat="server" Text=""></asp:Label></td>               
        </tr>
    </tbody>
</table>
<br /><br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
………………请配送员沿此线剪下，此线以上部分交给客户，以下部分留存。………………
<br /><br />
<table class="table table-striped table-bordered table-hover">
    <tbody id="Tbody2">
        <tr class="tdbg">
            <td colspan="2" align="center" class="title"><asp:Label ID="Label2" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" class="style13">收货人对货品反馈意见：</td><td align="left" class="style1">
            <asp:CheckBox ID="CheckBox1" runat="server"  Text="很满意"/>
            <asp:CheckBox ID="CheckBox2" runat="server"  Text="满意" />
            <asp:CheckBox ID="CheckBox3" runat="server"  Text="不满意" />
            </td>
        </tr>
        <tr>
            <td  align="center" class="style14">
                <span>收货人签名:</span></td>               
            <td>
                <br />
                <br />
                <br />
            </td>               
        </tr>
        <tr>
            <td  align="center" class="style14">
                <span>签收日期</span>:</td>               
            <td>
                &nbsp;<br />
&nbsp;&nbsp;&nbsp;<span lang="EN-US">______</span><span>年</span><span lang="EN-US">___ </span>
                <span>月</span><span lang="EN-US">____</span><span>日</span>&nbsp;<br />
            </td>               
        </tr>
        <tr>
            <td  align="center" class="style11">
                <span>请留下您的宝贵意见</span>:</td>               
            <td class="style12">
                <br />
                <br />
                <br />
                <br />
            </td>               
        </tr>
        <tr>
            <td  align="center" class="style14" colspan="2">
                <asp:Button ID="Button1" class="C_input"  style="width:100px;" runat="server" Text="打印签收单" Width="100px" OnClientClick="preview();return false;" /></td>               
        </tr>
    </tbody>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function preview() {
            location.href = "Chickorder.aspx?id=" +<%=Request.QueryString["id"] %> +"&menu=print";
            pageload();
        }
        $().ready(function () {
            pageload();
        })
    function pageload() {
<%
if(Request.QueryString["menu"]=="print") 
{
%>
    bdhtml = window.document.body.innerHTML;
    sprnstr = "<!--startprint-->";
    eprnstr = "<!--endprint-->";
    prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
    prnhtml = prnhtml.substring(0);
    window.document.body.innerHTML = prnhtml;
    window.print();
<%
} 
%>
}
        function show() {
            var fahuo = document.getElementById("fahuo").style;
            if (fahuo.display == "") {
                fahuo.display = "none";
            } else {
                fahuo.display = "";
            }
        }
        function opentitle(url, title) {
            var diag = new Dialog();
            diag.Width = 600;
            diag.Height = 400;
            diag.Title = title;
            diag.URL = url;
            diag.show();
        }        
    </script>
</asp:Content>
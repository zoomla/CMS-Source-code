<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paypalmanage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.I.Pay.Paypalmanage" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>编辑在线支付平台</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
    <tr align="center">
        <td class="spacingtitle" colspan="2">
            <asp:Label ID="LblTitle" runat="server" Text="PayPal支付平台" />
        </td>
    </tr>
    <tr>
        <td>方式名称：</td>
        <td>
            <asp:TextBox ID="payname" runat="server" ReadOnly="True" class="form-control text_md pull-left">Paypal或Visa,Master,Solo等银行卡在线支付</asp:TextBox>
        </td>            
    </tr>
    
   <tr>
        <td><strong>接口类型：</strong></td>
        <td>
            <asp:TextBox ID="pay_intf" runat="server" ReadOnly="True" class="form-control text_md pull-left">PayPal外贸必选(标准版)</asp:TextBox>
        </td>            
    </tr>
    
    <tr>
        <td><strong>客户号：</strong></td>
        <td>
            <asp:TextBox ID="clientid" runat="server" class="form-control text_md pull-left"/>
            <span class="p9gray tips">
            此处填写您的支付帐号、客户号或客户id等，此帐号在支付服务提供商处取得；</span>
        </td>            
    </tr>
    <tr>
        <td><strong>身份标记：</strong></td>
        <td>
            <asp:TextBox ID="title_T" runat="server" class="form-control text_md pull-left"/>
        </td>            
    </tr>
    <tr>
        <td><strong>支持交易货币：</strong></td>
        <td class="style3">
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="83px" RepeatColumns="8" Width="586px" AutoPostBack="True">                     
        <asp:ListItem Value='CNY' >人民币 </asp:ListItem>
        <asp:ListItem Value='USD' >美元 </asp:ListItem>
        <asp:ListItem Value='EUR' >欧元 </asp:ListItem>
        <asp:ListItem value='GBP' >英镑 </asp:ListItem>
        <asp:ListItem value='CAD' >加拿大元 </asp:ListItem>
        <asp:ListItem value='AUD' >澳元 </asp:ListItem>
        <asp:ListItem value='NZD' >新西兰元 </asp:ListItem>
        <asp:ListItem value='RUB' >卢布 </asp:ListItem>
        <asp:ListItem value='HKD' >港币 </asp:ListItem>
        <asp:ListItem value='TWD' >新台币 </asp:ListItem>
        <asp:ListItem value='KRW' >韩元 </asp:ListItem>
        <asp:ListItem value='SGD' >新加坡元 </asp:ListItem>
        <asp:ListItem value='JPY' >日元</asp:ListItem>
        <asp:ListItem value='MYR' >马元 </asp:ListItem>
        <asp:ListItem value='CHF' >瑞士法郎</asp:ListItem>
        <asp:ListItem value='SEK' >瑞典克朗 </asp:ListItem>
        <asp:ListItem value='DKK' >丹麦克朗 </asp:ListItem>
        <asp:ListItem value='PLZ' >兹罗提 </asp:ListItem>
        <asp:ListItem  value='NOK' >挪威克朗 </asp:ListItem>
        <asp:ListItem value='HUF' >福林 </asp:ListItem>
        <asp:ListItem  value='CSK' >捷克克朗</asp:ListItem>     
        </asp:CheckBoxList>
        选择某种交易货币，请先确认“货币管理－货币列表”中已经存在此币种，如不存在请先进行货币种类的添加；</span></td>            
    </tr>
    <tr>
        <td>支付手续费：</td>
        <td>
            <asp:TextBox ID="number" runat="server" Text="p*0.05" class="form-control text_md pull-left"/>
            <span class="tips">
            此处可以输入公式来计算支付手续费，并可使用后面的公式验算功能审核手续费，公式中订单金额以字母“p”表示，；</span>
        </td>            
    </tr>     
</table>
</ContentTemplate>
</asp:UpdatePanel>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="table table-bordered">
    <tr>
        <td align="center">
        &nbsp;&nbsp; &nbsp;
        <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary"/>&nbsp; &nbsp;
        <input name="Cancel" type="button" id="BtnCancel" value="取消" onclick="window.location.href = 'PayPlatManage.aspx'" class="btn btn-primary"/></td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>
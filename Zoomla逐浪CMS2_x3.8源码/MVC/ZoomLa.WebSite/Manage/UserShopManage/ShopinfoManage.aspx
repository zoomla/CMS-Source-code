<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopinfoManage.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.ShopinfoManage" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <title>信息配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class=" text-center" colspan="2">店铺信息配置</td>
        </tr>
        <tr style="display: none">
            <td class="tdleft"><b>启用多用户商城：</b></td>
            <td style="width: 80%;">
                <input type="checkbox" runat="server" id="IsOpen" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><b>商家发布商品需要审核：</b></td>
            <td style="width: 80%;">
                <input type="checkbox" runat="server" id="Auditing" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><b>允许匿名购买：</b></td>
            <td style="width: 80%;">
                <input type="checkbox" runat="server" id="Anonymity" class="switchChk" />
            </td>
        </tr>
        <tr style="display: none">
            <td class="tdleft"><b>允许使用点卡：</b></td>
            <td style="width: 80%;">
                <input type="checkbox" runat="server" id="Pointcard" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><b>允许使用虚拟货币：</b></td>
            <td style="width: 80%;">
                <input type="checkbox" runat="server" id="Dummymoney" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><b>允许评论：</b></td>
            <td style="width: 80%;">
                <input type="checkbox" runat="server" id="Comment" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><b>允许用户修改订单价格：</b></td>
            <td style="width: 80%;">
                <input type="checkbox" runat="server" id="ChangOrder" class="switchChk" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><b>好评积分：</b></td>
            <td style="width: 80%;">
                <asp:TextBox ID="goodpl" class="form-control text_md" runat="server">0</asp:TextBox></td>
        </tr>

        <tr>
            <td class="tdleft"><b>中评积分：</b></td>
            <td style="width: 80%;">
                <asp:TextBox ID="centerpl" class="form-control text_md" runat="server">0</asp:TextBox></td>
        </tr>

        <tr>
            <td class="tdleft"><b>差评积分：</b>
            </td>
            <td style="width: 80%;">
                <asp:TextBox ID="badpl" class="form-control text_md" runat="server">0</asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdleft"><b>赠送积分折算率：</b>
            </td>
            <td style="width: 80%;">
                <asp:TextBox ID="ScorePoint" class="form-control text_x" runat="server">0</asp:TextBox><span>%(即扣除现金比率)</span></td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="更新设置" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>


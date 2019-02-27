<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedPacketAdd.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.RedPacketAdd" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>红包管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr><td class="td_m">公众号</td><td><asp:Label runat="server" ID="Alias_L"></asp:Label></td></tr>
        <tr><td>红包名称</td><td><ZL:TextBox runat="server" ID="Name_T" AllowEmpty="false" class="form-control text_300" MaxLength="16"/></td></tr>
        <tr><td>匹配码</td><td>
            <ZL:TextBox runat="server" ID="Flow_T" AllowEmpty="false" class="form-control text_300" MaxLength="12"/>
            <span class="rd_green">领取红包需要核对匹配码与领取码</span>
                        </td></tr>
        <tr><td>金额范围</td><td>
            <ZL:TextBox runat="server" ID="AmountRange_T" AllowEmpty="false" class="form-control text_300" Text="1-10"/>
            <span class="rd_green">可直接指定数值,或设为随机金额,格式:最小值-最大值</span>
           </td></tr>
        <tr><td>红包数量</td><td><ZL:TextBox runat="server" ID="RedNum_T" AllowEmpty="false" ValidType="IntPostive" class="form-control text_300" Text="10"/></td></tr>
        <tr><td>红包码格式</td>
            <td>
               <ZL:TextBox runat="server" ID="CodeFormat_T" CssClass="form-control text_300" Text="RD{000000AA}" AllowEmpty="false" MaxLength="20"/>
               <span class="rd_green">示例:RD{000000AA},0表示数字占位符,A表示字母占位符</span>
            </td>
        </tr>
        <tr><td>生效日期</td><td><asp:TextBox runat="server" ID="SDate_T" class="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' });"/></td></tr>
        <tr><td>到期日期</td><td><asp:TextBox runat="server" ID="EDate_T" class="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' });"/></td></tr>
        <tr><td>祝福语</td><td><asp:TextBox runat="server" ID="Wishing_T" class="form-control m715-50" MaxLength="64"/></td></tr>
        <tr><td>活动备注</td><td><asp:TextBox runat="server" ID="Remind_T" CssClass="form-control m715-50" MaxLength="128" /></td></tr>
        <tr><td></td><td>
            <asp:Button runat="server" ID="Save_Btn" Text="生成红包" class="btn btn-primary" OnClick="Save_Btn_Click" />
         </td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>

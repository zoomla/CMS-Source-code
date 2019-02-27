<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BCAdd.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Sched.BCAdd" MasterPageFile="~/Common/Master/UserEmpty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<style type="text/css">
.datePick {width:90px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="draftnav">
        <a href="PBManage.aspx">排班管理</a>/<a href="BCManage.aspx">班次管理</a>/<span>添加班次</span>
    </div>
    <div style="margin-left:10px; margin-right:10px;">
        <table class="table_li table-border">
            <tr>
                <td style="text-align:right;">班次名:</td>
                <td><asp:TextBox runat="server" CssClass="form-control" ID="bcNameT" MaxLength="20" Width="213" /></td>
            </tr>
            <tr>
                <td style="text-align:right;">时间段:</td>
                <td>
                    <asp:TextBox runat="server" ID="beginTimeT" CssClass="datePick form-control" onfocus="WdatePicker({dateFmt: 'HH:mm', minDate: '0:00:00', maxDate: '25:00:00' })"/>
                    <img src="/App_Themes/AdminDefaultTheme/images/sys_idx_arrow.gif" style="position:relative;margin-top:6px; float:left;" />
                    <asp:TextBox runat="server" ID="endTimeT"  CssClass="datePick form-control" onfocus="WdatePicker({dateFmt: 'HH:mm', minDate: '0:00:00', maxDate: '25:00:00' })"/>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">备注:</td>
                <td><asp:TextBox runat="server" ID="remindT" CssClass="form-control" TextMode="MultiLine" Height="100" Width="213" /></td>
            </tr>
            <tr>
                <td style="text-align:right;">色彩:</td>
                <td>
                    <script language="JavaScript" type="text/javascript">
                        function SelectColor(t, clientId) {
                            var url = "/Common/SelectColor.aspx?d=f&t=6";
                            var old_color = (document.getElementById(clientId).value.indexOf('#') == 0) ? '&' + document.getElementById(clientId).value.substr(1) : '&' + document.getElementById(clientId).value;
                            var color = "";
                            if (document.all) {
                                color = showModalDialog(url + old_color, "", "dialogWidth:18.5em; dialogHeight:16.0em; status:0");
                                if (color != null) {
                                    document.getElementById(clientId).value = color;
                                } else {
                                    document.getElementById(clientId).focus();
                                }
                            } else {
                                color = window.open(url + '&' + clientId, "hbcmsPop", "top=200,left=200,scrollbars=yes,dialog=yes,modal=no,width=300,height=260,resizable=yes");
                            }
                        }
                    </script>
                    <asp:TextBox ID="ColorDefault" CssClass="form-control" Width="60" runat="server" ></asp:TextBox>
                    <img alt="" onclick="SelectColor(this,'ColorDefault');" src="/App_Themes/AdminDefaultTheme/Images/selectclolor.gif" style="margin-left:5px; vertical-align:middle; border-width: 0px; cursor: pointer" />
                    <asp:RegularExpressionValidator ID="REV1" ControlToValidate="ColorDefault" ValidationExpression="^(#[0-9a-fA-F]{6}$)|(#[0-9a-fA-F]{3}$)" runat="server" ForeColor="Red" ErrorMessage="颜色代码输入格式不正确"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">操作:</td>
                <td>
                    <asp:Button runat="server" ID="saveBtn" CssClass="btn btn-primary" Text="保存" OnClick="saveBtn_Click"/>
                    <input type="button" id="retBtn" onclick="window.location.href = 'BCManage.aspx'" class="btn btn-primary" value="返回" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

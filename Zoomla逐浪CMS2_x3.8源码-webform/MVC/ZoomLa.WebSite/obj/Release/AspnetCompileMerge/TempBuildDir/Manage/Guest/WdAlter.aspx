<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WdAlter.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.WdAlter" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>问答中心</title>
    <style>
    </style>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <table class="table table-bordered table-hover table-condensed">
        <tr>
            <td class="text-center td_m" style=" line-height:120px;">
                您的问题:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtContent"  CssClass="m715-50" TextMode="MultiLine" Height="200"></asp:TextBox>
                <%=Call.GetUEditor("txtContent",3) %>
            </td>
        </tr>
        <tr>
            <td class="text-center" style="line-height:120px;">补充问题:</td>
            <td>
                <asp:TextBox runat="server" ID="txtSupplyment"  CssClass="m715-50"  TextMode="MultiLine" Height="200"></asp:TextBox>
                <%=Call.GetUEditor("txtSupplyment",3) %>
            </td>
        </tr>
        <tr>
            <td class="text-center tdtitle">问题分类:</td>
            <td>
                <asp:DropDownList ID="ddlCate" CssClass="form-control" runat="server" Width="100px">
                                <asp:ListItem>电脑网络</asp:ListItem>
                                <asp:ListItem>生活</asp:ListItem>
                                <asp:ListItem>医疗健康</asp:ListItem>
                                <asp:ListItem>体育运动</asp:ListItem>
                                <asp:ListItem>电子数码</asp:ListItem>
                                <asp:ListItem>商业理财</asp:ListItem>
                                <asp:ListItem>教育科学</asp:ListItem>
                                <asp:ListItem>社会民生</asp:ListItem>
                                <asp:ListItem>文化艺术</asp:ListItem>
                                <asp:ListItem>娱乐休闲</asp:ListItem>
                                <asp:ListItem>烦恼</asp:ListItem>
                                <asp:ListItem>地区旅行</asp:ListItem>
                            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="text-center tdtitle">悬赏分数:</td>
            <td>
                <asp:DropDownList ID="ddlScore" CssClass="form-control" runat="server" Width="100px">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>60</asp:ListItem>
                                <asp:ListItem>70</asp:ListItem>
                                <asp:ListItem>80</asp:ListItem>
                                <asp:ListItem>90</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button runat="server" ID="btnSubmit" Text="提交" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                <a href="WdCheck.aspx" class="btn btn-primary">返回</a>
            </td>
        </tr>
    </table>
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function supplyment() {
            var div = document.getElementById("divSupplyment");
            if (div.style.display == "none") {
                div.style.display = "block";
            }
            else {
                div.style.display = "none";
            }
        }
        function CheckDirty() {
            var TxtTTitle = document.getElementById("TxtTTitle").value;
            var TxtValidateCode = document.getElementById("TxtValidateCode").value;

            if (value == "" || TxtTTitle == "" || TxtValidateCode == "") {
                if (value == "") {
                    var obj = document.getElementById("RequiredFieldValidator1");
                    obj.innerHTML = "<font color='red'>内容不能为空！</font>";
                }
                else {
                    var obj = document.getElementById("RequiredFieldValidator1");
                    obj.innerHTML = "";
                }
                if (TxtTTitle == "") {
                    var obj2 = document.getElementById("RequiredFieldValidator2");
                    obj2.innerHTML = "<font color='red'>留言标题不能为空！</font>";
                }
                else {
                    var obj2 = document.getElementById("RequiredFieldValidator2");
                    obj2.innerHTML = "";
                }
                if (TxtValidateCode == "") {
                    var obj3 = document.getElementById("sp1");
                    obj3.innerHTML = "<font color='red'>验证码不能为空！</font>";
                } else {
                    var obj3 = document.getElementById("sp1");
                    obj3.innerHTML = "";
                }
                return false;
            }
            else {
                var obj = document.getElementById("RequiredFieldValidator1");
                obj.innerHTML = "";
                var obj2 = document.getElementById("RequiredFieldValidator2");
                obj2.innerHTML = "";
                var obj3 = document.getElementById("sp1");
                obj3.innerHTML = "";
                document.getElementById("EBtnSubmit2").click();
            }
        }
    </script>
</asp:Content>

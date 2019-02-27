<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddArrive.aspx.cs" Inherits="manage_Shop_Arrive_AddArrive" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>编辑优惠劵</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
        <tr>
            <td><strong>卡别名：</strong></td>
            <td>
                <ZL:TextBox ID="txtName" runat="server" CssClass="form-control text_300" AllowEmpty="false" ValidType="String"></ZL:TextBox>
                <font color="red">*</font><font color="green">便于识别的名称</font>
            </td>
        </tr>
        <tr>
            <td><strong>优惠券类型：</strong></td>
            <td>
                <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="Magclass">
                    <asp:ListItem Value="0" Selected="True"><lable style="font-weight:normal">现金卡&nbsp;&nbsp;</lable></asp:ListItem>
                    <asp:ListItem Value="1"><lable style="font-weight:normal">银行卡&nbsp;&nbsp;</lable></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="EditecodType" runat="server">
            <td><strong>编码类型：</strong></td>
            <td>
                <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="EcodeType">
                    <asp:ListItem Value="2" Selected="True"><lable style="font-weight:normal">混淆 </lable></asp:ListItem>
                    <asp:ListItem Value="0"><lable style="font-weight:normal">数字 </lable></asp:ListItem>
                    <asp:ListItem Value="1"><lable style="font-weight:normal">字母 </lable></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tbody id="no" runat="server">
            <tr>
                <td><strong>编号:</strong></td>
                <td>
                    <asp:TextBox ID="txtNo" runat="server" Enabled="false" CssClass="form-control text_md"></asp:TextBox></td>
            </tr>
            <tr>
                <td><strong>密码:</strong></td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server" Enabled="false" CssClass="form-control text_md"></asp:TextBox></td>
            </tr>
            <tr>
                <td><strong>状态:</strong></td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" Enabled="false" CssClass="form-control text_md"></asp:TextBox></td>
            </tr>
            <tr>
                <td><strong>所属用户:</strong></td>
                <td>
                    <asp:TextBox ID="txtUserID" runat="server" Enabled="false" CssClass="form-control text_md"></asp:TextBox>
                    <asp:HiddenField ID="hfid" runat="server" />
                    <input id="Button1" type="button" value="选择用户" onclick="ShowUserInfo(); void (0)" class="btn btn-primary" />
                    <input type="button" id="btnC" value="不送出" class="btn btn-primary" onclick="clears()" />
                </td>
            </tr>
        </tbody>
        <tr id="amountTr">
            <td><strong>金额：</strong></td>
            <td>
               <ZL:TextBox ID="Amount_T" runat="server" CssClass="form-control text_md" AllowEmpty="false" ValidType="FloatPostive" Text="0" /><span class="rd_red">*</span>
            </td>
        </tr>
        <tr id="amountAreaTr">
            <td><strong>适用范围：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="minAmount_T" class="form-control text_x" MaxLength="10" Text="0" /><span>到</span>
                <asp:TextBox runat="server" ID="maxAmount_T" class="form-control text_x" MaxLength="10" />
                <span class="rd_green">*如输入(200-300)则只有200-300单价商品才能使用，不填则代表不限制。</span>
        </tr>
        <tbody id="createNum" runat="server">
            <tr>
                <td><strong>生成数量：</strong></td>
                <td>
                    <ZL:TextBox ID="txtCreateNum" runat="server" CssClass="form-control text_md" AllowEmpty="false" ValidType="FloatPostive" Text="10" /><span class="rd_red">*</span></td>
            </tr>
        </tbody>
        <tr>
            <td><strong>生效时间：</strong></td>
            <td>
                <asp:TextBox ID="AgainTime_T" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd'});" runat="server" CssClass="form-control text_md"/>
                <span style="line-height: 26px;color: green; ">&nbsp;<i style="font-size:26px;" class="fa fa-calendar"></i></span>
                <asp:RequiredFieldValidator ID="RV2" runat="server" ControlToValidate="AgainTime_T" ErrorMessage="生效时间不能为空!" />
            </td>
        </tr>
        <tr>
            <td><strong>到期时间:</strong></td>
            <td>
                <asp:TextBox ID="EndTime_T" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd'});" runat="server" CssClass="form-control text_md"/>
                <span style="line-height: 26px; color: green;">&nbsp;<i style="font-size:26px;" class="fa fa-calendar"></i> 默认一年有效</span>
                <font color="red"><asp:RequiredFieldValidator ID="RV3" runat="server" ControlToValidate="EndTime_T" ErrorMessage="到期时间不能为空!" />
            </font></td>
        </tr>
        <tr>
            <td><strong>是否立即激活:</strong></td>
            <td>
                <asp:CheckBox runat="server" ID="isValid_Chk" Checked="true" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="制作优惠劵" runat="server" OnClick="EBtnSubmit_Click" />
                <a href="ArriveManage.aspx" class="btn btn-default">返回列表</a></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script>
    var userdiag = new ZL_Dialog();
    function ShowUserInfo() {
        var url = "/Common/Dialog/SelGroup.aspx";
        comdiag.maxbtn = false;
        ShowComDiag(url, "选择用户");
    }

    function UserFunc(json, select) {
        var uname = "";
        var uid = "";
        for (var i = 0; i < json.length; i++) {
            uname += json[i].UserName + ",";
            uid += json[i].UserID + ",";
        }
        if (uid) uid = uid.substring(0, uid.length - 1); {
            $("#txtUserID").val(uname);
            $("#hfid").val(uid);
        }
        CloseComDiag();
    }

    function clears() {
        var ite = document.getElementById("txtUserID");
        var ite1 = document.getElementById("hfid");
        ite.value = "未激活";
        ite1.value = "0";
    }
</script>
</asp:Content>

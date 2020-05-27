<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClass.aspx.cs" Inherits="ZoomLaCMS.MIS.Ke.AddClass"  MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
        <title>编辑班级</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField runat="server" ID="classid_Hid" />
    <div class="container margin_t5">
         <ol class="breadcrumb">
        <li><a href="/User/">用户中心</a></li>
        <li><a href="ClassManage.aspx">班级管理</a></li>
        <li class="active">编辑班级</li> 
        </ol>
</div>
    <div class="container btn_green">
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="ssjd_txt" runat="server" Text="班级名称："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Regulationame" runat="server" class="form-control text_md" ></asp:TextBox>
                    &nbsp;<font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label7" runat="server" Text="班级类别："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:DropDownList ID="classtype" runat="server" CssClass="form-control text_md"></asp:DropDownList>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="负责人："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Regulation" runat="server" class="form-control text_md" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label3" runat="server" Text="售价："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtShiPrice" runat="server" class="form-control text_md"  ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13"
                        runat="server" ControlToValidate="txtShiPrice" ErrorMessage="售价格式不对!" ValidationExpression="\d+[.]?\d*"
                        Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="优惠价："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtLinkPrice" runat="server" class="form-control text_md" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                        runat="server" ControlToValidate="txtLinkPrice" ErrorMessage="优惠价格式不对!" ValidationExpression="\d+[.]?\d*"
                        Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label5" runat="server" Text="课时："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtCoureTime" runat="server" class="form-control text_md"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label6" runat="server" Text="订购此班级是否赠送此课程："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <input type="checkbox" runat="server" id="rbl" class="switchChk"/>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Ratednumber_name" runat="server" Text="额定人数："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Ratednumber" runat="server" class="form-control text_xs"  ></asp:TextBox>
                    人
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label14" runat="server" Text="建立时间："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Setuptime" runat="server" class="form-control text_md"  onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label15" runat="server" Text="结束时间："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Endtime" runat="server" class="form-control text_md"   onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label8" runat="server" Text="开班天数："></asp:Label>
                </td>
                <td class="bqright">
                    <asp:Label ID="lbDay"  runat="server" CssClass="form-control text_xs"></asp:Label>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">

                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加班级" runat="server" OnClick="EBtnSubmit_Click" />
                &nbsp;
                <a href="ClassManage.aspx" class="btn btn-primary">返回列表</a>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        var typediag = new ZL_Dialog();
        function Openwin() {
            typediag.title = "选择分类";
            typediag.url = "SelectCourse.aspx";
            typediag.ShowModel();
        }
    </script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SnsModel.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.SnsModel" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<title>添加学校会员模型</title>
<script type="text/javascript">
    function ChangeImgItemIcon(icon)
    {
        document.getElementById("<%= ImgItemIcon.ClientID %>").src = "../../Images/ModelIcon/"+icon;
    }
    function ChangeTxtItemIcon(icon)
    {
        document.getElementById("<%= TxtItemIcon.ClientID %>").value = icon;
    }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="clearbox"></div>    
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="添加会员模型"></asp:Literal></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>会员模型名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtModelName" runat="server" CssClass="form-control text_300" Width="156" MaxLength="200" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtModelName">会员模型名称不能为空</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>创建的数据表名：</strong>
            </td>
            <td>
                <asp:Label ID="LblTablePrefix" runat="server" Text="ZL_School_" />
                <asp:TextBox ID="TxtTableName" CssClass="form-control text_300" runat="server" Width="120" MaxLength="50" /><font color="red">*</font>
                <asp:RegularExpressionValidator ID="ValeTableName" runat="server" ControlToValidate="TxtTableName"
                    ErrorMessage="只允许输入字母、数字或下划线" ValidationExpression="^[\w_]+$" SetFocusOnError="true"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>项目名称：</strong>
                <br />
                例如：文章、软件、图片、商品
            </td>
            <td>
                <asp:TextBox ID="TxtItemName" CssClass="form-control text_300" runat="server" Width="156" MaxLength="20" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtItemName"
                    ErrorMessage="RequiredFieldValidator">项目名称不能为空</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>项目单位：</strong>
                <br />
                例如：篇、个、张、件
            </td>
            <td>
                <asp:TextBox ID="TxtItemUnit" runat="server" CssClass="form-control text_300" Width="156" MaxLength="20" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtItemUnit"
                    ErrorMessage="RequiredFieldValidator">项目单位不能为空</asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>项目图标：</strong>
                <br />
                图标存放在~/Images/ModelIcon/目录下
            </td>
            <td>
                <asp:TextBox ID="TxtItemIcon" Text="Default.gif" CssClass="form-control text_300" runat="server" Width="156" MaxLength="20" />
                <asp:Image ID="ImgItemIcon" runat="server" ImageUrl="~/Images/ModelIcon/Default.gif" />
                <=<asp:DropDownList ID="DrpItemIcon" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>模型描述：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Width="365px"
                    Height="43px" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
            <asp:HiddenField ID="HdnModelId" runat="server" />
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" CssClass="btn btn-primary" runat="server" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" onclick="window.location.href='ModelManage.aspx';" />                
            </td>
        </tr>
    </table>
</asp:Content>

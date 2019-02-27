<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserReg_Move3.aspx.cs" Inherits="UserReg_Move3" EnableViewState="false" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<%@ Register Src="WebUserControlUserInfoTop.ascx" TagName="WebUserControlUserInfoTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>个人信息管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class='active'>个人信息管理</li>
    </ol>
</div> 
<div class="container btn_green"> 
        <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
        <uc1:WebUserControlUserInfoTop ID="WebUserControlUserInfoTop1" runat="server" />
</div> 
<div class="container btn_green"> 
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td align="right" width="17%">身高：</td>
                <td width="60%">
                    <asp:TextBox ID="Staturetxt" runat="server" CssClass="form-control text_md" style="max-width:50px;" ReadOnly="true"></asp:TextBox>Cm
                </td>
                <td>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Staturetxt" ErrorMessage="请输入正确的身高信息" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></td>
            </tr>
            <tr>
                <td align="right">体重：</td>
                <td>
                    <asp:TextBox ID="Avoirtxt" runat="server" CssClass="form-control text_md" style="max-width:50px;"></asp:TextBox>Kg
                </td>
                <td>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="Avoirtxt" ErrorMessage="请输入正确的体重信息" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></td>
            </tr>
            <tr>
                <td align="right">婚姻状况：</td>
                <td>
                    <asp:DropDownList ID="ddlMarry" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">宗教信仰：</td>
                <td>
                    <asp:DropDownList ID="xinyang" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">外貌自评：</td>
                <td>
                    <asp:CheckBoxList ID="facey" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                        <asp:ListItem Value="潇洒大方" />
                        <asp:ListItem Value="高大威武" />
                        <asp:ListItem Value="温暖亲切" />
                        <asp:ListItem Value="成熟稳重" />
                        <asp:ListItem Value="浓眉大眼" />
                        <asp:ListItem Value="阳光运动" />
                        <asp:ListItem Value="文质彬彬" />
                        <asp:ListItem Value="风度翩翩" />
                        <asp:ListItem Value="朴实无华" />
                        <asp:ListItem Value="低调内敛" />
                        <asp:ListItem Value="酷似明星" />
                        <asp:ListItem Value="个性有品" />
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td align="right">月收入：</td>
                <td>
                    <asp:DropDownList ID="ddlMonth" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">住房条件：</td>
                <td>
                    <asp:DropDownList ID="ddlHome" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right" style="height: 24px">有没有孩子：</td>
                <td style="height: 24px">
                    <asp:DropDownList ID="ddlChild" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td style="height: 24px"></td>
            </tr>
            <tr>
                <td align="right">体型：</td>
                <td>
                    <asp:DropDownList ID="ddlSomato" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">脸型：</td>
                <td>
                    <asp:DropDownList ID="faceType" CssClass="form-control text_md" runat="server">
                        <asp:ListItem Value="圆脸" />
                        <asp:ListItem Value="方脸" />
                        <asp:ListItem Value="长脸" />
                        <asp:ListItem Value="娃娃脸" />
                        <asp:ListItem Value="鹅蛋脸" />
                        <asp:ListItem Value="瓜子脸" />
                        <asp:ListItem Value="三角脸" />
                        <asp:ListItem Value="菱形脸" />
                        <asp:ListItem Value="国字脸" />
                    </asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">发型：</td>
                <td>
                    <asp:DropDownList ID="hairType" CssClass="form-control text_md" runat="server">
                        <asp:ListItem Value="过肩长发" />
                        <asp:ListItem Value="中等长度" />
                        <asp:ListItem Value="短发" />
                        <asp:ListItem Value="自然卷" />
                        <asp:ListItem Value="光头" />
                        <asp:ListItem Value="其他" />
                    </asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">血型：</td>
                <td>
                    <asp:DropDownList ID="ddlBlood" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">学历：</td>
                <td>
                    <asp:DropDownList ID="ddlBachelor" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">毕业学校：</td>
                <td>
                    <asp:TextBox ID="Schooltxt" runat="server" CssClass="form-control text_md"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">所学专业：</td>
                <td>
                    <asp:DropDownList ID="Zhuan" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">所在行业：</td>
                <td>
                    <asp:DropDownList ID="Hang" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">工作状态：</td>
                <td>
                    <asp:DropDownList ID="JobStatus" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">事业前景：</td>
                <td>
                    <asp:DropDownList ID="JobFuture" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right" style="height: 22px">民族：</td>
                <td style="height: 22px">
                    <asp:DropDownList ID="Nationtxt" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td style="height: 22px"></td>
            </tr>
            <tr>
                <td align="right">家中排行：</td>
                <td>
                    <asp:DropDownList ID="BrotherDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right" style="height: 26px">语言能力：</td>
                <td style="height: 26px">
                    <asp:DropDownList ID="LanguageDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td style="height: 26px"></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:Button ID="nextButton" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="nextButton_Click" />
                </td>
            </tr>
        </table>
    </div>
	</div> 
</asp:Content>

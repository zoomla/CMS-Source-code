<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourseware.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.AddCourseware" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <title>添加试听文件</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="liCoures" runat="server" Visible="false"></asp:Literal>
    <table class="table table-striped table-bordered table-hover">
    <tr class="title text-center">
        <td style="width:25%">课件名称</td>
        <td style="width:10%">主讲人</td>
        <td style="width:10%">设计者</td>
        <td style="width:8%">课件次序</td>
        <td style="width:25%">试听地址</td>
        <td style="width:6%">试听</td>
        <td style="width:20%">操作</td>
    </tr>
    <ZL:ExRepeater ID="Repeater1" PageSize="10" runat="server" PagePre="<tr id='page_tr'><td colspan='7' id='page_td'>" PageEnd="</td></tr>">
        <ItemTemplate>
            <tr class="text-center">
                <td><%#Eval("Courseware")%></td>
                <td><%#Eval("Speaker")%></td>
                <td><%#Eval("SJName")%></td>
                <td>第<%#Eval("CoursNum")%>讲</td>
                <td><a href="<%#Eval("FileUrl")%>" target="_blank"><%#Eval("FileUrl")%></a></td>
                <td> <%#GetListon(Eval("Listen", "{0}"))%></td>
                <td>               
                    <a href="AddCourseware.aspx?CourseID=<%=Request.QueryString["CourseID"] %>&id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                    <a href="AddCourseware.aspx?CourseID=<%=Request.QueryString["CourseID"] %>&id=<%#Eval("ID")%>&menu=del" onclick="return confirm('确实要删除此课程?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
</table>
    <table class="table table-striped table-bordered table-hover">
        <tr><td colspan="2"> <asp:Label ID="Label1" runat="server" Text="添加课件"></asp:Label></td></tr>
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="ssjd_txt" runat="server" Text="课件主题："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Courename" runat="server" class=" form-control" style="width:200px"></asp:TextBox>&nbsp;<span style="color:red;">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="课件主题不能为空!" ControlToValidate="txt_Courename"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="Label3" runat="server" Text="主讲人："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="TextBox1" runat="server" class=" form-control" style="width:200px"></asp:TextBox>&nbsp;  
                     <asp:HiddenField ID="hfid" runat="server" />
                    <input type="button" value="选择主讲人 " class="btn btn-primary" style="width: 100px;" data-toggle="modal" data-target="#Teacher_div" onclick="Openwin(); void (0)" />
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="Label6" runat="server" Text="设计者："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="TextBox2" runat="server" class=" form-control" style="width:200px"></asp:TextBox>&nbsp;<span style="color:red;">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="设计者不能为空!" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="Label4" runat="server" Text="课件次序："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Order" runat="server" class=" form-control" style="width:100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="课件次序不能为空!" ControlToValidate="txt_Order"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="Label7" runat="server" Text="课件类型："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">外部课件</asp:ListItem>
                        <asp:ListItem Value="1">SCORM标准课件 </asp:ListItem>
                    </asp:RadioButtonList>                 
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="Label8" runat="server" Text="状态："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">可用</asp:ListItem>
                        <asp:ListItem Value="1">不可用 </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="Label5" runat="server" Text="可否试听："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                     <input type="checkbox" runat="server" id="rblHot" class="switchChk"/>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">

                    <asp:Label ID="Label9" runat="server" Text="简介："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtP_Content" runat="server" class=" form-control" TextMode="MultiLine" Height="152px" style="width:380px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" style="width: 20%">
                    <asp:Label ID="Label2" runat="server" Text="试听地址："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtUrl" runat="server" class=" form-control" style="width:306px"></asp:TextBox>
                    <input type="button" value="选择模板 " class="btn btn-primary" style="width: 100px;" onclick="WinOpenDialog('/manage/Template/TemplateList.aspx?OpenerText=' + escape('txtUrl') + '&FilesDir=', 650, 480)" />
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:HiddenField ID="coureId" runat="server" />
                <asp:HiddenField ID="cid" runat="server" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" runat="server" OnClick="EBtnSubmit_Click" />
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='CoureseManage.aspx';return false;" UseSubmitBehavior="False" CausesValidation="False" />
            </td>
        </tr>
    </table>
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        var typediag = new ZL_Dialog();
        function Openwin() {
            typediag.titile = "选择分类";
            typediag.url = "SelectTeacherName.aspx";
            typediag.ShowModal();
        }
    </script>
</asp:Content>
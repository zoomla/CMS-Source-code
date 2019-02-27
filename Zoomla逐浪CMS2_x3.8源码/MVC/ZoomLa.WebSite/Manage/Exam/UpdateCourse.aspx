<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateCourse.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.UpdateCourse" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>修改试听文件</title> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div>
        <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td class="spacingtitle">
                <asp:Label ID="Label1" runat="server" Text="修改课件"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tabs0">
            <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
               
                    <asp:Label ID="ssjd_txt" runat="server" Text="课件主题："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Courename" runat="server" class="l_input" Width="200px"  ></asp:TextBox>&nbsp;<font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="课件主题不能为空!" ControlToValidate="txt_Courename"></asp:RequiredFieldValidator>
                </td>
            </tr>             
               <tr>
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label3" runat="server" Text="主讲人："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="TextBox1" runat="server" class="l_input" Width="200px"></asp:TextBox>&nbsp;  
                     <asp:HiddenField ID="hfid" runat="server"  />
                    <input type="button" data-toggle="modal" data-target="#Teacher_div" value="选择主讲人 " class="btn btn-primary"  onclick="Openwin(); void (0)" />
                </td>
            </tr>   <tr >
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label6" runat="server" Text="设计者："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="TextBox2" runat="server" class="l_input" Width="200px"></asp:TextBox>&nbsp;<font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="设计者不能为空!" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>
                </td>
            </tr>   
            <tr >
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label4" runat="server" Text="课件次序："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Order" runat="server" class="l_input" Width="100px"></asp:TextBox>
                    <span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="课件次序不能为空!" ControlToValidate="txt_Order"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr >
                <td class="tdbgleft" style="width: 20%" align="right">                 
                    <asp:Label ID="Label7" runat="server" Text="课件类型："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">外部课件</asp:ListItem>
                        <asp:ListItem Value="1">SCORM标准课件 </asp:ListItem>
                    </asp:RadioButtonList><span>                    
                </td>
            </tr>   <tr >
                <td class="tdbgleft" style="width: 20%" align="right">
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
             <tr >
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label5" runat="server" Text="可否试听："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:RadioButtonList ID="rblHot" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr >
                <td class="tdbgleft" style="width: 20%" align="right">
               
                    <asp:Label ID="Label9" runat="server" Text="简介："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtP_Content" runat="server" class="l_input" TextMode="MultiLine" Height="152px" Width="380px"></asp:TextBox>
                </td>
            </tr> 
            <tr >
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label2" runat="server" Text="试听地址："></asp:Label>&nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txtUrl" runat="server" class="l_input"   Width="306px"></asp:TextBox>
                     <input type="button" value="选择模板 " class="btn btn-primary"   onclick="WinOpenDialog('/manage/Template/TemplateList.aspx?OpenerText=' + escape('txtUrl') + '&FilesDir=', 650, 480)"/>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
            <asp:HiddenField ID="coureId" runat="server" />
             <asp:HiddenField id="cid" runat="server"/>
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="修改" runat="server" onclick="EBtnSubmit_Click"/>
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" OnClientClick="location.href='CoureseManage.aspx';return false;" UseSubmitBehavior="False"  CausesValidation="False" />
            </td>
        </tr>
    </table>
    </div>
    <div class="modal" id="Teacher_div">
        <div class="modal-dialog" style="width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>选择分类</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="Teacher_ifr" style="width:100%;height:400px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Common.js" type="text/javascript"></script>
<script type="text/javascript" src="/JS/Dialog.js"></script>
   <script type="text/javascript">
       function Openwin() {
           $("#Teacher_ifr").attr("src", "SelectTeacherName.aspx");
       }
    </script>
</asp:Content>
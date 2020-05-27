<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddExamExroom.aspx.cs" EnableEventValidation="false" Inherits="manage_Question_AddExamExroom" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加组别</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label runat="server" ID="Literal1" Visible="false"></asp:Label>
    <asp:HiddenField runat="server" ID="hiden_Hid" />
    <table class="table table-striped table-bordered table-hover">
        <tr><td colspan="2"><asp:Label ID="Label1" runat="server" Text="添加考场"></asp:Label></td></tr>
        <tbody id="Tabs0">
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="ssjd_txt" runat="server" Text="考场名称："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_RoomName" runat="server" class="l_input" Width="200px"></asp:TextBox>
                    &nbsp;<font color="red">*</font>
                </td>
            </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="考试开始时间："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Starttime" runat="server" class="l_input" Width="160px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Ratednumber_name" runat="server" Text="考试结束时间："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_Endtime" runat="server" class="l_input" Width="160px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label12" runat="server" Text="模板试卷："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <div id="Exarinfo" name="Exarinfo"></div>
                    <asp:HiddenField ID="pageid" runat="server" />
                    <asp:Button ID="Button2" runat="server" Text="选择试卷" CssClass="btn btn-primary" data-toggle="modal" data-target="#paper_div" OnClientClick="open_Paper();return false;" />
                </td>
            </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" style="width: 20%" align="right">
                    <asp:Label ID="Label14" runat="server" Text="添加时间："></asp:Label>
                    &nbsp;
                </td>
                <td class="bqright">
                    <asp:TextBox ID="txt_AddTime" runat="server" class="l_input" Width="160px" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td class="tdbgleft" style="width: 20%" align="right">
                    <a href="javascript:void(0)" onclick="open_Studio()" data-toggle="modal" data-target="#studio_div" style="color: Red">添加考生</a>：<br />
                    (点击上面红色位置选择考生) <br />
                    <asp:Button ID="Button1" runat="server" Text="删除考生" CssClass="btn btn-primary" OnClientClick="deleteselectoption();return false;" CausesValidation="false" />
                </td>
                <td class="bqright">
                    <asp:ListBox ID="txt_Stuidlist" runat="server" Width="220px" Height="200px" 
                        CssClass="x_input" SelectionMode="Multiple">
                    </asp:ListBox>
                   
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加考场" runat="server" 
                    onclick="EBtnSubmit_Click" />
                &nbsp;
                <asp:Button ID="BtnBack" class="btn btn-primary" runat="server" Text="返回列表" UseSubmitBehavior="False"
                    CausesValidation="False" OnClick="BtnBack_Click" />
            </td>
        </tr>
    </table>
    <div class="modal" id="paper_div">
        <div class="modal-dialog" style="width: 760px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>选择试卷</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="paper_ifr" style="width:100%;height:500px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="studio_div">
        <div class="modal-dialog" style="width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>添加考生</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="studio_ifr" style="width:100%;height:400px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Common.js" ></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
    <script type="text/javascript">
        function openurls(url) {
            Dialog.open({ URL: url });
        }
        function open_Paper() {
            $("#paper_ifr").attr("src", "AddPaper.aspx");
        }
        function open_Studio() {
            $("#studio_ifr").attr("src", "SelectStudio.aspx");
        }

        function opentitle(url, title) {
            var diag = new Dialog();
            diag.Width = 800;
            diag.Height = 600;
            diag.Title = title;
            diag.URL = url;
            diag.show();
        }

        function closdlg() {
            Dialog.close();
        }


        function deleteselectoption() {
            if (document.getElementById("txt_Stuidlist")) {
                if (document.getElementById("txt_Stuidlist").options.length > 0) {
                    for (var i = 0; i < document.getElementById("txt_Stuidlist").options.length; i++) {
                        if (document.getElementById("txt_Stuidlist").options[i].selected) {
                            var index = document.getElementById("txt_Stuidlist").options[i].selectedIndex;
                            document.getElementById("txt_Stuidlist").options.remove(index);
                        }
                    }
                }
            }
        }
        function deletepage() {
            var mainright = window.top.main_right;
            var txt_Exarinfo = document.getElementById("Exarinfo");
            var pageidtxt = document.getElementById("pageid");
            pageidtxt.value = "";
            txt_Exarinfo.innerHTML = "";
        }
    </script>
</asp:Content>
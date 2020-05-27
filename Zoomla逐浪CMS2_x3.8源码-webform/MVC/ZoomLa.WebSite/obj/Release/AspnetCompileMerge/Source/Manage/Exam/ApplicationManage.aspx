<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.ApplicationManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>教师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr class="title text-center">
            <td style="width: 5%"></td>
            <td style="width: 10%" class="title">ID
            </td>
            <td style="width: 10%" class="title">招生人员姓名
            </td>
            <td style="width: 10%" class="title">加入时间
            </td>
            <td style="width: 10%" class="title">学员人数
            </td>
            <td style="width: 15%" class="title">联系电话
            </td>
            <td style="width: 15%" class="title">操作
            </td>
        </tr>
        <ZL:ExRepeater ID="Repeater1" runat="server" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='6' id='page_td'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr>
                    <td class="text-center">
                        <input name="idchk" type="checkbox" value='<%#Eval("id") %>' />
                    </td>
                    <td class="text-center">
                        <%#Eval("id", "{0}")%>
                    </td>
                    <td class="text-center">
                        <%#GetUserName(Eval("UserID","{0}"))%>
                    </td>
                    <td class="text-center">
                        <%#Eval("RegTime")%>
                    </td>
                    <td class="text-center">
                        <%#getusercount(Eval("id", "{0}"))%>
                    </td>
                    <td class="text-center">
                        <%#Eval("OfficePhone")%>
                    </td>
                    <td class="text-center">
                        <a href="?menu=delete&id=<%#Eval("id")%>" onclick="return confirm('确实要删除此教师?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a>
                        <a href="StudioInfoListByTech.aspx?id=<%#Eval("id")%>" class="option_style"><i class="fa fa-magic" title="管理"></i>学员资料管理</a>
                    </td>
                </tr>
            </ItemTemplate>
        </ZL:ExRepeater>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" Style="width: 110px;" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"
            Text="批量删除" OnClick="Button3_Click" />
    </div>
    <div class="modal" id="TechUser_div">
        <div class="modal-dialog" style="width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>导入招生资料</strong></span>
                </div>
                <div class="modal-body">
                    <iframe id="TechUser_ifr" style="width: 100%; height: 400px; border: none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
    <script type="text/javascript">
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }

        function openurls(url) {
            Dialog.open({ URL: url });
        }
        function open_window() {
            $("#TechUser_ifr").attr("src", "InputTechUser.aspx");
        }
    </script>
</asp:Content>

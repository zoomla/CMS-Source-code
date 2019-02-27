<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudioInfoListByTech.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.StudioInfoListByTech" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>教师管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table  class="table table-striped table-bordered table-hover">
        <tr class="text-center">
            <td style="width:5%">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
            </td>
            <td style="width:10%">
                ID
            </td>
            <td style="width:10%">
                学员姓名
            </td>
            <td style="width:10%">
                预设用户名
            </td>
            <td style="width:10%">
                联系电话
            </td>
            <td style="width:15%">
                登记时间
            </td>
            <td style="width:15%">
                操作
            </td>
        </tr>
        <ZL:ExRepeater ID="Rec_RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='12' class='text-center'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr>
                    <td><input name="idchk" type="checkbox" value='<%#Eval("ssid") %>' /></td>
                    <td><%#Eval("ssid", "{0}")%></td>
                    <td><%#Eval("Studioname", "{0}")%></td>
                    <td><%#Eval("PriorUserName")%></td>
                    <td><%#Eval("Tel")%></td>
                    <td><%#Eval("WriteTime")%></td>
                    <td>
                        <a href="AddStudioInfo.aspx?stuid=<%#Eval("ssid")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                        <a href="?menu=delete&id=<%#Eval("ssid")%>" onclick="return confirm('确实要删除此信息?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </ZL:ExRepeater>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" Style="width: 110px;" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"
            Text="批量删除" OnClick="Button3_Click" />
            <asp:Button ID="Button1" class="btn btn-primary" Style="width: 110px;" runat="server"
            Text="导出Excel" OnClick="Button1_Click" />
            </div>
    <div class="modal" id="InputUser_div">
        <div class="modal-dialog" style="width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong>导入招生资料</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="InputUser_ifr" style="width:100%;height:400px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
     <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
    <script>
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
            $("#InputUser_ifr").attr("src", "InputUser.aspx?Tid=<%=Request.QueryString["id"] %>");
        }
    </script>
</asp:Content>
    
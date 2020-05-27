<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamExroom.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.ExamExroom" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加组别</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <table class="table table-striped table-bordered table-hover">
        <tr align="center" class="title">
            <td width="2%">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
            </td>
            <td width="10%">
                ID
            </td>
            <td width="10%">
                考场名称
            </td>
            <td width="10%">
                考试开始时间
            </td>
            <td width="10%">
                考试结束时间
            </td>
            <td width="10%">
                试卷模板
            </td>
            <td width="10%">
                添加时间
            </td>
            <td width="10%">
                考生人数
            </td>
            <td width="6%" class="title">
                操作
            </td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='9' class='text-center'><input type='checkbox' id='CheckAll'" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr id="<%#Eval("ExrID") %>" ondblclick="ShowTabs(this.id)">
                    <td height="22" align="center">
                        <input name="Item" type="checkbox" value='<%#Eval("ExrID") %>' />
                    </td>
                    <td height="22" align="center">
                        <%#Eval("ExrID")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("RoomName")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Starttime")%>
                    </td>
                     <td height="22" align="center">
                        <%#Eval("Endtime")%>
                    </td>
                     <td height="22" align="center">
                        <%#GetExaName(Eval("ExaID","{0}"))%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("AddTime")%>
                    </td>
                    <td height="22" align="center">
                        <%#GetStuidoNum(Eval("Stuidlist","{0}"))%>
                    </td>
                    <td height="22" align="center">
                        <a href="AddExamExroom.aspx?cid=<%#Eval("ExrID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                         <a href="?menu=delete&cid=<%#Eval("ExrID")%>" OnClick="return confirm('确定要删除此学员?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"
            Text="批量删除" onclick="Button3_Click" /></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
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
    </script>
</asp:Content>
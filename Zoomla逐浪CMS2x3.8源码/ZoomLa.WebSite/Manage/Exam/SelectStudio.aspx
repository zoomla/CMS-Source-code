<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectStudio.aspx.cs" Inherits="manage_Question_SelectStudio" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>选择学员</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table class="table table-striped table-bordered table-hover">
        <tr align="center" class="title">
            <td width="2%">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
            </td>
            <td width="10%">
                学员ID
            </td>
            <td width="10%">
                学员姓名
            </td>
            <td width="10%">
                所在组别
            </td>
            <td width="10%">
                是否合格
            </td>
            <td width="10%">
                到期时间
            </td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='6' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr id="<%#Eval("Stuid") %>" ondblclick="ShowTabs(this.id)">
                    <td height="22" align="center">
                        <input name="Item" type="checkbox" value='<%#Eval("Stuid") %>' />
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Stuid")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Stuname")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Stugroup")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Qualified")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("Exptime")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" Text="选择学员"
            OnClick="Button3_Click" /></div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
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

        function clickext(txt) {
            var mainright = window.top.main_right;
            var selectname = txt;
            var selectoption = mainright.frames["main_right_" + parent.nowWindow].document.getElementById("txt_Stuidlist");
            if (selectname.indexOf('||') > 0) {
                var selectname = "";
                var namearr = selectname.split('||');
                for (var i = 0; i < namearr.length; i++) {
                    var arrstr = namearr[i].toString();
                    if (arrstr.indexOf('|') > -1) {
                        var strarrlist = arrstr.split('|');
                        var oOption = mainright.frames["main_right_" + parent.nowWindow].document.createElement("option");
                        oOption.text = strarrlist[1];
                        oOption.value = strarrlist[0];
                        var isd = 0;
                        var iid = 0;
                        for (var ii = 0; ii < selectoption.options.length; ii++) {
                            iid = iid + 1;
                            var optiont = selectoption.options[ii];
                            if (optiont.text != strarrlist[1] && optiont.value != strarrlist[0]) {
                                isd = isd + 1;
                            }
                        }
                        if (isd == iid) {
                            oOption.selected = true;
                            selectoption.add(oOption);
                        }
                    }
                }
            }
            else {
                if (selectname.indexOf('|') > -1) {
                    var strarrlist = selectname.split('|');
                    var oOption = mainright.frames["main_right_" + parent.nowWindow].document.createElement("option");
                    oOption.text = strarrlist[1];
                    oOption.value = strarrlist[0];
                    var isd = 0;
                    var iid = 0;
                    for (var ii = 0; ii < selectoption.options.length; ii++) {
                        iid = iid + 1;
                        var optiont = selectoption.options[ii];
                        if (optiont.text != strarrlist[1] && optiont.value != strarrlist[0]) {
                            isd = isd + 1;
                        }
                    }
                    if (isd == iid) {
                        oOption.selected = true;
                        selectoption.add(oOption);

                    }
                }
            }
            parent.Dialog.close();
        }
</script>
</asp:Content>
            
   
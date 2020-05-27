<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Addproject.aspx.cs" Inherits="manage_Shop_Addproject" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>选择商品</title>
    <link type="text/css" rel="stylesheet" href="/App_Themes/V3.css" />
    <script language="javascript" type="text/javascript">
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

        function GetCheckvalue() {
            var spanChk = window.document.getElementById("CheckBox1");
            var elm = document.form1.elements;
            var listbox = "";
            var listheight = 0;
            var listnum = 0;
            var boxlist = 0;
            var innterhtml = "";
            var hiddenidvalue = opener.document.getElementById("PromoProlist"); //获取已经存在的ID值

            //循环本页选择的项目
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != spanChk.id) {
                    boxlist = boxlist + 1;
                    if (elm[i].checked == true) {
                        var tempvalue = "," + elm[i].value + ",";//初始ID
                        listheight = listheight + 1;
                        var Pronamesvalue = document.getElementById("Pronames" + elm[i].value).value;
                        //循环父页的Option值;
                        var addthis = true;
                        if (hiddenidvalue.options.length > 0) {
                            for (var ii = hiddenidvalue.options.length - 1; ii >= 0; ii--) {
                                if (hiddenidvalue[ii].text == document.getElementById("Pronames" + elm[i].value).value) {
                                    addthis = addthis && false;
                                }
                            }

                            if (addthis == true) {
                                var oOption = opener.document.createElement("option");
                                oOption.text = Pronamesvalue;
                                oOption.value = elm[i].value;
                                opener.document.getElementById("PromoProlist").add(oOption);
                            }
                        }
                        else {
                            var oOption = opener.document.createElement("option");
                            oOption.text = Pronamesvalue;
                            oOption.value = elm[i].value;
                            opener.document.getElementById("PromoProlist").add(oOption);
                        }
                    }
                }
            }

            for (var ii = hiddenidvalue.options.length - 1; ii >= 0; ii--) {
                opener.document.form1.PromoProlist.options[ii].selected = true;
            }
            window.close();
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table width="100%" border="0" cellpadding="2" cellspacing="0" class="border" id="TABLE1">
            <tr class="title" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                <td align="left">
                    <b>礼品列表：</b></td>
                <td align="right">
                    <div class="form-group col-lg-8 col-md-8 col-sm-8">
                        <asp:TextBox ID="TxtKeyWord" runat="server" class="form-control">
                        </asp:TextBox>
                    </div>
                    <div class="form-group col-lg-4 col-md-4 col-sm-4">
                        <asp:Button ID="BtnSearch" class="btn btn-primary" runat="server" Text="查找" OnClick="BtnSearch_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <table width="100%" border="0" cellspacing="1" cellpadding="1">
                        <tr class="tdbgleft">
                            <td width="5%" height="24" align="center"><strong>ID</strong></td>
                            <td width="5%" height="24" align="center">
                                <asp:CheckBox ID="CheckBox1" name="CheckBox1" runat="server" onclick="CheckAll(this);" /></td>
                            <td width="10%" height="24" align="center"><strong>礼品图片</strong></td>
                            <td width="40%" height="24" align="center"><strong>礼品名称</strong></td>
                            <td width="35%" height="24" align="center"><strong>礼品零售价</strong></td>
                        </tr>
                        <ZL:ExRepeater ID="Pagetable" runat="server" PagePre="<tr><td colspan='5' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
                            <ItemTemplate>
                                <tr class="tdbg">
                                    <td height="24" align="center"><%#Eval("ID") %></td>
                                    <td height="24" align="center">
                                        <input name="Item" id="Item<%#Eval("ID") %>" type="checkbox" value="<%#Eval("ID") %>"></td>
                                    <td height="24" align="center"><%#getproimg(DataBinder.Eval(Container,"DataItem.Thumbnails","{0}"))%></td>
                                    <td height="24" align="center"><%#Eval("Proname") %><input type="hidden" id="Pronames<%#Eval("ID") %>" value="<%#Eval("Proname") %>" />
                                    </td>
                                    <td height="24" align="center"><%#Eval("LinPrice","{0:c}")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </ZL:ExRepeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="添加礼品" OnClientClick="GetCheckvalue();return false;" />
                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="取消添加" OnClientClick="window.close();return false;" /></td>
            </tr>
        </table>
</asp:Content>

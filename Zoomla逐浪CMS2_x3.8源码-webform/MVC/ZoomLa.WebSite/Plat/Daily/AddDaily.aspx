<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDaily.aspx.cs" Inherits="ZoomLaCMS.Plat.Daily.AddDaily" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>添加日志</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />  
<script type="text/javascript" src="/js/ajaxrequest.js"></script>
<script type="text/javascript" src="../../JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/MisView.js"></script> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
<div>
    <table class="border staff_frm" width="100%" >
        <tr height="25" class="title" style="background-image: none; background-attachment: scroll; background-repeat: repeat; background-position-x: 0%; background-position-y: 0%; background-color: rgb(231, 243, 255);">

            <th width="100"></th>
            <th>详细内容</th>
            <th width="180">编辑时间</th> 
        </tr>
        <tr><td width="100" align="center" valign="top">今日计划 </td>
            <td id="log_box" colspan="2">
                <div id="mylog_title_div" ><a href="javascript:void(0);"  onclick="ShowHidDiv('mylog_title_div','edit_middel_div')"> >>工作记录</a></div>
                <div id="edit_middel_div" runat="server" style="display:none;"><textarea id="log_1" name="log_1" cols="60" rows="6" runat="server"></textarea><br />
                    日期选择：<asp:TextBox ID="TextDate" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" class="M_input" runat="server" />
                    <asp:Button ID="Button1" CssClass="btn btn-primary" OnClick="Button1_Click" runat="server" Text="确定" />
                    <input id="btn1Cln" type="button" onclick="ShowHidDiv('mylog_title_div', 'edit_middel_div')" value="取消" />
                </div>
                <div id="dayList">
                    <ul>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <li>
                                    <span><%#Eval("CreateTime","{0:MM-dd hh:mm}") %></span>
                                    <a href="<%#Eval("ID") %>"><%#Eval("Title") %><%#Eval("Content") %></a>
                                    <div><asp:LinkButton ID="lbtSet" CommandArgument='<%#Eval("ID") %>' CommandName="SetSta" Text="设置完成" runat="server" ></asp:LinkButton>  | <a href="javascript:void(0)" onclick="ShowDiv('Dayshow_<%#Eval("ID") %>')" >编辑</a> | <asp:LinkButton ID="lbtDel" CommandArgument='<%#Eval("ID") %>' CommandName="SetDel" Text="删除" runat="server" ></asp:LinkButton> | <a href="javascript:void(0)" onclick="ShowDiv('DayChang_<%#Eval("ID") %>')" >延期到</a>
        <%--<asp:LinkButton ID="lbtDate" CommandArgument='<%#Eval("ID") %>' CommandName="SetDate" Text="延期到" runat="server" ></asp:LinkButton>--%>
                                    </div>
                                </li>
                                <li id="Dayshow_<%#Eval("ID") %>" style="display:none;"  class="bgyellow">
                                    <textarea id='logEdit_<%#Eval("ID") %>' name='logEdit_<%#Eval("ID") %>' cols="60" rows="6" ><%#Eval("Content") %></textarea>
                                    <asp:Button ID="btnEdDay" CssClass="btn btn-primary" CommandArgument='<%#Eval("ID") %>' CommandName="Edit"   runat="server" Text="确定" />
                                    <input id="Button5" type="button" onclick="ShowDiv('Dayshow_<%#Eval("ID") %>')"  value="取消" />
                                </li>
                                <li id="DayChang_<%#Eval("ID") %>" style="display:none;"  class="bgyellow">
                                    <input id="edtDate_<%#Eval("ID") %>"  name='edtDate_<%#Eval("ID") %>'  value="<%#Eval("createtime") %>"   onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" class="M_input"  />
                                    <asp:Button ID="Button4" CssClass="btn btn-primary" CommandArgument='<%#Eval("ID") %>' CommandName="EditDate"   runat="server" Text="确定" />
                                    <input id="Button6" type="button" onclick="ShowDiv('DayChang_<%#Eval("ID") %>')" value="取消" />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </td>
        </tr>
        <tr><td align="center" valign="top">工作小结 </td>
            <td id="end_box"  colspan="2">
                <div id="jtit"><a href="javascript:void(0);"  onclick="ShowHidDiv('jtit','Div1')">  >>工作小结  </a></div>
                <div id="Div1"  style="display:none;">
                    <textarea  id="jie_1" name="jie_1"  cols="60" rows="6"></textarea><br />
                    <asp:Button ID="Button2" CssClass="btn btn-primary" OnClick="Button2_Click" runat="server" Text="确定" />
                    <input id="Button5" type="button" onclick="ShowHidDiv('jtit', 'Div1')" value="取消" />
        </div>
        <div id="DaiJie">
        <ul>
        <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater1_ItemCommand">
        <ItemTemplate> 
        <li><span><%#Eval("CreateTime","{0:MM-dd hh:mm}") %>  </span><a href="<%#Eval("ID") %>"><%#Eval("Title") %><%#Eval("Content") %></a> <div><asp:LinkButton ID="lbtSet" CommandArgument='<%#Eval("ID") %>' CommandName="SetSta" Text="设置完成" runat="server" ></asp:LinkButton>  | <a href="javascript:void(0)" onclick="ShowDiv('Dayshow_<%#Eval("ID") %>')" >编辑</a> | <asp:LinkButton ID="lbtDel" CommandArgument='<%#Eval("ID") %>' CommandName="SetDel" Text="删除" runat="server" ></asp:LinkButton> |<a href="javascript:void(0)" onclick="ShowDiv('DayChang_<%#Eval("ID") %>')" >延期到</a>
        <%--<asp:LinkButton ID="lbtDate" CommandArgument='<%#Eval("ID") %>' CommandName="SetDate" Text="延期到" runat="server" ></asp:LinkButton> --%>
        </div>    </li>
        <li id='Dayshow_<%#Eval("ID") %>' style="display:none;">
        <textarea id='logEdit_<%#Eval("ID") %>' name='logEdit_<%#Eval("ID") %>' cols="60" rows="6" ><%#Eval("Content") %></textarea><br />
        <asp:Button ID="btnEdDay" CssClass="C_inputs btn btn-primary" CommandArgument='<%#Eval("ID") %>' CommandName="Edit"   runat="server" Text="确定" />
        <input id="Button5" type="button" class="C_inputs btn-primary" onclick="ShowDiv('Dayshow_<%#Eval("ID") %>')"  value="取消" />
        </li>
        <li id='DayChang_<%#Eval("ID") %>' style="display:none;">
        <input id='edtDate_<%#Eval("ID") %>' name='edtDate_<%#Eval("ID") %>'  value="<%#Eval("createtime") %>"  onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" class="M_input"  />
        <asp:Button ID="Button4" CssClass="C_inputs btn btn-primary" CommandArgument='<%#Eval("ID") %>' CommandName="EditDate"   runat="server" Text="确定" />
        <input id="Button6" type="button" class="C_inputs btn-primary" onclick="ShowDiv('DayChang_<%#Eval("ID") %>" value="取消" />
        </li>
        </ItemTemplate>
        </asp:Repeater>
        </ul></div></td></tr>
        <tr><td align="center" valign="top"> 附件 </td><td  colspan="2">
        <table><tr><td><a href="javascript:void(0)" onclick="additem('filesDiv')"> >>添加附件 </a>
        <textarea id="Textfile" cols="100" rows="6" style="display:none;"></textarea></td></tr></table>
        <table><tr><td>
        <ul class="lsist-unstyled">
        <asp:Repeater ID="Repeater3" runat="server" OnItemCommand="Repeater1_ItemCommand">
        <ItemTemplate> 
        <li><span><%#Eval("CreateTime","{0:MM-dd hh:mm}") %>  </span><a href="/UploadFiles/<%#Eval("Content") %>"><%#Eval("Title") %><%#Eval("Content") %></a> 
        <div> 
             <asp:LinkButton ID="lbtDel" CommandArgument='<%#Eval("ID") %>' CommandName="SetDel" Text="删除" runat="server" ></asp:LinkButton> 
        <%--<asp:LinkButton ID="lbtDate" CommandArgument='<%#Eval("ID") %>' CommandName="SetDate" Text="延期到" runat="server" ></asp:LinkButton> --%>
        </div>
        </li>
        </ItemTemplate></asp:Repeater>
        </ul>
        </td></tr>
        </table>
        <table id="filesDiv"></table>
        <asp:Button ID="Button3" OnClick="Button3_Click" CssClass="btn btn-primary" runat="server" Text="确定" />
        </td></tr>
    </table>      
</div>
<asp:HiddenField runat="server" ID="hfNumber" Value="" /> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    var ajax = new AJAXRequest();
    var count = 0, number = 0;
    var save = "";

    function deleteItem(obj, string) {
        //alert("a");
        if (number >= 1) {
            number = number - 1;
            var curRow = obj.parentNode.parentNode;
            var tb3 = document.getElementById("filesDiv");
            var i;
            string = string + ",";
            i = save.indexOf(string);
            saveh1 = save.substr(0, i);
            saveh2 = save.substr(i + 2, save.length - i - 2);
            save = saveh1 + saveh2;
            tb3.deleteRow(curRow.rowIndex);
            document.getElementById("hfNumber").value = save;
        }
    }
    var str = ""; 
    function additem(id) {
        str =  '<input type="file" name="File" style="width: 300px" runat="server"/>';
        var row, cell, str;
        var tab1 = document.getElementById(id); 
        row = tab1.insertRow(number);
        if (row != null) {
            row.insertCell(0).innerHTML = "<td>"+str+" <a href=\"javascript:void(0)\" class=\"button\"   onclick=\'deleteItem(this," + count + ");\'>删除</a></td>";
            save = save + count + ",";
            count++;
            number++;
        }
        document.getElementById("hfNumber").value = save; 
    } 
</script>
</asp:Content>
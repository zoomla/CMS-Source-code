<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertLabel.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" EnableViewStateMac="false" Inherits="ZoomLa.WebSite.Manage.Template.InsertLabel" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>动态标签编辑器-<%:LName%></title>
<link href="/App_Themes/V3.css" rel="stylesheet" />
<style>
    .ilabel_input{width:70px;}
    .label_diag_width{width:350px;}
    .ilabel span{font-size:14px;}
    #form1 table td{border-top:0px;}
    .model_body{overflow:auto;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table ilabel boder0">
    <tr>
        <td>
            <div>
                <div>
                    <asp:Label ID="labelintro" Visible="false" runat="server">无标签说明</asp:Label>
                </div>
                <div>
                    <asp:Label ID="labelbody" runat="server"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
            
</table>
<script type="text/javascript">
    function submitdate(){
        var lbltype="<%= LabelType %>";
        var returnstr;
        if(lbltype=="1")
            returnstr = "{ZL.Source id=\"<%:LName %>\"";
        else if(lbltype=="21")//扩展日期格式转换，不需要返回值
            returnstr = "{ZL:ConverToWeek(时间为空自动输出当前星期)";
        else
            returnstr = "{ZL.Label id=\"<%:LName %>\"";
    var oSpanArr = document.getElementsByTagName('SPAN');
    for (var i = 0; i < oSpanArr.length; i++) {
        if (oSpanArr[i].getAttribute("stype") == "0") {
            var pvalue = document.getElementById(oSpanArr[i].getAttribute("sid")).value;
            if(pvalue != "" && i > 0)
                returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"" + pvalue + "\"";
        }
        if (oSpanArr[i].getAttribute("stype") == "1") {
            var txt = $("#h" + oSpanArr[i].getAttribute("sid")).val();
            returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"" + txt+ "\"";   
        }
    }
    returnstr= returnstr + " /}";
    parent.PasteValue(returnstr);     
    parent.closeCuModal();
}
    function selectchecked(objid) {
        var hid = objid.name;
        var hidid = hid.slice(1, hid.length);
        var hidobj = $("#h" + hidid);
        if ($(objid).attr("checked")) {
            if (hidobj.val().length == 0) {
                hidobj.val($(objid).val());
            } else {
                var htxt = hidobj.val();
                htxt += "$" + $(objid).val();
                hidobj.val(htxt);
            } 
        } else {
            var hgtxt = hidobj.val();
            var hgsub = hgtxt.replace("$" + $(objid).val() + "", "").replace(""+$(objid).val() + "$", "").replace("" + $(objid).val() + "", "");
            hidobj.val(hgsub);
        }
    }
    $().ready(function(){
        $(":input[type=text]").addClass("form-control");
        $(":input[type=text]").addClass("ilabel_input");
        parent.setdiagTitle("标签名称："+$("#LabelName").text());
    })
</script>
</asp:Content>

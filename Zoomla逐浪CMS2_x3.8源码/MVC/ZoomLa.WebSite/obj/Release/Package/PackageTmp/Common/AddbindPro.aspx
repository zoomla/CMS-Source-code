<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddbindPro.aspx.cs" Inherits="ZoomLaCMS.Common.AddbindPro" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/App_Themes/V3.css" rel="stylesheet" />
<style>.table{margin-top:5px;margin-bottom:40px;}</style>
<title>选择商品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar" style="position:fixed;top:0px;width:100%;">
        <div class="input-group text_400 pull-left">
            <asp:TextBox ID="Skey_T" class="form-control text_400" runat="server" placeholder="商品名称"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="Skey_Btn" runat="server" class="btn btn-info" Text="查找" OnClick="Skey_Btn_Click" />
            </span>
        </div>
        <div class="pull-left margin_l5">
            <asp:Button runat="server" ID="AddIDC_Btn" Text="选定商品" OnClick="AddIDC_Btn_Click" OnClientClick="return subchk();" CssClass="btn btn-info" Visible="false" />
            <input runat="server" id="AddPro_Btn" type="button" onclick="GetCheckvalue();" class="btn btn-info" value="选定商品" />
        </div>
        <div class="clearfix"></div>
    </div>
    <table class="table table-striped table-bordered table-hover" style="margin-top:50px;">
        <tr class="tdbgleft">
            <td></td>
            <td>ID</td>
            <td class="text-center"><strong>图片</strong></td>
            <td class="text-center"><strong>名称</strong></td>
            <td class="text-center td_md"><strong>数量</strong></td>
            <td class="text-center"><strong>零售价</strong> </td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" PageSize="20" PagePre="<tr><td><input type='checkbox' id='chkAll' onclick='selectAllByName(this);'/></td><td colspan='6'><div class='text-center'>" PageEnd="</div></td></tr>">
            <ItemTemplate>
                <tr>
                    <td class="td_xs"><input name="idchk" type="checkbox" value="<%#Eval("ID") %>"></td>
                    <td class="td_xs"><%#Eval("ID") %></td>
                    <td class="td_s td_thumbnails" data-thumbnails="<%#Eval("Thumbnails") %>"><%#getproimg(DataBinder.Eval(Container,"DataItem.Thumbnails","{0}"))%></td>
                    <td class="td_proname" data-proname="<%#Eval("Proname") %>"><%#Eval("Proname") %></td>
                    <td><input name="pronum<%#Eval("ID") %>" type="text" class="form-control procount" value="1" id="pronum<%#Eval("ID") %>"/></td>
                    <td class="td_linprice td_m" data-linprice="<%#Eval("LinPrice","{0:c}")%>"><%#Eval("LinPrice","{0:c}")%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <script src="/JS/SelectCheckBox.js"></script>
    <script>
        //将选中的值生成为Json
        function GetCheckvalue() {
            var chkArr = $("input[name=idchk]:checked");
            var valueAttr = [];
            for (var i = 0; i < chkArr.length; i++) {
                var $trobj = $(chkArr[i]).closest('tr');
                valueAttr.push({ id: $(chkArr[i]).val(), Thumbnails: $trobj.find(".td_thumbnails").attr("data-Thumbnails"), Proname: $trobj.find(".td_proname").attr("data-Proname"), LinPrice: $trobj.find(".td_linprice").attr("data-LinPrice"), pronum: $trobj.find("#pronum" + $(chkArr[i]).val()).val() });

            }
            parent.BindPro(JSON.stringify(valueAttr));
            parent.CloseDiag();
        }
        function subchk() {
            if ($("input[name=idchk]:checked").length < 1) { alert("请选择需要添加的商品"); return false; }
            else { return true; }
        }
    </script>
</asp:Content>

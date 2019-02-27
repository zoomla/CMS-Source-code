<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelIDCPro.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.IDC.SelIDCPro" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选择IDC商品</title><style>#chkAll{display:none;}</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar" style="position:fixed;top:0px;width:100%;">
        <div class="input-group text_405 pull-left ">
            <asp:TextBox ID="Skey_T" class="form-control text_400" runat="server" placeholder="商品名称"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="Skey_Btn" runat="server" class="btn btn-info" Text="查找" OnClick="Skey_Btn_Click" />
            </span>
        </div>
        <div class="pull-left margin_l5">
            <input runat="server" id="AddPro_Btn" type="button" onclick="GetCheckvalue();" class="btn btn-info" value="选定商品" />
        </div>
        <div class="clearfix"></div>
    </div>
    <table class="table table-striped table-bordered table-hover text-center" style="margin-top:50px;">
        <tr>
            <td></td>
            <td>ID</td>
            <td class="text-center"><strong>名称</strong></td>
            <td class="text-center"><strong>图片</strong></td>
            <td class="text-center td_md"><strong>时长</strong></td>
        </tr>
        <ZL:ExRepeater ID="RPT" runat="server" PageSize="20" PagePre="<tr><td><input type='checkbox' id='chkAll' onclick='selectAllByName(this);'/></td><td colspan='6'><div class='text-center'>" PageEnd="</div></td></tr>">
            <ItemTemplate>
                <tr>
                    <td class="td_s"><input name="idrad" type="radio" value="<%#Eval("ID") %>" /></td>
                    <td class="td_s"><%#Eval("ID") %></td>
                    <td class="td_proname"><%#Eval("Proname") %></td>
                    <td class="td_thumbnails" data-thumbnails="<%#Eval("Thumbnails") %>"><%#getproimg(DataBinder.Eval(Container,"DataItem.Thumbnails","{0}"))%></td>
                    <td><%#GetIDCPrice(Eval("IDCPrice").ToString()) %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/SelectCheckBox.js"></script>
    <script>
        //将选中的值生成为Json
        function GetCheckvalue() {
            var id = $("input[name=idrad]:checked").val();
            if (!id || id == "") { alert("请选择需要添加的商品"); return false; }
            var $tr = $("input[name=idrad]:checked").closest('tr');
            var $op = $tr.find(".idcprice").find("option:selected");
            var model = { "id": id, Thumbnails: $tr.find(".td_thumbnails").attr("data-Thumbnails"), Proname: $tr.find(".td_proname").text() };
            model.time = $op.data("time");
            model.name = $op.text();
            model.price = $op.val();
            parent.BindPro(model);
            parent.CloseDiag();
        }
    </script>
</asp:Content>

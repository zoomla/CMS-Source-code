<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MtrlsMrktng.aspx.cs" Inherits="manage_Shop_MtrlsMrktng" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
       <title>商品推广</title>   
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" 
             ForeColor="Black" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" GridLines="None"  HeaderStyle-Height="28px"  >
            <RowStyle CssClass="tdbg" Height="26px"></RowStyle>
            <Columns>               
                <asp:TemplateField HeaderText="推广人" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbUserID" runat="server"><%#getUserNameById(ZoomLa.Common.DataConverter.CLng(Eval("UserID").ToString()))%></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="总额" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblNum" runat="server" Text="" ><%#Eval("Money")%></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="返利比例" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblgu" runat="server" Text=""><%#(Eval("Scale"))%></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="返利金额" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text=""><%#Eval("BalanceMoney")%></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <%--<asp:TemplateField HeaderText="相关操作" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>                       
                        <a href="?menu=delete&id=<%#Eval("ID")%>" onclick="return confirm('确实要删除此推广吗？');">删除</a>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </ZL:ExGridView>
    </div>
    <div class="tdbg" align="center">
        <%=getUserNameById(ZoomLa.Common.DataConverter.CLng(Request.QueryString["id"]))%>的返利余额为:<asp:Label runat="server" ID="MoneyID" Text=""></asp:Label>
    </div>
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
        function getinfo(id) {
            location.href = "Stock.aspx?menu=edit&id=" + id + "";
        }
    </script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APPList.aspx.cs" Inherits="App_Default" MasterPageFile="~/Common/Master/Commenu.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>我的APP</title>
</asp:Content>
<asp:content runat="server" contentplaceholderid="Content">
<div style="margin-top:50px;">
<asp:GridView runat="server" ID="EGV" OnRowCommand="EGV_RowCommand"
    CssClass="table table-striped table-bordered" EmptyDataText="尚无APP数据" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="名称" DataField="APPName" />
          <asp:TemplateField HeaderText="状态">
            <ItemTemplate>
              <%#GetTypeStr() %>  
            </ItemTemplate>
        </asp:TemplateField>
  <%--      <asp:BoundField HeaderText="大小" DataField="ExSize" />--%>
        <asp:TemplateField HeaderText="类型"><ItemTemplate>Android APP</ItemTemplate></asp:TemplateField>
        <asp:TemplateField HeaderText="起始Url">
            <ItemTemplate>
                <a href="<%#UrlDeal() %>" target="_blank"><%#Eval("FUrl") %></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="二维码管理">
            <ItemTemplate>
            <%#GetCodeImg() %>
            </ItemTemplate>
            <ItemStyle CssClass="text-center" />
        </asp:TemplateField>
        <asp:BoundField HeaderText="创建时间" DataField="AddTime" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton Visible='<%#Eval("MyStatus","").Equals("1") %>' runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="down">下载</asp:LinkButton>
                <a href="CL.aspx?appid=<%#Eval("ID") %>">二维码管理</a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    </div>
    <div style="display:none;" id="preimg_div">
        <div class="text-center">
        <img id="precode_img" src="#" style="width:300px; height:300px;" />
        </div>
    </div>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var diag = new ZL_Dialog();
        $().ready(function () {
            $(".codeimg").click(function () {
                diag.title = "二维码扫描";
                diag.width = "none";
                diag.content = "preimg_div";
                diag.ShowModal();
                $("#precode_img").attr('src', $(this).attr('src'));
            });
        });
    </script>
</asp:content>

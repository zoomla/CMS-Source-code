<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserFunc.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.UserFunc" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title><%=Resources.L.会员导航 %></title>
</asp:Content> 
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs" id="linktype_ul">
      <li data-id="2" role="presentation" ><a href="UserFunc.aspx?EliteLevel=2"><%=Resources.L.所有 %></a></li> 
      <li data-id="1" role="presentation"><a href="UserFunc.aspx?EliteLevel=1"><%=Resources.L.推荐 %></a></li>
      <li data-id="0" role="presentation"><a href="UserFunc.aspx"><%=Resources.L.普通 %></a></li> 
    </ul>
    <div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="False" PageSize="20" AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging"
            CssClass="table table-striped table-bordered table-hover" IsHoldState="false" OnRowDataBound="gvCard_RowDataBound" >
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <input  type="checkbox" name="chkSel" title="" value='<%#Eval("id") %>' />
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,名称 %>" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <a href='<%#Eval("id","AddUserFunc.aspx?menu=edit&id={0}") %>' title="<%=Resources.L.点击编辑导航 %>"><%#Eval("Name") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,状态 %>" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblState" runat="server" Text='<%# Eval("state") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,文件或地址路径 %>">
                    <ItemTemplate>
             <%--           <asp:HyperLink ID="hlLink" runat="server" NavigateUrl='<%#Eval("FlieUrl") %>' Text='<%#Eval("FlieUrl") %>' ToolTip='<%# Eval("Name") %>' Visible="false" />--%>
                        <a href="<%# Eval("FileUrl") %>" title="<%# Eval("Name") %>" target="_blank"><%#Eval("FileUrl") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,图标地址 %>">
                    <ItemTemplate>
                        <span class="font_red"><%#GetItemIcon() %></span>：<asp:Label ID="lblpic" runat="server" Text='<%# Eval("ico") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,手动排序 %>">
                     <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <input type="number" min="1" class="text_x text-center" name="order_T" value="<%#Eval("OrderID") %>" />
                        <input type="hidden" name="order_Hid" value="<%#Eval("ID")+":"+Eval("OrderID")+":"+Eval("OrderID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,连接类型 %>" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="linkType" runat="server"></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,创建时间 %>" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblTime" runat="server" Text='<%# getDate(Eval("time","{0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        <div class="text-center">
        <asp:Button ID="BtnDelete" runat="server" CssClass="btn btn-primary" OnClick="BtnDelete_Click" OnClientClick="if(!IsSelectedId()){alert('请选择内容');return false;}else{return confirm('确定删除？')}" Text="<%$Resources:L,删除导航 %>"/>
        <asp:Button ID="Button1" runat="server" Text="<%$Resources:L,批量启用 %>" CssClass="btn btn-primary" OnClick="Btnuse_Click" />
        <asp:Button ID="Button2" runat="server" Text="<%$Resources:L,批量停用 %>" CssClass="btn btn-primary" OnClick="Btnstop_Click" />
        <asp:Button ID="Button3" runat="server" Text="<%$Resources:L,保存排序 %>" CssClass="btn btn-primary" OnClick="saveOrder_Btn_Click" OnClientClick="return confirm('确定要保存修改后的排序吗!!!');"/>
    </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        $().ready(function () {
            ZL_Regex.B_Num("input[type=number]");
            $("#AllID_Chk").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });

            var EliteLevel = '<%=EliteLevel %>'; 
            $("#linktype_ul [data-id='" + EliteLevel + "']").addClass('active');
        }) 
    </script>
</asp:Content>

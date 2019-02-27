<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPaper.aspx.cs" Inherits="manage_Question_AddPaper" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>系统试卷管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="divbox" id="nocontent" runat="server" style="display: none">暂无试卷信息</div>
    <div>
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" AllowSorting="true" CssClass="table table-striped table-bordered table-hover" 
            EnableTheming="False" GridLines="None" DataKeyNames="id" OnRowDataBound="gvPapers_RowDataBound" HeaderStyle-Height="28px" OnPageIndexChanging="EGV_PageIndexChanging">
            <RowStyle CssClass="tdbg" Height="26px"></RowStyle>
            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <%# Eval("id") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="试卷名称" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <%#Eval("p_name") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="试卷分类" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfClassId" runat="server" Value='<%#Eval("P_type") %>' />
                        <asp:Label ID="lblClassId" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <a href="javascript:void(0)" onclick="clickext('<%# Eval("id") %>','<%#Eval("p_name") %>')">选择</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        function clickext(id, name) {
            var mainright = window.top.main_right;
            var txt_Exarinfo = mainright.frames["main_right_" + parent.nowWindow].document.getElementById("Exarinfo");
            var pageidtxt = mainright.frames["main_right_" + parent.nowWindow].document.getElementById("pageid");
            pageidtxt.value = id;
            txt_Exarinfo.innerHTML = name + " <a href='javascript:void(0)' onclick='deletepage()'>删除</a>";
            parent.Dialog.close();
        }
    </script>
</asp:Content>
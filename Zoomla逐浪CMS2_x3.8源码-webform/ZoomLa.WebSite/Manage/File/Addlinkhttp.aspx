<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Addlinkhttp.aspx.cs" Inherits="manage_File_Addlinkhttp" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>վ������</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="�������Ϣ����">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="chkSel" title="" value='<%#Eval("ID") %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="hidden-xs hidden-sm" Width="5%" />
                <ItemStyle CssClass="hidden-xs hidden-sm" />
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField HeaderText="�ؼ���" DataField="KeyWord" />
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <%# GetUrl(Eval("KeyValue","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="������ʽ" DataField="Regex" />
            <asp:BoundField HeaderText="�����滻ֵ" DataField="RegexValue" />
            <asp:BoundField HeaderText="Ȩֵ" DataField="Priority" />
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <a href="tjlink.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="�޸�"></i></a>
                    <asp:LinkButton ID="LinkButton2" CommandName="del1" CommandArgument='<%#Eval("ID") %>' runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="ɾ��"></i>ɾ��</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('���ɻָ���ɾ������,��ȷ����������ɾ����');" Text="����ɾ��" OnClick="Button3_Click1" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV����
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV ȫѡ
                selectAllByName(this, "chkSel");
            });

            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
        });
    </script>
</asp:Content>

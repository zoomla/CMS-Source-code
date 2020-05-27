<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreViewProg.aspx.cs" Inherits="MIS_PreViewProg" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>流程预览</title>
<script type="text/javascript">
    $().ready(function () {
        $("tr:not(:first):not(:last)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
    });
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="margin:10px;">
    <div class="draftnav" style="margin:0; margin-bottom:10px;">
        <span>流程预览</span>
    </div>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1" Width="100%" CssClass="table table-striped table-bordered table-hover" OnPageIndexChanging="EGV_PageIndexChanging" BackColor="White" DataKeyNames="ID" AllowUserToOrder="true" EmptyDataText="当前没有信息!!" >
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-Height="22" Visible="false" />
            <asp:BoundField HeaderText="序号" DataField="StepNum"/>
            <asp:BoundField HeaderText="步骤名" DataField="StepName"/>
            <asp:TemplateField HeaderText="经办人" >
                <ItemTemplate >
                    <%#GetUserInfo(Eval("ReferUser","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="抄送人">
                <ItemTemplate>
                    <%# GetUserInfo(Eval("CCUser","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="会签">
                <ItemTemplate>
                    <%# GetHQoption(Eval("HQoption","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="强制转交">
                <ItemTemplate>
                    <%# GetQzzjoption(Eval("Qzzjoption","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="回退">
                <ItemTemplate>
                    <%# GetHToption(Eval("HToption","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注" DataField="Remind"/>
        </Columns>
        <PagerStyle HorizontalAlign="Center"/>
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
</div>
</asp:Content>
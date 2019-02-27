<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamPointManage.aspx.cs" Inherits="manage_Question_ExamPointManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>考点管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
    CssClass="table table-striped table-bordered table-hover" EmptyDataText="<%$Resources:L,当前没有信息 %>" 
    OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="EGV_RowDataBound" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                   <input name="Item" type="checkbox" value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="管理员">
                <ItemTemplate><%#GetAdminName(Eval("AddUser","{0}"))%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="添加时间" DataField="AddTime" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="UpdateExamPoint.aspx?id=<%#Eval("ID")%>&&AdminId=<%#Eval("AddUser") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                    <a href="?menu=delete&id=<%#Eval("ID")%>" onclick="return confirm('确实要删除此课程?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"
            Text="批量删除" OnClick="Button3_Click" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
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
    </script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Papers_System_Manage.aspx.cs" Inherits="manage_Question_Papers_System_Manage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>试卷管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="top_opbar">
        <div class="input-group text_300">
            <asp:TextBox runat="server" ID="Skey_T" CssClass="form-control text_300" />
            <span class="input-group-btn">
                <asp:Button runat="server" ID="Skey_Btn" Text="搜索" CssClass="btn btn-primary" OnClick="Skey_Btn_Click" />
            </span>
        </div>
    </div>
     <ZL:ExGridView ID="EGV" runat="server" AllowPaging="true" AutoGenerateColumns="False"  AllowSorting="true" 
         CssClass="table table-striped table-bordered table-hover" EnableTheming="False"
            DataKeyNames="id" OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="EGV_RowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="td_s">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="试卷标题">
                    <ItemTemplate>
                        <%#Eval("p_name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="试卷分类">
                    <ItemTemplate>
                       <%#Eval("TypeName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="出题方式">
                    <ItemTemplate>
                          <%#GetModus(Eval("p_type", "{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="阅卷方式">
                    <ItemTemplate>
                       <%#GetRType(Eval("p_rtype", "{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="Add_Papers_System.aspx?id=<%#Eval("id") %>" class="option_style">修改试卷</a>
                        <a href="Paper_QuestionManage.aspx?pid=<%#Eval("id") %>" class="option_style">题目列表</a>
                        <a href="ViewPaperCenter.aspx?id=<%#Eval("id") %>" class="option_style" target="_blank">浏览试卷</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
     <asp:Button ID="BtnDelete" runat="server" class="btn btn-primary" OnClientClick="return confirm('确定删除?')" Text="批量删除" OnClick="BtnDelete_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/SelectCheckBox.js"></script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Paper_QuestionManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.Paper_QuestionManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>试卷题目管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
            <li class="active"><a href="Papers_System_Manage.aspx">试卷列表</a>[<asp:Label runat="server" ID="PTitle_L"></asp:Label>]
                <a href='javascript:;' onclick='SelQuestion()'>[选择试题]</a></li>
        </ol>
        <ul class="nav nav-tabs hidden-xs hidden-sm">
            <li id="tab_99"><a href="#tab-1" data-toggle="tab" onclick="ShowTabs(99)"><%:lang.LF("所有内容") %></a></li>
            <li id="tab_0"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">单选题</a></li>
            <li id="tab_1"><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">多选题</a></li>
            <li id="tab_2"><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2)">填空题</a></li>
            <li id="tab_3"><a href="#tab3" data-toggle="tab" onclick="ShowTabs(3)">解析题</a></li>
            <li id="tab_10"><a href="#tab10" data-toggle="tab" onclick="ShowTabs(10)">大题</a></li>
        </ul>
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
            class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input name="idchk" type="checkbox" value="<%#Eval("p_id") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="试题标题">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Upd" CommandArgument='<%#Eval("p_id") %>'><%# Eval("p_title")%></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类别" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <%#GetClass(Eval("p_Class","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="题目类型">
                    <ItemTemplate>
                        <%#GetQuesType() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="题型" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <%#GetType(Eval("p_Type", "{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </ZL:ExGridView>
        <asp:Button ID="Dels_B" runat="server" CssClass="btn btn-primary" OnClick="Dels_B_Click" OnClientClick="return confirm('是否删除选中项?')" Text="批量删除" />
        <asp:HiddenField ID="SelIDS_IDS" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        var diag = new ZL_Dialog();
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
        function SelQuestion() {
            diag.title = "选择题目";
            diag.url = "/User/Exam/SelQuestion?pid=<%=Pid %>&selids=" + $("#SelIDS_IDS").val();
            diag.maxbtn = false;
            diag.ShowModal();
        }
        function CLoseDIag() {
            diag.CloseModal();
            window.location = location;
        }
        function ActiveTab(id) {
            $("#tab_" + id).addClass("active");
        }
        function ShowTabs(id) {
            location.href = "Paper_QuestionManage.aspx?qtype=" + id + "&pid=<%=Pid %>";
        }
    </script>
</asp:Content>
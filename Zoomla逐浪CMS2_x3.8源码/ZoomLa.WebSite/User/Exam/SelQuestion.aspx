<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelQuestion.aspx.cs" Inherits="Manage_Exam_SelQuestion" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择试题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container-fluid">
    <div>
        <div class="input-group text_400">
            <asp:TextBox ID="KeyWord_T" runat="server" CssClass="form-control" placeholder="试题标题"></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton ID="Search_B" runat="server" CssClass="btn btn-default" OnClick="Search_B_Click"><span class="fa fa-search"></span></asp:LinkButton>
                <asp:Button ID="Sel_B" runat="server" CssClass="btn btn-primary" OnClick="Sel_B_Click" Text="确定" />
                <asp:Button ID="LargeSel_B" Visible="false" runat="server" class="btn btn-primary" OnClick="LargeSel_B_Click" Text="确定" />
                <button class="btn btn-primary" onclick="parent.CloseComDiag()" type="button">取消</button>
            </span>
        </div>
    </div>
    <ul class="nav nav-tabs margin_t5">
        <li id="tab_99"><a href="#tab-1" data-toggle="tab" onclick="ShowTabs(99)">所有试题</a></li>
        <li id="tab_0"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">单选题</a></li>
        <li id="tab_1"><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">多选题</a></li>
        <li id="tab_2"><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2)">填空题</a></li>
        <li id="tab_3"><a href="#tab3" data-toggle="tab" onclick="ShowTabs(3)">解析题</a></li>
        <li id="tab_4"><a href="#tab3" data-toggle="tab" onclick="ShowTabs(4)">完形填空</a></li>
        <li id="tab_10"><a href="#tab10" data-toggle="tab" onclick="ShowTabs(10)">大题</a></li>
    </ul>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"   EnableTheming="False" IsHoldState="false"
                class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"  OnPageIndexChanging="EGV_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-CssClass="td_m">
                    <ItemTemplate>
                        <%#GetCheck() %>
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
            <asp:TemplateField HeaderText="题型" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <%#GetType(Eval("p_Type", "{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:HiddenField ID="SelIds_Hid" runat="server" />
</div>
<script>
    $(function () {
        $("input[name=idchk]").click(function () {
            if ($(this)[0].checked) {
                $("#SelIds_Hid").val($("#SelIds_Hid").val() + "," + $(this).val() + ",");
            } else {
                $("#SelIds_Hid").val($("#SelIds_Hid").val().replace("," + $(this).val() + ",", ","));
            }
        });
        $("#AllID_Chk").click(function () {
            $("input[name=idchk]").each(function (i, v) {
                var $tr = $(v).closest("tr");
                if ($(v)[0].checked) {
                    if ($("#SelIds_Hid").val().indexOf("," + $(v).val() + ",") < 0)//判断是否存在该id
                        $("#SelIds_Hid").val($("#SelIds_Hid").val() + "," + $(v).val() + ",");
                } else {
                    $("#SelIds_Hid").val($("#SelIds_Hid").val().replace("," + $(v).val() + ",", ","));
                }
            });
        });
    });
    function ActiveTab(id) {
        $("#tab_" + id).addClass("active");
    }
    function ShowTabs(id) {
        location.href = "SelQuestion.aspx?qtype=" + id + "&pid=<%=Pid %>&islage=<%=IsLage %>&selids=" + $("#SelIds_Hid").val();
    }
    function CloseDIag() {
        parent.CLoseDIag();
    }
    function DisTab(id) {
        $("#tab_" + id).remove();
    }
    function SelQuestion(qids, qtitle) {
        parent.GetQuestion(qids, qtitle);
    }
</script>
</asp:Content>

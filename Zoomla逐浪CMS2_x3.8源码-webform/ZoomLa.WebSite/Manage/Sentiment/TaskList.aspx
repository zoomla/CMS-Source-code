<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskList.aspx.cs" Inherits="Manage_Sentiment_Source" MasterPageFile="~/Manage/I/Default.master" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content runat="server" ID="ContentID1" ContentPlaceHolderID="head">
<title>监测来源</title>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<style>
.addbox{ display:none;}
</style>
</asp:Content>
<asp:Content runat="server"  ContentPlaceHolderID="Content">
<div> 
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging"  OnRowCommand="EGV_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="td_m" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" data-skey="<%#Eval("Condition") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-CssClass="td_m" />
                <asp:BoundField HeaderText="标题" DataField="Title" />
                <asp:BoundField HeaderText="来源" DataField="Source" />
                <asp:BoundField HeaderText="关键字" DataField="Condition" /> 
                <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                <a href="AddTask.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                <a href="GetReport.aspx?Skey=<%#HttpUtility.UrlEncode(Eval("Condition","")) %>" class="option_style report_a"><i class="fa fa-eye" title="查看"></i>查看报告</a>                
                </ItemTemplate>
                </asp:TemplateField>            
            </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
    <input type="button" value="生成报表" class="btn btn-primary" onclick="GetReport();" />
    <asp:Button runat="server" ID="BatDel_Btn" OnClick="BatDel_Btn_Click" Text="批量删除" OnClientClick="return confirm('确定要删除吗');" CssClass="btn btn-primary" />       
</div>     
</asp:Content> 
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    function GetReport() {
        var skey = "";
        var $chkarr = $("input[name=idchk]:checked");
        if ($chkarr.length < 1) { alert("请先选择关键词"); return false; }
        $chkarr.each(function () {
            skey += $(this).data("skey") + ",";
        })
        comdiag.ShowMask("正在努力抓取数据,请稍等");
        location = "GetReport.aspx?Skey=" + escape(skey);
    }
    $(".report_a").click(function () {
        comdiag.ShowMask("正在努力抓取数据,请稍等");
    });
</script>
</asp:Content>
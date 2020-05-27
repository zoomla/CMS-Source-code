<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoLog.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Collect.InfoLog" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>检索动态</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="width:100%; height:40px; ">
        <div>
            <asp:TextBox runat="server" Style="float: left;" ID="BeginTime_T" onclick="WdatePicker()" Width="150px" CssClass=" form-control" placeholder="开始时间"></asp:TextBox>
            <span style="float:left; margin-top:5px;margin-left:2px; color:blue; margin-right:2px;">-</span>
    <asp:TextBox runat="server" ID="EndTime_T" Style="float: left;" onclick="WdatePicker()" Width="150px" CssClass=" form-control" placeholder="结束时间"></asp:TextBox>
        </div>
        <span style="float:left; margin-top:5px;margin-left:2px; color:blue; margin-right:2px;">-</span>
        <div class="input-group" style="width: 220px; float: left;">
            <asp:TextBox runat="server" ID="Search_T" class="form-control" placeholder="请输入需要搜索的内容" />
            <span class="input-group-btn">
                <asp:LinkButton runat="server" CssClass="btn btn-default" OnClick="souchok_Click" ID="souchok_B"><span class="fa fa-search"></span></asp:LinkButton>
            </span>
        </div>
    </div>
    <asp:CompareValidator ID="comparevalidator2" runat="server" ForeColor="Red"  ControlToCompare="BeginTime_T" ControlToValidate="EndTime_T" Operator="GreaterThan" ErrorMessage="结束日期必须大开于始日期"></asp:CompareValidator>
    <ZL:ExGridView runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="当前日志为空！" OnPageIndexChanging="EGV_PageIndexChanging" AutoGenerateColumns="false" PageSize="10" ID="EGV" CssClass="table table-bordered table-hover table-striped">
        <Columns>
            <asp:TemplateField HeaderText="采集结果">
                <ItemTemplate>
                    <%#Eval("LogTypeStr") %>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="采集信息">
                <ItemTemplate>
                    <%#Eval("Remind") %>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="采集时间">
                <ItemTemplate>
                    <%#Eval("CreateDate") %>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>

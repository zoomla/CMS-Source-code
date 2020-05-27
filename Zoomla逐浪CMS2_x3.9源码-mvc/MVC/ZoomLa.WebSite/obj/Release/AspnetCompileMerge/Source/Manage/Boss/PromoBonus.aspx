<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromoBonus.aspx.cs" Inherits="ZoomLaCMS.Manage.Boss.PromoBonus" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>推荐奖</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="margin_b2px table table-bordered">
        <asp:TextBox runat="server" ID="Time_T" onclick="WdatePicker({dateFmt:'yyyy/MM/dd',firstDayOfWeek:1,isShowWeek:'true',onpicked:function(){WhichWeek();}});" CssClass="form-control text_md" />
        <input type="text" id="Week_T" class="form-control text_x" readonly="readonly"/>
        <asp:Button runat="server" ID="Begin_Btn" Text="查询" OnClick="Begin_Btn_Click" CssClass="btn btn-primary" />
<%--        <asp:Button runat="server" ID="ReBegin_Btn" Text="重新生成" OnClientClick="return confirm('确定要重新生成分成报表吗');" OnClick="ReBegin_Btn_Click" CssClass="btn btn-primary" />--%>
<%--        <asp:Button runat="server" ID="AutoUnit_Btn" Text="自动分成" OnClientClick="return UnitConfirm(this);" OnClick="AutoUnit_Btn_Click" CssClass="btn btn-danger" />--%>
    </div>
    <div class="margin_b2px table table-bordered" style="display:none;">
<%--        <asp:CheckBox runat="server" ID="OnlyUnit_Chk" Checked="false" Text="仅显示已分成的用户" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
        <asp:CheckBox runat="server" ID="OnleyBonus_Chk" Checked="false" Text="仅显示分成金额>0的用户" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />--%>
    </div>
      <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" 
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="没有佣金信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="EGV_RowDataBound" >
            <Columns>
           <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="周期">
                <ItemTemplate>
<%--                    <%#GetDate() %>--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="推广佣金" DataField="Unit" DataFormatString="{0:f2}" />
           <asp:TemplateField HeaderText="用户名">
                <ItemTemplate><a href="UserInfo.aspx?id=<%#Eval("UserID") %>" target="_blank"><%#Eval("UserName") %></a></ItemTemplate>
            </asp:TemplateField>
         <%--       <asp:TemplateField HeaderText="分红来源">
                    <ItemTemplate><%#GetBonusSource() %></ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="入账状态">
                    <ItemTemplate><%#GetStatus() %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="备注" DataField="Remark" />
                <asp:BoundField HeaderText="日期" DataField="CDate" />
<%--            <asp:TemplateField HeaderText="推荐人">
                <ItemTemplate><a href="UserInfo.aspx?id=<%#Eval("PUserID") %>"  target="_blank"><%#Eval("PUserID") %></a></ItemTemplate>
            </asp:TemplateField>--%>
                 <%--<asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                       <a href="UnitMain.aspx?uid=<%#Eval("UserID") %>&stime=<%=STime.ToString("yyyy/MM/dd") %>">查看详情</a>
                       <a href="UnitDetail.aspx?Pid=<%#Eval("ID") %>">查看流水</a>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <RowStyle Height="24px" HorizontalAlign="Center"  />
            </ZL:ExGridView>
    <%-- <asp:Button runat="server" ID="Count_Btn" Text="计算佣金" CssClass="btn btn-primary" OnClick="Count_Btn_Click" OnClientClick="return confirm('确定要计算佣金吗?');" />--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>

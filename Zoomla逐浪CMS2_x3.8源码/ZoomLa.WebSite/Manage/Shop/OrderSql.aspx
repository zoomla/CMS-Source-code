<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderSql.aspx.cs" Inherits="manage_Shop_OrderSql" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>账单申请</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" CssClass="table table-striped table-bordered table-hover" 
        EmptyDataText="<%$Resources:L,当前没有信息 %>" OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="EGV_RowDataBound" OnRowCommand="EGV_RowCommand">
        <Columns>
            <asp:TemplateField >
                <ItemTemplate>
                    <input type="checkbox" value="<%#Eval("id") %>" name="idchk" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="账单类型">
                <ItemTemplate>
                    <a href="OrderSql.aspx?Type=<%#Eval("OrderType")%>"><%#getOrderType(Eval("OrderType","{0}"))%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="会员名">
                <ItemTemplate><%#getUserName(Convert.ToInt32(Eval("UserName")))%> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="提交金额">
                <ItemTemplate><%#Eval("money","{0:N2}")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="返分会员名">
                <ItemTemplate><%#getUserName(Convert.ToInt32(Eval("PuserID")))%> </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注" DataField="remark" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#getStatus(Eval("status","{0}"))%> | <%#getPayStatus(Eval("PayState","{0}"))%> | <%#getRunStatus(Eval("SqlState","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="执行次数" DataField="RunNum" />
            <asp:BoundField HeaderText="提交时间" DataField="AddTime" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                <%--    <a href="EditOrderSql.aspx?ID=<%#Eval("id") %>" class="option_style"><i class="fa fa-eye" title="查看"></i></a>--%>
              <%--      <asp:LinkButton ID="StatBtn"  runat="server" Text='<%#getStabtn(Eval("status","{0}"))%>' CssClass="option_style"><span style='color:red'></span></asp:LinkButton>
                    <asp:LinkButton ID="PayBtn" runat="server" Text='<%#getPaybtn(Eval("PayState","{0}"))%>' CommandName="pay" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"></asp:LinkButton>--%>
                    <asp:LinkButton ID="Del_btn" runat="server" CommandName='Del' Text='删除' CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                   <%-- <asp:LinkButton ID="runBtn" runat="server" Text='执行' CommandName="Run" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-cubes" title="批处理"></i> 执行批处理 </asp:LinkButton>--%>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='生成' CommandName="save" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-spinner" title="生成"></i>生成脚本文件</asp:LinkButton>
                    <asp:LinkButton ID="save_btn" runat="server" Text='下载' CommandName="download" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-download" title="下载"></i>下载数据库脚本</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div class="clear"></div>
    <asp:Button runat="server" ID="BatAudit_Btn" Text="批量审核" OnClick="BatAudit_Btn_Click" CssClass="btn btn-info"/>
    <asp:Button runat="server" ID="BatUnAudit_Btn" Text="取消审核" OnClick="BatUnAudit_Btn_Click" CssClass="btn btn-info"/>
    <asp:Button runat="server" ID="BatPay_Btn" Text="批量支付" OnClick="BatPay_Btn_Click" CssClass="btn btn-info" />
    <div id="RunOK" runat="server"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>




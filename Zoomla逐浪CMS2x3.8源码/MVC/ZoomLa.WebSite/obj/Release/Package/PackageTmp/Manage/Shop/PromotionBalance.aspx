<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromotionBalance.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.PromotionBalance" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商品推广结算</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="tab-content">
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" IsHoldState="false" 
         CssClass="table table-striped table-bordered table-hover" EmptyDataText="<%$Resources:L,当前没有信息 %>"
         OnPageIndexChanging="EGV_PageIndexChanging" >
            <Columns>
                <asp:TemplateField HeaderText=""><ItemTemplate><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="推广人">
                    <ItemTemplate>
                        <a style="color:blue;" href='OrderBlanace.aspx?id=<%#Eval("PromotionUserId") %>&balance=0 ' title="查看此用户推广信息">
                            <%#Eval("UserName")%>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="时间" DataField="AddTime" />
                <asp:BoundField HeaderText="商品ID" DataField="CartProId" />
                <asp:TemplateField HeaderText="商品名称">
                    <ItemTemplate><%#Eval("ProName") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品价格">
                    <ItemTemplate><%#Eval("Shijia","{0:f2}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="购买数量">
                    <ItemTemplate><%#Eval("ProNum")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品总价">
                    <ItemTemplate><%#Eval("AllMoney")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查看">
                    <ItemTemplate>
                        <a href='MtrlsMrktng.aspx?id=<%#Eval("PromotionUserId") %>' style="color:Blue;" title="查看返利信息">返利信息</a>
                    </ItemTemplate>    
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
</asp:Content>
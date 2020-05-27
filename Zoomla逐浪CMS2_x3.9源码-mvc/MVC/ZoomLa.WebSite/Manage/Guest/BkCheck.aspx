<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BkCheck.aspx.cs" Inherits="ZoomLaCMS.Manage.Guest.BkCheck" ValidateRequest="false" EnableViewStateMac="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>词条管理</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-bottom:10px;">
        <div class="pull-left" style="line-height:32px;">
            按词条名搜索：
        </div>
        <div class="input-group pull-left" style="width:300px;">
            <asp:TextBox CssClass="form-control" ID="Key_T" runat="server"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="SearchBtn" CssClass="btn btn-default" runat="server" Text="搜索" OnClick="SearchBtn_Click" />
            </span>
        </div>
    </div><div class="clearfix"></div>
    <div class="borders">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#tab-10" onclick="ShowTabss(-100)" data-toggle="tab">所有词条</a></li>
            <li><a href="#tab0" data-toggle="tab" onclick="ShowTabss(0)"><%=lang.LF("待审核")%></a></li>
            <li><a href="#tab1" data-toggle="tab" onclick="ShowTabss(1)"><%=lang.LF("已审核")%></a></li>
        </ul>
    </div>
    <div class="panel panel-default" style="padding: 0px;">
        <div class="panel panel-body" style="padding: 0px; margin: 0px;">
            <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="td_s">
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" title="" value='<%#Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-CssClass="td_s" />
                    <asp:TemplateField HeaderText="词条">
                        <ItemTemplate>
                            <%#Eval("Tittle")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="版本" DataField="VerStr" HeaderStyle-CssClass="td_m"/>
                    <asp:TemplateField HeaderText="创建人" HeaderStyle-CssClass="td_s">
                        <ItemTemplate>
                            <%#Eval("UserName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间" HeaderStyle-CssClass="td_l">
                        <ItemTemplate>
                            <%#Eval("AddTime","{0:yyyy年MM月dd日}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
          <%--          <asp:TemplateField HeaderText="分类" HeaderStyle-CssClass="td_s">
                        <ItemTemplate>
                            <%#Eval("GradeName") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="推荐" HeaderStyle-CssClass="td_s">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#getElite(Eval("Elite").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-CssClass="td_s">
                        <ItemTemplate>
                            <%#getcommend()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="/Guest/Baike/Details.aspx?id=<%#Eval("ID") %>" target="_blank" class="option_style"><i class="fa fa-eye" title="预览"></i></a>
                            <a href="/Baike/BKEditor.aspx?id=<%#Eval("ID") %>&mode=admin" target="_blank" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return confirm('确实要删除吗？');" CommandArgument='<%#Eval("ID") %>' CommandName="Del" CausesValidation="false" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                            <%--<a href="baikeMerger.aspx?tittle=<%#Eval("Tittle").ToString()%>" class="option_style"><i class="fa fa-columns" title="合并"></i>合并词条</a>--%>
<%--                            <a href="InfoList.aspx?id=<%#Eval("ID").ToString()%>" class="option_style"><i class="fa fa-list-alt" title="列表"></i>信息列表</a>--%>
                              <a href="javascript:;" class="option_style" onclick="ShowBKList('<%#Eval("Flow") %>');"><i class="fa fa-list"></i>版本管理</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
        </div>
        <div class="panel panel-footer" style="padding: 3px; margin: 0px;">
            <asp:Button ID="Button1" runat="server" Text="批量删除" OnClick="BtnSubmit1_Click" UseSubmitBehavior="False" OnClientClick="if(!confirm('确定要批量删除词条吗？')){return false;}" CssClass="btn btn-primary" />
            <asp:Button ID="Button2" runat="server" Text="批量审核" OnClick="BtnSubmit2_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
            <asp:Button ID="Button3" runat="server" Text="取消审核" OnClick="BtnSubmit3_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
            <asp:Button ID="Button4" runat="server" Text="推荐" OnClick="BtnSubmit4_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
            <asp:Button ID="Button5" runat="server" Text="取消推荐" OnClick="BtnSubmit5_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
            <asp:Button ID="Button8" runat="server" Text="特色" OnClick="BtnSubmit8_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
            <asp:Button ID="Button9" runat="server" Text="取消特色" OnClick="BtnSubmit9_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
            <asp:Button ID="Button6" runat="server" Text="每日图片" OnClick="BtnSubmit6_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
            <asp:Button ID="Button7" runat="server" Text="取消每日图片" OnClick="BtnSubmit7_Click" UseSubmitBehavior="False" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/SelectCheckBox.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        function ShowTabss(ID) {
            location.href = 'BkCheck.aspx?NodeID=<%=Request.QueryString["NodeID"] %>&id=' + ID + '&type=' + ID;
        }
        $(function () {
            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
        })
        function displayToolbar() {
            $("#toolbar1").toggle("fast");
        }
        function ShowBKList(flow) {
            ShowComDiag("BKList.aspx?Flow=" + flow, "版本浏览");
        }
    </script>
</asp:Content>

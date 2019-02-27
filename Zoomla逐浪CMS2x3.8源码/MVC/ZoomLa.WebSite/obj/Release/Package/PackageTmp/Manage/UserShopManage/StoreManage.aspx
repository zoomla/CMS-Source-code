<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreManage.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.StoreManage" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>店铺审核</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="storeSearch" class="input-group" style="width:300px;">
      <asp:TextBox runat="server" ID="SKey" CssClass="form-control" placeholder="输入店铺名或店铺ID" />
      <span class="input-group-btn">
      <asp:Button runat="server" Text="搜索" CssClass="btn btn-default" OnClick="SearchStore" />
       </span>
    </div>
    <ul class="nav nav-tabs hidden-xs hidden-sm">
	<li class="active"><a href="#tab-1" data-toggle="tab" onclick="ShowTabs(-1)"><%:lang.LF("所有内容") %></a></li>
        <li><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2)"><%:lang.LF("推荐") %></a></li>
	<li><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)"><%:lang.LF("已审核") %></a></li>
	<li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)"><%:lang.LF("未审核") %></a></li>
</ul>
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="GeneralID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" OnRowDataBound="Egv_RowDataBound"
            IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" title="" value='<%#Eval("GeneralID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="GeneralID" />
                <asp:TemplateField HeaderText="店铺名称">
                    <ItemTemplate>
                        <a href="/Store/Storeindex?id=<%# Eval("GeneralID")%>" target="_blank" title="点击在前台预览店铺">
                            <%#GetStorename(Eval("GeneralID","{0}"))%>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请人用户名">
                    <ItemTemplate>
                        <a href="javascript:void(0);>" onclick="SelUser(<%#GetUserID(Eval("Inputer").ToString()) %>)"><%#(Eval("Inputer"))%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IP地址">
                    <ItemTemplate>
                        <%#GetIpLocation(Eval("ComplianceUserList","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请时间">
                    <ItemTemplate>
                        <%#GetAddtime(Eval("GeneralID","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审核">
                    <ItemTemplate>
                        <%#GetState(Eval("GeneralID","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="StoreEdit.aspx?id=<%#Eval("GeneralID")%>" class="option_style"><i class="fa fa-eye" title="预览"></i></a>
                        <a href="StroeUpdate.aspx?username=<%#Eval("Inputer")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                        <a target="_blank" href="/Store/StoreIndex?id=<%#Eval("GeneralID") %>" class="option_style"><i class='fa fa-globe' title="浏览"></i>浏览</a>                               
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        <div>
            <asp:Button ID="Batch" CssClass="btn btn-info" Style="width: 110px;" runat="server" CommandName="1" Text="批量审核" OnClick="Batch_Click" />
            <asp:Button ID="Bcancle" CssClass="btn btn-info" Style="width: 110px;" runat="server" CommandName="2" Text="取消审核" OnClick="Batch_Click" />
            <asp:Button ID="Button1" class="btn btn-info" Style="width: 110px;" runat="server" Text="设为推荐" CommandName="1" OnClick="Button1_Click" />
            <asp:Button ID="Button6" class="btn btn-info" Style="width: 110px;" runat="server" Text="取消推荐" CommandName="3" OnClick="Button1_Click" />
            <asp:Button ID="Button3" class="btn btn-info" Style="width: 110px;" runat="server" Text="批量删除" CommandName="5" OnClick="Button1_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" />
        </div>
    </div>
    <%
        if (Request.QueryString["ShowMsg"] == "true")
        { %>
    <div id="wbg">
    </div>
    <div id="img" style="background: #fff; border: 1px solid #ccc; padding: 20px; height: 200px;">
        <div class="msgdiv">
            <a onclick="tb_remove()" href="javascript:void(0);" class="wbgNone">关闭</a>
            <strong>店铺审核通知_未通过</strong>
            <p>
                未通过理由：
                    <br />
                <asp:TextBox ID="ids" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="types" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="msgContent" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine"
                    CssClass="x_input" Style="width: 300px; height: 100px;">店铺审核通知_未通过</asp:TextBox><br />
                <asp:Button ID="Sendemail" runat="server" Text="发送消息" CssClass="btn btn-primary" OnClick="msgsendBtn_Click" />
            </p>
        </div>
    </div>
    <%} %>
    <script type="text/javascript">
        function tb_remove() {
            document.getElementById('wbg').style.display = 'none';
            document.getElementById('img').style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $().ready(function () {
            ShowStatus();
        });
        function ShowStatus() {
            var type = '<%=Request.QueryString["type"] %>';
            if (type != '') {
                $("li a[href='#tab" + type + "']").parent().addClass("active").siblings("li").removeClass("active");
            }
        }
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
        function ShowTabs(ID) {
            location.href = 'StoreManage.aspx?type=' + ID;
        }
        //--------------查看用户信息
        var TemDiag = new ZL_Dialog();
        var SelUser = function (uid) {
            TemDiag.title = "用户信息";
            TemDiag.maxbtn = false;
            TemDiag.url = "../User/UserInfo.aspx?id=" + uid;
            TemDiag.ShowModal();
        }
    </script>
</asp:Content>

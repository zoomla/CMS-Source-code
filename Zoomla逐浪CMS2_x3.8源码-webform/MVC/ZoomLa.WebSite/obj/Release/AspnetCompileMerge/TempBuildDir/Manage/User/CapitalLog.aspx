<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapitalLog.aspx.cs" Inherits="ZoomLaCMS.Manage.User.CapitalLog"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title><%=Resources.L.财务流水 %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
        <li><a href='<%=customPath2 %>User/UserManage.aspx'><%=Resources.L.会员管理 %></a></li><li class='active'><%=Resources.L.财务流水 %></li>
        <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
        <div id="sel_box" class="padding5">
            <div class="input-group sel_box">
                <span class="input-group-btn">
                    <asp:DropDownList ID="Search_Drop" runat="server" CssClass="form-control" style="width:120px;">
                         <asp:ListItem Value="1" Text="<%$Resources:L,用户ID %>"></asp:ListItem>
                         <asp:ListItem Value="2" Text="<%$Resources:L,用户名 %>"></asp:ListItem>
                     </asp:DropDownList>
                 </span>
                <asp:TextBox runat="server" ID="Search_T" class="form-control" placeholder="<%$Resources:L,检索当前位置 %>" />
                <span class="input-group-btn">
                        <asp:LinkButton runat="server" CssClass="btn btn-default" ID="LinkButton1" OnClick="Search_Btn_Click" Text="<%$Resources:L,搜索 %>"></asp:LinkButton>
                        <asp:Button runat="server" ID="Search_Btn_Hid" OnClick="Search_Btn_Click" style="display:none;" />
                </span>
            </div>
        </div> 
    </ol>
    <ul class="nav nav-tabs margin_t5" role="tablist">
    <li data-type="1" class=''><a href="?type=1"><%=Resources.L.余额操作 %></a></li>
    <li data-type="3" class=''><a href="?type=3"><%=Resources.L.积分记录 %></a></li>
    <li data-type="2" class=''><a href="?type=2"><%=Resources.L.银币记录 %></a></li>
    <li data-type="4" class=''><a href="?type=4"><%=Resources.L.点券记录 %></a></li>
    <li data-type="6" class=''><a href="?type=6"><%=Resources.L.信誉值记录 %></a></li>
    <li data-type="7" class=''><a href="?type=7"><%=Resources.L.订单流水 %></a></li>
  </ul>
    <div class="tab-content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
    CssClass="table table-striped table-bordered" EmptyDataText="<%$Resources:L,当前没有信息 %>" 
    OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="EGV_RowDataBound" >
        <Columns>
                <asp:TemplateField ItemStyle-CssClass="td_xs">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("ExpHisID") %>" />
                    </ItemTemplate>
                    <ItemStyle CssClass="td_s" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="ExpHisID" ItemStyle-CssClass="td_s" />
                <asp:BoundField HeaderText="<%$Resources:L,用户ID %>" DataField="UserID" />
                <asp:TemplateField HeaderText="<%$Resources:L,用户名 %>">
                <ItemTemplate>
                    <a href='Userinfo.aspx?id=<%#Eval("UserID") %>'><%#Eval("UserName") %></a>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$Resources:L,操作时间 %>" DataField="HisTime" />
                <asp:BoundField HeaderText="<%$Resources:L,操作金额 %>" DataField="Score" DataFormatString="{0:f2}" />
                <asp:BoundField HeaderText="<%$Resources:L,详细 %>" DataField="Detail" />
            </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" Text="<%$Resources:L,批量删除 %>" OnClick="Dels_Btn_Click" OnClientClick="return confirm('是否删除选定记录!')" />
    </div>
    <script>
        //选择滑动门
        function CheckType(id) {
            $("[data-type='"+id+"']").addClass('active');
        }
        $("#sel_btn").click(function (e) {
            if ($("#sel_box").css("display") == "none") {
                $(this).addClass("active");
                $("#sel_box").slideDown(300);
            }
            else {
                $(this).removeClass("active");
                $("#sel_box").slideUp(200);
            }
        });
    </script>
</asp:Content>



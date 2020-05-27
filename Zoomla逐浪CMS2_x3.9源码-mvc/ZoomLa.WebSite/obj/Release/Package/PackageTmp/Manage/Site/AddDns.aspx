<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDns.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.AddDns"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <style>
        #subDomEGV tr th {font-weight:normal;background:url();}
        input {text-align:center;}
        th {text-align:center;}
    </style>
    <script type="text/javascript">
        $().ready(function () {
            $("#EGV tr:last >td>:text").css("line-height", "normal");
            $("#subDomEGV tr:first >th").removeClass("title");
            $("#subDomEGV tr:first >th:eq(0)").html("<input type=checkbox id='chkAll' style='margin-right:10px;floa'/>子域名");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "subChk");
            });
        });
        function disAddDiv() {
            $("#tab3").hide();
            $("#addDiv").show();
        }
    </script>
    <title>添加DNS</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content"> 
    <div id="site_main">
    <div id="tab3" runat="server">
        <div class="input-group" style="width:25%;">
  <asp:TextBox runat="server" ID="searchText" placeholder="请输入用户名" CssClass="form-control"/>
  <span class="input-group-btn">
      <asp:Button runat="server" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" CssClass="btn btn-primary"/> 
  </span>
</div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" RowStyle-CssClass="table table-condensed table-bordered"  OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand"
            CssClass="table table-bordered table-striped table-hover" 
            PageSize="10" EnableTheming="False" EmptyDataText="没有任何数据!"  AllowSorting="True" EnableModelValidation="True" IsHoldState="false" SerialText="">
            <Columns>
                <asp:BoundField HeaderText="会员ID" DataField="UserID" ReadOnly="true" />
                <asp:TemplateField HeaderText="会员名">
                    <ItemTemplate>
                        <%# Eval("UserName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="会员类型">
                    <ItemTemplate>
                        <%# GetGroupName(Eval("GroupID","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RegTime" HeaderText="注册时间" SortExpression="RegTime" DataFormatString="{0:yyyy-MM-dd}">
                </asp:BoundField>
                <asp:BoundField DataField="Purse" HeaderText="资金余额" DataFormatString="{0:F2}">
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("UserID") %>' CommandName="Select">选择</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
    </div>
    <div class="panel panel-primary" style="width:30%;float:left;" id="addDiv" runat="server" visible="false">
        <div class="panel-heading"><h3 class="panel-title">DNS映射</h3></div>
        <div class="panel-body">
            <table class="table table-bordered table-hover">
                <tbody>
                    <tr><td style="width: 80px;" >用户名：</td><td>
                        <asp:TextBox runat="server" ID="userNameT" class="form-control" disabled="disabled"/>
                        <asp:HiddenField runat="server" ID="userID" /></td></tr>
                    <tr><td style="width: 80px;" >域名：</td><td><asp:TextBox runat="server" ID="domNameT" class="form-control" onkeyup="GetEnterCode('focus','dom_ipT');"/>
                         <asp:RequiredFieldValidator ID="reg2" runat="server" ControlToValidate="domNameT" ForeColor="Red" ErrorMessage="不能为空" ValidationGroup="add"/></td></tr>
                    <tr><td>IP：</td><td><asp:TextBox runat="server" ID="dom_ipT" class="form-control" onkeyup="GetEnterCode('click','saveBtn');"/>
                         <asp:RequiredFieldValidator ID="reg1" runat="server" ControlToValidate="dom_ipT" ForeColor="Red" ErrorMessage="不能为空" ValidationGroup="add" /></td></tr>
                 <%--   <tr><td>Mail：</td><td><asp:TextBox runat="server" ID="dom_mailT" class="form-control"/></td></tr>--%>
                    <tr><td>子域名数：</td><td><asp:TextBox runat="server" ID="sub_dom_numT" Text="50" class="form-control" Width="50px"/>
                        <asp:RegularExpressionValidator ID="reg3" runat="server" ControlToValidate="sub_dom_numT" SetFocusOnError="true" ForeColor="Red"
                            ErrorMessage="必须为数字" Display="Dynamic" ValidationExpression="([0-9]+)" ValidationGroup="add"/>
                                      </td></tr>
                    <tr><td>Url转发：</td><td><asp:TextBox runat="server" ID="url_forwardT" Text="10" class="form-control" Width="50px"/>
                         <asp:RegularExpressionValidator ID="reg4" runat="server" ControlToValidate="url_forwardT" SetFocusOnError="true" ForeColor="Red"
                            ErrorMessage="必须为数字" Display="Dynamic" ValidationExpression="([0-9]+)" ValidationGroup="add"/>
                                       </td></tr>
                    <tr><td>立即启用：</td><td><asp:CheckBox runat="server" ID="dom_status" Checked="true" /> </td></tr>
                    <tr><td>操作：</td>
                        <td><asp:Button runat="server" ID="saveBtn" Text="添加" class="btn btn-primary" OnClick="saveBtn_Click"  ValidationGroup="add" UseSubmitBehavior="false"/>
                            <input type="button" id="returnBtn" value="返回列表" class="btn btn-primary" onclick="location = 'DNSManage.aspx';"/></td></tr>
                </tbody>
            </table>
            <span class="alert alert-success" runat="server" visible="false" id="remindSpan" style="padding:9px;"></span>
        </div>
    </div>
    <div class="panel panel-primary" style="float:right;width:68%;" id="subDiv" runat="server" visible="false">
        <div class="panel-heading"><h3 class="panel-title">子域名管理</h3></div>
        <div class="panel-body">
            <ZL:ExGridView runat="server" ID="subDomEGV" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="subDomEGV_PageIndexChanging" OnRowCommand="subDomEGV_RowCommand" CellPadding="2" CellSpacing="1" Width="100%" PageSize="10" GridLines="None" EnableTheming="False" EmptyDataText="没有任何数据！" AllowSorting="True" CheckBoxFieldHeaderWidth="3%" EnableModelValidation="True" IsHoldState="false" SerialText="" CssClass="table table-bordered table-hover">
                 <Columns>
                     <asp:TemplateField HeaderText="子域名">
                         <ItemTemplate>
                             <input type="checkbox" name="subChk" value="<%#Eval("ID") %>"/>
                             <asp:TextBox runat="server" ID="subDomainEdit" Text='<%#Eval("SubDomain") %>' />
                             <asp:HiddenField runat="server" ID="subDomID" Value='<%#Eval("ID") %>'/>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="数据">
                         <ItemTemplate><asp:TextBox runat="server" ID="subDomDataEdit" Text='<%#Eval("SubDomData") %>' TabIndex="1"/></ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="操作">
                         <ItemTemplate><asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>'
                             OnClientClick="return confirm('确定要删除吗!!!');" ><img src="/App_Themes/AdminDefaultTheme/images/del.png" /></asp:LinkButton></ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
                  <PagerStyle HorizontalAlign="Center" />
                 <RowStyle Height="24px" HorizontalAlign="Center" />
             </ZL:ExGridView>
            <asp:Button runat="server" ID="subSaveBtn" Text="保存修改" OnClick="subSaveBtn_Click" CssClass="btn btn-primary" UseSubmitBehavior="false"/>
            <asp:Button runat="server" ID="subDelBtn" Text="批量删除" OnClick="subDelBtn_Click" CssClass="btn btn-primary"
                 UseSubmitBehavior="false" OnClientClick="if(confirm('确定要删除吗!!!')){}else{return false;}"/>
            <table class="table table-bordered " style="margin-top:10px;">
                <tr><td>子域名:</td><td><asp:TextBox runat="server" ID="subDomNameT" CssClass="form-control text_md" onkeyup="return GetEnterCode('focus','subDomDataT');"/>
                    <asp:RequiredFieldValidator runat="server" ID="subAdd1" ControlToValidate="subDomNameT" ErrorMessage="不能为空!" Display="Dynamic" ForeColor="Red" ValidationGroup="subAdd"/>
                                 </td></tr>
                <tr><td>数据:</td><td>  <asp:TextBox runat="server" ID="subDomDataT"  CssClass="form-control text_md" onkeyup="return GetEnterCode('click','addSubBtn');"/>
                       <asp:RequiredFieldValidator runat="server" ID="subAdd2" ControlToValidate="subDomDataT" ErrorMessage="不能为空!" Display="Dynamic" ForeColor="Red" ValidationGroup="subAdd"/>
                                </td></tr>
                <tr><td>操作:</td><td>  <asp:Button runat="server" ID="addSubBtn" Text="添加" OnClick="addSubBtn_Click" CssClass="btn btn-primary" ValidationGroup="subAdd" UseSubmitBehavior="false"/></td></tr>
            </table>
        </div>
    </div>
    </div>
</asp:Content>


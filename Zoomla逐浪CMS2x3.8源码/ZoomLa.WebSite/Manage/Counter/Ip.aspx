<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ip.aspx.cs" Inherits="manage_Counter_Ip" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>总访问报表</title><style>.allchk_l{display:none;}</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%:CustomerPageAction.customPath2 %>I/Main.aspx'>工作台</a></li><li><a href='Counter.aspx'>访问统计</a></li><li><a href='Ip.aspx'>总访问报表</a></li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" class="padding5">
        <div class="input-group" style="width: 280px;margin-left:5px;float:left;">
		    <asp:DropDownList ID="FilterType_Drop" runat="server" CssClass="form-control" Style="width: 90px;border-right:none;">
		        <asp:ListItem Selected="True" Value="UserName">会员名</asp:ListItem>
		        <asp:ListItem Value="Source">来源</asp:ListItem>
		    </asp:DropDownList>
		    <asp:TextBox ID="IDName" runat="server" CssClass="form-control" style="width:150px;border-right:none;"></asp:TextBox>
		    <span class="input-group-btn">
		    <asp:LinkButton runat="server" ID="Search_Btn" OnClick="Search_Btn_Click" CssClass="btn btn-default"><span class="fa fa-search"></span></asp:LinkButton>
		    </span> 
        </div>
    </div>
</ol>
    <h3>
        <small>
            <a style="float: right;" href="counter.aspx">[返回]</a>访客累计：<asp:Label ID="SumCount_L" runat="server"></asp:Label>
        </small>
    </h3>
    <div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" PageSize="15" AllowPaging="true"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging">
            <Columns>
                <asp:BoundField ControlStyle-CssClass="td_s" HeaderText="ID" DataField="ID" />
                      <asp:TemplateField HeaderText="来源">
                          <ItemTemplate>
                              <%#Eval("Source") %>
                          </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="用户">
                          <ItemTemplate>
                              <%#Eval("UserName") %>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="IP">
                          <ItemTemplate>
                              <%#Eval("IP") %>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="操作系统">
                          <ItemTemplate>
                              <%#Eval("OSVersion") %>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="设备">
                          <ItemTemplate>
                              <%#Eval("Device") %>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="浏览器版本">
                          <ItemTemplate>
                              <%#Eval("BrowerVersion") %>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="创建时间">
                          <ItemTemplate>
                              <%#Eval("CDate") %>
                          </ItemTemplate>
                      </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $(function () {
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
        })
    </script>
</asp:Content>
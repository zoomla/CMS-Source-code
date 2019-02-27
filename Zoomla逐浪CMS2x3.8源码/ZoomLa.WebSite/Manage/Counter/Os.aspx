<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Os.aspx.cs" Inherits="manage_Counter_Os" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>操作系统统计报表</title><style>.allchk_l{display:none;}</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container-fluid">
        <div class="col-md-2 col-lg-2 padding0">
            <div class="panel panel-info">
                <div class="panel-heading">
                <h3 class="panel-title"><i class="fa fa-gears"></i> 操作系统统计</h3>
                </div>
                <ul class="list-group">
                <asp:Repeater ID="CountRPT" runat="server">
                    <ItemTemplate>
                        <li class="list-group-item"><%#GetOSInfo() %><span class="badge" title="访问量"><%#Eval("NUM") %></span></li>
                    </ItemTemplate>
                </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="col-md-10 col-lg-10">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" PageSize="15" AllowPaging="true"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" >
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
    </div>    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Counter.js" type="text/javascript"></script>
</asp:Content>
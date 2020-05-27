<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Mail.Contact"MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
    $().ready(function () {
        $("#<%=EGV.ClientID%>  tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
                $("#chkAll").click(function () { selectAllByName(this, "idChk"); });
            });
            $().ready(function () {
                $("tr:gt(1):not(:last)").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
            });
    </script>
<style type="text/css">
#EGV{ margin-top:10px;} 
</style>
    <title>联系人</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="main" style="margin:10px;">  
    <div class="input-group text_md">
    <asp:TextBox runat="server" ID="searchText" placeholder="请输入会员名,工号,部门" CssClass="form-control" />
    <span class="input-group-btn">
    <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click"/>
    </span>
    </div>          
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="98%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
                <Columns>
                   <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                       <ItemTemplate>
                           <input type="checkbox" name="idChk" value="<%#Eval("UserID") %>" />
                       </ItemTemplate>
                   </asp:TemplateField>
                     <asp:BoundField HeaderText="工号" DataField="WorkNum"/>
                     <asp:BoundField HeaderText="用户名" DataField="HoneyName" />
                     <asp:TemplateField HeaderText="真实名称">
                        <ItemTemplate>
                           <%# GetGN(Eval("TrueName")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="所属部门">
                        <ItemTemplate>
                           <%#GetStuName() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField HeaderText="手机号码" DataField="Mobile" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="MessageSend.aspx?uid=<%#Eval("UserID") %>" title="发送邮件">邮件</a>
                            <a href="Mobile.aspx?MB=<%#Eval("Mobile") %>" title="发送短信">短信</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                   </Columns>
                    <PagerStyle HorizontalAlign="Center"/>
                   <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        <div style="margin-top:10px;">
        <asp:Button runat="server" ID="batMsgBtn" CssClass="btn btn-primary" Text="批量邮件" OnClick="batMsgBtn_Click" />
        </div>
        </div>
</asp:Content>



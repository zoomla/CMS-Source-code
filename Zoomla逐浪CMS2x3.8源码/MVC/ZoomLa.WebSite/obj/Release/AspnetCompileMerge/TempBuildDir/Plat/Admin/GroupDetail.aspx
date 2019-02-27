<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupDetail.aspx.cs" Inherits="ZoomLaCMS.Plat.Admin.GroupDetail"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
 <title>部门详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
<div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">部门成员</span></div>
<ul class="nav nav-tabs">
<li><a href="#tab0" data-toggle="tab" onclick="location='GroupDetail.aspx?<%:"ID="+Gid+"&MType=0" %>';">成员信息</a></li>
<li><a href="#tab0" data-toggle="tab" onclick="location='GroupDetail.aspx?<%:"ID="+Gid+"&MType=1" %>';">部门管理员</a></li>
<li><a href="#tab1" data-toggle="tab">部门信息</a></li>
</ul>
  <div class="tab-content" style="margin-top:5px;">
    <div id="tab0" class="tab-pane active">
      <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
	class="table table-striped table-bordered table-hover" EmptyDataText="该部门尚无成员!!"
	OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
        <Columns>
        <asp:TemplateField>
          <ItemTemplate><input type="checkbox" name="idChk" value="<%#Eval("UserID") %>" /></ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="成员名" DataField="TrueName" />
        <%--<asp:BoundField HeaderText="职务" DataField=""/>--%>
        <asp:BoundField HeaderText="手机" DataField="Mobile" />
        <asp:TemplateField HeaderText="操作">
          <ItemTemplate>
            <asp:LinkButton runat="server" CommandName="Del2" CommandArgument='<%#Eval("UserID") %>' OnClientClick="return confirm('你确定要移除吗!');" ToolTip="移除"> <span class="fa fa-trash-o" style="color:#7D98A1;"></span></asp:LinkButton>
            <a href="#" title="编辑"><span class="fa fa-pencil" style="color: #7D98A1;"></span></a> </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center" />
      </ZL:ExGridView>
      <div style="margin-top: 10px;">
        <asp:Button runat="server" ID="BatRemove_Btn" Text="批量移除" CssClass="btn btn-primary" OnClick="BatRemove_Btn_Click" />
      </div>
    </div>
  </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
</asp:Content>
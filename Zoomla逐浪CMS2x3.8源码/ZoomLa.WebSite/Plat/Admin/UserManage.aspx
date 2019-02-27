<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="Plat_Admin_Default" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>用户管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
<div class="child_head"> <span class="child_head_span1" style="margin-bottom:-3px;"></span> <span class="child_head_span2">邀请注册</span> </div>
<div style="padding-top:10px;">
<div class="col-lg-7"> <span>使用邮箱地址邀请用,号隔开：</span>
  <div>
	<asp:TextBox runat="server" ID="BatEmail_T" CssClass="form-control"  placeholder="请输入Email地址用,号分隔" TextMode="MultiLine" style="height:150px;min-width:500px;margin-bottom:5px;" />
	<br />
	<asp:Button runat="server" ID="BatEmail_Btn" Text="邀请注册" OnClick="BatEmail_Btn_Click" CssClass="btn btn-primary" OnClientClick="return confirm('确定发送出邮件');" />
  </div>
</div>
<div runat="server" id="result_div" visible="false" style="position:absolute;left:10%;width:500px;padding:20px;padding-top:2px; background-color:#f0f0f0;z-index:10;border-radius:5px;">
  <button type="button" class="close" style="color:black;" title="关闭" onclick="$('#result_div').hide();"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
  <asp:Literal runat="server" EnableViewState="false" ID="SendResult_Lit" />
</div>
<div class="col-lg-5">
  <div>更多注册方式：</div>
  <div class="right_regdiv" title="文本导入">
	<div style="color:#0088cc;padding-bottom:5px;">文本导入</div>
	<div style="color:#ddd;">支持TXT,CSV,Excel</div>
  </div>
  <div class="right_regdiv" title="邀请链接" data-toggle="modal" data-target="#ulink_div">
	<div style="color:#0088cc;padding-bottom:5px;">获取邀请链接</div>
	<div style="color:#ddd;">您可以直接复制到公司全员邮件或QQ群等，实现快速邀请</div>
  </div>
  <div style="clear:both;"></div>
</div>
<div style="clear: both;"></div>
</div>
<div>
<div class="child_head"> <span class="child_head_span1" style="margin-bottom:-3px;"></span> <span class="child_head_span2">用户列表</span> </div>
<div class="input-group nav_searchDiv margin_t5">
  <asp:TextBox runat="server" ID="Search_T" class="form-control text_300" placeholder="请输入需要搜索的内容" />
  <span class="input-group-btn" style="width:auto;">
  <asp:LinkButton runat="server" CssClass="btn btn-default" OnClick="Search_Btn_Click" ID="Search_Btn"><span class="fa fa-search"></span></asp:LinkButton>
  </span> </div>
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
		class="table table-striped table-bordered table-hover margin_t5" EmptyDataText="当前没有信息!!"
		OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
  <Columns>
  <asp:TemplateField ItemStyle-CssClass="td_s"><ItemTemplate><input type="checkbox" name="idchk" value='<%# Eval("UserID") %>' /></ItemTemplate></asp:TemplateField>
  <asp:BoundField DataField="UserID" HeaderText="ID" SortExpression="UserID" ItemStyle-CssClass="td_s"></asp:BoundField>
    <asp:TemplateField HeaderText="头像">
        <ItemTemplate>
            <img class="img_s" src="<%#Eval("UserFace") %>" onerror="shownoface(this);" />
        </ItemTemplate>
    </asp:TemplateField>
  <asp:TemplateField HeaderText="会员名"><ItemTemplate> <%#Eval("UserName","{0}") %> </ItemTemplate></asp:TemplateField>
  <asp:BoundField HeaderText="真名" DataField="TrueName" />
  <asp:BoundField HeaderText="职务" DataField="Post" />
  <asp:BoundField HeaderText="手机" DataField="Mobile"/>
  <asp:TemplateField HeaderText="状态">
	<ItemTemplate> <%#GetStatus() %> </ItemTemplate>
  </asp:TemplateField>
  <asp:TemplateField HeaderText="加入时间">
	<ItemTemplate> <%#Eval("CreateTime","{0:yyyy年MM月dd日}") %> </ItemTemplate>
  </asp:TemplateField>
  <asp:TemplateField HeaderText="操作">
	<ItemTemplate> 
        <a href="/Plat/blog/?Uids=<%#Eval("UserID") %>" target="_blank" title="查看他的工作流"><span class="fa fa-eye"></span></a>
        <a href="/Plat/UPCenter.aspx?uid=<%#Eval("UserID") %>" title="修改" style="margin-left:5px;"><span class="fa fa-pencil"></span></a>
	  <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("UserID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除" style="margin-left:5px;"> <span class="fa fa-trash-o"></span></asp:LinkButton>
	</ItemTemplate>
  </asp:TemplateField>
  </Columns>
</ZL:ExGridView>
<asp:Button runat="server" ID="Audit_Btn" Text="批量激活" CssClass="btn btn-primary" OnClick="Audit_Btn_Click" />
<asp:Button runat="server" ID="UnAudit_Btn" Text="批量禁止" CssClass="btn btn-primary" OnClick="UnAudit_Btn_Click" />
<asp:Button runat="server" ID="BatDel_Btn" Text="批量移除" CssClass="btn btn-primary" OnClientClick="return confirm('你确定要删除吗!');" OnClick="BatDel_Btn_Click" />
</div>
<div class="modal fade" id="ulink_div" style="margin-top:20%;width:1000px;">
<div class="modal-dialog">
  <div class="modal-content">
	<div class="modal-header">
	  <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
	  <span class="modal-title">邀请链接</span> </div>
	<div class="modal-body">
	  <asp:TextBox runat="server" ID="ULink_L" TextMode="MultiLine" style="width:98%;max-width:100%;height:100px;" CssClass="form-control"></asp:TextBox>
	</div>
	<div class="modal-footer">
	  <button type="button" class="btn btn-primary" data-dismiss="modal">关闭</button>
	</div>
  </div>
</div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
#emails_ul li { margin-bottom: 5px; list-style-type: none; }
.right_regdiv{border:1px solid #0088cc;width:200px;height:120px;cursor:pointer;float:left;text-align:center;padding:10px 10px 0 10px; margin-right:20px;}
</style>
</asp:Content>
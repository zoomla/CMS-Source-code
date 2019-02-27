<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminManage.aspx.cs" Inherits="ZoomLaCMS.Manage.User.AdminManage"MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>管理员管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="BreadDiv" class="container-fluid mysite" style="margin-bottom:10px;">
        <div class="row">
            <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
                <li><a href="<%=CustomerPageAction.customPath2 %>Main.aspx">工作台</a></li>
                <li><a href="UserManage.aspx">用户管理</a></li>
                <li>管理员管理  <a href="AddManage.aspx">[添加管理员]</a> 
                </li>
				<div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a><a onclick="help_show('102')" title="帮助" class="help_btn"><span class="fa fa-question-circle"></span></a></div>
            <div id="sel_box" class="padding5">
                <div class="input-group sel_box">
                    <asp:TextBox runat="server" ID="Search_T" class="form-control" placeholder="检索当前位置" />
                    <span class="input-group-btn">
                         <asp:LinkButton runat="server" CssClass="btn btn-default" ID="Search_Btn" OnClick="Search_Btn_Click">搜索</asp:LinkButton>
                            <asp:Button runat="server" ID="Search_Btn_Hid" OnClick="Search_Btn_Click" style="display:none;" />
                    </span>
                </div>
                </div> 
            </ol>
        </div>
    </div>
    <ul class="nav nav-tabs">
        <li data-tag="-1"><a href="AdminManage.aspx">全部</a></li>
        <li data-tag="0"><a href="AdminManage.aspx?islock=0">已审核</a></li>
        <li data-tag="1"><a href="AdminManage.aspx?islock=1">未审核</a></li>
    </ul>
    <ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" OnPreRender="EGV_PreRender"
        class="table table-striped table-bordered table-hover" DataKeyNames="AdminID" OnPageIndexChanging="Egv_PageIndexChanging" 
        OnRowCommand="Lnk_Click" OnRowDataBound="Egv_RowDataBound">
        <Columns>
            <asp:TemplateField >
                <ItemTemplate>
                    <input  name="idchk" type="checkbox" title="全选" value="<%#Eval("AdminID") %>"  />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="AdminId" HeaderText="ID">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# ZoomLa.Common.DataConverter.CBool(DataBinder.Eval(Container, "DataItem.IsLock", "{0}")) ? "<span stytle='color:red;'>锁定</span>" : "正常"%> </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="管理员名">
                <ItemTemplate>
                    <a href="AddManage.aspx?id=<%#Eval("AdminId") %>"><%#Eval("AdminName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="真实姓名" DataField="AdminTrueName" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="管理员角色" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td_m">
                <ItemTemplate>
                 <%#GetRoleName() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="关联会员" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%#GetUserName() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最近IP">
                <ItemTemplate>
                    <div class="td_m" style="overflow:hidden;"><%#GetIpLocation(Eval("LastLoginIP").ToString()) %></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="上次修改密码时间" DataField="LastModifyPwdTime" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="创建时间" DataField="CDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd HH:mm}"/>
            <asp:BoundField HeaderText="最近活跃时间" DataField="LastLogoutTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField HeaderText="登录次数" DataField="LoginTimes"  ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="AddManage.aspx?ID=<%#Eval("AdminID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CommandName="DeleteAdmin" OnClientClick="return confirm('确实删除此管理员?');" CommandArgument='<%# Eval("AdminId")%>' CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <a href="RegionManage.aspx?guid=<%#Eval("AdminId") %>_<%#Eval("AdminName") %>" class="option_style"><i class="fa fa-key" title="权限"></i>区域订单权限</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div class="clearbox"></div>
    <asp:Button ID="Lock_B" runat="server" CssClass="btn btn-info" Text="批量锁定" OnClick="Lock_B_Click" />
    <asp:Button ID="UnLock_B" runat="server" CssClass="btn btn-info" Text="批量解锁" OnClick="UnLock_B_Click" />
    <asp:Button ID="Auit_B" runat="server" CssClass="btn btn-primary" Text="审核通过" OnClientClick="return CheckEmail()" OnClick="Auit_B_Click"  />
    <asp:Button ID="Button2" class="btn btn-info" runat="server" Text="批量删除" OnClick="Button2_Click" OnClientClick="return confirm('确定删除？')" />
    <asp:HiddenField ID="IsEmail_Hid" runat="server" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
	$(function () {
	    $(".nav-tabs [data-tag='<%=IsLock %>']").addClass('active');
	    $("#Search_T").keypress(function () {
	        if (event.keyCode == 13)
	            $("#Search_Btn_Hid").click();
	    });
	});
	function selectAllByName(obj, name) {
		var allInput = document.getElementsByName(chkSel);
		var loopTime = allInput.length;
		for (i = 0; i < loopTime; i++) {
			if (allInput[i].type == "checkbox") {
				allInput[i].checked = this.checked;
			}
		}
	}
	function CheckEmail() {
	    if ($("input[name=idchk]:checked").length < 1) { alert("未选择管理员"); return false; }
	    if (confirm("是否同时给选中用户发送通知邮件?")) 
	        $("#IsEmail_Hid").val('1');
	    else
	        $("#IsEmail_Hid").val('0');
	    return true;
	}
	function IsSelectedId() {
	    return $("input:checkbox[name=chkSel]:checked").length > 0;
	}
	$("#sel_btn").click(function(e) { 
		if($("#sel_box").css("display")=="none"){  
			$(this).addClass("active");
	        $("#sel_box").slideDown(300); 
		}
		else{
			$(this).removeClass("active");
			$("#sel_box").slideUp(200); 
		}
    });
 
</script>
</asp:Content>
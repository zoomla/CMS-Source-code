<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResList.aspx.cs" Inherits="Manage_Design_ResList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
    #Useage_Rad{display:inline-block;margin-right:15px;}
    </style>
    <title>资源管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li class='active'>资源管理 <a href='AddRes.aspx'>[上传资源]</a></li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block;">
            <asp:RadioButtonList ID="Useage_Rad" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                <asp:ListItem Text="动力模块" Value="bk_pc"></asp:ListItem>
                <asp:ListItem Text="H5场景" Value="bk_h5"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div style="display: inline-block; width: 230px;">
            <div class="input-group">
                <asp:TextBox ID="Skey_T" placeholder="资源名称" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<div id="template" runat="server">
    <ul class="nav nav-tabs hidden-xs hidden-sm">
        <li class="active"><a href="#tab_all" data-toggle="tab" onclick="ShowTabs('');">全部</a></li>
	    <li><a href="#tab_img" data-toggle="tab" onclick="ShowTabs('img');">图片</a></li>
	    <li><a href="#tab_music" data-toggle="tab" onclick="ShowTabs('music');">音乐</a></li>
        <li><a href="#tab_icon" data-toggle="tab" onclick="ShowTabs('icon');">图标</a></li>
        <li><a href="#tab_shape" data-toggle="tab" onclick="ShowTabs('shape');">形状</a></li>
        <li><a href="#tab_text" data-toggle="tab" onclick="ShowTabs('text');">文字</a></li>
    </ul>
     <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
         <Columns>
             <asp:TemplateField ItemStyle-CssClass="td_s"><ItemTemplate><input type="checkbox" name="idchk" value='<%# Eval("ID") %>' /></ItemTemplate></asp:TemplateField>
             <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-CssClass="td_s" />
             <asp:TemplateField HeaderText="名称">
	            <ItemTemplate> 
                    <a href="AddRes.aspx?id=<%#Eval("ID") %>" title="修改"><%# Eval("Name") %></a>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="应用场景">
                 <ItemTemplate>
                     <a href="?useage=<%#Eval("Useage") %>&ztype=<%#ZType %>"><%# GetUseage(Eval("Useage").ToString()) %></a>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="资源类型">
	            <ItemTemplate> 
                    <a href="?ztype=<%#Eval("ZType") %>&useage=<%#Useage %>"><%# GetType(Eval("ZType").ToString()) %></a>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="资源" ItemStyle-CssClass="text_405">
                 <ItemTemplate>
                     <%#GetRes() %>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="资源状态">
                <ItemTemplate>
                    <%#GetStatus() %>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField HeaderText="上传时间" DataField="CDate" />
             <asp:TemplateField HeaderText="操作">
	            <ItemTemplate> 
                  <a href="AddRes.aspx?id=<%#Eval("ID") %>" title="修改" class="option_style"><span class="fa fa-pencil"></span></a>
	              <asp:LinkButton runat="server" CssClass="option_style" CommandName="Del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除"> <span class="fa fa-trash-o"></span>删除</asp:LinkButton>
	            </ItemTemplate>
             </asp:TemplateField>
         </Columns>
     </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function showtab(type) {
        if (!type || type == "") { type = "all"; }
        $("li a[href='#tab_" + type + "']").parent().addClass("active").siblings("li").removeClass("active");
    }
    var $audio = $('<audio id="bgm" src="" loop="" style="display: none; width: 0; height: 0;"></audio>');
    function play(url,it)
    {
        var $this = $(it);
        $audio.attr("src", url);
        $audio[0].play();
        $this.hide();
        $this.next().show();
    }
    function pause(it) {
        var $this = $(it);
        $audio[0].pause();
        $this.hide();
        $this.prev().show();
    }
    function ShowTabs(type) {
        location.href = 'ResList.aspx?ztype=' + type;
    }
    $("#sel_btn").click(function (e) {
        if ($("#sel_box").css("display") == "none") {
            $(this).addClass("active");
            $("#sel_box").slideDown(300);
            $("#template").css("margin-top", "44px");
        }
        else {
            $(this).removeClass("active");
            $("#sel_box").slideUp(200);
            $("#template").css("margin-top", "0px");
        }
    });
</script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WxAppManage.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.WxAppManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微信公众号管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" 
    EnableTheming="False" EnableModelValidation="True" OnRowCommand="Egv_RowCommand" OnRowDataBound="Egv_RowDataBound" EmptyDataText="暂无公众号信息!">
    <Columns>
        <asp:TemplateField HeaderText="管理微信" HeaderStyle-CssClass="td_m">
            <ItemTemplate>
                <div class="option_area dropdown">
                    <a class="option_style" href="javascript:;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars"></i>操作<span class="caret"></span></a>
                    <ul class="dropdown-menu" role="menu"> 
                        <li><a class="option_style" href="WelPage.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-globe"></i>欢迎语</a></li>
                        <li><a class="option_style" href="ReplyList.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-comments-o"></i>自动回复</a></li>
                        <li><a class="option_style" href="WxMaterial.aspx?appid=<%#Eval("ID") %>&type=image"><i class="fa fa-newspaper-o"></i>素材管理</a></li>  
                        <li><a class="option_style" href="SendWx.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-comment"></i>发送消息</a></li>  
                        <li><a class="option_style" href="MsgsSend.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-file-image-o"></i>图文群发</a></li>  
                        <li><a class="option_style" href="EditWxMenu.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-book"></i>菜单配置</a></li>   
                        <li><a class="option_style" href="WxUserList.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-users"></i>粉丝管理</a></li>
                        <li><a class="option_style" href="MsgTlpList.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-comment"></i>模板消息</a></li>      
                        <li><a class="option_style" href="WxPayConfig.aspx?appid=<%#Eval("ID") %>"><i class="fa fa-shopping-cart"></i>支付配置</a></li>          
                    </ul>
                    </div>
            </ItemTemplate>
        </asp:TemplateField>
<%--        <asp:TemplateField>
            <ItemTemplate>
                <input type="checkbox" value="<%#Eval("ID") %>" name="idchk" />
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:BoundField DataField="ID" HeaderText="ID" />
        <asp:TemplateField HeaderText="公众号名" ItemStyle-CssClass="opt_div">
            <ItemTemplate>
                <span ondblclick="show_opt('<%#Eval("ID") %>');"><%#Eval("Alias") %></span>
                <div class="opt_cnt">
					<div class="alert alert-info">
                    <strong>Secret：</strong><%#Eval("Secret") %> <br/> 
					<strong>Token：</strong><%#Eval("Token") %> 
					</div>
					<div class="opt_border"></div>
                </div> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="WxNo" HeaderText="微信公众号" />
        <asp:BoundField DataField="APPID" HeaderText="APPID" />
 
        <asp:BoundField DataField="CDate" HeaderText="创建时间" />
        <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="opt_div">
            <ItemTemplate> 
                <a class="option_style" href="WxConfig.aspx?id=<%#Eval("ID") %>"><span class="fa fa-pencil" title="编辑"></span></a>
                <asp:LinkButton runat="server" OnClientClick="return confirm('是否删除该项')" CommandName="Del" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><span class="fa fa-trash-o" title="删除"></span> 删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</ZL:ExGridView>
<div class="alert alert-info fade in margin_b2px">
    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
    <h4>提示!</h4>
    本页用于管理、关联微信公众帐号，如果还没有微信公众号可点击<a href="https://mp.weixin.qq.com/" target="_blank">微信公众平台</a>申请；
</div>
<asp:Button runat="server" ID="Dels_B" Visible="false" OnClick="Dels_B_Click" OnClientClick="return confirm('确定删除选中项?')" CssClass="btn btn-primary" Text="批量删除" />
<style>
.allchk_l {display:none;}
.opt_div{ position:relative;}
.opt_div .opt_cnt{ display:none; position:absolute; bottom:15px; width:900px; max-width:500%;    text-align:justify; text-decoration:inherit}
.opt_cnt .alert-info{ background:#fff; border-color:#fff;}
.opt_border{ position:absolute; left:20px; bottom:0px; width:0; height:0; border-width:10px; border-color:#fff rgba(0,0,0,0) rgba(0,0,0,0) rgba(0,0,0,0); border-style:solid; } 
</style>
<script>
    var $leftwx = $(parent.document.getElementById("LeftPanel"));
    var action = "<%=Request.QueryString["action"] %>";
    $(function () {
        if (action == "add") {
            AddWxApp('<%=Request.QueryString["id"] %>', '<%=Request.QueryString["alias"]%>');
            location.href = "WxAppManage.aspx";
        }
        $("#Egv tr").dblclick(function () {
            if ($(this).find("[name='idchk']")[0])
                window.location.href = "WxConfig.aspx?id="+$(this).find("[name='idchk']").val();
        });
    });
    //以下函数同步左边栏内容操作
    function DelWxApp(id) {
        $leftwx.find("[data-id='" + id + "']").remove();
        //parent.DelWxTree(id);
    }
    function AddWxApp(id,alias) {
        var $ul = $leftwx.find("#menu7_9_ul2");
        $ul.append("<li data-id='"+id+"'><a href='javascript:;' onclick='ShowMain(\"\",\"<%=customPath2 %>WeiXin/Home.aspx?appid="+id+"\");'>"+alias+"</a></li>");
    }
    function DelsWxApp(ids) {
        var idsarray = ids.split(',');
        for (var i = 0; i < idsarray.length; i++) {
            $leftwx.find("[data-id='" + idsarray[i] + "']").remove();
        }
    }
	$(".opt_div").mouseover(function(e){
		$(".opt_div .opt_cnt").hide();
		$(this).find(".opt_cnt").show();
	}).mouseout(function(e){
		$(this).find(".opt_cnt").hide();
	})
	function show_opt(obj){
		window.location="WxConfig.aspx?id="+obj;
	}
</script>
</asp:Content>

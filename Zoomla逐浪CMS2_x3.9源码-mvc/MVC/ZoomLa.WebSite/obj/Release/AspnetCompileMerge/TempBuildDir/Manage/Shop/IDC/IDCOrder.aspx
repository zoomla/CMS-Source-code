<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IDCOrder.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.IDC.IDCOrder" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>IDC订单管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div  class="container-fluid mysite">
<div class="row">
    <ol class="breadcrumb navbar-fixed-top">
        <li><a href='../../Shop/ProductManage.aspx'>商城管理</a></li>
        <li><a href='../../Shop/OrderList.aspx'>订单管理</a></li>
	<li class="active"><a href="<%=Request.RawUrl %>">IDC订单管理</a></li><li> <a href="IDCOrder_Add.aspx?<%=Request.QueryString %>">  <i class="fa fa fa-pencil"></i> 补订单</a></li>
        <div id="help" class="pull-right text-center"><a href="javascript:;" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
        <div id="sel_box" runat="server" class="padding5">
            <span class="pull-left">高级查询：
    <asp:DropDownList ID="souchtable" CssClass="form-control" Width="150" runat="server">
        <asp:ListItem Selected="True" Value="orderno">订单编号</asp:ListItem>
        <asp:ListItem Value="rename">客户名称</asp:ListItem>
        <asp:ListItem Value="proname">商品名称</asp:ListItem>
    </asp:DropDownList>
            </span>
            <div class="input-group pull-left" style="width: 300px;">
                <asp:TextBox runat="server" ID="souchkey" class="form-control" placeholder="请输入需要搜索的内容" />
                <span class="input-group-btn">
                    <asp:LinkButton runat="server" CssClass="btn btn-default" ID="souchok" OnClick="souchok_Click"><span class="fa fa-search"></span></asp:LinkButton>
                </span>
            </div>
        </div>
    </ol>
</div>
</div>

<ul class="nav nav-tabs hidden-xs hidden-sm">
      <li class="active"><a href="#tab_all" data-toggle="tab" onclick="ShowTabs('all')">全部订单</a></li>
	  <li><a href="#tab_normal" data-toggle="tab" onclick="ShowTabs('normal')">正常订单</a></li>
	  <li><a href="#tab_aboutex" data-toggle="tab" onclick="ShowTabs('aboutex')">30天内到期订单</a></li>
	  <li><a href="#tab_expired" data-toggle="tab" onclick="ShowTabs('expired')">过期订单</a></li>
    <li><a href="#tab_nopay" data-toggle="tab" onclick="ShowTabs('nopay')">未付款订单</a></li>
  </ul>
<div id="TableList" runat="server">
      <table id="EGV" class="table table-bordered table-hover">
          <tr>
              <td></td>
              <td>ID</td>
              <td>绑定域名</td>
              <td>会员</td>
              <td>商品名称</td>
              <td><asp:LinkButton ID="AddTime_Order" runat="server" OnClick="AddTime_Order_Click">生效时间</asp:LinkButton></td>
              <td><asp:LinkButton ID="EndTime_Order" runat="server" OnClick="EndTime_Order_Click">到期时间</asp:LinkButton></td>
              <td>是否到期</td>
        <%--      <td>金额</td>--%>
              <td>状态</td>
              <td>财务</td>
              <td>操作</td>
          </tr>
            <ZL:ExRepeater runat="server" ID="RPT" OnItemCommand="RPT_ItemCommand" BoxType="dp"
                PageSize="20" PagePre="<tr id='page_tr'><td><input type='checkbox' id='AllID_Chk'/></td><td colspan='11' id='page_td'>" PageEnd="</td></tr>">
                <ItemTemplate>
                    <tr ondblclick="location='IDCOrderInfo.aspx?ID=<%#Eval("ID") %>';">
                        <td><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></td>
                        <td><%#Eval("ID") %></td>
                        <td><a href="<%#"http://"+Eval("Domain") %>" title="访问" target="_blank"><%#Eval("Domain","").ToLower().Replace("www.","") %></a></td>
                        <td><a onclick="opentitle('../../User/Userinfo.aspx?id=<%#Eval("userId") %>','查看会员')" href="javascript:;" title="查看会员"><%#GetUsers(Eval("UserID",""))%></a></td>
                        <td><a href="../ShowProduct.aspx?id=<%#Eval("ProID") %>" target="_blank" title="点击浏览商品"><%#Eval("Proname") %></a></td>
                        <td><%#Eval("STime","{0:yyyy-MM-dd}") %></td>
                        <td><%#Eval("ETime","{0:yyyy-MM-dd}") %></td>
                        <td><%#IsExpire(Eval("ETime"),Eval("Paymentstatus")) %></td>
             <%--           <td><%#Eval("OrdersAmount","{0:f2}")%></td>--%>
                        <td><%#formatzt(Eval("OrderStatus",""),"0")%></td>
                        <td><%#formatzt(Eval("Paymentstatus",""),"1")%></td>
                        <td>
                            <a href="javascript:;" title="详情" data-content="<%#Eval("Ordermessage") %>" class="option_style needpop"><i class="fa fa-sticky-note-o"></i>详记</a>
                            <a href="javascript:;" title="内注" data-content="<%#Eval("Internalrecords") %>" class="option_style needpop"><i class="fa fa-pencil"></i>内注</a>
                            <%#GetOP() %>
                            <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>' CssClass="option_style" OnClientClick="return confirm('确定要删除该订单吗');"><i class="fa fa-trash-o"></i>删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                  <%--  <tr><td colspan="11"><span>实际金额合计:</span><span class="rd_red_l"><%#GetTotalSum() %></span></td></tr>--%>
                </FooterTemplate>
            </ZL:ExRepeater>
        <asp:HiddenField runat="server" ID="TotalSum_Hid" />
    </table>
        <asp:Button ID="Del_B" class="btn btn-primary" Text="批量删除" runat="server" OnClick="Del_B_Click" OnClientClick="if(!IsSelectByName('idchk')){alert('请选择内容');return false;}else{return confirm('不可恢复性删除数据,你确定将该数据删除吗？')}" />
       <asp:LinkButton ID="ExportExcel_B" runat="server" CssClass="btn btn-primary" OnClick="ExportExcel_B_Click"><span class="fa fa-file-excel-o"></span> 导出Excel</asp:LinkButton>
</div>
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    var userdiag = new ZL_Dialog();
    var diag = new ZL_Dialog();
    $(function () {
        $("#AllID_Chk").click(function () { $('input:checkbox[name=idchk]:enabled').each(function () { this.checked = $('#AllID_Chk')[0].checked; }); });
        $(".needpop").click(function () {
            var $this = $(this);
            diag.reload = true;
            diag.maxbtn = false;
            diag.isBigClose = true;
            diag.backdrop = false;
            diag.title = $this.attr("title");
            diag.body = '<textarea class="form-control" style="height:500px;border:none;">' + $this.data("content") + '</textarea>';
            diag.ShowModal();
        });
    })
    function opentitle(url, title) {
        userdiag.reload = true;
        userdiag.title = title;
        userdiag.url = url;
        userdiag.backdrop = true;
        userdiag.maxbtn = false;
        userdiag.ShowModal();
    }
    function CloseDiag() {
        userdiag.CloseModal();
        CloseComDiag();
    }
    //-------------------------
    function ShowTabs(type) {
	    location.href = 'IDCOrder.aspx?type='+type;
    }
    function showtab(type) {
        if (!type || type == '') { type = 'all'; }
        $("ul.nav li").removeClass('active');
        $("a[href='#tab_" + type + "']").parent().addClass("active");
    }
    $("#sel_btn").click(function () {
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

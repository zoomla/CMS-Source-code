<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Passengers.aspx.cs" Inherits="ZoomLaCMS.Cart.Form.Passengers" MasterPageFile="~/Cart/order.master" EnableViewStateMac="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <title>信息补充</title>
    </asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="head_div">
         <a href="/"><img src="<%=Call.LogoUrl %>" class="margin_l5" /></a>
        <div class="input-group pull-right skey_div">
            <input type="text" id="skey_t" placeholder="商品搜索" class="form-control skey_t" />
            <span class="input-group-btn">
                <input type="button" value="搜索" class="btn btn-default" onclick="skey();" />
            </span>
        </div>
    </div>
    <div class="container">
        <div class="panel panel-primary">
          <div class="panel-heading"><span class="fa fa-plane"></span><span class="margin_l5">旅游路线</span></div>
          <div class="panel-body">
              <table class="table table-bordered">
        <tr><td>旅游线路</td><td>出发时间</td><td>备注</td><td>单价</td><td>人数</td><td>合计</td></tr>
        <asp:Repeater runat="server" ID="ProList_RPT">
            <ItemTemplate>
                <tr><td><%#Eval("ProName") %></td>
                <td><%#Eval("GoDate","{0:yyyy年MM月dd日 HH:mm}") %></td>
                <td><%#Eval("Remind") %></td>
                <td><%#Eval("Price","{0:f2}") %></td>
                <td><%#Eval("ProNum") %></td>
                <td><%#Eval("AllMoney","{0:f2}") %></td></tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr><td colspan="6"><span>总计:</span><span runat="server" id="AllMoney_sp" class="r_red_mid"></span></td></tr>
    </table>
          </div></div>
  <%--  <div><strong>保险数量:</strong><asp:TextBox runat="server" ID="Safenum_T" CssClass="form-control" /></div>--%>
    <div class="panel panel-primary">
        <div class="panel-heading"><span class="fa fa-user"></span><span class="margin_l5">旅客信息</span> <div class="pull-right"></div> <button type="button" class="btn btn-info" onclick="ShowSelCustomer()" style="color:#fff;"><span class="fa fa-plus"></span> 选择客户</button></div>
        <div class="panel-body">
            <ul id="user_ul">
        <li>
            <table class='table table-bordered'>
                <tr><td rowspan='2' class='r_green_mid min'>旅客<span class='num'>1</span></td><td><input type='text' class='form-control text_300 required' name='name_t_1' placeholder='姓名' />
                    <span class='btn btn-default fa fa-plus' onclick='AddGuest();'></span></td></tr>
                <tr><td><select name='certtype_sel_1' class='form-control min selnum'>
                    <option value='1' selected='selected'>身份证</option>
                    <option value='2'>护照</option>
                    <option value='3'>学生证</option>
                    <option value="4">其它证件</option>
                        </select><input type='text' class='form-control text_300 margin_l5 required digits IDCards' name='certcode_t_1' placeholder='证件号' /></td></tr>
            </table>
        </li>
    </ul></div></div>
     <div class="panel panel-primary">
          <div class="panel-heading"><span class="fa fa-user"></span><span class="margin_l5">联系人信息</span></div>
          <div class="panel-body">
              <table class="table table-bordered">
         <tr><td class="tdleft">姓名:</td><td><input type="text" class="form-control text_300 required" name="c_name_t" /></td></tr>
         <tr><td>手机:</td><td><input type="text" class="form-control text_300 required phones" name="c_mobile_t" /></td></tr>
         <tr><td>地址:</td><td><input type="text" class="form-control text_300 required" name="c_address_t" /></td></tr>
         <tr><td></td><td><asp:Button runat="server" ID="Save_Btn" Text="预订" CssClass="btn btn-primary" OnClientClick="return CheckSubmit();" /></td></tr>
    </table>
              </div></div>
     
    <input type="hidden" id="Pros_Hid" name="Pros_Hid" />
    <input type="hidden" id="Guest_Hid" name="Guest_Hid"/>
    <input type="hidden" id="Contract_Hid" name="Contract_Hid"/>
    <!--修改时调用-->
    <input type="hidden" id="Guest_Hid2" runat="server" />
    <input type="hidden" id="Contract_Hid2" runat="server" />
    <input type="hidden" id="IDS_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
       .tdleft{width:120px;}
       .panel-primary{margin-top:10px;}
    </style>
    <script>
        $("#Pros_Hid").val('<%=Pros%>');
        var guestName = "旅客";
    </script>
    <script type="text/javascript" src="/JS/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/JS/ICMS/ZL_Cart.js"></script>
    <script>
        var diag = new ZL_Dialog();
        function ShowSelCustomer() {
            diag.title = "选择客户";
            diag.url = "SelCustomer.aspx";
            diag.maxbtn = false;
            diag.ShowModal();
        }
        function CloseCurDialog() {
            diag.CloseModal();
        }

    </script>
</asp:Content>
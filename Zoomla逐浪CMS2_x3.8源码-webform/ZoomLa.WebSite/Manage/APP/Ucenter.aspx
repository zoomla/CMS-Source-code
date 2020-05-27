<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ucenter.aspx.cs" Inherits="manage_APP_Ucenter" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>跨站接入1.0</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table id="EGV"  class="table table-striped table-bordered table-hover">
     <tr class="tabtitle">
      <th width="3%"></th>
      <th width="3%">ID</th>
      <th>授权网址</th>
      <th> 添加用户</th>
      <th> 删除用户</th>
      <th> 更新用户资料</th>
      <th>接受订单</th>
     </tr>
    <ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='7' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemCommand="Repeater1_ItemCommand">
      <ItemTemplate>
        <tr class="tdbg">
          <td><input type="checkbox" name="Btchk" id="Btchk" value='<%# Eval("ID") %>' /></td>
          <td><%# Eval("ID") %></td>
          <td ondblclick="UpadateSite('<%#Eval("ID") %>')" title="双击修改授权网址" ><a href="http://<%# Eval("WebSite") %>" target="_blank"><%# Eval("WebSite") %></a></td>
          <td><asp:ImageButton ID="Add_lbk" runat="server" CommandName="Add" CommandArgument='<%#Eval("ID") %>' ImageUrl='<%# "~/Images/" + GetState(Eval("IsAdd", "{0}")) +".gif" %>' OnClick="ImageButton_Click"/></td>
          <td><asp:ImageButton ID="Del_lbk" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>' ImageUrl='<%# "~/Images/" + GetState(Eval("IsDel", "{0}")) +".gif" %>' OnClick="ImageButton_Click"/></td>
          <td><asp:ImageButton ID="imgbtnCanCopy" runat="server" CommandName="Update" CommandArgument='<%#Eval("ID") %>' ImageUrl='<%# "~/Images/" + GetState(Eval("IsUpdate", "{0}")) +".gif" %>' OnClick="ImageButton_Click"/></td>
          <td><asp:ImageButton ID="Order_lbk" runat="server" CommandName="Order" CommandArgument='<%#Eval("ID") %>' ImageUrl='<%# "~/Images/" + GetState(Eval("IsUpdate", "{0}")) +".gif" %>' OnClick="ImageButton_Click"/></td>
         </tr>
      </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:ExRepeater>
  </table>
 
    <table class="table table-striped table-bordered table-hover">
    <tr align="left">
      <td class="tdbg"><strong>使用说明：</strong>认证网站通过此认证后，可以分别就用户注册、用户密码修改、用户删除进行接口设定，其规则如：
        <p> 1、AJAX接入： </p>
           <div class="Tips"><span><a href="javascript:void(0)" id="less" onclick="less('Less','AjaxUrl')" title="收起">+</a></span>
                <ol id="AjaxUrl" style="display:none">
                <li>&lt;script src="http://code.z01.com/jquery/jquery.min-1.9.0.js" type="text/javascript"&gt;&lt;/script&gt;</li>
                 <li>&lt;script type="text/javascript"&gt;</li>
               <li> $.ajax({</li>
               <li>   url: "http://<%Call.Label("{$SiteURL/}"); %>/API/Ucenter.aspx?callback=?",</li>
               <li> type: "POST",</li>
               <li>   dataType: "jsonp",</li> 
               <li> jsonpCallback: "person",</li> 
               <li>    data: "uri=www.[Domain]&uname=yyyyyy&pwd=7fef6171469e80d32c0559f88b377245&tid=1",</li> 
               <li>//注：[Domain]变量为实际接入网址</li>
               <li>    success: function (msg) {</li> 
               <li>       switch (msg) {</li> 
               <li>         case 0: alert("无相应权限，请检查是否通过统一认证中心授权"); break;</li> 
               <li>         case 1: alert("认证成功"); break;</li> 
               <li>        case -1: alert("无添加权限"); break;</li> 
               <li>        case -2: alert("无修改权限"); break;</li> 
               <li>        case -3: alert("无删除权限"); break;</li> 
               <li>        case -4: alert("已存在此用户"); break;</li> 
               <li>       default: alert("认证失败" + msg); break;</li> 
               <li>   }</li> 
               <li>   }</li> 
             <li>    });</li> 
               <li>&lt;/script&gt;</li>
           </ol>
          <br /><a href="javascript:void(0)" onclick="copyToClipBoard('AjaxUrl')">复制</a> 
         </div>
            
       <p>2、 GET接入</p>
          <div class="Tips"><span> <a href="javascript:void(0)"  id="less01"  onclick="less('less01','GetUrl')" title="展开">+</a> </span>
              <ol  id="GetUrl" style="display:none">
                  <li>http://<%Call.Label("{$SiteURL/}"); %>/API/Ucenter.aspx?uri=www.[Domain].com&uname=UserName&pwd=7fef6171469e80d32c0559f88b377245&tid=1</li>
                  <li>&#60;&#33;&#45;&#45;注：[Domain]变量为实际接入网址&#45;&#45;&#62;</li>
              </ol><br />
               <a href="javascript:void(0)" onclick="copyToClipBoard('GetUrl')">复制</a>
           </div>
            <p>3、数据操作说明：系统一时通过验证，即可以通过传值来对相应参数更新操作，相应规则如下：</p>
          <div class="Tips">
              <span> <a href="javascript:void(0)"  id="less02"  onclick="less('less02','datecont')" title="展开">+</a> </span>
            <div  id="datecont" style="display:none">
            <p>uri:域名</p> 
            <p>uname:用户名</p> 
            <p>pwd:加密后的密码（MD5加密）</p> 
            <p>tid:操作方法，1即添加,为2即修改，为3即删除</p> 
           </div>
          </div>
            <p>4、 事件与调试</p>
            <div class="Tips">
                <span> <a href="javascript:void(0)"  id="less03"  onclick="less('less03','Val')" title="展开">+</a> </span>
             <div  id="Val" style="display:none">
            <p>系统的API/Ucenter.aspx页面会根据认证请求输出相应的值，约定如下：</p>
            <p>返回0：无权限，请检查是否通过统一认证中心授权</p>
            <p>返回-1：无添加权限</p>
            <p>返回-2：无修改权限</p>
            <p>返回-3：无删除权限</p>
            <p>返回-4：已存在此用户</p>
            </div>
            </div>
           <p>5、插入订单AJAX接入： </p>
           <div class="Tips"><span><a href="javascript:void(0)" id="A1" onclick="less('A1','Ol1')" title="收起">+</a></span>
                <ol id="Ol1" style="display:none">
                <li>&lt;script src="http://code.z01.com/jquery/jquery.min-1.9.0.js" type="text/javascript"&gt;&lt;/script&gt;</li>
                 <li>&lt;script type="text/javascript"&gt;</li>
               <li> $.ajax({</li>
               <li>   url: "http://<%Call.Label("{$SiteURL/}"); %>/API/Ucenter.aspx?callback=?",</li>
               <li> type: "POST",</li>
               <li>   dataType: "jsonp",</li> 
               <li> jsonpCallback: "person",</li> 
               <li>    data: "uri=uri=www.z01.com&shopid=1&Price=12&user=admin",</li> 
               <li>//注：[Domain]变量为实际接入网址</li>
               <li>    success: function (msg) {</li> 
               <li>       switch (msg) {</li> 
               <li>        case 0: alert("该域名未通过验证"); break;</li> 
               <li>         case 1: alert("该域名不可接受订单"); break;</li> 
               <li>       case 2: alert("不存在此用户"); break;</li> 
               <li>        case 3: alert("用户积分不足"); break;</li> 
               <li>        case 4: alert("商品插入失败"); break;</li> 
               <li>        case 5: alert("订单插入成功"); break;</li> 
               <li>       default: alert("插入失败" + msg); break;</li> 
               <li>   }</li> 
               <li>   }</li> 
             <li>    });</li> 
               <li>&lt;/script&gt;</li>
           </ol>
          <br /><a href="javascript:void(0)" onclick="copyToClipBoard('Ol1')">复制</a> 
         </div>
          <p>6、插入订单GET接入</p>
          <div class="Tips"><span> <a href="javascript:void(0)"  id="A2"  onclick="less('A2','Ol2')" title="展开">+</a> </span>
              <ol  id="Ol2" style="display:none">
                  <li>http://<%Call.Label("{$SiteURL/}"); %>/ApI/Orders.aspx?uri=www.z01.com&shopid=1&Price=12&user=admin</li>
                  <li>&#60;&#33;&#45;&#45;注：[Domain]变量为实际接入网址&#45;&#45;&#62;</li>
              </ol><br />
               <a href="javascript:void(0)" onclick="copyToClipBoard('Ol2')">复制</a>
           </div>
          <p>7、系统的/ApI/Orders.aspx页面会根据认证请求输出相应的值，约定如下：</p>
          <p>返回0: 该域名未通过验证</p>
            <p>1:该域名不可接受订单</p>
            <p>2:不存在此用户</p>
            <p>3:用户积分不足</p>
            <p>4: 商品插入失败</p>
			<p>5:订单插入成功</p>
        </td>
    </tr>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function IsSelectedId() {
            var flag = false;
            var s = document.form1.getElementsByTagName("INPUT")

            for (var i = 0; i < s.length; i++) {
                if (s[i].type == "checkbox") {
                    if (s[i].checked) {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
        function UpadateSite(id) {
            window.open('/manage/APP/AddUcenter.aspx?ID=' + id, 'main_right');
        }
        function copyToClipBoard(id) {
            var clipBoardContent = document.getElementById(id).innerText;
            window.clipboardData.setData("Text", clipBoardContent);
            alert("复制成功");
        }
        function less(id1, id2) {
            var no = document.getElementById(id2).style.display;
            if (no == 'none') {
                document.getElementById(id2).style.display = "block";
                document.getElementById(id1).innerHTML = "-";
                document.getElementById(id1).title = "收起";
            }
            else {
                document.getElementById(id2).style.display = "none";
                document.getElementById(id1).innerHTML = "+";
                document.getElementById(id1).title = "展开";
            }

        }
</script>
</asp:Content>
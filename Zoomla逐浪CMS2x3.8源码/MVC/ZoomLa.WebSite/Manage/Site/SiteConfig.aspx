<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.SiteConfigPage"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title><%=Call.SiteName%>_控制中心</title>
<link rel="stylesheet" href="/Plugins/Domain/css/style.css" type="text/css" media="all" />
<link rel="stylesheet" href="/Plugins/Domain/css/css.css" type="text/css" />
<script type="text/javascript" src="/Plugins/Domain/Site.js"></script>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="n_site_bread">
<ul class="breadcrumb">
<li><a href="<%= CustomerPageAction.customPath2 %>/Main.aspx">工作台</a></li>
<li><a href="Default.aspx">站群中心</a></li>
<li class="active"><a href="<%=Request.RawUrl %>">全局配置</a></li>
</ul> 
</div> 
    <div id="site_main" class="padding5" style="padding-bottom: 0;">
        <span runat="server" id="remind" style="color: red;" />
        <div id="editDiv">
            <ul style="margin-bottom: 0;">
                <li>
                    <input type="button" value="编辑" onclick="dis('tab4');" class="btn btn-primary" /></li>
            </ul>
        </div>
        <div id="tab4" style="display: none;">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding5">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#tabBody0" data-toggle="tab">基本设置</a></li>
                    <li><a href="#tabBody1" data-toggle="tab">智能建站</a></li>
                </ul>
                <table class="site_border tab-content">
                    <tbody id="tabBody0" class="tabBody tab-pane active">
                        <tr>
                            <td style="width:130px;"><span>超级管理员用户：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="adminName" autocomplete="off" CssClass="form-control text_300" />
                            </td>
                        </tr>
                        <tr>
                            <td><span>超级管理员密码：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="adminPasswd" TextMode="Password" autocomplete="off" CssClass="form-control text_300" />
                                <asp:Button runat="server" ID="chkBtn" Text="校验用户" OnClick="chkBtn_Click" Style="margin-left: 2px;" CssClass="btn btn-primary" />
                                <asp:Button runat="server" ID="logBtn" Text="保存用户" OnClick="logBtn_Click" Style="margin-left: 2px;" CssClass="btn btn-primary" />
                            </td>
                        </tr>
                        <tr>
                            <td><span>预设 站 点 URL：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="defaultIP" CssClass="form-control text_300" /></td>
                        </tr>
                        <tr>
                            <td><span>模型ID：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="modelIDT" CssClass="form-control text_300" />&nbsp;&nbsp;<span style="color: green">注:IDC商城所绑定的模型ID</span></td>
                        </tr>
                        <tr>
                            <td><span>商 城 节 点：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="nodeIDT" CssClass="form-control text_300" />&nbsp;&nbsp;<span style="color: green">注:IDC商城所绑定的节点ID</span></td>
                        </tr>
                        <tr>
                            <td><span>新网代理用户：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="newNetClientID" CssClass="form-control text_300" />&nbsp;&nbsp;<span style="color: green;">注:以agent开头的用户</span></td>
                        </tr>
                        <tr>
                            <td><span>新网API密码：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="newNetApiPasswd" TextMode="Password"
                                    onkeypress="if(event.keyCode==13){EnterFunc2();}" autocomplete="off" CssClass="form-control text_300" />&nbsp;&nbsp;<span style="color: green;">注:在API设置中配置,非登录密码</span></td>
                        </tr>
                        <tr>
                            <td><span>DNS：</span></td>
                            <td>
                                <input type="radio" name="dnsOption" value="0" checked="checked" onclick="showDiv(0);" />新网默认DNS
        <input type="radio" name="dnsOption" value="1" onclick="showDiv(1);" />自定义DNS</td>
                        </tr>
                        <tr>
                            <td><span>DNS文件目录：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="dnsOutputPath" MaxLength="50" CssClass="form-control text_300" /></td>
                        </tr>
                        <tr class="cdnsBody">
                            <td><span>DNS主服务器：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="dns1" MaxLength="30" CssClass="form-control text_300" /></td>
                        </tr>
                        <tr class="cdnsBody">
                            <td><span>DNS辅服务器：</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="dns2" MaxLength="30" CssClass="form-control text_300" /></td>
                        </tr>
                    </tbody>
                    <tbody id="tabBody1" class="tabBody tab-pane padding5">
                        <tr>
                            <td>二级域名：</td>
                            <td>
                                <asp:TextBox runat="server" ID="tDomNameT" MaxLength="50" CssClass="form-control text_300" /></td>
                        </tr>
                        <tr>
                            <td style="width:130px;">自动创建数据库：</td>
                            <td>
                                <asp:RadioButtonList runat="server" ID="autoCreatedbRadio" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="True">是</asp:ListItem>
                                    <asp:ListItem Value="False">否</asp:ListItem>
                                </asp:RadioButtonList><span class="rd_green">注:开启该项,允许用户创建站点时,自动生成对应数据库</span></td>
                        </tr>
                        <tr>
                            <td>数据库管理员帐号：</td>
                            <td>
                                <asp:TextBox runat="server" ID="dbm_NameT" MaxLength="20" CssClass="form-control text_300" /><br />
                                <span class="rd_green">注:推荐使用SA帐号,或拥有master数据库管理的帐号</span></td>
                        </tr>
                        <tr>
                            <td>数据库管理员密码：</td>
                            <td>
                                <asp:TextBox runat="server" ID="dbm_PasswdT" MaxLength="50" TextMode="Password" CssClass="form-control text_300" /></td>
                        </tr>
                    </tbody>
                </table>
                <div class="text-center margin_t5">
                    <asp:Button runat="server" ID="saveBtn4" Text="保存配置" CssClass="btn btn-primary" OnClick="saveBtn4_Click" />
                    <input type="button" value="取消" style="margin-left: 20px;" class="btn btn-primary" title="关闭编辑" onclick="dis('editDiv');" />
                </div>
            </div>
		<div class="tab col-lg-6 col-md-6 col-sm-6 col-xs-12 padding5">
            <div >
            <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#Div3" aria-controls="home" role="tab" data-toggle="tab">显示</a></li>
            <li role="presentation"><a href="#Div4" aria-controls="profile" role="tab" data-toggle="tab">选中</a></li> 
            </ul>
            <div class="tab-content selectbox">
            <div role="tabpanel" class="tab-pane active" id="Div3">
            <div class="site_border" style="padding-left:20px;">
            <p class="checkall0 allcheck"><label for="allChk">全选</label><input type="checkbox" class="allChk" onclick="selectAllByName(this, 'ext100');"/></p>
            <ul id="Ul3">
            <li><input type="checkbox" name="ext100" value=".com"   /><label for="e0">.com</label></li>
            <li><input type="checkbox" name="ext100" value=".net"    /><label for="e1">.net</label></li>
            <li><input type="checkbox" name="ext100" value=".cn"     /><label for="e2">.cn</label></li>
            <li><input type="checkbox" name="ext100" value=".com.cn" /><label for="e3">.com.cn</label></li>
            <li><input type="checkbox" name="ext100" value=".net.cn" /><label for="e4">.net.cn</label></li>
            <li><input type="checkbox" name="ext100" value=".org.cn" /><label for="e5">.org.cn</label></li>
            <li><input type="checkbox" name="ext100" value=".org"  /><label for="e6">.org</label></li>
            <li><input type="checkbox" name="ext100" value=".asia" /><label for="e7">.asia</label></li>
            <li><input type="checkbox" name="ext100" value=".cc"   /><label for="e8">.cc</label></li>
            <li><input type="checkbox" name="ext100" value=".biz"  /><label for="e9">.biz</label></li>
            <li><input type="checkbox" name="ext100" value=".info" /><label for="e10">.info</label></li>
            <li><input type="checkbox" name="ext100" value=".tv"   /><label for="e11">.tv</label></li>
            <li><input type="checkbox" name="ext100" value=".tw"   /><label for="e12">.tw</label></li>
            <li><input type="checkbox" name="ext100" value=".in"   /><label for="e13">.in</label></li>
            <li><input type="checkbox" name="ext100" value=".me"   /><label for="e14">.me</label></li>
            <li><input type="checkbox" name="ext100" value=".中国" /><label for="e15">.中国</label></li>
            <li><input type="checkbox" name="ext100" value=".pw"   /><label for="e16">.pw</label></li>
            <li><input type="checkbox" name="ext100" value=".公司" /><label for="c17">.公司</label></li>
            <li><input type="checkbox" name="ext100" value=".网络" /><label for="c18">.网络</label></li>
            </ul>
            <p style="display: block; border-top: 1px solid #D5E0EF; width: 99%; *zoom: 1; overflow: hidden; *height: 1px; margin: 20px 0"></p>
            <p class="checkall1 allcheck"><label for="allChk2">全选</label><input type="checkbox" id="Checkbox3" class="allChk" onclick="selectAllByName(this, 'ext101');"/></p>
            <ul id="Ul4">
            <li class="countryli"><input type="checkbox" name="ext101" value=".jl.cn" /><label for="m19">.jl.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".sh.cn" /><label for="m20">.sh.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".qh.cn" /><label for="m21">.qh.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".gx.cn" /><label for="m22">.gx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".ah.cn" /><label for="m23">.ah.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".sx.cn" /><label for="m24">.sx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".hk.cn" /><label for="m25">.hk.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".fj.cn" /><label for="m26">.fj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".xz.cn" /><label for="m27">.xz.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".hb.cn" /><label for="m28">.hb.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".hl.cn" /><label for="m29">.hl.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".tj.cn" /><label for="m30">.tj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".nx.cn" /><label for="m31">.nx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".hi.cn" /><label for="m32">.hi.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".jx.cn" /><label for="m33">.jx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".nm.cn" /><label for="m34">.nm.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".mo.cn" /><label for="m35">.mo.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".ac.cn" /><label for="m36">.ac.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".sn.cn" /><label for="m37">.sn.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".hn.cn" /><label for="m38">.hn.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".js.cn" /><label for="m39">.js.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".cq.cn" /><label for="m40">.cq.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".xj.cn" /><label for="m41">.xj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".sc.cn" /><label for="m42">.sc.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".sd.cn" /><label for="m43">.sd.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".ln.cn" /><label for="m44">.ln.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".yn.cn" /><label for="m45">.yn.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".bj.cn" /><label for="m46">.bj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".gs.cn" /><label for="m47">.gs.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".gd.cn" /><label for="m48">.gd.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".zj.cn" /><label for="m49">.zj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".he.cn" /><label for="m50">.he.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".tw.cn" /><label for="m51">.tw.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".gz.cn" /><label for="m52">.gz.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext101" value=".ha.cn" /><label for="m53">.ha.cn</label></li>
            <div class="clearfix"></div>
            </ul> 
            </div> 
            </div>
            <div role="tabpanel" class="tab-pane" id="Div4">
            <div class="site_border" style="padding-left:20px;">
            <p class="checkall0 allcheck"><label for="allChk">全选</label><input type="checkbox" id="Checkbox1" class="allChk" onclick="selectAllByName(this, 'ext');"/></p>
            <ul id="Ul1">
            <li><input type="checkbox" name="ext" value=".com"    /><label for="e0">.com</label></li>
            <li><input type="checkbox" name="ext" value=".net"    /><label for="e1">.net</label></li>
            <li><input type="checkbox" name="ext" value=".cn"     /><label for="e2">.cn</label></li>
            <li><input type="checkbox" name="ext" value=".com.cn" /><label for="e3">.com.cn</label></li>
            <li><input type="checkbox" name="ext" value=".net.cn" /><label for="e4">.net.cn</label></li>
            <li><input type="checkbox" name="ext" value=".org.cn" /><label for="e5">.org.cn</label></li>
            <li><input type="checkbox" name="ext" value=".org"  /><label for="e6">.org</label></li>
            <li><input type="checkbox" name="ext" value=".asia" /><label for="e7">.asia</label></li>
            <li><input type="checkbox" name="ext" value=".cc"   /><label for="e8">.cc</label></li>
            <li><input type="checkbox" name="ext" value=".biz"  /><label for="e9">.biz</label></li>
            <li><input type="checkbox" name="ext" value=".info" /><label for="e10">.info</label></li>
            <li><input type="checkbox" name="ext" value=".tv"   /><label for="e11">.tv</label></li>
            <li><input type="checkbox" name="ext" value=".tw"   /><label for="e12">.tw</label></li>
            <li><input type="checkbox" name="ext" value=".in"   /><label for="e13">.in</label></li>
            <li><input type="checkbox" name="ext" value=".me"   /><label for="e14">.me</label></li>
            <li><input type="checkbox" name="ext" value=".中国" /><label for="e15">.中国</label></li>
            <li><input type="checkbox" name="ext" value=".pw"   /><label for="e16">.pw</label></li>
            <li><input type="checkbox" name="ext" value=".公司" /><label for="c17">.公司</label></li>
            <li><input type="checkbox" name="ext" value=".网络" /><label for="c18">.网络</label></li>
            </ul>
            <p style="display: block; border-top: 1px solid #D5E0EF; width: 99%; *zoom: 1; overflow: hidden; *height: 1px; margin: 20px 0"></p>
            <p class="checkall1 allcheck"><label for="allChk2">全选</label><input type="checkbox" class="allChk" onclick="selectAllByName(this, 'ext2');"/></p>
            <ul id="Ul2">
            <li class="countryli"><input type="checkbox" name="ext2" value=".jl.cn" /><label for="m19">.jl.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".sh.cn" /><label for="m20">.sh.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".qh.cn" /><label for="m21">.qh.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".gx.cn" /><label for="m22">.gx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".ah.cn" /><label for="m23">.ah.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".sx.cn" /><label for="m24">.sx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".hk.cn" /><label for="m25">.hk.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".fj.cn" /><label for="m26">.fj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".xz.cn" /><label for="m27">.xz.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".hb.cn" /><label for="m28">.hb.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".hl.cn" /><label for="m29">.hl.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".tj.cn" /><label for="m30">.tj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".nx.cn" /><label for="m31">.nx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".hi.cn" /><label for="m32">.hi.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".jx.cn" /><label for="m33">.jx.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".nm.cn" /><label for="m34">.nm.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".mo.cn" /><label for="m35">.mo.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".ac.cn" /><label for="m36">.ac.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".sn.cn" /><label for="m37">.sn.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".hn.cn" /><label for="m38">.hn.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".js.cn" /><label for="m39">.js.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".cq.cn" /><label for="m40">.cq.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".xj.cn" /><label for="m41">.xj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".sc.cn" /><label for="m42">.sc.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".sd.cn" /><label for="m43">.sd.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".ln.cn" /><label for="m44">.ln.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".yn.cn" /><label for="m45">.yn.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".bj.cn" /><label for="m46">.bj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".gs.cn" /><label for="m47">.gs.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".gd.cn" /><label for="m48">.gd.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".zj.cn" /><label for="m49">.zj.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".he.cn" /><label for="m50">.he.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".tw.cn" /><label for="m51">.tw.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".gz.cn" /><label for="m52">.gz.cn</label></li>
            <li class="countryli"><input type="checkbox" name="ext2" value=".ha.cn" /><label for="m53">.ha.cn</label></li><div class="clearfix"></div>
            </ul><div class="clearfix"></div>
            </div>  
    </div> 
  </div>

	  
	</div>
</div>
</div>
<div class="clearfix"></div>
</div>
    <script type="text/javascript">
        $().ready(function () {
            $(":text").addClass("site_input");
            $(":password").addClass("site_input");
            $(":button").addClass("site_button");
            $(":submit").addClass("site_button");
        });
        function EnterFunc2() {
            $("#<%=saveBtn4.ClientID%>").trigger("click");
          }
          //设定值格式:key:value,key2,value:::下同
          function setValue(v) {
              var arr = v.split(",");
              var temp;
              for (i = 0; i < arr.length; i++) {
                  temp = arr[i].split(":");
                  $("#" + temp[0]).val(temp[1]);
              }
          }
        function dis(id)
        {
            $("#" + id).show().siblings("div").hide();
        }
        //DNS
        function showDiv(v) {
            if (v == "0") {
                $(".cdnsBody").hide();
            }
            else {
                $(".cdnsBody").show();
            }
        }
        //设置单选框的默认值
        function setRadio(v) {
            $("input[name='dnsOption'][value='" + v + "']").attr('checked', 'true');
            showDiv(v);
        }
    </script>
<div class="clearfix"></div>
    <div id="about" class="site_cont">
	<strong>欢迎使用Zoomla!逐浪CMS站群系统！</strong><br/>
Zoomla!逐浪CMS是面向高端用户开放的一代具备虚拟化与IDC标准的网站集群管理系统，在您使用前，需要确定并了解以下信息：<br/>
1、您是否拥有独立的服务器管理权限，从而以方便的接入多个站点管理单元。<br/>
2、您是否熟悉IIS的管理机制，我们的站群系统直接与IIS原生系统对接，托管windows的IIS管理机制并赋予了多项创新功能。<br/>
3、我们能够纳入非Zoomla!逐浪CMS构建的站点，且未来版本中将支持接入任意站点的数据交互，但不对非Zoomla!逐浪CMS站点构建数据安全性给予保障，我们提供免费迁移支持，点此获取逐浪CMS程序包。<br/>
4、如有问题请访问<a href="http://bbs.z01.com" target="_blank">bbs.z01.com</a>开发专区交流，官方微博<a href="http://weibo.com/zoomlacms" target="_blank">http://weibo.com/zoomlacms</a>。<br/>
5、推荐使用最低1024*768分辨以及最低IE9版本浏览器（兼容chrome、safri等主流浏览器）。
</div>
<%--<div style="width:100%;text-align:center;margin-bottom:200px;">
	<a href="Default.aspx" class="btn btn-lg btn-primary" style="color:#fff;"><i class="fa fa-arrow-circle-left"></i>返回主控站点内容管理</a>
	<span style="margin-right:360px;"><asp:CheckBox runat="server" ID="dPageChk" AutoPostBack="true" OnCheckedChanged="dPageChk_CheckedChanged"/><label for="<%=dPageChk.ClientID %>" style="position:relative;bottom:-2px;">下次自动略过</label> </span>
	</div>--%>
    </span>
</asp:Content>
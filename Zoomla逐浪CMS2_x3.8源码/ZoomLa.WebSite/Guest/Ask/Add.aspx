<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" ValidateRequest="false" Inherits="Guest_AskAdd" ClientIDMode="Static" EnableViewStateMac="false" MasterPageFile="~/Guest/Ask/Ask.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>在线提问_<%Call.Label("{$SiteName/}"); %>问答</title>
<script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<link href="/App_Themes/User.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
<ol class="breadcrumb">  
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Ask">问答中心</a></li>
<li class="active">我要提问</li>
</ol>


<div class="container">
    <ul class="list-inline">
    <li class="on"><a title="问答首页" href="#" class="btn btn-primary btn_guest">问答首页</a> </li>
    <li><a title="问答之星" href="Star.aspx" class="btn btn-primary btn_guest">问答之星</a></li>
    <li><a title="分类大全" href="Classification.aspx" class="btn btn-primary btn_guest">分类大全</a></li>
    <li><a href="List.aspx?strWhere=<%=Server.HtmlEncode(Request["strWhere"]) %>&QueType=<%=Server.HtmlEncode(Request["QueType"]) %>" class="btn btn-primary btn_guest">待完善问题</a></li>
    <!-- <li><a title="知识专题" href="Topic.aspx class="btn btn-primary btn_guest"">知识专题</a></li>-->
    </ul>
</div>


<div role="form">
<div class="form-group">
<label for="txtContent">请将您的疑问告诉我们吧：</label>
<div style="padding: 5px 0 5px 0;">
<asp:TextBox runat="server" ID="txtContent" CssClass="form-control" TextMode="MultiLine" placeholder="说点什么吧..." Rows="10"/>
</div>
</div>
<div class="form-group">
<label for="txtSupplyment">补充问题(选填)：</label>
<asp:TextBox runat="server" ID="txtSupplyment" data-type="normal" TextMode="MultiLine" Style="height: 200px;width:100%;"></asp:TextBox>
 <%=Call.GetUEditor("txtSupplyment",4)%>
</div>
<div class="form-group">
<label for="exampleInputFile">问题分类：</label>
    <div class="clearfix"></div>
<asp:DropDownList ID="ddlCate" CssClass="form-control" runat="server" Width="200px"></asp:DropDownList>
<select class="form-control text_md" id="subgrade" name="subgrade">
</select>
<div class="checkbox">
<span class="pull-">悬赏分：</span>
<asp:DropDownList ID="ddlScore" runat="server" CssClass="form-control" Width="100px">
<asp:ListItem>0</asp:ListItem>
<asp:ListItem>5</asp:ListItem>
<asp:ListItem>10</asp:ListItem>
<asp:ListItem>15</asp:ListItem>
<asp:ListItem>20</asp:ListItem>
<asp:ListItem>30</asp:ListItem>
<asp:ListItem>40</asp:ListItem>
<asp:ListItem>50</asp:ListItem>
<asp:ListItem>60</asp:ListItem>
<asp:ListItem>70</asp:ListItem>
<asp:ListItem>80</asp:ListItem>
<asp:ListItem>90</asp:ListItem>
<asp:ListItem>100</asp:ListItem>
</asp:DropDownList>
</div>
<label><asp:CheckBox ID="isNi" runat="server" /> 匿名</label>
<div id="needlog_div" runat="server" visible="false" class="alert alert-danger" role="alert">提示:当前系统不支持游客身份提问,请<a href="/User/Login.aspx?ReturnUrl=/Guest/Ask/Add.aspx">登录</a></div>
<div id="vode_div" runat="server" visible="false">
    <div class="alert alert-warning" role="alert">提示：当前系统支持游客身份提问，我们建议您以会员登陆提问获得更优体验。</div>
    <asp:TextBox ID="VCode" placeholder="验证码" MaxLength="6" runat="server" CssClass="form-control text_x" />
    <img id="VCode_img" class="codeimg" title="点击刷新验证码" />
    <input type="hidden" id="VCode_hid" name="VCode_hid" />
</div>
</div>
<div id="fix" class="form-group" runat="server" visible="false">
求助对象:<a href="/ShowList.aspx?id=<%=fixID %>" target="_blank"><%=fixName %></a>
<a href="javascript:void(0)" onclick="removeFix()"><img src="/App_Themes/Guest/images/layer_close.png" style="width:14px;height:13px;" alt=""/></a>
</div>
<div class="text-center">
<asp:Button runat="server" ID="btnSubmit" Text="提交问题" CssClass="btn btn-success" OnClick="btnSubmit_Click" OnClientClick="return CheckData();" />
</div>
</div>
</div>
<asp:HiddenField runat="server" ID="hfid" />
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<script src="/JS/ZL_ValidateCode.js"></script>
<script>
    $(function () {
        $("#top_nav_ul li[title='在线提问']").addClass("active");
        $("#VCode").ValidateCode();
        $("#ddlCate").change(function () {
            $.post('Add.aspx', { action: 'getgrade', value: $(this).val() }, function (data) {
                InitSubGrade(JSON.parse(data));
            })
        });
    });
    function removeFix() {
        if (confirm("你确定要删除求助对象？")) {
            document.getElementById("fix").style.display = "none";
        }
    }
    function CheckData() {
        var msg = $("#txtContent").val();
        if (msg.replace(" ", "") == "") { alert("内容不能为空"); return false; } 
    }
    function InitSubGrade(gradearr) {
        $("#subgrade").html('');
        for (var i = 0; i < gradearr.length; i++) {
            $("#subgrade").append('<option value="' + gradearr[i].GradeID + '">' + gradearr[i].GradeName + '</option>');
        }
    }
</script>
</asp:Content>

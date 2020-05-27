<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Site.Default" MasterPageFile="~/Manage/Site/SiteMaster2.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>智能建站</title>
    <link type="text/css" href="/Plugins/JqueryUI/Sly/css/style.css" rel="stylesheet" />
    <link type="text/css" href="/Plugins/JqueryUI/LightBox/css/lightbox.css" rel="stylesheet" />
    <script type="text/javascript" src="/JS/Common.js"></script>
    <script type="text/javascript" src="/Plugins/JqueryUI/Sly/jquery.sly.min.js"></script>
    <script type="text/javascript" src="/Plugins/JqueryUI/Sly/js/main.js"></script>
    <script type="text/javascript" src="/Plugins/JqueryUI/LightBox/jquery.lightbox.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".lightbox").lightbox({
                fitToScreen: true,
                imageClickClose: false,
                disNextBtn: true
            });
        });
        function Getpy() {
            var str=document.getElementById("TextBox3").value.trim();
            document.getElementById("domNameT").value = str;
        }
    </script>
    <style type="text/css">
        span {font-family:'Microsoft YaHei';color:grey;}
        .bigSpan {font-size:2em;font-weight:normal;margin-left:5px; color:#747484; font-family:'Microsoft YaHei';}
        .arrowL {float:left;height:195px;}
        .arrowL img {margin-top:80px; margin-right:20px;}
        .arrowR {float:right;height:195px;}
        .arrowR img {margin-top:80px; margin-left:20px;}
        .smart_f{ border:3px solid #333333; border-radius:5px; height:60px; width:590px;}
        .smart_f .smart_input{ margin-top:3px; height:48px;width:294px; border:none; padding-left:10px;font-size:16px; font-family:'Microsoft YaHei';color:#737384;}
        .smart_f .smart_span{margin-top:3px; display:block;width:177px;height:48px; background:#DADADA;border:1px solid #DADADA;border-radius:3px;color:#666666; font-size:40px;font-family:'Microsoft YaHei';line-height:46px;}
        .smart_f .smart_button{ width:95px; height:48px; margin-left:8px;margin-top:3px; font-family:'Microsoft YaHei';font-size:18px;}
        .smart_f .smart_nav{ margin-top:10px;margin-left:2%;margin-right:2%;}
        .smart_f .smart_nav li a{ color:#428bca;}
        .step{width:590px;margin:auto;margin-top:15px;}
        .thumbnail {margin-right:3px;}
    </style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="smart_nav">
    <ul class="breadcrumb">
            <li><a href="/">首页</a><span class="divider"></span></li>
            <li><a href="/Site/Default.aspx">智能建站</a><span class="divider"></span></li>
            <li>开始建站</li>
        </ul>
    </div>
    <div id="previewss">
        <div class="step">
        <span class="bigSpan" style="height:60px;line-height:60px;">1、开始智能建站</span>
        <div class="smart_f">
            <asp:TextBox runat="server" ID="domNameT" type="text" class="smart_input pull-left" onkeypress="GetEnterCode('click','checkBtn');" placeholder="输入你心仪的网址前缀,如Hello" ToolTip="输入你心仪的网址前缀,如Hello" ClientIDMode="Static"/>
            <span class="smart_span pull-left text-center"><%=GetTDomName() %></span>
            <asp:Button runat="server" UseSubmitBehavior="false" ID="checkBtn" OnClick="checkBtn_Click" class="btn btn-primary smart_button" Text="查询"/>
        </div>
            <asp:RequiredFieldValidator ID="RV1" runat="server" ControlToValidate="domNameT" ForeColor="Red" ErrorMessage="请输入子域名!!" Display="Dynamic" SetFocusOnError="true"/>
            <asp:RegularExpressionValidator ID="RV2" runat="server" ControlToValidate="domNameT" Display="Dynamic" ForeColor="Red" ErrorMessage="最少三位,最多十六位" ValidationExpression="^([a-zA-Z]([a-zA-Z0-9]){2,15}$)" />
        </div>
        <div class="clearfix"></div>
        <div style="margin-top:5em;">
            <div class="step">
                <span class="bigSpan" >2、选择你喜欢的模板</span>
            </div>
            <div id="sections" class="container">
		        <div id="horizontal">
			        <div class="slyWrap example1">
                        <a href="javascript:;" class="arrowL" id="prevPageBtn"><img src="/Plugins/JqueryUI/Sly/img/ArrowL.png"/></a>
                        <a href="javascript:;" class="arrowR" id="nextPageBtn"><img src="/Plugins/JqueryUI/Sly/img/ArrowR.png"/></a>
				        <div class="sly" data-options='{ "horizontal": 1, "itemNav": "basic", "dragContent": 1, "startAt": 3, "scrollBy": 1 }'>
                     <ul><asp:Repeater runat="server" ID="tempRepeater" ViewStateMode="Disabled">
                        <ItemTemplate>
                        <li><%#GetThumbnail(Eval("TempDirName").ToString(),Eval("Project").ToString()) %></li>
                        </ItemTemplate>
                    </asp:Repeater> </ul></div></div></div>
            </div><!--end:#horizontal-->
        </div>
        <div class="clear"></div>
        <div class="step" style="margin-top:3em;">
            <span class="bigSpan">3、管理你的数据库>></span><br />
            <span style="margin-left:80px;">高端数据自主挖掘运营</span>
        </div>
    </div>
    <div id="start" style="display:none;">
        <div style="width:590px; margin:auto; margin-top:200px;">
        <span class="bigSpan" style="font-size:58px;">1、开始智能建站</span>
        <div class="smart_f">
            <asp:TextBox runat="server" ID="TextBox3" type="text" class="smart_input pull-left" onkeypress="GetEnterCode('click','Button1');" 
                onkeyup="Getpy()" placeholder="输入你心仪的网址前缀,如Hello" ToolTip="输入你心仪的网址前缀,如Hello" ValidationGroup="va2"/>
            <span class="smart_span pull-left text-center"><%=GetTDomName() %></span>
            <asp:Button runat="server" UseSubmitBehavior="false" ID="Button1" OnClick="Button1_Click" class="btn btn-primary smart_button" Text="查询" ValidationGroup="va2"/>
        </div>
        <div style="padding-top:10px; font-size:12px;"><span >*您必须有设定一个域名才可以开启高端智能建站之旅（所有数据和内核均为独立云端部署可以自由适移设置）</span></div><div class="clearfix"></div>
        <asp:RequiredFieldValidator ID="regv1" runat="server" ControlToValidate="TextBox3" ForeColor="Red" ErrorMessage="请输入子域名!!" Display="Dynamic" SetFocusOnError="true" ValidationGroup="va2"/>
        <asp:RegularExpressionValidator ID="regv2" runat="server" ControlToValidate="TextBox3" Display="Dynamic" 
            ForeColor="Red" ErrorMessage="格式错误,最少五位,最多十六位" ValidationExpression="^([a-zA-Z]([a-zA-Z0-9]){4,15}$)" ValidationGroup="va2"/>
        </div>
        <div class="clearfix"></div>
    </div>
    <div class="modal" runat="server" visible="false" id="regDiv" style="display:block;margin:100px auto;">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close"  aria-hidden="true" data-dismiss="modal" onclick="closeDiv('<%=regDiv.ClientID %>');">×</button><!--data-dismiss="modal"-->
              <h4 class="modal-title">恭喜!<asp:Label runat="server" ID="domNameL" />可使用,免费注册帐号,三步完成建站!!!</h4>
            </div>
            <div class="modal-body" id="Div1" style="display:none;">
              <p><asp:TextBox runat="server" ID="userNameT" class="form-control" placeholder="用户名" onkeypress="return GetEnterCode('focus','userPwdT');"/></p>
              <p><asp:TextBox runat="server" ID="userPwdT" class="form-control" TextMode="Password" placeholder="密码" autocomplete="off"  onkeypress="GetEnterCode('focus','userPwdT2');"/></p>
              <p><asp:TextBox runat="server" ID="userPwdT2" class="form-control" TextMode="Password" placeholder="确认密码" autocomplete="off" onkeypress="GetEnterCode('click','userRegBtn');"/></p>
            </div>
              <div class="modal-body" id="Div2">
              <p><asp:TextBox runat="server" ID="TextBox1" class="form-control" placeholder="用户名" onkeypress="GetEnterCode('focus','TextBox2');"/></p>
              <p><asp:TextBox runat="server" ID="TextBox2" class="form-control" TextMode="Password" placeholder="密码" autocomplete="off" onkeypress="GetEnterCode('click','loginBtn');"/></p>
            </div>
            <div class="modal-footer" style="text-align:left;">
                <span id="span1" style="display:none;">
                <asp:Button runat="server" ID="userRegBtn" Text="注册" class="btn btn-primary" OnClick="userRegBtn_Click" ClientIDMode="Static" UseSubmitBehavior="false"/>
                <input type="button" value="已有帐号，点击登录" class="btn btn-primary" onclick="disLogin();"/></span>
                <span id="span2">
                <asp:Button runat="server" ID="loginBtn" Text="登录" class="btn btn-primary" OnClick="loginBtn_Click" ClientIDMode="Static" UseSubmitBehavior="false"/>
                <input id="returnBtn" type="button" value="没有帐号?点此注册" class="btn btn-primary" onclick="disReg();"/>
                </span>
                <span class="alert alert-danger" runat="server" visible="false" id="remindSpan" style="padding:9px;"></span>
          </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
      </div>
    </div>
<asp:HiddenField runat="server" ID="dataField" />
<script type="text/javascript">
    function closeDiv(id)
    {
        $("#" + id).hide();
    }
    function disLogin()
    {
        $("#Div1").hide();
        $("#span1").hide();
        $("#Div2").show();
        $("#span2").show();
        $("#<%=remindSpan.ClientID%>").text();
    }
    function disReg()
    {
        $("#Div1").show();
        $("#span1").show();
        $("#Div2").hide();
        $("#span2").hide();
        $("#<%=remindSpan.ClientID%>").text();
    }
    function preview()
    {
        $("#previewss").hide();
        $("#start").show();
    }
    $().ready(function () {
        v = $("#<%=dataField.ClientID%>").val();
        if (v)
        {
            $("#start").show();
            $("#previewss").hide();
        }
    });
</script>
</asp:Content>
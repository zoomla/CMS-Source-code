<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SohuChatManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.SohuChatManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>畅言评论管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="main">
    <div id="onlineDiv">
        <iframe src="http://changyan.sohu.com/login" style="width: 98%; height: 960px;" frameborder="0"></iframe>
    </div>
    <div id="configDiv" style="display:none;">
        <div class="panel panel-primary" style="width:50%;float:left; margin-left:20px;" id="addDiv">
            <div class="panel-heading"><h3 class="panel-title">畅言配置</h3></div>
            <div class="panel-body">
                <table class="table table-bordered table-hover">
                    <tbody>
                        <tr>
                            <td style="width: 80px; line-height:35px;" >APP_ID：</td>
                            <td><asp:TextBox runat="server" ID="chat_AppIDT" class="form-control"/></td>
                        </tr>
                        <tr>
                            <td style="width: 80px;line-height:35px;" >APP_Key：</td>
                            <td><asp:TextBox runat="server" ID="chat_AppKeyT" class="form-control" /></td>
                        </tr>
                         <tr>
                            <td style="width: 80px;line-height:35px;" >前台代码：</td>
                            <td><span style="position:relative;bottom:-8px;">
                                <input type="radio" name="codeRadio" value="1" title="可能有兼容问题,建议先测试" id="radio0" checked="checked"/><label for="label0">高速版</label> 
                                <input type="radio" name="codeRadio" value="2" title="加载速度慢于高速版" id="radion1" /><label for="radion1">兼容版</label>
                            </span></td>
                        </tr>
                        <tr><td style="line-height:35px;">操作：</td>
                            <td>
                                <asp:Button runat="server" CssClass="btn btn-primary" ID="Button1" Text="保存" OnClick="saveBtn_Click" />
                                <input type="button" class="btn btn-primary" id="Button2" value="返回" onclick="disDiv(2);"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="panel panel-primary pull-left" style="width:45%; margin-left:20px;">
            <div class="panel-heading"><h3 class="panel-title">前台输出（拷贝下方代码到模板中即可<span class="fa fa-arrow-down"></span>）：</h3></div>
            <div class="panel-body">
                <asp:TextBox runat="server" CssClass="thumbnail" ID="frontJS" TextMode="MultiLine" Width="100%" Height="300" Enabled="false"/>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    function disDiv(v) {
        switch (v) {
            case 1:
                $("#configDiv").show();
                $("#onlineDiv").hide();
                $("#regHref").show();
                $("#configHref").hide();
                break;
            case 2:
                $("#configDiv").hide();
                $("#onlineDiv").show();
                $("#regHref").hide();
                $("#configHref").show();
        }
    }
    function setRadio(v) {
        $(":radio[name='codeRadio'][value='" + v + "']").attr("checked", true);
    }
</script>
</asp:Content>
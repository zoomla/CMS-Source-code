<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.ShowContent" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<title>预览内容</title>
    <style>
    .tab-pane table tr td:first-child{ width:10%; }
    .bottom0{margin-bottom:0px;}
    #middle_td{vertical-align: middle;}
    .alert-info{margin-top:5px;}
    </style>
    <script src='/JS/Plugs/ZL_UAction.js'></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField ID="ModelID_Hid" runat="server" />
    <div>
        <ul class="nav nav-tabs">
            <li class="active"><a href="#base" data-toggle="tab">基本信息</a></li>
            <li><a href="#info" data-toggle="tab">信息属性</a></li>
            <li><a href="#process" data-toggle="tab">流程控制</a></li>
            <li><a href="#Copyright " data-toggle="tab">版权管理</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane fade in active" id="base">
                <table class="table table-bordered">
                    <tr>
                        <td class="fd_td_l">所属节点</td>
                        <td>
                            <asp:Label runat="server" ID="NodeName_L"></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td class="fd_td_l">ID</td>
						<td>
							<asp:Label ID="Gid_L" runat="server"></asp:Label>
						</td>
                    </tr>
                    <tr>
						<td class="fd_td_l"><asp:Label runat="server" ID="C_Title_L"></asp:Label></td>
						<td>
							<asp:Label ID="Title_L" runat="server"></asp:Label>
						</td>
					</tr>
                    <asp:Label ID="Base_L" runat="server"></asp:Label>
                    <tr><td class="fd_td_l">主编辑器扩展图</td><td><img runat="server" id="topimg_img" class="img_100" onerror="shownopic(this);" /></td></tr>
                </table>
            </div>
            <div class="tab-pane fade" id="info">
                <table class="table table-bordered">
                    <tr>
						<td>录入者</td>
						<td >
							<asp:Label ID="Inputer_L" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td >点击数</td>
						<td >
							<asp:Label ID="Hits_L" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td >录入时间</td>
						<td >
							<asp:Label ID="CreateTime_L" runat="server"></asp:Label>
						</td>
					</tr>
                    <tr>
						<td >更新时间</td>
						<td >
							<asp:Label ID="UpdateTime_L" runat="server"></asp:Label>
						</td>
					</tr>
                </table>
            </div>
            <div class="tab-pane fade" id="process">
                <table class="table table-bordered">
                    <tr>
						<td>审核状态<br />
						</td>
						<td class="tdrighttxt">
							<asp:Label ID="ConStatus_L" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td>推荐</td>
						<td>
							<asp:Label ID="Elite_L" runat="server"></asp:Label>
						</td>
					</tr>
                </table>
            </div>
            <div class="tab-pane fade" id="Copyright">
                <table class="table table-bordered cr_table">
                    <tr id="cr_tr">
                        <td id="middle_td" class="fd_td_l td_l">授权规则</td>
                        <td>
                            <div class="form-group text_300">
                                <label class="sr-only" for="exampleInputAmount">Amount (in dollars)</label>
                                <div class="input-group">
                                    <div class="input-group-addon">转载使用</div>
                                    <input type="text" class="form-control text-right" id="RepPri_T" disabled="disabled" placeholder="0-99元的整数">
                                    <div class="input-group-addon">.00</div>
                                </div>
                            </div>
                            <div class="form-group text_300 bottom0">
                                <label class="sr-only" for="exampleInputAmount">Amount (in dollars)</label>
                                <div class="input-group">
                                    <div class="input-group-addon">素材使用</div>
                                    <input type="text" class="form-control text-right" id="MatPri_T" disabled="disabled" placeholder="0-99元的整数">
                                    <div class="input-group-addon">.00</div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr id="worksid_tr">
                        <td class="fd_td_l">版权编号</td>
                        <td>
                            <label id="cr_worksid_l"></label>
                        </td>
                    </tr>
                    <tr id="workslink_tr">
                        <td class="fd_td_l">售卖网址</td>
                        <td id="cr_worklink"></td>
                    </tr>
                    <%--<tr>
                        <td class="fd_td_l">授权操作</td>
                        <td>
                            <asp:HiddenField runat="server" ID="worksID_F" />
                            <a href="javascript:void(0);" onclick="crhelper.add()" id="cradd_a" class="btn btn-primary disabled"><i class="fa fa-plus"></i>添加授权</a>
                            <a href="javascript:void(0);" onclick="crhelper.del()" id="crdel_a" class="btn btn-danger disabled"><i class="fa fa-trash"></i>暂停授权</a>
                            <a href="javascript:void(0);" onclick="crhelper.readd()" id="crreadd_a" class="btn btn-info disabled"><i class="fa fa-refresh"></i>重新授权</a>
                        </td>
                    </tr>--%>
                </table>
                <div id="configcr" class="alert alert-info" style="display: none;">
                    <span><strong>提示：</strong></span>未配置版权印接口，若要使用版权功能，请 <a class="btn btn-info" href="../Config/PlatInfoList.aspx">前往配置>></a>
                </div>
                <div id="nonecr" class="alert alert-info" style="display: none;">
                    <span><strong>提示：</strong></span>文章没有生成版权印，若要生成版权印，请前往修改页面进行操作!
                </div>
            </div>
        </div>
        <div class="text-center Conent_fix">
            <asp:Button ID="Button5" CssClass="btn btn-primary" runat="server" Text="修改/审核" OnClick="Button5_Click" />
				&nbsp;<asp:Button ID="Del_Btn" CssClass="btn btn-primary" runat="server" Text="删除" OnClientClick="return confirm('你确定将该数据删除到回收站吗？')"
					OnClick="delete_Click" />
				&nbsp;<asp:Button ID="Reject_Btn" CssClass="btn btn-primary" runat="server" Text="直接退稿" OnClick="Reject_Btn_Click" />
				&nbsp;<asp:Button ID="UnAudit_Btn" CssClass="btn btn-primary" runat="server" Text="取消审核" OnClick="UnAudit_Btn_Click" />
				&nbsp;<asp:Button ID="Elite_Btn" CssClass="btn btn-primary" runat="server" Text="设为推荐" OnClick="Elite_Btn_Click" />
                &nbsp;<asp:Button ID="Button6" CssClass="btn btn-primary" runat="server" Text="取消推荐" OnClick="Button6_Click" />
                <a href="/Item/<%:Request.QueryString["GID"] %>.aspx" target="_blank" class="btn btn-primary">浏览文章</a>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script>
    $(function () {
        crhelper.init();
        if (getParam("type")) {
            $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
        }
    })
    function ShowTabs(n) {
        location.href = 'ShowContent.aspx?GID=<%=Request.QueryString["GID"] %>&type=' + n + '&modeid=<%=Request.QueryString["modeid"] %>';
    }
    var diag = new ZL_Dialog();
    function opentitle(url, title) {
        diag.title = title;
        diag.url = url;
        diag.ShowModal();
    }
    function editnode(NodeID) {
        var answer = confirm("该栏目未绑定模板，是否立即绑定");
        if (answer == false) {
            return false;
        }
        else {
            open_page(NodeID, "EditNode.aspx?NodeID=");
        }
    }
    function closdlg() {
        Dialog.close();
    }
    function open_page(NodeID, strURL) {
        var diag = new Dialog();
        diag.Width = 1000;
        diag.Height = 750;
        diag.Title = "配置节点<span style='font-weight:normal'>[ESC键退出当前操作]</span>";
        diag.URL = strURL + NodeID;
        diag.show();
    }
    $().ready(function () {
        if ($("#Egv tr").length > 1) { $("#commentDiv").show(); }
    });

    var crhelper = { api: "/Manage/Copyright/cr.ashx", gid:"<%:Gid%>" };
    crhelper.init=function(){
        $.post(this.api+"?action=get",{ gid: crhelper.gid },function(data){
            var model = APIResult.getModel(data);
            if (APIResult.isok(model)) {
                $("#RepPri_T").val(model.result.RepPrice);
                $("#MatPri_T").val(model.result.MatPrice);
                $("#cr_worksid_l").text(model.result.WorksID);
                var link = "http://www.banquanyin.com/" + model.result.WorksID;
                $("#cr_worklink").text("").append($("<a href='" + link + "' target='_blank' >" + link + "</a>"));
            }
            else {
                $(".cr_table").hide();
                if(model.retmsg=="未配置版权印接口"){
                    $("#configcr").css("display","block");
                } else {
                    $("#nonecr").css("display", "block");
                }
            }
        });
    }
    </script>
</asp:Content>
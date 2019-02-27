<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPaperCenter.aspx.cs" Inherits="Manage_Exam_ViewPaperCenter" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped table-hover">
	<tr><td class="td_m">标题:</td><td><asp:Label runat="server" ID="Title_T"></asp:Label></td></tr>
	<tr><td>描述:</td><td><asp:Label runat="server" ID="Desc_T"></asp:Label></td></tr>
    <tr><td>操作:</td><td>
        <a href="Add_Papers_System.aspx?id=<%:PaperID %>" class="btn btn-info" target="_blank">修改试卷</a>
        <a href="Paper_QuestionManage.aspx?pid=<%:PaperID %>" class="btn btn-info" target="_blank">题目管理</a></td>
    </tr>
</table>
<div class="panel panel-default">
	<div class="panel-body">
		<asp:Repeater runat="server" ID="MainRPT" EnableViewState="false" OnItemDataBound="MainRPT_ItemDataBound">
			<ItemTemplate>
					<div style="margin-top: 5px;">
						<%#ZoomLa.BLL.Helper.StrHelper.ConvertIntegral(Container.ItemIndex+1) +"．"+Eval("QName")+"（有"+Eval("QNum")+"小题,共"+Eval("TotalScore")+"分）" %>
						<asp:Repeater runat="server" ID="RPT" EnableViewState="false">
							<ItemTemplate>
								<div class="item" data-id="<%#Eval("p_id") %>" id="item_<%#Eval("p_id") %>">
									<div class="content">
										<div class="opdiv alert-info">
											<input type="text" id="score_<%#Eval("p_id") %>" style="width:40px;" value="<%#Eval("p_defaultScores") %>" /><span>分</span>
											<a href="AddEngLishQuestion.aspx?id=<%#Eval("p_id") %>" target="_blank">修改试题</a>
											<a href="javascript:UPFunc(<%#Eval("p_id") %>);">上移</a>
											<a href="javascript:DownFunc(<%#Eval("p_id") %>);">下移</a>
											<a href="javascript:DelThis(<%#Eval("p_id") %>);">删除</a>
										</div>
										<span><%#Container.ItemIndex+1+"．"+Eval("P_Title") %></span><%#Eval("p_content") %>
									</div>
									<div class="submit">
										<ul class="submitul"><%#GetSubmit() %></ul>
										<div class="clearfix"></div>
									</div>
								</div>
							</ItemTemplate>
						</asp:Repeater>
					</div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
</div>
<div class="Conent_fix">
    <asp:Button runat="server" CssClass="btn btn-primary" ID="Sure_Btn" Text="保存修改" OnClick="Sure_Btn_Click" OnClientClick="return PreSave();" />
<%--    <input type="button" class="btn btn-default" value="清空试题" onclick="if (confirm('确定要清空吗?')) { AddToCart('cart_clear'); }" />--%>
</div>
<asp:HiddenField runat="server" ID="Qids_Hid" />
<asp:HiddenField runat="server" ID="QInfo_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
.opitem p {display:inline-block;}
label {font-weight:normal;}
.submitul li {float:left;margin-left:20px;}    
.item {border:1px solid #ddd; padding-left:20px;padding-top:5px;text-align:left;margin-bottom:5px;}
.item:hover {border:1px solid #169bef;}
.opdiv {float:right;font-size:12px;padding:5px;padding-left:15px;padding-right:15px;display:none;}
.opdiv > a {margin-left:8px;}
</style>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script>
    $(function () {
        $(".item").hover(function () { $(this).find(".opdiv").show(); }, function () { $(".opdiv").hide(); });
        ZL_Regex.B_Num('.int');
        ////下载
        //$("#paperSize_dp,#orient_dp").change(function () {
        //    var query = "ID=" + $("#NewID_Hid").val() + "&paperSize=" + $("#paperSize_dp").val() + "&orient=" + $("#orient_dp").val();
        //    $("#paper_down").attr("href", "/User/Exam/DownPaper.aspx?" + query);
        //});
    })
    function DelThis(qid) {
        $("#item_" + qid).fadeOut(500);
        $("#item_" + qid).remove();
        //AddToCart("cart_remove", qid);
    }
    //上移与下移
    function UPFunc(p_id) {
        var item = $("#item_" + p_id);
        var pre = item.prev(".item");
        if (pre.length < 1) { return; }
        else
        {
            pre.before(item); //item.remove();
        }
    }
    function DownFunc(p_id) {
        var item = $("#item_" + p_id);
        var next = item.next(".item");
        if (next.length < 1) { return; }
        else
        {
            next.after(item); //item.remove();
        }
    }
    //预保存
    function PreSave() {
        var items = $(".item"); var list = [];
        for (var i = 0; i < items.length; i++) {
            var item = $(items[i]);
            var model = { id: item.data("id"), score: "", order: "" };
            model.score = ConverToInt(item.find("#score_" + model.id).val(), 0);
            model.order = i;
            list.push(model);
        }
        $("#Qids_Hid").val(list.GetIDS());
        $("#QInfo_Hid").val(JSON.stringify(list));
    }
</script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaperCenter.aspx.cs" Inherits="User_Exam_PaperCenter" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>组卷中心</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div runat="server" id="add_div">
<table class="table table-bordered table-striped table-hover">
	<tr><td>标题:</td><td>
		<asp:TextBox runat="server" ID="Title_T" CssClass="form-control m715-50"></asp:TextBox>
		<asp:RequiredFieldValidator runat="server" ID="R1" ErrorMessage="标题不能为空" ControlToValidate="Title_T" ForeColor="Red" /></td></tr>
	<tr><td>描述:</td><td><asp:TextBox runat="server" ID="Desc_T" TextMode="MultiLine" CssClass="form-control m715-50" placeholder="描述主要用来方便查看您的试卷,描述内容可以是试卷题型和考点组成、难度、使用年级及时完成时间等"></asp:TextBox></td></tr>
	<tr><td>价格:</td><td><asp:TextBox runat="server" ID="Price_T" Text="0" CssClass="form-control text_x int"></asp:TextBox></td></tr>
   <%-- <tr><td>考试时间:</td><td><asp:TextBox runat="server" ID="Time_T" Text="120" CssClass="form-control text_300"></asp:TextBox><span class="r_green">0表示不限时长</span></td></tr>--%>
</table>
<div class="panel panel-default">
	<div class="panel-body" style="height:500px;overflow-y:auto;">
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
											<a href="QuestView.aspx?ID=<%#Eval("p_id") %>" target="_blank">查看解析</a>
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
	<div class="panel-footer text-center"" >
		 <asp:Button runat="server" CssClass="btn btn-primary" ID="Sure_Btn" Text="完成组卷" OnClick="Sure_Btn_Click" OnClientClick="return PreSave();" />
		 <input type="button" class="btn btn-default" value="取消组卷" onclick="parent.CloseComDiag();"  />
		 <input type="button" class="btn btn-default" value="清空试题篮" onclick="if (confirm('确定要清空吗?')) { AddToCart('cart_clear');}"/>
	</div>
</div>
</div>
<div runat="server" id="addsucc_div" class="panel panel-primary" visible="false">
<div class="panel-heading" style="font-size:18px;">【恭喜您组卷成功】</div>
<div class="panel-body">
	<table class="table table-bordered table-striped">
	<tr><td class="td_m">标题:</td><td><asp:Label runat="server" ID="Title_L"></asp:Label></td></tr>
	<tr><td>试卷类型:</td><td>练习</td></tr>
	<tr><td>考试时长:</td><td>不限时</td></tr>
</table>
</div>
<div class="panel-footer" style="padding-left: 120px;">
	<div class="input-group" style="width: 264px;float:left;">
	<asp:HiddenField runat="server" ID="NewID_Hid" />
	<select id="paperSize_dp" class="form-control" style="width: 90px; border-right: none;">
		<option value="16k">16K</option>
		<option value="A4" selected="selected">A4</option>
		<option value="A3">A3</option>
		<option value="B4">B4</option>
	</select>
	<select id="orient_dp" class="form-control" style="width: 90px; border-right: none;">
		<option value="false">纵向</option>
		<option value="true">横向</option>
	</select>
	<span class="input-group-btn">
		<a href="" runat="server" target="_blank" id="paper_down" class="btn btn-primary">下载试卷</a>
	</span>
</div>
	<a href="" runat="server" id="paper_view" class="btn btn-primary margin_l5">查看试卷</a>
	<a href="" runat="server" id="paper_edit" class="btn btn-primary" target="_blank">修改试卷</a>
	<input type="button" class="btn btn-primary" value="关闭" onclick="parent.CloseComDiag();" />
</div>
</div>
<asp:HiddenField runat="server" ID="QInfo_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
label {font-weight:normal;}
.submitul li {float:left;margin-left:20px;}    
.item {border:1px solid #ddd; padding-left:20px;padding-top:5px;text-align:left;margin-bottom:5px;}
.item:hover {border:1px solid #169bef;}
.opdiv {float:right;font-size:12px;padding:5px;padding-left:15px;padding-right:15px;display:none;}
.opdiv > a {margin-left:8px;}
.opitem p {display:inline-block;}
</style>
<script src="/JS/ZL_Regex.js"></script>
<script>
$(function () {
	$(".item").hover(function () { $(this).find(".opdiv").show(); }, function () { $(".opdiv").hide(); });
	ZL_Regex.B_Num('.int');
	//下载
	$("#paperSize_dp,#orient_dp").change(function () {
		var query = "ID=" + $("#NewID_Hid").val() + "&paperSize=" + $("#paperSize_dp").val() + "&orient=" + $("#orient_dp").val();
		$("#paper_down").attr("href", "/User/Exam/DownPaper.aspx?" + query);
	});
})
function DelThis(qid) {
    $("#item_" + qid).fadeOut(500);
    $("#item_" + qid).remove();
	AddToCart("cart_remove", qid);
}
function AddToCart(action, qid) {
    $.post("QuestAPI.aspx?action=" + action, { "qid": qid }, function (data) {
        if (action == "cart_clear") {
            location = location;
        }
    });
}
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
function PreSave() {
	if (!confirm('确定保存组卷吗?')) { return false; }
	var items = $(".item"); var list = [];
	for (var i = 0; i < items.length; i++) {
		var item = $(items[i]);
		var model = { id: item.data("id"), score: "", order: "" };
		model.score = ConverToInt(item.find("#score_" + model.id).val(), 0);
		model.order = i;
		list.push(model);
	}
	$("#QInfo_Hid").val(JSON.stringify(list));
}
</script>
</asp:Content>
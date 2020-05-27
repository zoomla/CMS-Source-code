<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paper.aspx.cs" Inherits="test_paper" EnableViewState="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>生成试卷</title>
<style type="text/css">ul {padding-left:0px;}</style>
</head>
<body>
<form id="form1" runat="server">
<div id="paper" style="font-size:12px;font-family:'Microsoft YaHei';">
<div style="width: 500px; margin: auto; text-align: center;">
	<h2 style="text-align: center;color:black;"><asp:Label runat="server" ID="Title_L"></asp:Label></h2>
	<h2 style="text-align: center;color:black;">试卷副标题</h2>
	<h3 style="text-align: center;color:black;">考试范围：XXX 考试时间：120分钟 命题人：XXX</h3>
</div>
<div>注意事项：</div>
<div>1,答题前填写好自己的姓名,班级,考号等信息,请将答案正确填写在答题卡上</div>
<asp:Repeater runat="server" ID="MainRPT" EnableViewState="false" OnItemDataBound="MainRPT_ItemDataBound">
	<ItemTemplate>
		<div style="margin-top: 5px;">
			<h3><%#ZoomLa.BLL.Helper.StrHelper.ConvertIntegral(Container.ItemIndex+1) +"．"+Eval("QName")+"（有"+Eval("QNum")+"小题,共"+Eval("TotalScore")+"分）" %></h3>
			<div><%#Eval("LargeContent") %></div>
            <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
				<ItemTemplate>
					<div class="item">
						<div class="content">
							<span><%#Container.ItemIndex+1+"．"+Eval("P_Title") %></span><%#GetContent() %>
						</div> 
						<div class="submit"><%#GetSubmit() %></div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
	</ItemTemplate>
</asp:Repeater>
</div>
</form>
</body>
</html>
<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="AddEditModel.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.AddEditModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<title>编辑模型</title>
<style>
    .btn-group{display:inline-table;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover">
	<tr>
		<td class="spacingtitle" colspan="2" align="center">
			<asp:Literal ID="LTitle" runat="server" ></asp:Literal>
		</td>
	</tr>
	<tr class="tdbg">
		<td class="tdbgleft" style="width: 300px" >
			<strong><%=bll.GetModelType(Convert.ToInt32(Request["ModelType"])) %>名称：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtModelName" class="form-control pull-left" onchange="checkTbName()" runat="server" Width="156" MaxLength="200" onkeyup="Getpy('TxtModelName','TxtTableName')" autofocus="true" />
            <span class="spanl_30">
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtModelName">内容模型名称不能为空</asp:RequiredFieldValidator></span>
        </td>
	</tr>
	<tr class="tdbg">
		<td class="tdbgleft">
			<strong>创建的数据表名：</strong>
		</td>
		<td>
            <div class="input-group pull-left">
            <asp:Label CssClass="input-group-addon" ID="LblTablePrefix" runat="server"  />
            <asp:TextBox ID="TxtTableName" CssClass="form-control" onchange="checkTbName()" runat="server" style="width:150px;" />
            </div>
            <span id="checkname_span" style="color:red;display:none;">表名重复!</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtTableName">数据表名不能为空</asp:RequiredFieldValidator>
		</td>
	</tr>
    <tr class="tdbg" id="PubType1" visible="false" runat="server">
            <td class="tdbgleft">
                <strong>互动类型：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="PubType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">评论</asp:ListItem>
                    <asp:ListItem Value="1">投票</asp:ListItem>
                    <asp:ListItem Value="2">活动</asp:ListItem>
                    <asp:ListItem Value="3">留言</asp:ListItem>
                    <asp:ListItem Value="4">问券调查</asp:ListItem>
                    <asp:ListItem Value="5">统计</asp:ListItem>
                    <asp:ListItem Value="6">竞标</asp:ListItem>
                </asp:RadioButtonList>
             </td>
        </tr>
	<tr class="tdbg">
		<td class="tdbgleft">
			<strong>项目名称：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtItemName" runat="server" CssClass="form-control pull-left" Width="156" MaxLength="20" />
            <div id="ItemTd" class="btn btn-group">
            </div>
            <span class="spanl_30"><font color="red">*</font>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtItemName" ErrorMessage="RequiredFieldValidator">项目名称不能为空</asp:RequiredFieldValidator></span>
		</td>
	</tr>
	<tr class="tdbg">
		<td class="tdbgleft">
			<strong>项目单位：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtItemUnit" runat="server" Text="篇" CssClass="form-control pull-left" Width="156" MaxLength="20" />
            <div id="Unitd" class="btn btn-group">
            </div>
            <span class="spanl_30"><font color="red">*</font>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtItemUnit" ErrorMessage="RequiredFieldValidator">项目单位不能为空</asp:RequiredFieldValidator></span>
		</td>
	</tr>
   <tr class="tdbg">
		<td class="tdbgleft">
			<strong>是否复制：</strong>
			  <br />
			  确定模型是否可以复制
		</td>
		<td>
           <input type="checkbox" runat="server" id="rblCopy" class="switchChk"  checked="checked" />
		</td>
	</tr>
    <tr class="tdbg">
		<td class="tdbgleft">
			<strong>是否批量添加：</strong>
			  <br />
			  确定用户中心是否可以批量添加
		</td>
		<td>
            <input type="checkbox" runat="server" id="rblIslotsize" class="switchChk"/>
		</td>
	</tr>
    <tr class="tdbg">
		<td class="tdbgleft">
			<strong>是否文件工厂:</strong>
			  <br />
			  文件工厂用于批量生成Word文档
		</td>
		<td>
            <input type="checkbox" runat="server" id="FileFactory" class="switchChk"/>
		</td>
	</tr>
	<tr class="tdbg">
		<td class="tdbgleft">
			<strong>项目图标：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtItemIcon" CssClass="form-control pull-left" runat="server" Width="156" />
		</td>
	</tr>
	<tr class="tdbg">
		<td class="tdbgleft">
			<strong>模型描述：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Width="365px" Height="77px" Rows="6" />
		</td>
	</tr>
    <tr class="tdbg" ID="RBLMultiFlag1" runat="server" visible="false">
      <td class="tdbgleft"><strong>是否多条记录：</strong></td>
      <td>
          <asp:RadioButtonList ID="RBLMultiFlag" runat="server" RepeatDirection="Horizontal" >
              <asp:ListItem Selected="True" Value="0">一个用户只能输入一条此模型信息</asp:ListItem>
              <asp:ListItem Value="1">一个用户可以输入多条此模型信息</asp:ListItem>
          </asp:RadioButtonList>
      </td>
    </tr>
	<tr class="tdbgbottom">
		<td colspan="2" class="text-center">
			<asp:Button ID="EBtnSubmit"  Text="保 存" OnClientClick="return CheckData();" CssClass="btn btn-primary"  OnClick="EBtnSubmit_Click" runat="server" />
			&nbsp;&nbsp;
			<input name="Cancel" type="button"  id="Cancel" class="btn btn-primary" value="取消" onclick="window.location.href='ModelManage.aspx?ModelType=<%=Request["ModelType"] %>';" />
		</td>
	</tr>
</table>
    <div id="icons" style="display:none"></div>
<style>.spanl_30{ line-height:36px; margin-left:5px;}</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
<script type="text/javascript" src="/JS/chinese.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/Plugs/IconSelector.js"></script>
<script>
    $("body").ready(function(){
        SelUnit();
        SelItem();
        var iconsel=new iconSelctor("TxtItemIcon");
    });
    function CheckData(){
        var patt = /^[a-zA-Z0-9_\-]+$/
        if (!patt.test($("#TxtTableName").val())) {
            alert('数据表名只能由字母、下划线或数字组成！');
            $("#TxtTableName").focus();
            return false;
        }
        return true;
    }
    

function Getpy(ontxt, id) {
    if ('<%=Request["ModelID"] %>' == "") {
        var str = document.getElementById(ontxt).value.trim();
        if (str == "") {
            document.getElementById(id).value = "";
            return;
        }
        var arrRslt = makePy(str);
        if (arrRslt.length > 0) {
            document.getElementById(id).value = arrRslt.toString().toLowerCase();
            if (document.getElementById(id).value.indexOf(',') > -1) {//判断栏目名称有多音字后栏目标识名“，”并去掉逗号后面的数据
                var str = document.getElementById(id).value;
                document.getElementById(id).value = str.split(',', 1);
            }
        }
    }
}
    function SelUnit(){
        var units= "篇,个,张,件";
        var array=units.split(",");
        for (var i = 0; i < array.length; i++) {
            var str="<button type='button' class='btn btn-default'>"+array[i]+"</button>"
            $("#Unitd").append(str);
        }
        $("#Unitd").find("button").click(function(){
            $("#TxtItemUnit").val($(this).text());
        });
            
    }

    function SelItem(){
        var units= "文章,软件,图片,商品";
        var array=units.split(",");
        for (var i = 0; i < array.length; i++) {
            var str="<button type='button' class='btn btn-default'>"+array[i]+"</button>"
            $("#ItemTd").append(str);
        }
        $("#ItemTd").find("button").click(function(){
            $("#TxtItemName").val($(this).text());
        });
            
    }
    //检查表名是否重复
    function checkTbName(){
        var tbname=$("#TxtTableName").val();
        //checkname_span
        $.ajax({
            type:'POST',
            data:{action:'checkname',tbname:tbname},
            success:function(data){
                if (data==-1) {
                    $("#checkname_span").show();
                    $("#EBtnSubmit").attr("disabled","disabled");
                }else{
                    $("#checkname_span").hide();
                    $("#EBtnSubmit").removeAttr("disabled");
                }
            }
        });
    }
</script>
</asp:Content>
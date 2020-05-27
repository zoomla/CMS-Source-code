<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDetail.aspx.cs" Inherits="ZoomLaCMS.Plat.Task.TaskDetail"  MasterPageFile="~/Plat/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<style>
.msg_content_attach { display: inline-block; cursor: pointer; margin-top: 2px; margin-bottom: 2px; text-align: center; }
.msg_content_attach div { margin-left: 15px; }
.task_top_div { height: 50px; line-height: 50px; background-color: #fafafa; padding-left: 10px; border-bottom: 1px solid #f2f2f2; }
.task_info_div { padding: 10px; border-bottom: 1px solid #fafafa; }
.task_info_detail_div { padding: 10px; }
.fa { cursor: pointer; color: black; }
.fa:hover { color: #0066cc; }
.imgface { border-radius: 50%; width: 30px; height: 30px; }
.colorSpan_F { padding: 2px; display: inline-block; border: 2px solid white; border-radius: 50%; margin-right: 5px; top: 5px; cursor: pointer; position: relative; }
.colorSpan { width: 30px; height: 30px; border-radius: 50%; float: left; }
.colorSpan_F:hover { border: 2px solid #0066cc; }
#TaskDetail_Div { width: 592px; }
#TaskMsg_ul { padding-top: 10px; }
#TaskMsg_ul li { padding: 5px; border-bottom: 1px dotted #ddd; }
#idsTable { margin-top: 5px; margin-bottom: 5px; width: 100%; }
#idsTable tr td { padding: 8px; border-bottom: 1px solid #ddd; }
</style>
<title>任务详情</title>
<link type="text/css" rel="stylesheet" href="/Plugins/Uploadify/style.css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div id="TaskDetail_Div" style="font-family:'Microsoft YaHei';">
    <div class="task_top_div">
      <div class="col-xs-3"> <img runat="server" id="userimg_img" src="/Images/userface/noface.png" class="imgface" />
        <asp:Label runat="server" ID="CreateUName_L"></asp:Label>
      </div>
      <div class="col-xs-3"><span class="fa fa-road"></span><span style="margin-left: 5px;">进行中</span></div>
      <div class="col-xs-3"> <span class="colorSpan_F"><span class="colorSpan" style="background-color: #3bb3ff; border: 1px solid #0084ff;"></span></span> </div>
      <div class="col-xs-3" style="text-align: right;"> <span class='fa fa-remove' title='关闭' onclick="parent.HideTaskDiv();"></span>
        <%-- <asp:LinkButton runat="server" ID="Del_Link" ToolTip="删除" OnClientClick="reutrn confirm('确定要删除吗?');" OnClick="Del_Link_Click">
				<span class='fa fa-trash-o'></span></asp:LinkButton>--%>
      </div>
    </div>
    <div class="task_info_div">
      <div style="padding-top: 5px; padding-bottom: 5px;">
        <input type="checkbox" id="completeTask" title="标识任务已完成" />
        <asp:Label runat="server" ID="TName_L"></asp:Label>
        <span class="fa fa-lock" title="公开该任务" style="float: right;"></span> </div>
      <div>
        <asp:Label runat="server" ID="BTime_L"></asp:Label>
        --
        <asp:Label runat="server" ID="ETime_L"></asp:Label>
      </div>
    </div>
    <div class="task_info_detail_div">
      <div>
        <asp:TextBox runat="server" ID="TaskContent_T" TextMode="MultiLine" CssClass="form-control" Style="max-width:100%;width:100%;  height: 50px;" placeholder="任务描述"></asp:TextBox>
      </div>
      <div>
        <table id="idsTable">
          <tr>
            <td style="width:120px;">负责人：</td>
            <td><asp:Label runat="server" ID="LeaderIDS"></asp:Label></td>
          </tr>
          <tr>
            <td>任务成员：</td>
            <td><asp:Label runat="server" ID="PartTakeIDS"></asp:Label></td>
          </tr>
        </table>
      </div>
      <asp:LinkButton runat="server" ID="upfilebt" OnClientClick="return openUpfile(false)"><span class="fa fa-paperclip" style="font-size:1em;">附件</span></asp:LinkButton>
      <div id="showfilelist">
        <asp:Repeater ID="RShowFilelist" OnItemCommand="RShowFilelist_ItemCommand" runat="server">
          <ItemTemplate>
            <div class="msg_content_attach"> <%#Eval("ExtName") %>
              <asp:LinkButton runat="server" CommandName="Down" CommandArgument='<%#Eval("Path") %>'><%#Eval("FileName") %></asp:LinkButton>
            </div>
          </ItemTemplate>
        </asp:Repeater>
      </div>
      <div style="margin-bottom: 5px; padding-bottom: 5px;">
        <div style="margin-bottom: 5px;">
          <asp:TextBox runat="server" ID="MsgContent_T" TextMode="MultiLine" CssClass="form-control" Style="max-width:100%;width:100%; height: 80px; margin-top: 10px; padding-left: 5px;" placeholder="我也说一句"></asp:TextBox>
        </div>
        <div>
          <asp:Button runat="server" Text="评论" CssClass="btn btn-info btn-sm" ID="AddMsg_Btn" OnClick="AddMsg_Btn_Click" Style=" margin-left: 10px;" />
        </div>
        <div style="clear: both;"></div>
      </div>
      <!--回复-->
      <div>
        <ul id="TaskMsg_ul">
          <asp:Repeater runat="server" ID="TaskMsg_Rep">
            <ItemTemplate>
              <li>
                <div class="col-sm-2 col-xs-2"> <img src="<%#Eval("UserFace") %>" class="imgface" /><br />
                  <strong><%#Eval("UserName") %></strong> </div>
                <div class="col-sm-10 col-xs-10">
                  <div style="padding-top:5px;"><%#Eval("MsgContent") %></div>
                  <div style="text-align: right; font-size: 0.8em; color: #999;"> <span><%#Convert.ToDateTime(Eval("CreateTime")).ToString("yyyy年MM月dd日 HH:mm") %></span> </div>
                </div>
                <div style="clear: both;"></div>
              </li>
            </ItemTemplate>
          </asp:Repeater>
        </ul>
      </div>
    </div>
  </div>
  <div style="display: none;" class="hidden_div"> <a href="javascript:;" data-toggle="modal" data-target="#myModal" id="Model_Btn"></a> <a href="javascript:;" data-toggle="modal" data-target="#fileup_div" id="FileUP_Btn"></a> <a href="javascript:;" data-toggle="modal" data-target="#forward_div" id="Forward_Btn"></a>
    <asp:HiddenField runat="server" ID="UserInfo_Hid" />
  </div>
  <div class="modal" id="fileup_div" style="position: fixed; top: 35%;">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
          <span class="modal-title" id="FileUP_Title"></span> </div>
        <div class="modal-body" style="height: 150px;">
            <input type="file" id="file_upload_1" />
          <input type="hidden" runat="server" id="Attach_Hid" name="Attach_Hid" />
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-info" data-dismiss="modal">关闭</button>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
  <script type="text/javascript" src="/Plugins/Uploadify/jquery.uploadify.js"></script>
  <script type="text/javascript">
      $(function () {
        $("#top_nav_ul li[title='主页']").addClass("active");
		var img = "<img src='{0}' class='purview_img'/>";
		var fileurl = "/Plugins/Uploadify/UploadFileHandler.ashx?value=" +<%=TaskID %>
		$("#file_upload_1").uploadify({
			//按钮宽高
			width: 120,
			height: 35,
			auto: true,
			swf: '/Plugins/Uploadify/uploadify.swf',
			uploader: fileurl,
			buttonText: "上传附件",
			buttonCursor: 'hand',
			fileTypeExts: "*.*",
			fileTypeDesc: "请选择文件",
			fileSizeLimit: "50000KB",
			formData: { "action": "Plat_Task" },
			queueSizeLimit: 1,
			removeTimeout: 2,
			multi: false,
			onUploadStart: function (file) { },
			onUploadSuccess: function (file, data, response) {//暂只允许上传一个文件,后期根据业务需要看是否更改, //json,result,tru||false
				$("#Attach_Hid").val(data);
				$("#edit").click();
			},
			onQueueComplete: function (queueData) { },
			onUploadError: function (file) { alert(file.name + "上传失败"); }
		});
	});
	function openUpfile(data) {
		var d = $("#FileUP_Btn").click();
		return data;
	}
</script>
</asp:Content>

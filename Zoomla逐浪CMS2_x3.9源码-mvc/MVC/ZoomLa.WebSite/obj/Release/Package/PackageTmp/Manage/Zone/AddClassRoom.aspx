<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClassRoom.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.AddClassRoom"  MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
 <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
<title>添加班级</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdleft"><strong>班级名称:</strong></td>
            <td>
                <asp:TextBox ID="txtRoomName" class="form-control text_300" runat="server"></asp:TextBox>
                <span style="color: Red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoomName" ErrorMessage="班级名称不能为空!"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <td class="td_l tdleft"><strong>所属学校:</strong></td>
            <td>
                <asp:TextBox ID="SchoolName_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                <button type="button" onclick="ShowSchool()" class="btn btn-primary">填写或选择学校</button>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>班级年级:</strong></td>
            <td>
                <asp:DropDownList ID="GradeList_Drop" CssClass="form-control text_300" runat="server" DataTextField="GradeName" DataValueField="GradeID"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>班主任:</strong></td>
            <td>
                <asp:TextBox ID="Manager_T" runat="server" CssClass="form-control text_300" disabled="disabled"></asp:TextBox>
                <button type="button" onclick="ShowSelTearch()" class="btn btn-primary">选择教师</button>
                <asp:HiddenField ID="Manager_Hid" runat="server" />
            </td>
        </tr>
        <%--<tr>
            <td class="tdleft"><strong>担任教师:</strong></td>
            <td>
                <asp:TextBox ID="txtTeacher" class="form-control text_s" runat="server"></asp:TextBox>
                <span style="color: Red">*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTeacher" ErrorMessage="请输入班主任名字"></asp:RequiredFieldValidator></span>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="tdleft"><strong>副管理员:</strong></td>
            <td>
                <asp:TextBox ID="txtAdviser" class="form-control" runat="server" Width="304px"></asp:TextBox>多个请用逗号 , 隔开</td>
        </tr>--%>
        <tr>
            <td class="tdleft"><strong>最大人数:</strong></td>
            <td>
                <asp:TextBox ID="ClassNum_T" runat="server" Text="100" CssClass="form-control text_s int"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否毕业:</strong></td>
            <td>
                <input type="checkbox" class="switchChk" runat="server" id="IsDone_Check" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>班级星级:</strong></td>
            <td>
                <div class="col-md-9" id="star_div">
                <i class="staricon fa fa-star-o" data-val="1"></i>
                <i class="staricon fa fa-star-o" data-val="2"></i>
                <i class="staricon fa fa-star-o" data-val="3"></i>
                <i class="staricon fa fa-star-o" data-val="4"></i>
                <i class="staricon fa fa-star-o" data-val="5"></i>
                <asp:HiddenField runat="server" id="star_hid" value="0"></asp:HiddenField>
            </div>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>班级徽标:</strong></td>
            <td>
                <asp:TextBox ID="ClassIcon_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否审核:</strong></td>
            <td>
                <input type="checkbox" runat="server" id="txtIsTrue" class="switchChk" checked="checked"/>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>班级介绍:</strong></td>
            <td>
                <asp:TextBox ID="txtClassinfo" TextMode="MultiLine" class="form-control" runat="server" Height="168px" Width="443px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:HiddenField ID="txtSchoolID" runat="server" />
                <asp:HiddenField ID="txtRoomID" runat="server" />
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="确认添加" OnClientClick="return CheckData();" OnClick="Button1_Click" />
                <a href="SnsClassRoom.aspx" class="btn btn-primary">返回列表</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
<script type="text/javascript" src="/JS/ZL_Regex.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/Plugs/IconSelector.js"></script>
<script>
    $(function () {
        ZL_Regex.B_Num('.int');
        StarInit();
        var iconsel = new iconSelctor("ClassIcon_T");
    })
	//function SelectRule() {
	//	window.open('SchoolList.aspx', '', 'width=600,height=450,resizable=0,scrollbars=yes');
	//}
	function StarInit() {
	    $(".staricon").hover(function () {
	        //fa-star-o空心,
	        $(this).removeClass("fa-star-o").addClass("fa-star");
	        $(this).prevAll(".staricon").removeClass("fa-star-o").addClass("fa-star");
	    }, function () {
	        StarByVal($("#star_hid").val());
	    }).click(function () {
	        $("#star_hid").val($(this).data("val"));
	        StarByVal($(this).data("val"));
	    });
	    //移出div块,除非已click,否则清除值
	    $("#star_div").mouseleave(function () {
	        var val = $("#star_hid").val();
	        StarByVal(val);
	    });
	    //根据val点亮或熄灭评星
	    function StarByVal(val) {
	        if (val == "" || val == 0 || val == "0") { $(".staricon").removeClass("fa-star").addClass("fa-star-o"); }
	        else
	        {
	            var ref = $(".staricon[data-val=" + val + "]"); ref.removeClass("fa-star-o").addClass("fa-star");
	            ref.prevAll().removeClass("fa-star-o").addClass("fa-star");
	            ref.nextAll().removeClass("fa-star").addClass("fa-star-o");
	        }
	    }
	}
    //提交检测数据
	function CheckData() {
	    if ($("#txtRoomName").val().trim() == "") { alert("班级名称不能为空!"); $("#txtRoomName").focus(); return false; }
	    return true;
	}

	function GetTearcher(id, username) {
	    $("#Manager_T").val(username);
	    $("#Manager_Hid").val(id);
	    teachDiag.CloseModal();
	}
	function CloseDiag() {
	    teachDiag.CloseModal();
	}
	var teachDiag = new ZL_Dialog();
	function ShowSelTearch() {
	    teachDiag.width = "none";
	    teachDiag.maxbtn = false;
	    teachDiag.reload = true;
	    teachDiag.title = "选择教师";
	    teachDiag.url="/User/Exam/SelTearcher.aspx";
	    teachDiag.ShowModal();
	}
    //选择图标
	function ShowIcon() {
	    comdiag.ajaxurl = "/Common/icon.html";
	    comdiag.title = "选择奥森图标";
	    comdiag.ShowModal();
	}
	function LoadIcon() {
	    $("[name=glyphicon]").click(function () {
	        $("#ClassIcon_T").val($(this).prev().prev().attr("class"));
	        $("#imgIcon").attr("class", $(this).prev().prev().attr("class"));
	        CloseComDiag();
	    });
	}

	//选择学校
	function ShowSchool() {
	    teachDiag.url = "/User/Exam/SelSchool.aspx";
	    teachDiag.title = "选择学校";
	    teachDiag.reload = true;
	    teachDiag.maxbtn = false;
	    teachDiag.width = "width1100";
	    teachDiag.ShowModal();
	}
	function CloseDiag() {
	    teachDiag.CloseModal();
	}
	function GetSchoolName(name) {
	    $("#SchoolName_T").val(name);
	    teachDiag.CloseModal();
	}
</script>
</asp:Content>

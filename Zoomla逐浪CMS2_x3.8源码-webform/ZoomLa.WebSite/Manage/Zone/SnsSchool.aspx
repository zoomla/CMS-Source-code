<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SnsSchool.aspx.cs" Inherits="manage_Zone_SnsSchool" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>学校信息配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href="<%=customPath2 %>Main.aspx">工作台</a></li><li><a href="../Exam/Papers_System_Manage.aspx">教育模块</a></li><li class="active">学校管理<a href="AddSchool.aspx">[添加学校]</a></li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" class="padding5">
        <div class="input-group pull-left" style="width:630px">
            <select id="tbProvince" class="form-control schooldp" style="width:120px;"></select>
            <select id="tbCity" class="form-control schooldp" style="width:120px;"></select>
            <select id="tbCounty" class="form-control schooldp" style="width:150px;"></select>
            <asp:TextBox ID="Search_T" CssClass="form-control text_md" runat="server" placeholder="学校名"></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton ID="Search_Btn" runat="server" OnClick="Search_Btn_Click" CssClass="btn btn-primary"><span class="fa fa-search"></span></asp:LinkButton>
            </span>
        </div>
        <div class="pull-left" style="margin-left:10px; line-height:30px;">
            <label><asp:CheckBox ID="NoProvince_Check" Checked="true" runat="server" />忽略省份</label> 
            <label><asp:CheckBox ID="NoCity_Check" Checked="true" runat="server" />忽略城市</label> 
            <label><asp:CheckBox ID="NoCounty_Check" Checked="true" runat="server" />忽略县</label> 
        </div>
    </div>
</ol>
<table id="EGV" class="table table-striped table-bordered table-hover content_list">
    <tr>
        <td class="td_s"></td>
        <td>ID</td>
        <td class="egv_td_min60">学校微标</td>
        <td>学校名称</td>
        <td>地址</td>
        <td>添加时间</td>
        <td>操作</td>
    </tr>
    <ZL:Repeater ID="RPT" runat="server" PageSize="10" PagePre="<tr><td><label class='allchk_l'><input type='checkbox' id='chkAll'/>全选</label></td><td colspan='8'><div class='text-center'>" PageEnd="</div></td></tr>" OnItemDataBound="RPT_ItemDataBound" OnItemCommand="RPT_ItemCommand">
        <ItemTemplate>
            <tr>
                <td><input name="idchk" type="checkbox" value='<%#Eval("ID")  %>' /></td>
                <td><%#Eval("ID") %></td>
                <td><%#GetIcon(Eval("Country").ToString()) %></td>
                <td><%#Eval("SchoolName") %></td>
                <td><%#Eval("Province") + " " + Eval("City") + " " + Eval("County") %></td>
                <td><%#Eval("AddTime","{0:yyyy年MM月dd日 HH:mm}") %></td>
                <td>
                    <a href="SchoolShow.aspx?id=<%#Eval("ID") %>" title="浏览"><i class="fa fa-eye"></i></a>
                    <a href="AddSchool.aspx?id=<%#Eval("ID") %>" title="修改"><i class="fa fa-pencil"></i></a>
                    <asp:LinkButton ID="LinkButton1" CommandName="del1" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('是否删除!')" ToolTip="删除" runat="server"><i class="fa fa-trash"></i></asp:LinkButton>
                    <a href="AddClassRoom.aspx?sid=<%#Eval("ID") %>"><i class="fa fa-plus"></i>添加班级</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </ZL:Repeater>
</table>
<asp:Button ID="DelBtn" CssClass="btn btn-primary" runat="server" Text="批量删除" OnClick="DelBtn_Click" OnClientClick="return confirm('你确认要删除选定的记录吗？')}" />
<asp:HiddenField runat="server" ID="address_hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
<script type="text/javascript">
    var pcc = new ZL_PCC("tbProvince", "tbCity", "tbCounty");
    $(function () {
        $("#EGV tr").dblclick(function () {
            window.location.href = "AddSchool.aspx?id="+$(this).find("input[name=idchk]").val();
        });
        $("#chkAll").click(function () {//EGV 全选
            selectAllByName(this, "chkSel");
        });
        $(".schooldp").change(function () {
            setTimeout(function () {
                var val = $("#tbProvince").val() + "," + $("#tbCity").val() + "," + $("#tbCounty").val();
                $("#address_hid").val(val);
            }, 200);
        });
        if ($("#address_hid").val() != "") {
            var defdata = $("#address_hid").val().split(',');
            pcc.SetDef(defdata[0], defdata[1], defdata[2]);
            pcc.ProvinceInit();
        }
        else { pcc.ProvinceInit(); }
    })
    function IsSelectedId() {
        var checkArr = $("input[type=checkbox][name=chkSel]:checked");
        if (checkArr.length > 0)
            return true
        else
            return false;
    }
    $("#sel_btn").click(function (e) {
        if ($("#sel_box").css("display") == "none") {
            $(this).addClass("active");
            $("#sel_box").slideDown(300);
        }
        else {
            $(this).removeClass("active");
            $("#sel_box").slideUp(200);
        }
    });
</script>
</asp:Content>

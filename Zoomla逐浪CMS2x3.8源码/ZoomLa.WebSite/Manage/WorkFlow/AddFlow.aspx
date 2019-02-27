<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddFlow.aspx.cs" Inherits="Manage_WorkFlow_AddFlow" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>流程设计</title>
<style>
.breadcrumb{line-height:30px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb ">
        <li><a href="/Admin/Main.aspx">工作台</a></li>
        <li><a href="Default.aspx">流程列表</a></li>
        <li class="active">
            <asp:Label runat="server" ID="ProceName_L"></asp:Label>
            <a href="AddFlowStep.aspx?proID=<%=ProID %>&Add=1">[添加步骤]</a>
            <div class="input-group pull-right" style="width: 300px; margin-left: 10px;">
                <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control" />
                <span class="input-group-btn">
                    <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" />
                </span>
            </div>
        </li>
    </ol>
    <div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1" OnRowDataBound="EGV_RowDataBound" Width="100%" CssClass="table table-striped table-bordered table-hover" OnPageIndexChanging="EGV_PageIndexChanging" RowStyle-CssClass="tdbg" BackColor="White" DataKeyNames="ID" OnRowCommand="EGV_RowCommand" AllowUserToOrder="true" EmptyDataText="当前没有信息!!">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                    </ItemTemplate><ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="序号" DataField="StepNum" />
                <asp:BoundField HeaderText="步骤名" DataField="StepName" />
                <asp:TemplateField HeaderText="经办人">
                    <ItemTemplate>
                        <%#GetUserInfo(Eval("ReferUser","{0}"),Eval("ReferGroup","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="抄送人">
                    <ItemTemplate>
                        <%# GetUserInfo(Eval("CCUser","{0}"),Eval("CCGroup","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="会签">
                    <ItemTemplate>
                        <%# GetHQoption(Eval("HQoption","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="强制转交">
                    <ItemTemplate>
                        <%# GetQzzjoption(Eval("Qzzjoption","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="回退">
                    <ItemTemplate>
                        <%# GetHToption(Eval("HToption","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="备注" DataField="Remind" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="AddFlowStep.aspx?proID=<%= ProID %>&stepID=<%#Eval("ID") %>">查看详情</a>|
                            <a onclick="uptd(this)">上移</a>|
                            <a onclick="downtd(this)">下移</a>|
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除">
                              删除
                            </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
        <button type="button"  onclick="OrderList()" name="Order" class="btn btn-primary" id="orderBtn" runat="server" visible="false">保存排序</button>
        <button type="button" class="btn btn-primary" onclick="ShowImgDiag()">查看流程图</button>
    </div>
    <%--<div id="Addstep" runat="server" visible="false">
           <table class="table table-bordered" style="width:100%;">
            <tr><td>序号:</td><td><asp:TextBox runat="server" CssClass="form-control" ID="stepCodeT" ReadOnly="true" Width="60" style="text-align:center;"/></td></tr>
            <tr><td>步骤名称:</td><td><asp:TextBox runat="server" CssClass="form-control" Width="300" ID="stepNameT" /></td></tr>
             <tr><td>允许发起人:</td><td><asp:TextBox runat="server" ID="sendManT" />
                    <input type="button" id="sendManBtn" value="选择" class="btn btn-info" onclick="showdiv('div_share', 'sendMan');"/>
                    <asp:HiddenField runat="server" ID="sendManD" />提示:只有第一步,这里才可以选择
                                </td></tr>
            <tr><td>经办人:</td><td><asp:TextBox runat="server" CssClass="form-control pull-left" ID="referUserT" Width="300" />
                <input type="button" id="refSelBtn" style="margin-left:10px;" value="选择" class="btn btn-info" onclick="showdiv('div_share', 'referUser');"/>
                <asp:HiddenField runat="server" ID="referUserD" />
                                </td></tr>
            <tr><td>抄送人员:</td><td><asp:TextBox CssClass="form-control pull-left" runat="server" ID="ccUserT" Width="300" />
                <input type="button" id="ccSelBtn" value="选择" style="margin-left:10px;" class="btn btn-info" onclick="showdiv('div_share', 'ccUser');"/>提示:只有查看,无审批权限
                    <asp:HiddenField runat="server" ID="ccUserD" />
                                </td></tr>
            <tr><td>会签选项:</td><td><asp:DropDownList runat="server" ID="hqOptionDP">
                <asp:ListItem value="0">任意一人即可</asp:ListItem>
                <asp:ListItem value="1">必须全部审核</asp:ListItem></asp:DropDownList>
                    提示:选择必须全部审核,则需要所有经办人审核并同意,才能进入下一步骤</td></tr>
            <tr><td>强制转交:</td><td>经办人未办理完毕时是否允许主办人强制转交:
                <asp:DropDownList runat="server" ID="qzzjDP">
                    <asp:ListItem Value="1">允许</asp:ListItem>
                    <asp:ListItem Value="0">不允许</asp:ListItem>
                </asp:DropDownList></td></tr>
            <tr><td>回退选项：</td><td>是否允许回退：
                <asp:DropDownList runat="server" ID="htDP">
                    <asp:ListItem Value="0">不允许</asp:ListItem>
                    <asp:ListItem Value="1">允许回退上一步骤</asp:ListItem>
                    <asp:ListItem Value="1">允许回退之前步骤</asp:ListItem>
                </asp:DropDownList></td></tr>
            <tr><td>转交时,邮件自动通知以下人员：</td>
                <td><asp:TextBox runat="server" CssClass="form-control pull-left" Width="300" ID="emailAlertT" />
                    <input type="button" id="emailSelBtn" style="margin-left:10px;" value="选择" class="btn btn-info" onclick="showdiv('div_share', 'emailAlert');"/>
                    <asp:HiddenField runat="server" ID="emailAlertD" />
                </td></tr>
            <tr><td>转交时,短信通知以下人员：</td>
                <td><asp:TextBox runat="server" CssClass="form-control pull-left" Width="300" ID="smsAlertT" />
                    <input type="button" id="smsSelBtn" style="margin-left:10px;" value="选择" class="btn btn-info" onclick="showdiv('div_share', 'smsAlert');"/>
                    <asp:HiddenField runat="server" ID="smsAlertD" />
                </td></tr>
            <tr><td>公共附件选项：</td><td>
                    <asp:DropDownList runat="server" ID="PublicAttachOptionDP">
                    <asp:ListItem Value="0">不允许删除</asp:ListItem>
                    <asp:ListItem Value="1">允许删除</asp:ListItem>
                </asp:DropDownList></td></tr>
            <tr><td>流程简述：</td><td><asp:TextBox runat="server" ID="remindT" CssClass="form-control pull-left" Width="300" /></td></tr>
            <tr><td>操作：</td><td>
                <asp:Button runat="server" ID="saveBtn" Text="添加" OnClick="saveBtn_Click" CssClass="btn btn-primary"/>
                <input type="button" value="删除" class="btn btn-primary" /></td></tr>
        </table>
       </div>--%>
    <div id="div_share" class="panel panel-primary" style="display: none; position: absolute; z-index: 3;">
        <div class="panel-heading">
            <h3 class="panel-title">选择会员</h3>
        </div>
        <div class="panel-body">
            <iframe id="main_right" style="z-index: 2; visibility: inherit; overflow: auto; overflow-x: hidden; width: 100%; height: 500px;" name="main_right" src="/Mis/OA/Mail/SelUser.aspx" frameborder="0"></iframe>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="ImgData_Hid" />
    <input type="hidden" id="image_hid" />
    <asp:HiddenField runat="server" ID="dataField" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        function OrderList() {
            var id = ""
            $("#EGV tr:not(:first):not(:last)").each(function () {
                id += $(this).children('td').eq(0).html() + ",";
            })
            id = id.substring(0, id.length - 1);
            postToCS(id)
        }
        function postToCS(a) {
            $.ajax({
                type: "Post",
                url: "AddFlow.aspx",
                data: { IDList: a, },
                success: function (data) { if (data == 1) { alert("修改成功"); } },
                error: function () { alert("发送失败"); }
            });
        }
        function uptd(obj) {
            var onthis = $(obj).closest("tr");
            var getup = $(obj).parent().parent().prev();
            var upid = parseInt(getup.children('td').eq(1).html());
            var downid = parseInt(onthis.children('td').eq(1).html());
            if (downid == 1) {
                alert("首行无法上移");
            }
            else {
                getup.children('td').eq(1).html(upid + 1);
                onthis.children('td').eq(1).html(downid - 1)
                $(getup).before(onthis);
            }
        }
        function downtd(obj) {
            var onthis = $(obj).parent().parent();
            var getdown = $(obj).parent().parent().next();
            var upid = parseInt(getdown.children('td').eq(1).html());
            var downid = parseInt(onthis.children('td').eq(1).html());
            if (downid == $("#EGV tr").length - 2)
            { alert("末行无法下移"); }
            else {
                getdown.children('td').eq(1).html(upid - 1);
                onthis.children('td').eq(1).html(downid + 1)
                $(getdown).after(onthis);
            }
        }
        
        var diag = new ZL_Dialog();
        function ShowImgDiag() {
            diag.title = "查看流程图";
            diag.url = "/mis/oa/flow/ImgWorkFlow.aspx?proid=<%:ProID %>";
            diag.maxbtn = false;
            diag.ShowModal();
        }
    </script>
    <script>
        function showdiv(div_id, f) {
            $("#dataField").val(f);
            var div_obj = $("#" + div_id);
            var h = (document.documentElement.offsetHeight) / 2;
            var w = (document.documentElement.offsetWidth - 400) / 2;
            div_obj.animate({ opacity: "show", left: w, top: h, width: div_obj.width, height: div_obj.height }, 300);
            div_obj.focus();
        }
        function hideDiv() {
            $("#div_share").hide();
        }
        //组名外加[],用户名则不加，用,隔开
        function seluser(groupJson, userJson) {
            var names = "";
            var ids = "";
            //for (var i = 0; i < groupJson.length; i++) {
            //    n += "[" + groupJson[i].Gname + "],";
            //}
            for (var i = 0; i < userJson.length; i++) {
                names += userJson[i].Uname + ",";
                ids += userJson[i].Uid + ",";
            }
            var flag = $("#dataField").val();
            $("#" + flag + "T").val(names);
            $("#" + flag + "D").val(ids);
            hideDiv();
        }
    </script>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPrompt.aspx.cs" Inherits="Plat_Addon_SetPrompt" MasterPageFile="~/Plat/Main.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<title>提示管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container platcontainer">
        <div class="child_head"><span class="child_head_span1"></span><span class="child_head_span2">提示管理</span></div>
        <table class="table table-bordered table-striped">   
            <tr><td class="td_md">标题</td><td><ZL:TextBox runat="server" ID="Title_T" CssClass="form-control text_300" MaxLength="60" /></td></tr>
            <tr><td>提示内容</td><td><asp:TextBox runat="server" ID="Content_T" CssClass="m715-50" TextMode="MultiLine" style="height:120px;" MaxLength="2000" /></td></tr>
            <tr><td>提示时间</td><td><asp:TextBox runat="server" ID="BeginDate_T" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss'});" CssClass="form-control text_300"/></td></tr>
            <tr><td>提示用户</td><td>
                <input type="button" value="选择" onclick="user.sel('manage', 'plat');" class="btn btn-info" />
                <table class="table table-bordered table-striped margin_t5" style="width: 500px;">
                    <thead>
                        <tr>
                            <td>ID</td>
                            <td>姓名</td>
                            <td>操作</td>
                        </tr>
                    </thead>
                    <tbody id="manage_body"></tbody>
                </table>
                <asp:HiddenField runat="server" ID="manage_hid" />
                             </td></tr>
            <tr><td>提示列表</td><td>
                <table class="table table-bordered table-striped">
                    <tr>
                        <td>标题</td>
                        <td>内容</td>
                        <td>提示人</td>
                        <td>时间</td>
                     <%--   <td>状态</td>--%>
                        <td>操作</td>
                    </tr>
                    <ZL:ExRepeater runat="server" ID="RPT" PageSize="10" PagePre="<tr><td colspan='5' class='text-center'>" PageEnd="</td></tr>" OnItemCommand="RPT_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Title") %></td>
                                <td><%#Eval("Content") %></td>
                                <td><%#GetUser() %></td>
                                <td><%#Eval("BeginDate","{0:yyyy年MM月dd日 HH:mm}") %></td>
                              <%--  <td><%#GetStatus() %></td>--%>
                                <td>
                                    <a href="SetPrompt.aspx?ID=<%#Eval("ID") %>"><i class="fa fa-pencil"></i></a>
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确定要删除吗?');"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></FooterTemplate>
                    </ZL:ExRepeater>
                </table>
                <a href="NotifyList.aspx" title="提示列表" >点击查看全部提示信息 >></a>
               </td></tr>
            <tr><td></td><td>
                <asp:Button runat="server" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click" CssClass="btn btn-info" />
                <a href="/Plat/Blog/" class="btn btn-default">返回</a>
             </td></tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script>
    var manage = { select: "manage", list: [], $db: $("#manage_hid"), $body: $("#manage_body"), tlp: '<tr id="tr_@UserID"><td>@UserID</td><td>@HoneyName</td><td><a href="javascript:;" onclick="manage.del(@UserID);" title="删除"><i class="fa fa-remove"></a></td></tr>' };
    manage.init = function () {
        var ref = this;
        var val = ref.$db.val();
        if (val && val != "" && val != "[]") { ref.list = JSON.parse(val); ref.$db.val(ref.list.GetIDS("UserID")); ref.render(); }
        user.hook[ref.select] = function (list, select) {
            //console.log(list,select);
            ref.list.addAll(list, "UserID");
            ref.render();
            CloseComDiag();
        }
    }
    manage.render = function () {
        var ref = this;
        var $items = JsonHelper.FillItem(ref.tlp, ref.list, null);
        ref.$body.html("").append($items);
        ref.$db.val(ref.list.GetIDS("UserID"));
    }
    manage.del = function (uid) {
        var ref = this;
        ref.$body.find("#tr_" + uid).remove();
        ref.list.RemoveByID(uid, "UserID");
        ref.$db.val(manage.list.GetIDS("UserID"));
    }
    manage.init();
</script>
<%=Call.GetUEditor("Content_T") %>
</asp:Content>
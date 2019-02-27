<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddJsonP.aspx.cs" Inherits="ZoomLaCMS.Manage.APP.AddJsonP"MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加调用</title>
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <ul class="nav nav-tabs">
        <li class="active"><a href="#tabs0" data-toggle="tab">基本信息</a></li>
        <li><a href="#tabs1" data-toggle="tab">扩展信息</a></li>
    </ul>
    <div class="tab-content">
        <div id="tabs0" class="tab-pane active">
            <table class="table table-striped table-bordered">
        <tr>
            <td class="td_l tdleft"><strong>调用名称：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300" />
                <asp:RequiredFieldValidator runat="server" ID="RegAlias_Re" ErrorMessage="调用名称不能为空!" ControlToValidate="Alias_T" ForeColor="Red" Display="Dynamic" SetFocusOnError="false"></asp:RequiredFieldValidator>
                <input type="button" class="btn btn-info" onclick="LoadSingle();" value="装载单表示例" />
                <input type="button" class="btn btn-info" onclick="LoadDef();" value="多表示例" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>主表名：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="T1_T" CssClass="form-control text_300" /><span class="rd_green">主表别名为A</span>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="主表名不能为空!" ControlToValidate="T1_T" ForeColor="Red" Display="Dynamic" SetFocusOnError="false"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>次表名：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="T2_T" CssClass="form-control text_300" /><span class="rd_green">次表别名为B</span></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>主表主键：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="MyPK_T" CssClass="form-control text_300" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="主表主键不能为空!" ControlToValidate="MyPK_T" ForeColor="Red" Display="Dynamic" SetFocusOnError="false"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>返回字段：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="Fields_T" CssClass="form-control text_300" Text="" /><span class="rd_green">推荐字段名全小写,便于JS调用</span>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="主返回字段不能为空!" ControlToValidate="Fields_T" ForeColor="Red" Display="Dynamic" SetFocusOnError="false"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>ON联接筛选：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="ONStr_T" CssClass="form-control text_300" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>Wheret筛选：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="WhereStr_T" CssClass="form-control text_300" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>Order排序：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="OrderStr_T" CssClass="form-control text_300" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>开放参数：</strong></td>
            <td id="params_td">
                <div>
                    <input name="param" placeholder="参数名" class="form-control text_s" />
                    <select name="paramtype" class="form-control text_s">
                        <option value="string">string</option>
                        <option value="int">int</option>
                        <option value="like">like</option>
                    </select>
                    <input name="defvalue" placeholder="默认值" class="form-control text_s" />
                    <button type="button" onclick="AddParams()" class="btn btn-default"><span class="fa fa-plus"></span></button>
                </div>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>是否启用：</strong></td>
            <td>
                <input type="checkbox" runat="server" id="MyState_Chk" class="switchChk" checked="checked" /></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>备注信息：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="ReMark_T" TextMode="MultiLine" Height="100" CssClass="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>操作：</strong></td>
            <td>
                <asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click" Text="保存" />
                <a href="JsonPManage.aspx" class="btn btn-primary">返回</a></td>
        </tr>
    </table>
        </div>
        <div id="tabs1" class="tab-pane">
            <table class="table table-bordered table-striped">
                <tr><td class="td_l tdleft"><strong>SQL 语句：</strong></td><td><asp:TextBox runat="server" ID="SQL_T" style="height:120px;" CssClass="form-control" TextMode="MultiLine" /></td></tr>
                <tr><td class="tdleft"><strong>异常信息：</strong></td><td><asp:Label style="color:red;" runat="server" ID="Exception_L"></asp:Label></td></tr>
               <%-- <tr><td>查询结果：</td><td><asp:GridView runat="server" ID="EGV" AutoGenerateColumns="true" PageSize="10" EmptyDataText="没有查询到数据"></asp:GridView></td></tr>--%>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="Params_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script>
        //多表示例
        function LoadDef() {
            $("#Alias_T").val("myapi<%=DateTime.Now.ToString("_HHmmss")%>");
            $("#T1_T").val("ZL_CommonModel");
            $("#T2_T").val("ZL_C_Article");
            $("#MyPK_T").val("A.GeneralID");
            $("#Fields_T").val("A.generalid,A.title,A.createtime,B.author");
            $("#ONStr_T").val("A.GeneralID=B.ID");
            $("#WhereStr_T").val("A.Inputer=@Inputer OR A.Title LIKE @Title");
            $("#OrderStr_T").val("A.CreateTime DESC");
            var params = [{ name: 'Inputer', type: 'string', defval: 'Admin' }, { name: 'Title', type: 'string', defval: '测试标题' }];
            $("#Params_Hid").val(JSON.stringify(params));
            initParamData();
            $("#Params_T").val("Inputer|Title");
        }
        function LoadSingle() {
            $("#Alias_T").val("myapi<%=DateTime.Now.ToString("_HHmmss")%>");
            $("#T1_T").val("ZL_CommonModel");
            $("#MyPK_T").val("GeneralID");
            $("#Fields_T").val("generalid,title,createtime");
            $("#WhereStr_T").val("Title LIKE @Title");
            $("#OrderStr_T").val("CreateTime DESC");
            var params = [{ name: 'Title', type: 'like', defval: '测试标题' }];
            $("#Params_Hid").val(JSON.stringify(params));
            initParamData();
            $("#Params_T").val("Title");
        }
        var paramtlp = "<div><input name='param' placeholder='参数名' class='form-control text_s' /> <select name='paramtype' class='form-control text_s'><option value='string'>string</option><option value='int'>int</option><option value='like'>like</option></select>"
                     + " <input name='defvalue' placeholder='默认值' class='form-control text_s' /> <button type='button' onclick='removeParams(this)' class='btn btn-default'><span class='fa fa-minus'></span></button></div>";

        $().ready(function () {
            initParamData();
        });
        
        function initParamData() {
            if ($("#Params_Hid").val()) {
                $(".fa-minus").closest('div').remove();//清空添加的参数
                var paramarr = JSON.parse($("#Params_Hid").val());
                for (var i = 0; i < paramarr.length ; i++) {
                    var temparr = paramarr[i];
                    if (i == 0) {//第一个参数处理
                        $("[name='param']").val(temparr.name);
                        $("[name='paramtype'] option[value='" + temparr.type + "']").attr("selected", "selected");
                        $("[name='defvalue']").val(temparr.defval);
                        continue;
                    }
                    AddParams(temparr);
                }
            }
        }
        //添加开放参数
        function AddParams(paramobj) {
            if (!paramobj) {//添加空数据
                $("#params_td").append(paramtlp);
                return;
            }
            var $paramdata = $("<div>" + paramtlp + "</div>");
            $paramdata.find("[name='param']").attr("value", paramobj.name);
            $paramdata.find("[name='paramtype'] option[value='" + paramobj.type + "']").attr("selected", "selected");
            $paramdata.find("[name='defvalue']").attr("value", paramobj.defval);
            $("#params_td").append($paramdata.html());
        }
        function removeParams(obj) {
            $(obj).parent().remove();
        }


    </script>
</asp:Content>

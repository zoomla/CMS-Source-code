<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="Asklist.aspx.cs" Inherits="Manage_I_Guest_Asklist" EnableViewStateMac="false" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>问题回复</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td style="width:150px;" class="text-center">问答内容</td>
            <td><asp:Label ID="askContent" runat="server" style="margin-left:5px;"/></td>
        </tr>
        <tr>
            <td class="text-center">提 问 人</td>
            <td>
                <asp:Label ID="askName" runat="server" Text="提 问 人：" style="margin-left:5px;" />
            </td>
        </tr>
        <tr>
            <td class="text-center">提问时间</td>
            <td>
                <asp:Label ID="askTime" runat="server" Text="提问时间：" Style="margin-left: 5px;" />
            </td>
        </tr>
    </table>
    <ul class="nav nav-tabs">
        <li class="active"><a href="#tab0" onclick="ShowTabss(0)" data-toggle="tab">所有回复</a></li>
        <li><a href="#tab1" data-toggle="tab" onclick="ShowTabss(1)">待审回复</a></li>
        <li><a href="#tab2" data-toggle="tab" onclick="ShowTabss(2)">已审回复</a></li>
    </ul>
    <div class="user_t">
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Lnk_Click" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <%#Eval("ID")%>
                    </ItemTemplate>
                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="问题类型" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <%#gettypes(Eval("QueId", "{0}"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle CssClass="tdbg"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="参与人" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <%#Eval("UserName")%>
                    </ItemTemplate>
                    <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle CssClass="tdbg"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="互动时间" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <%#Eval("AddTime")%>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle CssClass="tdbg"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="回复及追问内容" HeaderStyle-Width="8%">
                    <ItemTemplate>
                        <label id="<%#Eval("ID") %>"> <%#Eval("Content")%></label>
                    </ItemTemplate>
                    <HeaderStyle Width="18%" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle CssClass="tdbg"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审核状态">
                    <ItemTemplate>
                        <%#getcommend(Eval("status"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle CssClass="tdbg"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Audit" CausesValidation="false">审核</asp:LinkButton>
                        |<asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Edit2" CausesValidation="false" Visible="false">修改|</asp:LinkButton>
                        <a href="javascript:disUpdate(<%#Eval("ID") %>)">修改|</a>
                        <asp:LinkButton ID="LinkButton3" runat="server" OnClientClick="return confirm('确实要删除?');"
                            CommandArgument='<%#Eval("ID") %>' CommandName="Del" CausesValidation="false">删除</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center"  />
            <RowStyle HorizontalAlign="Center" />
        </ZL:ExGridView>
        <asp:ObjectDataSource ID="askData" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAskListByID" TypeName="GuestAsk">
            <SelectParameters>
                <asp:QueryStringParameter Name="id" QueryStringField="ID" Type="String" />
                <asp:QueryStringParameter Name="type" QueryStringField="type" Type="String" DefaultValue="0" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <asp:Button ID="delBtn" runat="server" Text="删除" OnClick="DelBtn_Click" UseSubmitBehavior="False"
        OnClientClick="if(checkedNum()){if(!confirm('确定要批量删除吗？')){return false;}}else{return false;}" class=" btn btn-primary" />
    <asp:Button ID="Button2" runat="server" Text="审核通过" OnClick="BtnSubmit2_Click"  UseSubmitBehavior="False" class="btn btn-primary" />
    <asp:Button ID="Button3" runat="server" Text="取消审核" OnClick="BtnSubmit3_Click"  UseSubmitBehavior="False" class="btn btn-primary" />
    <input type="button"  value="回复问题" class="btn btn-primary" onclick="dialog('管理员回复');" />
    <br />
    
    <div id="replyDiv" style="display:none;">
        <div class="container-fluid text-center">
            <textarea id="txtContent" style="height: 300px; width: 100%; margin-top: 10px;" runat="server"></textarea>
            <input type="button" id="addBtn" value="管理员回复" onclick="if (addReply()) { disableBtn(this); }" class="btn btn-primary" />
            <input type="button" id="updateBtn" value="修改回复" onclick="updateReply()" class="btn btn-primary" style="display: none;" />
            <input type="button" value="返回提问列表" class="btn btn-primary" onclick="location = 'WdCheck.aspx'" style="margin-left: 5px;" />
        </div>
    </div>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            UE.getEditor("txtContent");
        });
        var diag = new ZL_Dialog();
        function dialog(title) {
            UE.getEditor("txtContent").setContent("");
            diag.title = title;
            diag.content = "replyDiv";
            diag.ShowModal();
        }
        function disableBtn(o)
        {
            setTimeout(function () { o.disabled = true; }, 50)
        }

        
        function addReply()
        {
            $("#updateBtn").hide();
            $("#addBtn").show();
            var obj = UE.getEditor("txtContent");
            if (obj.getContent() == "") { return false; }
            var id =<%=Request["id"]%>
                $.post("Asklist.aspx", { content: obj.getContent(), id: id, action: "add" },
                    function (data) {
                        if (data == 1) { $("#replyDiv").hide(); location = location; }
                        else { alert("添加失败"); }
                    }, "Json");
            return true;
        }
        var temp = 0;
        //显示修改
        function disUpdate(id)
        {
            $("#updateBtn").show();
            $("#addBtn").hide();
            dialog("修改回复");
            setTimeout(function(){UE.getEditor("txtContent").setContent($("#" + id).html());},500);
            temp = id;
        }
        //修改更新
        function updateReply()
        {
            var obj = UE.getEditor("txtContent");
            if (obj.getContent() == "") { return false;}
            $.post("Asklist.aspx", { content: obj.getContent(), id: temp, action: "update" },
                function (data) {
                    if (data == 1) { $("#replyDiv").hide(); location = location; }
                    else { alert("更新失败"); }
                }, "Json");
            return true;
        }
        function ShowTabss(ID)
        {
            location.href = 'Asklist.aspx?ID=<%=Request.QueryString["ID"] %>&type=' + ID;
        }
        $().ready(function () {
            
            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
        })
    </script>
    <style>
        th{ text-align:center;}
    </style>
</asp:Content>
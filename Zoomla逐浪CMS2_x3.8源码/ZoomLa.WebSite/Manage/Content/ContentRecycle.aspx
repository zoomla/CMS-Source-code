<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="ContentRecycle.aspx.cs" Inherits="Manage_I_Content_ContentRecycle" ClientIDMode="Static" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>回收站</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div>
            <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="GeneralID" PageSize="20" 
                OnPageIndexChanging="Egv_PageIndexChanging"  IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" 
                EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" title="" value='<%#Eval("GeneralID") %>' />
                        </ItemTemplate>
                        <HeaderStyle CssClass="hidden-xs hidden-sm" Width="5%" />
                        <ItemStyle CssClass="hidden-xs hidden-sm" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="GeneralID" HeaderText="ID"/>
                    <asp:TemplateField HeaderText="标题">
                        <ItemTemplate>
                            <div style="position: relative;cursor:pointer;">
                                <span onmouseover="ShowPopover(this)" onmouseout="HidePopover(this)" style="float:left;">
                                    <%# GetTitle()%>
                                </span>
                                <div class="popover bottom" style="right: 0px; top: 20px; max-width: none;">
                                    <div class="arrow"></div>
                                    <h3 class="popover-title"><%#Eval("Title") %><span style="margin-left:5px;"><%# Convert.ToDateTime(Eval("CreateTime", "{0}")).ToShortDateString() %></span><span class="badge pull-right"><%# Eval("Hits") %></span><span class="pull-right">点击数：</span></h3>
                                    <div class="popover-content">
                                        <%# GetContent(Eval("GeneralID","{0}")) %>
                                    </div>
                                </div>
                            </div> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="所属栏目">
                        <HeaderStyle Width="10%" />
                        <ItemTemplate>
                            <%# GetNodeName(Eval("NodeID", "{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="内容模型">
                        <HeaderStyle Width="10%" />
                        <ItemTemplate>
                            <%# GetModelName(Eval("ModelID", "{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Inputer" HeaderText="录入者"></asp:BoundField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
        <div class="text-center">
            <asp:Button ID="btnClear" CssClass="btn btn-primary" runat="server" Text="彻底删除选中" OnClick="btnClear_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('数据删除后不可恢复，确认要删除选中项？')}" UseSubmitBehavior="true" />
            <asp:Button ID="btnRevert" CssClass="btn btn-primary" runat="server" Text="还原选中" OnClick="btnRevert_Click" OnClientClick="if(!IsSelectedId()){alert('请选择内容');return false;}else{return confirm('还原后请进入内容对信息进行审核！')}" />
            <asp:Button ID="btnRevertAll" CssClass="btn btn-primary" runat="server" Text="还原所有" OnClientClick="return confirm('还原后请进入内容对信息进行审核！')" OnClick="btnRevertAll_Click" />
            <asp:Button ID="btnClearAll" CssClass="btn btn-primary" runat="server" Text="清空回收站" OnClick="btnClearAll_Click" OnClientClick="return confirm('数据删除后不可恢复，确认清空？')" />
        </div>
        <asp:HiddenField runat="server" ID="GeneralID_Hid" />
        <asp:Button ID="DBlclickDel" runat="server" OnClick="DBlclickDel_Click" style="display:none;" />
    </div>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $(".tvNav a.list1").click(function () { showlist(this); });
            if (window.localStorage.Recycle_tvNav) {
                expand($("#" + window.localStorage.Recycle_tvNav));
            }
            
        })
        document.onkeyup=function GetKeyCode()
        {
            if (event.keyCode == 46)
            {
                $("#btnClear").trigger("click");
            }
        }
        function showlist(obj) {
            $(obj).parent().parent().find("a").removeClass("activeLi");//a-->li-->ul
            $(obj).parent().children("a").addClass("activeLi");//li
            $(obj).parent().siblings("li").find("ul").slideUp();
            $(obj).parent().children("ul").slideToggle();
            window.localStorage.Recycle_tvNav = obj.id;
        }
        function expand(obj)//超链接,不以动画效果显示
        {
            $a = $(obj).parent().parent(".tvNav ul").parent("li").find("a:first");
            if ($a.length > 0) expand($a);
            $(obj).addClass("activeLi");
            $(obj).parent("li").children("ul").show();
        }
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=idchk]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
        function ShowPopover(obj)
        {
            $(obj).parent().find(".popover").show();
        }
        function HidePopover(obj)
        {
            $(obj).parent().find(".popover").hide();
        }
        function DbDel(id)
        {
            $("#GeneralID_Hid").val(id);
            $("#DBlclickDel").trigger("click");
        }
    </script>
</asp:Content>


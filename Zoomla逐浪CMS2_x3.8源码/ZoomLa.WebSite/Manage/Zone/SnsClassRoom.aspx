<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SnsClassRoom.aspx.cs" Inherits="manage_Zone_SnsClassRoom" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>班级信息管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active" data-tabid="-1"><a href="SnsClassRoom.aspx?status=-1">全部</a></li>
        <li role="presentation" data-tabid="1"><a href="SnsClassRoom.aspx?status=1">已审核</a></li>
        <li role="presentation" data-tabid="0"><a href="SnsClassRoom.aspx?status=0" >未审核</a></li>
      </ul>
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="RoomID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Egv_RowCommand" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" title="" value='<%#Eval("RoomID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="RoomID" />
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <a href="ClassShow.aspx?cid=<%#Eval("RoomID") %>"><%#Eval("RoomName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="班标">
                <ItemTemplate>
                    <%#GetIcon() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="所属学校">
                <ItemTemplate>
                    <%#Eval("SchoolName")%>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="创建人">
                <ItemTemplate>
                    <%#string.IsNullOrEmpty(Eval("UserName").ToString())?"管理员":Eval("UserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="是否毕业">
                <ItemTemplate>
                    <%#GetIsDone() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="星级">
                <ItemTemplate>
                    <%#GetStar() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="添加时间">
                <ItemTemplate>
                    <%#Eval("Creation")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="审核状态">
                <ItemTemplate>
                    <%#GetAudit() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="学生人数" DataField="StuCount" />
            <asp:BoundField HeaderText="教师人数" DataField="TeachCount" />
            <asp:BoundField HeaderText="家长人数" DataField="FamilyCount" />
            <asp:TemplateField HeaderText="相关操作">
                <ItemTemplate>
                    <a href="ClassShow.aspx?cid=<%#Eval("RoomID") %>" title="预览"><span class="fa fa-eye"></span></a>
                    <a href="AddClassRoom.aspx?menu=edit&id=<%#Eval("RoomID") %>" title="修改"><span class="fa fa-pencil"></span></a>
                    <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("RoomID") %>' CommandName="del" OnClientClick="return confirm('是否删除!')" ToolTip="删除" runat="server"><span class="fa fa-trash"></span></asp:LinkButton>
                    <a href="StudentList.aspx?cid=<%#Eval("RoomID") %>&stutype=1" class="option_style"><i class="fa fa-child"></i>学生列表</a>
                    <a href="StudentList.aspx?cid=<%#Eval("RoomID") %>&stutype=2" class="option_style"><i class="fa fa-user"></i>教师列表</a>
                    <a href="StudentList.aspx?cid=<%#Eval("RoomID") %>&stutype=3" class="option_style"><i class="fa fa-users"></i>家长列表</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Button1" class="btn btn-primary" runat="server" OnClick="Button1_Click" OnClientClick="return confirm('是否删除?')" Text="批量删除" />
    <asp:Button ID="Button2" class="btn btn-primary" runat="server" OnClick="Button2_Click" Text="批量审核" />
    <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClick="Button3_Click" Text="取消审核" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
            $("li[data-tabid]").removeClass('active');
            $("li[data-tabid='<%=ClassStatus %>']").addClass('active');
        })
        function IsSelectedId()
        {
            var checkArr = $("input[type=checkbox][name=chkSel]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>

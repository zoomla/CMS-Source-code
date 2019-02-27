<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="StructList.aspx.cs" Inherits="manage_AddOn_StructList" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>组织结构</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div>
           <asp:Label runat="server" ID="curStr_L" Visible="false"></asp:Label>
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
                class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" value="<%#Eval("ID") %>"/><asp:Label ID="Lbl" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="ID" DataField="ID"/>
                    <asp:TemplateField HeaderText="名称">
                        <ItemTemplate>
                            <img src='/Images/TreeLineImages/t.gif' border='0' /><a href="AddStruct.aspx?ID=<%#Eval("ID") %>"><%#GetIcon() %> <%#GetName() %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建人">
                        <ItemTemplate>
                            <%#getName(Eval("UserID").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间">
                        <ItemTemplate>
                        <%#Eval("AddTime") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="开放">
                        <ItemTemplate>
                            <%#getOpen(Eval("Opens").ToString()) %> 
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%#getStatus(Eval("Status").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成员数量">
                        <ItemTemplate>
                            <%#GetCount(Eval("ID")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                             <asp:LinkButton CommandName="Edit" CommandArgument='<%#Eval("ID") %>' ToolTip="修改" runat="server"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                            <a href='javascript:;' onclick='delstruct(<%#Eval("ID") %>)'><i class='fa fa-trash-o' title="删除"></i></a>
                            <a href="StructMenber.aspx?id=<%#Eval("ID") %>" title="成员管理"><i class="fa fa-users" title="管理"></i>成员管理</a>
                            <asp:LinkButton ID="Add_lbn" CommandName="Add" CommandArgument='<%#Eval("ID") %>' Text="添加子级" runat="server"><i class="fa fa-plus"></i>添加子级</asp:LinkButton>
                            <a <%#IsHasChild(Eval("ID")) %> ><i class="fa fa-eye"></i>查看子级</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
       <asp:Button ID="Button1" class="btn btn-primary" Text="批量排除" runat="server" OnClientClick="if(!IsSelectedId()){alert('请选择内容');return false;}else{return confirm('不可恢复性数据,你确定将该成员从此结构中排除吗？');}" OnClick="Button1_Click" /></td>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
        var type = '<%=Request["type"] %>';
        var trtlp = "<tr data-pid='@pid' data-index='@index'><td><input name='idchk' type='checkbox' value='@id'></td><td>@id</td><td>@treeindex<img src='/Images/TreeLineImages/t.gif' border='0' /><a href='AddStruct.aspx?ID=@id'>@icon @title</a></td>"
                        + "<td>@inputer</td><td>@addtime</td><td>@opens</td><td>@status</td><td>@usercount</td>"
                        + "<td><a href='StructMenber.aspx?id=@id' title='成员管理'><i class='fa fa-users'></i></a> <a href='AddStruct.aspx?ID=@id&type="+type+"'><i class='fa fa-pencil'></i></a> "
                        + "<a href='javascript:;' onclick='delstruct(@id)'><i class='fa fa-trash-o'></i></a> <a href='AddStruct.aspx?pid=@id&type="+type+"'><i class='fa fa-plus'></i>添加子级</a></td></tr>";
        $().ready(function () {
            $("#EGV tr").each(function () {
                GetChildList($(this));
            });
            $("#EGV tr").click(function () {
                GetChildList($(this));
            });
        });
        //获取子节点
        function GetChildList($tr) {
            var id = $tr.find("[name='idchk']").val();
            if (!id)
                return;
            $.ajax({
                type: 'POST',
                data: { action: 'getchild', pid: id },
                success: function (data) {
                    var datas = JSON.parse(data);
                    var index=$tr.data('index')?parseInt($tr.data('index'))+1:1;//节点层数
                    var treeicon="";//层级图标(空格图)
                    for (var i = 0; i < index; i++) {
                        treeicon+="<img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20' />";
                    }
                    var html = "";
                    for (var i = 0; i < datas.length; i++) {
                        html += trtlp.replace(/@id/g, datas[i].ID).replace(/@treeindex/g, treeicon)
                        .replace(/@title/g, datas[i].Name).replace(/@inputer/g, datas[i].UserName).replace(/@addtime/g, datas[i].AddTime).replace(/@opens/g, datas[i].OpensStr)
                        .replace(/@status/g, datas[i].StatusStr).replace(/@pid/g, id).replace(/@usercount/g, datas[i].UserCount).replace(/@index/g, index)
                        .replace(/@icon/g, datas[i].childCount > 0 ? "<span class='icon fa fa-plus-square'></span>" : "");
                    }
                    $(html).insertAfter($tr);
                    $tr.unbind('click');
                    $tr.parent().find("[data-pid='" + id + "']").click(function () { GetChildList($(this)); });
                    $tr.parent().find("[data-pid='" + id + "']").bind('ondisplay', function () { TrDisplayEvent($(this), $tr) });
                    $tr.parent().find("[data-pid='" + id + "']").trigger('ondisplay');//触发显示隐藏事件
                    $tr.dblclick(function () { $tr.parent().find("[data-pid='" + id + "']").toggle(); $tr.parent().find("[data-pid='" + id + "']").trigger('ondisplay') });
                }
            });
        }
        //行级显示与隐藏事件
        function TrDisplayEvent($tr,$parent) {
            var id = $tr.find("[name='idchk']").val();
            if ($tr.is(":hidden")) {
                $parent.find('.icon').removeClass('fa  fa-minus-square').addClass('fa  fa-plus-square');
                $tr.parent().find("[data-pid='" + id + "']").hide();
                $tr.parent().find("[data-pid='" + id + "']").trigger('ondisplay');
            } else {
                $parent.find('.icon').removeClass('fa  fa-plus-square').addClass('fa fa-minus-square');
            }
        }
        function delstruct(id) {
            if (confirm('是否删除该项!')) {
                $.ajax({
                    type: 'POST',
                    data: { action: 'del', id: id },
                    success: function (data) {
                        if (data == '1')
                            window.location = location;
                        else
                            alert('删除失败!');
                    }
                });
            }
        }
    </script>
</asp:Content>

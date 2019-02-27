<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageStyle.aspx.cs" Inherits="manage.Page.PageStyle" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>黄页模板</title>
    <style>
        .temple_img{width:30px;height:30px;}
        .templebig_img{position:absolute;max-width:300px;max-height:300px;display:none;margin-left:5px;}
         #EGV tr td{padding-left:8px;padding-bottom:3px;padding-top:3px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="EGV_RowDataBound"
         OnPageIndexChanging="ExGridView1_PageIndexChanging"  PageSize="10"  class="table table-striped table-bordered table-hover">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("PageNodeid") %>" />
                </ItemTemplate>
                <ItemStyle CssClass="td_s" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="PageNodeid" />
            <asp:TemplateField HeaderText="模板预览">
                <ItemTemplate>
                    <img class="temple_img" onerror="shownopic(this);" src="<%#Eval("TemplateIndexPic") %>" />
                    <img class="templebig_img" onerror="shownopic(this);" src="<%#Eval("TemplateIndexPic") %>" />
                </ItemTemplate>
                <ItemStyle CssClass="td_m" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="样式名称" DataField="PageNodeName" />
            <asp:TemplateField HeaderText="启用状态">
                <ItemTemplate>
                    <%#getistrue(Eval("isTrue","{0}")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="添加时间">
                <ItemTemplate>
                    <%#Eval("Addtime") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="排列顺序" DataField="Orderid" />
            <asp:TemplateField HeaderText="相关操作">
                <ItemTemplate>
                    <a href="AddPageStyle.aspx?menu=edit&sid=<%#Eval("PageNodeid") %>" class="option_style"><span class="fa fa-pencil" title="修改"></span></a>
                    <a href="?menu=del&id=<%#Eval("PageNodeid") %>" class="option_style" onclick="return confirm('确实删除此黄页样式? 删除该样式后前台黄页将无法使用该样式!');"><i class="fa fa-trash-o" title="删除"></i></a>
                    <a href="PageTemplate.aspx?styleid=<%#Eval("PageNodeid") %>" title="栏目管理" class="option_style"><i class="fa fa-th-list" title="栏目管理"></i>栏目管理</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Enable_Btn" runat="server" CssClass="btn btn-primary" OnClick="Enable_Btn_Click" Text="启用" />
    <asp:Button ID="Disble_Btn" runat="server" CssClass="btn btn-primary" OnClick="Disble_Btn_Click" Text="停用" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function getinfo(id) {
            location.href = 'PageTemplate.aspx?styleid=' + id;
        }

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
        HideColumn("3,4,5");
        $().ready(function () {
            $('.temple_img').hover(function () { $(this).next().show(); }, function () { $(this).next().hide(); });
        });
    </script>
</asp:Content>
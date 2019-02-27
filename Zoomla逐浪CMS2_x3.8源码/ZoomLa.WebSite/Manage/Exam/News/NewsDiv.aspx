<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsDiv.aspx.cs" Inherits="Manage_I_Exam_NewsDiv" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>版面管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"
        CssClass="table table-striped table-bordered table-hover" EmptyDataText="还没为该报纸设置版面!!"
        OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="版面名">
                <ItemTemplate>
                    <a href="Publish.aspx?Pid=<%#Eval("Pid")+"&ID="+Eval("ID") %>"> <%#Eval("Title") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="图片">
                <ItemTemplate>
                    <img class="mini_img" src="<%#Eval("ImgPath") %>" style="width: 50px; height: 50px;"/>
                    <img class="mid_img" id="mid_img" src="#" style="display:none;position:absolute;"/> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="高清附件">
                <ItemTemplate>
                    <a href="<%#Eval("AttachFile") %>" title="点击下载" target="_blank"><%#System.IO.Path.GetFileName(Eval("AttachFile") as string) %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="/Rss/News/ViewPublish.aspx?Pid=<%#Eval("Pid") %>&ID=<%#Eval("ID") %>" target="_blank" title="前台浏览" class="option_style"><i class="fa fa-globe" title="浏览"></i></a><span class="sperspan"></span>
                    <a href="Publish.aspx?Pid=<%#Eval("Pid") %>&ID=<%#Eval("ID") %>" target="_parent" class="option_style"><i class="fa fa-pencil" title="修改"></i></a><span class="sperspan"></span>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Del2" OnClientClick="return confirm('确定要删除该版面?');"  CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .curspan{color:gray;padding-left:5px;padding-right:5px;}
        .sperspan{color:gray;padding-left:5px;}
    </style>
    <script type="text/javascript">
        $().ready(function () {
            $(".mini_img").mouseover(function () {
                console.log("123");
                $(".mid_img").hide();
                $(this).siblings("#mid_img").attr("src", $(this).attr("src")).show();
            }).mouseout(function () { $(this).siblings("#mid_img").hide(); });
        });
    </script>
</asp:Content>
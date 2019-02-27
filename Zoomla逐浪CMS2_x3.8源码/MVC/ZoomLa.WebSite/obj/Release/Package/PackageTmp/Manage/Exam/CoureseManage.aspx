<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoureseManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.CoureseManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>课程管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
    CssClass="table table-striped table-bordered table-hover" EmptyDataText="<%$Resources:L,当前没有信息 %>" 
    OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="EGV_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input name="idchk" type="checkbox" value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="课程名称" DataField="CourseName" />
            <asp:BoundField HeaderText="课程缩写" DataField="CoureseThrun" />
            <asp:BoundField HeaderText="课程代码" DataField="CoureseCode" />
            <asp:TemplateField HeaderText="热门课程">
                <ItemTemplate><%#GetHot(Eval("Hot","{0}"))%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程分类">
                <ItemTemplate><%#GetClass(Eval("CoureseClass","{0}"))%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="学分" DataField="CoureseCredit" />
            <asp:BoundField HeaderText="简介" DataField="CoureseRemark" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddCoures.aspx?id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                    <a href="?menu=delete&id=<%#Eval("ID")%>" OnClick="return confirm('确实要删除此课程?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a>
                    <a href="AddCourseware.aspx?CourseID=<%#Eval("ID")%>" class="option_style"><i class="fa fa-list-alt" title="列表"></i>课件列表</a> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
<div>
    <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" onclick="Button3_Click" /></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
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
        $().ready(function () {
            $("#Egv tr").dblclick(function () {
                var id = $(this).find("[name=Item]").val();
                if (id) {
                    location = "AddCoures.aspx?id="+id;
                }
            });
        });
    </script>
</asp:Content>
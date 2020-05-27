<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyTlpList.aspx.cs" Inherits="App_APPTlp_MyTlpList" MasterPageFile="~/Common/Master/Commenu.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>我的模板</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div id="stepbar" style="padding-left: 140px; margin-bottom: 10px;">
                <ul class="step_bar">
                    <li class="step g_step1"><i class="fa fa-desktop active"></i><a class="g_a_step1" href="javascript:;">设定参数</a></li>
                    <li class="green_line"></li>
                    <li class="step g_step2"><a class="g_a_step2" href="javascript:;"><i class="fa fa-paint-brush"></i>定制效果</a></li>
                    <li class="green_line"></li>
                    <li class="step step3"><a class="a_step3" href="javascript:;"><i class="fa fa-android"></i>生成APP</a></li>
                    <li>
                        <a href="/App/APPList.aspx" class="btn btn-info" style="margin-top:8px;" >我的APP</a>
                        <a href="TlpList.aspx" class="btn btn-info" style="margin-top:8px;">在线模板</a>
                    </li>
                </ul>
                <div style="clear: both;"></div>
            </div>
        </div>
        <div class="panel-body" style="padding:0px;">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" CssClass="table table-bordered table-striped table-hover" EnableTheming="False" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" EmptyDataText='<div class="text-center">您还没有自己的模板,请点击上方"在线模板"按钮添加模板!</div>'>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                        </ItemTemplate>
                        <ItemStyle CssClass="td_s" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="名称">
                        <ItemTemplate>
                            <a href="/App/AppTlp/Design.aspx?id=<%#Eval("ID") %>&vpath=<%#Eval("TlpUrl") %>" target="_blank"><%#Eval("Alias") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="模板文件名">
                        <ItemTemplate>
                            <%#System.IO.Path.GetFileName(Eval("TlpUrl",""))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="创建时间" DataField="CDate" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="/App/AppTlp/Design.aspx?id=<%#Eval("ID") %>&vpath=<%#Eval("TlpUrl") %>" target="_blank">修改</a>
                            <a href="javascript:;" onclick="PreTlp('<%#Eval("TlpUrl") %>')" target="_blank">浏览</a>
                            <asp:LinkButton runat="server" CommandName="down" CommandArgument='<%#Eval("ID") %>'>下载</asp:LinkButton>
                         <%--   <asp:LinkButton runat="server" CommandName="capp" CommandArgument='<%#Eval("ID") %>'>生成APP</asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
            <div style="padding:5px;">
            <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" Text="批量删除" OnClientClick="return confirm('确认删除选中项?')" OnClick="Dels_Btn_Click" />
            </div>
        </div>
    </div>
    <div id="pretlp_div" style="display:none;">
        <iframe style="border: none; width: 275px; height: 463px;" id="pretlp_ifr"></iframe>
    </div>
    <style>
        .remind div {margin-bottom:3px;}
        .pretlp_div{width:310px;height:470px;}
    </style>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var diag = new ZL_Dialog();
        function PreTlp(src) {
            diag.title = "预览模板";
            diag.content = "pretlp_div";
            diag.width = "pretlp_div";
            diag.ShowModal();
            $("#pretlp_ifr").attr('src', src);
        }
    </script>
</asp:Content>


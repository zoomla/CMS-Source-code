<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CssManage.aspx.cs" Inherits="Manage_I_Content_CssManage" MasterPageFile="~/Manage/I/Default.master" %>

<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
    .sfile_body{width:375px;display:inline-block;margin-bottom:-13px;margin-left:5px;}
    </style>
    <title><%=Resources.L.风格管理 %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table width="100%" class="table table-bordered">
        <tr>
            <td align="left">
                <asp:Label ID="lblDir" runat="server"></asp:Label>
            </td>
            <td align="right">
                <asp:Literal ID="LitParentDirLink" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <div class="panel panel-default" style="padding: 0px;">
        <div>
            <table class="table table-striped table-bordered table-hover" style="border-top: none;">
                <tr class="gridtitle" align="center">
                    <td width="5%"><%=Resources.L.操作 %></td>
                    <td width="45%"><%=Resources.L.名称 %></td>
                    <td width="10%"><%=Resources.L.大小 %></td>
                    <td width="10%"><%=Resources.L.类型 %></td>
                    <td><%=Resources.L.修改时间 %></td>
                </tr>
                <asp:Repeater ID="repFile" runat="server" OnItemCommand="repFileReName_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <div class="option_area dropdown">
                                    <a class="option_style" href="javascript:;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars"></i><%=Resources.L.操作 %><span class="caret"></span></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Eval("Name").ToString()%>'
                                                CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "":"DownFiles" %>'
                                                Enabled='<%# System.Convert.ToInt32(Eval("type")) == 1 ? false:true %>' Visible='<%# System.Convert.ToInt32(Eval("type")) == 1 ? false : true %>' CssClass="option_style"><i class="fa fa-download" title="<%=Resources.L.下载 %>"></i><%=Resources.L.下载 %></asp:LinkButton>
                                        <li>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "CopyDir":"CopyFiles" %>'
                                                CommandArgument='<%# Eval("Name").ToString()%>' CssClass="option_style"><i class="fa fa-copy" title="<%=Resources.L.复制 %>"></i><%=Resources.L.复制 %></asp:LinkButton>
                                        <li>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Name").ToString()%>'
                                                CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                                                OnClientClick="return confirm('你确认要删除该文件夹或文件吗？')" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                                    </ul>
                                </div>
                            </td>
                            <td align="left">
                                <i style='color:#4586BD;' class='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "fa fa-folder" :"fa fa-file-text" %>'></i>
                                <%# System.Convert.ToInt32(Eval("type")) != 1&&(Eval("content_type").ToString()=="jpg"||Eval("content_type").ToString()=="gif"||Eval("content_type").ToString()=="jpge"||Eval("content_type").ToString()=="png")?"<span onmouseover=\"ShowADPreview('" + GetFileContent(Eval("Name").ToString(), Eval("content_type").ToString()) + "')\" onmouseout=\"hideTooltip('dHTMLADPreview')\">":"<span>"%>
                                <a href="<%#isvideo(Eval("content_type").ToString())?""+TemplateDir+"/"+Eval("Name")+"\" rel=\"vidbox":(isimg(Eval("content_type").ToString())?"javascript:void(0);": (System.Convert.ToInt32(Eval("type")) == 1 ?  "CssManage.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()):"CssEdit.aspx?filepath="+ Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()))) %>">
                                    <%# Eval("Name") %></a></span>
                            </td>
                            <td align="center">
                                <%# GetSize(Eval("size").ToString()) %>
                            </td>
                            <td align="center">
                                <asp:HiddenField ID="HdnFileType" Value='<%#Eval("type") %>' runat="server" />
                                <%# System.Convert.ToInt32(Eval("type")) == 1 ? Resources.L.文件夹 : Eval("content_type").ToString() + Resources.L.文件 %>
                            </td>
                            <td align="center">
                                <%#Eval("lastWriteTime")%>
                            </td>

                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="panel panel-footer" style="padding: 5px; margin: 0px;">
            <div>
                <asp:Button ID="btnCSSBackup" class="btn btn-info" runat="server" Style="width: 110px;" Text="<%$Resources:L,备份当前风格 %>" OnClientClick="return confirm('是否创建备份？(提示：备份同名文件会覆盖！）');" OnClick="btnCSSBackup_Click" />
                <%=Resources.L.目录名称 %>：
                <asp:TextBox ID="txtForderName" class="form-control text_300" runat="server" Width="350px"></asp:TextBox>
                <asp:Button ID="btnCreateFolder" class="btn btn-info" runat="server" Text="<%$Resources:L,创建目录 %>" OnClick="btnCreateFolder_Click" />
                <ZL:SFileUp ID="SFile_Up" runat="server" IsRelName="true" FType="All" />
                <asp:Button ID="btnTemplateUpLoad" class="btn btn-info" runat="server" Text="<%$Resources:L,上传风格 %>" OnClientClick="return confirm('即将覆盖同名风格，是否继续？');" OnClick="btnTemplateUpLoad_Click" />
            </div>
        </div>
    </div>
    <div id="dHTMLADPreview" style="z-index: 1000; left: 0px; visibility: hidden; width: 10px; position: absolute; top: 0px; height: 10px"></div>
    <div class="clearbox"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Popup.js"></script>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.UploadFile" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>上传文件管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td>
                当前目录：<asp:Label ID="LblCurrentDir" runat="server"></asp:Label><asp:HiddenField ID="HdnPath"
                    runat="server" />                
            </td>
            <td align="right">
                <%= string.IsNullOrEmpty(m_ParentDir) ? "<a disabled=\"true\">返回上一级</a>" : "<a href=\"UploadFile.aspx?Dir=" + m_ParentDir + "\">返回上一级</a>"%>
            </td>
        </tr>
    </table>
    <div class="filemanage">
    <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover" id="EGV">
                    <tr class="title" style="height:24px;text-align:center;">
                        <td>名称</td>
                        <td style="width: 60px">大小</td>
                        <td style="width: 80px">类型</td>
                        <td>创建时间</td>
                        <td style="width: 120px">最后修改时间</td>
                        <td style="width: 40px">操作</td>
                    </tr>                    
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align:left;width:20%;">
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<i class='fa fa-folder' style='color:#4586BD;'></i>" : GetShowExtension(Eval("content_type").ToString())%>
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href=\"UploadFile.aspx?Dir=" + Server.UrlEncode(CurrentDir + "/" + Eval("Name").ToString()) + "\">" + Eval("Name").ToString() + "</a>" : "<span onmouseover=\"ShowADPreview('" + GetFileContent(Eval("Name").ToString(), Eval("content_type").ToString()) + "')\" onmouseout=\"hideTooltip('dHTMLADPreview')\">" + "<a href='javascript:ReturnUrl(\"" + Request.QueryString["Dir"] + "/" + Eval("Name").ToString() + "\")'>" + Eval("Name").ToString() + "</a></span>"%>
                    </td>
                    <td align="center">
                        <%# GetSize(Eval("size").ToString())%>
                    </td>
                    <td align="center">
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %>
                    </td>
                    <td style="width:30%" align="center">
                        <%# Eval("createTime")%>
                    </td>
                    <td style="width:30%" align="center">
                        <%# Eval("lastWriteTime")%>
                    </td>
                    <td style="width:10%" align="center">
                        <asp:LinkButton ID="LbtnDelList" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                            CommandArgument='<%# Eval("Name")%>' OnClientClick="if(!this.disabled) return confirm('确定要删除?');" CssClass="option_style"
                             runat="server">[<i class="fa fa-trash-o" title="删除"></i>删除]</asp:LinkButton></td>
                </tr>                
            </ItemTemplate> 
            <FooterTemplate>
                </table></FooterTemplate>           
        </asp:Repeater>   
    </div>         
        <div class="clearbox"></div> 
        <div id="dHTMLADPreview" style="z-index: 1000; left: 0px; visibility: hidden; width: 10px;
            position: absolute; top: 0px; height: 10px">
        </div>
<%--        <asp:Button ID="Backup" runat="server" Text="备份当前目录文件" class="btn btn-primary" OnClick="Backup_Click1" OnClientClick="if(!this.disabled) return confirm('是否创建备份？(提示：备份同名文件会覆盖！)');"/>--%>
     <a href="javascript:;" id="upfile_btn" class="btn btn-primary">上传文件</a>
        <!-- 提示是否 -->
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Popup.js" type="text/javascript"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Webup.js"></script> 
    <script type="text/javascript">
        $(function () {
            $(".fa-file-photo-o").css("color", "#4586BD");
        });
        function ReturnUrl(url) {

            if (url.substring(0, 1) == "/") {
                url = url.substring(1, url.Length);
            }
            var isMSIE = (navigator.appName == "Microsoft Internet Explorer");
            if (isMSIE) {
                window.returnValue = url;
                window.close();
            }
            else {
                var thumbClientId = '<%= Request.QueryString["ThumbClientId"] %>';
                  if (thumbClientId == '') {
                      var obj = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
            var newurl = '下载地址' + (obj.length + 1) + '|' + url;
            obj.options[obj.length] = new Option(newurl, newurl);
            ChangeHiddenFieldValue();
        }
        else {
            var obj = opener.document.getElementById(thumbClientId);
            obj.value = url;
        }
        window.close();
    }
}

function ChangeHiddenFieldValue() {
    var obj = opener.document.getElementById('<%= Request.QueryString["HiddenFieldId"] %>');
    var softUrls = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
    var value = "";
    for (i = 0; i < softUrls.length; i++) {
        if (value != "") {
            value = value + "$$$";
        }
        value = value + softUrls.options[i].value;
    }
    obj.value = value;
}
        HideColumn("3,4");

        $("#upfile_btn").click(ZL_Webup.ShowFileUP);
        function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
        function reloadPage() { 
            location.replace(location.href);
        }
    </script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateManage.aspx.cs" Inherits="Manage_I_Content_TemplateManage" MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title><%=Resources.L.模板管理 %></title>
<style>
.temp_func li {float: left;}
.temp_func li {position: relative;min-height: 50px;}
.temp_func li div {display: none;position: absolute;top: 40px;}
.diag_width {width:400px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <link href="/Plugins/JqueryUI/LightBox/css/lightbox.css" rel="stylesheet" media="screen" />
    <table width="100%" class="table table-bordered">
        <tr>
            <td align="left"><asp:Label ID="lblDir" runat="server" ></asp:Label></td>
            <td align="right">
                <asp:Literal ID="LitParentDirLink" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="table table-striped table-bordered table-hover" align="center">
        <tr class="gridtitle" align="center" style="height: 25px;">
            <td width="5%"><%=Resources.L.操作 %></td>
            <td width="40%"><%=Resources.L.名称 %></td>
            <td width="10%"><%=Resources.L.大小 %></td>
            <td width="10%"><%=Resources.L.类型 %></td>
            <td width="20%"><%=Resources.L.修改时间 %></td>
        </tr>
        <asp:Repeater ID="repFile" runat="server" OnItemCommand="repFileReName_ItemCommand">
            <ItemTemplate>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'"  ondblclick="dd('<%# Eval("Name").ToString()%>')"  id="chk_<%# Eval("Name").ToString()%>" onmouseout="this.className='tdbg'">
                    <td align="center">
                    <div class="option_area dropdown" >
                    <a class="option_style" href="javascript:;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars"></i><%=Resources.L.操作 %><span class="caret"></span></a>
                    <ul class="dropdown-menu" role="menu">
                    <li><asp:LinkButton ID="LinkButton3" CssClass="option_style" runat="server" CommandArgument='<%# Eval("Name").ToString()%>' CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "":"DownFiles" %>' Visible='<%#Eval("content_type").ToString() == "config" ? false:true %>'><i class="fa  fa-download"></i><%=Resources.L.下载 %></asp:LinkButton></li>
                    <li><asp:LinkButton ID="LinkButton1" CssClass="option_style" runat="server" CommandArgument='<%# Eval("Name").ToString()%>' CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>' OnClientClick="return confirm('你确认要删除该文件夹或文件吗？')"><i class="fa fa-trash-o"></i><%=Resources.L.删除 %></asp:LinkButton></li>
                    <li><asp:LinkButton ID="LinkButton2" CssClass="option_style" runat="server" CommandArgument='<%# Eval("Name").ToString()%>' CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "CopyDir":"CopyFiles" %>' Enabled='<%# System.Convert.ToInt32(Eval("type")) == 1 ? false:true %>'><i class="fa fa-copy"></i><%=Resources.L.复制 %></asp:LinkButton></li>
                    </ul>
                    </div>                      
                    </td>
                    <td align="left">
                        <i style='color:#4586BD;' class='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "fa fa-folder" :"fa fa-file-text" %>'></i>
                        <a id="hre_<%# Eval("Name").ToString()%>"  <%#isimg(Eval("content_type").ToString().Trim())?" class=\"lightbox\" href=\"/Template/" + CurDir +"/"+ Eval("Name").ToString(): "href=\""+(System.Convert.ToInt32(Eval("type")) == 1 ?  "TemplateManage.aspx?setTemplate="+Server.UrlEncode(TemplateDir)+"&Dir=" + Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()):"TemplateEdit.aspx?setTemplate="+Server.UrlEncode(TemplateDir)+"&filepath="+ Server.UrlEncode(CurDir +"/"+ Eval("Name").ToString())) %>">
                        <%# Eval("Name") %></a>
                    </td>
                    <td align="center">
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "" : GetSize(Eval("size").ToString())%>
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
    <div class="clearfix"></div>
    <a href="javascript:;" class="btn btn-info" onclick="ShowDirDiag()"><%=Resources.L.创建目录 %></a>
    <a href="javascript:;" class="btn btn-info" onclick="SelTempFile()"><%=Resources.L.上传模板 %></a>
    <asp:Button ID="BackGrup" class="btn btn-info" runat="server" Text="<%$Resources:L,备份当前方案 %>" OnClick="BackGrup_Click" OnClientClick="if(!this.disabled) return confirm('是否创建备份？(提示：备份同名文件会覆盖！)');" />
    <a runat="server" id="ViewEdit_A" class="btn btn-info" target="_blank"><i class="fa fa-edit" style="margin-right:5px;"></i><%=Resources.L.可视化编辑 %></a>
<div id="create_dir" style="display:none;">  
  <asp:TextBox ID="txtForderName" class="form-control text_md" runat="server"></asp:TextBox>
  <asp:Button ID="btnCreateFolder" class="btn btn-primary" runat="server" Text="<%$Resources:L,创建 %>" OnClick="btnCreateFolder_Click" />
</div>
    <ZL:FileUpload ID="fileUploadTemplate" style="display:none;" onchange="$('#btnTemplateUpLoad').click();" runat="server" />
<%--  <asp:FileUpload ID="fileUploadTemplate" style="display:none;" onchange="$('#btnTemplateUpLoad').click();" runat="server" />--%>
  <asp:Button ID="btnTemplateUpLoad" style="display:none;" class="btn btn-primary" runat="server" Text="<%$Resources:L,上传 %>" OnClientClick="return confirm('即将覆盖同名模板，是否继续？');" OnClick="btnTemplateUpLoad_Click" /> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
        <script src="/Plugins/JqueryUI/LightBox/jquery.lightbox.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            base_url = document.location.href.substring(0, document.location.href.indexOf('index.html'), 0);
            $(".lightbox").lightbox({
                fitToScreen: true,
                imageClickClose: false
            });
        });
        function dd(id) {
            document.getElementById("hre_" + id + "").click();
        }

    </script>
	<script type="text/javascript">
	    $().ready(function (e) {
	        $(".temp_func li").mouseenter(function (e) {
	            $(this).find("div").show();
	        }).mouseleave(function (e) {
	            $(this).find("div").hide();
	        });;
	    });
	    var diag = new ZL_Dialog();
	    var diag2 = new ZL_Dialog();
	    function ShowDirDiag() {
	        diag.title = "<%=Resources.L.创建目录 %>";
	        diag.width = "diag_width";
	        diag.content = "create_dir";
	        diag.ShowModal();
	    }
	    function SelTempFile() {
	        $("#fileUploadTemplate").click();
	    }
    </script>
</asp:Content>

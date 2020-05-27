<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="User_BossInfo_index" ClientIDMode="Static" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>内容管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Pages/Default.aspx">企业黄页</a></li>
        <li class="active">我的加盟商</li>
    </ol>
    <div class="s_body">
        <div class="s_bleft">
            <iframe frameborder="0" width="100%" height="500px" src="NodeTree.aspx" scrolling="auto"
                id="contentleft" name="main_left"></iframe>
        </div>
        <div class="s_bright">
            <iframe id="contentright" width="580px" height="500px" onload="dyniframesize()" ondatabinding="dyniframesize()"
                src="BossContent.aspx" frameborder="0" name="main_right"></iframe>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent" >
    <script language="javascript" type="text/javascript">
        //** iframe自动适应页面 **//
        //输入你希望根据页面高度自动调整高度的iframe的名称的列表
        //用逗号把每个iframe的ID分隔. 例如: ["myframe1", "myframe2"]，可以只有一个窗体，则不用逗号。
        //定义iframe的ID
        //alert("begin");
        var iframeids = ["contentright"];
        //如果用户的浏览器不支持iframe是否将iframe隐藏 yes 表示隐藏，no表示不隐藏
        var iframehide = "yes";
        function dyniframesize() {
            for (i = 0; i < iframeids.length; i++) {
                if (document.getElementById) {
                    var dyniframe = document.getElementById(iframeids[i]);
                    if (dyniframe && !window.opera) {
                        dyniframe.style.display = "block";
                        if (dyniframe.contentDocument && dyniframe.contentDocument.body.offsetHeight) //如果用户的浏览器是NetScape
                        {
                            dyniframe.style.height = (dyniframe.contentDocument.body.offsetHeight + 5) + "px";
                        }
                        else if (dyniframe.Document && dyniframe.Document.body) //如果用户的浏览器是IE
                        {
                            if (dyniframe.Document.body.scrollHeight) {
                                dyniframe.style.height = dyniframe.Document.body.scrollHeight + 5;
                            }
                        }
                    }
                }
                //根据设定的参数来处理不支持iframe的浏览器的显示问题
                if ((document.all || document.getElementById) && iframehide == "no") {
                    var tempobj = document.all ? document.all[iframeids[i]] : document.getElementById(iframeids[i]);
                    tempobj.style.display = "block";
                }
            }
        }
</script>
</asp:Content>
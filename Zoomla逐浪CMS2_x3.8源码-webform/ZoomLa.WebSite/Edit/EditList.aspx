<%@ Page Language="C#" EnableViewStateMac="false" AutoEventWireup="true" CodeFile="EditList.aspx.cs" Inherits="Edit_EditList" %>
<!DOCTYPE html>
<html>
<head>
<title>作品列表</title>
<style type="text/css">
.Btcen{text-align:center;}
</style>
<!--[if IE]>
<script src="/js/html5.js"></script>
<![endif]-->
<script type="text/javascript">
    function alertWin(title, msg, w, h) {
        var titleheight = "22px"; // 提示窗口标题高度 
        var bordercolor = "rgb(154,199,240)"; // 提示窗口的边框颜色 
        var titlecolor = "#FFFFFF"; // 提示窗口的标题颜色 
        var titlebgcolor = "rgb(154,199,240)"; // 提示窗口的标题背景色 
        var bgcolor = "#FFFFFF"; // 提示内容的背景色 

        var iWidth = document.documentElement.clientWidth;
        var iHeight = document.documentElement.clientHeight;
        var bgObj = document.createElement("div");
        bgObj.style.cssText = "position:absolute;left:0px;top:0px;width:" + iWidth + "px;height:" + Math.max(document.body.clientHeight, iHeight) + "px;filter:Alpha(Opacity=30);opacity:0.3;background-color:#000000;z-index:101;";
        document.body.appendChild(bgObj);

        var msgObj = document.createElement("div");
        msgObj.style.cssText = "position:absolute;font:11px '宋体';top:" + (iHeight - h) / 2 + "px;left:" + (iWidth - w) / 2 + "px;width:" + w + "px;height:" + h + "px;text-align:center;border:1px solid " + bordercolor + ";background-color:" + bgcolor + ";padding:1px;line-height:22px;z-index:102;";
        document.body.appendChild(msgObj);

        var table = document.createElement("table"); //www.divcss5.com divcss5
        msgObj.appendChild(table);
        table.style.cssText = "margin:0px;border:0px;padding:0px; width:100% ;";
        table.cellSpacing = 0;
        var tr = table.insertRow(-1);
        var titleBar = tr.insertCell(-1);
        titleBar.style.cssText = "width:100%;height:" + titleheight + "px;text-align:left;padding:3px;margin:0px;font:bold 13px '宋体';color:" + titlecolor + ";border:1px solid " + bordercolor + ";cursor:move;background-color:" + titlebgcolor;
        titleBar.style.paddingLeft = "10px";
        titleBar.innerHTML = title;
        var moveX = 0;
        var moveY = 0;
        var moveTop = 0;
        var moveLeft = 0;
        var moveable = false;
        var docMouseMoveEvent = document.onmousemove;
        var docMouseUpEvent = document.onmouseup;
        titleBar.onmousedown = function () {
            var evt = getEvent();
            moveable = true;
            moveX = evt.clientX;
            moveY = evt.clientY;
            moveTop = parseInt(msgObj.style.top);
            moveLeft = parseInt(msgObj.style.left);

            document.onmousemove = function () {
                if (moveable) {
                    var evt = getEvent();
                    var x = moveLeft + evt.clientX - moveX; //www.divcss5.com divcss5
                    var y = moveTop + evt.clientY - moveY;
                    if (x > 0 && (x + w < iWidth) && y > 0 && (y + h < iHeight)) {
                        msgObj.style.left = x + "px";
                        msgObj.style.top = y + "px";
                    }
                }
            };
            document.onmouseup = function () {
                if (moveable) {
                    document.onmousemove = docMouseMoveEvent; //www.divcss5.com divcss5
                    document.onmouseup = docMouseUpEvent;
                    moveable = false;
                    moveX = 0;
                    moveY = 0;
                    moveTop = 0;
                    moveLeft = 0;
                }
            };
        }

        var closeBtn = tr.insertCell(-1);
        closeBtn.style.cssText = "cursor:pointer; padding:2px;background-color:" + titlebgcolor;
        closeBtn.innerHTML = "<span style='font-size:15pt; color:" + titlecolor + ";'>×</span>";
        closeBtn.onclick = function () {
            document.body.removeChild(bgObj);
            document.body.removeChild(msgObj);
        }
        var msgBox = table.insertRow(-1).insertCell(-1);
        msgBox.style.width = "100%";
        msgBox.style.height = h-25 + "px";
        msgBox.colSpan = 2;
        msgBox.innerHTML = "<iframe  src=" + msg + " ' style='width:97%; height:97%; overflow:auto'></iframe>"

        // 获得事件Event对象，用于兼容IE和FireFox 
        function getEvent() {
            return window.event || arguments.callee.caller.arguments[0];
        }
    }
</script>
    <style type="text/css">

table{font-size:12px;}
*{ margin:0px; padding:0px;}
*{margin:0px; padding:0px}
.title, .gridtitle, .spacingtitle, th{	background: #CBE6FC url('../App_Themes/AdminDefaultTheme/Images/title.gif') repeat-x 50% top;	/*line-height: 120%;*/	
padding:2px;	color: #0E529D;	font-weight: bold;
        }
th{	font-weight: normal;}
td{	line-height: 150%; font-size:12px;}
input[type=checkbox]{	vertical-align: middle;}
a:link, a:visited, a:active{color: #000000;}
a{text-decoration: none;}
a{text-decoration: none; color:#70BFFA;}
a{text-decoration:none; color:#003399;}	
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
     |  <a style="font-size:12px;font-weight:bold;color:Gray" href="Default.aspx?ID=0&DocType=doc&DocTitle=">添加Word</a>
    <table runat="server" id="tt" class="title" style=" text-align:center; width:100%; padding-bottom:0px; font-weight:normal">
            <tr>
            <%--<td  style="height:25px;">选择</td>--%>
            <td  style="width:6%;">ID</td>
            <td  style="width:45%;">标题</td>
            <td >文章类型</td>
            <td >修改时间</td>
            <td>操作</td>
            </tr>
            </table>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
                <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID">
                <HeaderStyle Width="6%" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField  DataNavigateUrlFields="id,title,Type" 
                DataNavigateUrlFormatString="default.aspx?ID={0}&amp;DocTitle={1}&amp;DocType={2}&amp;uptp=Iser" 
                HeaderText="标题" DataTextField="title" >
                <ItemStyle CssClass="Btcen" />
                </asp:HyperLinkField>
                <asp:TemplateField HeaderText="文章类型">
                <ItemTemplate>
                <%# Eval("Type","{0}") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
			
                <asp:TemplateField HeaderText="修改时间">
                <ItemTemplate>
                <%# Eval("CreateTime","{0}")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="id,title,Type" 
                DataNavigateUrlFormatString="default.aspx?ID={0}&amp;DocTitle={1}&amp;DocType={2}&amp;uptp=Iser" 
                Text="[修改]" HeaderText="操作" />
                <asp:TemplateField>
                <ItemTemplate>
                <asp:LinkButton ID="TgaoLink" runat="server"  CommandName="Tgao" CommandArgument='<%# Eval("ID") %>'
                >[投稿]</asp:LinkButton>            
                <asp:LinkButton ID="LinkButton3" runat="server"  CommandName="Del" CommandArgument='<%# Eval("ID") %>'
                OnClientClick="javascript:return confirm('你确定将该数据删除吗？');">[删除]</asp:LinkButton>                                
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center"  />
        </ZL:ExGridView>
        
			<%--<asp:TemplateField HeaderStyle-Height="25px" HeaderText="选择">
				<ItemTemplate>
					<asp:CheckBox ID="chkSel" runat="server"  />
				</ItemTemplate>
                <HeaderStyle Height="25px"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center" />
			</asp:TemplateField>--%>
			
			<%--<asp:TemplateField HeaderText="标题">
				<HeaderStyle Width="45%" />
				<ItemTemplate>
						<center> <%# Eval("Title", "{0}")%></center>
				</ItemTemplate>
			</asp:TemplateField>--%>    
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteFileManage.aspx.cs" Inherits="manage_IISManage_IISFileManage" MasterPageFile="~/Manage/I/Default.master" Title="文件浏览"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>文件详情</title> 
<link type="text/css" href="/Plugins/JqueryUI/ProgressBar/css/jquery.ui.theme.css" rel="stylesheet" />
<link type="text/css" href="/Plugins/JqueryUI/ProgressBar/css/jquery.ui.progressbar.css" rel="stylesheet" />
<script type="text/javascript" src="/JS/jquery.contextmenu.r2.js"></script>
<script type="text/javascript" src="/Plugins/JqueryUI/ProgressBar/js/jquery-ui.custom.js"></script>
<script type="text/javascript" src="/JS/Common.js"></script>

<script>
	$(function () {
		var progressbar = $("#progressbar"),
			progressLabel = $(".progress-label");

		progressbar.progressbar({//绑定进度条事件
			value: false,
			change: function () {
				progressLabel.text(progressbar.progressbar("value") + "%");
			},
			complete: function () {
				progressLabel.text("完成!");
			}
		});

		function progress() {
			var val = progressbar.progressbar("value") || 0;
			progressbar.progressbar("value", val + 1);
		}
	});

	function increase(value) {
		var progressbar = $("#progressbar")
		var val = progressbar.progressbar("value") || 0;
		progressbar.progressbar("value", value);
		//progressbar.progressbar("value", val + 1);
	}

   //实时获取进度,后台用异步或线程池
	function ajaxPost(obj, a, val) {//this,actionName,要上传的值,回调方法,是否弹出回复窗(true显示,false不显示)
		$.ajax({
			type: "Post",
			url: "SiteFileManage.aspx",
			data: { action: a, value: val },
			success: function (data) {
				if (a=="getProgress"&&data == "100")
				{
					increase(100);
					clearInterval(interval);
					if(confirm("下载完成,现在就要开始解压缩吗!"))
					{
						$("#<%=unzipbtn.ClientID%>").trigger('click');
					} else {
					    $("#progressDiv").hide();
					}
				}
				else if(a=="getUnZipProg"&&data == "100")
				{
					increase(100);
					clearInterval(interval);
					alert('解压完成，点击确定开始安装.如果弹出窗口被阻止,你也可以点击(开始安装)按钮,开始安装.');
					Setup();
				}
				else
				{
					increase(parseInt(data));//必须要转换次，否则返回的字符串变量无用为0%
				}
			},
			error: function (data) {
			}
		});
	}
	var interval; 
	function beginCheck(request) { interval = setInterval(function () {ajaxPost(this, request, '') }, 1000); }
	function beginDown()
	{
		if (confirm("点击确定开始下载"))
		{
			postToCS(this, 'beginDown', "<%=Server.HtmlEncode(Request.QueryString["siteName"])+":"+Server.HtmlEncode(Request.QueryString["index"])%>", '');
			beginCheck('getProgress');
			$('#progressDiv').show();
		}
	}

    function Setup()
    {
        window.open("<%=url%>");
    }
function modalDialog(url, name, width, height) {
	if (width == undefined) {
		width = 600;
	}
	if (height == undefined) {
		height = 500;
	}

	if (window.showModalDialog) {
		window.showModalDialog(url, name, 'dialogWidth=' + (width) + 'px; dialogHeight=' + (height + 5) + 'px; status=off');
	}
	else {
		x = (window.screen.width - width) / 2;
		y = (window.screen.height - height) / 2;

		window.open(url, name, 'height=' + height + ', width=' + width + ', left=' + x + ', top=' + y + ', toolbar=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, modal=yes');
	}
}
$().ready(function () {
    url = "SiteDetail.aspx?SiteName=<%=Server.UrlEncode(siteName)%>";

    if (getParam("remote") == "true") {
        url += "&remote=true"
    }
    $("#ahref1").attr("href", url);

});
</script>
<style>
  #jqContextMenu ul{ width:120px;}
  .progress-label{font-size:20px; text-align:center; position:absolute; top:202px; left:49%;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="width:100%;position:fixed; height:100%;display:none; background-color:rgba(0,0,0,0.5)" id="progressDiv">
        <div id="progressbar" style="width:50%;margin-left:25%; height:35px; margin-top:200px;" ></div><div class="progress-label" >开始下载</div>
        </div>
    <div id="BreadDiv" class="container-fluid mysite">
        <div class="row">
            <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
                <li><a href="../Main.aspx">工作台</a></li>
                <li><a href="Default.aspx">站群中心</a></li>
                <li class="active">文件详情<a id="ahref1" href="#">[查看网站详情]</a></li>
            </ol>
        </div>
    </div>
       <div id="site_main">
        
        <div>
            <div class="top_opbar">
                <a href="<%="SiteFileManage.aspx?siteName="+Server.UrlEncode(Request.QueryString["siteName"])+"&index="+Server.HtmlEncode(Request.QueryString["index"]) %>"><i class="fa fa-home" style="font-size:16px; margin-right:2px;"></i>返回根目录</a>
                <a href="javascript:history.go(-1);"><i class="fa fa-arrow-circle-left" style="font-size:16px; margin-right:2px;"></i>返回上一目录</a>
                <asp:TextBox runat="server" ID="searchText" onkeypress="CheckInfo(this)"  OnTextChanged="searchText_TextChanged" CssClass="form-control text_md"></asp:TextBox>
                <select name="searchOption" class="form-control text_s">
                    <option value="1">当前目录</option>
                </select>
                <asp:Button runat="server" ID="ZipSite" OnClick="ZipSite_Click" Text="全站打包下载" UseSubmitBehavior="false" CssClass="btn btn-primary" OnClientClick="return confirm('点击确定开始压缩全站');"/>
                <input type="button" id="downBtn" value="从逐浪官方下载源码"  class="btn btn-primary" onclick="beginDown();" />
                <asp:Button runat="server" id="unzipbtn" text="解压源码并安装" onclick="UnZipBtn_Click"  class="btn btn-primary"/> 
                <input type="button" id="beginSetup" value="开始安装"  class="btn btn-primary" onclick="Setup();"/>
            </div>
            <script type="text/javascript">
                function CheckInfo(obj) {
                    if (event.keyCode == 13) {
                        form1.submit();
                    }
                }
                $(function () {
                    var s = "请输入文件名,回车搜索";
                    $("#<%=searchText.ClientID%>").val(s).css('color', '#666')
                        .focus(function () { if (this.value == s) { this.value = ''; this.style.color = 'black'; } })
                    .blur(function () { if (this.value == '') { this.value = s; this.style.color = '#666'; } });
                });
            </script>
            <!--不允许全路径,避免回朔漏洞和暴露物理路径,name用于ajax更新-->
            <table class="table table-striped table-bordered table-hover" id="fileList" ellpadding="2" cellspacing="1">
                <tr>
                    <th><input type="checkbox" id="chkAll" /> 名称</th><th>类型</th><th>修改日期</th><th>大小</th>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr class="tdbg"  style="height:30px;text-align:center;" onmouseover="this.className='tdbgmouseover';" onmouseout="this.className='tdbg';" >
                            <td style='width:25%;text-align:left;' name="name">
                            <input type='checkbox' name='chk' onclick="this.value=$(this).parent().parent().find('[name=path]').val()">
                             <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<i class='fa fa-folder'></i><a href='SiteFileManage.aspx?siteName="
                             +siteName+"&index="+index+"&PPath=" +PPath+Eval("PartPath")+"' title=点击浏览 name='ta'>"+Eval("name")+"</a>" :  
                             GetShowExtension(Eval("content_type").ToString())+"<a href='javascript:;' style='cursor:default;'>"+Eval("name")+"</a>"%>
                             <%# "</td><td style='text-align:left;width:20%'>"+Eval("ExType")+"</td><td style='text-align:left;'>"
                             +DataBinder.Eval(Container.DataItem,"lastWriteTime","{0:yyyy年M月d日}")
                             +"</span></td><td style='text-align:left;'>"+Eval("ExSize")%>
                            <input type="hidden" value="<%# siteName+":"+index+":"+(PPath+Eval("PartPath")).Replace("\\",@"\\") %>" name="path"/>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Button runat="server" ID="Btn1" OnClick="Btn1_Click" Text="批量删除" class="btn btn-primary" UseSubmitBehavior="false"/>
      <div class="contextMenu" id="myMenu1" style="display:none;">
      <ul>
        <li id="open"><i class="fa fa-folder-open"></i> 打开文件夹</li>
        <li id="refresh" onclick="location=location;"><i class="fa fa-refresh"></i> 刷新页面</li>
        <li id="rename"><i class="fa fa-reply-all"></i> 重命名</li>
        <li id="delete"><i class="fa fa-trash"></i> 删除</li>
      </ul>
    </div>
            <script type="text/javascript">
                var oldstr;
                $(function () {
                    $("#chkAll").click(function () {
                        selectAll(this, "chk");
                    });
                });

                function selectAll(obj, name) {
                    var allInput = document.getElementsByName(name);
                    var loopTime = allInput.length;
                    for (i = 0; i < loopTime; i++) {
                        if (allInput[i].type == "checkbox") {
                            allInput[i].checked = obj.checked;
                        }
                    }
                }

                function postToCS(obj, a, name,newName) {
                    $.ajax({
                        type: "Post",
                        url: "SiteFileManage.aspx",
                        data: { action: a, fullPath: name },
                        success: function (data)
                        {
                            if (data == 1)
                            {   
                                if (a == "del")//delete success
                                    $(obj).parent().parent().remove();
                                else if (a == "rename")//Restore and display the new value;
                                {
                                    $(obj).parent().html(oldstr).find("a").text(newName);
                                    location = location;//以后改为上方这个,需要处理，及时理新问题,否则更新过后的文件夹无法打开,也无法再次重命名。
                                }

                            }
                            else if (a == "beginDown") { }
                            else if(a=="beginUnZip"){}
                            else alert(data);
                        },
                        error: function (data) { alert("失败:"+data); }
                    });
                }

                $("#fileList tr").contextMenu('myMenu1',
                    {
                        bindings:
                        {
                            'open': function (t) {
                                $ta = $(t).find("[name=ta]");
                                if($ta.attr("href")!=undefined)
                                    location = $ta.attr("href");
                            },
                            'rename': function (t) {
                                //------------------------关闭其他重命名
                                $cancelA = $(t).siblings().find("[name=cancelA]");
                                $cancelA.trigger("click");
                                //------------------------开始展命名
                                $path = $(t).find("[name=path]");
                                newstr = "<input type='text' name='renameT' onkeypress='if(event.keyCode==13){renameF(this);}'/>" +
                                    "<a href='javascript:;' onclick='renameF(this);' ><i class='fa  fa-check-square-o'></i></a> " +
                                    "<a href='javascript:;' style='margin-left:2px;' onclick='cancelF(this);' name='cancelA'><i class='fa fa-close'></i></a>";
                                $td = $(t).find("[name=name]");//name为目标 TD
                                strText = $.trim($td.text());
                                oldstr = $td.html();
                                $td.html(newstr).find("[name=renameT]").focus().val(strText);
                            },
                            'save': function (t) {
                                alert('Trigger was ' + t.id + '\nAction was Save');
                            },
                            'delete': function (t) {
                                if (confirm("你确定要删除吗!"))
                                {
                                    $path = $(t).find("[name=path]");
                                    postToCS($path, 'del', $path.val());
                                }
                            }
                        }

                    });

                function renameF(obj)//将名字与路径信息提交到后台.
                {
                    $path = $(obj).parent().parent().find("[name=path]");
                    $newName = $(obj).parent().find("[name=renameT]");
                    //因为其会自动将\\转义,所以需要如下处理
                    postToCS(obj, "rename", $path.val() + ":<%=PPath.Replace("\\",@"\\")%>\\" + $newName.val(), $newName.val());//最后的一个值只用于jquery中显示
                }
                function cancelF(obj)//取消此次编辑
                {
                    $td = $(obj).parent();
                    $td.html(oldstr);
                }
            </script>
        </div>
        </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="office.aspx.cs" Inherits="MIS_OA_Flow_office" MasterPageFile="~/User/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>文档管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <style type="text/css">
        .exdiv { display: none; }
        .divline {margin-top:5px;margin-bottom:5px;border-bottom:1px solid #ddd;}
        .tdimg {width:50px;height:50px;border:1px solid #ddd; padding:2px;}
        .tdimg200 {max-width:200px;max-height:200px;border:1px solid #ddd; padding:2px;}
        .item_img_div {display:none;position:absolute;}
		#signpwd_t { margin-right:5px; max-width:150px;}
		#Head_DP,#Sign_DP { margin-right:5px;}
		#signcheck_div .btn { margin-right:5px;}
    </style>
    <select name="doctype" size="1" id="doctype" style="display: none;">
        <option value="doc">Word</option>
        <option value="xls">Excel</option>
        <option value="wps">wps</option>
    </select>
    <div style="text-align:center;">
        <%--<input type="button" class="btn btn-primary" value="签章验证" onclick="$('#Signed_Btn').trigger('click');" />--%>
        <span id="remind_sp" class="r_green_mid"></span>
        <input runat="server" id="save_btn" type="button" class="btn btn-primary" value="保存公文" onclick=" if (confirm('确定要保存吗')) { SaveToServer(); }" />
        <input runat="server" id="traceless_btn" type="button" class="btn btn-primary" value="无痕编辑" onclick="document.all.WebOffice1.SetTrackRevisions(0);" />
        <input runat="server" id="hastrace_btn" type="button" class="btn btn-primary" value="有痕编辑" onclick="document.all.WebOffice1.SetTrackRevisions(1);" />
        <input runat="server" id="hidetrace_btn" type="button" class="btn btn-link" value="隐藏痕迹" onclick="document.all.WebOffice1.ShowRevisions(0); " />
        <input runat="server" id="showtrace_btn" type="button" class="btn btn-link" value="显示痕迹" onclick="document.all.WebOffice1.ShowRevisions(1); " />
    </div>
    <div class="divline"></div>
    
    <table class="table table-bordered table-striped">
        <tr>
            <td>
                <div>
                    <asp:DropDownList runat="server" ID="Head_DP" DataValueField="WordPath" DataTextField="ModelName" CssClass="form-control pull-left text_md" onchange="AddRedHead();"></asp:DropDownList>
                    <asp:DropDownList runat="server" ID="Sign_DP" DataTextField="SignName" DataValueField="ID" CssClass="form-control pull-left text_md" onchange="AddSign();" Enabled="false"></asp:DropDownList>
                    <div class="exdiv pull-left" id="signcheck_div">
                        <input type="text" class="form-control pull-left" id="signpwd_t" placeholder="请输入签章密码" />
                        <input type="button" class="btn btn-primary pull-left" value="确认" onclick="SignPwdCheck();" />
                        <input type="button" class="btn btn-default pull-left" value="取消" onclick="$('#signcheck_div').hide();" />
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="divline"></div>
                
                <div class="panel panel-primary " id="signedlist_div" hidden>
                    <div class="panel-heading"><span class="fa-user-secret"></span><span class="margin_l5">已有签章</span></div>
                    <div class="panel-body">
                        <asp:UpdatePanel runat="server" ID="SignedList_Panel">
                            <ContentTemplate>
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <td>签章名</td><td>签章图</td><td>签章人</td><td>签章日期</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="Signed_RPT">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("SignName") %></td>
                                                <td>
                                                    <img src="<%#Eval("VPath") %>" class="tdimg" />
                                                    <div class="item_img_div"><img class="tdimg200" src="<%#Eval("VPath") %>" /></div>
                                                </td>
                                                <td><%#Eval("CUName") %></td>
                                                <td><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <asp:Button runat="server" ID="Signed_Btn" OnClick="Signed_Btn_Click" style="display:none;" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div><script src="LoadWebOffice.js"></script></div>
            </td>
        </tr>
   </table>
    <asp:HiddenField runat="server" ID="Protect_Hid" Value="1" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" FOR="WebOffice1" EVENT="NotifyCtrlReady">
<!--
    //WebOffice1_NotifyCtrlReady()			// 在装载完Weboffice(执行<object>...</object>)控件后执行 "WebOffice1_NotifyCtrlReady"方法
    //-->
</script>
<script type="text/javascript">
<!--
    // ---------------------=== 控件初始化WebOffice方法 ===---------------------- //
    function WebOffice1_NotifyCtrlReady() {
        document.all.WebOffice1.LoadOriginalFile("", "doc");
    }
    // ---------------------=== 新建文档 ===---------------------- //
    function newDoc() {
        var doctype = document.all.doctype.value;
        document.all.WebOffice1.LoadOriginalFile("", doctype);
    }
    // ---------------------=== 显示打印对话框 ===---------------------- //
    function showPrintDialog() {
        document.all.WebOffice1.PrintDoc(1);
    }
    // ---------------------=== 直接打印 ===---------------------- //
    function zhiPrint() {
        document.all.WebOffice1.PrintDoc(0);
    }
    // ---------------------== 关闭页面时调用此函数，关闭文件 ==---------------------- //
    function window_onunload() {
        document.all.WebOffice1.Close();
    }

    // ---------------------------== 解除文档保护 ==---------------------------------- //
    function UnProtect() {
        document.all.WebOffice1.ProtectDoc(0, 1, document.all.docPwd.value);
    }

    // ---------------------------== 设置文档保护 ==---------------------------------- //
    function ProtectFull() {
        //document.all.docPwd.value = "temp";
        document.all.WebOffice1.ProtectDoc(1, 1, "Temp");
    }
    // ---------------------------== 禁止打印 ==---------------------------------- //
    function notPrint() {
        document.all.WebOffice1.SetSecurity(0x01);
    }
    // ---------------------------== 恢复允许打印 ==---------------------------------- //
    function okPrint() {
        document.all.WebOffice1.SetSecurity(0x01 + 0x8000);

    }
    // ---------------------------== 禁止保存 ==---------------------------------- //
    function notSave() {
        document.all.WebOffice1.SetSecurity(0x02);

    }
    // ---------------------------== 恢复允许保存 ==---------------------------------- //
    function okSave() {
        document.all.WebOffice1.SetSecurity(0x02 + 0x8000);

    }
    // ---------------------------== 禁止复制 ==---------------------------------- //
    function notCopy() {
        document.all.WebOffice1.SetSecurity(0x04);
    }
    // ---------------------------== 恢复允许复制 ==---------------------------------- //
    function okCopy() {
        document.all.WebOffice1.SetSecurity(0x04 + 0x8000);

    }
    // ---------------------------== 禁止拖动 ==---------------------------------- //
    function notDrag() {
        document.all.WebOffice1.SetSecurity(0x08);
    }
    // ---------------------------== 恢复拖动 ==---------------------------------- //
    function okDrag() {
        document.all.WebOffice1.SetSecurity(0x08 + 0x8000);

    }
    // -----------------------------== 修订文档 ==------------------------------------ //
    function ProtectRevision() {
        document.all.WebOffice1.SetTrackRevisions(1)
    }

    // -----------------------------== 隐藏修订 ==------------------------------------ //
    function UnShowRevisions() {
        document.all.WebOffice1.ShowRevisions(0);
    }

    // --------------------------== 显示当前修订 ==---------------------------------- //
    function ShowRevisions() {
        document.all.WebOffice1.ShowRevisions(1);

    }

    // -------------------------== 接受当前所有修订 ==------------------------------- //
    function AcceptAllRevisions() {
        document.all.WebOffice1.SetTrackRevisions(4);
    }

    // ---------------------------== 设置当前操作用户 ==------------------------------- //
    function SetUserName() {
        if (document.all.UserName.value == "") {
            alert("用户名不可为空")
            document.all.UserName.focus();
            return false;
        }
        document.all.WebOffice1.SetCurrUserName(document.all.UserName.value);
    }
    // -------------------------=== 设置书签 ===------------------------------ //
    function addBookmark() {
        document.all.WebOffice1.SetFieldValue("mark_1", "北京信息技术有限公司", "::ADDMARK::");
    }

    // -------------------------=== 设置书签插入图片 ===------------------------------ //
    function addImage() {
        document.all.WebOffice1.SetFieldValue("mark_1", "", "::ADDMARK::");			// 添加书签
        //document.all.WebOffice1.SetFieldValue("mark_1", "E:\\Code\\ZoomlaCMS2\\ZoomLa.WebSite\\test\\image\\sign.png", "::JPG::");
        document.all.WebOffice1.SetFieldValue("mark_1", "http://win100:86/test/image/sign.png", "::JPG::");
    }


    // -----------------------------== 返回首页 ==------------------------------------ //
    function return_onclick() {
        document.all.WebOffice1.Close();
        window.location.href = "index.jsp"
    }
    // 打开本地文件
    function docOpen() {
        document.all.WebOffice1.LoadOriginalFile("open", "doc");
    }
    // -----------------------------== 保存文档 ==------------------------------------ //
    function newSave() {
        document.all.WebOffice1.Save();
    }
    // -----------------------------== 另存为文档 ==------------------------------------ //
    function SaveAsTo() {
        document.all.WebOffice1.ShowDialog(84);
    }
    // -----------------------------== 隐藏菜单 ==------------------------------------ //
    function notMenu() {
        document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 8);
    }
    // -----------------------------== 显示菜单 ==------------------------------------ //
    function okMenu() {
        document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 11);
    }
    // -----------------------------== 隐藏常用工具栏 ==------------------------------------ //
    function notOfter() {
        document.all.WebOffice1.SetToolBarButton2("Standard", 1, 8);
    }
    // -----------------------------== 显示常用工具栏 ==------------------------------------ //
    function okOfter() {
        document.all.WebOffice1.SetToolBarButton2("Standard", 1, 11);
    }
    // -----------------------------== 隐藏格式工具栏 ==------------------------------------ //
    function notFormat() {
        document.all.WebOffice1.SetToolBarButton2("Formatting", 1, 8);
    }
    // -----------------------------== 显示格式工具栏 ==------------------------------------ //
    function okFormat() {
        document.all.WebOffice1.SetToolBarButton2("Formatting", 1, 11);
    }
</script>
<script>
    var config = { fname: "<%=HttpUtility.UrlEncode(FName)%>", AppID: "<%=AppID%>" };
    $(function () {
        $(".tdimg").mouseenter(function () {
            $(this).siblings(".item_img_div").show();
        }).mouseout(function () {
            $(".item_img_div").hide();
        });
        document.all.WebOffice1.HideMenuItem(0x01 + 0x02 + 0x04 + 0x10 + 0x20 + 0x2000);
    })
    function AddSign() {
        if ($("#Sign_DP").val() != "") {
            //ShowZLDiag("请输入签章密码", "signcheck_div");
            $("#signcheck_div").show();
        }
        else { $("#signcheck_div").hide(); }
    }
    function SignPwdCheck() {
        var val = $("#signpwd_t").val(), id = $("#Sign_DP").val();
        if (val == "") { alert("密码不能为空"); return false; }
        $.post("/Plugins/Office/office.aspx?ProID=<%=ProID%>&AppID="+config.AppID, { action: "signpwdcheck", signid: id, signpwd: val }, function (data) {
            console.log(data);
            if (data == 0) {
                alert("密码不正确");
            }
            else {
                var url = WordPathDeal(data);
                document.all.WebOffice1.SetFieldValue("mark_3", "", "::ADDMARK::");
                document.all.WebOffice1.SetFieldValue("mark_1", url, "::JPG::");
                AddPicture("mark_3", url, 5);
                SaveToServer();
                $("#Sign_DP").val("")
                $("#signcheck_div").hide();
            }
        });
    }
    //function TestPic()
    //{
    //    document.all.WebOffice1.SetFieldValue("mark_1", "", "::ADDMARK::");
    //    AddPicture("mark_1", "D:\\sign.png", 5);
    //}
    //---------扩展Office方法 
    function AddPicture(strMarkName, strBmpPath, vType) {
        //定义一个对象，用来存储ActiveDocument对象
        var obj;
        obj = new Object(document.all.WebOffice1.GetDocumentObject());
        if (obj != null) {
            var pBookMarks;
            // VAB接口获取书签集合
            pBookMarks = obj.Bookmarks;
            var pBookM;
            // VAB接口获取书签strMarkName
            pBookM = pBookMarks(strMarkName);
            var pRange;
            // VAB接口获取书签strMarkName的Range对象
            pRange = pBookM.Range;
            var pRangeInlines;
            // VAB接口获取书签strMarkName的Range对象的InlineShapes对象
            pRangeInlines = pRange.InlineShapes;
            var pRangeInline;
            // VAB接口通过InlineShapes对象向文档中插入图片
            pRangeInline = pRangeInlines.AddPicture(strBmpPath);
            //设置图片的样式，5为浮动在文字上面
            pRangeInline.ConvertToShape().WrapFormat.TYPE = vType;
            delete obj;
        }
    }
    function SaveToServer() {
        var returnValue;					
        document.all.WebOffice1.HttpInit();	
        document.all.WebOffice1.HttpAddPostString("username", "OA用户");//这些上传给word文档处理的,并不会在路径中
        document.all.WebOffice1.HttpAddPostCurrFile("AipFile", "");
        var url = WordPathDeal("/Plugins/Office/OfficeAction.ashx?action=saveword&fname=" + config.fname + "&AppID=" + config.AppID);
        returnValue = document.all.WebOffice1.HttpPost(url);
        if (window.parent && parent.WordBack) {
            parent.WordBack(config.fname);
        }
        if ("true" == returnValue) {
             alert("保存成功");
        } else {
            alert("保存失败,原因:" + returnValue);
            //console.log("保存失败,原因:" + returnValue);
        }
    }
    //Action=add&ProID=2&fname=%e4%bc%9a%e8%ae%ae%e7%94%b3%e8%af%b720150523114329.docx
    function LoadFile(url) {
        url = WordPathDeal(url);
        console.log(url);
        // 如果对URL进行UTF8编码就调用
        document.all.WebOffice1.OptionFlag |= 0x0020;
        document.all.WebOffice1.OptionFlag &= 0xffdf;
        document.all.WebOffice1.LoadOriginalFile(url, "doc");
        //调用下面语句，则显示进度条：
        document.all.WebOffice1.OptionFlag |= 0x0080;
        if ($("#Protect_Hid").val() == "0") { ProtectFull(); }
    }
    function CreateDoc()
    {
        setTimeout(newDoc,1000);
    }
    function DelayLoadFile(url)
    {
        setTimeout(function () { LoadFile(url); }, 1000);
    }
    function AddRedHead() {
        var url = $("#Head_DP").val();
        if (url == "") return;
        url = WordPathDeal(url);
        document.all.WebOffice1.SetFieldValue("mark_1", "", "::ADDMARK::");			// 添加书签
        document.all.WebOffice1.SetFieldValue("mark_1", url, "::FILE::");
    }
    function WordPathDeal(url) {
        //路由后的路径需要用http,另其不支持/根目录
        var base = "<%=Request.Url.Authority%>";
        if (url.indexOf("/") == 0) {
            url = base + url;
            url = "http://" + url.replace("//","/");
        }
        return url;
    }
    /*----------通用区--------------*/
    var zldiag = new ZL_Dialog();
    function ShowZLDiag(title, url) {
        zldiag.title = title;
        zldiag.body = $("#" + url).show();
        zldiag.body = $("#" + url).remove().html();
        zldiag.maxbtn = false;
        zldiag.backdrop = true;
        zldiag.ShowModal();
    }
    function CloseZLDiag() { zldiag.CloseModal(); }
</script>
</asp:Content>
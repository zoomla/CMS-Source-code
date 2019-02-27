<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeFile="Edit.aspx.cs" Inherits="Edit_EditContent_EditCont" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>文档编辑</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
         <% 
    string DocTitle = "";
    //起草文件,则ID为NULL,否则为记录的主键
    string ID = Server.HtmlEncode(Request.QueryString["ID"]);
    //获取文件的 类型
    if (string.IsNullOrEmpty(DocType))
    {
        DocType = "doc";
    }
    if (!string.IsNullOrEmpty(ID))
    {
        DocTitle = Server.HtmlEncode(Request.QueryString["DocTitle"]);
    }
    %>
    <div style="background-color:#E0F2FE;padding:5px 0px 5px 5px;margin-bottom:5px;color:#0066FF">
<input type="hidden" id="DocTitle" value='<%=DocTitle %>'  />
<input type="hidden" name="DocUrl" id="DocUrl" />  
<span id="doctilespan" style="width:100%;display:block;text-align:center;margin-bottom:5px;"><%=DocTitle %></span>
<input type="button" id="Crea"     value="新建模板"   onclick="newDoc()" />  
<input type="button" id="Opendoc"  value="浏览文档"   onclick="OpenWord()" />
<input type="button" id="tiG"    value="返回"   onclick="parent.closeDoc();" />

<asp:TextBox ID="TxtTagKey" runat="server" Text='' Width="200px" Height="22px" CssClass="l_input" style="display:none;" />
</div>
<div id="Xtg" style="display:none;">

<textarea  cols="90" rows="10" id="content" name="content" runat="server"></textarea>
</div>
<div id="sp_01">
    <input type="hidden" id="Tgid" runat="server" />
        <div class="editcontent" >
        <object style="z-index: 1;" id="WebOffice1" height="130%" width="100%" classid="clsid:E77E049B-23FC-4DB8-B756-60529A35FAD5"
            codebase="wordEdit/WebOffice.ocx#version=6,0,5,0">
            <param name="_Version" value="65536" />
            <param name="_ExtentX" value="31141" />
            <param name="_ExtentY" value="15610" />
            <param name="_StockProps" value="0" />
            <param name="wmode" value="transparent">
        </object>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link href="/App_Themes/UserThem/edit.css" type="text/css" rel="stylesheet" />
 <%//获取服务器的地址 
   string URL = this.Session["URL"].ToString(); %>
    <script type="text/javascript">
    var idArrary = "";
    var i = 0;
    var docArrary = new Array();//存文档路径
    var docFileName = new Array();//文件名
    var replaceFlag = false;//是否替换原文，默认为false不替换
    //选择标签集(disuse)
    function labelSelect() {
        window.open('/Edit/labelSelect.aspx', '', 'width=600,height=450,resizable=0,scrollbars=yes');
    }
    //连接服务端，获取替换值后再生成文档
    function GenerateDoc() {
        if (document.all.WebOffice1.GetDocumentObject() == null) { alert("请先打开文档，再生成"); return; }
        //if (idArrary.replace(" ","") == "") { alert("未选择标签集,在生成文档前必须先选定标签集"); return;}
        $.post("Edit.aspx", { need: "single" },
            function (data) {
                //for (i = 0; i < data.length; i++)
                //{
                //    document.all.WebOffice1.ReplaceText(data[i].Label, data[i].LabelValue, 1);
                //}
                alert(data);
            }, "Json")
    }//GenerateDoc end;

    //等待一段时间，毫秒,这段时间不能做其他事.
    function sleep(numberMillis) {
        var now = new Date();
        var exitTime = now.getTime() + numberMillis;
        while (true) {
            now = new Date();
            if (now.getTime() > exitTime) return;
        }
    }

    //用户中心使用,批量生成
    function batGenerateDoc2(docTitle, docParam) {
        //--------------------完成docParam的切割
        //docArrary[0] = "../UploadFiles/DocTemp/测试模板.doc";
        //docFileName[0]="测试模板";
        var arr = new Array();
        arr = docParam.split(";");
        //----是否替换原文
        //replaceFlag = arr[1].split(":")[1] == "是" ? true : false;
        //----模板列表
        docFileName = arr[0].toString().split(",");
        for (i = 0; i < docFileName.length; i++) {
            docArrary[i] = "../../UploadFiles/DocTemp/" + docFileName[i];
        }
        //-----取自定义的需替换值,从1开始,0为文档模板
        for (index = 0; index < docArrary.length; index++)//第一个词为模板列表
        {
            $.ajaxSetup({ async: false });
            //0为打开失败,1为正常
            if (document.all.WebOffice1.LoadOriginalFile(docArrary[index], "doc")) { }
            else if (document.all.WebOffice1.LoadOriginalFile(docArrary[index] + ".doc", "doc")) { }
            else if (document.all.WebOffice1.LoadOriginalFile(docArrary[index] + ".docx", "doc")) { }
            else if (document.all.WebOffice1.LoadOriginalFile(docArrary[index] + ".dot", "doc")) { }
            else { alert(docArrary[index] + "模板无法加载，请确定文件是否存在且后缀名为doc,docx,dot"); }
            //----------值替换,1开始才是需要替换的值
            for (var i = 1; i < arr.length - 1; i++) {
                strArrary = arr[i].split(":");
                //if (replaceFlag)
                document.all.WebOffice1.ReplaceText(strArrary[0], strArrary[1], 1);
                //else document.all.WebOffice1.ReplaceText(strArrary[0], strArrary[0] + strArrary[1], 1);
            }
            $.ajaxSetup({ async: true });
            batSaveDoc(docTitle, docFileName[index]);
        }
        return true;
    }

    //批量上传文档
    function batSaveDoc(docTitle, fileName) {
        var titleIs = $("#DocTitle").val();
        var up = "0";
        var tgid = $("#Tgid").val();
        $.get("Edit.aspx?Tgid=" + $("#Tgid").val() + "&Tg=true&va=" + escape($("#<%=content.ClientID %>").val()) + "&title=" + $("#DocTitle").val(), function (d) {
            $("#Tgid").val(d);
            // parent.ShowS("0");//父窗口此方法
        })
        myform.DocTitle.value = fileName;
        //恢复被屏蔽的菜单项和快捷键
        document.all.WebOffice1.SetToolBarButton2("Standard", 1, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 2, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 3, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 6, 3);
    <%if (DocType == "doc")
      {%>
        //恢复文件菜单项
        document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 4);
        //恢复 保存快捷键(Ctrl+S) 
        document.all.WebOffice1.SetKeyCtrl(595, 0, 0);
        //恢复 打印快捷键(Ctrl+P) 
        document.all.WebOffice1.SetKeyCtrl(592, 0, 0);
    <%}
      else if (DocType == "xls")
      {%>
        //恢复文件菜单项
        document.all.WebOffice1.SetToolBarButton2("Worksheet Menu Bar", 1, 4);
    <%} %>

        //初始化Http引擎
        document.all.WebOffice1.HttpInit();
        //添加相应的Post元
        document.all.WebOffice1.HttpAddPostString("DocTitle", escape(myform.DocTitle.value));
        if (up == "1") {
            document.all.WebOffice1.SetTrackRevisions(0);
            document.all.WebOffice1.ShowRevisions(0);
            document.all.WebOffice1.HttpAddPostString("ID", $("#Tgid").val());
        } else {
            document.all.WebOffice1.SetTrackRevisions(0);
            document.all.WebOffice1.ShowRevisions(0);
            document.all.WebOffice1.HttpAddPostString("ID", "0");
        }
        document.all.WebOffice1.HttpAddPostString("DocType", "<%=DocType%>");

        //把当前文档添加到Post元素列表中，文件的标识符䶿DocContent
        document.all.WebOffice1.HttpAddPostCurrFile("DocContent", "");
        var vtRet = "";
        //HttpPost执行上传的动仿WebOffice支持Http的直接上传，在upload.aspx的页面中,解析Post过去的数慿
        //拆分出Post元素和文件数据，可以有选择性的保存到数据库中，或保存在服务器的文件中⾿
        //HttpPost的返回值，根据upload.aspx中的设置，返回upload.aspx中Response.Write回来的数据
        vtRet = document.all.WebOffice1.HttpPost("<%=URL %>/batUpload.aspx?case=" + encodeURI(docTitle));
    }//batSaveDoc End;

    //根据传入的checkbox的选中状态设置所有checkbox的选中状态
    function selectAll(obj) {
        var allInput = document.getElementsByTagName("input");
        //alert(allInput.length);
        var loopTime = allInput.length;
        for (i = 0; i < loopTime; i++) {
            //alert(allInput[i].type);
            if (allInput[i].type == "checkbox") {
                allInput[i].checked = obj.checked;
            }
        }
    }
    //获取选中的CheckBox的ID(Disuse)
    function getChecked() {
        idArrary = "";
        var allInput = document.getElementsByName("pidCheckbox");
        for (i = 0; i < allInput.length; i++) {
            if (allInput[i].checked) {
                idArrary += allInput[i].value + ",";
            }
        }
        if (idArrary == "") { alert("请先选择需要载入的标签集"); }
        else { alert("标签集选择完成;"); }

    }
    //新建文档
    function newDoc() {
        if (document.all.WebOffice1.GetDocumentObject() != null) {
            if (confirm("当前有一份文档正在编辑，你确定要重建一个空文档吗"))
                location = "Edit.aspx?ID=0&DocType=&DocTitle=&uptp=&date=" + Date();
        }
        else
            location = "Edit.aspx?ID=0&DocType=&DocTitle=&uptp=&date=" + Date();
    }
    var edit;
    window.setInterval(function () {
        if ($("#sp_01").css("display") == "none") {
            $("#<%=content.ClientID %>").val(edit.document.getBody().getHtml());
            $.get("Edit.aspx?Tgid=" + $("#Tgid").val() + "&Tg=true&va=" + escape($("#<%=content.ClientID %>").val()) + "&title=" + $("#DocTitle").val(), function (d) {
                $("#Tgid").val(d);
                parent.ShowS("1");
            })
            window.setTimeout(function () {
                parent.ShowS("0");
            }, 3000)
        }
    }, 180000);
    //另存为功能
    function SaveAsTo() {
        try {
            var webObj = document.getElementById("WebOffice1");
            webObj.ShowDialog(84);
        } catch (e) {
            alert("异常\r\nError:" + e + "\r\nError Code:" + e.number + "\r\nError Des:" + e.description);
        }
    }
</script>
    <script  type="text/javascript">
    // ---------------------== 关闭页面时调用此函数，关闭文档--------------------- //
    function window_onunload() { document.all.WebOffice1.Close(); }
    // -----------------------------== 保存文档 ==------------------------------------ //
    function SaveDoc() {
        if (document.all.WebOffice1.GetDocumentObject() == null) { alert("请先打开模板文档，再上传"); return; }
        var titleIs = $("#DocTitle").val();
        var up = "0";
        var tgid = $("#Tgid").val();

        $.get("Edit.aspx?Tgid=" + $("#Tgid").val() + "&Tg=true&va=" + escape($("#<%=content.ClientID %>").val()) + "&title=" + $("#DocTitle").val(), function (d) {
            $("#Tgid").val(d);
            parent.ShowS("0");
        })
        //}else{

        if (tgid != "0") {
            if (titleIs == "") {
                var title = prompt("请输入模板名，不需要定义后缀。", "");
                if (title == null) {
                    return false;
                }
                myform.DocTitle.value = title;
            }
            $("#Xtg").css("display", "none");
            $("#sp_01").css("display", "");
            up = "1";
        } else {
            var title = prompt("请输入文档标题，不需要定义后缀。", "");
            if (title == null) {
                return false;
            }
            myform.DocTitle.value = title;
            up = "0";
        }
        //恢复被屏蔽的菜单项和快捷键
        document.all.WebOffice1.SetToolBarButton2("Standard", 1, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 2, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 3, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 6, 3);

    <%if (DocType == "doc")
      {%>
        //恢复文件菜单项
        document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 4);
        //恢复 保存快捷键(Ctrl+S) 
        document.all.WebOffice1.SetKeyCtrl(595, 0, 0);
        //恢复 打印快捷键(Ctrl+P) 
        document.all.WebOffice1.SetKeyCtrl(592, 0, 0);
    <%}
      else if (DocType == "xls")
      {%>
        //恢复文件菜单项
        document.all.WebOffice1.SetToolBarButton2("Worksheet Menu Bar", 1, 4);
    <%} %>

        //初始化Http引擎
        document.all.WebOffice1.HttpInit();
        //添加相应的Post元
        document.all.WebOffice1.HttpAddPostString("DocTitle", escape(myform.DocTitle.value));
        if (up == "1") {
            document.all.WebOffice1.SetTrackRevisions(0);
            document.all.WebOffice1.ShowRevisions(0);
            document.all.WebOffice1.HttpAddPostString("ID", $("#Tgid").val());
        } else {
            document.all.WebOffice1.SetTrackRevisions(0);
            document.all.WebOffice1.ShowRevisions(0);
            document.all.WebOffice1.HttpAddPostString("ID", "0");
        }
        document.all.WebOffice1.HttpAddPostString("DocType", "<%=DocType%>");


        //把当前文档添加到Post元素列表中，文件的标识符䶿DocContent
        document.all.WebOffice1.HttpAddPostCurrFile("DocContent", "");
        var vtRet;
        //HttpPost执行上传的动仿WebOffice支持Http的直接上传，在upload.aspx的页面中,解析Post过去的数慿
        //拆分出Post元素和文件数据，可以有选择性的保存到数据库中，或保存在服务器的文件中⾿
        //HttpPost的返回值，根据upload.aspx中的设置，返回upload.aspx中Response.Write回来的数据
        vtRet = document.all.WebOffice1.HttpPost("<%=URL %>/upload.aspx");
        alert(vtRet);
        if ($("#DocTitle").val() == "") {
            alert("草稿保存成功");
            location = "Edit.aspx?ID=" + $("#Tgid").val() + "&DocType=" + "<%=DocType%>" + "&DocTitle=" + myform.DocTitle.value + "&uptp=Iser";
        } else {
        }

        return_onclick();// }
    }

    function LSaveDoc() {
        //恢复被屏蔽的菜单项和快捷键
        document.all.WebOffice1.SetToolBarButton2("Standard", 1, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 2, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 3, 3);
        document.all.WebOffice1.SetToolBarButton2("Standard", 6, 3);
    <%if (DocType == "doc")
      {%>
        //恢复文件菜单项
        document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 4);
        //恢复 保存快捷键(Ctrl+S) 
        document.all.WebOffice1.SetKeyCtrl(595, 0, 0);
        //恢复 打印快捷键(Ctrl+P) 
        document.all.WebOffice1.SetKeyCtrl(592, 0, 0);
    <%}
      else if (DocType == "xls")
      {%>
        //恢复文件菜单项
        document.all.WebOffice1.SetToolBarButton2("Worksheet Menu Bar", 1, 4);
    <%} %>
        //初始化Http引擎,上传前一定要执行此方法
        document.all.WebOffice1.HttpInit();
        //添加表单域,可多次执行此方法 
        document.all.WebOffice1.HttpAddPostString("DocTitle", escape(myform.DocTitle.value));
        document.all.WebOffice1.SetTrackRevisions(0);
        document.all.WebOffice1.ShowRevisions(0);
        document.all.WebOffice1.HttpAddPostString("ID", "0");
        document.all.WebOffice1.HttpAddPostString("DocType", "<%=DocType%>");
        //添加要上传的word或excel，
        //param:要上传的文件的ID，上传后的文件名，为空由程序指定
    document.all.WebOffice1.HttpAddPostCurrFile("DocContent", "");
    var vtRet;
    vtRet = document.all.WebOffice1.HttpPost("<%=URL %>/upload.aspx");
        alert(vtRet);
        return_onclick();
    }
</script>
    <script type="text/javascript">
        function SetG(id) {
            if (id == "tiG") {
                $("#<%=content.ClientID %>").val(edit.document.getBody().getHtml());
            $.get("Edit.aspx?Tgid=" + $("#Tgid").val() + "&Tg=true&va=" + escape($("#<%=content.ClientID %>").val()) + "&title=" + $("#DocTitle").val(), function (d) {
                $("#Tgid").val(d);
            })
            $("#Xtg").css("display", "");
            $("#sp_01").css("display", "none");
        } else {
            parent.ShowS("0");
            $("#Xtg").css("display", "none");
            $("#sp_01").css("display", "");
        }
    }
    //var addNew="";
    function copytext(content) {
        var vObj = document.all.WebOffice1.GetDocumentObject();
        vObj.Application.Selection.Range.Text = content;
    }
    function copyimg(src) {
        //当前光标位置插入图片
        document.all.WebOffice1.InSertFile(src, 8);
    }
    function OpenWord() {
        window.open('/Edit/ShowEdit.aspx?OpenWords=<%=TxtTagKey.ClientID %>', '', 'width=650,height=450,resizable=0,scrollbars=yes');
    }
    function GetWord() {
        document.all.WebOffice1.LoadOriginalFile("../UploadFiles/DocTemp/" + document.getElementById('TxtTagKey').value, "doc");
    }
    //根据标题装载文档,User下，关生成，上传 title是path+文件名+后缀名
    function openWord2(title) {
        if (document.all.WebOffice1.LoadOriginalFile("../../UploadFiles/DocTemp/" + title, "doc")) { }
        else if (document.all.WebOffice1.LoadOriginalFile("../../UploadFiles/DocTemp/" + title + ".doc", "doc")) { }
        else if (document.all.WebOffice1.LoadOriginalFile("../../UploadFiles/DocTemp/" + title + ".docx", "doc")) { }
        else if (document.all.WebOffice1.LoadOriginalFile("../../UploadFiles/DocTemp/" + title + ".dot", "doc")) { }
        else { alert(title + "无法加载,请检查是否有该文件，或后缀名是否为doc,docx,dot") }
    }
    //用于plus目录,临时，后期将方法整合
    function openWord3(title) {
        if (document.all.WebOffice1.LoadOriginalFile("../../../UploadFiles/DocTemp/" + title, "doc")) { }
        else if (document.all.WebOffice1.LoadOriginalFile("../../../UploadFiles/DocTemp/" + title + ".doc", "doc")) { }
        else if (document.all.WebOffice1.LoadOriginalFile("../../../UploadFiles/DocTemp/" + title + ".docx", "doc")) { }
        else if (document.all.WebOffice1.LoadOriginalFile("../../../UploadFiles/DocTemp/" + title + ".dot", "doc")) { }
        else { alert(title + "无法加载,请检查是否有该文件，或后缀名是否为doc,docx,dot") }
    }
</script>
</asp:Content>


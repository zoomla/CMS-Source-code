<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="user_iServer_AddQuestion" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>提交问题</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="cnt"></div> 
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="FiServer.aspx">有问必答</a></li>
        <li class="active">提交问题</li> 
    </ol>
</div>
    <div class="container btn_green">
    <asp:HiddenField ID="OrderID" runat="server" />
    <table class="table table-bordered">
        <tr style="height: 25px; background-color: #fff;" valign="bottom">
            <td class="title" align="center">提交问题
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="width: 100%">
                <div id="viewPanel">
                    <table style="width:100%;">
                        <tr>
                            <td style="width:100%;">
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="2">
                                            <input type="hidden" name="module" value="mysupport_supportticketadd" />
                                            <input type="hidden" name="dosupportticketadd" value="" />
                                            <table class="table table-striped table-bordered table-hover">
                                                <tr>
                                                    <td style="width: 100px">优先级<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control text_md" runat="server" AutoPostBack="true">
                                                            <asp:ListItem Value="低">低</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="中">中</asp:ListItem>
                                                            <asp:ListItem Value="高">高</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>问题类别<span style="color: red">*</span></td>
                                                    <td>
                                                        <asp:DropDownList CssClass="form-control text_md" ID="DropDownList2" runat="server" AutoPostBack="true">   
                                                            <asp:ListItem Selected="True" Value="咨询">咨询</asp:ListItem>
                                                            <asp:ListItem Value="投诉">投诉</asp:ListItem>
                                                            <asp:ListItem Value="建议">建议</asp:ListItem>
                                                            <asp:ListItem Value="要求">要求</asp:ListItem>
                                                            <asp:ListItem Value="界面使用">界面使用</asp:ListItem>
                                                            <asp:ListItem Value="bug报告">bug报告</asp:ListItem>
                                                            <asp:ListItem Value="订单">订单</asp:ListItem>
                                                            <asp:ListItem Value="财务">财务</asp:ListItem>
                                                            <asp:ListItem Value="域名">域名</asp:ListItem>
                                                            <asp:ListItem Value="主机">主机</asp:ListItem>
                                                            <asp:ListItem Value="邮局">邮局</asp:ListItem>
                                                            <asp:ListItem Value="DNS">DNS</asp:ListItem>
                                                            <asp:ListItem Value="MSSQL">MSSQL</asp:ListItem>
                                                            <asp:ListItem Value="MySQL">MySQL</asp:ListItem>
                                                            <asp:ListItem Value="IDC">IDC</asp:ListItem>
                                                            <asp:ListItem Value="网站推广">网站推广</asp:ListItem>
                                                            <asp:ListItem Value="网站制作">网站制作</asp:ListItem>
                                                            <asp:ListItem Value="其它">其它</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>希望问题解决的时间</td>
                                                    <td id="dropdownlist3">  
                                                        <select class="form-control text_md" onchange="AddDate()">
                                                            <option value="0">立时</option>
                                                            <option value="1">1个工作日</option>
                                                            <option value="7">一周以内</option>
                                                            <option value="3650">不限</option>
                                                            <option value="0">自选具体时间</option>
                                                        </select> 
                                                        <input style="display:none;" type="text" id="test" onfocus="WdatePicker({})" />
                                                        <input type="text"  id="mydate_t" class="form-control text_md"  onfocus="WdatePicker({dateFmt:'yyyy/MM/dd',minDate:'<%=DateTime.Now.AddDays(1).ToString("yyyy/MM/dd")%>'})" />                                       
                                                    </td>
                                                     
                                                </tr>
                                                <tr>
                                                <tr>
                                                    <td style="width: 100px">问题标题<span style="color: red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control text_md" runat="server" onblur="isNulltitle();"></asp:TextBox>
                                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>问题内容<span style="color: red">*</span>
                                                    </td>
                                                    <td id="iframeHtml">
                                                        <textarea runat="server" id="textarea1"  name="content" rows="4" style="width:80%; height: 300px;"
                                                            cols="40" onblur="isNullstr();"></textarea>
                                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" CssClass="ckeditor" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>附件
                                                    </td>
                                                    <td>
                                                        <input type="button" id="upfile_btn" class="btn btn-primary" value="选择文件" />
                                                         <div style="margin-top:10px;" id="uploader" class="uploader"><ul class="filelist"></ul></div>
                                                        <asp:HiddenField runat="server" ID="Attach_Hid" />
                                                    </td>
                                                </tr>                                               
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="LinkButton1" runat="server" CssClass="btn btn-primary" OnClick="LinkButton1_Click"
                                                            Text="提交" />
                                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="取消" OnClick="Button1_Click" CausesValidation="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
         <div class="alert alert-success">
    <i class="fa fa-lightbulb-o"></i>
    提示：您可以使用AddQuestion.aspx?title=222&con=content的方法进行GET传递。
    </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <%=Call.GetUEditor("textarea1",2) %>
    <link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Webup.js"></script>
    <script type="text/javascript">
        function isNulltitle() {
            var lbltitle = document.getElementById("Label1");
            var title = document.getElementById("TextBox1").value;
            if (title == "") {
                lbltitle.innerHTML = "请输入问题标题!!!";
                return;
            }
            else
                lbltitle.innerHTML = "";
        }

        function isNullstr() {
            var lblstr = document.getElementById("Label2");
            var str = document.getElementById("textarea1").value;
            if (str == "")
                lblstr.innerHTML = "请输入问题内容!!!"
            else
                lblstr.innerHTML = "";
        }
        $(function () {
            $("#upfile_btn").click(ZL_Webup.ShowFileUP);
        })
        function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
        function AddDate() {
            var d = $dp.$DV("<%=DateTime.Now.ToString("yyyy/MM/dd") %>", { d: parseInt(event.srcElement.value) });
                document.getElementById("mydate_t").value = d.y + "/" + d.M + "/" + d.d;
            }
         
    </script>
</asp:Content>
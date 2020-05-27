<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormView.aspx.cs" Inherits="ZoomLaCMS.rss.FormView" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>逐浪年会调查表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content" ID="form1">
      <div class="bg">
        <div class="container">
            <div class="row">
                <div id="formDesign_Div">
                    <div id="myform">
                        <div class="formtitle" runat="server" id="formtitle"></div>
                        <div class="formintro" runat="server" id="formintro"></div>
                         <ul id="formul">
                        </ul>
                    </div>
                    <div style="text-align:center;">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="提交" ID="SaveDataBtn" OnClick="SaveDataBtn_Click" OnClientClick="return SaveData();" />
                    </div>
                </div>
            </div>
        </div></div>
        <asp:HiddenField runat="server" ID="PubContent" />
        <div style="display:none;">
            <div id="input_txtTlp">
                <li id='@id' class='formli'>
                        <span class='title_field'><label class='title'>@title</label></span>
                        <p class='intro'>@intro</p>
                        <div class='content'>@tlp</div>
                </li>
            </div>
            <div id="selectTlp">
                <li id='@id' class='formli'>
                    <span class='title_field'><label class='title'>@title</label></span>
                    <p class='intro'>@intro</p>
                    <div class='content'>@tlp</div>
                </li>
            </div>
            <div id="imgTlp">
                    <li id="@id" class='formli'>
                    <div class="content"><img class="img_normal" src="@src" /></div>
                    <p class="intro">@intro</p>
                </li>
            </div>
            <div id="strTlp">
                 <li id="@id" class='formli'>
                    <span class="title_field"><label class="title">@title</label></span>
                    <p class="intro">@intro</p>
                    <div class="content"></div>
                </li>
            </div>
        </div>
        
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        .bg{background:url(/App_Themes/AdminDefaultTheme/images/PubForm/bg.jpg);background-position: center;left:0;top:0;right:0;bottom:0; position: absolute; background-repeat:no-repeat;background-size:cover;}
        .container{font-family:'Microsoft YaHei';width:800px;}
        ul{padding-left:0px;}
        ul li{list-style:none;}
        /*表单呈现区*/
        #formDesign_Div{padding:10px;padding-top:5px;padding-bottom:20px;}
        #myform{color:#6f5f5c;background:#FFF; box-shadow:0px 1px 6px rgba(0, 0, 0, 0.44);}
        .formli{padding:5px;border:1px solid transparent;}
        .formli:hover{background-color:rgba(139, 83, 125, 0.40);}
        .formtitle{text-align:center;padding:5px;font-size:24px;font-weight:bold; color:white;line-height:30px;background-color:#724b68;}
        .formintro{font-size:12px;color:white;text-align:center;background-color:#724b68;}     
        /*表单元素CSS*/
        .com_required{color:red;padding-left:5px;}
        .input{border:1px solid #ddd;padding-left:5px;}
        .textarea{border:1px solid #ddd;padding-left:5px;resize:none;height:120px;}
        .intro{font-size:12px;border-top:1px dashed #ddd;padding:5px;}
        .large {width:90%;}
        .middle{width:60%;}
        .small {width:30%;}
        input[name=sel_rad], input[name=sel_chk]{margin-right:3px;}
        .img_normal{width:100%;height:200px;}
      </style>
    <script type="text/javascript" src="/JS/ZLForm.js"></script>
    <script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
    <script type="text/javascript">
        function GetArr() {
            var comArr = [];
            comArr = JSON.parse($("#PubContent").val());
            comArr.sort(function (a, b) { return a.sortnum > b.sortnum ? 1 : -1; });
            return comArr;
        }
        function InitForm() {
            var comArr = GetArr();
            for (var i = 0; i < comArr.length; i++) {
                ZLForm.AddFormli(comArr[i]);
            }//for end;
        }
        function SaveData() {
            var comArr = GetArr();
            for (var i = 0; i < comArr.length; i++) {
                var flag = ZLForm.GetValue(comArr[i]);
                if (!flag) { alert(comArr[i].title + "的值不能为空"); return false; }
            }
            document.getElementById("PubContent").value = JSON.stringify(comArr);
            return true;
        }
        </script>
</asp:Content>



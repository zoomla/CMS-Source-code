<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vote.aspx.cs" Inherits="Common_Vote" MasterPageFile="~/Common/Common.master" EnableViewStateMac="false"  ClientIDMode="Static" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>投票调查</title>
    <style>.list li{float:none;}</style>
    <script src="/JS/Verify.js"></script>
    <script src="/JS/ZL_ValidateCode.js"></script>
    <script type="text/javascript">
        //检查答题情况， 假定每个都是必填的
        var goto = false;
        var vflag = true;
        function CheckAns() {
            var flag = false;
            var tables = document.getElementsByTagName("table");
            preview(1);
            for (var i = 0; i < tables.length; i++) {
                var options = document.getElementsByName("vote_" + i);
                flag = false;
                if (options.length > 1) {
                    for (var j = 0; j < options.length; j++) {
                        if (options[j].checked) {
                            flag = true;
                            break;
                        }
                    }
                }
                else if (options.length == 1) {
                    if (options.value.length > 0)
                        flag = true;
                }
                if (flag == false || vflag == false) {
                    alert('问卷未完成， 请继续答题。。。');
                    // document.getElementById("mao_" + i).focus();
                    // GotoAnchor(i);
                    return false;
                }
            }
            return true;
        }
        //跳转到指定位置
        function GotoAnchor(pos) {
            var url = location.href;
            if (url.indexOf("#mao_") > 0) {
                url = url.substring(0, url.lastIndexOf('_') + 1);
                url = url + pos;
            }
            else {
                url = url + "&#mao_" + pos;
            }
            location.href = url;
        }
        //特殊文本的验证
        function IsLegalValue(id, type) {
            var value = document.getElementById("txt_" + id).value;
            switch (type) {
                case 0:
                    vflag = CheckEmail(value);
                    break;
                case 1:
                    vflag = CheckMobile(value);
                    break;
                case 2:
                    vflag = CheckPhone(value);
                    break;
                case 3:
                    vflag = CheckIdCard(value);
                    break;
                case 4:
                    vflag = CheckSchCard(value);
                    break;
            }
            if (vflag == false) {
                document.getElementById("span_" + id).style.display = 'block';
                //document.getElementById("txt_" + id).focus();
                // document.getElementById("txt_" + id).select();
            }
            else
                document.getElementById("span_" + id).style.display = 'none';
        }
    </script>
    <script>
        function preview(oper) {
            if (oper < 10) {
                bdhtml = document.getElementById("divContent").innerHTML;//获取当前页的html代码  
                sprnstr = "<!--startprint" + oper + "-->";//设置打印开始区域  
                eprnstr = "<!--endprint" + oper + "-->";//设置打印结束区域  
                prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 18); //从开始代码向后取html  

                prnhtmlprnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));//从结束代码向前取html  
                document.getElementById("divContent").innerHTML = prnhtml;
                window.print();
                document.getElementById("divContent").innerHTML = bdhtml;
            } else {
                window.print();
            }
        }

        function ShowflaUp(id) {
            if (document.getElementById("viewid_" + id).style.display != 'none')
                document.getElementById("viewid_" + id).style.display = 'none';
            else document.getElementById("viewid_" + id).style.display = '';
        }
    </script>
    <script type="text/javascript">
        function CheckIsNull()
        {
            var flag = true;
            if ($("#IsNull_H").val())
            {
                var nullArr = $("#IsNull_H").val().split(',');
                for (var i = 0; i < nullArr.length && flag; i++) {
                    if ($("input[name=" + nullArr[i] + "]:checked").length < 1)
                        flag = false;
                }
                if (!flag)
                    alert('有必填项尚未填写！！');
            }
            return flag;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="main" class="container">
     <div id="divContent" class="content" runat="server">
                 <div class="panel panel-primary margin_t10">
                  <div class="panel-heading">
                    <h3 class="panel-title"><asp:Literal ID="ltlSurveyName" runat="server" Text="Label"></asp:Literal> <span class="pull-right"> <asp:Literal ID="ltlDate" runat="server"></asp:Literal></span></h3>
                  </div>
                  <div class="panel-body">
                    <div class="desp title">
                        <p>
                            <asp:Literal ID="ltlDesp" runat="server" Text="Label"></asp:Literal>
                        </p>
                    </div>
                      <div class="list title">
                    <ul>
                        <asp:Literal ID="ltlResultHtml" runat="server"></asp:Literal>
                    </ul>
                    </div>
                    <div id="regVcodeRegister" class="list" runat="server" visible="false">
                        <table width='100%' align='center' class='border votetab'>
                            <tr>
                                <th width="10%" align="left">验证码：</th>
                                <td>
                                    <div class="reg_put">
                                        <asp:TextBox ID="SendVcode" MaxLength="6" runat="server" Style="width: 60px; border: #CCC solid 1px;"
                                            CssClass="input_out" onfocus="this.className='input_on';this.onmouseout=''" onblur="this.className='input_off';this.onmouseout=function(){this.className='input_out'};"
                                            onmousemove="this.className='input_move'" onmouseout="this.className='input_out'"></asp:TextBox>
                                        <asp:Image ID="SendVcode_img" runat="server" Height="20px"
                                            ToolTip="点击刷新验证码" Style="cursor: pointer; border: 0; vertical-align: middle;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="SendVcode"
                                            Display="Dynamic" ErrorMessage="验证码不能为空!"></asp:RequiredFieldValidator>
                                        <asp:Label ID="Validateinfo" runat="server" Text=""></asp:Label>
                                        <input type="hidden" id="SendVcode_hid" name="VCode_hid" />
                                        <script>
                                            $(function () { $("#SendVcode").ValidateCode(); })
                                        </script> 
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="bottom">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return CheckIsNull()" />
                <input type="button" class="btn btn-primary" value="打印" onclick=" CheckAns();" />
                <asp:Button ID="Button2" runat="server" Text="保存" OnClick="Button2_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnExport" runat="server" Text="导 出" CssClass="btn btn-primary" OnClick="btnExport_Click" />
            </div>
                  </div>
                </div>
                <%--<hr class="compart" />--%>
            </div>
            <!--/content-->
            <asp:HiddenField ID="IsNull_H" runat="server" />
            <%--<div id="foot">
                当前页面执行时间：
                <script type="text/javascript"> 
            <!-- 
                var startTime, endTime;
                var d = new Date();
                startTime = d.getTime();
                //--> 
                </script>
                <script type="text/javascript">
                    d = new Date();
                    endTime = d.getTime();
                    document.write((endTime - startTime) / 1000);
                </script>
            </div>--%>
        </div>
    <asp:HiddenField ID="HdnSID" runat="server" />
        <asp:HiddenField runat="server" ID="Random_Hid" />
</asp:Content>
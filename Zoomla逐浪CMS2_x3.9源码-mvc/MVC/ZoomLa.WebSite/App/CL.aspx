<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CL.aspx.cs" Inherits="ZoomLaCMS.App.CL" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>APP颁发</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <div class="col-md-7 col-lg-7" style="padding-left: 0px;">
            <table class="table table-bordered table-striped">
                <tr>
                    <td colspan="2" class="text-center"><strong>智能设备识别</strong></td>
                </tr>
                <tr>
                    <td>应用名称：</td>
                    <td>
                        <asp:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300" /></td>
                </tr>
                <tr>
                    <td class="td_m">Android：</td>
                    <td>
                        <asp:TextBox runat="server" ID="android_T" CssClass="form-control text_300 url_txt required" />
                        <span class="r_red txtvalid"></span><span class="r_red">*</span><span class="r_green">默认链接</span>
                    </td>
                </tr>
                <tr>
                    <td>iPhone：</td>
                    <td>
                        <asp:TextBox runat="server" ID="iphone_T" CssClass="form-control text_300 url_txt" /><span class="r_red txtvalid"></span></td>
                </tr>
                <tr>
                    <td>iPad：</td>
                    <td>
                        <asp:TextBox runat="server" ID="ipad_T" CssClass="form-control text_300 url_txt" /><span class="r_red txtvalid"></span></td>
                </tr>
                <tr>
                    <td>WPhone：</td>
                    <td>
                        <asp:TextBox runat="server" ID="wphone_T" CssClass="form-control text_300 url_txt" /><span class="r_red txtvalid"></span></td>
                </tr>
                <tr>
                    <td>PC：</td>
                    <td>
                        <asp:TextBox runat="server" ID="pc_T" CssClass="form-control text_300 url_txt" /><span class="r_red txtvalid"></span></td>
                </tr>
                <tr>
                    <td>生成链接：</td>
                    <td>
                        <asp:Label runat="server" ID="Link_L" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="CreateLink_Btn" OnClientClick="return CheckUrl();" OnClick="CreateLink_Btn_Click" Text="生成二维码" CssClass="btn btn-lg btn-primary" />
                        <a href="CLList.aspx" class="btn btn-lg btn-primary">返回列表</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-md-5 col-lg-5 text-center;">
            <div class="codediv" runat="server" id="codediv">
                <div class="codefont">
                    左侧输入链接<br />
                    点击即可生成二维码
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox runat="server" ID="CurAgent_T" CssClass="form-control" TextMode="MultiLine" Style="height: 100px;" Visible="false" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .codediv {
            border: 1px solid #bbb;
            border-radius: 3px;
            width: 320px;
            height: 320px;
            background-color: #fff;
            margin-top: 30px;
            padding: 20px;
            text-align: center;
        }

        .codefont {
            margin-top: 120px;
            font-size: 20px;
        }

        .codeimg {
            width: 280px;
            height: 280px;
        }
    </style>
    <script src="/JS/jquery.validate.min.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        function CheckUrl() {
            $(".url_txt").siblings('.txtvalid').text('');
            var urls = $(".url_txt");
            for (var i = 0; i < urls.length; i++) {
                var url = StrHelper.UrlDeal(urls[i].value);
                if (url != "" && !ZL_Regex.isUrl(url)) {
                    $(urls[i]).siblings(".txtvalid").text("格式不正确");
                    return false;
                }
            }
            var vaild = $("form").validate({ meta: "validate" });
            return (true && vaild.form());
        }
    </script>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.AddContent" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<%@ Register Src="~/Manage/I/ASCX/TemplateView.ascx" TagPrefix="ZL" TagName="TlpListView" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title><asp:Literal runat="server" ID="Title_L"></asp:Literal></title>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="manage_content">
        <div class="tabbable">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#ConInfo" data-toggle="tab"><%=Resources.L.内容信息 %></a></li>
                <li><a href="#InfoAttr" data-toggle="tab"><%=Resources.L.信息属性 %></a></li>
                <li><a href="#Process " data-toggle="tab"><%=Resources.L.流程控制 %></a></li>
                <li runat="server" id="cr_tab"><a href="#Copyright " data-toggle="tab">版权管理</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="ConInfo">
                    <table id="mainTable" class="table table-bordered table_padding0 addcontent_modeltale">
                        <tr>
                            <td style="overflow-x: hidden;" class="col-sm-1 col-xs-1 fd_td_l">
                                <asp:Label ID="bt_txt" runat="server" Text="<%$Resources:L,标题 %>"></asp:Label></td>
                            <td class="col-sm-11 col-xs-11">
                                <asp:TextBox ID="txtTitle" Style="background: url(/Images/bg1.gif) repeat-x;" CssClass="form-control m715-50" onkeyup="isgoEmpty('txtTitle','span_txtTitle');Getpy('txtTitle','PYtitle')" runat="server"></asp:TextBox>
                                <span class="vaild_tip">*</span>
                                <a href="javascript:;" id="Button11" class="btn btn-info btn-sm" onclick="ShowTitle()"><i class="fa fa-info"></i><%=Resources.L.标题属性 %></a>
                                <button type="button" class="btn btn-info btn-sm" onclick="ShowContentList()"><i class="fa fa-chain"></i><%=Resources.L.关联内容 %></button>
                                <button type="button" class="btn btn-info btn-sm" onclick="ShowSys();"><i class="fa fa-list"></i></button>
                                <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="txtTitle" ErrorMessage="<%$Resources:L,标题不能为空 %>" ForeColor="Red" Display="Dynamic" />
                                <span id="span_txtTitle" name="span_txtTitle"></span>
                                <asp:HiddenField ID="ThreadStyle" runat="server" />
                                <div id="duptitle_div" class="alert alert-warning" style="position: absolute; margin-left: 315px; display: none;">
                                    <ul id='duptitle_ul'></ul>
                                </div>
                            </td>
                        </tr>
                        <tbody id="Sys_Fied" runat="server" style="display: none;">
                            <tr>
                                <td class="fd_td_l"><span><%=Resources.L.拼音缩写 %>：</span></td>
                                <td>
                                    <asp:TextBox ID="PYtitle" CssClass="form-control m715-50" runat="server" /></td>
                            </tr>
                            <tr id="spec_tr">
                                <td class="fd_td_l"><span><%=Resources.L.所属专题 %>：</span></td>
                                <td>
                                    <div class="specDiv"></div>
                                    <span id="specbtn_span">
                                        <asp:Literal ID="SpecInfo_Li" runat="server"></asp:Literal></span>
                                    <asp:HiddenField ID="Spec_Hid" runat="server" />
                                </td>
                            </tr>
                            <tr runat="server">
                                <td class="fd_td_l">
                                    <asp:Label ID="gjz_txt" runat="server" Text="<%$Resources:L,关键字 %>"></asp:Label></td>
                                <td>
                                    <div id="OAkeyword"></div>
                                    <asp:TextBox ID="Keywords" runat="server" CssClass="form-control" />
                                    <asp:HiddenField runat="server" ID="IgnoreKey_Hid" />
                                    <span>(空格或回车键分隔，长度不超过10字符或5汉字)</span>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td class="fd_td_l">
                                    <asp:Label ID="Label4" runat="server" Text="<%$Resources:L,副标题 %>"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Subtitle" CssClass="form-control m715-50" runat="server"></asp:TextBox></td>
                            </tr>
                        </tbody>
                        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
                        <tr id="attPic" runat="server">
                            <td class="fd_td_l"><%=Resources.L.主编辑器扩展图 %>：</td>
                            <td style="height: 80px;">
                                <ul id="ThumImg_ul" class="preview_img_ul"></ul>
                                <div class="clearfix"></div>
                                <asp:HiddenField runat="server" ID="ThumImg_Hid" />
                            </td>
                        </tr>
                        <tr id="CreateHTML" runat="server">
                            <td class="fd_td_l">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:L,生成 %>"></asp:Label></td>
                            <td>
                                <asp:CheckBox ID="quickmake" runat="server" Checked="false" Text="<%$Resources:L,是否立即生成 %>" /></td>
                        </tr>
                    </table>
                    <div style="height: 50px;"></div>
                </div>
                <div class="tab-pane fade" id="InfoAttr">
                    <table class="table table-bordered table_padding0">
                        <tr>
                            <td class="fd_td_l td_l"><%=Resources.L.录入者 %></td>
                            <td>
                                <asp:TextBox ID="txtInputer" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><%=Resources.L.添加时间 %></td>
                            <td>
                                <asp:TextBox ID="txtAddTime" CssClass="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l">
                                <asp:Label ID="gx_time" runat="server" Text="<%$Resources:L,更新时间 %>"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtdate" CssClass="form-control text_md" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><span><%=Resources.L.自动审核 %></span></td>
                            <td>
                                <asp:TextBox ID="CheckDate_T" CssClass="form-control text_md" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><span><%=Resources.L.自动过期 %></span></td>
                            <td>
                                <asp:TextBox ID="TimeDate_T" CssClass="form-control text_md" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l">
                                <asp:Label ID="hits_txt" runat="server" Text="<%$Resources:L,点击统计 %>"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNum" CssClass="form-control text_md" runat="server">0</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><%=Resources.L.允许评论 %></td>
                            <td>
                                <asp:RadioButtonList ID="IsComm_Radio" runat="server" CssClass="pull-left" RepeatColumns="2">
                                    <asp:ListItem Value="1" Selected="True" Text="<%$Resources:L,是 %>"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="<%$Resources:L,否 %>"></asp:ListItem>
                                </asp:RadioButtonList>
                                <span class="pull-left rd_green">(<%=Resources.L.该操作只在节点管理下配置了允许发表评论时生效 %>!)</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><%=Resources.L.个性模板 %></td>
                            <td>
                                <%=PageCommon.GetTlpDP("TxtTemplate") %>
                                <asp:HiddenField ID="TxtTemplate_hid" runat="server" />

                            </td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><%=Resources.L.生成PDF文件 %></td>
                            <td>
                                <asp:CheckBox ID="Makepdf" runat="server" Text="<%$Resources:L,启用 %>" /></td>
                        </tr>
                    </table>
                    <ZL:TlpListView ID="TlpView_Tlp" IsFirstSelect="False" TemplateTitle="name" runat="server" />
                </div>
                <div class="tab-pane fade" id="Process">
                    <table class="table table-bordered">
                        <tr>
                            <td class="fd_td_l td_l">
                                <asp:Label ID="tj_txt" runat="server" Text="<%$Resources:L,推荐级别 %>"></asp:Label></td>
                            <td>
                                <asp:CheckBox ID="ChkAudit" runat="server" /><%=Resources.L.推荐 %></td>
                        </tr>
                        <tr id="contentsk" runat="server">
                            <td class="fd_td_l">
                                <asp:Label ID="zht_txt" runat="server" Text="<%$Resources:L,状态 %>"></asp:Label></td>
                            <td>
                                <asp:RadioButtonList ID="ddlFlow" runat="server" RepeatColumns="5">
                                    <%--<asp:ListItem Value="99" Selected="True">已审</asp:ListItem>
                            <asp:ListItem Value="0">待审</asp:ListItem>
                            <asp:ListItem Value="-1">退稿</asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane fade" id="Copyright">
                    <div class="alert alert-info">
                    <span><strong>提示：</strong></span>
                    未识别到文章ID,请保存后重编辑设定。
                    </div>
                </div>
            </div>
        </div>
        <div class="Conent_fix">
            <asp:HiddenField runat="server" ID="RelatedIDS_Hid" />
            <asp:Button runat="server" CssClass="btn btn-primary" ID="EBtnSubmit" Text="<%$Resources:L,添加项目 %>" OnClick="EBtnSubmit_Click" />
            <asp:Button runat="server" CssClass="btn btn-primary" ID="DraftBtn" Text="<%$Resources:L,存为草稿 %>" OnClick="DraftBtn_Click" />
            <a href="ContentManage.aspx?NodeID=<%=NodeID %>" class="btn btn-primary"><%=Resources.L.返回列表 %></a>
            <a href="javascript:;" id="toTop" onclick="Control.Scroll.ToTop();"><i class="fa fa-upload"></i>Top</a>
        </div>
        <div id="pushcon_div" class="alert alert-info">
            <div><%=Resources.L.保存时自动推送 %><a href="javascript:$('#pushcon_div').hide();;" title='<%=Resources.L.隐藏 %>'><span class="fa fa-remove"></span></a></div>
            <ul id="pushcon_ul" class="list-unstyled"></ul>
            <asp:HiddenField runat="server" ID="pushcon_hid" />
        </div>
        <ZL:TlpDown runat="server" />                  
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    var zlconfig = {
        updir: "<%=ZoomLa.Components.SiteConfig.SiteOption.UploadDir.ToLower()%>",
        duptitlenum: "<%=ZoomLa.Components.SiteConfig.SiteOption.DupTitleNum%>",
        modelid: "<%=ModelID%>",
        keys:<%=keys%>
        };
</script>
<script src="/JS/OAKeyWord.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/chinese.js"></script>
<script src="/JS/Common.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/Control.js"></script>
<script src="/JS/ICMS/tags.json"></script>
<script src="/JS/ZL_Content.js"></script>
<script>
    $(function () {
        Tlp_initTemp();
    });
    //智能模板选择事件
    function OnTemplateViewCheck(value) {
        $("#TxtTemplate_hid").val(value);
    }
</script>
</asp:Content>

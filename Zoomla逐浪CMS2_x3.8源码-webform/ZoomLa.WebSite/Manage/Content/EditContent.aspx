<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditContent.aspx.cs" Inherits="V_Content_EditContent" MasterPageFile="~/Manage/I/Default.master" ClientIDMode="Static" ValidateRequest="false" %>

<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<%@ Register Src="~/Manage/I/ASCX/TemplateView.ascx" TagPrefix="ZL" TagName="TlpListView" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <title>修改内容</title>
    <style>
    .bottom0{margin-bottom:0px;}
    #middle_td{vertical-align: middle;}
    </style>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">
    <div class="manage_content">
        <asp:HiddenField ID="nodename" runat="server" />
        <div class="tabbable">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#ConInfo" data-toggle="tab">内容信息</a></li>
                <li><a href="#InfoAttr" data-toggle="tab">信息属性</a></li>
                <li><a href="#Process " data-toggle="tab">流程控制</a></li>
                <li><a href="#Copyright " data-toggle="tab">版权管理</a></li>
            </ul>
            <div class="tab-content" style="margin-bottom: 40px;">
                <div class="tab-pane active" id="ConInfo">
                    <table class="table table-bordered table_padding0 addcontent_modeltale">
                        <tr>
                            <td style="overflow-x: hidden;" class="col-sm-1 col-xs-1 fd_td_l td_l">
                                <asp:Label ID="bt_txt" runat="server" Text="标题"></asp:Label></td>
                            <td class="col-sm-11 col-xs-11">
                                <asp:TextBox ID="txtTitle" Style="background: url(/Images/bg1.gif) repeat-x;" CssClass="form-control m715-50" onkeyup="isgoEmpty('txtTitle','span_txtTitle');Getpy('txtTitle','PYtitle')" runat="server" autofocus="true"></asp:TextBox>
                                <span id="span_txtTitle" name="span_txtTitle"></span><span><span class="vaild_tip">*</span></span>
                                <a href="javascript:;" id="Button3" onclick="ShowTitle()" class="btn btn-info btn-sm"><i class="fa fa-info"></i>标题属性</a>
                                <button type="button" class="btn btn-info btn-sm" onclick="ShowContentList()"><i class="fa fa-chain"></i>关联内容</button>
                                <button type="button" class="btn btn-info btn-sm" onclick="ShowSys();"><i class="fa fa-list"></i></button>
                                <asp:HiddenField ID="ThreadStyle" runat="server" />
                                <div id="duptitle_div" class="alert alert-warning" style="position: absolute; margin-left: 315px; display: none;">
                                    <ul id='duptitle_ul'></ul>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtTitle">内容标题必填</asp:RequiredFieldValidator></td>
                        </tr>
                        <tbody id="Sys_Fied" runat="server" style="display: none;">
                            <tr>
                                <td class="fd_td_l"><span>拼音缩写：</span></td>
                                <td>
                                    <asp:TextBox ID="PYtitle" runat="server" Text='' CssClass="form-control m715-50"></asp:TextBox></td>
                            </tr>
                            <tr id="spec_tr">
                                <td class="fd_td_l">所属专题：</td>
                                <td>
                                    <div class="specDiv"></div>
                                    <span id="specbtn_span">
                                        <asp:Literal ID="SpecInfo_Li" runat="server"></asp:Literal></span>
                                    <asp:HiddenField ID="Spec_Hid" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="fd_td_l">
                                    <asp:Label ID="gjz_txt" runat="server" Text="关键字"></asp:Label></td>
                                <td>
                                    <div id="OAkeyword" style="display: inline-block; min-width: 300px;"></div>
                                    <asp:TextBox ID="Keywords" runat="server" CssClass="form-control" />
                                    <asp:HiddenField runat="server" ID="IgnoreKey_Hid" />
                                    <span>(空格或回车键分隔，长度不超过10字符或5汉字)</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="fd_td_l">
                                    <asp:Label ID="SubTitle_L" runat="server" Text="副标题"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="Subtitle" runat="server" CssClass="form-control m715-50"></asp:TextBox></td>
                            </tr>
                            <tr>
                            </tr>
                        </tbody>
                        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
                        <tr>
                            <td class="fd_td_l">主编辑器扩展图：</td>
                            <td>
                                <ul id="ThumImg_ul" class="preview_img_ul">
                                    <div class="clearfix"></div>
                                </ul>
                                <asp:HiddenField runat="server" ID="ThumImg_Hid" />
                            </td>
                        </tr>
                        <tr id="CreateHTML" runat="server">
                            <td class="fd_td_l">生成</td>
                            <td>
                                <asp:CheckBox ID="quickmake" runat="server" Checked="true" Text="是否立即生成" /></td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane fade" id="InfoAttr">
                    <table class="table table-bordered table_padding0">
                        <tr>
                            <td class="fd_td_l td_l"><span>录入者</span></td>
                            <td>
                                <asp:TextBox ID="txtInputer" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><span>添加时间</span></td>
                            <td>
                                <asp:TextBox ID="txtAddTime" CssClass="form-control text_md" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><span>更新时间</span></td>
                            <td>
                                <asp:TextBox ID="txtdate" CssClass="form-control text_md" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><span>自动审核</span></td>
                            <td>
                                <asp:TextBox ID="CheckDate_T" CssClass="form-control text_md" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><span>自动过期</span></td>
                            <td>
                                <asp:TextBox ID="TimeDate_T" CssClass="form-control text_md" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="fd_td_l">
                                <asp:Label ID="hits_txt" runat="server" Text="点击数"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNum" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l">允许评论</td>
                            <td>
                                <asp:RadioButtonList ID="IsComm_Radio" runat="server" CssClass="pull-left" RepeatColumns="2">
                                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                                <span class="pull-left rd_green">(该操作只在节点管理下配置了允许发表评论时生效!)</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="fd_td_l"><span>个性模板</span></td>
                            <td>
                                <%=PageCommon.GetTlpDP("TxtTemplate") %>
                                <asp:HiddenField ID="TxtTemplate_hid" runat="server" />
                        </tr>
                        <tr id="outpdf">
                            <td class="fd_td_l">
                                <asp:Button runat="server" ID="makePDFBtn" Text="生成PDF" CssClass="btn btn-primary btn-xs" OnClick="makePDFBtn_Click" /></td>
                            <td>
                                <asp:LinkButton runat="server" ID="downPdfHref" ToolTip="点击下载" OnClick="downPdfBtn_Click" Visible="false" CssClass="btn btn-xs btn-info"><i class="fa fa-download"></i></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="delPdfHref" ToolTip="删除" OnClick="delPdfBtn_Click" OnClientClick="return confirm('确定要删除吗!')" Visible="false" CssClass="btn btn-xs btn-info"><i class="fa fa-trash-o"></i></asp:LinkButton></td>
                        </tr>
                        <tr id="outword">
                            <td class="fd_td_l">
                                <asp:Button runat="server" ID="makeWordBtn" Text="生成Word" CssClass="btn btn-primary btn-primary btn-xs" OnClick="makeWordBtn_Click" /></td>
                            <td>
                                <asp:LinkButton runat="server" ID="downWordHref" ToolTip="点击下载" OnClick="downWordBtn_Click" Visible="false" CssClass="btn btn-xs btn-info"><i class="fa fa-download"></i></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="delWordHref" ToolTip="删除" OnClick="delWordBtn_Click" OnClientClick="return confirm('确定要删除吗!')" Visible="false" CssClass="btn btn-xs btn-info"><i class="fa fa-trash-o"></i></asp:LinkButton></td>
                        </tr>
                    </table>
                    <ZL:TlpListView ID="TlpView_Tlp" IsFirstSelect="False" runat="server" TemplateTitle="name" />
                </div>
                <div class="tab-pane fade" id="Process">
                    <table class="table table-bordered">
                        <tr>
                            <td class="fd_td_l td_l">
                                <asp:Label ID="tj_txt" runat="server" Text="推荐级别"></asp:Label></td>
                            <td>
                                <asp:CheckBox ID="ChkAudit" Text="推荐" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l">
                                <asp:Label ID="zht_txt" runat="server" Text="状态"></asp:Label></td>
                            <td>
                                <asp:RadioButtonList ID="ddlFlow" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                                </asp:RadioButtonList></td>
                        </tr>

                    </table>
                </div>
                <div class="tab-pane fade" id="Copyright">
                    <table class="table table-bordered">
                        <tr>
                            <td id="middle_td" class="fd_td_l td_l">授权规则</td>
                            <td>
                                <div class="form-group text_300">
                                    <label class="sr-only" for="exampleInputAmount">Amount (in dollars)</label>
                                    <div class="input-group">
                                        <div class="input-group-addon">转载使用</div>
                                        <input type="text" class="form-control text-right" id="RepPri_T" placeholder="0-99元的整数">
                                        <div class="input-group-addon">.00</div>
                                    </div>
                                </div>
                                <div class="form-group text_300 bottom0">
                                    <label class="sr-only" for="exampleInputAmount">Amount (in dollars)</label>
                                    <div class="input-group">
                                        <div class="input-group-addon">素材使用</div>
                                        <input type="text" class="form-control text-right" id="MatPri_T" placeholder="0-99元的整数">
                                        <div class="input-group-addon">.00</div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="worksid_tr" class="hide">
                            <td class="fd_td_l">版权编号</td>
                            <td>
                                <label id="cr_worksid_l"></label>
                            </td>
                        </tr>
                        <tr id="workslink_tr" class="hide">
                            <td class="fd_td_l">售卖网址</td>
                            <td id="cr_worklink"></td>
                        </tr>
                        <tr>
                            <td class="fd_td_l">授权操作</td>
                            <td>
                                <asp:HiddenField runat="server" ID="worksID_F" />
                                <a href="javascript:void(0);" onclick="crhelper.add()" id="cradd_a" class="btn btn-primary disabled"><i class="fa fa-plus"></i>添加授权</a>
                                <a href="javascript:void(0);" onclick="crhelper.del()" id="crdel_a" class="btn btn-danger disabled"><i class="fa fa-trash"></i>暂停授权</a>
                                <a href="javascript:void(0);" onclick="crhelper.readd()" id="crreadd_a" class="btn btn-info disabled"><i class="fa fa-refresh"></i>重新授权</a>
                            </td>
                        </tr>
                    </table>
                    <div id="configcr" class="alert alert-info" style="display: none;">
                        <span><strong>提示：</strong></span>未配置版权印接口，若要使用版权功能，请 <a class="btn btn-info" href="../Config/PlatInfoList.aspx">前往配置>></a>
                    </div>
                </div>
            </div>
        </div>
        <div class="Conent_fix">
            <asp:HiddenField ID="RelatedIDS_Hid" runat="server" />
            <asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="保存项目" runat="server" OnClick="EBtnSubmit_Click" />
            <asp:Button ID="AddToNew_Btn" CssClass="btn btn-primary" Text="添加为新项目" runat="server" OnClick="AddToNew_Btn_Click" />
            <a href="javascript:history.go(-1);" class="btn btn-primary">返回列表</a>
            <a href="javascript:;" id="toTop" onclick="Control.Scroll.ToTop();"><i class="fa fa-upload"></i>Top</a>
            <asp:Button ID="BtnDoc" CssClass="btn btn-primary" runat="server" OnClientClick="return generate('docTitle','docParamValue');" Text="重新生成文档" Visible="false" />
            <%--      <asp:HiddenField ID="hfNode" runat="server" Value="1|选择节点," />--%>
        </div>
        <div id="pushcon_div" class="alert alert-info">
            <div>保存时自动推送<a href="javascript:$('#pushcon_div').hide();;" title='隐藏'><span class="fa fa-remove"></span></a></div>
            <ul id="pushcon_ul" class="list-unstyled"></ul>
            <asp:HiddenField runat="server" ID="pushcon_hid" />
        </div>
        <ZL:TlpDown runat="server" />
        <asp:HiddenField ID="CheckDate_Hid" runat="server" />
        <asp:HiddenField ID="TimeDate_Hid" runat="server" />
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
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/chinese.js"></script>
    <script src="/JS/Common.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/Control.js"></script>
    <script src="/JS/ZL_Content.js?v=20160514"></script>
    <script src="/JS/ICMS/tags.json"></script>
    <script src="/JS/OAKeyWord.js"></script>
    <script src="/JS/Modal/APIResult.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        $(function () {
            Tlp_initTemp();
            crhelper.init();
            ZL_Regex.B_Num("#RepPri_T,#MatPri_T");
            ZL_Regex.B_Value("#RepPri_T,#MatPri_T", {min: 0, max: 99, overmin: function () {}, overmax: function () {}});
        });
        //智能模板选择事件
        function OnTemplateViewCheck(value) {
            $("#TxtTemplate_hid").val(value);
        }
        var crhelper = { api: "/Manage/Copyright/cr.ashx", gid:"<%:GeneralID%>" };
    crhelper.init=function(){
        $.post(this.api+"?action=get",{ gid: crhelper.gid },function(data){
            var model = APIResult.getModel(data);
            if(APIResult.isok(model)){
                $("#workslink_tr,#worksid_tr").removeClass("hide");
                $("#RepPri_T").val(model.result.RepPrice);
                $("#MatPri_T").val(model.result.MatPrice);
                $("#cr_worksid_l").text(model.result.WorksID);
                var link = "http://www.banquanyin.com/"+model.result.WorksID;
                $("#cr_worklink").text("").append($("<a href='"+link+"' target='_blank' >"+link+"</a>"));
                $("#cradd_a").addClass("disabled");
                $("#crdel_a").removeClass("disabled");
                $("#crreadd_a").removeClass("disabled");
            }
            else{
                $("#RepPri_T,#MatPri_T").val("");
                $("#workslink_tr,#worksid_tr").addClass("hide");
                $("#cradd_a").removeClass("disabled");
                $("#crdel_a").addClass("disabled");
                $("#crreadd_a").addClass("disabled");
                if(model.retmsg=="未配置版权印接口"){
                    $("#cradd_a").addClass("disabled");
                    $("#configcr").css("display","block");
                }
            }
        });
    }
    crhelper.add = function(){
        var repprice = $("#RepPri_T").val();
        var matprice = $("#MatPri_T").val();
        $.post(this.api+"?action=add",{ gid: crhelper.gid, repprice: repprice, matprice: matprice },function(data){
            var model = APIResult.getModel(data);
            if(APIResult.isok(model)){
                crhelper.init();
                alert("添加成功，版权编号为:"+model.result);
            }else{ alert("操作失败:"+model.retmsg); }
        });
    }
    crhelper.del = function(){
        if(!confirm("确定要暂停授权吗")){return;}
        $.post(this.api+"?action=del",{ gid: crhelper.gid },function(data){
            var model = APIResult.getModel(data);
            if(APIResult.isok(model)){
                crhelper.init();
                alert("操作成功!");
            }else{ alert("操作失败:"+model.retmsg); }
        });
    }
    crhelper.readd = function(){
        var repprice = $("#RepPri_T").val();
        var matprice = $("#MatPri_T").val();
        var worksid = "";
        $.post(this.api+"?action=readd",{ id: crhelper.worksid, gid:"<%=Request.QueryString["GeneralID"] ?? ""%>", repprice: repprice, matprice: matprice },function(data){
            var model = APIResult.getModel(data);
            if(APIResult.isok(model)){
                crhelper.init();
                alert("操作成功!新的版权编号为:"+model.result);
            }else{ alert("操作失败:"+model.retmsg); }
        });
    }
    </script>
</asp:Content>

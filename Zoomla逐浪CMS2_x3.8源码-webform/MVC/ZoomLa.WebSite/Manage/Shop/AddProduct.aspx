<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.AddProduct" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.fd_td_l {width:120px; }
.proclass_tab {display:none;}
#Proname{background: url(/Images/bg1.gif) repeat-x;}
#cbind_btn{display:none;}
</style>
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<title>编辑商品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ul class="nav nav-tabs">
    <li class="active"><a href="#Tabs0" data-toggle="tab">基本信息</a></li>
    <li><a href="#Tabs1" data-toggle="tab">商品说明</a></li>
    <li><a href="#Tabs2" data-toggle="tab">库存厂税</a></li>
    <li><a href="#Tabs3" data-toggle="tab">价格运费</a></li>
    <li><a href="#Tabs4" data-toggle="tab">促销推广</a></li>
    <li><a href="#Tabs5" data-toggle="tab">详细属性</a></li>
</ul>
<div class="tab-content panel-body padding0">
    <div id="Tabs0" class="tab-pane active manage_content">
        <table class="table table-striped table-bordered">
                <tbody>
                    <tr>
                        <td class="td_m"><strong>所属节点：</strong></td>
                        <td>
                            <asp:Label ID="NodeName_L" runat="server"></asp:Label>
                            <asp:HiddenField runat="server" ID="ClickType" />
                            <asp:HiddenField runat="server" ID="Bind_Hid" />
                            <asp:HiddenField runat="server" ID="Group_Hid" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>商品编号：</strong></td>
                        <td>
                            <asp:TextBox ID="ProCode" runat="server" class="form-control text_md nofocus" /></td>
                    </tr>
                    <tr>
                        <td><strong>商品名称：</strong></td>
                        <td>
                            <asp:TextBox ID="Proname" runat="server" class="form-control text_500" onkeyup="isgoEmpty('Proname','span_Proname');"/>
                            <a href="javascript:;" title="捆绑销售" id="bind_btn" onclick="showbind();" class="btn btn-default"><i class="fa fa-plus"></i></a>
                            <a href="javascript:;" title="取消捆绑" id="cbind_btn" onclick="clearbind();" class="btn btn-default"><i class="fa fa-minus"></i></a>
                            <span class="rd_red">*<asp:RequiredFieldValidator ID="RV1" runat="server" ControlToValidate="Proname" Display="Dynamic" ErrorMessage="商品名称不能为空!" SetFocusOnError="True" /></span>
                            <span id="span_Proname"></span><span><span class="vaild_tip"></span></span>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>关键字：</strong></td>
                        <td>
                            <div id="OAkeyword" style="display: inline-block; min-width: 300px;"></div>
                            <asp:TextBox ID="Keywords" runat="server" CssClass="form-control" />
                            <asp:HiddenField runat="server" ID="IgnoreKey_Hid" />
                            <span>(空格或回车键分隔，长度不超过10字符或5汉字)</span></td>
                    </tr>
                    <tr>
                        <td><strong>商品类型：</strong></td>
                        <td>
                            <label><input type="radio" value="1" name="proclass_rad" onclick="proclass.switch(this.value);"/>正常商品</label>
                            <label><input type="radio" value="5" name="proclass_rad" onclick="proclass.switch(this.value);"/>虚拟商品</label>
                            <label><input type="radio" value="2" name="proclass_rad" onclick="proclass.switch(this.value);"/>特价</label>
                            <label><input type="radio" value="3" name="proclass_rad" onclick="proclass.switch(this.value);"/>积分</label>
                            <label><input type="radio" value="4" name="proclass_rad" onclick="proclass.switch(this.value);"/>团购</label>
                            <label><input type="radio" value="6" name="proclass_rad" onclick="proclass.switch(this.value);"/>IDC</label>
                            <label><input type="radio" value="7" name="proclass_rad" onclick="proclass.switch(this.value);"/>旅游</label>
                            <label><input type="radio" value="8" name="proclass_rad" onclick="proclass.switch(this.value);"/>酒店</label>
                            <asp:HiddenField ID="ProClass_Hid" runat="server" />
                            <div class="proclass_tab" id="proclass_tab6">
                                <table class="table table-bordered table-striped" style="width:400px;">
                                    <tr><td class="td_m">期限</td><td class="td_m">价格</td><td class="td_xs"><a href="javascript:;" class="btn btn-primary" onclick="idc.addrow();"><i class="fa fa-plus"></i></a></td></tr>
                                    <tbody id="idc_list">
                                        <tr><td><input type="text" class="form-control time" value="7天" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                        <tr><td><input type="text" class="form-control time" value="15天" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                        <tr><td><input type="text" class="form-control time" value="1月" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                        <tr><td><input type="text" class="form-control time" value="季度" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                        <tr><td><input type="text" class="form-control time" value="半年" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                        <tr><td><input type="text" class="form-control time" value="1年" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                        <tr><td><input type="text" class="form-control time" value="2年" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                        <tr><td><input type="text" class="form-control time" value="无限期" /></td><td><input type="text" class="form-control price"  value="0" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>
                                    </tbody>
                                </table>
                                <asp:HiddenField runat="server" ID="IDC_Hid" />
                                <div class="alert alert-info">价格或期限为空则不显示,支持(天|月|年|季度|无限期)</div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>多价格：</strong></td>
                        <td>
                            <table class="table table-bordered table-striped table-hover">
                                <tbody id="child_tb">
                                    <tr class="childtr">
                                        <td>
                                            <input type="hidden" name="C_code_hid" value="@code" />
                                            <span>品名：<input type="text" name="C_Proname_T" class="form-control text_md" value="@Proname" /></span>
                                            <span>价格：<input type="text" name="C_LinPrice_T" class="form-control text_md" value="@LinPrice" /></span>
                                            <span>零售价：<input type="text" name="C_ShiPrice_T" class="form-control text_md" value="@ShiPrice" /></span>
                                            <a href="javascript:;" class="btn btn-default addchild"><i class="fa fa-plus"></i></a>
                                            <a href="javascript:;" class="btn btn-default delchild"><i class="fa fa-minus"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:HiddenField runat="server" ID="ChildPro_Hid" />
                        </td>
                    </tr>
                    <tr id="bindpro_tr" hidden>
                        <td><strong>捆绑销售：</strong></td>
                        <td>
                            <div class="marginbot10">
                                <input type="button" class="btn btn-info" value="添加捆绑" onclick="OpenSelect();" />
                                <input type="button" class="btn btn-info" value="清空捆绑" onclick="ClearSelect();" />
                            </div>
                            <table id="bindpro_table" style="display: none;" class="table table-striped table-bordered">
                                <tr>
                                    <td>ID</td>
                                    <td>商品图片</td>
                                    <td>零售价</td>
                                    <td>商品名称</td>
                                </tr>
                                <tbody id="bindpro_body"></tbody>
                                <tr>
                                    <td colspan="5">
                                        <input type="button" class="btn btn-info" value="移除选定捆绑" onclick="RemoveBind();" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>自选数量：</strong></td>
                        <td>
                            <asp:TextBox ID="Propeid" runat="server" Text="0" class="form-control text_md" />
                            <span class="rd_green">(0为不允许自选购买;如果捆绑商品为空,则此数值为无效)</span>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>商品单位：</strong></td>
                        <td>
                            <asp:TextBox ID="ProUnit" runat="server" class="form-control text_md" Text="件" />
                            <span class="rd_red">
                                <asp:RequiredFieldValidator ID="RV2" runat="server" ControlToValidate="ProUnit" Display="Dynamic" ErrorMessage="商品单位不能为空!" SetFocusOnError="True" /></span>
                            <div id="Unitd" class="btn btn-group">
                                <button type="button" class="btn btn-default">件</button>
                                <button type="button" class="btn btn-default">个</button>
                                <button type="button" class="btn btn-default">只</button>
                                <button type="button" class="btn btn-default">组</button>
                                <button type="button" class="btn btn-default">套</button>
                                <button type="button" class="btn btn-default">把</button>
                                <button type="button" class="btn btn-default">双</button>
                                <button type="button" class="btn btn-default">台</button>
                                <button type="button" class="btn btn-default">年</button>
                                <button type="button" class="btn btn-default">月</button>
                                <button type="button" class="btn btn-default">日</button>
                                <button type="button" class="btn btn-default">季</button>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>商品重量：</strong></td>
                        <td>
                            <div class="text_md input-group">
                                <asp:TextBox ID="Weight" runat="server" class="form-control text_md" />
                                <span class="input-group-addon">千克</span>
                            </div>
                            <asp:RegularExpressionValidator ID="RV12" runat="server" ControlToValidate="Weight" ErrorMessage="商品重量必须是数字!" ValidationExpression="^(-?\d+)(\.\d+)?$" SetFocusOnError="True" Display="Dynamic" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>条形码：</strong></td>
                        <td>
                            <asp:TextBox ID="BarCode" runat="server" class="form-control text_300 num nofocus" /></td>
                    </tr>
                    <tr>
                        <td><strong>销售状态：</strong></td>
                        <td>
                            <asp:CheckBox ID="Sales_Chk" Text="打勾表示销售中，否则为停售状态" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><strong>属性设置：</strong></td>
                        <td>
                            <asp:CheckBox ID="istrue_chk" runat="server" Text="审核通过" Checked="true" />
                            <asp:CheckBox ID="isnew_chk" runat="server" Text="新品" />
                            <asp:CheckBox ID="ishot_chk" runat="server" Text="热销" />
                            <asp:CheckBox ID="isbest_chk" runat="server" Text="精品" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>售后设置：</strong></td>
                        <td>
                            <label>
                                <input type="checkbox" name="GuessXML" value="drawback" checked="checked" />
                                退货</label>
                            <label>
                                <input type="checkbox" name="GuessXML" value="exchange" />
                                换货</label>
                            <label>
                                <input type="checkbox" name="GuessXML" value="repair" />
                                维修</label>
                            <asp:HiddenField ID="restate_hid" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>会员特选：</strong></td>
                        <td>
                            <input type="checkbox" runat="server" id="Largess" class="switchChk" />
                            <span class="rd_green">(设置为特选商品后,只有指定会员才可购买)</span>
                        </td>
                    </tr>
                </tbody>
        </table>
    </div>
    <div id="Tabs1" class="tab-pane">
        <table class="table table-striped table-bordered">
            <tbody>
                    <tr>
                        <td class="td_l"><strong>商品简介：</strong></td>
                        <td>
                            <asp:TextBox ID="Proinfo" runat="server" TextMode="MultiLine"  class="form-control m715-50" style="height:70px;" placeholder="用于首页及栏目页显示，最多255个字符" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>详细介绍：</strong></td>
                        <td>
                            <textarea id="procontent" style="width: 715px; height: 300px;" runat="server"></textarea>
                            <%=Call.GetUEditor("procontent",3) %>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>商品清晰图：</strong></td>
                        <td>
                            <asp:TextBox ID="txt_Clearimg" runat="server" class="form-control text_300" />
                            <iframe id="bigimgs" style="top: 2px; width: 100%; height: 25px;" src="/Common/fileupload.aspx?FieldName=Clearimg&ModelID=<%:ModelID %>&NodeID=<%:NodeID %>" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                        </td>
                    </tr>
                    <tr id="addsmallimg">
                        <td><strong>商品缩略图：</strong></td>
                        <td>
                            <asp:TextBox ID="txt_Thumbnails" runat="server" class="form-control text_300" />
                            <iframe id="smallimgs" style="top: 2px; width: 100%; height: 25px;" src="/Common/fileupload.aspx?FieldName=Thumbnails&ModelID=<%:ModelID %>&NodeID=<%:NodeID %>" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>个性模板：</strong></td>
                        <td>
                            <%=PageCommon.GetTlpDP("ModeTemplate") %>
                            <asp:HiddenField ID="ModeTemplate_hid" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>点击数：</strong></td>
                        <td>
                            <asp:TextBox ID="AllClickNum_T" runat="server" class="form-control  text_300" Text="0" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>发布时间：</strong></td>
                        <td>
                            <asp:TextBox ID="AddTime" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" class="form-control  text_300" /></td>
                    </tr>
                    <tr>
                        <td><strong>更新时间：</strong></td>
                        <td>
                            <asp:TextBox ID="UpdateTime" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" class="form-control  text_300" /></td>
                    </tr>
                </tbody>
        </table>
    </div>
    <div id="Tabs2" class="tab-pane">
        <table class="table table-striped table-bordered">
            <tbody>
                    <tr>
                        <td class="td_m"><strong>生 产 商：</strong></td>
                        <td>
                            <div class="input-group text_300">
                                <asp:TextBox ID="Producer" runat="server" class="form-control text_300" />
                                <span class="input-group-btn">
                                    <input type="button" value="选择" class="btn btn-info" onclick="SelectProducer();" />
                                </span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>品牌/商标：</strong></td>
                        <td>
                            <div class="input-group text_300">
                                <asp:TextBox ID="Brand" runat="server" class="form-control text_300" />
                                <span class="input-group-btn">
                                    <input type="button" value="选择" class="btn btn-info" onclick="SelectBrand();" />
                                </span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>缺货时允许购买：</strong></td>
                        <td>
                            <asp:CheckBox ID="Allowed" runat="server" Text="打勾表示缺货时允许购买， 否则缺货时不允许购买" Checked="true" /></td>
                    </tr> 
                    <tr>
                        <td><strong>限购数量：</strong></td>
                        <td>
                            <label><input type="radio" name="Quota_Rad" value="0" onclick="$('#Quota_EGV').hide();" checked="checked"/>不设置</label>
                            <label><input type="radio" name="Quota_Rad" value="2" onclick="$('#Quota_EGV').show();"/>按会员组</label>
                            <table class="table table-bordered table-striped text_405" id="Quota_EGV" style="display:none;">
                                <tr><td class="td_l">会员组</td><td>数量(0为禁止,-1不限)</td></tr>
                                <asp:Repeater runat="server" ID="Quota_RPT" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr><td><label><%#Eval("GroupName") %></label></td><td><input type="text" class="form-control Quota_Group_T" data-gid="<%#Eval("GroupID") %>" value="<%#ZoomLa.Common.DataConverter.CLng(Eval("quota"),-1) %>" /></td></tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>最低购买数量：</strong></td>
                        <td>
                             <label><input type="radio" name="DownQuota_Rad" value="0" onclick="$('#DownQuota_EGV').hide();" checked="checked"/>不设置</label>
                             <label><input type="radio" name="DownQuota_Rad" value="2" onclick="$('#DownQuota_EGV').show();"/>按会员组</label>
                             <table class="table table-bordered table-striped text_405" id="DownQuota_EGV" style="display:none;">
                                <tr><td class="td_l">会员组</td><td>数量</td></tr>
                                <asp:Repeater runat="server" ID="DownQuota_RPT" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr><td><label><%#Eval("GroupName") %></label></td><td><input type="text" class="form-control DownQuota_Group_T" data-gid="<%#Eval("GroupID") %>" value="<%#ZoomLa.Common.DataConverter.CLng(Eval("downquota"),1) %>"/></td></tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>数量模式：</td>
                        <td>
                            <ZL:TextBox runat="server" ID="DownCar_T" CssClass="form-control text_300" ValidType="IntPostive" style="width:100px;"/><span class="rd_green">购买时必须为该数值的倍数</span>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>库存数量:</strong></td>
                        <td>
                            <%--<asp:HiddenField ID="Stock_Hid" runat="server" />--%>
                            <asp:TextBox ID="Stock" runat="server" Width="100px" class="form-control" ClientIDMode="Static" Text="10" />
                            <a href="javascript:;" id="SetStock_B" runat="server" visible="false" class="btn btn-info" onclick='SetStock();'>管理库存</a>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>库存报警下限：</strong></td>
                        <td>
                            <asp:TextBox ID="StockDown" runat="server" Width="100px" class="form-control">1</asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="StockDown" ErrorMessage="库存报警下限必须是数字!" Type="Integer" SetFocusOnError="True"/>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>前台库存计算方式:</strong></td>
                        <td>
                           <label><input type="radio" name="JisuanFs" value="0" checked="checked" />实际库存</label>
                           <label><input type="radio" name="JisuanFs" value="1"/>实际库存</label>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>税率设置:</strong></td>
                        <td>
                            <asp:RadioButtonList ID="Rateset" runat="server">
                                <asp:ListItem Selected="True" Value="1">含税，不开发票时有税率优惠 </asp:ListItem>
                                <asp:ListItem Value="2">含税，不开发票时没有税率优惠</asp:ListItem>
                                <asp:ListItem Value="3">不含税，开发票时需要加收税费</asp:ListItem>
                                <asp:ListItem Value="4">不含税，开发票时不需要加收税费</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>商品税率:</strong></td>
                        <td>
                            <div class="input-group text_s">
                                <asp:TextBox ID="Rate" runat="server" Text="0" class="form-control text_s num" />
                                <span class="input-group-addon">%</span>
                            </div>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="Rate" ErrorMessage="商品税率必须是数字!" ValidationExpression="^(-?\d+)(\.\d+)?$" SetFocusOnError="True" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>商品推荐等级:</strong></td>
                        <td>
                            <asp:DropDownList ID="Dengji" runat="server" CssClass="form-control text_md">
                                <asp:ListItem Value="5">★★★★★</asp:ListItem>
                                <asp:ListItem Value="4">★★★★</asp:ListItem>
                                <asp:ListItem Selected="True" Value="3">★★★</asp:ListItem>
                                <asp:ListItem Value="2">★★</asp:ListItem>
                                <asp:ListItem Value="1">★</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
        </table>
    </div>
    <div id="Tabs3" class="tab-pane">
        <table class="table table-striped table-bordered">
                <tbody>
                    <tr>
                        <td class="td_l"><strong>市场价格：<span class="rd_red">*</span></strong></td>
                        <td>
                            <div class="input-group text_s">
                                <asp:TextBox ID="ShiPrice" runat="server" Text="0" class="form-control text_s" autocomplete="off" /><span class="input-group-addon">元</span>
                            </div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="ShiPrice" ErrorMessage="价格格式不对!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ShiPrice" ErrorMessage="价格不能为空!" SetFocusOnError="True" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>售价：<span class="rd_red">*</span></strong></td>
                        <td>
                            <div class="input-group text_s">
                                <asp:TextBox ID="LinPrice" runat="server" class="form-control text_s" autocomplete="off" /><span class="input-group-addon">元</span>
                            </div>
                            <asp:RegularExpressionValidator ID="RV14" runat="server" ControlToValidate="LinPrice" ErrorMessage="零售价格式不对!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
                            <asp:RequiredFieldValidator ID="RV4" runat="server" ControlToValidate="LinPrice" Display="Dynamic" ErrorMessage="零售价不能为空!" SetFocusOnError="True" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>折扣优惠:</strong></td>
                        <td>
                            <div class="input-group text_s">
                                <asp:TextBox ID="Recommend_T" runat="server" Text="0" class="form-control text_s num" /><span class="input-group-addon">%</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>附加售价：</strong></td>
                        <td>
                            <div id="linprice_td">
                                <div class="input-group text_405">
                                    <span class="input-group-addon">余额</span><asp:TextBox ID="LinPrice_Purse_T" runat="server" class="form-control text_s curprice" Text="0" />
                                    <span class="input-group-addon">银币</span><asp:TextBox ID="LinPrice_Sicon_T" runat="server" class="form-control text_s curprice" Text="0" />
                                    <span class="input-group-addon">积分</span><asp:TextBox ID="LinPrice_Point_T" runat="server" class="form-control text_s curprice" Text="0" />
                                </div>
                                <asp:RegularExpressionValidator ID="RE3" runat="server" ControlToValidate="LinPrice_Purse_T" ErrorMessage="积分格式不对!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
                                <asp:RegularExpressionValidator ID="RV8" runat="server" ControlToValidate="LinPrice_Sicon_T" ErrorMessage="余额格式不对!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
                                <asp:RegularExpressionValidator ID="RE11" runat="server" ControlToValidate="LinPrice_Point_T" ErrorMessage="银币格式不对!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
                            </div>
                        </td>
                    </tr>
                    <tr><td><strong>区域价格:</strong></td><td>
                        <div class="input-group text_405">
                            <input type="text" id="region_skey_t" placeholder="请输入地区名称" class="form-control text_300" />
                            <span class="input-group-btn">
                                <a href="javascript:;" onclick="region.search();" class="btn btn-info"><i class="fa fa-search"></i></a>
                                <button class="btn btn-info" type="button" onclick="region.add()">设置多区域价格</button>
                            </span>
                        </div>
                            <div style="width:600px;max-height:300px;overflow-y:auto;">
                                <table class="table table-bordered table-striped margin_t5">
                                <tr><td>区域名称</td><td>会员组|价格</td><td>操作</td></tr>
                                <tbody id="region_div"></tbody>
                                </table>
                            </div>
                    </td></tr>
                    <tr>
                        <td><strong>会员价:</strong></td>
                        <td>
                            <label><input type="radio" value="0" name="UserPrice_Rad" onclick="$('.user_price_div').hide();" checked="checked"/>不设置</label>
                            <label><input type="radio" value="1" name="UserPrice_Rad" onclick="$('.user_price_div').hide(); $('#price_member_div').show();" />会员价</label>
                            <label><input type="radio" value="2" name="UserPrice_Rad" onclick="$('.user_price_div').hide(); $('#price_group_div').show();" />按会员组</label>
                            <span class="rd_green">为0则按原价计算</span>
                            <div id="price_member_div" runat="server" style="display: none;" class="input-group text_md user_price_div">
                                <span class="input-group-addon">会员价</span>
                                <asp:TextBox ID="Price_Member_T" runat='server' class="form-control" />
                                <span class="input-group-addon">元</span>
                            </div>
                            <div id="price_group_div" runat="server" style="display: none;" class="user_price_div">
                                <asp:Repeater ID="Price_Group_RPT" runat="server">
                                    <ItemTemplate>
                                        <div class="input-group text_300 margin_t5">
                                            <span class="input-group-addon" style="min-width:100px;"><%#Eval("GroupName")%></span>
                                            <input type="text" data-gid="<%#Eval("GroupID") %>" class="form-control Price_Group_T" value="<%#Eval("price") %>"></input>
                                            <span class="input-group-addon">元</span>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>运费设置:</strong></td>
                        <td>
                            <div>
                                <asp:RadioButtonList runat="server" ID="FareTlp_Rad" DataTextField="TlpName" DataValueField="ID" RepeatDirection="Horizontal" RepeatColumns="5"></asp:RadioButtonList>
                            </div>
                            <a href="DeliverType.aspx" class="btn btn-info">前往配置</a>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>预订价格:</strong></td>
                        <td>
                            <asp:TextBox ID="BookPrice_T" runat="server" class="form-control text_md" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="BookPrice_T" ErrorMessage="预订价格格式不正确!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>预订时间:</strong></td>
                        <td>
                            <asp:TextBox ID="BookDay_T" runat="server" class="form-control text_md" />
                            <span class="rd_green">节日时间提前N天为预订价</span>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="BookDay_T" ErrorMessage="预订价格格式不正确!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
                        </td>
                    </tr>
<%--                   
                    <tr>
                        <td><strong>礼品价格：</strong></td>
                        <td>
                            <div class="input-group text_s">
                                <asp:TextBox ID="Largesspirx" runat="server" Width="91px" class="form-control num">0</asp:TextBox>
                                <span class="input-group-addon">元</span>
                            </div>
                            <span class="rd_green">设置为礼品且允许单独销售时的价格</span>
                        </td>
                    </tr>--%>

                </tbody>
        </table>
    </div>
    <div id="Tabs4" class="tab-pane">
        <table class="table table-striped table-bordered">
                <tbody>
                    <tr>
                        <td class="td_l"><strong>促销方案：</strong></td>
                        <td>
                            <div id="projectTypeDiv">
                                <div>
                                    <asp:RadioButton runat="server" ID="ProjectType1_Rad" Checked="True" GroupName="ProjectType" />
                                    不促销</div>
                                <div>
                                    <asp:RadioButton runat="server" ID="ProjectType2_Rad" GroupName="ProjectType"/>
                                    买<asp:TextBox class="form-control text_xs" ID="ProjectPronum2" Text="1" runat="server" />件此商品 可以送一件同样商品</div>
                                <div>
                                    <asp:RadioButton runat="server" ID="ProjectType3_Rad" GroupName="ProjectType" />
                                    买<asp:TextBox Text="1" ID="ProjectPronum3" class="form-control text_xs" runat="server" />
                                        件此商品 可以送一件其他商品 赠品名称：<asp:TextBox ID="Productsname3" class="form-control text_md" runat="server" />
                                        <input type="button"  class="btn btn-info" value="浏览..." onclick="ProductsSelect('3');"/>
                                        <asp:HiddenField ID="HiddenField3" runat="server" /></div>
                                <div>
                                    <asp:RadioButton runat="server" ID="ProjectType4_Rad" GroupName="ProjectType" />
                                    送<asp:TextBox ID="ProjectPronum4" class="form-control text_xs" runat="server" Text="1" />件同样商品</div>
                                <div>
                                    <asp:RadioButton runat="server" ID="ProjectType5_Rad" GroupName="ProjectType" />
                                    送<asp:TextBox ID="ProjectPronum5" runat="server" class="form-control text_xs" Text="1" />
                                        件其他商品 送一件赠品名称：<asp:TextBox ID="Productsname5" class="form-control text_md" runat="server" />
                                        <asp:Button ID="Button7" runat="server" class="btn btn-info" Text="浏览..." OnClientClick="ProductsSelect('5');return false;" />
                                        <asp:HiddenField ID="HiddenField5" runat="server" /></div>
                                <div>
                                    <asp:RadioButton runat="server" ID="ProjectType6_Rad" GroupName="ProjectType" />
                                    加<asp:TextBox ID="ProjectMoney6" class="form-control text_xs" runat="server" Text="1" />
                                        元钱送商品 送一件赠品名称：<asp:TextBox ID="Productsname6" runat="server" class="form-control text_md" />
                                        <asp:Button ID="Button8" runat="server" class="btn btn-info" Text="浏览..." OnClientClick="ProductsSelect('6');return false;" />
                                        <asp:HiddenField ID="HiddenField6" runat="server" /></div>
                                <div>
                                    <asp:RadioButton runat="server" ID="ProjectType7_Rad" GroupName="ProjectType" />
                                    满<asp:TextBox ID="ProjectMoney7" runat="server" Text="1" class="form-control text_xs" />
                                        元钱送商品 送一件赠品名称：<asp:TextBox ID="Productsname7" class="form-control text_md" runat="server" />
                                        <asp:Button ID="Button5" class="btn btn-info" runat="server" Text="浏览..." OnClientClick="ProductsSelect('7');return false;" />
                                        <asp:HiddenField ID="HiddenField7" runat="server" /></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_m"><strong>其他促销方案：</strong></td>
                        <td>
                            <asp:Button ID="Button9" class="btn btn-info" runat="server" Text="绑定其他促销方案" Width="261px" OnClientClick="OpenprojectSelect();return false;" /><br />
                            <asp:ListBox ID="OtherProject" runat="server" Height="127px" Width="261px" SelectionMode="Multiple"></asp:ListBox><br />
                            <asp:Button ID="Button10" class="btn btn-info" runat="server" Text="删除选中方案" Width="260px" OnClientClick="Clearoption();return false;" /><br />
                            <span style="color: red;"><b>选中状态</b></span><span style="color: green;">的方案将被更新</span>支持<b>Ctrl</b>或<b>Shift</b>键多选
                        </td>
                    </tr>
                    <tr>
                        <td><strong>赠送积分：</strong></td>
                        <td>
                            <asp:TextBox ID="PointVal_T" runat="server" Text="0" class="form-control text_s" />
                            <asp:RegularExpressionValidator ID="REV2" runat="server" ControlToValidate="PointVal_T" ErrorMessage="积分值必须是数字!" ValidationExpression="^(-?\d+)(\.\d+)?$" SetFocusOnError="True" />
                        </td>
                    </tr>
                </tbody>
        </table>
    </div>
    <div id="Tabs5" class="tab-pane">
        <table class="table table-striped table-bordered">
                <tbody>
                    <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
                </tbody>
        </table>
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
<div style="height: 30px;"></div>
<div class="text-center Conent_fix">
    <input type="button" class="btn btn-primary" id="submit_btn" value="保存商品信息" onclick="PreSubmit();" />
    <asp:Button ID="EBtnSubmit" runat="server" OnClick="EBtnSubmit_Click" Style="display: none;" />
    <asp:Button ID="btnAdd" class="btn btn-primary" Text="添加为新商品" runat="server" OnClick="btnAdd_Click" />
    <a href="ProductManage.aspx?<%:"NodeID="+NodeID+"&StoreID="+StoreID%>" class="btn btn-primary">返回列表<i class="fa fa-arrow-circle-up"></i></a>
</div>
<ZL:TlpDown runat="server" EnableViewState="false" />
<input type="hidden" id="Price_Group_Hid" name="Price_Group_Hid" />
<input type="hidden" id="Quota_Group_Hid" name="Quota_Group_Hid" />
<input type="hidden" id="DownQuota_Group_Hid" name="DownQuota_Group_Hid"/>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/dist/js/bootstrap-switch.js"></script>
<script src="/JS/Common.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/ZL_Content.js"></script>
<script src="/JS/Modal/shop.js?v=3"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/JS/OAKeyWord.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script>
    var diag = new ZL_Dialog();
    //捆绑商品操作
    function BindPro(projson) {
        if (projson) { $("#Bind_Hid").val(projson); }
        if ($("#Bind_Hid").val() == "") { $("#bindpro_body").html(""); return; }
        $(".td_binddata").remove();//清空之前捆绑数据
        var proattr = [];
        try { proattr = JSON.parse($("#Bind_Hid").val()); } catch (ex) { }
        var trtlp = "<tr class='td_binddata'><td class='td_s'><label><input type='checkbox' name='bind_chk' value='@id'>@id</label></td><td class='td_m'><img onerror=\"shownopic(this);\" style='width:60px;height:45px;' src='/@Thumbnails' /></td><td class='td_m'>@LinPrice</td><td>@Proname</td></tr>";
        var html = JsonHelper.FillData(trtlp, proattr);
        $("#bindpro_body").append(html);
        $("#bindpro_table").show();
        showbind();
    }
    //移除选定捆绑
    function RemoveBind() {
        var $chks = $("input[name=bind_chk]:checked");
        if ($chks.length < 1) { alert("请先选中需要清除的捆绑"); return false; }
        var bindArr = JSON.parse($("#Bind_Hid").val());
        $chks.each(function () {
            bindArr.RemoveByID($(this).val());
        });
        if (bindArr.length < 1) { $("#Bind_Hid").val(""); }
        else { $("#Bind_Hid").val(JSON.stringify(bindArr)); }
        BindPro();
    }
    function OpenSelect() {
        diag.title = "添加捆绑商品";
        diag.maxbtn = false;
        diag.width = "none";
        diag.url = "/Common/AddbindPro.aspx?id=<%:ProID %>&filter=admin";
    diag.ShowModal();
    return false;
}
function ClearSelect() {
    $("#Bind_Hid").val("");
    $("#bindpro_body").html("");
    $("#bindpro_table").hide();
}
function showbind() {
    $("#bindpro_tr").show();
    $("#bind_btn").hide();
    $("#cbind_btn").show();
}
function clearbind() {
    ClearSelect();
    $("#bindpro_tr").hide();
    $("#bind_btn").show();
    $("#cbind_btn").hide();
}
//选择
function SelectProducer() {
    window.open('Producerlist.aspx', '', 'width=600,height=450,resizable=0,scrollbars=yes');
}
function ProductsSelect(act) {
    window.open('ProductsSelect.aspx?act=' + act, '', 'width=600,height=450,resizable=0,scrollbars=yes');
}
function OpenprojectSelect() {
    window.open('projectSelect.aspx', '', 'width=600,height=450,resizable=0,scrollbars=yes');
    return false;
}
function SelectBrand() {
    window.open('Brandlist.aspx?producer=' + document.getElementById('Producer').value + '', '', 'width=600,height=450,resizable=0,scrollbars=yes');
}
function CloseDiag() {
    diag.CloseModal();
    comdiag.CloseModal();
}

function SetStock(pid) {
    diag.url = "Stock.aspx?action=addpro&pid=<%=ProID %>";
    diag.title = "库存管理";
    diag.maxbtn = false;
    diag.reload = true;
    diag.backdrop = true;
    diag.ShowModal();
}
function addStock(pronum) {
    CloseDiag();
    var num = parseInt($("#Stock").val());
    $("#Stock").val(pronum + num);
}
//-------------------------------------------------------
var group = { list: JSON.parse($("#Group_Hid").val()) }
group.getName = function (gid) {
    var model = group.list.GetByID(gid, "GroupID");
    if (!model) { return "未匹配"; }
    else { return model.GroupName; }
}
//--------------------多区域价格(增加筛选搜索,增加为0不显示)
var region = { guid: "<%:ProGuid%>" };
region.fill = function (list) {
    var tlp = '<tr><td>@region</td><td class="price_td"></td><td>';
    tlp += '<a href="javascript:;" onclick="region.edit(\'@region\');" class="btn btn-xs btn-info">修改</a>';
    tlp += '<a href="javascript:;" onclick="region.del(this,\'@region\');" class="btn btn-xs btn-info" style="margin-left:5px;">删除</a>';
    tlp += '</td></tr>';
    var priceTlp = "<div><span>@gname</span> | <span>@price</span></div>";
    $div = $("#region_div"); $div.html("");
    $items = JsonHelper.FillItem(tlp, list, function ($item, model) {
        for (var i = 0; i < model.price.length; i++) {
            model.price[i]["gname"] = group.getName(model.price[i].gid);
        }
        var priceItems = JsonHelper.FillItem(priceTlp, model.price);
        $item.find(".price_td").append(priceItems);
    });
    $div.append($items);
}
region.add = function () {
    comdiag.reload = true;
    ShowComDiag("RegionPrice.aspx?guid=<%=ProGuid %>", "多区域价格");
}
region.edit = function (region) {
    ShowComDiag("EditRegionPrice.aspx?guid=" + this.guid + "&region=" + escape(region), "区域价格管理");
}
region.del = function (obj, region) {
    $(obj).closest("tr").remove();
    $.post("RegionPrice.aspx?action=del&guid=" + this.guid + "&region=" + escape(region), {}, function (data) {
        var model = APIResult.getModel(data);
        if (!APIResult.isok(model)) { console.log(model.retmsg); }
    });
}
region.search = function () {
    var skey = $("#region_skey_t").val();
    if (ZL_Regex.isEmpty(skey)) { $("#region_div tr").show(); }
    else {
        $("#region_div tr").hide();
        $("#region_div tr:contains('" + skey + "')").show();
    }
}
</script>
</asp:Content>

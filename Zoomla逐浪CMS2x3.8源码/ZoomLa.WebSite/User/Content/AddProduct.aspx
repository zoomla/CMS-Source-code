<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="User_Content_AddProduct" MasterPageFile="~/User/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
<script src="/dist/js/bootstrap-switch.js"></script>
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<title>编辑商品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div> 
<div class="container margin_t5">
<ol class="breadcrumb">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li><a href="ProductList.aspx">我的店铺</a></li>
    <li class="active">添加商品</li> 
</ol>
</div>
<div class="container btn_green">
<ul class="nav nav-tabs">
    <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabss(0)">基本信息</a></li>
    <li><a href="#tab1" data-toggle="tab" onclick="ShowTabss(1)">商品说明</a></li>
    <li><a href="#tab2" data-toggle="tab" onclick="ShowTabss(2)">库存厂税</a></li>
    <li><a href="#tab3" data-toggle="tab" onclick="ShowTabss(3)">价格运费</a></li>
    <li><a href="#tab4" data-toggle="tab" onclick="ShowTabss(4)">促销推广</a></li>
    <li><a href="#tab5" data-toggle="tab" onclick="ShowTabss(5)">详细属性</a></li>
</ul>
<table class="table table-striped table-bordered">
    <tbody id="Tabs0" style="border-top: none;">
        <tr>
            <td class="td_m"><strong>所属节点：</strong></td>
            <td>
                <asp:Label ID="NodeName_L" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="ClickType" />
                <asp:HiddenField runat="server" ID="Bind_Hid" />
            </td>
        </tr>
        <tr>
            <td><strong>所属专题：</strong></td>
            <td>
                <asp:Label ID="Categoryname" runat="server"></asp:Label>
                <div id="lblSpec" runat="server"></div>
                <input type="button" id="Button1" value="添加到专题" class="btn btn-info" onclick="AddToSpecial()" />
                <asp:HiddenField ID="Categoryid" runat="server" />
            </td>
        </tr>
        <tr>
            <td><strong>商品编号：</strong></td>
            <td><asp:TextBox ID="ProCode" runat="server" class="form-control text_md" /></td>
        </tr>
        <tr>
            <td><strong>条形码：</strong></td>
            <td><asp:TextBox ID="BarCode" runat="server" class="form-control text_300 num" /></td>
        </tr>
        <tr>
            <td><strong>商品名称：</strong></td>
            <td>
                <asp:TextBox ID="Proname" runat="server" class="form-control text_300" />
                <span class="rd_red">*<asp:RequiredFieldValidator ID="RV1" runat="server" ControlToValidate="Proname" Display="Dynamic" ErrorMessage="商品名称不能为空!" SetFocusOnError="True" /></span>
            </td>
        </tr>
        <tr>
            <td><strong>关键字：</strong></td>
            <td><asp:TextBox ID="Kayword" runat="server" class="form-control text_300" /></td>
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
        <tr>
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
                        <td colspan="5"><input type="button" class="btn btn-primary" value="移除选定捆绑" onclick="RemoveBind();" /></td>
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
                <span class="rd_red"><asp:RequiredFieldValidator ID="RV2" runat="server" ControlToValidate="ProUnit" Display="Dynamic" ErrorMessage="商品单位不能为空!" SetFocusOnError="True" /></span>
                <div id="Unitd" class="btn btn-group"></div>
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
            <td><strong>商品类型：</strong></td>
            <td>
                <input type="radio" id="normal" checked="checked" value="1" name="ProClass" /><label for="normal">正常销售</label>
                <input type="radio" id="TeJia" value="2" name="ProClass" /><label for="TeJia">特价处理</label>
                <input type="radio" id="JiFen" value="3" name="ProClass" /><label for="JiFen">积分商品</label>
                <input type="radio" id="Tuan" value="4" name="ProClass" /><label for="Tuan">团购</label>
                <input type="radio" id="DaiMai" value="5" name="ProClass" /><label for="YG">云购</label>
                <input type="radio" id="IDC" value="6" name="ProClass" /><label for="IDC">IDC商品</label>
                <input type="radio" id="LvYou" value="7" name="ProClass" /><label for="LvYou">旅游</label>
                <input type="radio" id="Hotel" value="8" name="ProClass" /><label for="Hotel">酒店</label>
                <asp:HiddenField ID="ProClass" runat="server" />
                <div id="TG">
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <table id="tgTable" class="classdiv">
                                <tr>
                                    <td>团购订金：</td>
                                    <td>
                                        <asp:TextBox ID="txtColoneDeposit" runat="server" class="form-control" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtColoneDeposit" ErrorMessage="团购订金格式不对!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>最低团购人数：</td>
                                    <td><asp:TextBox ID="NumberText" runat="server" class="form-control text_s" /><span>人</span></td>
                                    <td>团购价格:</td>
                                    <td><asp:TextBox ID="TGPrice" runat="server" class="form-control text_s" /></td>
                                </tr>
                                <tr>
                                    <td>开始时间</td>
                                    <td><asp:TextBox ID="ColonelStartTimetxt" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" class="form-control" /></td>
                                    <td>结束时间:</td>
                                    <td>
                                        <asp:TextBox ID="ColonelendTimetxt" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" class="form-control" />
                                        <asp:HiddenField ID="hfEndTime" runat="server" />
                                        <asp:HiddenField ID="hfBeginTime" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="text-center">
                                        <asp:Button ID="Button14" class="btn btn-info td_m" runat="server" Text="添加" CausesValidation="false" OnClick="Button14_Click" />
                                        <asp:Button ID="Button15" class="btn btn-info td_m" runat="server" Text="浏览价格" CausesValidation="false" OnClientClick="OpenSelect2();return false;" />
                                        <asp:Button ID="TGButtion" class="btn btn-primary td_m" runat="server" Text="取消" CausesValidation="false" OnClick="TGButtion_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </div>
                <table id="idcTable" class="classdiv">
                    <tr>
                        <td><strong>服务期限：</strong></td>
                        <td>
                            <asp:TextBox ID="ServerPeriod" runat="server" class="form-control text_xs" />
                            <asp:DropDownList ID="ServerType" CssClass="form-control text_x" runat="server">
                                <asp:ListItem Selected="True" Value="3">年</asp:ListItem>
                                <asp:ListItem Value="2">月</asp:ListItem>
                                <asp:ListItem Value="1">日</asp:ListItem>
                            </asp:DropDownList><span class="rd_green">每次购买,充值多长时间</span>
                            <asp:RegularExpressionValidator ID="reg11" runat="server" ControlToValidate="ServerPeriod" ErrorMessage="服务期限必须是数字!" ValidationExpression="^\d+$" SetFocusOnError="True" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>到期提醒：</strong></td>
                        <td>
                            <asp:DropDownList runat="server" ID="expRemindDP" CssClass="form-control text_x">
                                <asp:ListItem Value="1">1天</asp:ListItem>
                                <asp:ListItem Value="3">3天</asp:ListItem>
                                <asp:ListItem Value="5">5天</asp:ListItem>
                                <asp:ListItem Value="10">10天</asp:ListItem>
                                <asp:ListItem Value="15">15天</asp:ListItem>
                                <asp:ListItem Value="30">1月</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><strong>销售状态：</strong></td>
            <td><asp:CheckBox ID="Sales_Chk" Text="打勾表示销售中，否则为停售状态" runat="server" /></td>
        </tr>
        <tr>
            <td><strong>属性设置：</strong></td>
            <td>
                <asp:CheckBox ID="istrue_Chk" runat="server" Text="审核通过" Checked="true" />
                <asp:CheckBox ID="isnew" runat="server" Text="新品" />
                <asp:CheckBox ID="ishot" runat="server" Text="热销" />
                <asp:CheckBox ID="isbest" runat="server" Text="精品" />
            </td>
        </tr>
        <tr>
            <td><strong>售后设置：</strong></td>
            <td>
                <label><input type="checkbox" name="GuessXML" value="drawback" checked="checked" /> 退货</label>
                <label><input type="checkbox" name="GuessXML" value="exchange" /> 换货</label>
                <label><input type="checkbox" name="GuessXML" value="repair" /> 维修</label>
                <asp:HiddenField ID="restate_hid" runat="server" />
            </td>
        </tr>
    </tbody>
    <tbody id="Tabs1" style="border-top: none; display: none;">
        <tr>
            <td class="td_l"><strong>商品简介：</strong></td>
            <td>
                用于首页及栏目页显示，最多255个字符<br />
                <asp:TextBox ID="Proinfo" runat="server" Height="87px" TextMode="MultiLine" Width="500px" class="form-control" />
            </td>
        </tr>
        <tr>
            <td><strong>详细介绍：</strong></td>
            <td>
                <textarea id="Procontent" style="width: 715px; height: 300px;" runat="server"></textarea>
                <%=Call.GetUEditor("Procontent",3) %>
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
                <asp:TextBox ID="txtCountHits" runat="server" class="form-control  text_300" Text="0" />
            </td>
        </tr>
        <tr>
            <td><strong>更新时间：</strong></td>
            <td><asp:TextBox ID="UpdateTime" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" class="form-control  text_300" /></td>
        </tr>
    </tbody>
    <tbody id="Tabs2" style="border-top: none; display: none">
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
            <td><strong>缺货时允许购买:</strong></td>
            <td><asp:CheckBox ID="Allowed" runat="server" Text="打勾表示缺货时允许购买， 否则缺货时不允许购买" Checked="true" /></td>
        </tr>
        <tr>
            <td><strong>限购数量:</strong></td>
            <td>
                <asp:TextBox ID="Quota" runat="server" Width="100px" class="form-control">-1</asp:TextBox>
                <span class="rd_green">-1为不限制数量 0为不允许购买</span>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Quota" ErrorMessage="限购数量必须是数字!" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td><strong>最低购买数量:</strong></td>
            <td>
                <asp:TextBox ID="DownQuota" runat="server" Width="100px" class="form-control">-1</asp:TextBox>
                <span class="rd_green">-1为不限制数量 0为不允许购买</span>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DownQuota" ErrorMessage="最低购买数量必须是数字!" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td><strong>秒杀时间段:(限制购买时间)</strong></td>
            <td>
                <asp:TextBox ID="TextBox13" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" runat="server" class="form-control text_md" placeholder="开始时间" />
                <asp:TextBox ID="TextBox14" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" runat="server" class="form-control text_md" placeholder="结束时间" />
            </td>
        </tr>
        <tr>
            <td><strong>库存数量:</strong></td>
            <td>
                <asp:TextBox ID="Stock" runat="server" Width="100px" class="form-control" ClientIDMode="Static">10</asp:TextBox>
                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="Stock" ErrorMessage="库存数量必须是数字!" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td><strong>库存报警下限:</strong></td>
            <td style="width: 592px;">
                <asp:TextBox ID="StockDown" runat="server" Width="100px" class="form-control">1</asp:TextBox>
                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="StockDown" ErrorMessage="库存报警下限必须是数字!" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td><strong>前台库存计算方式:</strong></td>
            <td>
                <asp:RadioButtonList ID="JisuanFs" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">实际库存</asp:ListItem>
                    <asp:ListItem Value="1">虚拟库存</asp:ListItem>
                </asp:RadioButtonList>
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
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="Rate" ErrorMessage="商品税率必须是数字!" ValidationExpression="^(-?\d+)(\.\d+)?$" SetFocusOnError="True" />
                </div>
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
    <tbody id="Tabs3" style="border-top: none; display: none">
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
                    <asp:TextBox ID="txtRecommend" runat="server" Text="0" class="form-control text_s num" /><span class="input-group-addon">%</span>
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
        <tr>
            <td><strong>会员价:</strong></td>
            <td>
                <label><input type="radio" runat="server" value="0" groupname="UserPrice" name="UserPrice_Rad" id="UserPrice0_Rad" onclick="$('.user_price_div').hide();"/>不设置</label>
                <label><input type="radio" runat="server" value="1" groupname="UserPrice" name="UserPrice_Rad" id="UserPrice1_Rad" onclick="$('.user_price_div').hide(); $('#price_member_div').show();" />会员价</label>
                <label><input type="radio" runat="server" value="2" groupname="UserPrice" name="UserPrice_Rad" id="UserPrice2_Rad" onclick="$('.user_price_div').hide(); $('#price_group_div').show();" />按会员组</label>
                <div id="price_member_div" runat="server" style="display: none;" class="input-group text_md user_price_div">
                    <span class="input-group-addon">会员价</span>
                    <asp:TextBox ID="Price_Member_T" runat='server' class="form-control" />
                    <span class="input-group-addon">元</span>
                </div>
                <asp:RegularExpressionValidator ID="RV9" runat="server" ControlToValidate="Price_Member_T" ErrorMessage="会员价格格式不正确!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                <div id="price_group_div" runat="server" style="display: none;" class="user_price_div">
                    <asp:Repeater ID="Price_Group_RPT" runat="server">
                        <ItemTemplate>
                            <div class="input-group text_md margin_t5">
                                <span class="input-group-addon"><%#Eval("GroupName")%></span>
                                <asp:TextBox runat="server" ID="Price_Group_T" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">元</span>
                                <asp:HiddenField ID="GroupID_Hid" runat="server" Value='<%#Eval("GroupID") %>' />
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
            <td><strong>节日价格:</strong></td>
            <td>
                <asp:TextBox ID="txtDayPrice" runat="server" class="form-control text_md" />
                <asp:RegularExpressionValidator ID="rv3" runat="server" ControlToValidate="txtDayPrice" ErrorMessage="节日价格格式不正确!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True"/>
            </td>
        </tr>
        <tr>
            <td><strong>节日时间:</strong></td>
            <td>
                <div id='hotelDiv'>
                    <asp:TextBox ID='CheckInDate' runat="server" class="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" />至
                    <asp:TextBox ID='CheckOutDate' runat="server" class="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" />
                </div>
                <input id='CheckOut' type='hidden' runat="server" />
                <asp:HiddenField ID="serverdate" runat="server" Value='2010-8-31' />
                <div id='m_contentend'></div>
            </td>
        </tr>
        <tr>
            <td><strong>预订价格:</strong></td>
            <td>
                <asp:TextBox ID="txtBookPrice" runat="server" class="form-control text_md" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtBookPrice" ErrorMessage="预订价格格式不正确!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr>
            <td><strong>预订时间:</strong></td>
            <td>
                <asp:TextBox ID="txtBookDay" runat="server" class="form-control text_md" />
                <span class="rd_green">节日时间提前N天为预订价</span>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtBookDay" ErrorMessage="预订价格格式不正确!" ValidationExpression="\d+[.]?\d*" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr>
            <td><strong>单独销售：</strong></td>
            <td><input type="checkbox" runat="server" id="Wholesaleone" class="switchChk" checked="checked" /></td>
        </tr>
        <tr>
            <td><strong>设置为礼品：</strong></td>
            <td>
                <input type="checkbox" runat="server" id="Largess" class="switchChk" checked="checked" />
                <span class="rd_green">(自选数量大于0时设为礼品无效)</span>
            </td>
        </tr>
        <tr>
            <td><strong>礼品价格：</strong></td>
            <td>
                <div class="input-group text_s">
                    <asp:TextBox ID="Largesspirx" runat="server" Width="91px" class="form-control num">0</asp:TextBox> 
                    <span class="input-group-addon">元</span>
                </div>
                <span class="rd_green">设置为礼品且允许单独销售时的价格</span>
            </td>
        </tr>
    </tbody>
    <tbody id="Tabs4" style="border-top: none; display: none">
        <tr>
            <td class="td_l"><strong>促销方案：</strong></td>
            <td>
                <table class="table">
                    <tr>
                        <td rowspan="7" width="39px">
                            <asp:RadioButtonList ID="ProjectType" runat="server" Width="39px" CellSpacing="0" CellPadding="0">
                                <asp:ListItem Value="1" Selected="True" style="line-height: 26px"></asp:ListItem>
                                <asp:ListItem Value="2" style="line-height: 25px"></asp:ListItem>
                                <asp:ListItem Value="3" style="line-height: 25px"></asp:ListItem>
                                <asp:ListItem Value="4" style="line-height: 26px"></asp:ListItem>
                                <asp:ListItem Value="5" style="line-height: 26px"></asp:ListItem>
                                <asp:ListItem Value="6" style="line-height: 26px">6</asp:ListItem>
                                <asp:ListItem Value="7" style="line-height: 27px">7</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>不促销</td>
                    </tr>
                    <tr>
                        <td>买<asp:TextBox class="form-control text_xs" ID="ProjectPronum2" Text="1" runat="server" />件此商品 可以送一件同样商品</td>
                    </tr>
                    <tr>
                        <td>
                            买<asp:TextBox Text="1" ID="ProjectPronum3" class="form-control text_xs" runat="server" />
                            件此商品 可以送一件其他商品 赠品名称：<asp:TextBox ID="Productsname3" class="form-control text_md" runat="server" />
                            <asp:Button ID="Button6" class="btn btn-info" runat="server" Text="浏览..." OnClientClick="ProductsSelect('3');return false;" />
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>送<asp:TextBox ID="ProjectPronum4" class="form-control text_xs" runat="server" Text="1" />件同样商品</td>
                    </tr>
                    <tr>
                        <td>
                            送<asp:TextBox ID="ProjectPronum5" runat="server" class="form-control text_xs" Text="1" />
                            件其他商品 送一件赠品名称：<asp:TextBox ID="Productsname5" class="form-control text_md" runat="server" />
                            <asp:Button ID="Button7" runat="server" class="btn btn-info" Text="浏览..." OnClientClick="ProductsSelect('5');return false;" />
                            <asp:HiddenField ID="HiddenField5" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            加<asp:TextBox ID="ProjectMoney6" class="form-control text_xs" runat="server" Text="1" />
                            元钱送商品 送一件赠品名称：<asp:TextBox ID="Productsname6" runat="server" class="form-control text_md" />
                            <asp:Button ID="Button8" runat="server" class="btn btn-info" Text="浏览..." OnClientClick="ProductsSelect('6');return false;" />
                            <asp:HiddenField ID="HiddenField6" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            满<asp:TextBox ID="ProjectMoney7" runat="server" Text="1" class="form-control text_xs" />
                            元钱送商品 送一件赠品名称：
                            <asp:TextBox ID="Productsname7" class="form-control text_md" runat="server" />
                            <asp:Button ID="Button5" class="btn btn-info" runat="server" Text="浏览..." OnClientClick="ProductsSelect('7');return false;" />
                            <asp:HiddenField ID="HiddenField7" runat="server" />
                        </td>
                    </tr>
                </table>
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
            <td class="td_m"><strong>购物积分：</strong></td>
            <td>
                购买<asp:TextBox ID="IntegralNum" runat="server" Text="1" class="form-control text_xs" />
                件此商品可以得到<asp:TextBox ID="Integral" runat="server" Width="53px" class="form-control" MaxLength="3" /><span>积分</span>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="Integral" ErrorMessage="积分只能是数字或小数!" ValidationExpression="^\d+\.?\d+$|^\d+$" />
            </td>
        </tr>
         <tr>
            <td><strong>积分:</strong></td>
            <td>
                <asp:TextBox ID="txtPoint" runat="server" Text="0" class="form-control text_s" /><span class="rd_green">仅用于计算提成</span>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPoint" ErrorMessage="积分值必须是数字!" ValidationExpression="^(-?\d+)(\.\d+)?$" SetFocusOnError="True" />
            </td>
        </tr>
    </tbody>
    <tbody id="Tabs5" style="border-top: none; display: none">
        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
    </tbody>
</table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
<div class="text-center">
    <input type="button" class="btn btn-primary" id="submit_btn" value="保存商品信息" onclick="PreSubmit();" />
    <asp:Button ID="EBtnSubmit" runat="server" OnClick="EBtnSubmit_Click" Style="display: none;" />
    <asp:Button ID="btnAdd" class="btn btn-primary" Text="添加为新商品" runat="server" OnClick="btnAdd_Click" />
    <a href="ProductManage.aspx?<%:"NodeID="+NodeID+"&StoreID="+StoreID%>" class="btn btn-primary" id="Button2">返回列表<i class="fa fa-arrow-circle-up"></i></a>
</div>
<ZL:TlpDown runat="server" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Common.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Modal/shop.js"></script>
<script src="/JS/ZL_Content.js"></script>
<script type="text/JavaScript">
    var diag = new ZL_Dialog();

    $(function () {
        ZL_Regex.B_Num(".num");
        SelUnit();
        if($("#ProClass").val()=="")SelShopType($("#normal").val());
        $("input[name=ProClass]").each(function(i,v){
            if ($(v).val()==$("#ProClass").val()) {
                v.checked=true;
                SelShopType($(v).val());
                return false;
            }
        });
        $("input[name=ProClass]").click(function(){
            SelShopType($(this).val());
        });
        Tlp_initTemp();
        BindPro();//旅游订单不显示
        //售后设置属性
        var revalues = $("#restate_hid").val().split(',');
        if (revalues.length > 0) {
            $("input[name='GuessXML']").each(function () { this.checked = false; })
            for (var i = 0; i < revalues.length; i++) {
                if (!ZL_Regex.isEmpty(revalues[i]))
                {
                    var chk = $("input[name=GuessXML][value=" + revalues[i] + "]")[0];
                    if (chk) { chk.checked = true; }
                }
            }
        }
    });
    function SelShopType(value) {
        $(".classdiv").hide();
        switch (value) {
            case '4':
                $("#tgTable").show();
                break;
            case '6':
                $("#idcTable").show();
                break;
            default:
                break;
        }
    }
    function SelUnit(){
        var units= "件,个,只,组,套,把,双,台,年,月,日,季";
        var array=units.split(",");
        for (var i = 0; i < array.length; i++) {
            var str="<button type='button' class='btn btn-default'>"+array[i]+"</button>"
            $("#Unitd").append(str);
        }
        $("#Unitd").find("button").click(function(){
            $("#ProUnit").val($(this).text());
        });
            
    }
    //提交前处理
    function PreSubmit() {
        if (!MultiPrice.PreSubmit()) return false;
        $("#EBtnSubmit").click();
    }
    //-----滑动门
    var aid = 0;
    var showID = 0;
    var tID = 0;
    var arrTabTitle = new Array("TabTitle0", "TabTitle1", "TabTitle2", "TabTitle3", "TabTitle4", "TabTitle5", "TabTitle6", "TabTitle7");
    var arrTabs = new Array("Tabs0", "Tabs1", "Tabs2", "Tabs3", "Tabs4", "Tabs5", "Tabs6", "Tabs7");
    function ShowTabss(SID) {
        if (SID != tID) {
            if (document.getElementById(arrTabTitle[tID].toString()) != null)
                document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
            if (document.getElementById(arrTabTitle[SID].toString()) != null)
                document.getElementById(arrTabTitle[SID].toString()).className = "titlemouseover";
            if (document.getElementById(arrTabs[tID].toString()) != null)
                document.getElementById(arrTabs[tID].toString()).style.display = "none";
            if (document.getElementById(arrTabs[SID].toString()) != null)
                document.getElementById(arrTabs[SID].toString()).style.display = "";
            tID = SID;
            aid = SID;
        }
    }
    //捆绑商品操作
    function BindPro(projson) {
        if (projson) { $("#Bind_Hid").val(projson); }
        if ($("#Bind_Hid").val() == "") { $("#bindpro_body").html(""); return; }
        $(".td_binddata").remove();//清空之前捆绑数据
        var proattr = [];
        try { proattr = JSON.parse($("#Bind_Hid").val()); } catch (ex) { }
        var trtlp = "<tr class='td_binddata'><td class='td_s'><label><input type='checkbox' name='bind_chk' value='@id'>@id</label></td><td class='td_m'><img onerror=\"shownopic(this);\" style='width:60px;height:45px;' src='/@Thumbnails' /></td><td class='td_m'>@LinPrice</td><td>@Proname</td></tr>";
        var html = JsonHelper.FillData(trtlp,proattr);
        $("#bindpro_body").append(html);
        $("#bindpro_table").show();
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
    //选择
    function SelectProducer() {
        window.open('Producerlist.aspx', '', 'width=600,height=450,resizable=0,scrollbars=yes');
    }
    //团购信息
    function OpenSelect2() {
        window.open('TGALL.aspx?shopid=<%:ProID %>', '', 'width=600,height=450,resizable=0,scrollbars=yes');
        return false;
    }
    function CloseDiag() {
        diag.CloseModal();
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
    //清除促销方案
    function Clearoption() {
        var hiddenidvalue = document.getElementById("OtherProject"); //获取已经存在的ID值
        for (var i = hiddenidvalue.options.length - 1; i >= 0; i--) {
            if (hiddenidvalue[i].selected == true) {
                hiddenidvalue[i] = null;
            }
        }
    }
</script>
</asp:Content>
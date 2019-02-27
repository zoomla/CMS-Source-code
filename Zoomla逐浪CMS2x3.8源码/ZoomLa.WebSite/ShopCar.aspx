<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopCar.aspx.cs" Inherits="ShopCar" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>加入购物车</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="main" class="rg_inout">
    <h1>
        第一步:加入购物车<span>[<asp:Label ID="Label1" runat="server" BorderWidth="0px" ForeColor="Red"></asp:Label>]</span><img
            src="/user/images/regl1.gif" width="242" height="14" alt="" /></h1>
    <div class="cart_lei">
        <ul>
            <li class="i1">商品名称</li>
            <li class="i2">来源</li>
            <li class="i3">单价</li>
            <li class="i4">数量</li>
            <li class="i5">
                <asp:Label ID="lblProinfo" runat="server" Text="备注"></asp:Label></li>
            <li class="i6">金额</li>
            <li class="i7">操作</li>
        </ul>
    </div>
    <div class="cart_con">
        <asp:Repeater ID="cartinfo" runat="server">
            <ItemTemplate>
                <ul <%#(Eval("Bindpro","{0}")=="")?"":"style=background-color:#E6E6E6"%>>
                    <li class="i1">
                        <%#GetProtype(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%><a href="<%#Eval("ProSeller")%>"
                            target="_blank"><%#Eval("proname")%></a></li>
                    <li class="i2">
                        <%#Eval("proseller")%></li>
                    <li class="i3">
                        <%#getjiage("1", DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
                    <li class="i4">
                         <a href="javascript:;" class="J_minus" onblur="keydo('<%#Eval("id") %>')">-</a> <input onkeydown="if(event.keyCode==13){ keydo('<%#Eval("id") %>');return false;}"
                            id='num<%#Eval("id") %>' value='<%#getShu(DataBinder.Eval(Container, "DataItem.pronum", "{0}"))%>'
                            style="width: 30px; height: 20px;" onblur="keydo('<%#Eval("id") %>')"  class="J_input" /><a href="javascript:;" class="J_add" onblur="keydo('<%#Eval("id") %>')">+</a></li>
                    <li class="i5">
                        <%#getProinfo(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
                    <li class="i6"><span id='price<%#Eval("id") %>'>
                        <%#getprojia(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></span></li>
                    <li class="i7"><a href='ShopCar.aspx?menu=del&cid=<%#Eval("id")%>&ProClass=<%#Eval("ProClass") %>'
                        onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a></li>
                    <div class="clear">
                    </div>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="juan">
        <asp:Label ID="yhq" runat="server" Text="优惠券:" ForeColor="Red" Font-Size="12px"></asp:Label>
        <asp:TextBox ID="yhqtext" runat="server" BorderColor="Red" BorderStyle="Solid" Height="18px"
            Width="90px" Style="margin-bottom: -2px;"></asp:TextBox>
        <asp:Label ID="label" runat="server" Text="密&nbsp&nbsp  码:" ForeColor="Red" Font-Size="12px"></asp:Label>
        <asp:TextBox ID="yhqpwd" runat="server" BorderColor="Red" BorderStyle="Solid" Height="18px"
            Width="90px" Style="margin-bottom: -2px;"></asp:TextBox>
        <span>提示：只要填入合法的优惠券，系统会在下一步结算时计折。</span>
    </div>
    <div style="margin-left: 170px;">
        <ul>
            <li style="width: 100%;">共
                <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                个商品
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：
                <asp:Label ID="Nowpage" runat="server" Text="" />
                /
                <asp:Label ID="PageSize" runat="server" Text="" />
                页
                <asp:Label ID="pagess" runat="server" Text="" />
                个商品/页 转到第
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                页</li></ul>
    </div>
    <div id="tejia" runat="server">
        <div id="Div1" runat="server">
            特价商品:</div>
        <div id="Label3" runat="server">
            <div class="cart_lei">
                <ul>
                    <li class="i1">商品名称</li>
                    <li class="i2">来源</li>
                    <li class="i3">单价</li>
                    <li class="i4">数量</li>
                    <li class="i5">
                        <asp:Label ID="Label4" runat="server" Text="备注"></asp:Label></li>
                    <li class="6">金额</li>
                    <li class="7">操作</li>
                </ul>
            </div>
            <div class="cart_con">
                <asp:Repeater ID="cartinfo2" runat="server">
                    <ItemTemplate>
                        <ul <%#(Eval("Bindpro","{0}")=="")?"":"style=background-color:#E6E6E6"%>>
                            <li class="i1">
                                <%# Server.HtmlEncode(GetProtype(DataBinder.Eval(Container, "DataItem.proid", "{0}")))%><a href="<%# Server.HtmlEncode(Eval("ProSeller").ToString())%>"
                                    target="_blank"><%#Server.HtmlEncode(Eval("proname").ToString())%></a></li>
                            <li class="i2">
                                <%#Eval("proseller")%></li>
                            <li class="i3">
                                <%#getjiage("2", DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
                            <li class="i4">
                                <input onkeydown="if(event.keyCode==13){ keydo('<%#Eval("id") %>');return false;}"
                                    id='num<%#Eval("id") %>' value='<%#getShu(DataBinder.Eval(Container, "DataItem.pronum", "{0}"))%>'
                                    style="width: 30px; height: 20px;" onblur="keydo('<%#Eval("id") %>')" /></li>
                            <li class="i5">
                                <%# Server.HtmlEncode(getProinfo(DataBinder.Eval(Container, "DataItem.proid", "{0}")))%></li>
                            <li class="i6"><span id='price<%#Eval("id") %>'>
                                <%# Server.HtmlEncode(getprojia(DataBinder.Eval(Container, "DataItem.id", "{0}")))%></span></li>
                            <li class="i7"><a href='ShopCar.aspx?menu=del&cid=<%# Server.HtmlEncode(Eval("id").ToString())%>&ProClass=<%# Server.HtmlEncode(Eval("ProClass").ToString()) %>'
                                onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a></li>
                            <div class="clear">
                            </div>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="cleardiv" style="height: 30px">
        </div>
        <div id="jifensp" runat="server">
            <div id="Div2" runat="server">
                <b>[积分商品:]</b></div>
            <div id="Div4" runat="server">
                <div class="cart_lei">
                    <ul>
                        <li class="i1">商品名称</li>
                        <li class="i2">来源</li>
                        <li class="i3">积分</li>
                        <li class="i4">数量</li>
                        <li class="i5">
                            <asp:Label ID="Label6" runat="server" Text="备注"></asp:Label></li>
                        <li class="i6">总计</li>
                        <li class="i7">操作</li>
                    </ul>
                </div>
                <div class="cart_con">
                    <asp:Repeater ID="cartinfo3" runat="server">
                        <ItemTemplate>
                            <ul <%#(Eval("Bindpro","{0}")=="")?"":"style=background-color:#E6E6E6"%>>
                                <li class="i1">
                                    <%#GetProtype(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%><a href="<%#Eval("ProSeller")%>"
                                        target="_blank"><%#Eval("proname")%></a></li>
                                <li class="i2">
                                    <%#Eval("proseller")%></li>
                                <li class="i3">
                                    <%#getjiage("3", DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
                                <li class="i4">
                                    <input onkeydown="if(event.keyCode==13){ keydo('<%#Eval("id") %>');return false;}"
                                        id='num<%#Eval("id") %>' value='<%#getShu(DataBinder.Eval(Container, "DataItem.pronum", "{0}"))%>'
                                        style="width: 30px; height: 20px;" onblur="keydo('<%#Eval("id") %>')" /></li>
                                <li class="i5">
                                    <%#getProinfo(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
                                <li class="i6"><span id='price<%#Eval("id") %>'>
                                    <%#getprojia(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></span></li>
                                <li class="i7"><a href='ShopCar.aspx?menu=del&cid=<%#Eval("id")%>&ProClass=<%#Eval("ProClass") %>'
                                    onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a></li>
                                <div class="clear">
                                </div>
                            </ul>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <div class="jia">
        <ul>
            <li style="width: 250px; text-align: left;">金额合计：<asp:Label ID="alljiage" name="alljiage" runat="server" Text=""></asp:Label></li>
            <li style="width: 250px; text-align: left;">积分合计：<asp:Label ID="alljifen" runat="server" Text=""></asp:Label></li>
            <li style="width: 250px; text-align: left;">
                <asp:Button ID="Button1" runat="server" Text="去收银台结帐" OnClientClick="keydo()" OnClick="Button1_Click" />
                <asp:HiddenField ID="project" runat="server" />
                <asp:HiddenField ID="jifen" runat="server" />
                <asp:HiddenField ID="ProClass" runat="server" />
            </li>
        </ul>
    </div>
</div>
<div id="bottom">
    <a href="/">
        <img src="<%Call.Label("{$LogoUrl/}"); %>" alt="<%Call.Label("{$SiteName/}"); %>" /></a>
    <p>
        <script language="javascript" type="text/javascript"> 
<!--
    var year = "";
    mydate = new Date();
    myyear = mydate.getYear();
    year = (myyear > 200) ? myyear : 1900 + myyear;
    document.write(year); 
    --> 
        </script>
        &copy;&nbsp;Copyright&nbsp;
        <%Call.Label("{$SiteName/}"); %>
        All rights reserved.</p>
</div>
<input type="hidden" id="projuct" name="projuct" runat="server" />
<input type="hidden" id="Stock" name="Stock" runat="server" />
<!--数量 -->
<input type="hidden" id="GuestName" name="GuestName" runat="server" />
<!-- 客户名称 -->
<input type="hidden" id="comedate" name="comedate" runat="server" />
<!-- 入住时间 -->
<input type="hidden" id="GuestMobile" name="GuestMobile" runat="server" />
<!-- 客户手机 -->
<input type="hidden" id="cityname" name="cityname" runat="server" />
<!-- 城市名称 -->
<input type="hidden" id="preID" name="preID" runat="server" />
<!-- 证件号码 -->
<input type="hidden" id="Type" name="Type" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        var ajax = new AJAXRequest();
        function keydo(ids) {
            var num = document.getElementById("num" + ids).value;
            ajax.get(
            "/prompt/ShopCart/UpdateShopCar.aspx?cid=" + ids + "&num=" + num + "&menu=shopcar",
            function (obj) {
                var prics = obj.responseText.split('|');
                document.getElementById("alljiage").innerText = prics[0];
                document.getElementById("price" + ids).innerText = prics[1];
                document.getElementById("alljifen").innerText = prics[2];
            }
    );
        }

        /*商品加减*/
        $.fn.iVaryVal=function(iSet){
            /*
             * Minus:点击元素--减小
             * Add:点击元素--增加
             * Input:表单元素
             * Min:表单的最小值，非负整数
             * Max:表单的最大值，正整数
             * Fun:回调函数
             */
            iSet=$.extend({Minus:$('.J_minus'),Add:$('.J_add'),Input:$('.J_input'),Min:0,Max:20,Fun:null},iSet);
            var C=null,O=null;
            //输出值全局对象[若担心全局对象污染，可定义一个hidden表单，向其传值]
            $GLOBAL={};
            //增加
            iSet.Add.each(function(i){
                $(this).click(function(){
                    O=parseInt(iSet.Input.eq(i).val());
                    (O+1<=iSet.Max) || (iSet.Max==null) ? iSet.Input.eq(i).val(O+1) : iSet.Input.eq(i).val(iSet.Max);
                    //输出当前改变后的值
                    $GLOBAL.val=iSet.Input.eq(i).val();
                    $GLOBAL.index=i;
                    //回调函数
                    if (typeof iSet.Fun == 'function') {
                        iSet.Fun.call(this);
                    }
                });
            });
            //减少
            iSet.Minus.each(function(i){
                $(this).click(function(){
                    O=parseInt(iSet.Input.eq(i).val());
                    O-1<iSet.Min ? iSet.Input.eq(i).val(iSet.Min) : iSet.Input.eq(i).val(O-1);
                    $GLOBAL.val=iSet.Input.eq(i).val();
                    $GLOBAL.index=i;
                    //回调函数
                    if (typeof iSet.Fun == 'function') {
                        iSet.Fun.call(this);
                    }
                });
            });
            //手动
            iSet.Input.bind({
                'click':function(){
                    O=parseInt($(this).val());
                    $(this).select();
                },
                'keyup':function(e){
                    if($(this).val()!=''){
                        C=parseInt($(this).val());
                        //非负整数判断
                        if(/^[1-9]\d*|0$/.test(C)){
                            $(this).val(C);
                            O=C;
                        }else{
                            $(this).val(O);
                        }
                    }
                    //键盘控制：上右--加，下左--减
                    if(e.keyCode==38 || e.keyCode==39){
                        iSet.Add.eq(iSet.Input.index(this)).click();
                    }
                    if(e.keyCode==37 || e.keyCode==40){
                        iSet.Minus.eq(iSet.Input.index(this)).click();
                    }
                    //输出当前改变后的值
                    $GLOBAL.val=$(this).val();
                    $GLOBAL.index=iSet.Input.index(this);
                    //回调函数
                    if (typeof iSet.Fun == 'function') {
                        iSet.Fun.call(this);
                    }
                },
                'blur':function(){
                    $(this).trigger('keyup');
                    if($(this).val()==''){
                        $(this).val(O);
                    }
                    //判断输入值是否超出最大最小值
                    if(iSet.Max){
                        if(O>iSet.Max){
                            $(this).val(iSet.Max);
                        }
                    }
                    if(O<iSet.Min){
                        $(this).val(iSet.Min);
                    }
                    //输出当前改变后的值
                    $GLOBAL.val=$(this).val();
                    $GLOBAL.index=iSet.Input.index(this);
                    //回调函数
                    if (typeof iSet.Fun == 'function') {
                        iSet.Fun.call(this);
                    }
                }
            });
        }
        //调用
        $( function() {
	
            $('.i_box').iVaryVal({Fun:function(){
		
            }});
	
        });
</script>

</asp:Content>
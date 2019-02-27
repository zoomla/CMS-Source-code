<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ShopConfig"MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>商城参数设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table   class="table table-striped table-bordered table-hover">
      <tr align="center">
        <td colspan="2" class="spacingtitle">商城参数设置</td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>是否允许游客购买商品：</strong></td>
        <td width="66%" valign="middle">
        
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                Width="89px">
                <asp:ListItem Selected="True">是</asp:ListItem>
                <asp:ListItem>否</asp:ListItem>
            </asp:CheckBoxList></td>
      </tr>
       <tr class="WebPart">
        <td width="34%" class="tdbgleft" style="height: 23px"><strong>默认商品税率优惠类型：</strong></td>
        <td valign="middle" style="height: 23px">&nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem Selected="True">含税，不开发票时有税率优惠 </asp:ListItem>
                <asp:ListItem>含税，不开发票时没有税率优惠</asp:ListItem>
                <asp:ListItem>不含税，开发票时需要加收税费</asp:ListItem>
                <asp:ListItem>不含税，开发票时不需要加收税费</asp:ListItem>
            </asp:RadioButtonList></td>
      </tr>
      <tr>
        <td width="34%" height="22" class="tdbgleft"><strong>默认税率设置：</strong></td>
        <td height="22" valign="middle"><asp:TextBox ID="TextBox1" class="l_input" runat="server" Width="61px">0</asp:TextBox>
            %</td>
      </tr>
      <tr>
        <td width="34%" class="tdbgleft" style="height: 22px"><strong>订单编号前缀：</strong></td>
        <td valign="middle" style="height: 22px"><asp:TextBox ID="TextBox2" class="l_input" runat="server" Width="61px">PE</asp:TextBox></td>
      </tr>
      <tr>
        <td width="34%" height="22" class="tdbgleft"><strong>在线支付单编号前缀：</strong></td>
        <td height="22" valign="middle"><asp:TextBox ID="TextBox3" class="l_input" runat="server" Width="61px">OP</asp:TextBox></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>所在的地区：</strong></td>
        <td height="22" valign="middle"></td>
      </tr>
      <tr>
        <td width="34%" class="tdbgleft" style="height: 22px"><strong>所在地区的邮政编码：</strong></td>
        <td valign="middle" style="height: 22px"><asp:TextBox ID="TextBox4" class="l_input" runat="server">330000</asp:TextBox></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>允许会员自主对订单只支付部分金额（即交定金）：</strong><br />
          如果不允许，当会员预付款小于订单金额时，不允许支付；当对订单在线支付时，检查支付金额是否小于订单金额，如果小于，只打入会员帐户中作为预付款，不对订单进行支付。</td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                Width="102px">
                <asp:ListItem Selected="True">是</asp:ListItem>
                <asp:ListItem>否</asp:ListItem>
            </asp:RadioButtonList></td>
      </tr>
      <tr>
        <td width="34%" height="22" class="tdbgleft"><strong>支付部分金额比率：</strong><br />
          输入1-100之间的支付金额比率</td>
        <td height="22" valign="middle"><asp:TextBox ID="TextBox5" class="l_input" runat="server">10</asp:TextBox>
            %</td>
      </tr>
      <tr>
        <td width="34%" height="22" class="tdbgleft"><strong>订单商品种类的数量限制：</strong><br />
          当设置为“0”时，代表不限制</td>
        <td height="22" valign="middle"><asp:TextBox ID="TextBox6" class="l_input" runat="server">0</asp:TextBox></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>启用指派跟单员功能：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal"
                Width="96px">
                <asp:ListItem>是</asp:ListItem>
                <asp:ListItem Selected="True">否</asp:ListItem>
            </asp:RadioButtonList></td>
      </tr>
            <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>自动生成缩略图：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal"
                Width="96px">
                <asp:ListItem>是</asp:ListItem>
                <asp:ListItem Selected="True">否</asp:ListItem>
            </asp:RadioButtonList></td>
      </tr>
            <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>是否给商品图片添加水印：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
            <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>是否开启预付款密码功能：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
            <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在购物车页面的商品列表中显示商品类别：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
            <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在收银台页的商品列表中显示商品类别：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
            <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在订单预览页的商品列表中显示商品类别：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在购物车页面的商品列表中显示销售类型：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" class="tdbgleft" style="height: 22px"><strong>在收银台页的商品列表中显示销售类型：</strong> </td>
        <td valign="middle" style="height: 22px"><asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在订单预览页的商品列表中显示销售类型：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在购物车页面的商品列表显示市场价：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在收银台页的商品列表中显示市场价：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在订单预览页的商品列表中显示市场价：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在购物车显示商品缩略图：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在收银台显示商品缩略图：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList>
            <table id="ctl00_CphContent_tblPaymentProductThumbSize">
                <tr onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        缩略图宽度:<asp:TextBox ID="sytimgwidth1" class="l_input" runat="server" Width="30px">0</asp:TextBox>缩略图高度:<asp:TextBox
                            ID="sytimgheight1" class="l_input" runat="server" Width="30px">45</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 59px">
                        如果您不能确认图片的原始尺寸，无法指定具体的宽和高时，可以只指定宽度或者高度中的一个值，0代表不设置，图片会根据您设置的宽度或高度自动调节图片大小，以此避免图片失真的情况</td>
                </tr>
            </table>
        </td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在预览页页面显示商品缩略图：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList>
            <table id="Table1">
                <tr onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        缩略图宽度:<asp:TextBox ID="sytimgwidth2" class="l_input" runat="server" Width="30px">0</asp:TextBox>缩略图高度:<asp:TextBox
                            ID="sytimgheight2" class="l_input" runat="server" Width="30px">45</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 59px">
                        如果您不能确认图片的原始尺寸，无法指定具体的宽和高时，可以只指定宽度或者高度中的一个值，0代表不设置，图片会根据您设置的宽度或高度自动调节图片大小，以此避免图片失真的情况</td>
                </tr>
            </table>
        </td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在订单信息页的商品列表中显示商品缩略图：</strong></td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList>
            <table id="Table2">
                <tr onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td>
                        缩略图宽度:<asp:TextBox ID="sytimgwidth3" class="l_input" runat="server" Width="30px">0</asp:TextBox>缩略图高度:<asp:TextBox
                            ID="sytimgheight3" class="l_input" runat="server" Width="30px">45</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 59px">
                        如果您不能确认图片的原始尺寸，无法指定具体的宽和高时，可以只指定宽度或者高度中的一个值，0代表不设置，图片会根据您设置的宽度或高度自动调节图片大小，以此避免图片失真的情况</td>
                </tr>
            </table>
        </td>
      </tr>
      <tr class="WebPart">
        <td width="34%" height="22" class="tdbgleft"><strong>在商品管理页的商品列表中显示商品缩略图：</strong> </td>
        <td height="22" valign="middle"><asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatDirection="Horizontal"
                Width="96px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList>
            <table id="Table3">
                <tr>
                    <td>
                        缩略图宽度:<asp:TextBox ID="sytimgwidth4" class="l_input" runat="server" Width="30px">0</asp:TextBox>缩略图高度:<asp:TextBox
                            ID="sytimgheight4" class="l_input" runat="server" Width="30px">0</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 59px">
                        如果您不能确认图片的原始尺寸，无法指定具体的宽和高时，可以只指定宽度或者高度中的一个值，0代表不设置，图片会根据您设置的宽度或高度自动调节图片大小，以此避免图片失真的情况</td>
                </tr>
            </table>
        </td>
      </tr>
      <tr>
        <td width="34%" height="22" class="tdbgleft"><strong>购物车继续购物按钮链接地址：</strong></td>
        <td height="22" valign="middle">
            <asp:TextBox ID="TextBox7" class="l_input" runat="server" Width="277px"></asp:TextBox></td>
      </tr>
      <tr>
        <td width="34%" height="22" class="tdbgleft"><strong>订购成功返回按钮链接地址：</strong></td>
        <td height="22" valign="middle">
            <asp:TextBox ID="TextBox8" class="l_input" runat="server" Width="277px"></asp:TextBox></td>
      </tr>
      <tr class="tdbg">
        <td colspan="5" align="center" class="tdbg" style="height: 49px">
            <asp:Button ID="Button1" class="C_input"  style="width:100px;"  runat="server" Text="保存设置" /></td>
      </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
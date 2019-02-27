<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPresent.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="Zoomla.Website.manage.Shop.AddPresent" EnableViewStatemac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加礼品</title>
<script src="/JS/Common.js" type="text/javascript"></script>
<script type="text/javascript">        
function SetDisabled(checked)      
{
        document.getElementById('cpbh').disabled =checked;      
}

function Setsmallimgs(checked)      
{
if (checked==true){
        document.getElementById('addsmallimg').style.display = "none";
        }else{
        document.getElementById('addsmallimg').style.display = "";
        }
} 
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="r_navigation">后台管理&gt;&gt; 商城管理 &gt;&gt;  <asp:Label ID="Label1" runat="server" Text="添加商品"></asp:Label></div>
<ul class="nav nav-tabs">
    <li class="active"><a href="#Tabs0" data-toggle="tab">基本信息</a></li>
    <li><a href="#Tabs1" data-toggle="tab">介绍及图片</a></li>
</ul>
    <div class="tab-content panel-body padding0">
        <div class="tab-pane active" id="Tabs0">
            <table class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <td width="20%" class="tdbgleft">
                                <strong>促销礼品名称：</strong></td>
                          <td width="75%">
                              <asp:TextBox ID="Prename" class="form-control" runat="server" Width="233px"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Prename"
                                  ErrorMessage="促销礼品名称不能为空!"></asp:RequiredFieldValidator>
                              <asp:HiddenField ID="ID_H" runat="server" />
                              <asp:HiddenField ID="uptype" runat="server" />
                          </td>
                      </tr>
                        <tr>
                            <td class="tdbgleft" style="height: 19px">
                                <strong>礼品性质：</strong></td>
                          <td style="height: 19px"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td style="width: 10%">
                                  <asp:RadioButtonList ID="Pretype" runat="server" RepeatDirection="Horizontal"
                                      Width="237px">
                                      <asp:ListItem Selected="True">实物</asp:ListItem>
                                      <asp:ListItem>服务</asp:ListItem>
                                      <asp:ListItem>下载</asp:ListItem>
                                      <asp:ListItem>点卡</asp:ListItem>
                                  </asp:RadioButtonList></td>
                              <td width="52%">（如充值卡，游戏点卡，游戏装备等虚拟物品）</td>
                            </tr>
                          </table></td>
                      </tr>
                        <tr>
                            <td class="tdbgleft">
                                <strong>促销礼品编号：</strong>                            </td>
                          <td><asp:TextBox ID="procode" class="form-control text_300" runat="server"   /></td>
                        </tr>
                        <tr>
                            <td class="tdbgleft">
                                <strong>市场参考价：</strong>                          </td>
                          <td><asp:TextBox ID="ShiPrice" class="form-control text_300" runat="server" Width="80px"  /></td>
                        </tr>
                        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                            <td class="tdbgleft" style="height: 24px">
                                <strong>当前零售价：</strong></td>
                            <td style="height: 24px"><asp:TextBox ID="LinPrice" class="form-control" runat="server" Width="80px" />
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LinPrice"
                                    Display="Dynamic" ErrorMessage="当前零售价不能为空!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="LinPrice"
                                    Display="Dynamic" ErrorMessage="零售价的格式不正确!" ValidationExpression="\d+[.]?\d*"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="tdbgleft">
                                <strong>促销礼品单位：</strong>                                </td>
                            <td><asp:TextBox ID="ProUnit" class="form-control" runat="server" Width="155px" /></td>
                        </tr>
                        <tr>
                            <td class="tdbgleft">
                                <strong>库存数量：</strong>                                </td>
                            <td><asp:TextBox ID="Stock"  class="form-control" runat="server" Width="125px" /></td>
                        </tr>
                        <tr>
                            <td class="tdbgleft">
                                <strong>库存报警下限：</strong></td>
                            <td>
                                <asp:TextBox ID="StockDown" class="form-control" runat="server"></asp:TextBox></td>
                        </tr>
                   
                        <tr>
                            <td class="tdbgleft">
                                <strong>重量：</strong></td>
                            <td><asp:TextBox ID="Weight" class="form-control" runat="server" Width="91px" />
                                千克（Kg）<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="Weight"
                                    ErrorMessage="重量格式不正确!" MaximumValue="9999999" MinimumValue="0" Type="Double"></asp:RangeValidator></td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" style="height: 18px">
                                <strong>服务限制：</strong></td>
                            <td style="height: 18px">
                                <asp:TextBox ID="ServerPeriod" class="form-control" runat="server" Width="73px"></asp:TextBox>
                                <asp:DropDownList ID="ServerType" runat="server">
                                    <asp:ListItem Selected="True" Value="1">年</asp:ListItem>
                                    <asp:ListItem Value="2">月</asp:ListItem>
                                    <asp:ListItem Value="3">日</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ServerPeriod"
                                    ErrorMessage="服务期限必须为整数!" MaximumValue="9999999" MinimumValue="0" Type="Integer"></asp:RangeValidator></td>
                      </tr>
                         </tbody>

            </table>
        </div>
        <div class="tab-pane" id="Tabs1">
            <table  class="table table-striped table-bordered table-hover">
                <tbody>        
                    <tr>
                        <td class="tdbgleft" width="20%">
                            <strong>礼品简介：<br />用于礼品页显示，不要超过255个字符</strong></td>
                        <td>
                            <asp:TextBox ID="Preinfo" class="form-control" runat="server" Height="80px" 
                                TextMode="MultiLine" Width="343px" /></td>
                    </tr>
                    <tr>
                        <td class="tdbgleft" width="20%" style="height: 24px">
                            <strong>详细介绍：</strong></td>
                        <td style="height: 24px">
                            <asp:HiddenField ID="precontent" runat="server" /><iframe id="infoeditor" src="../../editor/fckeditor_1.html?InstanceName=precontent&Toolbar=Default" width="580px" height="150px" frameborder="no" scrolling="no"></iframe>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdbgleft" width="20%">
                            <strong>礼品清晰图：</strong></td>
                        <td><asp:TextBox ID="Clearimg" class="form-control" runat="server" Width="300px" /><iframe id="Clearimgs" style="top:2px" src="fileupload.aspx?menu=Clearimg" width="100%" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="自动生成缩略图" onclick="Setsmallimgs(this.checked)" />
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="添加水印" /></td>
                    </tr>
                    <tr>
                        <td class="tdbgleft" width="20%">
                            <strong>礼品缩略图：</strong></td>
                        <td><asp:TextBox ID="Thumbnails" class="form-control" runat="server" Width="300px" /><iframe id="Thumbnailss" style="top:2px" src="fileupload.aspx?menu=Thumbnails" width="100%" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="添加水印" /></td>
                    </tr>     
                </tbody>
            </table>
        </div>
    </div>

    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">                 
                <asp:Button ID="EBtnSubmit" Text="保存" class="btn btn-primary"  runat="server" OnClick="EBtnSubmit_Click" />&nbsp; &nbsp;
                <input name="Cancel" class="btn btn-primary"  type="button" id="BtnCancel" value="取消" onclick="window.location.href='NodeManage.aspx'" />                
            </td>
        </tr>
    </table>
</asp:Content>

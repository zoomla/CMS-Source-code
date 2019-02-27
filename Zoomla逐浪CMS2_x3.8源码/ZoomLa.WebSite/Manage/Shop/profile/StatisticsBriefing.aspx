<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatisticsBriefing.aspx.cs" Inherits="manage_Shop_profile_StatisticsBriefing" EnableViewStateMac="false" Debug="true" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>统计简报</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover"	>
        <tr >
            <td  colspan="1" class="spacingtitle"> 
                <b>合计</b>
            </td>
        </tr>
        <tr >
            <td valign="top" align="left" >
                <table class="table table-striped table-bordered table-hover"	 >
                    <tr class="tdbg">
                        <td valign="top">
                            点击统计： <b>0</b><br/>
                            <br/>
                        </td>
                        <td valign="top">
                            已经确认的销售： <b>0</b><br/>
                            等待确认的销售： <b>0</b><br/>
                            无效的销售： <b>0</b><br/>
                            销售推广的退款/拒付总额： <b>0</b><br/>
                            <br/>
                        </td>
                        <td valign="top">
                            确认的引导： <b>0</b><br/>
                            待确认的引导： <b>0</b><br/>
                            无效的引导： <b>0</b><br/>
                            无效的引导： <b>0</b><br/>
                            <br/>
                        </td>
                        <td valign="top">
                            已经确认的佣金： <b>0 元</b><br/>
                            等待确认的佣金： <b>0 元</b><br/>
                            无效的佣金： <b>0 元</b><br/>
                            合计已经支付的佣金： <b>0 元</b><br/>
                            佣金的退款/拒付总额： <b>0 元</b><br/>
                        </td>
                    </tr>
                </table>
                <br/>
            </td>
        </tr>
        <tr >
            <td colspan="1"  class="spacingtitle"> 
                <b>总计</b>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <table class="table table-striped table-bordered table-hover"	 style="text-align:center;" >
                   <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
    
                        <td width="20%" class="title"> 
                        </td>
                        <td width="20%" class="title"> 
                            <b>批准</b>
                        </td>
                        <td width="20%" class="title"> 
                            <b>待批</b>
                        </td>
                        <td width="20%" class="title"> 
                            <b>拒批</b>
                        </td>
                        <td width="20%" class="title"> 
                            <b>退款/拒付</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            显示 (IP/全部) 
                        </td>
                        <td>
                            0 / 0
                        </td>
                        <td>
                            -
                        </td>
                        <td>
                            -
                        </td>
                        <td>
                            -
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             点击 
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                            销售额 
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                             引导数量 
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td colspan="5" align="center" class="tdbgleft">
                            <b>多层次下线</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="title"> 
                        </td>
                        <td class="title"> 
                            <b>批准</b>
                        </td>
                        <td class="title"> 
                            <b>待批</b>
                        </td>
                        <td class="title"> 
                            <b>拒批</b>
                        </td>
                        <td class="title"> 
                            <b>退款/拒付</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             点击 
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             销售额 
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                            引导数量 
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                        <td>
                            0
                        </td>
                    </tr>
                </table>
                 <br/>
            </td>
        </tr>
        <tr >
            <td  colspan="1" class="spacingtitle"> 
                <b>佣金</b>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <table class="table table-striped table-bordered table-hover"	 style="text-align:center;">
                    <tr class="tdbg">
                        <td width="20%" class="title"> 
                        </td>
                        <td width="16%" class="title"> 
                            <b>已付款</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>批准</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>待批</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>拒批</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>退款/拒付</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             介绍新联盟会员 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             多层次新联盟会员 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                            CPM（广告千次展示）
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                            点击 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                            销售额 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                            引导数量
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             总计： 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td colspan="6" align="center" class="tdbgleft">
                            <b>多层次下线</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td width="20%" class="title"> 
                        </td>
                        <td width="16%" class="title"> 
                            <b>已付款</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>批准</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>待批</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>拒批</b>
                        </td>
                        <td width="16%" class="title"> 
                            <b>退款/拒付</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             多层次新联盟会员 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                            CPM（广告千次展示） 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             点击 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td  class="tdbgleft">
                             销售额 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            引导数量 
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                             总计：
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                        <td>
                            0 元
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>

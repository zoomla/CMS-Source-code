<%@ Control Language="C#" AutoEventWireup="true" CodeFile="receprint.ascx.cs" Inherits="MIS_OA_Tlp_receprint" %>
<link href="/Mis/OA/Tlp/SpecialForm.css" rel="stylesheet" />
<style type="text/css">
    .noborder {border:1px solid #fff;
    }
</style>
<table class="table table_red1 formtb">
    	<tr>
        	<th colspan="2" style="text-align:center;">
            	<h1>江西省鹰潭市教育局收文处理专用纸</h1>
        	</th>
        </tr>
        <tr>
        	<td>
            	<div class="media">
                    <div class="media-left"><span style="line-height:34px;">收文序列：</span></div>
                    <div class="media-body">
                        <asp:Label runat="server" ID="Label1" CssClass="labelfont" Text="鹰教信字"></asp:Label>
                        <asp:Label runat="server" ID="No_T" CssClass="labelfont"></asp:Label>
                    </div>
                </div>
            </td>
            <td>
            	<div class="media">
                    <div class="media-left"><span style="line-height:34px;">收文日期：</span></div>
                    <div class="media-body">
                        <asp:Label runat="server" ID="SendDate_T" CssClass="labelfont"></asp:Label>
                     </div>
                </div>
            </td>
        </tr>
        <tr>
        	<td>
            	<div class="media">
                    <div class="media-left"><span style="line-height:34px;">归档类目：</span></div>
                    <div class="media-body">
                        <asp:Label runat="server" ID="L1" Text="鹰教信字" CssClass="labelfont"></asp:Label>
                    </div>
                </div>
            </td>
            <td>
            	<div class="media">
                    <div class="media-left"><span style="line-height:34px;">保存期限：</span></div>
                    <div class="media-body">
                        <asp:Label runat="server" ID="bcqx" CssClass="labelfont"></asp:Label>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
        	<td colspan="2">
            	<div class="media">
                    <div class="media-left"><span style="line-height:34px;">文件文号：</span></div>
                    <div class="media-body">
                         <asp:Label runat="server" ID="wjwh" CssClass="labelfont"></asp:Label>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
        	<td colspan="2">
            	<div class="media">
                    <div class="media-left"><span>来文机关：</span></div>
                    <div class="media-body heimid">
                         <asp:Label runat="server" ID="lwjg" CssClass="labelfont"></asp:Label>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
        	<td colspan="2">
            	<div class="media">
                    <div class="media-left"><span>标题：</span></div>
                    <div class="media-body heimid">
                        <asp:Label runat="server" ID="Title_T" CssClass="labelfont"></asp:Label>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
        	<td colspan="2" style="color:#666;">
                <span>拟办：</span>
                <%=GetHQInfo(1) %>
        	</td>
        </tr>
        <tr>
        	<td colspan="2" style="color:#666;">
                <span>批阅与批发：</span>
                <%=GetHQInfo(2) %>
        	</td>
        </tr>
        <tr>
        	<td colspan="2">
            	<div class="media">
                    <div class="media-left" style="vertical-align:middle;"><span>阅文人：</span></div>
                    <div class="media-body">
                       <table class="table table-bordered table_red" style="margin-bottom:0;">
                        	<tr>
                            	<td class="text-center">姓名</td>
                            	<td class="text-center">批示日期</td>
                            	<td class="text-center">结果</td>
                                <td class="text-center">批示</td>
                            </tr>
                            <asp:Repeater runat="server" ID="RPT">
                               <ItemTemplate>
                                   <tr>
                                       <td class="text-center"><%#Eval("UserName") %></td>
                                       <td class="text-center"><%#Eval("CreateTime","{0:yyyy年MM月dd日}") %></td>
                                       <td class="text-center"><%#GetResult() %></td>
                                       <td class="text-center"><%#Eval("Remind") %></td>
                                   </tr>
                               </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
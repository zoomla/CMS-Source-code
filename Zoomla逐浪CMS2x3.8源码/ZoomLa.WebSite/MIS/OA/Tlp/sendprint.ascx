<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sendprint.ascx.cs" Inherits="MIS_OA_Tlp_sendprint" %>
<link href="/Mis/OA/Tlp/SpecialForm.css" rel="stylesheet" />
<asp:TextBox runat="server" ID="SendDate_T" style="display:none;"></asp:TextBox>
<table class="table formtb table_red">
    	<tr>
        	<th style="border-color:#f00;text-align:center;" colspan="2">
            	<h1>鹰潭市教育局发文稿纸</h1>
        	</th>
        </tr>
    <tr>
        <td colspan="2">
            <span>编号：</span>
            <asp:Label class="labelfont" runat="server" ID="bhxx">鹰教信字</asp:Label>
            <asp:Label class="labelfont" runat="server" ID="No_T"></asp:Label><span>号</span>
            <span>保存期限：</span>
            <asp:Label class="labelfont" runat="server" ID="bcqx"></asp:Label>
            <span>归档类目：</span>
            <asp:Label class="labelfont" runat="server" ID="gdlm"></asp:Label>
            <span>等级：</span>
            <asp:Label class="labelfont" runat="server" ID="dj"></asp:Label>
        </td>
    </tr>
        <tr>
            <td style="vertical-align: top; height: 200px;">
                <span>签发：</span>
                <div class="labelfont"><%=GetHQInfo(1) %></div>
            </td>
            <td style="vertical-align: top;">
                 <span>拟稿：</span>
                 <div class="labelfont"><%=GetHQInfo(2) %></div>
            </td>
        </tr>
    <tr>
        <td style="vertical-align: top; height: 200px;"><span>审批：</span>
            <div class="labelfont"><%=GetHQInfo(3) %></div>
        </td>
        <td style="vertical-align: top;">
            <span>核稿：</span>
            <div class="labelfont"><%=GetHQInfo(4) %></div>
        </td>
    </tr>
        <tr>
            <td style="vertical-align:top;height:200px;" colspan="2">
                <span>会签：</span>
                <div class="labelfont"><%=GetHQInfo(5) %></div>
            </td>
        </tr>
        <tr>
        <td colspan="2">
            <div class="media">
                <div class="media-left"><span>标题：</span></div>
                <div class="media-body heimid">
                    <asp:Label CssClass="labelfont" runat="server" ID="Title_T"></asp:Label>
                </div>
            </div>
        </td>
        </tr>
        <tr>
        <td colspan="2">
            <div class="media">
                <div class="media-left"><span>主送：</span></div>
                <div class="media-body heimid">
                    <asp:Label CssClass="labelfont" runat="server" ID="zs"></asp:Label>
                </div>
            </div>
        </td>
        </tr>
        <tr>
        <td colspan="2">
            <div class="media">
                <div class="media-left"><span>抄报：</span></div>
                <div class="media-body heimid">
                     <asp:Label CssClass="labelfont" runat="server" ID="cb"></asp:Label>
                </div>
            </div>
            <div class="media">
                <div class="media-left"><span>抄送：</span></div>
                <div class="media-body heimid">
                     <asp:Label CssClass="labelfont" runat="server" ID="cs"></asp:Label>
                </div>
            </div>
        </td>
        </tr>
        <tr>
        	<td>
            	<span>复核：</span>
                <asp:Label CssClass="labelfont" runat="server" ID="fh"></asp:Label>
            </td>
            <td>
            	<span>校核：</span>
                 <asp:Label CssClass="labelfont" runat="server" ID="xh"></asp:Label>
            </td>
        </tr>
    </table>
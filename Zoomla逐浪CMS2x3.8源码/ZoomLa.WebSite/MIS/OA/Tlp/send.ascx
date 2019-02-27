<%@ Control Language="C#" AutoEventWireup="true" CodeFile="send.ascx.cs" Inherits="MIS_OA_Tlp_send" %>
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
            <asp:DropDownList CssClass="form-control text_x" runat="server" ID="bhxx">
                <asp:ListItem Value="鹰教信字">鹰教信字</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="No_T" disabled="disabled" CssClass="form-control text_md" />
            <span>号</span>
            <button type="button" class="btn btn-default">产生文号</button>
            <button type="button" class="btn btn-default">文号查看</button>
            <span>保存期限</span>
            <asp:DropDownList runat="server" ID="bcqx" CssClass="form-control text_x"></asp:DropDownList>
            <span>归档类目</span>
            <asp:DropDownList runat="server" ID="gdlm" CssClass="form-control text_x"></asp:DropDownList>
        <span>等级</span>
        <asp:DropDownList runat="server" ID="dj" CssClass="form-control text_x"></asp:DropDownList></td>
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
                <div style="color: #666;"><%=GetHQInfo(5) %></div>
            </td>
        </tr>
        <tr>
        <td colspan="2">
            <div class="media">
                <div class="media-left"><span>标题：</span></div>
                <div class="media-body">
                    <asp:TextBox runat="server" ID="Title_T" TextMode="MultiLine" class="form-control" Rows="4"></asp:TextBox>
                </div>
            </div>
        </td>
        </tr>
        <tr>
        <td colspan="2">
            <div class="media">
                <div class="media-left"><span>主送：</span></div>
                <div class="media-body">
                    <asp:TextBox runat="server" ID="zs" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
            </div>
        </td>
        </tr>
        <tr>
        <td colspan="2">
            <div class="media">
                <div class="media-left"><span>抄报：</span></div>
                <div class="media-body">
                    <asp:TextBox runat="server" ID="cb" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
            </div>
            <div class="media">
                <div class="media-left"><span>抄送：</span></div>
                <div class="media-body">
                    <asp:TextBox runat="server" ID="cs" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
            </div>
        </td>
        </tr>
        <tr>
        	<td>
            	<span>复核：</span>
                <asp:TextBox runat="server" ID="fh"  class="form-control text_300"></asp:TextBox>
            </td>
            <td>
            	<span>校核：</span>
                 <asp:TextBox runat="server" ID="xh"  class="form-control text_300"></asp:TextBox>
            </td>
        </tr>
    </table>
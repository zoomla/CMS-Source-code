<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StructList.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Other.StructList" MasterPageFile="~/Common/Master/UserEmpty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>组织结构</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="margin_t10 margin_l5">
    <div id="nodeNav" style="padding: 0 0 0 0;" class="col-lg-2 col-md-2 col-xs-2 col-sm-2">
        <div>
            <div class="tvNavDiv">
                <div class="left_ul">
                    <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
                </div>
            </div>
            <span style="color: green;" runat="server" id="remind" visible="false" />
        </div>
    </div>
    <div class="col-lg-10 col-md-10 col-xs-10 col-sm-10">
        <table id="EGV" class="table table-striped table-bordered table-hover" >
           <tr>
	        <td></td>
	        <td><asp:LinkButton runat="server" ID="ID_A" CommandArgument="AscID" OnClick="OrderBtn_Click">ID<i class='fa fa-arrow-up order_img'></i></asp:LinkButton></td>
	        <td>会员名</td>
	        <td>工号</td>
	      <%--  <td>部门名</td>--%>
	        <td><asp:LinkButton runat="server" ID="Addtime_A" CommandArgument="Addtime" OnClick="OrderBtn_Click">注册时间<i class='fa fa-arrow-up order_img'></i></asp:LinkButton></td>
	        <td><asp:LinkButton runat="server" ID="point_A" CommandArgument="point" OnClick="OrderBtn_Click">积分<i class='fa fa-arrow-up order_img'></i></asp:LinkButton></td>
	        <td><asp:LinkButton runat="server" ID="LoginTimes_A" CommandArgument="LoginTimes" OnClick="OrderBtn_Click">登录次数<i class='fa fa-arrow-up order_img'></i></asp:LinkButton></td>
	        <td><asp:LinkButton runat="server" ID="AuthTime_A" CommandArgument="AscAuthTime" OnClick="OrderBtn_Click">最后登录时间<i class='fa fa-arrow-up order_img'></i></asp:LinkButton></td>
	        <td>状态</td>
          </tr>
           <ZL:ExRepeater runat="server" ID="RPT" PageSize="10" PagePre="<tr><td><input type='checkbox' id='AllID_Chk'></td><td colspan='12'><div class='text-center'>" PageEnd="</div></td></tr>">
	        <ItemTemplate>
	          <tr ondblclick="location='UserInfo.aspx?id=<%# Eval("UserID") %>';">
		        <td><input type="checkbox" name="chkSel" value='<%# Eval("UserID") %>' /></td>
		        <td><%#Eval("UserID") %></td>
		        <td><a href="/user/Info/UserInfo?id=<%# Eval("UserID") %>" target="_blank"><%#Eval("HoneyName") %></a></td>
		        <td><%#Eval("WorkNum") %></td>
		        <td><%#Eval("RegTime","{0:yyyy年MM月dd日 HH:mm}") %></td>
		        <td><%#Eval("UserExp") %></td>
		        <td><%#Eval("LoginTimes") %></td>
		        <td><%#Eval("LastLoginTime","{0:yyyy年MM月dd日 HH:mm}") %></td>
		        <td>
                   <%-- <%#GetStatus(Eval("Status","{0}")) %>--%>
		        </td>
	          </tr>
	        </ItemTemplate>
	        <FooterTemplate></FooterTemplate>
          </ZL:ExRepeater>
        </table>
    </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        .order_img {display:none;}
        #nodeNav .tvNavDiv{width:100%;margin-top:1px;border:1px solid #ddd;border-bottom:none;}
        #nodeNav .tvNav_ul li a{padding-left:1em;}
        #nodeNav .left_ul ul li {border-bottom:1px solid #ddd;}
        #nodeNav .left_ul ul li a{color:#1963aa;display:block;text-decoration:none;height:2.4em;line-height:2.4em; padding-left:8px;}
        #nodeNav .left_ul ul li a:hover{ background:#61b0e9; color:#fff;}
        #nodeNav .activeLi{ background:#61b0e9; color:#fff;border-bottom:1px solid #ddd;}
        #nodeNav .list_span {margin-right:10px;}
        .nav_menu{ top:inherit; left:inherit}
        .SelectedA {color: white;background-color: #5bc0de; }
    </style>
    <script>
        function ShowOrderIcon(id) {
            $("#" + id).find(".order_img").show();
        }
    </script>
</asp:Content>
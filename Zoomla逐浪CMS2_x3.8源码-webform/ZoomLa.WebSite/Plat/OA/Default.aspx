<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Plat_Blog_OA_Default" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style>
.oaul { margin-top: 5px; }
.oaul li { float: left; display: inline-block; cursor: pointer; list-style-type: none; margin-right: 10px; }
.app{padding:5px;}
.cdiv { height:100%; text-align:center; padding-top:35%; }
.cdiv span { color: white; font-size: 1.5em; }
</style>
  <title>OA办公</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
  <div>
    <div class="child_head"> <span class="child_head_span1"></span> <span class="child_head_span2">公文流转</span>
    </div>
      <div>
        <div class="col-md-2 col-sm-4 col-xs-6 app"> 
            <div style="background: #FE7906;" title="公文拟稿" class="cdiv" onclick="location='Drafting.aspx';">
                <span class="fa fa-folder-open"></span><br /> <span>公文拟稿</span>
            </div>
        </div>
          <div class="col-md-2 col-sm-4 col-xs-6 app"> 
            <div style="background: #004B9B;" title="待办公文" class="cdiv" onclick="location='AffairsList.aspx?View=1';">
                <span class="fa fa-file"></span><br /> <span>待办公文</span>
            </div>
        </div>
          <div class="col-md-2 col-sm-4 col-xs-6 app"> 
            <div style="background: #74B512;" title="已办事务" class="cdiv" onclick="location='AffairsList.aspx?View=2';">
                <span class="fa fa-folder"></span><br /> <span>已办事务</span>
            </div>
        </div>
          <div class="col-md-2 col-sm-4 col-xs-6 app"> 
            <div style="background: #A43AE3;" title="发文管理" class="cdiv" onclick="location='SendAffairList.aspx';">
                <span class="fa fa-book"></span><br /> <span>发文管理</span>
            </div>
        </div>
          <div class="col-md-2 col-sm-4 col-xs-6 app"> 
            <div style="background: #22AFC2;" title="收文管理" class="cdiv" onclick="location='AffairsList.aspx?View=3';">
                <span class="fa fa-hdd-o"></span><br /> <span>收文管理</span>
            </div>
        </div>
      </div>
  </div>
</div>
    <script>
        $(function () {
            $("#top_nav_ul li[title='办公']").addClass("active");
            $(".app").height($(".app").width());
            window.onresize = function () {
                $(".app").height($(".app").width());
            }
        });
        setactive("办公");
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>

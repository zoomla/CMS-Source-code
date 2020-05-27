<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rank.aspx.cs" Inherits="ZoomLaCMS.Manage.Workload.Rank" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>分类排行</title>
<style>
.padding0{ padding-left:0; padding-right:0;}
.padding5{ padding-left:5px; padding-right:5px;}
.padding10{ padding-left:10px; padding-right:10px;}
.ranksort{ margin-top:0.5em;}
.ranksort .nav-tabs li a{ padding:0; width:100px; height:36px; line-height:36px; color:#666; text-align:center;}
.ranksort .nav-tabs li a:hover,.ranksort .nav-tabs li a:focus{ outline:none; background:none; border-color:rgba(209, 222, 241, 1);}
.ranksort .nav-tabs .active a,.ranksort .nav-tabs .active a:hover,.ranksort .nav-tabs .active a:focus{ background:#eee; border-color:rgba(209, 222, 241, 1); color:#000;}
.rankbody{ margin-top:0.5em;}
.rankbody_title{ padding-left:0.5em; height:2.6em; line-height:2.6em; font-size:1.2em; box-shadow:0 2px 5px #ddd;}
.rankbody_title i{ margin-right:0.5em; color:#428bca;}
.rankbody_l{ height:500px; background:#f5f5f5; border:1px solid #ddd; box-shadow:1px 1px 5px #eee; border-radius:4px;}
.rankbody_l li{ padding-left:1em; height:2em; line-height:2em;}
.depart_rtitle{ padding:0.3em; min-height:2.6em; background:#f5f5f5; border:1px solid #ddd; border-radius:4px;}
.depart_list{ margin-top:0.5em;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="ranksort">
        <ul class="nav nav-tabs" role="tablist">
        <li><a href="ContentRank.aspx">综合排行</a></li>
        <li class="active"><a href="Rank.aspx?Type=click">点击排行</a></li>
        <li><a href="Rank.aspx?Type=comment">评论排行</a></li>
        <li><a href="#Export">导出</a></li>
        </ul>
    </div>
    <div class="rankbody">
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 padding0 rankbody_l">
        <div class="rankbody_title"><p><i class="fa fa-list"></i>栏目列表</p></div>    
        <ul class="list-unstyled">
        <li><a href="#">的哈设计的哈数</a></li>
        <li><a href="#">的哈设计的哈数</a></li>
        <li><a href="#">的哈设计的哈数</a></li>
        <li><a href="#">的哈设计的哈数</a></li>
        <li><a href="#">的哈设计的哈数</a></li>
        <li><a href="#">的哈设计的哈数</a></li>
        </ul>
        <div class="clearfix"></div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 padding10 rankbody_r">
        <div class="depart_rtitle">        
        <asp:DropDownList ID="ModelList" runat="server" CssClass="form-control" Width="200" DataTextField="ModelName" DataValueField="ModelId"></asp:DropDownList>
        <asp:DropDownList ID="NodeList" runat="server" CssClass="form-control" Width="200" DataTextField="NodeName" DataValueField="NodeId" SelectionMode="Multiple"></asp:DropDownList>
            起始时间：<asp:TextBox ID="start" CssClass="form-control" Width="190" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server"></asp:TextBox> ~
        截止时间：<asp:TextBox ID="end" CssClass="form-control" Width="190" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server"></asp:TextBox>
        <asp:Button ID="Button" runat="server" CssClass="btn btn-md btn-primary"  Text="查询" />           
        </div>
        <div class="depart_list">
        <table class="table table-striped">
        <tr>
        <th>标题</th><th>点击</th><th>录入</th><th>录入时间</th>
        </tr>
        <tbody>
            <tr>
                <td>的话撒娇的哈稍等哈等哈等哈睡地铺</td><td>456</td><td>ncsjsd</td><td>2015-01-01 11:11:11</td> 
                </tr>
            <tr>
                <td>的话撒娇的哈稍等哈等哈等哈睡地铺</td><td>456</td><td>ncsjsd</td><td>2015-01-01 11:11:11</td> 
                </tr>
            <tr>
                <td>的话撒娇的哈稍等哈等哈等哈睡地铺</td><td>456</td><td>ncsjsd</td><td>2015-01-01 11:11:11</td> 
                </tr>
            <tr>
                <td>的话撒娇的哈稍等哈等哈等哈睡地铺</td><td>456</td><td>ncsjsd</td><td>2015-01-01 11:11:11</td> 
                </tr>
            <tr>
                <td>的话撒娇的哈稍等哈等哈等哈睡地铺</td><td>456</td><td>ncsjsd</td><td>2015-01-01 11:11:11</td> 
                </tr>
            <tr>
                <td>的话撒娇的哈稍等哈等哈等哈睡地铺</td><td>456</td><td>ncsjsd</td><td>2015-01-01 11:11:11</td> 
                </tr>
            <tr>
                <td>的话撒娇的哈稍等哈等哈等哈睡地铺</td><td>456</td><td>ncsjsd</td><td>2015-01-01 11:11:11</td> 
                </tr>
        </tbody>
        </table>
        </div>

        </div>







    </div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>


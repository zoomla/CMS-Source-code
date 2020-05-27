<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subject.aspx.cs" Inherits="ZoomLaCMS.Manage.Workload.Subject" MasterPageFile="~/Manage/I/Default.master" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>工作统计</title>
    <style>
    .depart{ min-height:600px; border:1px solid #ddd; background:#eee; border-radius:4px;}
    .depart h4{ padding-left:0.5em; height:2em; line-height:2em; border:1px solid #ddd; background:#f5f5f5; border-radius:4px; color:#428BCA; font-size:1.2em;}
    .depart i{ margin-right:0.5em; color:#0094ff; }
    .panel-body{ padding:0;} 
    .depart .panel-body li{ margin-bottom:-1px; height:2.4em; line-height:2.4em; border:1px solid #eee;} 
    .depart .panel-body li a{ display:block; padding-left:2.2em; background:rgba(247, 247, 247, 1);}
    .depart .panel-body li a:hover{ background:#61B0E9; color:#fff;}
    .depart_rtitle{ padding:0.3em; min-height:2.6em; background:#f5f5f5; border:1px solid #ddd; border-radius:4px;}
    .depart_list{ margin-top:1em;}
    </style>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
        <tr valign="middle">
            <td class="spacingtitle" colspan="2" style="height: 5px; text-align: center">
                <strong>
                    <asp:Label ID="Title_L" runat="server" Text=""></asp:Label></strong>
            </td>
        </tr>
    </table>
    <div id="t1" runat="server">
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 padding5 depart">
        <h4><i class="fa fa-users"></i>用户角色</h4>       
            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">
            <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
            <i class="fa fa-user"></i>管理员组
            </a>
            </div>
            <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel">
            <div class="panel-body">
            <ul class="list-unstyled">
            <asp:Repeater ID="AdminRole" runat="server">
            <ItemTemplate>
                <li><a href="/Admin/Workload/Subject.aspx?Rid=<%# Eval("RoleID") %>"><%# Eval("RoleName") %></a></li>
            </ItemTemplate>
            </asp:Repeater>
            </ul>
            </div>
            </div>
            </div>
            <div class="panel panel-default">
            <div class="panel-heading"> 
            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
            <i class="fa fa-user"></i>会员组
            </a> 
            </div>
            <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel">
            <div class="panel-body">
 
            </div>
            </div>
            </div>



            </div>


        </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 padding5">
        <div class="depart_rtitle">
        起始时间：<asp:TextBox ID="start_T" CssClass="form-control" Width="190" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server"></asp:TextBox> ~
        截止时间：<asp:TextBox ID="end_T" CssClass="form-control" Width="190" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server"></asp:TextBox>
        <asp:DropDownList ID="ModelList" runat="server" CssClass="form-control" Width="200" DataTextField="ModelName" DataValueField="ModelId"></asp:DropDownList>
        <asp:DropDownList ID="NodeList" runat="server" CssClass="form-control" Width="200" DataTextField="NodeName" DataValueField="NodeId" SelectionMode="Multiple"></asp:DropDownList>
        <asp:Button ID="Button" runat="server" CssClass="btn btn-md btn-info" OnClick="count_Click" Text="查询" />
        <asp:Button ID="Export" runat="server" OnClick="Export_Click" CssClass="btn btn-md btn-info" Text="导出" />
        </div>
        <div class="depart_list">
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" >
                <Columns> 
                    <asp:BoundField DataField="Inputer" HeaderText="编辑" />
                    <asp:BoundField DataField="PCount" HeaderText="发稿量"  />
                    <asp:BoundField DataField="Hits" HeaderText="访问量" />
                    <asp:BoundField DataField="ComCount" HeaderText="评论量" />
                    <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="../Content/ContentManage.aspx?KeyType=1&KeyWord=<%#HttpUtility.UrlEncode(Eval("Inputer","")) %>" title="查看">查看</a>
                    </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center"  />
            </ZL:ExGridView>
        </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/GetTable.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>

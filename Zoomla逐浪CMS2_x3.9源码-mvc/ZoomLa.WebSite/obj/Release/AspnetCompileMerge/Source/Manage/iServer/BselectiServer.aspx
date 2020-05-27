<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BselectiServer.aspx.cs" Inherits="ZoomLaCMS.Manage.iServer.BselectiServer"MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>有问必答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-hover" style="border:1px solid #ddd;">
        <tr>
            <td style="width:220px;">
                <span>
                    <asp:Button ID="btnEmailNotice" UseSubmitBehavior="false" CssClass="btn btn-primary" runat="server" Text="启用邮件通知" OnClick="btnEmailNotice_Click" />
                    <asp:Button ID="btnSMSNotice" UseSubmitBehavior="false" runat="server" CssClass="btn btn-primary" Text="启用短信通知" OnClick="btnSMSNotice_Click" />
                </span>
            </td>
            <td>
                <div class="input-group nav_searchDiv" style="position:relative; top:0px; left:20px;">
                    <asp:TextBox runat="server" ID="search_title" class="form-control" placeholder="请输入需要搜索的标题" />
                    <span class="input-group-btn">
                        <%--<asp:LinkButton runat="server" CssClass="btn btn-default" ID="Button1" OnClick="Button1_Click"><span class="fa fa-search"></span></asp:LinkButton>--%>
                        <button class="btn btn-default" type="submit" onserverclick="Button1_Click" runat="server"><span class="fa fa-search"></span></button>
                    </span>
                </div>
            </td>
        </tr>
    </table>
    <div style="margin-bottom: 10px;">
        <asp:DropDownList ID="DropDownList2" CssClass="form-control" Width="150" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Bind">
            <asp:ListItem Value="所有状态">所有状态</asp:ListItem>
            <asp:ListItem Value="未解决">未解决</asp:ListItem>
            <asp:ListItem Value="处理中">处理中</asp:ListItem>
            <asp:ListItem Value="已解决">已解决</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList3" runat="server" Width="150" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="Bind">
            <asp:ListItem Selected="True" Value="所有优先级">所有优先级</asp:ListItem>
            <asp:ListItem Value="低">低</asp:ListItem>
            <asp:ListItem Value="中">中</asp:ListItem>
            <asp:ListItem Value="高">高</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnSelectedIndexChanged="Bind">
            <asp:ListItem Selected="True" Value="所有来源">所有来源</asp:ListItem>
            <asp:ListItem Value="电话">电话</asp:ListItem>
            <asp:ListItem Value="E-mai">E-mail</asp:ListItem>
            <asp:ListItem Value="网页表单">网页表单</asp:ListItem>
            <asp:ListItem Value="直接面谈">直接面谈</asp:ListItem>
            <asp:ListItem Value="其它">其它</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnSelectedIndexChanged="Bind">
            <asp:ListItem Selected="True" Value="0">所有类别</asp:ListItem>      
            <asp:ListItem Value="1">咨询</asp:ListItem>      
            <asp:ListItem Value="2">投诉</asp:ListItem>
            <asp:ListItem Value="3">建议</asp:ListItem>
            <asp:ListItem Value="4">要求</asp:ListItem>
            <asp:ListItem Value="5">界面使用</asp:ListItem>
            <asp:ListItem Value="6">bug报告</asp:ListItem>
            <asp:ListItem Value="7">订单</asp:ListItem>
            <asp:ListItem Value="8">财务</asp:ListItem>
            <asp:ListItem Value="9">域名</asp:ListItem>
            <asp:ListItem Value="10">主机</asp:ListItem>
            <asp:ListItem Value="11">邮局</asp:ListItem>
            <asp:ListItem Value="12">DNS</asp:ListItem>
            <asp:ListItem Value="13">MSSQL</asp:ListItem>
            <asp:ListItem Value="14">MySQL</asp:ListItem>
            <asp:ListItem Value="15">IDC</asp:ListItem>
            <asp:ListItem Value="16">网站推广</asp:ListItem>
            <asp:ListItem Value="17">网站制作</asp:ListItem>
            <asp:ListItem Value="18">其它</asp:ListItem>
        </asp:DropDownList>
    </div>
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="chkSel" value='<%# Eval("QuestionId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="QuestionId" />
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a href="BiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>"><%# ZoomLa.Common.BaseClass.CheckInjection(Eval("Title", "{0}"))%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户名">
                <ItemTemplate>
                    <a data-toggle="modal" data-target="#userinfo_div" onclick="opentitle('<%=customPath2 %>User/Userinfo.aspx?id=<%#Eval("UserId") %>','查看会员')" href="javascript:;" title="查看会员"><%#GetUserName(Eval("UserId","{0}"))%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户类型">
                <ItemTemplate>
                    <%# GetGroupName()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="优先级" DataField="Priority" />
            <asp:BoundField HeaderText="问题类型" DataField="Type" />
            <asp:BoundField HeaderText="已读次数" DataField="ReadCount" />
            <asp:BoundField HeaderText="提交时间" DataField="SubTime" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <span style="color:#f00;"><%# Eval("State")%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="BiServerInfo.aspx?QuestionId=<%#Eval("QuestionId")%>" class="option_style"><i class="fa fa-pencil" title="编辑"></i></a> &nbsp;&nbsp;
                    <asp:LinkButton runat="server" CommandName="Del" CommandArgument='<%#Eval("QuestionId") %>' OnClientClick="return confirm('确认要删除此项?')" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div>
        <asp:Button ID="batDel" class="btn btn-primary" UseSubmitBehavior="false" Text="批量删除" runat="server" OnClientClick="if(!isAnyOneChecked()){alert('未选中任何内容');return false;}else{return confirm('你确定要删除选中内容吗？')}" OnClick="batDel_Click" />
    </div>
    <asp:HiddenField runat="server" ID="scouOrderID" />
    <asp:HiddenField runat="server" ID="hidenmenu" />
    <div class="modal" id="userinfo_div">
        <div class="modal-dialog" style="width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong id="title">用户信息</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="user_ifr" style="width:100%;height:500px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Dialog.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
        })
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=chkSel]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
        function opentitle(url, title) {
            $("#title").text(title);
            $("#user_ifr").attr("src", url);
        }
        function checkAll() {
            xstatus = document.getElementById("cbAll").checked;
            var checkBoxs = document.getElementsByName("Btchk");
            for (i = 0; i < checkBoxs.length; i++) {
                checkBoxs[i].checked = xstatus;
            }

        }
        function isAnyOneChecked() {
            var checkBoxs = document.getElementsByName("chkSel");
            for (i = 0; i < checkBoxs.length; i++) {
                if (checkBoxs[i].checked) return true;
            }
        }
    </script>
</asp:Content>

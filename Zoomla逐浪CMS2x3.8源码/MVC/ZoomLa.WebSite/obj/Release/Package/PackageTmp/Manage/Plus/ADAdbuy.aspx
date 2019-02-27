<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADAdbuy.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.ADAdbuy" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>广告申请</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无相关数据！">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="chkSel" title="" value='<%#Eval("ID") %>' />
                </ItemTemplate>
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="序号" DataField="ID">
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="申请人">
                <ItemTemplate>
                    <a href="../User/UserInfo.aspx?ID=<%# Eval("UID") %>" title="<%# SetName(Eval("UID", "{0}"))%>" ><%# SetName(Eval("UID", "{0}"))%></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="版位名称">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:HyperLink ID="LnkADID" ToolTip='<%# SetZoomName(Eval("ADID", "{0}"))%>' runat="server"><%# SetZoomName(Eval("ADID", "{0}"))%></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="权重">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:HyperLink ID="LnkSales" ToolTip='<%#SalesType(Eval("Scale", "{0}"))%>' runat="server"><%# SalesType(Eval("Scale", "{0}"))%></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="投放时间">
                <ItemTemplate>
                    <asp:HyperLink ID="LnkPtime" ToolTip='<%# SetTime(Eval("Ptime", "{0}"))%>' runat="server"><%# SetTime(Eval("Ptime", "{0}"))%></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请天数">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:HyperLink ID="LnkShowTime" ToolTip='<%# Eval("ShowTime")%>' runat="server"><%# Eval("ShowTime")%></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="广告内容">
                <HeaderStyle />
                <ItemTemplate>
                    <asp:HyperLink ID="LnkContent" ToolTip='<%# ContentType(Eval("Content", "{0}"))%>' runat="server"><%# ContentType(Eval("Content", "{0}"))%></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="附件">
                <HeaderStyle />
                <ItemTemplate><%#LnkFiles(Eval("Files", "{0}"))%> </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请时间">
                <ItemTemplate>
                    <asp:HyperLink ID="LnkTime" ToolTip='<%# SetTime(Eval("Time", "{0}"))%>' runat="server"><%# SetTime(Eval("Time", "{0}"))%></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已审核">
                <HeaderStyle />
                <ItemTemplate><%# (bool)Eval("Audit") == false ? "<span style=\"color: #ff0033\">×</span>" : "√"%> </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="BtnAudit" runat="server" Text="批量审核" OnClientClick="if(!IsSelectedId()){alert('请选择申请');}" class="btn btn-primary" OnClick="BtnAudit_Click" />
    <asp:Button ID="BtnCancel" runat="server" Text="取消审核" OnClientClick="if(!IsSelectedId()){alert('请选择申请');}" class="btn btn-primary" OnClick="BtnCancel_Click" />
    <asp:Button ID="BtnDelete" runat="server" Text="批量删除" OnClientClick="if(!IsSelectedId()){alert('请选择申请');return false;}else{return confirm('你确定要删除选中条目吗？')}" OnClick="BtnDelete_Click" class="btn btn-primary" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
            if (getParam("type")) {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
        })
        //添加专题
        function AddToSpecial() {
            var urlstr = "SpecialList.aspx";
            var isMSIE = (navigator.appName == "Microsoft Internet Explorer");
            var special = null;
            if (isMSIE) {
                special = window.showModalDialog(urlstr, "self,width=200,height=150,resizable=yes,scrollbars=yes");
                if (special != "") {
                    var arr = special.split(',');
                    var odlsp = document.getElementById("HdnSpec").value;
                    var odlarr = odlsp.split(',');
                    var s = true;
                    for (var arri = 0; arri < odlarr.length; arri++) {
                        if (odlarr[arri] == arr[0]) {
                            s = false;
                        }
                    }
                    if (s) {
                        AddRow(arr[1], arr[0], document.all.SpecTable, "此专题", 2);
                        document.getElementById("HdnSpec").value = odlsp + arr[0] + ",";
                    }
                }
            }
            else {
                window.open(urlstr, 'newWin', 'modal=yes,width=200,height=150,resizable=yes,scrollbars=yes');
            }
        }
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=chkSel]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>

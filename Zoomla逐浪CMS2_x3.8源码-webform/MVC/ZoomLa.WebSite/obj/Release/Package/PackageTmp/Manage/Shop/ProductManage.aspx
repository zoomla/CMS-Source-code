<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ProductManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>商品列表</title><style>.popover{max-width:200px;width:181px;}</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
        <li><a href='ProductManage.aspx'><%=Resources.L.商城管理 %></a></li>
        <li class='active'><a href='ProductManage.aspx'><%=Resources.L.商品列表 %></a></li>
        <li class='active'><a href='<%=Request.RawUrl %>'>
            <asp:Label ID="NodeName_L" runat="server"></asp:Label></a></li>
        <div class="pull-right hidden-xs" style="padding-right: 10px;">
            <span><a href="/Class_<%:NodeID<=0?2:NodeID %>/Default.aspx" target="_blank" title="<%=Resources.L.前台浏览 %>"><span class="fa fa-eye"></span></a>
                <span onclick="opentitle('<%:NodeID %>','<%=Resources.L.配置本节点 %>');" class="fa fa-cog" title="<%=Resources.L.配置本节点 %>" style="cursor: pointer; margin-left: 5px;"></span></span>
        </div>
    </ol>
    <div class="clearfix"></div>
    <div class="btn-group" style="margin-top: 2px; margin-bottom: 2px;">
        <a class="btn btn-info" href="ProductManage.aspx?<%:"NodeID="+NodeID+"&StoreID="+StoreID+"&quicksouch=1"%>"><%=Resources.L.商品列表 %></a>
        <a class="btn btn-info" href="ProductManage.aspx?<%:"NodeID="+NodeID+"&StoreID="+StoreID+"&flag=Elite"%>"><%=Resources.L.推荐商品 %></a>
        <a class="btn btn-info" href="ProductManage.aspx?<%:"StoreID="+StoreID+"&quicksouch=15"%>"><%=Resources.L.所有礼品 %></a>
        <a class="btn btn-info" href="ProductManage.aspx?<%:"StoreID="+StoreID+"&quicksouch=16"%>"><%=Resources.L.已审核商品 %></a>
        <a class="btn btn-info" href="ProductManage.aspx?<%:"StoreID="+StoreID+"&quicksouch=17"%>"><%=Resources.L.待审核商品 %></a>
        <a class="btn btn-info" href="PromotionBalance.aspx"><%=Resources.L.推广管理 %></a>
        <a class="btn btn-info" href="ProductManage.aspx?<%:"NodeID="+NodeID+"&StoreID="+StoreID+"&quicksouch=18"%>"><%=Resources.L.用户商品 %></a>
        <a class="btn btn-info" href="ProductManage.aspx?<%:"NodeID="+NodeID+"&StoreID="+StoreID+"&quicksouch=19"%>"><%=Resources.L.合作商品 %></a>
    </div>
    <div class="clearfix"></div>
    <div style="background: #ddd; padding: 4px; margin-bottom: 2px; border-radius: 4px; line-height: 35px; padding-left: 15px;">
        <%=Resources.L.商品操作 %>：<asp:Literal runat="server" ID="lblAddContent"></asp:Literal>
        <span class="pull-right visible-xs visible-sm">
            <input type="button" class="btn btn-success m_modal" onclick="opentitle(<%:NodeID %>,'<%=Resources.L.配置本节点 %>    ');" value="<%=Resources.L.节点选择 %>" /></span>
        <div id="help" class="pull-right text-center pad_r10" style="padding-right: 10px;">
            <span><%=Resources.L.商品数 %>：</span><span class="pad_r10" runat="server" id="countsp"></span>
            <a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a>
        </div>
        <div id="sel_box" style="top: inherit;" class="padding5">
            <div style="display: inline-block;">
                <asp:DropDownList ID="souchtable" CssClass="form-control" Width="120" runat="server">
                    <asp:ListItem Value="0" Text="<%$Resources:L,请选择 %>"></asp:ListItem>
                    <asp:ListItem Value="1" Selected="True" Text="<%$Resources:L,商品名称 %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$Resources:L,商品简介 %>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="<%$Resources:L,商品介绍 %>"></asp:ListItem>
                    <asp:ListItem Value="4" Text="<%$Resources:L,厂商 %>"></asp:ListItem>
                    <asp:ListItem Value="5" Text="<%$Resources:L,品牌商标 %>"></asp:ListItem>
                    <asp:ListItem Value="6" Text="<%$Resources:L,条形码 %>"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="display: inline-block; width: 230px;">
                <div class="input-group" style="position: relative; margin-bottom: -12px;">
                    <asp:TextBox ID="souchkey" placeholder="<%$Resources:L,商品名称 %>" runat="server" CssClass="form-control text_md" />
                    <span class="input-group-btn">
                        <asp:Button ID="souchok" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="souchok_Click" />
                    </span>
                </div>
            </div>
            <div style="display: inline-block;">
                <asp:DropDownList ID="txtbyfilde" CssClass="form-control text_s" runat="server" OnSelectedIndexChanged="txtbyfilde_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="txtbyOrder" CssClass="form-control text_s" runat="server" OnSelectedIndexChanged="txtbyOrder_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div style="display: inline-block;">
                <asp:DropDownList ID="quicksouch" Style="width: 120px;" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="quicksouch_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="<%$Resources:L,所有商品 %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$Resources:L,正在销售的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="<%$Resources:L,未销售的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="4" Text="<%$Resources:L,正常销售的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="5" Text="<%$Resources:L,特价处理的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="6" Text="<%$Resources:L,所有热销的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="7" Text="<%$Resources:L,所有新品 %>"></asp:ListItem>
                    <asp:ListItem Value="8" Text="<%$Resources:L,所有精品商品 %>"></asp:ListItem>
                    <asp:ListItem Value="9" Text="<%$Resources:L,有促销活动的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="10" Text="<%$Resources:L,实际库存报警的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="11" Text="<%$Resources:L,预定库存报警的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="12" Text="<%$Resources:L,已售完的商品 %>"></asp:ListItem>
                    <asp:ListItem Value="14" Text="<%$Resources:L,所有捆绑销售商品 %>"></asp:ListItem>
                    <asp:ListItem Value="15" Text="<%$Resources:L,所有礼品 %>"></asp:ListItem>
                    <asp:ListItem Value="16" Text="<%$Resources:L,已审核商品 %>"></asp:ListItem>
                    <asp:ListItem Value="17" Text="<%$Resources:L,待审核商品 %>"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ShowImport() {
            $("#divImp").show("middle");
        }
    </script>
    <div id="divImp" class="divline">
        <%=Resources.L.导入商城 %>：<ZL:FileUpload runat="server" Style="display: inline;" ID="FileUpload1" />
        <%--<asp:FileUpload ID="FileUpload1" runat="server" style="display:inline;"/>--%>
        <asp:Button ID="btImport" runat="server" Text="<%$Resources:L,导入 %>" OnClick="btImport_Click" class="btn btn-default" />
        <asp:LinkButton ID="lbtnSaveTempter" runat="server" OnClick="lbtnSaveTempter_Click" CausesValidation="true">
            <span style="color: #0E529D;"><%=Resources.L.下载 %><asp:Label runat="server" ID="NodeName_L1"></asp:Label>
                的<asp:Label runat="server" ID="Item_L1"></asp:Label>
                <%=Resources.L.模型CSV导入模板 %></span>
        </asp:LinkButton>(<%=Resources.L.下载后用EXCEL打开从第二行开始按规范填写并保存即可 %>)
    </div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="10" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="<%$Resources:L,无相关数据 %>">
        <Columns>
            <asp:TemplateField HeaderStyle-Width="3%">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value='<%#Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" HeaderStyle-Width="3%" DataField="ID" />
            <asp:TemplateField HeaderText="<%$Resources:L,商品图片 %>">
                <HeaderStyle CssClass="td_m" />
                <ItemTemplate>
                    <a href="ShowProduct.aspx?id=<%#Eval("id")%>">
                        <img src="<%#getproimg() %>" class="img_50" onerror="shownopic(this);" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,商品名称 %>">
                <ItemTemplate>
                    <%#GetNode() %>
                    <a href="ShowProduct.aspx?id=<%#Eval("id")%>"><%#Eval("ProName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="<%$Resources:L,录入者 %>" DataField="AddUser" HeaderStyle-Width="5%" />
            <asp:BoundField HeaderText="<%$Resources:L,单位 %>" DataField="ProUnit" HeaderStyle-Width="5%" />
            <asp:TemplateField HeaderText="<%$Resources:L,库存 %>">
                <ItemTemplate>
                    <%#Stockshow(DataBinder.Eval(Container,"DataItem.id","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,价格 %>">
                <ItemTemplate>
                    <%#GetPrice()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,类型 %>">
                <ItemTemplate>
                    <%#formatnewstype(Eval("ProClass",""))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,商品属性 %>">
                <ItemTemplate>
                    <%#GetProAttr() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,销售中 %>">
                <ItemTemplate>
                    <%#formattype(DataBinder.Eval(Container,"DataItem.Sales","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,状态 %>">
                <ItemTemplate>
                    <%#GetStatus() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,排序 %>">
                <ItemTemplate>
                    <asp:LinkButton ID="UpMove" CssClass="option_style" CommandName="UpMove" CommandArgument='<%# Eval("ID") %>' runat="server">↑<%=Resources.L.上移 %> </asp:LinkButton>
                    <asp:LinkButton ID="DownMove" CssClass="option_style" CommandName="DownMove" CommandArgument='<%# Eval("ID") %>' runat="server"><%=Resources.L.下移 %>↓</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                <ItemTemplate>
                    <a class="precode" href="javascript:;" <%#Eval("istrue").ToString().Equals("1") ? "":"hidden" %> data-href="<%#GetShopUrl() %>" ><i class="fa fa-qrcode"></i></a>
                    <a class="option_style" href="<%#GetShopUrl() %>" target="_blank" title="<%=Resources.L.预览 %>"><i class="fa fa-eye"></i></a>
                    <a class="option_style" href="AddProduct.aspx?menu=edit&ModelID=<%#Eval("ModelID") %>&NodeID=<%#Eval("Nodeid") %>&id=<%#Eval("id")%>"><i class="fa fa-pencil" title="<%=Resources.L.编辑 %>"></i></a>
                    <asp:LinkButton ID="Del1" runat="server" CssClass="option_style" CommandName="Del1" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('确定要将商品移入回收站吗');"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="<%$Resources:L,开始销售 %>" OnClick="Button1_Click" CssClass="btn btn-primary" />
                <asp:Button ID="Button2" runat="server" Text="<%$Resources:L,设为热卖 %>" OnClick="Button2_Click" CssClass="btn btn-primary" />
                <asp:Button ID="Button6" runat="server" Text="<%$Resources:L,设为精品 %>" OnClick="Button6_Click" CssClass="btn btn-primary" />
                <asp:Button ID="Button5" runat="server" Text="<%$Resources:L,设为新品 %>" OnClick="Button5_Click" CssClass="btn btn-primary" />
                <asp:Button ID="Button3" runat="server" Text="<%$Resources:L,批量删除 %>" CssClass="btn btn-primary" OnClick="Button3_Click" OnClientClick="return confirm('确定要将商品移入回收站吗'); " />
                <asp:Button ID="Button4" runat="server" Text="<%$Resources:L,停止销售 %>" OnClick="Button4_Click" CssClass="btn btn-primary" />
                <div style="height: 10px;"></div>
                &nbsp;<asp:Button ID="Button7" runat="server" Text="<%$Resources:L,取消热卖 %>" OnClick="Button7_Click" CssClass="btn btn-primary" />
                <asp:Button ID="Button8" runat="server" Text="<%$Resources:L,取消精品 %>" OnClick="Button8_Click" CssClass="btn btn-primary" />
                <asp:Button ID="Button9" runat="server" Text="<%$Resources:L,取消新品 %>" OnClick="Button9_Click" CssClass="btn btn-primary" />
                <asp:Button ID="Button10" runat="server" Text="<%$Resources:L,批量移动 %>" OnClick="btnMove_Click" UseSubmitBehavior="true" CssClass="btn btn-primary" />
                <asp:Button ID="Button11" runat="server" Text="<%$Resources:L,批量审核 %>" UseSubmitBehavior="true" OnClick="Button11_Click" class="btn btn-primary" />
                <asp:Button ID="Button12" runat="server" Text="<%$Resources:L,取消审核 %>" UseSubmitBehavior="true" OnClick="Button12_Click" class="btn btn-primary" />
                <%--<asp:Button ID="MtrlsMrktng" runat="server" class="C_input" Text="<%$Resources:L,商品推广 %>" onclick="MtrlsMrktng_Click"/>--%>
            </td>
        </tr>
        <tr>
            <td>
                <strong><%=Resources.L.商品属性中的各项含义 %>：</strong>
                <span style="color: blue;"><%=Resources.L.精 %></span>----<%=Resources.L.推荐精品 %>， 
                <span style="color: red;"><%=Resources.L.热 %></span>----<%=Resources.L.热门商品 %>， 
                <span style="color: green;"><%=Resources.L.新 %></span>----<%=Resources.L.推荐新品 %>， 
                <span style="color: blue;"><%=Resources.L.图 %></span>----<%=Resources.L.有商品缩略图 %>， 
                <span style="color: maroon;"><%=Resources.L.绑 %></span>----<%=Resources.L.捆绑商品销售 %>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var diag = new ZL_Dialog();
        function opentitle(nid, title) {
            if (!nid||nid == 0) alert("尚未选择节点");
            else {
                var url = "../Content/EditNode.aspx?NodeID=" + nid;
                diag.url = url;
                diag.title = title;
                diag.ShowModal();
            }
        }
        HideColumn("1,4,5,6,7,8,9,10,11,12");
        $("#sel_btn").click(function (e) {
            if ($("#sel_box").css("display") == "none") {
                $(this).addClass("active");
                $("#sel_box").slideDown(300);
            }
            else {
                $(this).removeClass("active");
                $("#sel_box").slideUp(200);
            }
        });
        $(function(){
            pop.bindEvent($(".precode"));
        });
        var pop = { href:'' };
        pop.bindEvent = function ($items) {
            $items.popover({
                animation: true, placement: 'left', title: "扫描二维码浏览", content: function () {
                    return '<div><img src="/common/common.ashx?url=' + pop.href + '" style="width:150px;height:150px;"></div>';
                }, html: true, trigger: 'manual',
            }).hover(function () {
                var $uinfo = $(this);
                pop.href = $uinfo.data("href");
                $uinfo.popover('show');
            }, function () {
                $(this).popover('hide');
            })
        }
    </script>
    <style>
        .divline{padding: 5px; border: 1px solid rgb(221, 221, 221); border-radius: 4px; margin-bottom: 10px;display:none;}
        .divline li {float: left;margin-left: 8px;}
    </style>
</asp:Content>
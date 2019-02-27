<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAgioCommodity.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.AddAgioCommodity" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择商品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="tr1" runat="server">
        <div style="margin-bottom:10px;">
            <div class="input-group" style="width:300px;">
                <asp:TextBox ID="TxtKeyWord" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button ID="BtnSearch" runat="server" Text="查找" class="btn btn-primary" OnClick="BtnSearch_Click" />
                </span>
            </div>
        </div>
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input name="Item" id="Item<%#Eval("ID") %>" type="checkbox" value="<%#Eval("ID") %>">
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:TemplateField HeaderText="商品图片">
                    <ItemTemplate>
                        <%#getproimg(DataBinder.Eval(Container,"DataItem.Thumbnails","{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品名称">
                    <ItemTemplate>
                        <%#Eval("Proname") %>
                        <input type="hidden" id="Pronames<%#Eval("ID") %>" value="<%#Eval("Proname") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品零售价">
                    <ItemTemplate>
                        <%#Eval("LinPrice","{0:c}")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
    <div id="tr2" runat="server" style="padding-left:20px;">
        <div class="tvNavDiv">
            <div class="left_ul">
                <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </div>
    <asp:Button ID="Add_B" class="btn btn-primary" runat="server" Text="添加商品" />
    <asp:Button ID="Cancel_Add_B" class="btn btn-primary" runat="server" Text="取消添加" OnClientClick="window.close();return false;" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style>
        .tvNav_ul{ padding-left:10px;}
        .tvNav_ul li{ line-height:20px;}
    </style>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type='checkbox' id='chkAll'/>");//EGV顶部
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "Item");
            });
        })
        function GetCheckvalue(cb) {
            var spanChk = window.document.getElementById("CheckBox1");
            var elm = $("input[type=checkbox][name=Item]:checked");
            var hiddenidvalue = opener.document.getElementById("PromoProlist"); //获取已经存在的ID值
            //循环本页选择的项目
            for (i = 0; i < elm.length; i++) {
                if (elm[i].checked == true) {
                    var tempvalue = "," + elm[i].value + ",";//初始ID
                    var Pronamesvalue = document.getElementById("Pronames" + elm[i].value).value;
                    //循环父页的Option值;
                    var addthis = true;
                    if (hiddenidvalue.options.length > 0) {
                        for (var ii = hiddenidvalue.options.length - 1; ii >= 0; ii--) {
                            if (hiddenidvalue[ii].text == document.getElementById("Pronames" + elm[i].value).value) {
                                addthis = addthis && false;
                            }
                        }

                        if (addthis == true) {
                            opener.$("#PromoProlist").append("<option value='" + elm[i].value + "'>" + Pronamesvalue + "</option>");
                        }
                    }
                    else {
                        opener.$("#PromoProlist").append("<option value='" + elm[i].value + "'>" + Pronamesvalue + "</option>");
                    }
                }
            }
            opener.$("#PromoProlist").find("option").attr("selected", true);
            window.close();
        }
        function GetCheckNode() {
            var elm = $("input[type=checkbox][name=Item]:checked");
            var hiddenidvalue = opener.document.getElementById("PromoProlist"); //获取已经存在的ID值
            //循环本页选择的项目
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox") {
                    if (elm[i].checked == true) {
                        var tempvalue = "," + elm[i].value + ",";//初始ID
                        var Pronamesvalue = document.getElementById("Pronames" + elm[i].value).value;
                        //window.alert(document.getElementById("Pronames" + elm[i].value).value);
                        //循环父页的Option值;
                        var addthis = true;
                        if (hiddenidvalue.options.length > 0) {
                            for (var ii = hiddenidvalue.options.length - 1; ii >= 0; ii--) {
                                if (hiddenidvalue[ii].text == document.getElementById("Pronames" + elm[i].value).value) {
                                    addthis = addthis && false;
                                }
                            }
                            if (addthis == true) {
                                opener.$("#PromoProlist").append("<option value='" + elm[i].value + "'>" + Pronamesvalue + "</option>");
                            }
                        }
                        else {
                            opener.$("#PromoProlist").append("<option value='" + elm[i].value + "'>" + Pronamesvalue + "</option>");
                        }
                    }
                }
            }
            opener.$("#PromoProlist").find("option").attr("selected", true);
            window.close();
        }
        function SetNodeSelect(spanChk,pid)
        {
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = $("input[type=checkbox][name=Item]");
            var len;
            for (var i = 0; i < elm.length; i++) {
                var x = elm[i].id;
                var len = "Item_" + pid + "_";
                if (elm[i].type == "checkbox" && x.indexOf("Item_" + pid + "_") > -1 && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState) {
                        elm[i].checked = xState;
                        
                    }
                    SetNodeSelect(elm[i], elm[i].id.substring(len.length));
                }
            }
        }
        $().ready(function () {
            $(".tvNav a.list1").click(function () { showList(this); });
            $(".tvNav  a.list1 .NodeP_Span").click(function ()
            {
                window.event.cancelBubble = true;//停止冒泡
                window.event.returnValue = false;//阻止事件的默认行为
                showList($(this).parent());
            });
        })
        function showList(obj)
        {
            $(obj).parent().parent().find("a").removeClass("activeLi");//a-->li-->ul
            $(obj).parent().children("a").addClass("activeLi");//li
            $(obj).parent().siblings("li").find("ul").slideUp();
            $(obj).parent().children("ul").slideToggle();
        }
    </script>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectionStep3.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Collect.CollectionStep3" MasterPageFile="~/Manage/I/Default.master" ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>采集项目</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%--    <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover" runat="server">
        <ItemTemplate>
            <%#GetHtml(Eval("FieldAlias","{0}"),Eval("FieldName","{0}"),Eval("FieldType","{0}"),Eval("Content","{0}"),Eval("Description","{0}"), Eval("ModelID","{0}"))%>
        </ItemTemplate>
    </asp:DataList>
    <asp:DataList ID="DataList2" CssClass="table table-striped table-bordered table-hover" runat="server">
        <ItemTemplate>
            <%#GetHtml(Eval("FieldAlias","{0}"),Eval("FieldName","{0}"),Eval("FieldType","{0}"),Eval("Content","{0}"),Eval("Description","{0}"), Eval("ModelID","{0}"))%>
        </ItemTemplate>
    </asp:DataList>--%>
    <table class="table table-bordered table-striped" style="margin-bottom:30px;">
        <tr><td colspan="3" class="text-center"><strong>系统字段</strong></td></tr>
        <tr><td class="td_l">字段名称</td><td class="td_m">字段类型</td><td>赋值规则</td></tr>
        <asp:Repeater runat="server" ID="SYS_RPT">
            <ItemTemplate>
                <tr id="item_<%#Eval("FieldName") %>" data-field="<%#Eval("FieldName") %>" class="item_field"><td><%#Eval("FieldAlias") %></td>
                    <td><%#Eval("FieldType") %></td>
                    <td>
                    <label><input type="radio" name="<%#Eval("FieldName")+"_rad" %>" class="colltype_rad" value="1" checked="checked"/>使用默认值</label>
                    <label><input type="radio" name="<%#Eval("FieldName")+"_rad" %>" class="colltype_rad" value="2" />指定值 <input type="text" class="form-control text_300 collvalue_t"/></label>
                    <label><input type="radio" name="<%#Eval("FieldName")+"_rad" %>" class="colltype_rad" value="3" /><input type="button" class="btn btn-info" value="使用采集规则" onclick="showpage('<%#ItemID%>    ','<%#Eval("FieldName") %>    ','<%#Server.UrlEncode(Eval("FieldAlias",""))%>    ');" /></label>
                        <input type="hidden" class="collvalue_hid" id="<%#Eval("FieldName") %>_hid" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table class="table table-bordered table-striped">
        <tr><td colspan="3" class="text-center"><strong>扩展字段</strong></td></tr>
        <tr><td class="td_l">字段名称</td><td class="td_m">字段类型</td><td>赋值规则</td></tr>
        <asp:Repeater runat="server" ID="RPT">
            <ItemTemplate>
                <tr id="item_<%#Eval("FieldName") %>" data-field="<%#Eval("FieldName") %>" class="item_field"><td><%#Eval("FieldAlias") %></td>
                    <td><%#Eval("FieldType") %></td>
                    <td>
                    <label><input type="radio" name="<%#Eval("FieldName")+"_rad" %>" class="colltype_rad" value="1" checked="checked"/>使用默认值</label>
                    <label><input type="radio" name="<%#Eval("FieldName")+"_rad" %>" class="colltype_rad" value="2" />指定值 <input type="text" class="form-control text_300 collvalue_t"/></label>
                    <label><input type="radio" name="<%#Eval("FieldName")+"_rad" %>" class="colltype_rad" value="3" /><input type="button" class="btn btn-info" value="使用采集规则" onclick="showpage('<%#ItemID%>','<%#Eval("FieldName") %>','<%#Server.UrlEncode(Eval("FieldAlias",""))%>');" /></label>
                        <input type="hidden" class="collvalue_hid" id="<%#Eval("FieldName") %>_hid" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="text-center Conent_fix">
        <a href="CollectionStep2.aspx?itemid=<%:ItemID %>" class="btn btn-primary">上一步</a>
        <input type="button" value="保存信息" class="btn btn-primary" onclick="saveinfo();" />
        <asp:Button ID="Save_Btn" runat="server" OnClick="Save_Btn_Click" style="display:none;" />
    </div>
<div style="height:50px;"></div>
<asp:HiddenField runat="server" ID="InfoPage_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<style type="text/css">
    label{font-weight:normal;cursor:pointer;}
</style>
<script>
    var diag = new ZL_Dialog();
    diag.title = "字段采集规则";
    diag.maxbtn = false;
    diag.backdrop = true;
    diag.width = "width1100";
    //存储设定的正则规范
    function SaveConfig(name,json) {
        name=name.replace(/ /g,"");
        document.getElementById(name+"_hid").value = json;
    }
    function showpage(itemid, name, alias) {
        $("#item_"+name).find(":radio[value=3]")[0].checked=true;
        diag.url = "CollectionInfoPage.aspx?ItemID=" + itemid + "&Name=" + name + "&Alias=" + alias;
        diag.ShowModal();
    }
    function Close()
    {
        diag.CloseModal();
    }
    function saveinfo()
    {
        var list=[];
        var $items= $(".item_field");
        if($items.length<1){alert("该模型未指定字段");return;}
        for (var i = 0; i < $items.length; i++) {
            var $item=$($items[i]);
            var model={field:$item.data("field"),colltype:"",collvalue:""};
            model.colltype=$item.find(".colltype_rad:checked").val();
            switch(model.colltype)
            {
                case "1":
                    break;
                case "2":
                    model.collvalue=$item.find(".collvalue_t").val();
                    break;
                case "3":
                    model.collvalue=$item.find(".collvalue_hid").val();
                    break;
            }
            list.push(model);
        }
        $("#InfoPage_Hid").val(JSON.stringify(list));
        $("#Save_Btn").click();
    }
    $(function(){
        if($("#InfoPage_Hid").val()!="")
        {
            var list=JSON.parse(JSON.parse($("#InfoPage_Hid").val()));
            for (var i = 0; i < list.length; i++) {
                var model=list[i];
                var $item=$("#item_"+model.field);
                $item.find(".colltype_rad[value='"+model.colltype+"']")[0].checked=true;
                console.log($item.length,"#item_"+model.field,list[i]);
                switch(model.colltype)
                {
                    case "2":
                        $item.find(".collvalue_t").val(model.collvalue);
                        break;
                    case "3":
                        $item.find(".collvalue_hid").val(model.collvalue);
                        break;
                }
            }

        }
    })
</script>
</asp:Content>

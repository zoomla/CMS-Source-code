<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelModelFieds.aspx.cs" Inherits="ZoomLaCMS.Manage.WorkFlow.SelModelFieds" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择模型</title>
    <style>
        .list-group-item:hover{background-color:#ccc!important;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container-fluid text-right option"><button type="button" class="btn btn-default selall">全选</button> <button type="button" class="btn btn-default clearall">清空</button></div>
    <div class="container-fluid margin_t5">
        <div class="list-group">
          <ZL:Repeater ID="RPT" runat="server">
              <ItemTemplate>
                  <a href="javascript:;"  onclick="parent.GetModelID('<%=Request.QueryString["cid"] %>','<%#Eval("ModelID") %>')" class="list-group-item"><span class="<%#Eval("ItemIcon") %>"></span> <strong><%# Eval("ModelName")%></strong></a>
              </ItemTemplate>
          </ZL:Repeater>
            <ZL:Repeater ID="Fied_RPT" runat="server">
              <ItemTemplate>
                  <a href="javascript:;"  class="list-group-item selfield"><%#Eval("FieldName")%>(<%#Eval("FieldAlias")%>)<input name="fields" style="display:none;" value="<%#Eval("FieldName")%>" type="checkbox" /><span class="badge" style="display:none;"><span class="fa fa-check"></span></span></a>
              </ItemTemplate>
          </ZL:Repeater>
        </div>
        <div class="text-center">
            <button type="button" onclick="parent.CloseDiag()" class="btn btn-primary">确定</button>
        </div>
    </div>
    <script>
        var cid = '<%=Request.QueryString["cid"] %>';
        $().ready(function () {
            BindEvent();
            BindData();
        });
        function SelAll(flag) {
            $("[name='fields']").each(function (fi, fv) {
                fv.checked = flag;
                ChangeData(fv);
            });
        }
        function BindData() {
            $parent = $(parent.document);
            var ids = $parent.find("#" + cid).val()
            if (ids == "*") {//*号全选
                SelAll(true);
                return;
            }
            if (ids != "") {
                var arr = ids.split(',');
                for (var i = 0; i < arr.length; i++) {
                    $("[name='fields']").each(function (fi, fv) {
                        if ($(fv).val() == arr[i])
                            fv.checked = true;
                            ChangeData(fv);
                    });
                }
            }
            
        }
        function BindEvent() {
            if ($("[name='fields']")[0]){//判断是否是选择字段模式
                $(".option").show();
                $(".selall").click(function () { SelAll(true); });
                $(".clearall").click(function () { SelAll(false)});
            }
            else
                $(".option").hide();
            $(".selfield").click(function () { $(this).find("input").click(); ChangeData($(this).find("input")[0]); });
            
        }
        function ChangeData(checkbox) {
            if (checkbox.checked) {
                $(checkbox).next().show();
            } else {
                $(checkbox).next().hide();
            }
            SetParentData();
        }
        function SetParentData() {
            $parent = $(parent.document);
            var ids = "";
            $("[name='fields']:checked").each(function (i,v) {
                ids += $(v).val();
                if (i < ($("[name='fields']:checked").length - 1))
                    ids += ',';
            });
            $parent.find("#" + cid).val(ids);
        }
    </script>
</asp:Content>

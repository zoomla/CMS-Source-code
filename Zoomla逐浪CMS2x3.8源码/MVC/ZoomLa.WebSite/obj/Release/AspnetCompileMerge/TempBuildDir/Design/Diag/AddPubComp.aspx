<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPubComp.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.AddPubComp" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>添加互动</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="publist">
    <div class="item" data-type="pub_input" data-comp="text">文本输入框</div>
    <div class="item" data-type="pub_input" data-comp="textarea">多行文本框</div>
<%--<div class="item" data-type="pub_select" data-comp="radio">单选框</div>
    <div class="item" data-type="pub_select" data-comp="checkbox">多选框</div>--%>
    <div class="item" data-type="pub_select" data-comp="select">下拉菜单</div>
    <div class="item" data-type="pub_button" data-comp="button">提交按钮</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.publist {}
.publist .item {border:1px dashed #ddd;padding:5px;margin-bottom:5px;margin-right:5px;cursor:pointer;}
.publist .item:hover {background-color:#D0E2F3;border-color:#57A5CA;border-style:solid;}
.publist .item {transition-property: all;transition-duration: 200ms;transition-delay: 0;transition-timing-function: ease-in-out;-webkit-transition-property: all;-webkit-transition-duration: 200ms;-webkit-transition-delay: 0;-webkit-transition-timing-function: ease-in-out;-moz-transition-property: all;-moz-transition-duration: 200ms;-moz-transition-delay: 0;-moz-transition-timing-function: ease-in-out;-o-transition-property: all;-o-transition-duration: 200ms;-o-transition-delay: 0;-o-transition-timing-function: ease-in-out;}
</style>
<script>
    $(function () {
        $(".item").click(function () {
            var $item = $(this);
            var model = {
                dataMod: {},
                config: { type: $item.data("type"), compid: $item.data("comp"), css: "candrag", style: 'position:absolute;width:400px;height:80px;', regex: "" }
            };
            //name作为总选项名,可作为text的placeholder
            switch (model.config.type)//根据不同的选项,加载初始数据
            {
                case "pub_input":
                    model.config.name = "文本";
                    if (model.config.compid == "textarea") { model.config.style = 'position:absolute;width:400px;height:200px;'; }
                    break;
                case "pub_select":
                    model.config.name = "下拉";
                    model.dataMod.list = [
                        { text: "选项1", value: "选项1" },
                        { text: "选项2", value: "选项2" },
                        { text: "选项3", value: "选项3" }
                    ];
                    break;
                case "pub_button":
                    model.config.fname = "信息表单";//表单名称
                    model.dataMod.value = "提交";
                    model.config.btnstyle = "width:100%;height:80px;font-size:40px;";//为高度的一半
                    model.dataMod.click = { type: "5",prompt:"感谢你提交的信息" };
                    break;
            }
            parent.AddComponent(model);
        });
    })
</script>
</asp:Content>
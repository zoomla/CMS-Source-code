<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WxMaterial.aspx.cs" Inherits="Manage_WeiXin_WxMaterial" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>素材管理</title>
<style>
    .table td{vertical-align:middle !important;}
    .materlist .infolist{border:1px solid #ddd; padding:0; text-align:center;}
    .materlist .infolist img {width: 100%;border-bottom: 1px solid #ddd;}
    .materlist .infolist .title{line-height:25px;border-bottom:1px solid #ddd; background:none;color:#999;word-wrap:break-word;}
    .materlist .infolist .option{color:#999; line-height:25px;}
    .materlist .newslist {border: 1px solid #ddd;line-height:25px; padding:0px; cursor:pointer; position:relative;}
     .materlist .newslist .info{padding:10px;}
     .materlist .newslist .share_alt{position:absolute; right:5px; top:-2px; font-size:16px;}
    .materlist .newslist img{width:100%; height:100px;}
    .materlist .newslist .option div{color:#999;float:left;width:50%; line-height:40px; text-align:center; border-top:1px solid #ddd;}
    .materlist .newslist .option div:first-child{ margin-left:-1px; border-right: 1px solid #ddd;}
    .materlist .newslist .info .title{color:white; background-color:#777; padding:5px; font-weight:normal;}
    .materlist .newslist .sub_info{border-top:1px solid #ddd; padding:10px;}
    .materlist .newslist .sub_info div{width:30%; float:left;}
     .materlist .newslist .sub_info div:first-child{width:70%;}
    .materlist .newslist .sub_info div img{width:60px; height:60px;}
    .materlist .newslist:hover{ box-shadow:0 0 3px 1px #ddd;}
    .materlist .active{ border:2px solid #ff0000;}
    .materlist .active:hover{border:2px solid #ff0000;}
    .wxlist li{cursor:pointer;}
    .wxlist li .badge{display:none;}
    .wxlist li .active{display:inline-block;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container-fluid">
    <div class="row">
        <div class="col-md-2 col-sm-12">
                <div class="panel panel-primary">
                  <!-- Default panel contents -->
                  <div class="panel-heading">选择公众号</div>
                  <ul class="list-group wxlist">
                      <asp:Repeater ID="WxApp_RPT" runat="server">
                          <ItemTemplate>
                                <li class="list-group-item wx_option" data-appid="<%#Eval("ID") %>">
                                    <a href="WxMaterial.aspx?type=news&appid=<%#Eval("ID") %>"><%#Eval("Alias") %> <span class="badge pull-right"><span class="fa fa-check"></span></span></a> 
                                </li>
                          </ItemTemplate>
                      </asp:Repeater>
                  </ul>
                </div>
            </div>
        <div class="col-md-10 col-sm-12">
            <div>
    <ul class="nav nav-tabs" role="tablist">
    <li id="news_tab"><a href="WxMaterial.aspx?type=news&appid=<%=AppId %>">图文素材</a></li>
    <li id="image_tab"><a href="WxMaterial.aspx?type=image&appid=<%=AppId %>">图片素材</a></li>
  </ul>
<div class="panel panel-default">
  <div class="panel-heading"><label>素材管理</label> </div>
  <div class="panel-body">
    <div class="container-fluid materlist">
        <div class="rows" id="otherList">
            <ZL:ExRepeater ID="OtherRPT" runat="server" PageSize="12" PagePre="<div class='clearfix'></div><div class='panel-footer text-center'>" PageEnd="</div>">
                <ItemTemplate>
                    <div class="col-md-2 listdata margin_t10">
                        <div class="container-fluid infolist">
                            <img src="<%#Eval("url") %>" />
                            <div class="title"><label><input type="checkbox" value="<%#Eval("media_id") %>" /><%#Eval("name") %></label></div>
                            <div class="title"><%#Eval("media_id") %></div>
                            <div class="option"><a href="javascript:;" onclick="DelMater(this,'<%#Eval("media_id") %>')"><span class="fa fa-trash-o"></span></a></div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
            <ZL:ExRepeater ID="NewsRPT" runat="server" OnItemDataBound="NewsRPT_ItemDataBound" PageSize="12" PagePre="<div class='clearfix'></div><div class='panel-footer text-center'>" PageEnd="</div>">
                <ItemTemplate>
                    <div class="col-md-2 listdata margin_t10">
                        <div class="container-fluid newslist" data-appid="<%#Eval("media_id") %>">
                            <div class="share_alt"><a href="<%#GetPreUrl() %>" target="_blank"><span class="fa fa-eye"></span></a> <a href="MsgsSend.aspx?action=share&appid=<%=AppId %>&media_id=<%#Eval("media_id") %>" title="引用素材"><span class="fa fa-share-alt"></span></a> </div>
                            <asp:Literal ID="Infos_Li" runat="server" EnableViewState="false"></asp:Literal>
                            <div class="option">
                                <div><a href="MsgsSend.aspx?media_id=<%#Eval("media_id") %>&appid=<%=AppId %>" title="修改素材"><span class="fa fa-pencil"></span></a></div>
                                <div><a href="javascript:;" onclick="DelMater(this,'<%#Eval("media_id") %>')" title="删除素材"><span class="fa fa-trash-o"></span></a></div>
                            </div> 
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
            <div id="Empty_Div" class="alert alert-info" style="display:none;" role="alert"><span class="fa fa-spinner fa-spin"></span> 正在加载..</div>
        </div>
    </div>
  </div>
</div>
</div>
        </div>
    </div>
</div>


<div class="text-center" id="selnews_div" style="display:none;"><button type="button" class="btn btn-primary">确定</button> <button type="button" class="btn btn-primary">取消</button></div>
<%--<button class="btn btn-info" type="button" onclick="$('#ImgFile_Up').click();">上传图片</button>--%>
<%--<asp:FileUpload ID="ImgFile_Up" runat="server" style="display:none;" />--%>
<ZL:FileUpload ID="ImgFile_Up" runat="server" OnlyImg="true"  style="display:none;" />
<asp:Button ID="ImgFile_B" runat="server" style="display:none;" OnClick="ImgFile_B_Click" />
<asp:Button ID="BindData_B" runat="server" style="display:none;" OnClick="BindData_B_Click" />
<asp:HiddenField ID="Items_value" runat="server" Value="" />
<asp:HiddenField ID="Datas_Hid" runat="server" />
<script type="text/javascript">
    var objdatas = {};
    var type ='<%=Type %>';//获取素材类型
    $().ready(function () {
        BrNews();
        objdatas = JSON.parse($("#Datas_Hid").val());
        if ($("#Items_value").val() == "") {
            $("#Empty_Div").show();
            if (!objdatas.item)
                $("#Items_value").val(JSON.stringify(objdatas));
            else if (type == "news")
                $("#Items_value").val(JSON.stringify(ToSubNewsData(objdatas.item)));
            else
                $("#Items_value").val(JSON.stringify(objdatas.item));

            $("#BindData_B").click();
        }
        $("#ImgFile_Up").change(function () {//上传图片事件
            $("#ImgFile_B").click();
        });
        //InitDatas();//初始化素材列表
        $(".infolist img").height($("img").width() * 1.1);
        if (type == "news")
            $("#news_tab").addClass('active');
        else
            $("#image_tab").addClass('active');
        //当前选中公众号
        $("[data-appid='<%=AppId %>'] .badge").show();
    });
    //function SelMode() {
    //    if (parent.SelMaterial) {
    //        $("#selnews_div").show();
    //    }
    //}
    //function SelNews() {
    //    if ($(".newslist.active")[0]) {
    //        parent.SelMaterial($(".newslist.active").data("appid"));
    //    } else {
    //        parent.CloseComDiag();
    //    }
        
    //}
    //对图文列表进行排位(以防样式错乱)
    function BrNews() {
        $(".listdata").each(function (i, v) {
            if (i%6==0) {
                $("<div class='clearfix'></div>").insertBefore(v);
            }
        });
    }
    //itemdata:素材数组
    function ToSubNewsData(itemdata) {//将多图文json转换为后台可解析形式
        var datas = [];
        for (var i = 0; i < itemdata.length; i++) {
            datas.push(itemdata[i]);
            datas[i].saveimg = 0;//是否同步图片url
            datas[i].content = JSON.stringify(datas[i].content.news_item);
        }
        return datas;
    }
    
    //删除
    function DelMater(obj,media_id) {
        if (confirm("确定删除该素材吗?")) {
            $.ajax({
                type: 'POST',
                url:'WxMaterial.aspx?appid=<%=AppId %>',
                data: { action: 'del', media_id: media_id,type:'<%=NodeName %>',appid:'<%=AppId %>' },
                success: function (data) {
                    var result = JSON.parse(data);
                    if (result.errcode==0) {
                        $(obj).closest('.listdata').remove();
                    } else {
                        alert("删除失败,错误信息:" + data);
                    }
                }
            });
        }
    }
</script>
</asp:Content>

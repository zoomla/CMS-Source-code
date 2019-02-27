<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LabelGuide.ascx.cs" Inherits="Manage_I_ASCX_LabelGuide" %>
<div class="menu_tit" style="border-bottom:1px solid #ddd"><span class="fa fa-chevron-down"></span><%=Resources.L.标签管理 %></div>
<div class="tvNavDiv" style="height:auto;">
    <div class="left_ul">
        <ul>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/TemplateSet.aspx');"><%=Resources.L.方案设置 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/TemplateManage.aspx')"><%=Resources.L.模板管理 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/LabelManage.aspx')"><%=Resources.L.标签管理 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/CSSManage.aspx')"><%=Resources.L.风格管理 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/TemplateSetOfficial.aspx')"><%=Resources.L.云端下载 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/LabelCallTab.aspx')"><%=Resources.L.标签助手 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/ExternDS/DSList.aspx')"><%=Resources.L.外部数据源 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Template/GetPageHtml.aspx')"><%=Resources.L.源码查看器 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Content/Addon/MNBakList.aspx')"><%=Resources.L.元数据备份 %></a></li>
            <li><a href="javascript:;" onclick="ShowMain('','{$path}Design/Addon/Restore.aspx')">加载动力版</a></li>
        </ul>
        <div class="clearfix"></div>
    </div>
     <div class="input-group margintop10" >
        <input type="text" id="keyWord" class="form-control ascx_key" placeholder="<%=Resources.L.请输入标签名称 %>"  onkeydown="return ASCX.OnEnterSearch('<%:CustomerPageAction.customPath2+"Template/LabelManage.aspx?keyWord=" %>',this);"/>
        <span class="input-group-btn">
            <button class="btn btn-default" type="button" onclick="ASCX.Search('<%:CustomerPageAction.customPath2+"Template/LabelManage.aspx?keyWord=" %>','keyWord');"><span class="fa fa-search"></span></button>
        </span>
    </div>

</div>   

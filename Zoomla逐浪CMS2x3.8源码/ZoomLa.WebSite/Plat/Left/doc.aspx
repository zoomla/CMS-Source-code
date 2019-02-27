<%@ Page Language="C#" AutoEventWireup="true" CodeFile="doc.aspx.cs" Inherits="Plat_Left_doc" EnableViewState="false"%>
<ul class="listul">
    <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
        <ItemTemplate>
            <li title="点击预览" onclick="leftnav.preview('<%#Eval("Guid") %>');">
                <div>
                    <span class="icon"><%#GroupPic.GetExtNameMini(GetExt()) %></span>
                    <span class="icon_txt"><%#Eval("FileName") %></span>
                </div>
                <div class="r_gray">[<%#Eval("HoneyName") %>] <i class="fa fa-calendar"></i> <%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></div>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
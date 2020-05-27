<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DomTempMange.aspx.cs" Inherits="Manage_Site_DomTempMange" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<title>模板管理</title>
<script type="text/javascript" src="/Plugins/Domain/Site.js"></script>
<script type="text/javascript">
    function Showtemplatediv()
    {
        var dr = document.getElementById("templatediv");
        dr.style.display = "block";
    }
</script>
<style type="text/css">
#site_main #tab3 td,#site_main #tab3 th,#site_main #fileList th,#site_main #fileList td{ height:45px; line-height:45px;}
#site_main #tab3 td,#site_main #fileList td{ border-bottom:1px solid #ccc;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="site_main" style="margin-top:15px;">
     <div id="tab3">
        <div class="input-group text_300">
        <asp:TextBox runat="server" ID="searchText" CssClass="form-control " />
        <span class="input-group-btn">
        <asp:Button runat="server" ID="searchBtn" Text="搜索" CssClass="btn btn-primary"/>
        </span>
        </div> 
          <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" RowStyle-CssClass="tdbg" OnRowCommand="EGV_RowCommand" OnPageIndexChanging="mimeEGV_PageIndexChanging" EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1"  Width="98%" EmptyDataText ="没有任何数据！" IsHoldState="false">
                <Columns>
                  <asp:BoundField HeaderText="ID" DataField="ID" ReadOnly="true" />
                  <asp:TemplateField HeaderText="模板名">
                      <ItemTemplate>
                        <a href="#" title="点击浏览详情"><%#Eval("TempName") %></a>  
                      </ItemTemplate>
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="所属用户">
                        <ItemTemplate>
                         <a href="#" title="点击查看用户信息" > <%#Eval("OwnUserID") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="操作">
                         <ItemTemplate>
                      <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Edit2">修改</asp:LinkButton>
                      <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Del2">删除</asp:LinkButton>
                  </ItemTemplate>
                  <EditItemTemplate>
                      <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Renewals"  >确定</asp:LinkButton>
                      <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Cancel">取消</asp:LinkButton>
                  </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
          <PagerStyle HorizontalAlign="Center" />
          <RowStyle Height="45px" HorizontalAlign="Center"/>
        </ZL:ExGridView> 
     </div>
        <div id="templatediv" style="margin-bottom:10px; margin-top:10px; display:none;">
        <table id="templateTable"  style="margin:auto;">
        <tr><td>模板名：</td><td><span class="redStar">*&nbsp;</span><input type="text" id="tempName" name="tempName"  class="site_input" size="30" />
            <asp:DropDownList runat="server" ID="tempListDP" AutoPostBack="true" OnSelectedIndexChanged="tempListDP_SelectedIndexChanged"></asp:DropDownList> </td></tr>
        <tr>
            <td>单位名称（中文名）：</td>
            <td><span class="redStar">*&nbsp;</span><input id="uname1" type="text" class="site_input" size="30" name="uname1" /></td>
        </tr>
        <tr>
            <td>单位名称（英文名）：</td>
            <td><span class="redStar">*&nbsp;</span><input id="uname2" type="text" class="site_input" size="30" name="uname2"  /></td>
        </tr>
        <tr class="CNAddr1">
            <td></td>
            <td style="color: red;">联系人中文名中至少含有1个中文字符，英文名信息中名和姓必须以空格分开。</td>
        </tr>
        <tr>
            <td>联系人（中文名）：</td>
            <td><span class="redStar">*&nbsp;</span><input id="rname1" type="text" class="site_input" size="30" name="rname1" /></td>
        </tr>
        <tr>
            <td>联系人（英文名）：</td>
            <td><span class="redStar">*&nbsp;</span><input id="rname2" type="text" class="site_input" size="30" name="rname2" /></td>
        </tr>
        <%--<tr class="CNAddr">
            <td>URL指向:</td>
            <td>
                <span class="redStar">*</span>
                <input id="urlId" type="text" class="site_input" size="30" name="url" value="http://www." />
            </td>
        </tr>--%>
        <tr>
            <td>电子邮箱：</td>
            <td><span class="redStar">*&nbsp;</span><input id="aemail" type="text" class="site_input" size="30" name="aemail"  /></td>
        </tr>
        <tr>
            <td>所属区域：</td>
            <td><span class="redStar">*</span>
                 <asp:DropDownList ID="DropDownList1" runat="server" class="dpclass"><asp:ListItem Value="01">中国</asp:ListItem></asp:DropDownList><br />
                 <span class="redStar">*</span>
                 <asp:DropDownList runat="server" ID="prvinceDP" ClientIDMode="Static"  class="dpclass"></asp:DropDownList><br />
                 <span class="redStar">*</span>
                 <input id="cityText" name="cityText" type="text"  class="site_input"/>
            </td>
        </tr>
        <tr class="CNAddr3">
            <td></td>
            <td style="color: red;">通迅地址（中文）信息中必须至少含有1个中文字符</td>
        </tr>
        <tr>
            <td>通迅地址（中文）：</td>
            <td><span class="redStar" style="position:relative;bottom:70px;">*</span>
                <textarea id="uaddr1" rows="4" cols="28" name="uaddr1" class="site_input" style="height:150px; margin-bottom:5px;" ></textarea>
            </td>
        </tr>
        <tr>
            <td>通迅地址（英文）：</td>
            <td><span class="redStar" style="position:relative;bottom:70px;">*</span>
                <textarea id="uaddr2" rows="4" cols="28" name="uaddr2" class="site_input" style="height:150px;" ></textarea>
            </td>
        </tr>
        <tr>
            <td>邮编：</td>
            <td><span class="redStar">*</span>
                <input id="uzip" type="text" name="uzip" class="site_input" size="30"  />
            </td>
        </tr>
        <tr>
            <td>手机：</td>
            <td><span class="redStar">*</span>
                <input id="uteln" type="text" class="site_input" name="uteln" />
            </td>
        </tr>
        <tr class="logBtn">
            <td>操作：</td>
            <td>
                <asp:Button runat="server" ID="addTempBtn" Text="添加模板" Style="cursor: pointer; margin-left:12px;" CssClass="site_button"  OnClick="addTempBtn_Click"
                    OnClientClick="return checkValue();"/>
            </td>
        </tr>
    </table>
    </div>
    </div>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.BannerManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>栏目管理</title>
<script>
    function CheckAll(spanChk)//CheckBox全选
    {
    var oItem = spanChk.children;
    var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
    xState=theBox.checked;
    elm=theBox.form.elements;
    for(i=0;i<elm.length;i++)
    if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
    {
        if(elm[i].checked!=xState)
        elm[i].click();
    }
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
      <tbody id="Tabss">
               <tr class="tdbg">
          <td width="3%" height="24" align="center" class="title"><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
          <td width="12%" class="title"><span class="tdbgleft">栏目ID</span></td>
          <td width="12%" class="title"><span class="tdbgleft">栏目名称</span></td>
          <td width="12%" class="title"><span class="tdbgleft">栏目类型</span></td>
          <td width="7%" class="title"><span class="tdbgleft">栏目状态</span></td>
          <td width="8%" class="title"><span class="tdbgleft">操作</span></td>
        </tr>
        <asp:Repeater ID="Productlist" runat="server">
        <ItemTemplate>
         <tr>
          <td>
              <input name="Item" type="checkbox" value='<%# Eval("ID")%>'/>
              </td>
          <td><%# Eval("ID")%></td>
          <td><%#DataBinder.Eval(Container.DataItem, "BannerName")%></td>
          <td><%#GetIndex(DataBinder.Eval(Container.DataItem, "BannerIndex").ToString())%></td>
          <td><%#GetState(DataBinder.Eval(Container.DataItem, "BannerShow").ToString())%></td>
          <td><a href="BannerEdit.aspx?id=<%#Eval("ID")%>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
              <asp:LinkButton ID="LinkButton1" runat="server"  CommandName='<%#Eval("ID")%>' OnClick="Button1_Click" OnClientClick="return confirm('不可恢复性删除数据,确定将该数据删除?');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton></td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
                 <tr class="tdbg">
          <td height="24" colspan="6" align="center" class="tdbgleft">共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>&nbsp;
              <asp:Label ID="Toppage" runat="server" Text="" /> <asp:Label ID="Nextpage" runat="server" Text="" /> <asp:Label ID="Downpage" runat="server" Text="" /> <asp:Label ID="Endpage" runat="server" Text="" />  页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />页  <asp:Label ID="pagess" runat="server" Text="" />个/页
              转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
              </asp:DropDownList>页</td>
        </tr>
     </tbody>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td style="height: 21px">&nbsp;<asp:Button ID="Button1" runat="server" Text="设为显示" CommandName="1" OnClick="SaveBtn_Click"  />
            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="设为不显示" CommandName="0" OnClick="SaveBtn_Click"  />
            <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Text="批量删除" CommandName="5" OnClick="SaveBtn_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" />
            </td>
      </tr>
            <tr>
        <td></td>
      </tr>
    </table>
</asp:Content>



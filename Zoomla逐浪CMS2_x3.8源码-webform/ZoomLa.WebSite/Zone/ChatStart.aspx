<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatStart.aspx.cs" Inherits="Zone_ChatStart" EnableViewStateMac="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>黑灯瞎火蒙面聊</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="false" EnableTheming="False"  
            CssClass="table table-striped table-bordered table-hover"  GridLines="None" EmptyDataText="当前没有信息!!"  >
                <Columns>
                <asp:TemplateField>
                  <ItemTemplate> <strong>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName='<%#DataBinder.Eval(Container.DataItem, "Name")%>' CausesValidation="False" OnClick="LinkButton1_Click"><%# DataBinder.Eval(Container.DataItem, "Name") %></asp:LinkButton>
                    [<font color='#4D0000'>
                    <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Sex")%>'></asp:Label>
                    </font>](<font color='#666666'>
                    <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Addtime")%>'></asp:Label>
                    </font>)
                    <asp:Label ID="LinkButton2" runat="server" Text='<%# GetType(DataBinder.Eval(Container.DataItem, "ChatType").ToString())%>' ></asp:Label>
                    说：</strong><br />
                    <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ChatContent")%>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center"  />
        </ZL:ExGridView>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
          <asp:View ID="View1" runat="server"> &nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="取个名字："></asp:Label>
            <asp:TextBox ID="txtName" runat="server" MaxLength="25" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
				Display="Dynamic" ErrorMessage="请取个名字"></asp:RequiredFieldValidator>
            <asp:Label ID="Label5" runat="server" Text="选择性别："></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server">
              <asp:ListItem>男生</asp:ListItem>
              <asp:ListItem>女生</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList1"
				ErrorMessage="请选择性别"></asp:RequiredFieldValidator>
            <asp:Button ID="Button1" runat="server" Text="确定" OnClick="Button1_Click" CssClass="btn btn-primary" />
          </asp:View>
          <asp:View ID="View2" runat="server"> 对
            <asp:LinkButton ID="typeLinkButton" runat="server" CausesValidation="False" OnClick="typeLinkButton_Click" Text="所有人" ></asp:LinkButton>
            &nbsp;
            <asp:Label ID="Label2" runat="server" Text="发送信息："></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="361px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1" ErrorMessage="写点什么吧"></asp:RequiredFieldValidator>
            &nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="发送" CausesValidation="False" />
            &nbsp;
            <asp:Button ID="Button4" runat="server" CausesValidation="false" Text="修改设置" OnClick="Button4_Click" />
            &nbsp;
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click1" Text="清空聊天记录" CausesValidation="False" />
          </asp:View>
        </asp:MultiView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">

</asp:Content>
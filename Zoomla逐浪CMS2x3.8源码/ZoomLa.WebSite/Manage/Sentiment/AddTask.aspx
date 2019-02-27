<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTask.aspx.cs" Inherits="Manage_Sentiment_AddSentiment" MasterPageFile="~/Manage/I/Default.master" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content runat="server" ID="ContentID1" ContentPlaceHolderID="head">
<title>监测任务</title>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
</asp:Content>
<asp:Content runat="server"  ContentPlaceHolderID="Content">
    <div>
    <table class="table table-bordered table-striped"> 
        <tr>       
        <td class="text-right td_l">标题</td><td><asp:TextBox runat="server" ID="Title_T" CssClass="form-control text_300" />
            <asp:RequiredFieldValidator runat="server" ID="R1" ForeColor="Red" ErrorMessage="标题不能为空" ControlToValidate="Title_T" Display="Dynamic" /></td>
        </tr>
        <tr>
        <td class="text-right">类型</td>
        <td>
            <label><input type="checkbox" name="source_chk" value="新闻" checked="checked" />新闻</label>
            <label><input type="checkbox" name="source_chk" value="微博" checked="checked"/>微博</label>
            <label><input type="checkbox" name="source_chk" value="微信" checked="checked"/>微信</label>
            <label><input type="checkbox" name="source_chk" value="论坛" />论坛</label>
            <label><input type="checkbox" name="source_chk" value="贴吧" />贴吧</label>
            <label><input type="checkbox" name="source_chk" value="网页" />网页</label>
        </td>
        </tr>
        <tr><td class="text-right">搜索方式</td><td><asp:DropDownList runat="server" ID="SType_DP" CssClass="form-control text_300">
            <asp:ListItem Value="0" Selected="True">标题&时间</asp:ListItem>
            <asp:ListItem Value="1">正文&相关度</asp:ListItem>
                             </asp:DropDownList></td></tr>
        <tr><td class="text-right">关键词</td><td><asp:TextBox runat="server" ID="Condition_T" CssClass="form-control text_300"></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" ID="R3" ForeColor="Red" ErrorMessage="关键词不能为空" ControlToValidate="Condition_T" Display="Dynamic" />
             <span class="rd_green">示例:股市动荡</span>
                         </td></tr>
        <tr><td class="text-right">内容匹配词(可选)</td><td>
            <asp:TextBox runat="server" ID="SuitKey_T" CssClass="form-control text_300"></asp:TextBox>
           </td></tr>
        <tr>
        <td class="text-right">任务状态</td><td>
            <input type="checkbox" runat="server" id="Status_Chk" checked="checked" />
                                        </td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button runat="server" Text="提交" CssClass="btn btn-primary" ID="Save_Btn" OnClick="Save_Btn_Click"/>
                <a href="Default.aspx" class="btn btn-primary">取消</a></td>
        </tr>
    </table>
    </div>
</asp:Content>     
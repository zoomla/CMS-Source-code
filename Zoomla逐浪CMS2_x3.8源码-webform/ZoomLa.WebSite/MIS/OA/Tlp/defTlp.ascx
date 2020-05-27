<%@ Control Language="C#" AutoEventWireup="true" CodeFile="defTlp.ascx.cs" Inherits="MIS_OA_Tlp_defTlp" %>
<table class="table table-bordered table-striped">
        <tr><td colspan="6" class="flow_title" style="text-align:center;font-family:'Microsoft YaHei';font-size:24px;"><asp:Label runat="server" ID="ProceName_L"></asp:Label></td></tr>
        <tr>
            <td class="text-right td_md">发起人：</td>
            <td class="td_md">
                <asp:Label runat="server" ID="sendManL" />
            </td>
            <td class="td_md text-right">编号：</td>
            <td class="text_300">
                <div class="input-group">                 	                               	
                    <a href="javascript:;" class="btn btn-default input-group-addon">No</a>
                    <asp:TextBox runat="server" ID="No_T" disabled="disabled" CssClass="form-control text_300"  />
                </div>
            </td>
            <td class="td_md text-right">发起日期：</td><td>
                <asp:TextBox runat="server" ID="SendDate_T" CssClass="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" /></td>
        </tr>
        <tr>
            <td class="text-right">当前步骤：</td><td><asp:Label runat="server" ID="stepNameL" Text="填写表单" /></td>
            <td class="text-right">标题：</td><td colspan="3"><asp:TextBox runat="server" ID="Title_T" CssClass="form-control text_300"></asp:TextBox></td>
        </tr>
        <tr><td class="text-right">公文编辑器：</td><td colspan="5"><a href="javascript:;" onclick="ShowWord();" class="btn btn-info">打开公文</a></td></tr>
        <tr>
            <td colspan="6">
                <table class="table table-border table-striped">
                    <asp:Literal runat="server" ID="Html_Lit" EnableViewState="false"></asp:Literal>
                </table>
            </td>
        </tr>
    </table>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreeProNext.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.FreeFlow.FreeProNext" MasterPageFile="~/Common/Master/UserEmpty.master"  ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>指定办理人</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <div id="Div1" runat="server" class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title" style="text-align:center;">已有流程(已执行的流程不允许更改)</h3></div>
            <div class="panel-body">
                   <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1" 
                       Width="100%" CssClass="table table-bordered table-hover table-striped table-condensed" OnPageIndexChanging="EGV_PageIndexChanging" RowStyle-CssClass="tdbg" BackColor="White" DataKeyNames="ID"  AllowUserToOrder="true"
                        EmptyDataText="尚未指定流程步骤!!" >
                <Columns>
               <%--     <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-Height="22" />--%>
                    <asp:BoundField HeaderText="序号" DataField="StepNum"/>
                    <asp:BoundField HeaderText="步骤名" DataField="StepName"/>
                    <asp:TemplateField HeaderText="经办人" >
                        <ItemTemplate >
                            <%#GetUserInfo(Eval("ReferUser","{0}"),Eval("ReferGroup","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <%--  <asp:TemplateField HeaderText="抄送人">
                        <ItemTemplate>
                            <%# GetUserInfo(Eval("CCUser","{0}"),Eval("CCGroup","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="会签">
                        <ItemTemplate>
                            <%# GetHQoption(Eval("HQoption","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="强制转交">
                        <ItemTemplate>
                            <%# GetQzzjoption(Eval("Qzzjoption","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="回退">
                        <ItemTemplate>
                            <%# GetHToption(Eval("HToption","{0}")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="备注" DataField="Remind"/>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <%#GetEditLink(Eval("ID"),Eval("StepNum")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                   </Columns>
                    <PagerStyle HorizontalAlign="Center"/>
                   <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
            </div>
        </div>
       <span runat="server" visible="false" id="remind2" style="color:green;font-size:16px;margin-top:10px;">已为自由流程创建步骤,你可以修改最新创建的流程,或关闭本页面!!!</span>
    <div id="Free_Div" runat="server" class="panel panel-primary" style="margin-top:10px;">
            <div class="panel-heading">
                <h3 class="panel-title" style="text-align:center;">自由流程,请选择需要投递的用户</h3></div>
            <div>
                <table class="table table-bordered table-striped table-condensed" style="width:960px;">
                    <tr>
                        <td class="text-right td_l">
                            <button type="button" name="selruser" id="selruser" class="btn btn-primary" onclick="selRuser();">选择主办人</button>
                        </td>
                        <td style="word-wrap:break-word;">
                            <asp:Label runat="server" ID="ReferUser_T" Style="height: 60px;word-wrap:break-word;"></asp:Label><asp:HiddenField runat="server" ID="ReferUser_Hid" />
                        </td>
                    </tr>
                    <tr>
                        <td  class="text-right">
                            <button type="button" name="selcuser" id="selcuser" class="btn btn-primary" onclick="selCuser();">选择协办人</button>
                        </td>
                        <td style="word-wrap: break-word; ">
                            <asp:Label runat="server" ID="CCUser_T" Style="height: 60px;Word-wrap:break-word;"></asp:Label><asp:HiddenField runat="server" ID="CCUser_Hid" />
                        </td>
                    </tr>
                    <tr><td class="text-right">短信通知：</td><td>
                        <asp:CheckBox runat="server" ID="SmsToRefer_Chk" Text="主办人" />
                        <asp:CheckBox runat="server" ID="SmsToCCUser_Chk" Text="协办人" />
                                    </td></tr>
                    <tr>
                        <td class="text-right">操作：</td>
                        <td>
                            <asp:Button runat="server" ID="return_Btn" class="btn btn-primary" Style="margin-left: 5px;" OnClick="return_Btn_Click" OnClientClick="return confirm('确定返回吗?');" Text="返回" Visible="false" />
                            <asp:Button runat="server" ID="Free_Sure_Btn" class="btn btn-primary" Style="margin-left: 5px;" OnClick="Free_Sure_Btn_Click" OnClientClick="return FreeCheck();" Text="保存" />
                            <span runat="server" id="remind" style="color: green; font-size: 14px; font-family: 'Microsoft YaHei';"></span>
                        </td>
                    </tr>
                </table>         
            </div>
            <div class="panel-body" style="display:none;" id="select">
                <div id="foo" style="position:absolute;left:30%;top:50%;"></div>
                <iframe id="User_IFrame" style="visibility: inherit; overflow: auto; overflow-x: hidden;width: 1000px;height:300px;"  name="main_right" src="/Common/Dialog/SelStructure.aspx?Type=AllInfo" frameborder="0"></iframe>
            </div>
        </div>
    </div>
    <style type="text/css">
        #AllID_Chk {display:none;}
    </style>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script type="text/javascript">
        function PaerntUrl(url) {
            if (opener) { opener.location = url; }
        }
        function UserFunc(json, select)
        {
            Def_UserFunc(json, select);
            $("#select").hide();
        }
        function FreeCheck() {
            if ($("ReferUser_Hid").val() == "") {
                alert("尚未选定主办人!!!");
                return false;
            }
            return true;
        }
        function selRuser() {
            $("#User_IFrame").attr("src", "/Common/Dialog/SelStructure.aspx?Type=AllInfo#ReferUser");
            $("#User_IFrame")[0].contentWindow.ClearChk();
            $("#select").show();
        }
        function selCuser() {
            $("#User_IFrame").attr("src", "/Common/Dialog/SelStructure.aspx?Type=AllInfo#CCUser");
            $("#User_IFrame")[0].contentWindow.ClearChk();
            $("#select").show();
        }
    </script>
</asp:Content>


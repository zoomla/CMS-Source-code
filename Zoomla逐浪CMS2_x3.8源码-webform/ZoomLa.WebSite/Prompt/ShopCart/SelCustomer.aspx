<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelCustomer.aspx.cs" MasterPageFile="~/Common/Common.master" Inherits="Prompt_ShopCart_SelCustomer" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择客户</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                 CssClass="table table-striped table-bordered table-hover" PageSize="10" OnPageIndexChanging="EGV_PageIndexChanging"
                EnableTheming="False" EmptyDataText="没有客户数据！" IsHoldState="True">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <input type="checkbox" value="<%#Eval("Code") %>" data-name="<%#Eval("P_name") %>" data-cretcode="<%#Eval("ID_Code") %>" name="idchk" />
                </ItemTemplate>
                <ItemStyle CssClass="td_s" />
            </asp:TemplateField>
            <asp:BoundField DataField="Code" HeaderText="客户编号" />
            <asp:BoundField DataField="P_name" HeaderText="客户名" />
            <asp:BoundField DataField="Birthday" HeaderText="出生日期" />
            <asp:BoundField DataField="Id_Code" HeaderText="证件号码" />
        </Columns>
    </ZL:ExGridView>
    <div class="text-center">
        <button type="button" class="btn btn-primary" onclick="AddCustomer()">确定</button>
        <button type="button" onclick="parent.CloseCurDialog()" class="btn btn-primary">取消</button>
    </div>
    <asp:HiddenField ID="Codes_Hid" runat="server" />
    <script>
        //当前选中数组
        var curdatas = [];
        $().ready(function () {
            EventCheck();
            InitCheckData();
        });
        //初始化选择客户事件
        function EventCheck() {
            $("[name='idchk']").click(function () {
                var data = { code: $(this).val(), Name: $(this).data("name"), CertCode: $(this).data("cretcode"), CertType: 1 };
                RemoveData(data);
                if ($(this)[0].checked)
                    curdatas.push(data);
                $("#Codes_Hid").val(JSON.stringify(curdatas));
            });
            $("#AllID_Chk").click(function () {//全选操作
                $("[name='idchk']").each(function (i, v) {
                    var data = { code: $(v).val(), Name: $(v).data("name"), CertCode: $(v).data("cretcode"), CertType: 1 };
                    RemoveData(data);
                    if ($(v)[0].checked)
                        curdatas.push(data);
                });
                $("#Codes_Hid").val(JSON.stringify(curdatas));
            });
        }
        //获取以选中数据
        function InitCheckData() {
            if ($("#Codes_Hid").val() != "") {
                curdatas = JSON.parse($("#Codes_Hid").val());
                for (var i = 0; i < curdatas.length; i++) {
                    var checkobj = $("[type='checkbox'][value='" + curdatas[i].code + "']")[0];
                    if (checkobj) {
                        checkobj.checked = true;
                    }
                }
            }
        }
        //移除数组元素
        function RemoveData(obj) {
            for (var i = 0; i < curdatas.length; i++) {
                if (curdatas[i].code == obj.code) {
                    curdatas.splice(i, 1);
                    break;
                }
            }
        }
        function AddCustomer() {
            parent.AddCustomer(curdatas);
            parent.CloseCurDialog();
        }
    </script>
</asp:Content>



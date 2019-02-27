<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelSchool.aspx.cs" MasterPageFile="~/Common/Common.master" Inherits="User_Exam_SelSchool" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择学校</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="input-group" style="width:628px">
        <select id="tbProvince" class="form-control schooldp" style="width:120px;"></select>
        <select id="tbCity" class="form-control schooldp" style="width:120px;"></select>
        <select id="tbCounty" class="form-control schooldp" style="width:150px;"></select>
        <asp:TextBox ID="Search_T" CssClass="form-control text_md" runat="server" placeholder="学校名"></asp:TextBox>
        <span class="input-group-btn">
            <asp:LinkButton ID="Search_Btn" runat="server" OnClick="Search_Btn_Click" CssClass="btn btn-primary"><span class="fa fa-search"></span></asp:LinkButton>
        </span>
    </div>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" 
        CssClass="table table-bordered table-striped table-hover margin_t10" EnableTheming="False" OnPageIndexChanging="EGV_PageIndexChanging" EmptyDataText="未找到对应的学校">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                  <%--  <input type="radio" data-name="<%#Eval("SchoolName") %>" name="idchk" value="<%#Eval("ID") %>" />--%>
                    <input type="button" value="选定" class="btn btn-xs btn-info" onclick="GetSchoolName('<%#Eval("SchoolName")%>');" />
                </ItemTemplate>
                <ItemStyle CssClass="td_s" />
            </asp:TemplateField>
   <%--         <asp:TemplateField HeaderText="类型">
                <ItemTemplate>
                    <%#GetSchoolType(Eval("SchoolType","")) %>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="学校名称">
                <ItemTemplate>
                    <%#GetIcon(Eval("Country").ToString()) %>
                    <span><%#Eval("SchoolName") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="省份" DataField="Province" />
            <asp:BoundField HeaderText="城市" DataField="City" />
            <asp:BoundField HeaderText="区域" DataField="County" />
        </Columns>
    </ZL:ExGridView>
    <div class="text-center hidden">
        <button type="button" onclick="GetSchoolName()" class="btn btn-primary">确定</button>
        <button type="button" class="btn btn-primary" onclick="parent.CloseDiag()">取消</button>
    </div>
<asp:HiddenField runat="server" ID="address_hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>.allchk_l{display:none;}</style>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
<script>
    var pcc = new ZL_PCC("tbProvince", "tbCity", "tbCounty");//,null, { text: "全部", value: "" }
    $(function () {
        $(".schooldp").change(function () {
            setTimeout(function () {
                var val = $("#tbProvince").val() + "," + $("#tbCity").val() + "," + $("#tbCounty").val();
                $("#address_hid").val(val);
            }, 200);
        });
        if ($("#address_hid").val() != "") {
            var defdata = $("#address_hid").val().split(',');
            pcc.SetDef(defdata[0], defdata[1], defdata[2]);
            pcc.ProvinceInit();
        }
        else { pcc.ProvinceInit(); }
    });
    function GetSchoolName(name) {
        parent.GetSchoolName(name);
    }
</script>
</asp:Content>



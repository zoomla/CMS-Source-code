<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelKeyWords.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.SelKeyWords" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择关键字</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="text-right">
        <div class="input-group text_300">
          <asp:TextBox ID="Search_T" placeholder="搜索关键字" runat="server" CssClass="form-control"></asp:TextBox>
          <span class="input-group-btn">
              <asp:LinkButton ID="Search_Btn" CssClass="btn btn-default" OnClick="Search_Btn_Click" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
          </span>
        </div>
    </div> 
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" 
                CssClass="table table-striped table-bordered table-hover margin_t5" EmptyDataText="没有相关数据!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("KeywordText") %>" />
                </ItemTemplate>
                <ItemStyle CssClass="td_s" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="关键字" DataField="KeywordText" />
        </Columns>
    </ZL:ExGridView>
    <asp:HiddenField ID="SelKeys_Hid" Value="" runat="server" />
    <div class="text-center"><button type="button" onclick="CheckKeyWord()" class="btn btn-primary">确定</button> <button type="button" onclick="parent.CloseComDiag()" class="btn btn-primary">取消</button></div>
    <script>
        $(function () {
            $("input[name='idchk']").click(function () { SaveKeys(); });
            $("#AllID_Chk").click(function () { SaveKeys(); });
            InintCheck();
        });
        function InintCheck() {
            var keyarry = $("#SelKeys_Hid").val().split(',');
            for (var i = 0; i < keyarry.length; i++) {
                if (keyarry[i] != "") {
                    $("input[value='" + keyarry[i] + "']")[0].checked = true;
                }
            }
        }
        function SaveKeys() {
            var keywords = "";
            if ($("#SelKeys_Hid").val() != "") {
                keywords = $("#SelKeys_Hid").val();
            }
            $("input[name='idchk']").each(function () {
                var keyword = "," + $(this).val() + ",";
                if ($(this)[0].checked) { keywords = keywords.replace(keyword, "") + keyword; }
                else { keywords = keywords.replace(keyword, ""); }
            });
            $("#SelKeys_Hid").val(keywords);
        }

        function CheckKeyWord() {
            var keyarry = $("#SelKeys_Hid").val().split(',');
            var keystr = "";
            for (var i = 0; i < keyarry.length; i++) {
                if (keyarry[i] != "") {
                    keystr += keyarry[i]+",";
                }
            }
            parent.GetKeyWords(keystr.substr(0,keystr.length-1));
        }
    </script>
</asp:Content>

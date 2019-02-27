<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitMain.aspx.cs" Inherits="ZoomLaCMS.Manage.User.UnitMain"  MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/Boss/StructDP.ascx" TagPrefix="ZL" TagName="StructDP" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>会员提成</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="margin_b2px table table-bordered">
        <ZL:StructDP runat="server" ID="StructDP" />
        <asp:TextBox runat="server" ID="Time_T" onclick="WdatePicker({dateFmt:'yyyy/MM/dd',firstDayOfWeek:1,isShowWeek:'true',onpicked:function(){WhichWeek();}});" CssClass="form-control text_md" />
        <input type="text" id="Week_T" class="form-control text_x" readonly="readonly"/>
        <asp:Button runat="server" ID="Begin_Btn" Text="查询" OnClick="Begin_Btn_Click" CssClass="btn btn-primary" />
        <%--<asp:Button runat="server" ID="ReBegin_Btn" Text="重新生成" OnClientClick="return confirm('确定要重新生成分成报表吗');" OnClick="ReBegin_Btn_Click" CssClass="btn btn-primary" />--%>
<%--        <asp:Button runat="server" ID="AutoUnit_Btn" Text="自动分成" OnClientClick="return UnitConfirm(this);" OnClick="AutoUnit_Btn_Click" CssClass="btn btn-danger" />--%>
    </div>
    <div class="margin_b2px table table-bordered">
        <asp:CheckBox runat="server" ID="OnlyUnit_Chk" Checked="false" Text="仅显示已分成的用户" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
        <asp:CheckBox runat="server" ID="OnleyBonus_Chk" Checked="false" Text="仅显示分成金额>0的用户" OnCheckedChanged="Chk_CheckedChanged" AutoPostBack="true" />
    </div>
      <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" 
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="没有提成信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowDataBound="EGV_RowDataBound" >
            <Columns>
           <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="流水号">
                <ItemTemplate>
                    <%#Eval("Flow") %>
                <%--    <%#GetDate() %>--%>
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="用户名">
                <ItemTemplate><a href="UserInfo.aspx?id=<%#Eval("UserID") %>" target="_blank"><%#Eval("UserName") %></a></ItemTemplate>
            </asp:TemplateField>
    <%--        <asp:TemplateField HeaderText="推荐人">
                <ItemTemplate><a href="UserInfo.aspx?id=<%#Eval("PUserID") %>"  target="_blank"><%#Eval("PUserName") %></a></ItemTemplate>
            </asp:TemplateField>--%>
            <%--    <asp:BoundField HeaderText="自身消费金额" DataField="AMount" />--%>
                <asp:TemplateField HeaderText="下线业绩金额(汇总)"><ItemTemplate><%#Eval("ChildAMount","{0:f0}")+"<span class='remind_g_x'>"+Eval("Remind")+"</span>" %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="入账状态"><ItemTemplate><%#GetStatus() %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="无提成"><ItemTemplate><%#Eval("RealUnit0","{0:f0}") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="可提成1%"><ItemTemplate><%#Eval("RealUnit10","{0:f0}")+GetUnit(Eval("RealUnit10","{0:f0}"),0.01) %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="可提成2%"><ItemTemplate><%#Eval("RealUnit20","{0:f0}")+GetUnit(Eval("RealUnit20"),0.02) %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="可提成3%"><ItemTemplate><%#Eval("RealUnit30","{0:f0}")+GetUnit(Eval("RealUnit30"),0.03) %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="总计提成金额"><ItemTemplate><span class="unit"><%#Eval("RealUnit","{0:f0}") %></span></ItemTemplate></asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="子_未提成"><ItemTemplate><%#Eval("ChildUnit0") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="子_提成10%(>20240)"><ItemTemplate><%#Eval("ChildUnit10") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="子_提成20%(>40480)"><ItemTemplate><%#Eval("ChildUnit20") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="子_提成30%(>80960)"><ItemTemplate><%#Eval("ChildUnit30") %></ItemTemplate></asp:TemplateField>--%>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
              <%--          <a href="UnitMain.aspx?uid=<%#Eval("UserID") %>&stime=<%=STime.ToString("yyyy/MM/dd") %>">查看详情</a>--%>
                        <a href="UnitDetail.aspx?Pid=<%#Eval("ID")+"&Flow="+Eval("Flow") %>">查看流水</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <RowStyle Height="24px" HorizontalAlign="Center"  />
            </ZL:ExGridView>
    <%--       <div class="rd_green">查  询：查询所选定周报表</div>
           <div class="rd_green">重新生成：重新生成所选定周的分成报表</div>
           <div class="rd_green">自动分成：根据分成报表,将金额打入用户帐户</div>--%>
          <%-- <div>提成10%：例如下级提掉了20%,而你的总金额计算能最高提成30%,则可再从其中提出10%</div>--%>
<style type="text/css">
    /*.rd_green{color:green;font-weight:normal;}*/
    .remind_g_x {color:green;float:right;}
    .unit {color:red;}
    /*.text_md{max-width:200px;display:inline-block;}
    .text_x{max-width:80px;display:inline-block;}*/
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        //var now = new Date();                    //当前日期  
        //var nowDayOfWeek = now.getDay();         //今天本周的第几天  
        //var nowDay = now.getDate();              //当前日  
        //var nowMonth = now.getMonth();           //当前月  
        //var nowYear = now.getYear();             //当前年  
        //nowYear += (nowYear < 2000) ? 1900 : 0;  // 
        //function getWeekStartDate() {
        //    var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
        //    return formatDate(weekStartDate);
        //}

        ////获得本周的结束日期  
        //function getWeekEndDate() {
        //    var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
        //    return formatDate(weekEndDate);
        //}
        //function TimeChange() {
        //    $.post("UnitMain.aspx", { value: $("#Time_T").val() }, function (data) {
        //        $("#STime_L").text("起始日期:" + data.split('|')[0]);
        //        $("#ETime_L").text("结束日期:" + data.split('|')[1]);
        //    });
        //}
        $(function () {
            $dp.$('Week_T').value = "第" + $dp.cal.getP('W', 'WW') + "周";
        })
        function WhichWeek()
        {
            $dp.$('Week_T').value = "第"+$dp.cal.getP('W', 'WW')+"周";
        }
        function UnitConfirm(o) {
            if (confirm("确定将[" + $dp.cal.getP('W', 'WW') + "周]的提成分配给用户吗?"))
            { disBtn(o); return true; } else return false;
        }
    </script>
</asp:Content>
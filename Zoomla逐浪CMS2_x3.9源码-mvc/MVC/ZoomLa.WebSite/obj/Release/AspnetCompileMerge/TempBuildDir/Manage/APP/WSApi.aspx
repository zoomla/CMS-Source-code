<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WSApi.aspx.cs" Inherits="ZoomLaCMS.Manage.APP.WSApi" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>跨站接入2.0</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" 
        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-CssClass="td_s">
              <ItemTemplate>
                  <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
              </ItemTemplate>
          </asp:TemplateField>
            <asp:BoundField HeaderText="名称" DataField="Alias" />
            <asp:TemplateField HeaderText="授权网址">
              <ItemTemplate>
                 <a href="<%# Eval("WebSite") %>" target="_blank"><%# Eval("WebSite") %></a>
              </ItemTemplate>
          </asp:TemplateField>
            <asp:TemplateField HeaderText="Key">
                <ItemTemplate>
                    <%#Eval("Key") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="数据库用户名" DataField="DBUName" />
            <asp:TemplateField HeaderText="创建时间">
                <ItemTemplate>
                    <%#Eval("AddTime","{0:yyyy年MM月dd日 HH:mm}") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#Eval("Status","{0}")=="0"?"<span class='rd_red'>禁用</span>":"<span class='rd_green'>启用</span>" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddUcenter.aspx?ID=<%#Eval("ID") %>">修改</a>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('确定要删除吗?');">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div>
        <div class="panel panel-primary">
            <div class="panel-heading"><span class="fa fa-flag"></span><span class="margin_l5">接口说明</span></div>
            <div class="panel-body">
                <div><strong>使用说明：</strong>跨站接入2.0是针对Zoomla！逐浪CMS2产品线，并基于标准的WebServices标准制定的接口平台，网站通过此认证后,可以分别就用户管理,问答管理,内容管理,或对指定的表进行接口操作,其使用如下：
</div>
                <div>
                    1,添加授权网站,自动生成Key<br />
                    2,目标网站实现对WebServices的调用,所以的接口调用都需要验证Key<br />
                    3,使用提供的接口完成功能
                </div>
                <div class="divline"></div>
                <div><strong>1、查询示例：</strong></div>
                <div>
                      首先添加WebSerives引用,目标网址<a href="<%Call.Label("{$SiteURL/}"); %>/API/Center.asmx" target="_blank"><%Call.Label("{$SiteURL/}"); %>/API/Center.asmx</a> <br />
                      命名空间可自定义,这里我们设为“ZLAPI”(<span class="rd_green">C#是原生支持,其他语言请下载WebServices的客户端插件</span>)
                <pre class="brush:c#;toolbar:false">ZLAPI.CenterSoapClient client = new ZLAPI.CenterSoapClient();<br>string result = client.Select("key", "ZL_User", "UserName,UserID", "UserID&lt;10", "UserID DESC", null);<br>返回:[{"UserName":"admin","UserID":1}];<br></pre>   
                </div>
                <div class="divline"></div>
                <div><strong>2、添加一条问答</strong></div>
                <div>
                    <pre class="brush:c#;toolbar:false">ZLAPI.CenterSoapClient client = new ZLAPI.CenterSoapClient();<br>ZLAPI.M_Ask askMod = new ZLAPI.M_Ask();<br>askMod.UserId = 1;<br>askMod.UserName = "test";<br>askMod.Qcontent = "问答内容";<br>askMod.Supplyment = "补充提问";<br>askMod.AddTime = DateTime.Now;<br>askMod.Score = 5;<br>int result = client.AddAsk(key,askMod);<br>返回:刚增加的问答ID</pre>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
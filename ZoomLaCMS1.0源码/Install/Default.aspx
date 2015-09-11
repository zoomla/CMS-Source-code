<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Install_Default" ResponseEncoding="utf-8"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">   
   
    <title>安装向导</title>
    <link rev="stylesheet" media="all" href="Images/style.css" type="text/css" rel="stylesheet" /> 
     <script type="text/javascript">
        function ShowProgress()
        {
            var labelDoingID=document.getElementById("LblCreateDataProgress");
            var labelBeforeID=document.getElementById("LblCreateDataBaseBefore");
            
            if(labelDoingID!=null)
            {
                labelDoingID.style.visibility="visible";
            }
            if(labelBeforeID!=null)
            {
                labelBeforeID.innerText="正在创建数据库。";
            }
        }
    </script>  
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
</head>
<body>
    <div id="top">
<div id="logo"><a href="http://www.zoomla.cn" target="_blank"><img src="images/logo.gif"></a></div>
  <div id="sous">
  <form action="" method="post" name="ff">
  <input type="text"  onclick="value=''" onmouseover="this.style.backgroundColor='#ffffff'"
                            onmouseout="this.style.backgroundColor='#ebf7ff'" value="逐浪全站搜索" size="15" name="11" />
  <input src="images/ss.jpg" type="image" />
  </form>
  </div>
  <div id="nav"><a href="http://www.zoomla.cn/" target="_blank" title="访问逐浪官方网站">zooml.cn</a> | <a href="http://zoomla.net/soft/" target="_blank" title="免费下载系列软件产品">免费下载 </a>|<a href="http://zoomla.net/User/" target="_blank" title="逐浪官方网站客户自助服务">客户自动服务</a>| <a href="http://bbs.hx008.cn//" target="_blank" title="今天您上逐浪论坛了吗？">逐浪论坛</a></div>
</div>
<div style="clear:both"></div>    

<div id="title">
        <form id="form2" runat="server"> 
         <h1 class="title_1" align="center">欢迎安装逐浪CMS</h1> 
            <asp:Wizard ID="WzdInstall" OnNextButtonClick="WzdInstall_NextButtonClick" OnFinishButtonClick="WzdInstall_FinishButtonClick"
                runat="server" ActiveStepIndex="0" DisplaySideBar="False" Width="100%">
                <WizardSteps>
                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1"><div id="DivDefault" runat="server" class="left140">
                        
                             
                                       <dl><dd class="message">
                                    本向导将协助您一步步的安装此软件。<br />
                                    建议您在运行本向导前仔细阅读程序包中的《安装说明》文档，如果您已经阅读过，请点击下一步。<br />
                                    <table width="82%" border="0" style="text-align: left;" cellpadding="1" cellspacing="1"
                                        class="border">
                                        <tr class='tdbg'>
                                            <td style="height: 18px" align="center">第一步
                                                <strong>阅读许可协议</strong></td>
                                        </tr>
                                        <tr class="tdbg">
                                            <td align="center">
                                                <textarea id="TxtLicense" style="width: 99%; height: 180px" cols="100" rows="12"
                                                    runat="server" readonly="readonly">                                
                                                    </textarea>
                                                    </td>
                                        </tr>
                                        <tr class="tdbg">
                                            <td align='left'>
                                                <asp:CheckBox ID="ChlkAgreeLicense" AutoPostBack="True" OnCheckedChanged="ChlkAgreeLicense_CheckedChanged"
                                                    runat="server" /><label for="ChlkAgreeLicense">我已经阅读并同意此协议</label></td>
                                        </tr>
                                    </table>
                                </dd> 
                                <asp:Button ID="StartNextButton" runat="server" Enabled="False" CommandName="MoveNext" Text="下一步" />
                            </dl> 
                           
                        </div>
                    
                    </asp:WizardStep>
       
                    <asp:WizardStep ID="WizardStep2" AllowReturn="true" runat="server" Title="Step 3">
                    <div id="DivInstall2" runat="server">
                          
                               <dl> <dd><center> 
                               第二步
                                   <strong>下面进行数据库连接设置</strong></center>
                                     <label style="color: Blue;">请确保设置好的数据库中没有旧的数据表和存储过程。</label>
                                        <table cellspacing="1" cellpadding="1" class="table_date">
                                            <tr>
                                                <td style="width: 235px">
                                                    请选择数据库版本：</td>
                                                <td>
                                                    <asp:DropDownList ID="DropSqlVersion" runat="server"><asp:ListItem Value="2000">Sql Server 2000</asp:ListItem><asp:ListItem Selected="True" Value="2005">Sql Server 2005</asp:ListItem></asp:DropDownList>
                                                    <asp:HiddenField ID="HDSql" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px; height: 23px;">
                                                    数据源：</td>
                                                <td style="height: 23px">
                                                    <asp:TextBox ID="TxtDataSource" runat="server" Width="150px" Text="(local)"></asp:TextBox><asp:RequiredFieldValidator ID="ValrDataSource" runat="server" ControlToValidate="TxtDataSource"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>如本地:local 远程为远程主机名</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px">
                                                    数据库名称：</td>
                                                <td>
                                                    <asp:TextBox ID="TxtDataBase" runat="server" Width="150px" Text="ZoomLa"></asp:TextBox><asp:RequiredFieldValidator ID="ValrDataBase" runat="server" ControlToValidate="TxtDataBase"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>请确认是否该数据是否存</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px">
                                                    数据库用户名称：</td>
                                                <td>
                                                    <asp:TextBox ID="TxtUserID" runat="server" Width="150px" Text="ZoomLa"></asp:TextBox><asp:RequiredFieldValidator ID="ValrUserID" runat="server" ControlToValidate="TxtUserID"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>有权限访问该数据库的有效用户名和密码</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px">
                                                    数据库用户口令：
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:TextBox ID="TxtPassword"  EnableViewState="true" runat="server" TextMode="Password" Width="150px" Text="ZoomLa"></asp:TextBox>
<asp:RequiredFieldValidator ID="ValrPassword" runat="server" ControlToValidate="TxtPassWord"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="TxtPwd" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="center">
                                                        <asp:Label ID="LblCheckConnectString" Visible="False" runat="server" ForeColor="Red">请检查数据库连接字符串设置是否正确或数据库服务器身份验证模式是否SQL Server和Windows混合模式！</asp:Label></div>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </dd> 
                                    
      <asp:Button ID="PreviousButtonStep3" runat="server" CausesValidation="False" CssClass="button_link"
                                            CommandName="MovePrevious" Text="上一步" OnClick="PreviousButtonStep3_Click" />&nbsp;&nbsp;<asp:Button ID="NextButtonStep3" runat="server" CssClass="button_link" CommandName="MoveNext" OnClick="NextButtonStep3_Click"
                                            Text="下一步" />
                            </dl>
                        </div>
                                                                                               
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Step 4"><div id="DivInstall3" runat="server" class="left140">
                            <dl>
                                
                                <dd><center>第三步
                                    <strong>下面将创建数据库，大约需要1～2分钟。</strong></center>      
                                    <label
                                        id="LblCreateDataBaseMessage" runat="server" style="visibility: visible;">点击“<strong>开始创建</strong>”按钮开始后，请耐心等候。</label>
                                   
                                    
                                        <table cellspacing="1" cellpadding="1" class="table_date">
                                            <tr>
                                                <td style="height: 33px;">
                                                    <label id="LblCreateDataBaseBefore" runat="server" style="visibility: visible;">
                                                        准备创建数据库。</label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <div class="center">
                                                        <label id="LblCreateDataProgress" runat="server" style="visibility: hidden;">
                                                            创建中。。。</label>                                                       
                                                       
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                       
                                        <asp:CheckBox ID="ChlkIsCreateDataBase" runat="server" AutoPostBack="True" OnCheckedChanged="ChlkIsCreateDataBase_CheckedChanged" /><label style="color: blue" for="ChlkIsCreateDataBase">如果数据库已创建好，可跳过这一步。</label>
                                     <br />
                                     <asp:Button ID="PreviousButtonStep4" runat="server" CausesValidation="False" CssClass="button_link"
                                            CommandName="MovePrevious" Text="上一步" />
                                           <asp:Button ID="NextButtonStep4" runat="server" CommandName="MoveNext" CssClass="button_link"
                                            Text="下一步" Enabled="False" OnClick="NextButtonStep4_Click" />
                                            
                                            <asp:Button ID="BtnCreateDateBase" CssClass="button_link" runat="server" OnClientClick="ShowProgress();" Text="开始创建"
                                            OnClick="BtnCreateDateBase_Click" /> 
                                            </dd> 
                                  
                            </dl>
                        </div>       

                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep4" runat="server" Title="Step 5"><div id="DivInstall4" runat="server" class="left140">
                            <dl>                               
                                  
                                <dd><center>第四步 <strong> 下面进行配置文件设置。</strong></center> 
                                    <table cellspacing="1" cellpadding="1" class="table_date">
                                        <tr>
                                            <td style="width: 30%">
                                                网站标题：</td>
                                            <td>
                                                <asp:TextBox ID="TxtSiteTitle" runat="server" Width="150px" Text="逐浪CMS"></asp:TextBox><asp:RequiredFieldValidator ID="ValrSiteTitle" runat="server" ControlToValidate="TxtSiteTitle"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 23px">
                                                网站地址：</td>
                                            <td style="height: 23px">
                                                <asp:TextBox ID="TxtSiteUrl" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="ValrSiteUrl" runat="server" ControlToValidate="TxtSiteUrl"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>                                       
                                        <tr>
                                            <td>
                                                后台管理认证码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtSiteManageCode" runat="server" Width="150px" Text="8888"></asp:TextBox><asp:RequiredFieldValidator ID="ValrSiteManageCode" runat="server" ControlToValidate="TxtSiteManageCode"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                         <tr>
                                            <td>
                                                Email：</td>
                                            <td>
                                                <asp:TextBox ID="TxtEmail" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtEmail"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 15px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                管理员名称：</td>
                                            <td>
                                                <asp:TextBox ID="TxtAdminName" runat="server" Width="150px">admin</asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                管理员密码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtAdminPassword" runat="server" Width="150px" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="ValrAdminPassword" runat="server" ControlToValidate="TxtAdminPassword"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                确认密码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtAdminPasswordAgain" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ValrAdminPasswordAgain" runat="server" ControlToValidate="TxtAdminPasswordAgain"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator><asp:CompareValidator ID="ValcAdminPasswordAgain" runat="server" ErrorMessage="两次密码不相同"
                                                    ControlToCompare="TxtAdminPassword" ControlToValidate="TxtAdminPasswordAgain"></asp:CompareValidator></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="center">
                                                  </div>
                                            </td>
                                        </tr>
                                    </table>
 
                                </dd><asp:Button ID="PreviousButtonStep5" runat="server" CssClass="button_link" CausesValidation="False"
                                        CommandName="MovePrevious" Text="上一步" /> &nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="NextButtonStep5" runat="server" CssClass="button_link" CommandName="MoveNext"  OnClick="NextButtonStep5_Click" Text="下一步" />
                            </dl>
                        </div> 
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep5" runat="server" Title="Step 6"><div id="DivIntallComplete" runat="server" class="left140">
                            <dl>                             
                                  
                                <dd class="message"><center> 第五步 <strong>安装完成</strong></center>
                                    已经成功安装！<br/>请点击“<strong>完成</strong>”按钮跳转到首页。
                                    <input id="HdnPassword" type="hidden" visible="False" runat="server" />
                                    <div class="clearbox">
                                    </div>
                                </dd>
                            </dl>
                        </div>
                    </asp:WizardStep>
                </WizardSteps>
                <StepNavigationTemplate>
                    <center>
                    </center>
                </StepNavigationTemplate>
                <StartNavigationTemplate>
                    <center>
                    </center>
                </StartNavigationTemplate>
                <FinishNavigationTemplate>
                    <center>
                        <br />
                         <asp:Button ID="PreviousButtonFinish" runat="server" CausesValidation="False" CssClass="button_link"
                                            CommandName="MovePrevious" Text="上一步" />
       <input name="Finish" type="button" id="Finish" value="完成" onclick="javascript:window.location.href='../Default.aspx'" />                                     
     </center>
                </FinishNavigationTemplate>
            </asp:Wizard> 
      
           
        </form>      </div>
    <div id="bottom">
<div id="bottom_1">
<ul ><li>Copyright © 2004-2008华夏互联hx008.cn版权所��?常年法律顾问:刘平律师|江西朗秋律师事务所 
</li>
<li>中华人民共和国网警备案号:3601040103 经营许可证号:工商3601002021063 赣ICP��?6002798��?
</li></ul>
</div>
</div>

  
</body>
</html>

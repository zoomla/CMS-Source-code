<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyScore.aspx.cs" Inherits="User_SurveyScore" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>调查问卷</title>
<script type="text/javascript" language="javascript">
    function Setscore(id) {
        var score = document.getElementById(id).alt;
        document.getElementById("SaveScore").value = score
        document.getElementById("NextOrprior").value = 0;
        document.forms[0].submit();
    }
    function LastPage() {
        var currpage = document.getElementById("CurrentPage").value;
        document.getElementById("CurrentPage").value = currpage - 2;
        document.getElementById("NextOrprior").value = 1;
        document.forms[0].submit();
    }
    function SeeResult() {
        document.forms[0].action = "SurveyResult.aspx";
        document.forms[0].submit();
    }
</script>
</head>
<body>
<form id="form1" action="SurveyScore.aspx" method="post">
<div style="width:560px;height:100px;">
    <asp:Repeater ID="RepDiv" runat="server">
    <ItemTemplate>
        <div style="width:560px;height:100px;background-color:#ffe6a9">
	        <%#Eval("Qtitle")%>
	    </div>
    </ItemTemplate>
    </asp:Repeater>
    </div>
    <div id="showScore" runat="server">
    <div>一点也不准确》》》》》》》》》》》》》》》》》》》》》》》》很准确</div>
    <div>  
    <img style="cursor:pointer" alt="5" src="../../Images/survey/o_1.gif" id="img1" onclick="Setscore('img1')"/>
    <img style="cursor:pointer" alt="10" src="../../Images/survey/o_2.gif" id="img2" onclick="Setscore('img2')"/>
    <img style="cursor:pointer" alt="15" src="../../Images/survey/o_3.gif" id="img3" onclick="Setscore('img3')"/>
    <img style="cursor:pointer" alt="20" src="../../Images/survey/5o_4.gif" id="img5" onclick="Setscore('img5')"/>
    <img style="cursor:pointer" alt="25" src="../../Images/survey/_r2_c6.gif" id="img7" onclick="Setscore('img7')"/>
    <img style="cursor:pointer" alt="30" src="../../Images/survey/_r2_c7.gif" id="img8" onclick="Setscore('img8')"/>
    <img style="cursor:pointer" alt="35" src="../../Images/survey/_r2_c8.gif" id="img9" onclick="Setscore('img9')"/>
    <img style="cursor:pointer" alt="40" src="../../Images/survey/_r2_c9.gif" id="img10" onclick="Setscore('img10')"/>
    <img style="cursor:pointer" alt="45" src="../../Images/survey/_r2_c10.gif" id="img11" onclick="Setscore('img11')"/>
    <img style="cursor:pointer" alt="50" src="../../Images/survey/_r2_c11.gif" id="img12" onclick="Setscore('img12')"/>
    <img style="cursor:pointer" alt="55" src="../../Images/survey/_r2_c12.gif" id="img13" onclick="Setscore('img13')"/>
    <img style="cursor:pointer" alt="60" src="../../Images/survey/_r2_c13.gif" id="img14" onclick="Setscore('img14')"/>
    <img style="cursor:pointer" alt="65" src="../../Images/survey/_r2_c14.gif" id="img15" onclick="Setscore('img15')"/>
    <img style="cursor:pointer" alt="70" src="../../Images/survey/_r2_c15.gif" id="img16" onclick="Setscore('img16')"/>
    <img style="cursor:pointer" alt="75" src="../../Images/survey/_r2_c16.gif" id="img17" onclick="Setscore('img17')"/>
    <img style="cursor:pointer" alt="80" src="../../Images/survey/_r2_c17.gif" id="img18" onclick="Setscore('img18')"/>
    <img style="cursor:pointer" alt="85" src="../../Images/survey/_r2_c18.gif" id="img20" onclick="Setscore('img20')"/>
    <img style="cursor:pointer" alt="90" src="../../Images/survey/_r2_c20.gif" id="img21" onclick="Setscore('img21')"/>
    <img style="cursor:pointer" alt="95" src="../../Images/survey/_r2_c22.gif" id="img23" onclick="Setscore('img23')"/>
    <img style="cursor:pointer" alt="100" src="../../Images/survey/_r2_c23.gif" id="img24" onclick="Setscore('img24')"/>    
    </div>
</div>
<div id="showYesOrNo" runat="server">
</div>
<label style="cursor:pointer" id="Nextpage" runat="server" onclick="LastPage()"><img alt="上一页" src="../../Images/survey/lastpage.jpg" id="img6"/></label>  
<div>
<input type="hidden" id="surveyID" name="surveyID" runat="server"/>
<input type="hidden" id="filename" name="filename" runat="server" />
<input type="hidden" id="CurrentPage" name="CurrentPage" runat="server" />
<input type="hidden" id="SaveScore" name="SaveScore" runat="server" />
<input type="hidden" id="NextOrprior" name="NextOrprior" value="0" runat="server" />  
<input type="submit" id="putsubmit" value="提交" runat="server" style="display:none;" />
</div>
</form>
</body>
</html>

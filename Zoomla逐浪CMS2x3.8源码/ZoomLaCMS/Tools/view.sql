CREATE VIEW [dbo].[ZL_User_PlatView]
AS
SELECT a_1.*,b.CompName
,Cast(','+(SELECT GroupName+',' FROM ZL_Plat_Group  Where MemberIDS Like '%,'+ CAST(UserID as varchar(20))+',%' FOR XML PATH(''))as varchar(300)) as GroupName
,Cast(','+(SELECT CAST(ID as varchar(20))+',' FROM ZL_Plat_Group  Where MemberIDS Like '%,'+ CAST(UserID as varchar(20))+',%' FOR XML PATH(''))as varchar(300)) as Gid
FROM  (SELECT u.*, up.CompID,up.Plat_Group,up.Plat_Role,up.Status,up.CreateTime,up.ATCount
FROM  (SELECT A.UserID,A.UserName,A.Status AS UserStatus,A.HoneyName AS TRUENAME,A.HoneyName,A.salt AS UserFace,B.Position,b.Position as Post, B.Mobile FROM ZL_User A LEFT JOIN ZL_UserBase B ON A.UserID=B.UserID ) AS u INNER JOIN
ZL_User_Plat AS up ON u.UserID = up.UserID) AS a_1 LEFT OUTER JOIN
ZL_Plat_Comp AS b ON a_1.CompID = b.ID

GO
CREATE VIEW [dbo].[ZL_Guest_BarView]
AS
SELECT A.*,B.HoneyName FROM (SELECT     B.*, C.Catename, C.ParentID, C.NeedLog,C.[Status] as C_Status,C.SendScore,C.ReplyScore, C.PostAuth, C.BarImage,C.[Desc],C.BarInfo, C.OrderID,C.BarOwner,
(Select COUNT(ID) From ZL_Guest_Bar Where Pid=B.ID ) RCount
FROM ZL_Guest_Bar B  INNER JOIN ZL_Guestcate C ON B.CateID = C.Cateid) A LEFT JOIN ZL_User B ON A.CUser=B.UserID

GO
CREATE VIEW [dbo].[ZL_SearchView] AS
SELECT GeneralID AS ID,S=0,NodeID,ItemID,TableName,Title,Inputer,Hits,CreateTime,[Status],PageUrl='',TagKey,HtmlLink,TopImg,0 As LinPrice
from ZL_CommonModel where Status=99 And TableName like 'ZL[_]C[_]%'
UNION
select ID,S=1,Nodeid,ItemID,TableName,Proname,AddUser,AllClickNum,AddTime,Sales,'',Kayword,'',Thumbnails,LinPrice from ZL_Commodities where Sales=1 And Recycler=0 And Istrue=1 And UserShopID=0
UNION
select ID,S=2,CateID,Pid,'ZL_Guest_BarView',Title,CUName,HitCount,CDate,Status,'','','','',0 from ZL_Guest_BarView where Status=99
UNION 
SELECT ID,S=3,QueType,0,'',Qcontent,UserName,0,AddTime,Status,'','','','',0 FROM ZL_Ask where Status>0
UNION
select Gid,S=4,Cateid,0,'',Title,(select UserName from ZL_User where ZL_User.UserID=ZL_Guestbook.Userid),0,Gdate,Status,'','','','',0 from ZL_Guestbook where Parentid=0 And Status=1
UNION
select ID,S=5,Nodeid,ItemID,TableName,Proname,AddUser,AllClickNum,AddTime,Sales,'',Kayword,'',Thumbnails,LinPrice from ZL_Commodities where Sales=1 And Recycler=0 And Istrue=1 And UserShopID>0

GO
CREATE VIEW [dbo].[ZL_Order_ShareView] AS
SELECT A.*,B.HoneyName RHoney,B.UserName RUName,B.salt AS UserFace FROM 
(SELECT A.*,B.HoneyName AS CHoney,B.UserName AS CUName,B.GroupID FROM ZL_Order_Share A LEFT JOIN ZL_User B ON A.UserID=B.UserID) AS A
LEFT JOIN ZL_User AS B ON A.ReplyUid=B.UserID

GO
Create View [dbo].[ZL_Exam_ClassView] AS 
SELECT A.*,B.SchoolName,C.GradeName,D.UserName FROM ZL_Exam_ClassRoom A LEFT JOIN ZL_School B ON A.SchoolID=B.ID LEFT JOIN ZL_Grade C ON C.GradeID=A.Grade LEFT JOIN ZL_User D ON D.UserID=A.CreateUser

GO
CREATE VIEW  [dbo].[ZL_Plat_BlogView] AS
SELECT 
CateID=0,CateName='',ID,Pid,ReplyID,MsgType,Title,MsgContent,ReplyUserID,ColledIDS,LikeIDS,[Status],CUser,CUName,CDate,
GroupIDS,[Attach],EndTime,VoteOP,VoteResult,ForwardID,ATUser,CompID,
ReplyUName,Location,Source='plat'
FROM ZL_Plat_Blog Union ALL 

SELECT A.Cateid,A.CateName,-B.ID AS ID,-B.Pid AS Pid,-B.ReplyID AS ReplyID,MsgType,Title,MsgContent,ReplyUserID,ColledIDS,LikeIDS,B.[Status],CUser,CUName,CDate,
GroupIDS='',[Attach]='', EndTime='2016-09-14 18:29:21.123',VoteOP='',VoteResult='',ForwardID=0,ATUser='',IsPlat AS CompID,
ReplyUName=CUName,Location='',Source='bar' FROM ZL_Guestcate A LEFT JOIN ZL_Guest_Bar B ON A.Cateid=B.CateID
WHERE A.IsPlat>0 AND B.[Status]=99

GO
CREATE VIEW ZL_Order_PayedView AS
SELECT A.*,B.PayTime,B.PayPlatID FROM ZL_Orderinfo A LEFT JOIN ZL_Payment B 
ON A.PaymentNo=B.PayNo WHERE A.PaymentNO IS NOT NULL AND A.PaymentNO !=''

GO
CREATE VIEW  ZL_Order_ProSaleView AS
SELECT A.Pronum,A.AllMoney,A.ProID,ZL_Commodities.Proname,ZL_Commodities.Nodeid,ZL_Node.NodeName,ZL_Orderinfo.PaymentNo,ZL_Orderinfo.OrderStatus,ZL_Payment.PayTime
FROM ZL_CartPro A 
LEFT JOIN ZL_Commodities ON A.ProID=ZL_Commodities.ID
LEFT JOIN ZL_Node ON ZL_Commodities.NodeID=ZL_Node.NodeID
LEFT JOIN ZL_Orderinfo ON A.Orderlistid=ZL_Orderinfo.ID
LEFT JOIN ZL_Payment ON ZL_Orderinfo.PaymentNo=ZL_Payment.PayNo
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
SELECT GeneralID AS ID,S=0,NodeID,ItemID,TableName,Title,Inputer,Hits,CreateTime,[Status],PageUrl='',TagKey,HtmlLink
FROM ZL_CommonModel WHERE [Status]=99 AND (TableName LIKE 'ZL[_]C[_]%' OR TableName LIKE 'ZL[_]P[_]%' OR TableName LIKE 'ZL[_]S[_]%')

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
GroupIDS='',[Attach]='', EndTime='2015-09-14 18:29:21.123',VoteOP='',VoteResult='',ForwardID=0,ATUser='',IsPlat AS CompID,
ReplyUName=CUName,Location='',Source='bar' FROM ZL_Guestcate A LEFT JOIN ZL_Guest_Bar B ON A.Cateid=B.CateID
WHERE A.IsPlat>0 AND B.[Status]=99
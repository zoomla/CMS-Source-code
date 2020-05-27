<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelHelp.aspx.cs" Inherits="manage_Zone_ZoneLabelHelp" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>空间标签调用逻辑说明</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered">
            <tr>
                <td><img src="/App_Themes/AdminDefaultTheme/Images/img_u.gif" alt="" />空间标签调用逻辑说明</td>
            </tr>
            <tr>
                <td>
                    <div>
                        <div>
                            $Zone_UserLog(参数1,参数2,参数3,参数4,参数5)$ 日志列表
                            <br />
                            参数1 显示多少条<br />
                            参数2 标题字数<br />
                            参数3 0所有日志 1用户空间日志<br />
                            参数4 0降序 1为升序
                            <br />
                            参数5 0热门 1为最新
                         
                            <br />
                            <br />

                            $Zone_UserPhoto(参数1,参数2,参数3,参数4,参数5,参数6)$ 相册列表
                            <br />
                            参数1 显示多少条<br />
                            参数2 横向显示几条<br />
                            参数3 图片显示宽度<br />
                            参数4 图片显示高度<br />
                            参数5 0所有相册 1用户空间相册
                            参数6 1用户空间相册
                            <br />
                            参数6 0热门 1为最新
                            <br />
                            <br />

                            $Zone_UserGroup(参数1,参数2,参数3,参数4,参数5,参数6)$ 族群
                            <br />
                            参数1 显示多少条<br />
                            参数2 横向显示几条<br />
                            参数3 图片显示宽度<br />
                            参数4 图片显示高度<br />
                            参数5 0所有族群 1用户空间族群
                            参数6 1用户空间族群
                            <br />
                            参数6 0热门 1为最新
                            <br />
                            <br />


                            $Zone_UserBanner$ 用户栏目
                            <br />
                            <br />

                            $Zone_Name$ 空间名称
                            <br />
                            <br />

                            $Zone_LogTitle$ 日志标题
                            <br />
                            <br />

                            $Zone_LogContent$ 日志内容
                            <br />
                            <br />

                            $Zone_LogTime$ 日志添加时间
                            <br />
                            <br />

                            $Zone_PicList(参数1,参数2,参数3,参数4)$ 空间相片
                            <br />
                            参数1 显示多少条<br />
                            参数2 横向显示几条<br />
                            参数3 图片显示宽度<br />
                            参数4 图片显示高度
                            <br />
                            <br />

                            $Zone_PhotoName$ 相册名称
                            <br />
                            <br />

                            $Zone_PhotoUrl(参数1,参数2)$ 相册图片
                            <br />
                            参数1 图片宽度<br />
                            参数2 图片高度
                            <br />
                            <br />

                            $Zone_PhotoTime$ 相册添加时间
                            <br />
                            <br />

                            $Zone_PicName$ 相片名称
                            <br />
                            <br />

                            $Zone_PicUrl(参数1,参数2)$ 相片图片
                            <br />
                            <br />

                            $Zone_PicTime$ 相片添加时间
                            <br />
                            <br />

                            $Zone_Login$ 登录框
                            <br />
                            <br />

                            $Zone_ZoneList(参数1,参数2,参数3,参数4,参数5)$ 用户空间列表
                            <br />
                            参数1 显示多少条<br />
                            参数2 横向显示几条<br />
                            参数3 图片显示宽度<br />
                            参数4 图片显示高度<br />
                            参数5 1为最新
                            <br />
                            <br />

                            $Zone_StyleList(参数1,参数2,参数3,参数4,参数5)$ 空间模板列表
                            <br />
                            参数1 显示多少条<br />
                            参数2 横向显示几条<br />
                            参数3 图片显示宽度<br />
                            参数4 图片显示高度<br />
                            参数5 1为最新&nbsp;<br />
                            <br />

                            $Zone_GroupName$ 族群名称
                            <br />
                            <br />

                            $Zone_GroupUrl(参数1,参数2)$ 族群图标
                            <br />
                            参数1 图片宽度<br />
                            参数2 图片高度
                            <br />
                            <br />

                            $Zone_GroupTime$ 族群创建时间
                            <br />
                            <br />

                            $Zone_GroupToPic(参数1,参数2,参数3,参数4)$ 族群话题
                            <br />
                            参数1 显示多少条<br />
                            参数2 标题字数<br />
                            参数3 0所有话题 1用户空间话题
                            参数4 1用户空间话题
                            <br />
                            参数4 0热门 1为最新
                            <br />
                            <br />

                            $Zone_TopicName(参数1)$ 话题标题
                            <br />
                            参数1 话题ID<br />
                            <br />
                            <br />

                            $Zone_TopicTime(参数1)$ 话题发表时间
                            <br />
                            参数1 话题ID<br />
                            <br />
                            <br />

                            $Zone_TopicContent(参数1)$ 话题内容
                            <br />
                            参数1 话题ID<br />
                            <br />
                            <br />

                            $Zone_TopicRe(参数1)$ 话题回复内容
                            <br />
                            参数1 话题ID<br />
                            <br />
                            <br />

                            $Zone_GroupUser(参数1,参数2,参数3,参数4)$ 族群成员
                            <br />
                            参数1 显示总数<br />
                            参数2 横向显示几条<br />
                            参数3 头像宽度<br />
                            参数4 族群成员<br />
                            <br />
                            <br />

                            $Zone_UserFriend(参数1,参数2,参数3,参数4)$ 好友列表
                            <br />
                            参数1 显示总数<br />
                            参数2 横向显示几条<br />
                            参数3 头像宽度<br />
                            参数4 头像高度
                            <br />
                            <br />

                            $Zone_Message$ 留言列表
                            <br />
                            <br />

                            $Zone_Comment$ 评论和回复
                            <br />
                            <br />

                            $Zone_HomeSet$ 显示小屋
                            <br />
                            <br />
                            $Zone_Online$ 在线人数<br />
                            <br />
                            $Zone_UserHot(参数1,参数2)$ 人气排名<br />
                            参数1 0为女生 1为男生<br />
                            参数2 显示条数
                            <br />
                            <br />
                            $Zone_UserSearch$ 好友搜索
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

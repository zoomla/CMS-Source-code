/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： Answer.cs
// 文件功能描述：定义数据表Answer的业务实体
//
// 创建标识：Owner(2008-10-22) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///Answer业务实体
    /// </summary>
    [Serializable]
    public class Answer
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///答案内容
        ///</summary>
        private string answerContent = String.Empty;

        ///<summary>
        ///是否正确
        ///</summary>
        private bool isRight;

        private Guid questionID;

       


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public Answer()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Answer
        (
            Guid iD,
            string answerContent,
            bool isRight
        )
        {
            this.iD = iD;
            this.answerContent = answerContent;
            this.isRight = isRight;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///答案内容
        ///</summary>
        public string AnswerContent
        {
            get { return answerContent; }
            set { answerContent = value; }
        }

        ///<summary>
        ///是否正确
        ///</summary>
        public bool IsRight
        {
            get { return isRight; }
            set { isRight = value; }
        }


        public string IsRightS
        {
            get
            {
                if (IsRight)
                    return "正确答案";
                else return "错误答案";
            }
        }

        /// <summary>
        /// 问题编号
        /// </summary>
        public Guid QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }

        #endregion

    }
}

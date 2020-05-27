using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.HtmlLabel.Task
{
    public class T_ExecuteSQL : BaseTask
    {
        public T_ExecuteSQL(M_Content_ScheTask scheMod)
        {
            base.TaskFlag = "T_ExecuteSQL";
            base.scheMod = scheMod;
            base.SetInterval();
            base.ExecuteTask += new ElapsedEventHandler(ExecuteFunc);
        }
        public override void ExecuteFunc(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (scheMod.TaskContent.StartsWith("/")) //若以'/'或'\'开头则为脚本
            {
                DBHelper.ExecuteSqlScript(DBCenter.DB.ConnectionString, function.VToP(scheMod.TaskContent));
            }
            else
            {
                SqlHelper.ExecuteSql(scheMod.TaskContent);
            }
        }
    }
}

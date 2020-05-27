using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;

/*
 * OA表单模板父类
 */
namespace ZoomLa.BLL.MIS
{
    /*
     * 1,InitControl
     * 2,MyBind()
     * 3,保存前必须重新为vstate赋值
     */
    public class B_OAFormUI : System.Web.UI.UserControl
    {
        private B_ModelField fieldBll = new B_ModelField();
        public StateBag vstate = null;
        public int ModelID
        {
            get
            {
                if (vstate == null) return 0;
                return Convert.ToInt32(vstate["ascx_modelid"]);
            }
            set {
                if (vstate != null)
                { vstate["ascx_modelid"] = value; }
            }
        }
        public DataRow dataRow = null;
        public void InitControl(StateBag vs, int modelid)
        {
            this.vstate = vs;
            this.ModelID = modelid;
            //如果是下拉选单,则装载定义好的值
            //ListBoxType
            DataTable dt = fieldBll.GetModelFieldListall(modelid);
            DataRow[] dpdrs = dt.Select("FieldType='ListBoxType'");
            foreach (DataRow dr in dpdrs)
            {
                InitDP(dr["FieldName"].ToString(), dr);
            }
        }
        //初始化解析DP
        /// <summary>
        /// 需要用于填充的dp控件值
        /// </summary>
        /// <param name="dr"></param>
        ///  //1=1|1.1$0||2|2.2$0||3|3$0,
        private void InitDP(string cname,DataRow dr) 
        {
            var control = this.FindControl(cname.Trim());
            if (control==null||!control.GetType().Name.Equals("DropDownList")) return;
            string opstr = dr["Content"].ToString().Split('=')[1];
            if (string.IsNullOrEmpty(opstr)) return;
            DropDownList dp = (DropDownList)control;
            string[] opts = Regex.Split(opstr,Regex.Escape("||"));
            //开始添加
            foreach (string opt in opts)
            {
                string[] arr=opt.Replace("$0","").Split('|');
                string text = arr[0];
                string value = arr.Length > 1 ? arr[1] : text;
                dp.Items.Add(new ListItem(text, value));
            }
        } 
        public virtual void MyBind(string fields="") 
        {
            if (string.IsNullOrEmpty(fields))
            {
                fields = GetFields(ModelID);
            }
            if (string.IsNullOrEmpty(fields)) { throw new Exception("未指定字段,字段为空,模型" + ModelID); }
            if(dataRow==null)return;
            foreach (string field in fields.Split(','))
            {
                SetVal(field,dataRow);
            }
        }
        /// <summary>
        /// 返回需要操作的字段
        /// </summary>
        /// <returns></returns>
        public string GetFields(int modelid)
        {
            DataTable dt = fieldBll.GetModelFieldListall(modelid);
            string fields = "";
            foreach (DataRow dr in dt.Rows)
            {
                fields += dr["FieldName"] + ",";
            }
            return fields.Trim(',');
        }
        public virtual DataTable CreateTable(string[] fields)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            foreach (string field in fields)
            {
                string value = GetVal(field);
                AddRow(table, field, value);
            }
            return table;
        }
        /*---------Tools----------*/
        /// <summary>
        /// 根据控件ID,返回控件的值
        /// </summary>
        /// <param name="cname">控件ID</param>
        /// <returns>值</returns>
        public string GetVal(string field)
        {
            string value = "";
            var text = this.FindControl(field);
            if (text != null)
            {
                switch (text.GetType().Name)
                {
                    case "Label":
                        value = ((Label)text).Text;
                        break;
                    case "DropDownList":
                        value = ((DropDownList)text).SelectedValue;
                        break;
                    default:
                        value = ((TextBox)text).Text;
                        break;
                }
            }
            return value;
        }
        //填充值
        public void SetVal(string field, DataRow dr)
        {
            SetVal(field, DataConvert.CStr(dr[field]));
        }
        public void SetVal(string field,string value)
        {
            var text = this.FindControl(field);
            if (text != null)
            {
                switch (text.GetType().Name)
                {
                    case "Label":
                        ((Label)text).Text = value;
                        break;
                    case "DropDownList":
                        ((DropDownList)text).SelectedValue = value;
                        break;
                    default:
                        ((TextBox)text).Text = value;
                        break;
                }
            }

        }
        public void AddRow(DataTable dt, string field, string value)
        {
            DataRow dr = dt.NewRow();
            dr["FieldName"] = field;
            dr["FieldValue"] = value;
            dr["FieldType"] = "TextType";
            dt.Rows.Add(dr);
        }
        //-----OA_Document中自带的字段,必须实现
        public string Title_ASCX
        {
            get { return GetVal("Title_T"); }
            set { SetVal("Title_T", value); }
        }
        public string SendDate_ASCX
        {
            get { return GetVal("SendDate_T"); }
            set { SetVal("SendDate_T", value); }
        }
        public string NO_ASCX
        {
            get { return GetVal("NO_T"); }
            set { SetVal("NO_T", value); }
        }
        public TextBox No_ASCX_T
        {
            get { return this.FindControl("NO_T") as TextBox; }
        }
    }
}

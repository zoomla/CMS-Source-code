using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Controls
{
    internal sealed class InputCheckBoxField : CheckBoxField
    {
        public const string CheckBoxID = "CheckBoxButton";

        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);
            if (cell.Controls.Count == 0)
            {
                CheckBox child = new CheckBox();
                child.ID = "CheckBoxButton";
                cell.Controls.Add(child);
            }
        }
    }
}


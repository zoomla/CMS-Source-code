using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBDiff.Model
{
    public class M_DB_Table
    {
        public string TbName = "";
        public string PK = "";
        public string[,] FieldList = {};
        public M_DB_Table(string tbname, string pk,  string[,] fieldlist)
        {
            TbName = tbname;
            PK = pk;
            FieldList = fieldlist;
        }
    }
}

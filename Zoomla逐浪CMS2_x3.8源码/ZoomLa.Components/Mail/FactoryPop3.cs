using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Components.Mail
{
    public class FactoryPop3
    {
        public String Pop3Type = "OpenPop";
        public Pop3 CreatePop3()
        {
            Pop3 pop = null;
            if (Pop3Type == "OpenPop")
            {
                return pop = new OpenPopPop3();
            }
            //else if (Pop3Type == "LumiSoft")
            //{
            //    return pop = new LumiSoftPop3();
            //}
            else
            {
                return null;
            }
        }       
    }
}

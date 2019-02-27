using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;

namespace BDULogic
{
    public class Servant_Logic
    {

        #region ÐÞ¸ÄÆÍÈËÕÛ¿Û
        public static void Update(Servant sitem)
        {
            string SQLstr = @"UPDATE ZL_Servant SET [Agio] = @Agio
 WHERE [ServantID] = @ServantID,[MasterID] = @MasterID";
            if (SQLstr.Length > 0) { }

        }
        #endregion
    }
}

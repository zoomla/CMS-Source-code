using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model.Page;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Page;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace ZoomLa.BLL
{
    public class B_Content_Count
    {
        //按天数统计
        public DataTable GroupDayCount(int NodeID, int year, int moth, int modelid)
        {
            string nodewhere = NodeID <= 0 ? "" : "AND A.NodeID=" + NodeID;
            string yearwhere = year <= 0 ? "" : " AND DATENAME(YEAR,A.CreateTime)=" + year;//年
            string mothwhere = moth <= 0 ? "" : " AND DATENAME(MONTH,A.CreateTime)=" + moth;
            string modelidwhere = modelid == -1 ? "" : " AND A.ModelID=" + modelid;
            string sql = "COUNT(A.GeneralID) AS PCount,COUNT(B.GeneralID) AS ComCount,SUM(A.Hits) AS Hits,CAST(DATENAME(DAY,A.CreateTime) as Int) AS Days,DATENAME(MONTH,A.CreateTime) AS Months,"
                        + " DATENAME(YEAR,A.CreateTime) AS Years";
            return SqlHelper.JoinQuery(sql, "ZL_CommonModel", "ZL_Comment", "A.GeneralID=B.GeneralID", "1=1 " + nodewhere + yearwhere
                                        + mothwhere + modelidwhere + " GROUP BY DATENAME(DAY,A.CreateTime),DATENAME(MONTH,A.CreateTime),DATENAME(YEAR,A.CreateTime) order by Years,Months, Days ");
        }
        public DataTable SelGroupByDate(int nodeid, int modelid, string sdate, string edate, string names = "")
        {
            string sql = "COUNT(A.GeneralID) AS PCount,COUNT(B.GeneralID) AS ComCount,SUM(A.Hits) AS Hits,A.Inputer";
            string where = " 1=1 ";
            if (nodeid > 0)
            {
                where += " AND A.NodeID=" + nodeid;
            }
            if (modelid > 0)
            {
                where += " AND A.ModelID=" + modelid;
            }
            if (!string.IsNullOrEmpty(names))
            {
                where += " AND A.Inputer IN(@names)";
            }
            if (!string.IsNullOrEmpty(sdate))
            {
                where += " AND A.CreateTime >@sdate";
            }
            if (!string.IsNullOrEmpty(edate))
            {
                where += " AND A.CreateTime < @edate";
            }
            where += " GROUP BY A.Inputer";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("names",names),
                new SqlParameter("sdate", sdate),
                new SqlParameter("edate", edate)
            };
            return SqlHelper.JoinQuery(sql, "ZL_CommonModel", "ZL_Comment", "A.GeneralID=B.GeneralID", where, "", sp);
        }

        public DataTable SelRankData(int order, int fid = -1)
        {
            string orderstr = "";
            string wherestr = fid > -1 ? "A.FirstNodeID=" + fid : "B.ParentID=0";
            switch (order)
            {
                case 1:
                    orderstr = "Hits";
                    break;
                case 2:
                    orderstr = "ComCount";
                    break;
                case 3:
                    orderstr = "GCount";
                    break;
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@order", order) };
            string sql = "Top 10 A.Hits,(SELECT COUNT(*) FROM ZL_Comment comm WHERE comm.GeneralID=A.GeneralID) ComCount,(SELECT COUNT(*) FROM ZL_Comment_Count GCount WHERE GCount.GeneralID=A.GeneralID) GCount ,A.Title";
            return SqlHelper.JoinQuery(sql, "ZL_CommonModel", "ZL_Node", "A.NodeID=B.NodeID", wherestr, orderstr + " DESC", sp);
        }

    }
}

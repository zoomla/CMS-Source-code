using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_Favorite:M_Base
    {
        public int FavoriteID { get; set; }
        /// <summary>
        /// 收藏人ID
        /// </summary>
        public int Owner { get; set; }
        /// <summary>
        /// 被收藏内容的ID
        /// </summary>
        public int InfoID { get; set; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 收藏类型：1:内容,2:商品,3网店,4:问题,5:百科
        /// </summary>
        public int FavoriType { get; set; }
        /// <summary>
        /// 收藏URl
        /// </summary>
        public string FavUrl { get; set; }
        /// <summary>
        /// 收藏相关内容ID(多个ID以逗号隔开)
        /// </summary>
        public string FavItemID { get; set; }
        /// <summary>
        /// 收藏标题
        /// </summary>
        public string Title { get; set; }
        public override string PK { get { return "FavoriteID"; } }
        public override string TbName { get { return "ZL_Favorite"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"FavoriteID","Int","4"},
                                  {"Owner","Int","4"},
                                  {"InfoID","Int","4"},
                                  {"FavoriteDate","DateTime","8"}, 
                                  {"FavoriType","Int","4"},
                                  {"FavUrl","NVarChar","500"}, 
                                  {"FavItemID","NVarChar","50"},
                                  {"Title","NVarChar","500"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Favorite model = this;
            if (model.AddDate <= DateTime.MinValue) { model.AddDate = DateTime.Now; }
            SqlParameter[] sp =GetSP();
            sp[0].Value = model.FavoriteID;
            sp[1].Value = model.Owner;
            sp[2].Value = model.InfoID;
            sp[3].Value = model.AddDate;
            sp[4].Value = model.FavoriType;
            sp[5].Value = model.FavUrl;
            sp[6].Value = model.FavItemID;
            sp[7].Value = model.Title;
            return sp;
        }
        public M_Favorite GetModelFromReader(SqlDataReader rdr)
        {
            M_Favorite model = new M_Favorite();
            model.FavoriteID = Convert.ToInt32(rdr["FavoriteID"]);
            model.Owner = ConvertToInt(rdr["Owner"]);
            model.InfoID = ConvertToInt(rdr["InfoID"]);
            model.AddDate = ConvertToDate(rdr["FavoriteDate"]);
            model.FavoriType = ConvertToInt(rdr["FavoriType"]);
            model.FavUrl = ConverToStr(rdr["FavUrl"]);
            model.FavItemID = ConverToStr(rdr["FavItemID"]);
            model.Title = ConverToStr(rdr["Title"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
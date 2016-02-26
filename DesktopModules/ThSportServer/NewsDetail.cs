using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.OleDb;
using System.Configuration;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Modules;
using System.Data;
using DotNetNuke.Data;
using System.Data.SqlClient;
using DotNetNuke.Services.Exceptions;

namespace SportSiteServer
{
	public class NewsDetail
	{
		public int Compitetionid { get; set; }
		public int TeamId { get; set; }
		public string Createdby { get; set; }
		public string Modifyby { get; set; }
		public string Title { get; set; }
		public string NewsDate { get; set; }
		public string Description { get; set; }
		public int PortalId { get; set; }
		public int newsid { get; set; }
		public DateTime createdon { get; set; }
		public string News_UploadPhoto { get; set; }

		public int GameKeyValue { get; set; }
		public int SportLight { get; set; }
        public int IsNationalTeam { get; set; }
	}
	public class NewsDetailControl
	{
		private readonly DataProvider dataProvider = DataProvider.Instance();

    	public int InsertNewsDetail(NewsDetail news)
		{
			try
			{
				dataProvider.ExecuteNonQuery("usp_InsertNewsDetail", news.TeamId, news.Compitetionid, news.Title, news.Description, news.PortalId, news.Createdby, news.Modifyby, news.News_UploadPhoto, news.GameKeyValue, news.SportLight, news.NewsDate,news.IsNationalTeam);
			}
			catch (Exception ex)
			{
				Exceptions.LogException(ex);
			}
			return 0;
		}

		public int UpdateNewsDetail(NewsDetail news)
		{
			try
			{
				dataProvider.ExecuteNonQuery("usp_UpdateNewsDetail", news.newsid, news.TeamId, news.Compitetionid, news.Title, news.Description, news.PortalId, news.Modifyby, news.News_UploadPhoto, news.GameKeyValue, news.SportLight, news.NewsDate,news.IsNationalTeam);
			}
			catch (Exception ex)
			{
				Exceptions.LogException(ex);
			}
			return 0;
		}

        public DataTable GetPhotoNewsDetail(int NewsID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPhotoNewsDetail", NewsID))
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }
                return dt;
            }
        }

        public DataTable GetNewsDetail(string current_user, int id, int flag, int IsNationalTeam,int SportStageValue)
		{
			DataTable dt = new DataTable();
			try
			{
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetAllNewsInfo", current_user, id, flag, IsNationalTeam, SportStageValue))
				{
					dt.Load(rdr);
				}
				return dt;
			}
			catch (Exception e)
			{
    		}
			return dt;

		}

        public DataTable GetAllNewsDetailInfo(int SportStageValue)
		{

			DataTable dt = new DataTable();
			try
			{
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetAllNewsDetailInfo", SportStageValue))
				{
					dt.Load(rdr);
				}
				return dt;
			}
			catch (Exception e)
			{

			}
			return dt;

		}

        public DataTable GetNewsDetailByNewsID( int NewsID )
		{
            DataTable dt = new DataTable();
			try
			{
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetNewsDetailByNewsID",NewsID))
				{
					dt.Load(rdr);
				}
				return dt;
			}
			catch (Exception e)
			{

			}
			return dt;

		}

		public DataTable Getteam()
		{
			DataTable dt = new DataTable();
			try
			{
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_Getteam"))
				{
					dt.Load(rdr);
				}
				return dt;
			}
			catch (Exception e)
			{

			}
			return dt;
		}

		public DataTable GetNewsDetailbyid(int newsid)
		{

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNewsDetailbyid", newsid))
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }

                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }

                return dt;
            }
		}

		public int DeleteNews(int Newsid)
		{
			int i = 0;
			try
			{
                dataProvider.ExecuteNonQuery("usp_DeleteNews", Newsid);
				return i;
			}
			catch (Exception ex)
			{
				Exceptions.LogException(ex);
			}
			return i;
		}


		public DataTable GetNewsByCompetitionId(int competitionId)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNewsByCompetitionId", competitionId, DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().PortalId))
					{
						dt.Load(reader);
						return dt;
					}
				}

				catch (Exception ex)
				{
					Exceptions.LogException(ex);
				}

				return dt;
			}
		}

		public DataTable GetNewsByTeamId(int teamId)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNewsByTeamId", teamId, DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().PortalId))
					{
						dt.Load(reader);
						return dt;
					}
				}

				catch (Exception ex)
				{
					Exceptions.LogException(ex);
				}

				return dt;
			}
		}

        public DataTable GetNewsByNationalTeamId(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNewsByNationalTeamId", teamId, DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().PortalId))
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }

                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }

                return dt;
            }
        }


		public DataTable GetAllNews()
		{
			DataTable dt = new DataTable();

			try
			{
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetAllNews"))
				{
					dt.Load(rdr);
				}
				return dt;
			}
			catch (Exception e)
			{

			}
			return dt;
		}

        public DataSet GetAllCupNews(int SportStageValue)
		{
			using (DataSet ds = new DataSet())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCupNews", SportStageValue,DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().PortalId))
					{
						ds.Load(reader, LoadOption.Upsert, "FeaturedNews", "LatestNews");
						return ds;
					}
				}
				catch (Exception ex)
				{
					Exceptions.LogException(ex);
				}

				return ds;
			}
		}

        public DataSet GetAllLeagueNews(int SportStageValue)
		{
			using (DataSet ds = new DataSet())
			{
				try
				{
					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLeagueNews",SportStageValue, DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().PortalId))
					{
						ds.Load(reader, LoadOption.Upsert, "FeaturedNews", "LatestNews");
						return ds;
					}
				}
				catch (Exception ex)
				{
					Exceptions.LogException(ex);
				}

				return ds;
			}
		}

        public DataSet GetAllTeamNews(int SportStageValue)
		{
			using (DataSet ds = new DataSet())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamNews", SportStageValue,DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().PortalId))
					{
						ds.Load(reader, LoadOption.Upsert, "FeaturedNews", "LatestNews");
						return ds;
					}
				}
				catch (Exception ex)
				{
					Exceptions.LogException(ex);
				}

				return ds;
			}
		}

        public DataTable GetAllPlayerSportLightNews()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("GetAllPlayerSportLightNews"))
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }

                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                }

                return dt;
            }
        }

        public DataTable GetAllPlayerSportLightNews(int SportStageValue)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllPlayerSportLightNews", SportStageValue))
					{
						dt.Load(reader);
						return dt;
					}
				}

				catch (Exception ex)
				{
					Exceptions.LogException(ex);
				}

				return dt;
			}
		}
	}
}

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
	public class clsTeamDirectory
	{

	}

	public class clsTeamDirectoryController
	{
		private readonly DataProvider dataProvider = DataProvider.Instance();
		private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

		public clsTeamDirectoryController()
		{

		}

		#region GetData Methods


		public DataTable FetchAllLeaguesList(string competition_type)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{

					using (IDataReader reader = dataProvider.ExecuteReader("usp_FetchAllLeaguesList", competition_type))
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

        public DataTable GetDetailAllTeamList()
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDetailAllTeamList"))
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

		public DataTable GetDetailAllTeamCompetitionGroup()
		{
			using (DataTable dt = new DataTable())
			{
				try
				{

					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDetailAllTeamCompetitionGroup"))
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

		public DataTable GetDateCompetitioNameAndID()
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
				    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDateCompetitioNameAndID"))
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

        public DataTable GetDetailAllCompetitionList(int CompetitionID, int SportStageValue,int SeasonID)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCompetitionList", CompetitionID, SportStageValue, SeasonID))
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

        public DataTable GetDetailAllCompetitionListWithResults(int Flage, int CompetitionID, int SportStageValue)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCompetitionListWithResults", Flage, CompetitionID, SportStageValue))
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

        public DataTable GetDetailAllLeagueList(int CompetitionID, int SportStageValue, int SeasonID)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllLeagueList", CompetitionID, SportStageValue, SeasonID))
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

        public DataTable GetDetailAllLeagueListWithResults(int flage, int competitionid, int SportStageValue)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
			        using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllLeagueListWithResults", flage, competitionid, SportStageValue))
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

		public DataTable GetDateTeamNameAndID()
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDateTeamNameAndID"))
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

		public DataTable GetDateCompetitioGroupNameAndID()
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
				    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDateCompetitioGroupNameAndID"))
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

		public DataTable GetDateGroupNameAndIDByCompetitionID(int CompetitionID)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
			        using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDateGroupNameAndIDByCompetitionID", CompetitionID))
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

		public DataTable GetCompetitionListByCompetitionType(string competition_type)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{

					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionListByCompetitionType", competition_type))
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

		public DataTable GetDetailAllTeamCompetitionGroupByCompetitionId(int competitionID)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{

					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDetailAllTeamCompetitionGroupByCompetitionId", competitionID))
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

        // this Proc Use For Team Directory 
        public DataTable GetDetailAllTeamByCompetitionId(int competitionID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDetailAllTeamByCompetitionId", competitionID))
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

		public DataTable GetDetailAllTeamCompetitionGroupByGroupId(int groupid)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDetailAllTeamCompetitionGroupByGroupId", groupid))
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

        public DataTable GetDetailAllTeamListByTeamName(string str, int SportValueID)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDetailAllTeamListByTeamName", str, SportValueID))
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

		public DataTable GetDetailAllTeamCompetitionGroupByTeamId(string str)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
					using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDetailAllTeamCompetitionGroupByTeamName", str))
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

		public DataTable GetTeamDetailByTeamId(string str)
		{
			using (DataTable dt = new DataTable())
			{
				try
				{
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamDetailByTeamId", str))
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
		#endregion GetData Methods

        public DataTable GetAllCompetitionListWithResultsStandingcupleague(int Flage, int CompetitionID, int SportStageValue, int SeasonID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCompetitionListWithResultsStandingcupleague", Flage, CompetitionID, SportStageValue, SeasonID))
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

        public DataTable GetAllCompetitionListWithResultsStandingleague(int Flage, int CompetitionID, int SportStageValue, int SeasonID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCompetitionListWithResultsStandingleague", Flage, CompetitionID, SportStageValue, SeasonID))
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

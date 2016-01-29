using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using System.Collections;

namespace ThSportServer
{
    public class clsNews
    {
        public int NewsId { get; set; }
        public int NewsLevelId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDesc { get; set; }
        public string NewsText { get; set; }
        public string NewsDate { get; set; }
        public string NewsPicture { get; set; }
        public string NewsVideo { get; set; }
        public int ActiveFlagId { get; set; }
        public int ShowFlagId { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public string NewsOtherVideoPath { get; set; }
        public int VideoType { get; set; }
            
        public int SportsId { get; set; }
        public int CountryId { get; set; }
        public int EventId { get; set; }
        public int SeasonId { get; set; }
        public int CompetitionId { get; set; }
        public int ClubId { get; set; }
        public int ClubOwnersId { get; set; }
        public int ClubMemberId { get; set; }
        public int TeamId { get; set; }
        public int TeamMemberId { get; set; }
        public int PlayerId { get; set; }
        public int SponsorId { get; set; }
    }

    public class clsNewsController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsNewsController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataSponsor()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataSponsor"))
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


        #endregion Getdata Methods

        #region Insert,Update,Delete Methods

        public int InsertNews(clsNews cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertNews", cs.NewsLevelId, cs.NewsTitle, cs.NewsDesc, cs.NewsText, Convert.ToDateTime(cs.NewsDate), cs.NewsPicture, cs.NewsVideo, cs.ActiveFlagId, cs.ShowFlagId, cs.PortalID, cs.CreatedById, cs.ModifiedById, cs.NewsOtherVideoPath, cs.VideoType);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertNewsLinks(clsNews cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertNewsLinks", cs.NewsId, cs.SportsId, cs.CountryId, cs.SeasonId, cs.CompetitionId, cs.ClubId, cs.ClubOwnersId, cs.ClubMemberId, cs.TeamId, cs.TeamMemberId, cs.SponsorId, cs.EventId, cs.PlayerId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateNews(clsNews cs)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateNews", cs.NewsId,cs.NewsLevelId,cs.NewsTitle,cs.NewsDesc,cs.NewsText,Convert.ToDateTime(cs.NewsDate),cs.NewsPicture,cs.NewsVideo,cs.ActiveFlagId,cs.ShowFlagId,cs.PortalID,cs.ModifiedById,cs.NewsOtherVideoPath,cs.VideoType);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdateNewsLinks(clsNews cs)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateNewsLinks", cs.NewsId,cs.SportsId,cs.CountryId,cs.SeasonId,cs.CompetitionId,cs.ClubId,cs.ClubOwnersId,cs.ClubMemberId,cs.TeamId,cs.TeamMemberId,cs.SponsorId,cs.EventId,cs.PlayerId);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetNewsDataByNewsID(int NewsID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNewsDataByNewsID", NewsID))
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

        public DataTable GetSportIDAndSportName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSportIDAndSportName"))
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

        public DataTable GetEventIDAndEventName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetEventIDAndEventName"))
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

        public DataTable GetSeasonIDAndSeasonName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSeasonIDAndSeasonName"))
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

        public DataTable GetPlayerIDAndPlayerName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerIDAndPlayerName"))
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

        public DataTable GetPlayerIDAndPlayerNameByTeamID(int TeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerIDAndPlayerNameByTeamID", TeamID))
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

        public DataTable GetCompetitionIDAndCompetitionName(int SportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionIDAndCompetitionName", SportID))
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

        public DataTable GetClubIDAndClubName(int SportID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubIDAndClubName", SportID))
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

        public DataTable GetTeamIDAndTeamName(int SportID, int ClubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamIDAndTeamName", SportID, ClubID))
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

        public DataTable GetClubOwnerIDAndClubOwnerName(int ClubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubOwnerIDAndClubOwnerName", ClubID))
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

        public DataTable GetClubMemberIDAndClubMemberName(int ClubID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetClubMemberIDAndClubMemberName", ClubID))
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

        public DataTable GetTeamMemberIDAndTeamMemberName(int TeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamMemberIDAndTeamMemberName", TeamID))
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

        public DataTable GetLatestNewsID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestNewsID"))
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

        public DataTable FillComptitionIDAndCompetitionName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillComptitionIDAndCompetitionName"))
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

        public DataTable FillClubIDAndClubName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillClubIDAndClubName"))
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

        public DataTable FillClubOwnerIDAndClubOwnerName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillClubOwnerIDAndClubOwnerName"))
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

        public DataTable FillClubMemberIDAndClubMemberName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillClubMemberIDAndClubMemberName"))
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


        public DataTable FillTeamIDAndTeamName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillTeamIDAndTeamName"))
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

        public DataTable FillTeamMemberIDAndTeamMemberName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillTeamMemberIDAndTeamMemberName"))
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

        public DataTable FillCountryIDAndCountryName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_FillCountryIDAndCountryName"))
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

        public DataTable GetNewsLogoByNewsID(clsNews cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNewsLogoByNewsID", cs.NewsId))
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

        public DataTable GetSponsorIDAndSponsorName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSponsorIDAndSponsorName"))
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

        public DataTable GetOtherVideoPathByNewsID(clsNews cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetOtherVideoPathByNewsID", cs.NewsId))
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

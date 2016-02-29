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
    public class clsVideos
    {
        public int VideoId { get; set; }
        public string VideoLevelId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDesc { get; set; }
        public string VideoDate { get; set; }
        public int VideoType { get; set; }
        public string VideoYouTubeFile { get; set; }
        public string VideoOtherFile { get; set; }
        public string PictureFile { get; set; }
        public int ActiveFlagId { get; set; }
        public int ShowFlagId { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }

        public int SportId { get; set; }
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

    public class clsVideosController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsVideosController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataVideo()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataVideo"))
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

        public DataTable GetAllCompetition()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCompetition"))
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

        public DataTable GetAllTeams()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllTeams"))
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

        public DataTable GetAllClubs()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllClubs"))
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

        public DataTable GetAllPlayer()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllPlayer"))
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

        public int InsertVideo(clsVideos cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertVideo", cs.VideoLevelId,cs.VideoTitle,cs.VideoDesc,Convert.ToDateTime(cs.VideoDate),cs.VideoType,cs.VideoYouTubeFile,cs.VideoOtherFile,cs.ActiveFlagId,cs.ShowFlagId,cs.PortalID,cs.CreatedById,cs.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertVideoLinks(clsVideos cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertVideoLinks", cs.VideoId,cs.SportId,cs.CountryId,cs.SeasonId,cs.CompetitionId,cs.ClubId,cs.ClubOwnersId,cs.ClubMemberId,cs.TeamId,cs.TeamMemberId,cs.SponsorId,cs.EventId,cs.PlayerId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateVideo(clsVideos cs)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateVideo", cs.VideoId,cs.VideoLevelId,cs.VideoTitle,cs.VideoDesc,Convert.ToDateTime(cs.VideoDate),cs.VideoType,cs.VideoYouTubeFile,cs.VideoOtherFile,cs.ActiveFlagId,cs.ShowFlagId,cs.PortalID,cs.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdateVideoLinks(clsVideos cs)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateVideoLinks", cs.VideoId,cs.SportId,cs.CountryId,cs.SeasonId,cs.CompetitionId,cs.ClubId,cs.ClubOwnersId,cs.ClubMemberId,cs.TeamId,cs.TeamMemberId,cs.SponsorId,cs.EventId,cs.PlayerId);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetVideoDataByVideoID(int VideoID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetVideoDataByVideoID", VideoID))
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

        public DataTable GetLatestVideoID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestVideoID"))
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

        public DataTable GetPictureLogoByPictureID(clsPictures cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPictureLogoByPictureID", cs.PictureId))
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

        public DataTable GetOtherVideoPathByVideoID(clsVideos cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetOtherVideoPathByVideoID", cs.VideoId))
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

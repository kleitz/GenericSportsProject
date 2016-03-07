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
  
    public class clsPictures
    {
        public int PictureId { get; set;} 
        public string PictureLevelId { get; set;} 
        public string PictureTitle { get; set;} 
        public string PictureDesc { get; set;} 
        public string PictureDate { get; set;} 
        public string PictureFile { get; set;} 
        public int ActiveFlagId { get; set;} 
        public int ShowFlagId { get; set;} 
        public int PortalID { get; set;} 
        public string CreatedById { get; set;} 
        public string ModifiedById { get; set;} 

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

    public class clsPicturesController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsPicturesController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataPicture()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataPicture"))
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

        public int InsertPicture(clsPictures cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertPicture", cs.PictureLevelId, cs.PictureTitle, cs.PictureDesc, Convert.ToDateTime(cs.PictureDate), cs.PictureFile, cs.ActiveFlagId, cs.ShowFlagId, cs.PortalID, cs.CreatedById, cs.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertPictureLinks(clsPictures cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertPictureLinks", cs.PictureId, cs.SportsId, cs.CountryId, cs.SeasonId, cs.CompetitionId, cs.ClubId, cs.ClubOwnersId, cs.ClubMemberId, cs.TeamId, cs.TeamMemberId, cs.SponsorId, cs.EventId, cs.PlayerId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdatePicture(clsPictures cs)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdatePicture", cs.PictureId,cs.PictureLevelId,cs.PictureTitle,cs.PictureDesc,Convert.ToDateTime(cs.PictureDate),cs.PictureFile,cs.ActiveFlagId,cs.ShowFlagId,cs.PortalID,cs.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdatePictureLinks(clsPictures cs)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdatePictureLinks", cs.PictureId, cs.SportsId,cs.CountryId,cs.SeasonId,cs.CompetitionId,cs.ClubId,cs.ClubOwnersId,cs.ClubMemberId,cs.TeamId,cs.TeamMemberId,cs.SponsorId,cs.EventId,cs.PlayerId);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        public DataTable GetPictureDataByPictureID(int PictureID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPictureDataByPictureID", PictureID))
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

        public DataTable GetLatestPictureID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestPictureID"))
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

   
    }
}

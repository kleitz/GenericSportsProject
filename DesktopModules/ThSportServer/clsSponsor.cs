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
    public class clsSponsor
    {
        public int SponsorId { get; set; }
        public int SponsorTypeId { get; set; }
        public int SponsorLevelId { get; set; }
        public string SponsorName { get; set; }
        public string SponsorAbbr { get; set; }
        public string SponsorDesc { get; set; }
        public string SponsorLogoName { get; set; }
        public string SponsorLogoFile { get; set; }
        public string SponsorStartDate { get; set; }
        public string SponsorEndDate { get; set; }
        public int SponsorAmt { get; set; }
        public int ActiveFlagId { get; set; }
        public int ShowFlagId { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }

        public int SportsId { get; set; }
        public int EventId { get; set; }
        public int SeasonId { get; set; }
        public int CompetitionId { get; set; }
        public int ClubId { get; set; }
        public int ClubOwnersId { get; set; }
        public int ClubMemberId { get; set; }
        public int TeamId { get; set; }
        public int TeamMemberId { get; set; }
        public int PlayerId { get; set; }
    }

    public class clsSponsorController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsSponsorController()
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

        public int InsertSponsor(clsSponsor cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertSponsor", cs.SponsorLevelId, cs.SponsorTypeId, cs.SponsorName, cs.SponsorAbbr, cs.SponsorDesc, cs.SponsorLogoName, cs.SponsorLogoFile, Convert.ToDateTime(cs.SponsorStartDate), Convert.ToDateTime(cs.SponsorEndDate), cs.SponsorAmt, cs.ActiveFlagId, cs.ShowFlagId, cs.PortalID, cs.CreatedById, cs.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertSponsorSports(clsSponsor cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertSponsorSports", cs.SponsorId, cs.SportsId, cs.EventId, cs.SeasonId, cs.CompetitionId, cs.ClubId, cs.ClubOwnersId, cs.ClubMemberId, cs.TeamId, cs.TeamMemberId, cs.PlayerId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateSponsor(clsSponsor cs)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateSponsor", cs.SponsorId,cs.SponsorLevelId,cs.SponsorTypeId,cs.SponsorName,cs.SponsorAbbr,cs.SponsorDesc,cs.SponsorLogoName,cs.SponsorLogoFile,Convert.ToDateTime(cs.SponsorStartDate),Convert.ToDateTime(cs.SponsorEndDate),cs.SponsorAmt,cs.ActiveFlagId,cs.ShowFlagId,cs.PortalID,cs.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdateSponsorSport(clsSponsor cs)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateSponsorSport", cs.SponsorId,cs.SportsId,cs.EventId,cs.SeasonId,cs.CompetitionId,cs.ClubId,cs.ClubOwnersId,cs.ClubMemberId,cs.TeamId,cs.TeamMemberId,cs.PlayerId);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int DeleteSponsor(int sponsorID)
        {
            try
            {

                dataProvider.ExecuteNonQuery("[usp_DeleteSponsor]", sponsorID);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }


        #endregion Insert,Update,Delete Methods

        public DataTable GetSponsorDataBySponsorID(int SponsorID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSponsorDataBySponsorID", SponsorID))
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

        public DataTable GetLatestSponsorID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestSponsorID"))
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

        public DataTable GetSponsorLogoBySponsorID(clsSponsor cs)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSponsorLogoBySponsorID", cs.SponsorId))
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

        public DataTable GetSponsorLevelIDAndSponsorLevelName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSponsorLevelIDAndSponsorLevelName"))
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

        public DataTable GetSponsorTypeIDAndSponsorTypeName()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSponsorTypeIDAndSponsorTypeName"))
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

        public DataTable IsSponsorHasOtherData(int sponsorID)
        {
            
            DataTable dt = new DataTable();
            try
            {

                using (IDataReader reader = dataProvider.ExecuteReader("[usp_IsSponsorHasOtherData]", sponsorID, ""))
                {
                    dt.Load(reader);
                    return dt;
                }

                //returnvalue = dataProvider.ExecuteReader("[usp_IsCompetitionHasOtherData]", cmp.CompetitionId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            //return 0;
            return dt;
        }

        public DataTable GetSponsorListForUserSide()

		{
		using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSponsorListForUserSide"))
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
        #region Get Competition Sponsor Details

       

        public DataTable GetCompetitionSponsorByCompetitionID(int competitionID)

        {
            using (DataTable dt = new DataTable())
            {
                try
                {

               

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionSponsorByCompetitionID", competitionID))

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


        #endregion



    }
}

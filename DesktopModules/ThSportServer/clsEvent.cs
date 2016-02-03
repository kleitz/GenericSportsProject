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
    public class clsEvent
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDetail { get; set; }
        public string EventStartDateTime { get; set; }
        public string EventEndDateTime { get ; set; }
        public int EventActive { get; set; }
        public string EventPriority { get; set; }
        public int PortalID { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }

        public int SportsId { get; set;}
        public int SeasonId { get; set;}
        public int CompetitionId { get; set;}
        public int ClubId { get; set;}
        public int ClubMemberId { get; set;}
        public int ClubOwnersId { get; set;}
        public int TeamId { get; set;}
        public int TeamMemberId { get; set;}
        public int SponsorId { get; set; }
    }

    public class clsEventController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsEventController()
        {

        }

        #region Getdata Methods

        public DataTable GetDataEvent()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataEvent"))
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

        public int InsertEvent(clsEvent cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertEvent", cs.EventName, cs.EventDetail, Convert.ToDateTime(cs.EventStartDateTime), Convert.ToDateTime(cs.EventEndDateTime), cs.EventActive, cs.EventPriority, cs.PortalID, cs.CreatedById, cs.ModifiedById);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertEventSports(clsEvent cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertEventSports", cs.EventID, cs.SportsId, cs.SeasonId, cs.CompetitionId, cs.TeamId, cs.TeamMemberId, cs.ClubId, cs.ClubMemberId, cs.ClubOwnersId, cs.SponsorId);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateEvent(clsEvent cs)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateEvent", cs.EventID,cs.EventName,cs.EventDetail,Convert.ToDateTime(cs.EventStartDateTime),Convert.ToDateTime(cs.EventEndDateTime),cs.EventActive,cs.EventPriority,cs.PortalID,cs.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdateEventSport(clsEvent cs)
        {
            int i = 0;
            try
            {
                //CompReg.CreatedBy,
                dataProvider.ExecuteNonQuery("usp_UpdateEventSport", cs.EventID, cs.SportsId,cs.SeasonId,cs.CompetitionId,cs.TeamId,cs.TeamMemberId,cs.ClubId,cs.ClubMemberId,cs.ClubOwnersId,cs.SponsorId);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        

        #endregion Insert,Update,Delete Methods

        public DataTable GetEventDataByEventID(int EventID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetEventDataByEventID", EventID))
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

        public DataTable GetLatestEventID()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLatestEventID"))
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

        
        

    }
}

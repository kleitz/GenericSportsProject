using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using System.Data;
using DotNetNuke.Services.Exceptions;

namespace SportSiteServer
{
    [Serializable]
    public class TeamsClass
    {
        public int TeamID { get; set; }
        public int CompRegID { get; set; }
        public int CompetitionGroupID { get; set; }
        public string TeamName { get; set; }
        public int ClubID { get; set; }
        public string TeamHistory { get; set; }
        public string TeamLogo { get; set; }
        public string TeamPhoto { get; set; }
        public string GoogleID { get; set; }
        public string FaceBookID { get; set; }
        public string TwitterID { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyBy { get; set; }
        public int IsNationalTeam { get; set; }
        public int IsDeleted { get; set; }
        public int TeamMasterID { get; set; }
    }

    public class TeamsController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public TeamsController()
        {

        }

        #region Getdata Methods
        
        public DataTable GetLastestGameForTeamMaster(int team_master_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetLastestGameForTeamMaster", team_master_id))
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
        
        public DataTable GetSelectedCompetitionGameWithTeam(int comp_id, int team_master_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSelectedCompetitionGameWithTeam", comp_id, team_master_id))
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


        public DataTable FetchAllTeams_New(string current_user)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllTeams_New", current_user))
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

        public DataTable FetchAllTeams(string current_user)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllTeams", current_user))
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



        public DataTable FetchAllNationalTeams()
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllNationalTeams"))
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

        public DataTable GetTeamListByCompetitionType(string current_user, string competition_type)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamListByCompetitionType", current_user, competition_type))
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



        public DataTable GetAllTeamsWithCompetitionType(string current_user, string competition_type)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllTeamsWithCompetitionType", current_user, competition_type))
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
        public DataTable GetAllLeagueCompetitionList()
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("GetAllLeagueCompetitionList"))
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


        public DataTable GetAllCupCompetitionList()
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("GetAllCupCompetitionList"))
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
        public DataTable GetAllLeagueCompetitionList(int SportStageValue)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllLeagueCompetitionList", SportStageValue))
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

        public DataTable GetAllLeagueCompetitionListForTopScoreAssistDisCards(int SportStageValue, int SeasonID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllLeagueCompetitionListForTopScoreAssistDisCards", SportStageValue, SeasonID))
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

        public DataTable GetAllCupCompetitionList(int SportStageValue)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCupCompetitionList", SportStageValue))
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

        public DataTable GetAllCupCompetitionListForTopScoreAssistDisCards(int SportStageValue, int SeasonID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCupCompetitionListForTopScoreAssistDisCards", SportStageValue, SeasonID))
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

        


        public DataTable GetDateAllCompIdName(string current_user)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("GetAllCompetitionData", current_user))
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

        public DataTable GetTeamListForClubAdmin()
        {
            DataTable dt = new DataTable();
            try
            {
                using (IDataReader rdr = dataProvider.ExecuteReader("usp_GetTeamListForClubAdmin"))
                {
                    dt.Load(rdr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetGroupNameID(int id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetGroupNameIDBySelectCompetitionIdD", id))
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

        public DataTable GetTeamByID(int id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamByID", id))
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

        public DataTable GetNationalTeamByID(int id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNationalTeamByID", id))
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

        public DataTable GetTeamHomePageAllDetail(int TeamMasterID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamHomePageAllDetail", TeamMasterID))
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

        public DataTable GetTeamHomePageLatestGame(int TeamMasterID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamHomePageLatestGame", TeamMasterID))
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

        public DataTable GetSelectedCompetitionTeamGameApperance(int CompetitionID, int TeamMasterID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetSelectedCompetitionTeamGameApperance", CompetitionID, TeamMasterID))
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

        public DataTable GetAllCompetitionListByTeamMasterID(int team_master_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCompetitionListByTeamMasterID", team_master_id))
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

        public DataTable GetAllCompetitionListByTeamMasterIDUpCommingCompetition(int team_master_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCompetitionListByTeamMasterIDUpCommingCompetition", team_master_id))
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

        public DataTable GetTeamIDFromTeamMasterID(int team_master_id, int comp_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamIDFromTeamMasterID", team_master_id, comp_id))
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

        public DataTable TeamMatchDetailSingleMatchWise(int comp_id, int team_master_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_TeamMatchDetailSingleMatchWise", comp_id, team_master_id))
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

        public string GetSingleMatchTotalPlayers(int comp_id, int team_masterid, int match_id)
        {
            string total_players = "0";

            using (DataTable returnTable = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_SingleMatchTotalPlayers", comp_id, team_masterid, match_id))
                    {
                        returnTable.Load(reader);
                        if (returnTable != null && returnTable.Rows.Count > 0)
                        {
                            total_players = returnTable.Rows[0].ItemArray.GetValue(0).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                    total_players = "0";
                }
                return total_players;
            }
        }

        public DataTable GetCompetitionByTeamID(int team_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetCompetitionByTeamID", team_id))
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

        public DataTable GetTeamMasterID(int TeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamMasterID", TeamID))
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

        public DataTable GetAllPlayersByTeam(int teamId, int flag, int SportsValue)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllPlayersByTeam", teamId, flag, SportsValue))
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

        public DataTable GetAllPlayersWithPosition(int teamId, int SportsValue)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllPlayersWithPosition", teamId, SportsValue))
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

        public DataTable GetAllCoachByTeam(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCoachByTeam", teamId))
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

        public DataTable GetTeamDetailsSelectedByTeamId(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamDetailsSelectedByTeamId", teamId))
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

        public int InsertTeam(TeamsClass team)
        {
            try
            {
                return Convert.ToInt32(dataProvider.ExecuteScalar("usp_InsertTeam", team.TeamName, team.ClubID, team.CompRegID, team.CompetitionGroupID, team.TeamHistory, team.TeamLogo, team.TeamPhoto, team.GoogleID, team.FaceBookID, team.TwitterID, team.CreatedBy, team.ModifyBy, team.IsNationalTeam, team.TeamMasterID));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }


        public int UpdateTeam(TeamsClass team)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateTeam", team.TeamID, team.TeamName, 1, team.CompRegID, team.CompetitionGroupID, team.TeamHistory, team.TeamLogo, team.TeamPhoto, team.GoogleID, team.FaceBookID, team.TwitterID, team.CreatedBy, team.ModifyBy);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateTeam_NameForBoth(string name, int id)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateTeam_NameForBoth", name, id);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        public int UpdateMasterTeam(TeamsClass team)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateMasterTeam", team.TeamID, team.TeamName, 1, team.TeamHistory, team.TeamLogo, team.TeamPhoto, team.GoogleID, team.FaceBookID, team.TwitterID, team.ModifyBy);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteTeam(int teamid)
        {

            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteTeam", teamid);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int DeleteMasterTeam(int teamid)
        {

            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteMasterTeam", teamid);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }
        

        public int DeleteEntryInCompetitionTeam(int teamid)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteEntryInCompetitionTeam", teamid);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        #endregion Insert,Update,Delete Methods
        
        public DataTable GetTeamSponsorByTeamID(int teamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamSponsorByTeamID", teamID))
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

        public DataTable GetNationalTeamSponsorByTeamID(int teamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNationalTeamSponsorByTeamID", teamID))
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

        public DataTable GetTeamPlayCompetitionYearWise(int MasterTeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamPlayCompetitionYearWise", MasterTeamID))
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

        public DataTable GetTeamPlayCompetitionYearWiseNotTop(int MasterTeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamPlayCompetitionYearWiseNotTop", MasterTeamID))
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

        public DataTable GetTeamPlayCompetitionYearWiseSession(int MasterTeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamPlayCompetitionYearWiseSession", MasterTeamID))
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

        

        public DataTable GetTeamRosterKeyValue(int KeyParentsID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamRosterKeyValue", KeyParentsID))
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

        public DataTable GetTeamVideoByTeamID(int teamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamVideoByTeamID", teamID))
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

        public DataTable GetTeamOtherVideoByTeamID(int teamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamOtherVideoByTeamID", teamID))
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

        

        public DataTable GetTeamVideoAndOtherVideoPathByTeamID(int teamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamVideoAndOtherVideoPathByTeamID", teamID))
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

        public DataTable GetNationalTeamVideoByTeamID(int teamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNationalTeamVideoByTeamID", teamID))
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

        public DataTable GetNationalTeamOtherVideoByTeamID(int teamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNationalTeamOtherVideoByTeamID", teamID))
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

        

        public DataTable GetTeamPhotoByTeamID(int teamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamPhotoByTeamID", teamID))
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

        public DataTable GetNationalTeamPhotoByTeamID(int teamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNationalTeamPhotoByTeamID", teamID))
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

        public DataTable GetTeamScheduleResultsByTeamID(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamScheduleResultsByTeamID", teamId))
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

        public DataTable GetTeamScheduleByTeamID(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamScheduleByTeamID", teamId))
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

        public DataTable GetTeamScheduleAndResultsByCompetitionID(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamScheduleAndResultsByCompetitionID", teamId))
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

        public DataTable GetTeamFixturesByCompetitionID(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamFixturesByCompetitionID", teamId))
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

        public DataTable GetTeamResultsByCompetitionID(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamResultsByCompetitionID", teamId))
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

        public DataTable GetNationalTeamScheduleAndResults(int teamId)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {

                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNationalTeamScheduleAndResults", teamId))
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


        public DataTable GetTeamLogo(TeamsClass Team)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamLogo", Team.TeamID))
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

        public DataTable GetMasterTeamLogo(TeamsClass Team)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetMasterTeamLogo", Team.TeamMasterID))
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

        public DataTable teamPhotoFile(TeamsClass Team)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_teamPhotoFile", Team.TeamID))
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

        public DataTable MasterTeamPhotoFile(TeamsClass Team)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_MasterTeamPhotoFile", Team.TeamID))
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

        public DataTable GetManagementDetailTeamID(int TeamID, int KeyValue)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetManagementDetailTeamID", TeamID, KeyValue))
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

        public DataTable usp_GetAllCoachByTeamIDAndFutsalValue(int teamId, int FutsalValue)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllCoachByTeamIDAndFutsalValue", teamId, FutsalValue))
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

        public DataTable GetManagementKeyDetailTeamID(int TeamID)
        {

            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetManagementKeyDetailTeamID", TeamID))
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



        public DataTable GetManagementDataDetailTeamID(int TeamID, int KeyValue)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetManagementDataDetailTeamID", TeamID, KeyValue))
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

        public DataTable GetManagementDataDetailNationalTeamID(int TeamID, int KeyValue)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetManagementDataDetailNationalTeamID", TeamID, KeyValue))
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

        public DataTable GetTeamPlayerDataByTeamIDAndPlayerPositionName(int TeamID, string KeyParentsName)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamPlayerDataByTeamIDAndPlayerPositionName", TeamID, KeyParentsName))
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

        public DataTable GetTeamPlayerDataByTeamIDAndPlayerPositionNameTop(int TeamID, string KeyParentsName, string LocalSeasonID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamPlayerDataByTeamIDAndPlayerPositionNameTop", TeamID, KeyParentsName, LocalSeasonID))
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
        
        public DataTable GetNationalTeamPlayerDataByTeamIDAndPlayerPositionName(int TeamID, string KeyParentsName)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetNationalTeamPlayerDataByTeamIDAndPlayerPositionName", TeamID, KeyParentsName))
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

        public DataTable GetAllTeamNameByCompetitionAndCompetitionGroupID(int CompetitionID, int CompetitionGroupID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllTeamNameByCompetitionAndCompetitionGroupID", CompetitionID, CompetitionGroupID))
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

        public DataTable GetAllTeamsByCompetitionGroupID(string current_user, int CompetitionID, int CompetitionGroupID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllTeamsByCompetitionGroupID", current_user, CompetitionID, CompetitionGroupID))
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

        public DataTable GetAllTeamsByTeamName(string current_user, int TeamID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader(" usp_GetAllTeamsByTeamName", current_user, TeamID))
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

        public DataTable GetTestSelectedWinningTeam()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTestSelectedWinningTeam"))
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

        public DataTable GetAllWinneingTeam(int SportStageValue)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAllWinneingTeam", SportStageValue))
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

        public DataTable GetTeamAwardsList(int portalId, int team_id)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetTeamAwardsList", portalId, team_id))
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

        public DataTable CheckConditionThisTeamInCompetitionOrNot(int TeamMasterID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_CheckConditionThisTeamInCompetitionOrNot", TeamMasterID))
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

        public DataTable GetPlayerPhotoSeasonWiseOtherWiseProfile(int PlayerID, int SeasonID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetPlayerPhotoSeasonWiseOtherWiseProfile", PlayerID, SeasonID))
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

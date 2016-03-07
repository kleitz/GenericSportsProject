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
    [Serializable]
    public class clsAlbumPicture
    {
        public string PictureFile { get; set; }
        public int Id { get; set; }
        public int AlBumLinkId { get; set; }
        public int IsDeleted { get; set; }
    }

    [Serializable]
    public class clsAlbumVideo
    {
        public string VideoFile { get; set; }
        public string VideoURL { get; set; }
        public int Id { get; set; }
        public int AlBumLinkId { get; set; }
        public int IsDeleted { get; set; }
    }

    public class clsAlbum
    {
        public int AlbamId { get; set; }
        public string AlbamAbbr{ get; set; } 
        public string AlbumName { get; set;} 
        public string AlbumDesc { get; set;}
        public string AlbamDate{ get; set; }
        public int AlbamType { get; set; }
      //  public string PictureFile { get; set;} 
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

    public class clsAlbumLink
    {
        public int AlbamLinkId { get; set; }
        public int AlbamId { get; set; }
        public string PictureFile { get; set; }
        public int VideoType { get; set; }
        public string VideoYouTubeFile { get; set; }
        public string VideoOtherFile { get; set; }
    
    }

    public class clsAlbumController
    {
        private readonly DataProvider dataProvider = DataProvider.Instance();
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        public clsAlbumController()
        {
             
        }

        #region Getdata Methods

        public DataTable GetDataAlbum()
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataAlbum"))
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

        public DataTable GetDataAlbumByAlbumID(int albumID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetDataAlbumByAlbumID",albumID))
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

        public DataTable GetAlbumLinkDataByAlbumId(int albumID)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    using (IDataReader reader = dataProvider.ExecuteReader("usp_GetAlbumLinkDataByAlbumId", albumID))
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

        public int InsertAlbum(clsAlbum cs)
        {
            try
            {
              return  Convert.ToInt32(dataProvider.ExecuteScalar("usp_InsertAlbum", cs.SportsId, cs.CountryId, cs.SeasonId, cs.CompetitionId, cs.ClubId, cs.ClubOwnersId, cs.ClubMemberId, cs.TeamId, cs.TeamMemberId, cs.SponsorId, cs.EventId, cs.PlayerId, cs.AlbumName, cs.AlbumDesc, cs.AlbamAbbr, Convert.ToDateTime(cs.AlbamDate),cs.AlbamType, cs.ActiveFlagId,cs.ShowFlagId, cs.PortalID, cs.CreatedById));
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int InsertAlbumLinks(clsAlbumLink cs)
        {
            try
            {
                dataProvider.ExecuteNonQuery("usp_InsertAlbumLinks", cs.AlbamId, cs.PictureFile, cs.VideoType, cs.VideoYouTubeFile, cs.VideoOtherFile);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return 0;
        }

        public int UpdateAlbum(clsAlbum cs)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_UpdateAlbum",cs.AlbamId, cs.SportsId, cs.CountryId, cs.SeasonId, cs.CompetitionId, cs.ClubId, cs.ClubOwnersId, cs.ClubMemberId, cs.TeamId, cs.TeamMemberId, cs.SponsorId, cs.EventId, cs.PlayerId, cs.AlbumName, cs.AlbumDesc, cs.AlbamAbbr, Convert.ToDateTime(cs.AlbamDate),cs.AlbamType, cs.ActiveFlagId,cs.ShowFlagId, cs.PortalID, cs.ModifiedById);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

      

        public int DeleteAlbumLinksByAlbumLinkId(int AlbumLinkId)
        {
            int i = 0;
            try
            {
                dataProvider.ExecuteNonQuery("usp_DeleteAlbumLinksByAlbumLinkId", AlbumLinkId);
            }
            catch (Exception ex)
            {
                return i;
            }
            return i;
        }

        #endregion Insert,Update,Delete Methods

        
    }
}

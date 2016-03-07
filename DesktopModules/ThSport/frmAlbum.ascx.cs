using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Modules;
using ThSportServer;
using DotNetNuke.Entities.Users;
using System.Data;
using System.IO;
//using System.Collections.

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmAlbum :PortalModuleBase
    {
         public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\Pictures\\";
        public string imhpathDB = "Images\\Pictures\\";

        public string VideoUploadFolder = "DesktopModules\\ThSport\\Videos\\Video\\";
        public string videohpathDB = "Videos\\Video\\";

        clsAlbum objclsAblum = new clsAlbum();
        clsAlbumLink objclsAblumLink = new clsAlbumLink();
        clsAlbumController objclsAlbumController = new clsAlbumController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        List<clsAlbumPicture> _lstPicture = new List<clsAlbumPicture>();
        List<clsAlbumVideo> _lstVideo = new List<clsAlbumVideo>();



        List<clsAlbumPicture> lstPicture
        {
            get
            {
                if (ViewState["lstPicture"] == null)
                {
                    return new List<clsAlbumPicture>();
                   
                }
                return (List<clsAlbumPicture>)ViewState["lstPicture"];
            }
            set { ViewState["lstPicture"] = value; }
        }

        List<clsAlbumVideo> lstVideo
        {
            get
            {
                if (ViewState["lstVideo"] == null)
                {
                    return new List<clsAlbumVideo>();

                }
                return (List<clsAlbumVideo>)ViewState["lstVideo"];
            }
            set { ViewState["lstVideo"] = value; }
        }

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {

            //PnlGridAlbum.Visible = false;
            //pnlEntryVideo.Visible = true; pnlPicture.Visible = true; pnlVideo.Visible = false; divYouTubeVideopath.Visible = false;
            if (!IsPostBack)
            {
                FillGridView();
            }
           
        }
        #endregion

        #region Gridview action
        protected void grdAlbumList_OnRowDataBound(Object sender, GridViewRowEventArgs e)
        {


        }

        protected void grdAlbumList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAlbumList.PageIndex = e.NewPageIndex;
           // FillBannerList();
        }

        protected void grdAlbumList_OnRowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("DeletePicture"))
            {
                int banner_id = 0;
                int.TryParse(e.CommandArgument.ToString(), out banner_id);

                (from p in lstPicture where p.Id == banner_id select p).ToList().ForEach(
                                                                                           x =>
                                                                                           x.IsDeleted =
                                                                                           1);


                //lstPicture = new List<clsAlbumPicture>(lstPicture.Where(x => x.Id != banner_id).ToList());
                ////lstPicture.RemoveAt(banner_id);
                grdAlbumList.DataSource = lstPicture.Where(x=>x.IsDeleted==0).ToList();
                grdAlbumList.DataBind();
            }
        }

        protected void grdVideoList_OnRowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("DeleteVideo"))
            {
                int banner_id = 0;
                int.TryParse(e.CommandArgument.ToString(), out banner_id);
                   
                    (from p in lstVideo where p.Id == banner_id select p).ToList().ForEach(
                                                                                           x =>
                                                                                           x.IsDeleted =
                                                                                           1);
            
                    grdVideoList.DataSource = lstVideo.Where(x=>x.IsDeleted==0).ToList();
                    grdVideoList.DataBind();;
                
            }
        }

        protected void gvAlbum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAlbumList.PageIndex = e.NewPageIndex;
            // FillBannerList();
        }


        #endregion

        #region DropDown SelectedIndexChanged

        protected void ddlAlbumtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAlbumtype.SelectedValue == "Video")
            {
                pnlVideo.Visible = true;
                pnlPicture.Visible = false;
                divYouTubeVideopath.Visible = true;
            }
            else
            {
                pnlVideo.Visible = false;
                pnlPicture.Visible = true;

            }
       
          
        }
        protected void ddlVideotype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVideotype.SelectedValue == "YouTube")
            {
                divYouTubeVideopath.Visible = true;
                divOtherVideoPath.Visible = false;
            }
            else
            {
                divYouTubeVideopath.Visible = false;
                divOtherVideoPath.Visible = true;
            }
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionAlbumID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                FillSport();
                FillCountry();
                FillEvent();
                FillSeason();
                FillCompetition();
                FillClub();
                FillClubOwner();
                FillClubMember();
                FillTeam();
                FillTeamMember();
                FillPlayer();
                FillSponsor();

                int AlbumID = 0;
                int.TryParse(str, out AlbumID);

                LinkButton btn = sender as LinkButton;

                //cls cs = new clsVideos();
                //clsVideosController csc = new clsVideosController();

                DataTable dt = new DataTable();

                dt = objclsAlbumController.GetDataAlbumByAlbumID(AlbumID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["AlbumID"].ToString();

                    ddlSports.SelectedValue = dt.Rows[0]["SportId"].ToString();
                    ddlSeason.SelectedValue = dt.Rows[0]["SeasonId"].ToString();
                    ddlCompetition.SelectedValue = dt.Rows[0]["CompetitionId"].ToString();
                    ddlClub.SelectedValue = dt.Rows[0]["ClubId"].ToString();
                    ddlClubOwner.SelectedValue = dt.Rows[0]["ClubOwnersId"].ToString();
                    ddlClubMember.SelectedValue = dt.Rows[0]["ClubMemberId"].ToString();
                    ddlTeam.SelectedValue = dt.Rows[0]["TeamId"].ToString();
                    ddlTeamMember.SelectedValue = dt.Rows[0]["TeamMemberId"].ToString();
                    ddlPlayer.SelectedValue = dt.Rows[0]["PlayerId"].ToString();
                    ddlEvent.SelectedValue = dt.Rows[0]["EventId"].ToString();
                    ddlSponsor.SelectedValue = dt.Rows[0]["SponsorId"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["CountryID"].ToString();

                    //ddlVideoPriority.SelectedValue = dt.Rows[0]["VideoLevelId"].ToString();

                    txtAlbumTitle.Text = dt.Rows[0]["AlbumName"].ToString();
                    txtAlbumDesc.Text = dt.Rows[0]["AlbumDesc"].ToString();
                    txtAlbumDate.Text = dt.Rows[0]["AlbumDate"].ToString();

                      DataTable dt1 = new DataTable();

                     dt1 = objclsAlbumController.GetAlbumLinkDataByAlbumId(AlbumID);
                     ddlAlbumtype.Enabled = false;
                    if (dt.Rows[0]["AlbumType"].ToString() == "1")
                    {
                        ddlAlbumtype.SelectedValue = "Video";

                        pnlVideo.Visible=true;
                        if (dt1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                if (dt1.Rows[i]["VideoType"].ToString() == "1")
                                {
                                    ddlVideotype.SelectedValue = "YouTube";
                                    //divYouTubeVideopath.Visible = true;
                                    //for (int i = 0; i < dt1.Rows.Count; i++)
                                    //{
                                        clsAlbumVideo objClsvideo = new clsAlbumVideo();

                                        objClsvideo.Id = i;
                                        objClsvideo.VideoFile = "http://www.youtube.com/watch?v=" + dt1.Rows[i]["VideoYouTubeFile"].ToString();
                                        objClsvideo.VideoURL = dt1.Rows[i]["VideoYouTubeFile"].ToString();
                                        objClsvideo.AlBumLinkId = Convert.ToInt32(dt1.Rows[i]["AlbumLinksId"].ToString());
                                        _lstVideo = lstVideo;
                                        _lstVideo.Add(objClsvideo);
                                        lstVideo = _lstVideo;

                                  //  }
                                }
                                else if (dt1.Rows[i]["VideoType"].ToString() == "2")
                                {
                                    ddlVideotype.SelectedValue = "Other";
                                    //divOtherVideoPath.Visible = true;
                            
                                        clsAlbumVideo objClsvideo = new clsAlbumVideo();
                                        char[] delimiterChars = { '\\' };
                                        string[] fileName = (dt1.Rows[i]["VideoOtherFile"].ToString()).Split(delimiterChars);
                                        if (fileName.Length >= 3)
                                        {
                                            objClsvideo.VideoFile = fileName[2].ToString();

                                        }
                                        else
                                        {
                                            objClsvideo.VideoFile = "";
                                        }
                                        objClsvideo.Id = i;
                                        objClsvideo.VideoURL = dt1.Rows[i]["VideoOtherFile"].ToString();
                                        objClsvideo.AlBumLinkId = Convert.ToInt32(dt1.Rows[i]["AlbumLinksId"].ToString());
                                        _lstVideo = lstVideo;
                                        _lstVideo.Add(objClsvideo);
                                        lstVideo = _lstVideo;

                                    }
                                }
                         
                        }
                        else
                        {
                            divYouTubeVideopath.Visible = true;
                        }
                        if (ddlVideotype.SelectedValue == "YouTube")
                        {
                            divYouTubeVideopath.Visible = true;

                            divOtherVideoPath.Visible = false;
                        }
                        else
                        {
                            divOtherVideoPath.Visible = true;
                            divYouTubeVideopath.Visible = false;
                        }
                       
                        grdVideoList.DataSource = lstVideo;
                        grdVideoList.DataBind();
                    }
                    else
                    {
                        ddlAlbumtype.SelectedValue = "Picture";
                        pnlPicture.Visible = true;
                        pnlVideo.Visible = false;
                        if (dt1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                clsAlbumPicture objClsPicture = new clsAlbumPicture();

                                objClsPicture.Id = i;
                                objClsPicture.PictureFile = dt1.Rows[i]["PictureFile"].ToString();
                                objClsPicture.AlBumLinkId = Convert.ToInt32(dt1.Rows[i]["AlbumLinksId"].ToString());
                                _lstPicture = lstPicture;
                                _lstPicture.Add(objClsPicture);
                                lstPicture = _lstPicture;
                                
                            }
                            grdAlbumList.DataSource = lstPicture;
                            grdAlbumList.DataBind();
                        }
                    }

                  

                    if (dt.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    else
                    {
                        ChkIsActive.Checked = false;
                    }

                    if (dt.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                    else
                    {
                        ChkIsShow.Checked = false;
                    }

                    pnlEntryVideo.Visible = true;
                    PnlGridAlbum.Visible = false;
                    btnUpdateAlbum.Visible = true;
                   btnSaveAlbum.Visible = false;
                }
            }
            
        }
        #endregion

        #region Button Click Event

        protected void btnAddPicture_Click(object sender, EventArgs e)
        {
            SaveImage();
            grdAlbumList.DataSource = lstPicture;
            grdAlbumList.DataBind();

        }

        protected void btnAddAlbum_Click(object sender, EventArgs e)
        {
            funClearData();
            pnlEntryVideo.Visible = true;
            PnlGridAlbum.Visible = false;
            btnSaveAlbum.Visible = true;
            btnUpdateAlbum.Visible = false;
            FillSport();
            FillCountry();
            FillEvent();
            FillSeason();
            FillSponsor();
            FillEvent();
            FillTeam();
            FillClub();
            FillCompetition();
            FillClubOwner();
            FillClubMember();
            FillTeamMember();
            FillPlayer();
            pnlVideo.Visible = true;
            ddlVideotype.SelectedValue = "YouTube";
            divYouTubeVideopath.Visible = true;
            divOtherVideoPath.Visible = false;
        }

        protected void btnAddVideo_Click(object sender, EventArgs e)
        {
            SaveVideo();
            grdVideoList.DataSource = lstVideo.Where(x => x.IsDeleted == 0).ToList(); ;
            grdVideoList.DataBind();
            txtVideoPath.Text = "";
        }

        protected void btnSaveAlbum_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            if (ddlSports.SelectedValue == "")
            {
                objclsAblum.SportsId = 0;
            }
            else
            {
                objclsAblum.SportsId = Convert.ToInt32(ddlSports.SelectedValue);
            }

            if (ddlCountry.SelectedValue == "")
            {
                objclsAblum.CountryId = 0;
            }
            else
            {
                objclsAblum.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            }

            if (ddlEvent.SelectedValue == "")
            {
                objclsAblum.EventId = 0;
            }
            else
            {
                objclsAblum.EventId = Convert.ToInt32(ddlEvent.SelectedValue);
            }

            if (ddlSeason.SelectedValue == "")
            {
                objclsAblum.SeasonId = 0;
            }
            else
            {
                objclsAblum.SeasonId = Convert.ToInt32(ddlSeason.SelectedValue);
            }

            if (ddlCompetition.SelectedValue == "")
            {
                objclsAblum.CompetitionId = 0;
            }
            else
            {
                objclsAblum.CompetitionId = Convert.ToInt32(ddlCompetition.SelectedValue);
            }

            if (ddlClub.SelectedValue == "")
            {
                objclsAblum.ClubId = 0;
            }
            else
            {
                objclsAblum.ClubId = Convert.ToInt32(ddlClub.SelectedValue);
            }

            if (ddlClubOwner.SelectedValue == "")
            {
                objclsAblum.ClubOwnersId = 0;
            }
            else
            {
                objclsAblum.ClubOwnersId = Convert.ToInt32(ddlClubOwner.SelectedValue);
            }

            if (ddlClubMember.SelectedValue == "")
            {
                objclsAblum.ClubMemberId = 0;
            }
            else
            {
                objclsAblum.ClubMemberId = Convert.ToInt32(ddlClubMember.SelectedValue);
            }

            if (ddlTeam.SelectedValue == "")
            {
                objclsAblum.TeamId = 0;
            }
            else
            {
                objclsAblum.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
            }

            if (ddlTeamMember.SelectedValue == "")
            {
                objclsAblum.TeamMemberId = 0;
            }
            else
            {
                objclsAblum.TeamMemberId = Convert.ToInt32(ddlTeamMember.SelectedValue);
            }

            if (ddlPlayer.SelectedValue == "")
            {
                objclsAblum.PlayerId = 0;
            }
            else
            {
                objclsAblum.PlayerId = Convert.ToInt32(ddlPlayer.SelectedValue);
            }

            if (ddlSponsor.SelectedValue == "")
            {
                objclsAblum.SponsorId = 0;
            }
            else
            {
                objclsAblum.SponsorId = Convert.ToInt32(ddlSponsor.SelectedValue);
            }

            objclsAblum.AlbumName = txtAlbumTitle.Text;
            objclsAblum.AlbumDesc = txtAlbumDesc.Text;
            objclsAblum.AlbamDate = txtAlbumDate.Text;

            // string youtubelink = "http://www.youtube.com/embed/";
          //  string videopath = txtVideoPath.Text;
            //youtubelink +


            if (ddlAlbumtype.SelectedValue == "Video")
            {
                objclsAblum.AlbamType = 1;
            }
            else
            {
                objclsAblum.AlbamType = 2;
            }

            if (ChkIsActive.Checked == true)
            {
                objclsAblum.ActiveFlagId = 1;
            }
            else
            {
                objclsAblum.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                objclsAblum.ShowFlagId = 1;
            }
            else
            {
                objclsAblum.ShowFlagId = 0;
            }

          //  cs.VideoLevelId = ddlVideoPriority.SelectedValue;
           
            objclsAblum.PortalID = PortalId;
            objclsAblum.CreatedById = currentUser.Username;
            objclsAblum.ModifiedById = currentUser.Username;

            int spid = objclsAlbumController.InsertAlbum(objclsAblum);
            objclsAblumLink.AlbamId = spid;
            if (ddlAlbumtype.SelectedValue == "Video")
            {
               
                objclsAblum.AlbamType = 1;
                if (ddlVideotype.SelectedValue == "YouTube")
                {
                    objclsAblumLink.VideoType = 1;
                }
                else
                {
                    objclsAblumLink.VideoType = 2;
                }
                _lstVideo = new List<clsAlbumVideo>(lstVideo.Where(x => x.IsDeleted != 1));
                for (int i = 0; i < _lstVideo.Count; i++)
                {
                    if (objclsAblumLink.VideoType == 1)
                    {
                        objclsAblumLink.VideoYouTubeFile = _lstVideo[i].VideoURL;
                    }
                    else
                    {
                        objclsAblumLink.VideoOtherFile = _lstVideo[i].VideoURL;
                    }
                    objclsAlbumController.InsertAlbumLinks(objclsAblumLink);
                }
            }
            else
            {
                 objclsAblum.AlbamType= 2;
                 _lstPicture = new List<clsAlbumPicture>(lstPicture.Where(x => x.IsDeleted != 1));
                 for (int i = 0; i < _lstPicture.Count; i++)
                {
                    objclsAblumLink.PictureFile = _lstPicture[i].PictureFile;
                   
                    objclsAlbumController.InsertAlbumLinks(objclsAblumLink);
                }
            }

            pnlEntryVideo.Visible = false;
            PnlGridAlbum.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnUpdateAlbum_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully()", true);


            //Boolean FileOK = false;
            //Boolean FileSaved = false;
            objclsAblum.AlbamId = Convert.ToInt32(hidRegID.Value);

            if (ddlSports.SelectedValue == "")
            {
                objclsAblum.SportsId = 0;
            }
            else
            {
                objclsAblum.SportsId = Convert.ToInt32(ddlSports.SelectedValue);
            }

            if (ddlCountry.SelectedValue == "")
            {
                objclsAblum.CountryId = 0;
            }
            else
            {
                objclsAblum.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            }

            if (ddlEvent.SelectedValue == "")
            {
                objclsAblum.EventId = 0;
            }
            else
            {
                objclsAblum.EventId = Convert.ToInt32(ddlEvent.SelectedValue);
            }

            if (ddlSeason.SelectedValue == "")
            {
                objclsAblum.SeasonId = 0;
            }
            else
            {
                objclsAblum.SeasonId = Convert.ToInt32(ddlSeason.SelectedValue);
            }

            if (ddlCompetition.SelectedValue == "")
            {
                objclsAblum.CompetitionId = 0;
            }
            else
            {
                objclsAblum.CompetitionId = Convert.ToInt32(ddlCompetition.SelectedValue);
            }

            if (ddlClub.SelectedValue == "")
            {
                objclsAblum.ClubId = 0;
            }
            else
            {
                objclsAblum.ClubId = Convert.ToInt32(ddlClub.SelectedValue);
            }

            if (ddlClubOwner.SelectedValue == "")
            {
                objclsAblum.ClubOwnersId = 0;
            }
            else
            {
                objclsAblum.ClubOwnersId = Convert.ToInt32(ddlClubOwner.SelectedValue);
            }

            if (ddlClubMember.SelectedValue == "")
            {
                objclsAblum.ClubMemberId = 0;
            }
            else
            {
                objclsAblum.ClubMemberId = Convert.ToInt32(ddlClubMember.SelectedValue);
            }

            if (ddlTeam.SelectedValue == "")
            {
                objclsAblum.TeamId = 0;
            }
            else
            {
                objclsAblum.TeamId = Convert.ToInt32(ddlTeam.SelectedValue);
            }

            if (ddlTeamMember.SelectedValue == "")
            {
                objclsAblum.TeamMemberId = 0;
            }
            else
            {
                objclsAblum.TeamMemberId = Convert.ToInt32(ddlTeamMember.SelectedValue);
            }

            if (ddlPlayer.SelectedValue == "")
            {
                objclsAblum.PlayerId = 0;
            }
            else
            {
                objclsAblum.PlayerId = Convert.ToInt32(ddlPlayer.SelectedValue);
            }

            if (ddlSponsor.SelectedValue == "")
            {
                objclsAblum.SponsorId = 0;
            }
            else
            {
                objclsAblum.SponsorId = Convert.ToInt32(ddlSponsor.SelectedValue);
            }

            objclsAblum.AlbumName = txtAlbumTitle.Text.Trim();
            objclsAblum.AlbumDesc = txtAlbumDesc.Text.Trim();
            objclsAblum.AlbamDate = txtAlbumDate.Text.Trim();




            if (ddlAlbumtype.SelectedValue == "Video")
            {
                objclsAblum.AlbamType = 1;
            }
            else
            {
                objclsAblum.AlbamType = 2;
            }

            if (ChkIsActive.Checked == true)
            {
                objclsAblum.ActiveFlagId = 1;
            }
            else
            {
                objclsAblum.ActiveFlagId = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                objclsAblum.ShowFlagId = 1;
            }
            else
            {
                objclsAblum.ShowFlagId = 0;
            }

            //  cs.VideoLevelId = ddlVideoPriority.SelectedValue;

            objclsAblum.PortalID = PortalId;
            objclsAblum.CreatedById = currentUser.Username;
            objclsAblum.ModifiedById = currentUser.Username;

            int spid = objclsAlbumController.UpdateAlbum(objclsAblum);
            objclsAblumLink.AlbamId = Convert.ToInt32(hidRegID.Value); ;
            if (ddlAlbumtype.SelectedValue == "Video")
            {

                objclsAblum.AlbamType = 1;
                if (ddlVideotype.SelectedValue == "YouTube")
                {
                    objclsAblumLink.VideoType = 1;
                }
                else
                {
                    objclsAblumLink.VideoType = 2;
                }

                for (int i = 0; i < lstVideo.Count; i++)
                {
                    if (objclsAblumLink.VideoType == 1)
                    {
                        objclsAblumLink.VideoYouTubeFile = lstVideo[i].VideoURL;
                    }
                    else
                    {
                        objclsAblumLink.VideoOtherFile = lstVideo[i].VideoURL;
                    }

                    if (lstVideo[i].IsDeleted == 1 && lstVideo[i].AlBumLinkId != 0)
                    {
                        objclsAlbumController.DeleteAlbumLinksByAlbumLinkId(lstVideo[i].AlBumLinkId);
                    }
                    else if (lstVideo[i].IsDeleted != 1 && lstVideo[i].AlBumLinkId == 0)
                    {
                        objclsAlbumController.InsertAlbumLinks(objclsAblumLink);
                    }
                  
                }
            }
            else
            {
                objclsAblum.AlbamType = 2;
                for (int i = 0; i < lstPicture.Count; i++)
                {
                    objclsAblumLink.PictureFile = lstPicture[i].PictureFile;

                    if (lstPicture[i].IsDeleted == 1 && lstPicture[i].AlBumLinkId != 0)
                    {
                        objclsAlbumController.DeleteAlbumLinksByAlbumLinkId(lstPicture[i].AlBumLinkId);
                    }
                    else if (lstPicture[i].IsDeleted != 1 && lstPicture[i].AlBumLinkId == 0)
                    {
                        objclsAlbumController.InsertAlbumLinks(objclsAblumLink);
                    }
                  
                }
            }

            pnlEntryVideo.Visible = false;
            PnlGridAlbum.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnCloseAlbum_Click(object sender, EventArgs e)
        {
            pnlEntryVideo.Visible = false;
            PnlGridAlbum.Visible = true;
            FillGridView();
        }
        #endregion

        #region Method

        private void FillGridView()
        {
            DataTable dt = new DataTable();

            //if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            //{
            dt = objclsAlbumController.GetDataAlbum();
            //}

            if (dt.Rows.Count > 0)
            {
                gvAlbum.DataSource = dt;
                gvAlbum.DataBind();
            }
        }
        private void SaveImage()
        {
            Boolean FileOK = false;
            Boolean FileSaved = false;
            clsAlbumPicture objClsPicture = new clsAlbumPicture();

            if (!string.IsNullOrEmpty(PictureLogoFile.PostedFile.FileName))
            {
                objClsPicture.PictureFile = imhpathDB + PictureLogoFile.PostedFile.FileName.Replace(" ", "");
                _lstPicture = lstPicture;
                objClsPicture.Id = _lstPicture.Count;
                _lstPicture.Add(objClsPicture);
                lstPicture = _lstPicture;
            }

            if (PictureLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(PictureLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".png", ".jpg", ".gif", ".jpeg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(PictureLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (PictureLogoFile.PostedFile.ContentLength > 10485760)
                {
                    //dvMsg.Attributes.Add("style", "display:block;");
                    //return;
                }
                else
                {
                    //dvMsg.Attributes.Add("style", "display:none;");
                }

                try
                {
                    PictureLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + PictureLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

        }

        private void SaveVideo()
        {

            Boolean FileOKVidoe = false;
            Boolean FileSavedVideo = false;
            clsAlbumVideo objClsVideo = new clsAlbumVideo();
            if (ddlVideotype.SelectedValue == "Other" )
            {
                if (!string.IsNullOrEmpty(AlbumLogoFile.PostedFile.FileName))
                {
                    objClsVideo.VideoFile = AlbumLogoFile.PostedFile.FileName.Replace(" ", "");
                    _lstVideo = lstVideo;
                    objClsVideo.Id = _lstVideo.Count;
                    objClsVideo.VideoURL = videohpathDB + AlbumLogoFile.PostedFile.FileName.Replace(" ", "");
                    objClsVideo.AlBumLinkId = 0;
                    _lstVideo.Add(objClsVideo);
                    lstVideo = _lstVideo;
                }
            }
            else if (!string.IsNullOrEmpty(txtVideoPath.Text))
            {
                objClsVideo.VideoFile = "http://www.youtube.com/watch?v=" + txtVideoPath.Text.Trim();
                _lstVideo = lstVideo;
                objClsVideo.Id = _lstVideo.Count;
                objClsVideo.VideoURL = txtVideoPath.Text.Trim();
                objClsVideo.AlBumLinkId = 0;
                _lstVideo.Add(objClsVideo);
                lstVideo = _lstVideo;

            }

            if (AlbumLogoFile.PostedFile != null)
            {

                String FileExtension = Path.GetExtension(AlbumLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
                String[] allowedExtensions = { ".flv", ".webm", ".mkv", ".vob", ".ogv", ".ogg", ".avi", ".mov", ".wmv", ".rm", ".mp4", ".m4p", ".m4v", ".mpg", ".mp2", ".mpeg", ".mpe", ".mpv", ".m2v", ".m4v", ".svi", ".3gp", ".3g2", ".nsv", ".asf", ".asx", ".srt", ".swf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOKVidoe = true;
                        break;
                    }
                }
            }

            if (FileOKVidoe)
            {
                try
                {
                    AlbumLogoFile.PostedFile.SaveAs(physicalpath + VideoUploadFolder + AlbumLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSavedVideo = true;
                }
                catch (Exception ex)
                {
                    FileSavedVideo = false;
                }
            }

        }

        private void FillCompetition()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillComptitionIDAndCompetitionName();
            if (dt.Rows.Count > 0)
            {
                ddlCompetition.DataSource = dt;
                ddlCompetition.DataTextField = "CompetitionName";
                ddlCompetition.DataValueField = "CompetitionId";
                ddlCompetition.DataBind();
                ddlCompetition.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClub()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillClubIDAndClubName();
            if (dt.Rows.Count > 0)
            {
                ddlClub.DataSource = dt;
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubOwner()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillClubOwnerIDAndClubOwnerName();
            if (dt.Rows.Count > 0)
            {
                ddlClubOwner.DataSource = dt;
                ddlClubOwner.DataTextField = "ClubOwnerName";
                ddlClubOwner.DataValueField = "ClubOwnersId";
                ddlClubOwner.DataBind();
                ddlClubOwner.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillClubMember()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillClubMemberIDAndClubMemberName();
            if (dt.Rows.Count > 0)
            {
                ddlClubMember.DataSource = dt;
                ddlClubMember.DataTextField = "ClubMemberName";
                ddlClubMember.DataValueField = "ClubMemberId";
                ddlClubMember.DataBind();
                ddlClubMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeam()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillTeamIDAndTeamName();
            if (dt.Rows.Count > 0)
            {
                ddlTeam.DataSource = dt;
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataValueField = "TeamId";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillTeamMember()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillTeamMemberIDAndTeamMemberName();
            if (dt.Rows.Count > 0)
            {
                ddlTeamMember.DataSource = dt;
                ddlTeamMember.DataTextField = "TeamMemberName";
                ddlTeamMember.DataValueField = "TeamMemberID";
                ddlTeamMember.DataBind();
                ddlTeamMember.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillCountry()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.FillCountryIDAndCountryName();
            if (dt.Rows.Count > 0)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillSport()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();

            DataTable dt = new DataTable();

            dt = ec.GetSportIDAndSportName();
            if (dt.Rows.Count > 0)
            {
                ddlSports.DataSource = dt;
                ddlSports.DataTextField = "SportName";
                ddlSports.DataValueField = "SportID";
                ddlSports.DataBind();
                ddlSports.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillEvent()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();

            DataTable dt = new DataTable();

            dt = ec.GetEventIDAndEventName();
            if (dt.Rows.Count > 0)
            {
                ddlEvent.DataSource = dt;
                ddlEvent.DataTextField = "EventName";
                ddlEvent.DataValueField = "EventID";
                ddlEvent.DataBind();
                ddlEvent.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillSeason()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();

            DataTable dt = new DataTable();

            dt = ec.GetSeasonIDAndSeasonName();
            if (dt.Rows.Count > 0)
            {
                ddlSeason.DataSource = dt;
                ddlSeason.DataTextField = "SeasonName";
                ddlSeason.DataValueField = "SeasonID";
                ddlSeason.DataBind();
                ddlSeason.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillPlayer()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetAllPlayer();
            if (dt.Rows.Count > 0)
            {
                ddlPlayer.DataSource = dt;
                ddlPlayer.DataTextField = "PlayerName";
                ddlPlayer.DataValueField = "RegistrationId";
                ddlPlayer.DataBind();
                ddlPlayer.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void FillSponsor()
        {
            clsVideos e = new clsVideos();
            clsVideosController ec = new clsVideosController();
            DataTable dt = new DataTable();

            dt = ec.GetSponsorIDAndSponsorName();
            if (dt.Rows.Count > 0)
            {
                ddlSponsor.DataSource = dt;
                ddlSponsor.DataTextField = "SponsorName";
                ddlSponsor.DataValueField = "SponsorId";
                ddlSponsor.DataBind();
                ddlSponsor.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void funClearData()
        {
            txtAlbumTitle.Text = "";
            txtAlbumDesc.Text = "";
            txtAlbumDate.Text = "";
            //txtVideoPath.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
            divOtherVideoPath.Visible = false;
            pnlPicture.Visible = false;
            ddlAlbumtype.Enabled = true;
            lstPicture.Clear();
            lstVideo.Clear();

            grdVideoList.DataSource = lstVideo;
            grdVideoList.DataBind();

            grdAlbumList.DataSource = lstPicture;
            grdAlbumList.DataBind();

        }
        #endregion

    }

    

    //public class clsPicture
    //{
    //    int _Id {get;set;}
    //    string _imageUrl { get; set; }
    //}
}
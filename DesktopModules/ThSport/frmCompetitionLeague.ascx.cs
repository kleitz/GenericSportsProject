using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Modules;
using ThSportServer;
using DotNetNuke.Entities.Users;
using System.Data;
using System.IO;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmCompetitionLeague : PortalModuleBase
    {
        clsCompetitionLeague cl = new clsCompetitionLeague();
        clsCompetitionLeagueController clc = new clsCompetitionLeagueController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\CompetitionLeagueLogo\\";
        public string imhpathDB = "Images\\CompetitionLeagueLogo\\";
        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        int competitionleagueID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnCompetitionLeagueID.Value))
                {
                    int.TryParse(hdnCompetitionLeagueID.Value, out id);
                }
                return id;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateCompetitionLeague.Visible = false;
            btnSaveCompetitionLeague.Visible = false;
            pnlCompetitionLeagueEntry.Visible = false;

            LoadCompetitionLeagueGrid();

        }

        #endregion Page Events

        #region Methods

        public void LoadCompetitionLeagueGrid()
        {
            DataTable dt = new DataTable();
            dt = clc.GetCompetitionLeagueList();

            if (dt.Rows.Count > 0)
            {
                gvCompetitionLeague.DataSource = dt;
                gvCompetitionLeague.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveCompetitionLeague_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            cl.CompetitionLeagueName = txtCompetitionLeague.Text.Trim();
            cl.CompeititionLeagueAbbr = txtCompetitionLeagueAbbr.Text;
            cl.CompetitionLeagueDesc = txtCompetitionLeagueDesc.Text.Trim();
            cl.CompetitionLeagueLogoName = txtCompetitionLeagueLogoName.Text;

            cl.CompetitionLeagueLogoFile = imhpathDB + CompetitionLeagueLogoFile.PostedFile.FileName.Replace(" ", "");

            if (CompetitionLeagueLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(CompetitionLeagueLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(CompetitionLeagueLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (CompetitionLeagueLogoFile.PostedFile.ContentLength > 10485760)
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
                    CompetitionLeagueLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + CompetitionLeagueLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }


            cl.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            cl.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            cl.PortalID = PortalId;
            cl.CreatedById = currentUser.Username;
            cl.ModifiedById = currentUser.Username;

            // Call Save Method
            clc.InsertCompetitionLeague(cl);

            btnAddCompetitionLeague.Visible = true;
            pnlCompetitionLeagueGrid.Visible = true;
            LoadCompetitionLeagueGrid();
            ClearData();

        }

        protected void btnUpdateCompetitionLeague_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            cl.CompetitionLeagueId = competitionleagueID;
            cl.CompetitionLeagueName = txtCompetitionLeague.Text.Trim();
            cl.CompeititionLeagueAbbr = txtCompetitionLeagueAbbr.Text;
            cl.CompetitionLeagueDesc = txtCompetitionLeagueDesc.Text.Trim();
            cl.CompetitionLeagueLogoName = txtCompetitionLeagueLogoName.Text;

            if (CompetitionLeagueLogoFile.PostedFile.FileName == "")
            {
                //DataTable dt1 = new DataTable();
                //cl.CompetitionLeagueId = Convert.ToInt32(hdnCompetitionLeagueID.Value);
                //dt1 = clc.GetCompetitionLeagueDetailByCompetitionLeagueID(cl.CompetitionLeagueId);
                //CompetitionLeagueLogoImage.ImageUrl = dt1.Rows[0]["CompetitionLeagueLogoFile"].ToString();
                //string ufname = dt1.Rows[0]["CompetitionLeagueLogoFile"].ToString().Replace(" ", "");
                //CompetitionLeagueLogoFile.ResolveUrl("ufname");
                //cl.CompetitionLeagueLogoFile = ufname.Replace(" ", "");

                cl.CompetitionLeagueLogoFile = CompetitionLeagueLogoImage.ImageUrl;
                FileOKForUpdate = true;
            }
            else
            {

                cl.CompetitionLeagueLogoFile = imhpathDB + CompetitionLeagueLogoFile.PostedFile.FileName.Replace(" ", "");

                if (CompetitionLeagueLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(CompetitionLeagueLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(CompetitionLeagueLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (CompetitionLeagueLogoFile.PostedFile.ContentLength > 10485760)
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
                        CompetitionLeagueLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + CompetitionLeagueLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            cl.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            cl.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
            cl.PortalID = PortalId;
            cl.ModifiedById = currentUser.Username;

            // Call Update Method
            clc.UpdateCompetitionLeague(cl);

            btnAddCompetitionLeague.Visible = true;
            pnlCompetitionLeagueGrid.Visible = true;
            btnSaveCompetitionLeague.Visible = true;
            btnUpdateCompetitionLeague.Visible = false;
            LoadCompetitionLeagueGrid();
            ClearData();
        }

        protected void btnCancelCompetitionLeague_Click(object sender, EventArgs e)
        {
            pnlCompetitionLeagueGrid.Visible = true;
            pnlCompetitionLeagueEntry.Visible = false;
            btnSaveCompetitionLeague.Visible = false;
            btnUpdateCompetitionLeague.Visible = false;
            LoadCompetitionLeagueGrid();
            ClearData();
        }

        public void ClearData()
        {
            txtCompetitionLeague.Text = "";
            txtCompetitionLeagueDesc.Text = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnAddCompetitionLeague_Click(object sender, EventArgs e)
        {
            pnlCompetitionLeagueGrid.Visible = false;
            pnlCompetitionLeagueEntry.Visible = true;
            btnSaveCompetitionLeague.Visible = true;
            btnUpdateCompetitionLeague.Visible = false;
            ClearData();
        }

        #endregion Button Click Events

        #region Gridview Events

        protected void gvCompetitionLeague_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompetitionLeague.PageIndex = e.NewPageIndex;
            LoadCompetitionLeagueGrid();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnCompetitionLeagueID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdn_CompetitionLeague_Id")).Value;
            
            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ClearData();
                DataTable dt1 = clc.GetCompetitionLeagueDetailByCompetitionLeagueID(competitionleagueID);

                if (dt1.Rows.Count > 0)
                {
                    txtCompetitionLeague.Text = dt1.Rows[0]["CompetitionLeagueName"].ToString();
                    txtCompetitionLeagueDesc.Text = dt1.Rows[0]["CompetitionLeagueDesc"].ToString();
                    txtCompetitionLeagueAbbr.Text = dt1.Rows[0]["CompetitionLeagueAbbr"].ToString();
                    txtCompetitionLeagueLogoName.Text = dt1.Rows[0]["CompetitionLeagueLogoName"].ToString();
                    CompetitionLeagueLogoImage.ImageUrl = dt1.Rows[0]["CompetitionLeagueLogoFile"].ToString();

                    string ufname = dt1.Rows[0]["CompetitionLeagueLogoFile"].ToString().Replace(" ", "");
                    CompetitionLeagueLogoImage.ResolveUrl("ufname");


                    if (dt1.Rows[0]["ActiveFlagId"].ToString() == "1")
                    {
                        ChkIsActive.Checked = true;
                    }
                    if (dt1.Rows[0]["ShowFlagId"].ToString() == "1")
                    {
                        ChkIsShow.Checked = true;
                    }
                }

                btnUpdateCompetitionLeague.Visible = true;
                btnSaveCompetitionLeague.Visible = false;
                pnlCompetitionLeagueEntry.Visible = true;
                pnlCompetitionLeagueGrid.Visible = false;
            }
            else if (ddlSelectedValue == "Delete")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetLeague(), "alert", "DeleteSuccessfully();", true);
                //int documentid = 0;
                //int.TryParse(str, out documentid);
                //new CompetitionSponsorController().DeleteCompeSpon(documentid);
                //LoadDocumentsGrid(CompetitionID);
            }
        }

        #endregion
    }
}
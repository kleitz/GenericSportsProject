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
    public partial class frmDivision : PortalModuleBase
    {
        clsDivision dvClass = new clsDivision();
        clsDivisionController dvController = new clsDivisionController();
        clsSeasonController sController = new clsSeasonController();

        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        #region variables

        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\DivisionLogo\\";
        public string imhpathDB = "Images\\DivisionLogo\\";
        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        int DivisionID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdnDivisionID.Value))
                {
                    int.TryParse(hdnDivisionID.Value, out id);
                }
                return id;
            }
        }

        #endregion variables

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateDivision.Visible = false;
            btnSaveDivision.Visible = false;
            pnlDivisionEntry.Visible = false;

            LoadDivisionGrid();

            if (IsPostBack)
            {
                FillSeason();
            }

        }

        #endregion Page Events

        #region Methods

        public void FillSeason()
        {
            using (DataTable Season_dt = sController.GetDataSeason())
            {
                if (Season_dt.Rows.Count > 0)
                {
                    ddlSeason.DataSource = Season_dt;
                    ddlSeason.DataTextField = "SeasonName";
                    ddlSeason.DataValueField = "SeasonID";
                    ddlSeason.DataBind();
                }
                ddlSeason.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        public void LoadDivisionGrid()
        {
            DataTable dt = new DataTable();
            dt = dvController.GetDivisionList();

            if (dt.Rows.Count > 0)
            {
                gvDivision.DataSource = dt;
                gvDivision.DataBind();
            }
        }

        #endregion Methods

        #region Button Click Events

        protected void btnSaveDivision_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SaveSuccessfully();", true);

            int.TryParse(ddlSeason.SelectedValue, out dvClass.SeasonId);

            dvClass.DivisionName = txtDivision.Text.Trim();
            dvClass.DivisionAbbr = txtDivisionAbbr.Text;
            dvClass.DivisionDesc = txtDivisionDesc.Text.Trim();
            dvClass.DivisionLogoName = txtDivisionLogoName.Text;

            int.TryParse(txtDivisionLevel.Text, out dvClass.DivisionLevel);
            int.TryParse(txtTotalNoOfTeam.Text,out dvClass.TotalNumofTeams);
            int.TryParse(txtPromotedNum.Text, out dvClass.PromotedNum);
            int.TryParse(txtDemotedNum.Text, out dvClass.DemotedNum);

            dvClass.DivisionLogoFile = imhpathDB + DivisionLogoFile.PostedFile.FileName.Replace(" ", "");

            if (DivisionLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(DivisionLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(DivisionLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.dvClassientScript.RegisterdvClassientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (DivisionLogoFile.PostedFile.ContentLength > 10485760)
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
                    DivisionLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + DivisionLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }
            
            dvClass.PortalID = PortalId;
            dvClass.CreatedById = currentUser.Username;
            dvClass.ModifiedById = currentUser.Username;

            // Call Save Method
            dvController.InsertDivision(dvClass);

            btnAddDivision.Visible = true;
            pnlDivisionGrid.Visible = true;
            LoadDivisionGrid();
            ClearData();

        }

        protected void btnUpdateDivision_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "UpdateSuccessfully();", true);

            dvClass.DivisionId = DivisionID;
            int.TryParse(ddlSeason.SelectedValue, out dvClass.SeasonId);

            dvClass.DivisionName = txtDivision.Text.Trim();
            dvClass.DivisionAbbr = txtDivisionAbbr.Text;
            dvClass.DivisionDesc = txtDivisionDesc.Text.Trim();
            dvClass.DivisionLogoName = txtDivisionLogoName.Text;

            int.TryParse(txtDivisionLevel.Text, out dvClass.DivisionLevel);
            int.TryParse(txtTotalNoOfTeam.Text, out dvClass.TotalNumofTeams);
            int.TryParse(txtPromotedNum.Text, out dvClass.PromotedNum);
            int.TryParse(txtDemotedNum.Text, out dvClass.DemotedNum);
            
            if (DivisionLogoFile.PostedFile.FileName == "")
            {
                //DataTable dt1 = new DataTable();
                //dvClass.DivisionId = Convert.ToInt32(hdnDivisionID.Value);
                //dt1 = dvClassc.GetDivisionDetailByDivisionID(dvClass.DivisionId);
                //DivisionLogoImage.ImageUrl = dt1.Rows[0]["DivisionLogoFile"].ToString();
                //string ufname = dt1.Rows[0]["DivisionLogoFile"].ToString().Replace(" ", "");
                //DivisionLogoFile.ResolveUrl("ufname");
                //dvClass.DivisionLogoFile = ufname.Replace(" ", "");

                dvClass.DivisionLogoFile = DivisionLogoImage.ImageUrl;
                FileOKForUpdate = true;
            }
            else
            {

                dvClass.DivisionLogoFile = imhpathDB + DivisionLogoFile.PostedFile.FileName.Replace(" ", "");

                if (DivisionLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(DivisionLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(DivisionLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.dvClassientScript.RegisterdvClassientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (DivisionLogoFile.PostedFile.ContentLength > 10485760)
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
                        DivisionLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + DivisionLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            dvClass.PortalID = PortalId;
            dvClass.ModifiedById = currentUser.Username;

            // Call Update Method
            dvController.UpdateDivision(dvClass);

            btnAddDivision.Visible = true;
            pnlDivisionGrid.Visible = true;
            btnSaveDivision.Visible = true;
            btnUpdateDivision.Visible = false;
            LoadDivisionGrid();
            ClearData();
        }

        protected void btnCancelDivision_Click(object sender, EventArgs e)
        {
            pnlDivisionGrid.Visible = true;
            pnlDivisionEntry.Visible = false;
            btnSaveDivision.Visible = false;
            btnUpdateDivision.Visible = false;
            LoadDivisionGrid();
            ClearData();
        }

        public void ClearData()
        {
            txtDivision.Text = "";
            txtDivisionDesc.Text = "";
            txtDivisionAbbr.Text = "";
            txtDivisionLevel.Text = "";
            txtDivisionLogoName.Text = "";
            txtTotalNoOfTeam.Text = "";
            txtPromotedNum.Text = "";
            txtDemotedNum.Text = "";
            ddlSeason.ClearSelection();

        }

        protected void btnAddDivision_Click(object sender, EventArgs e)
        {
            pnlDivisionGrid.Visible = false;
            pnlDivisionEntry.Visible = true;
            btnSaveDivision.Visible = true;
            btnUpdateDivision.Visible = false;
            ClearData();
        }

        #endregion Button Click Events

        #region Gridview Events

        protected void gvDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDivision.PageIndex = e.NewPageIndex;
            LoadDivisionGrid();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnDivisionID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdn_Division_Id")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                ClearData();
                DataTable dt1 = dvController.GetDivisionByDivisionID(DivisionID);

                if (dt1.Rows.Count > 0)
                {
                    txtDivision.Text = dt1.Rows[0]["DivisionName"].ToString();
                    txtDivisionDesc.Text = dt1.Rows[0]["DivisionDesc"].ToString();
                    txtDivisionAbbr.Text = dt1.Rows[0]["DivisionAbbr"].ToString();
                    txtDivisionLogoName.Text = dt1.Rows[0]["DivisionLogoName"].ToString();
                    DivisionLogoImage.ImageUrl = dt1.Rows[0]["DivisionLogoFile"].ToString();

                    ddlSeason.SelectedValue = dt1.Rows[0]["SeasonId"].ToString();

                    txtDivisionLevel.Text = dt1.Rows[0]["DivisionLevel"].ToString();
                    txtTotalNoOfTeam.Text = dt1.Rows[0]["TotalNumofTeams"].ToString();
                    txtPromotedNum.Text = dt1.Rows[0]["PromotedNum"].ToString();
                    txtDemotedNum.Text = dt1.Rows[0]["DemotedNum"].ToString();
                    
                    string ufname = dt1.Rows[0]["DivisionLogoFile"].ToString().Replace(" ", "");
                    DivisionLogoImage.ResolveUrl("ufname");
                }

                btnUpdateDivision.Visible = true;
                btnSaveDivision.Visible = false;
                pnlDivisionEntry.Visible = true;
                pnlDivisionGrid.Visible = false;
            }
            //else if (ddlSelectedValue == "Team")
            //{
            //    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", "mctl=" + "frmDivisionTeams", "DivisinID=" + DivisionID));
            //}
            else if (ddlSelectedValue == "Delete")
            {
                //Page.dvClassientScript.RegisterStartupScript(this.GetLeague(), "alert", "DeleteSuccessfully();", true);
                //int documentid = 0;
                //int.TryParse(str, out documentid);
                //new CompetitionSponsorController().DeleteCompeSpon(documentid);
                //LoadDocumentsGrid(CompetitionID);
            }
        }

        #endregion
    }
}
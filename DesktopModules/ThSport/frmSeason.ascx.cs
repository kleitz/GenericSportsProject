using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThSportServer;
using System.Data;
using System.IO;
using System.Globalization;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Modules.ThSport
{
    public partial class frmSeason : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSeason cs = new clsSeason();
        clsSeasonController csc= new clsSeasonController();

        string m_controlToLoad;
        string VName;
        int SeasonID = 0;
        string physicalpath = HttpContext.Current.Request.PhysicalApplicationPath;
        public string ImageUploadFolder = "DesktopModules\\ThSport\\Images\\AllImage\\";
        public string imhpathDB = "Images\\AllImage\\";

        Boolean FileOK = false;
        Boolean FileSaved = false;
        Boolean FileOKForUpdate = false;
        Boolean FileSavedForUpdate = false;

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCountry();
                FillGridView();
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillGridView();
        }

        #endregion

        private void FillGridView()
        {
            DataTable dt = new DataTable();

            if (currentUser.IsSuperUser || currentUser.IsInRole("Club Admin"))
            {
                dt = csc.GetDataSeason();
            }

            DataView dv = new DataView();
            dv = dt.AsDataView();
            dv.RowFilter = " SeasonName like '%%" + txtSeasonNameSearch.Text.Trim() + "%%'";

            if (dv.ToTable().Rows.Count > 0)
            {
                ViewState["dt"] = dv.ToTable();
                gvSeason.DataSource = dv.ToTable();
                gvSeason.DataBind();
            }
        }

        private void FillCountry()
        {
            DataTable dt = new DataTable();
            dt = csc.FillCountryDropdown();
            if (dt.Rows.Count > 0)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-- Select Country --", "0"));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            funClearData();
        }

        private void funClearData()
        {
            txtSeasonTitle.Text = "";
            txtSeasonDescription.Text = "";
            txtSeasonStartDate.Text = "";
            txtSeasonEndDate.Text = "";
            txtSeasonLogoName.Text = "";
            SeasonLogoImage.ImageUrl = "";
            ChkIsActive.Checked = false;
            ChkIsShow.Checked = false;
        }

        protected void btnSaveSeason_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SeasonSaveSuccessfully();", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            cs.SeasonName = txtSeasonTitle.Text.Trim();
            cs.SeasonDesc = txtSeasonDescription.Text.Trim();
            cs.StartDate = txtSeasonStartDate.Text.ToString();

            DateTime startDate;
            DateTime.TryParse(txtSeasonStartDate.Text, out startDate);

            if (!string.IsNullOrEmpty(txtSeasonEndDate.Text))
            {
                DateTime endDate;
                DateTime.TryParse(txtSeasonEndDate.Text, out endDate);

                int result = DateTime.Compare(startDate, endDate);

                if (result >= 0)
                {

                    error_msg.Text = "Start date must be less than End date.";
                    error_msg.Visible = true;

                    error_div.Attributes.Add("style", "display:block;");
                    error_div.Attributes.Add("class", "alert alert-error");
                    return;
                }
                else
                {
                    error_msg.Visible = false;
                    error_div.Attributes.Add("style", "display:none;");
                }
            }
            else
            {
                txtSeasonEndDate.Text = DateTime.Now.ToString();
            }

            cs.EndDate = txtSeasonEndDate.Text;

            cs.SeasonLogoName = txtSeasonLogoName.Text.Trim();

            cs.SeasonLogoFile = imhpathDB + SeasonLogoFile.PostedFile.FileName.Replace(" ", "");

            if (SeasonLogoFile.PostedFile != null)
            {
                String FileExtension = Path.GetExtension(SeasonLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

            if (!string.IsNullOrEmpty(SeasonLogoFile.PostedFile.FileName))
            {
                if (!FileOK)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                    return;
                }
            }

            if (FileOK)
            {
                if (SeasonLogoFile.PostedFile.ContentLength > 10485760)
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
                    SeasonLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SeasonLogoFile.PostedFile.FileName.Replace(" ", ""));
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }

            if (ChkIsActive.Checked == true)
            {
                cs.ActiveFlagID = 1;
            }
            else
            {
                cs.ActiveFlagID = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cs.ShowFlagID = 1;
            }
            else
            {
                cs.ShowFlagID = 0;
            }

            cs.PortalID = PortalId;
            cs.CreatedByID = currentUser.Username;
            cs.ModifiedById = currentUser.Username;
            
            int Season_Id = csc.InsertSeason(cs);

            mainContentSeason.Visible = false;
            PnlGridSeason.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void btnAddSeason_Click(object sender, EventArgs e)
        {
            funClearData();
            mainContentSeason.Visible = true;
            PnlGridSeason.Visible = false;
            btnSaveSeason.Visible = true;
            btnUpdateSeason.Visible = false;
            FillCountry();
        }

        protected void btnCloseSeason_Click(object sender, EventArgs e)
        {
            mainContentSeason.Visible = false;
            PnlGridSeason.Visible = true;
            FillGridView();
        }

        protected void btnUpdateSeason_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "SeasonUpdateSuccessfully()", true);

            Boolean FileOK = false;
            Boolean FileSaved = false;

            cs.SeasonID = Convert.ToInt32(hidRegID.Value);
            cs.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            cs.SeasonName = txtSeasonTitle.Text.Trim();
            cs.SeasonDesc = txtSeasonDescription.Text.Trim();
            cs.StartDate = txtSeasonStartDate.Text.ToString();

            DateTime startDate;
            DateTime.TryParse(txtSeasonStartDate.Text, out startDate);

            if (!string.IsNullOrEmpty(txtSeasonEndDate.Text))
            {
                DateTime endDate;
                DateTime.TryParse(txtSeasonEndDate.Text, out endDate);

                int result = DateTime.Compare(startDate, endDate);

                if (result >= 0)
                {

                    error_msg.Text = "Start date must be less than End date.";
                    error_msg.Visible = true;

                    error_div.Attributes.Add("style", "display:block;");
                    error_div.Attributes.Add("class", "alert alert-error");
                    return;
                }
                else
                {
                    error_msg.Visible = false;
                    error_div.Attributes.Add("style", "display:none;");
                }
            }
            else
            {
                txtSeasonEndDate.Text = DateTime.Now.ToString();
            }

            cs.EndDate = txtSeasonEndDate.Text;

            cs.SeasonLogoName = txtSeasonLogoName.Text.Trim();

            if (SeasonLogoFile.PostedFile.FileName == "")
            {
                DataTable dt1 = new DataTable();
                cs.SeasonID = Convert.ToInt32(hidRegID.Value);
                dt1 = csc.GetSeasonLogoBySeasonID(cs);
                SeasonLogoImage.ImageUrl = dt1.Rows[0]["SeasonLogoFile"].ToString();
                string ufname = dt1.Rows[0]["SeasonLogoFile"].ToString().Replace(" ", "");
                SeasonLogoFile.ResolveUrl("ufname");
                cs.SeasonLogoFile = ufname.Replace(" ", "");
                FileOKForUpdate = true;
            }
            else
            {
                cs.SeasonLogoFile = imhpathDB + SeasonLogoFile.PostedFile.FileName.Replace(" ", "");

                if (SeasonLogoFile.PostedFile != null)
                {
                    String FileExtension = Path.GetExtension(SeasonLogoFile.PostedFile.FileName.Replace(" ", "")).ToLower();
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

                if (!string.IsNullOrEmpty(SeasonLogoFile.PostedFile.FileName))
                {
                    if (!FileOK)
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif images For Competition !')", true);
                        return;
                    }
                }

                if (FileOK)
                {
                    if (SeasonLogoFile.PostedFile.ContentLength > 10485760)
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
                        SeasonLogoFile.PostedFile.SaveAs(physicalpath + ImageUploadFolder + SeasonLogoFile.PostedFile.FileName.Replace(" ", ""));
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
            }

            if (ChkIsActive.Checked == true)
            {
                cs.ActiveFlagID = 1;
            }
            else
            {
                cs.ActiveFlagID = 0;
            }

            if (ChkIsShow.Checked == true)
            {
                cs.ShowFlagID = 1;
            }
            else
            {
                cs.ShowFlagID = 0;
            }

            cs.PortalID = PortalId;
            cs.CreatedByID = currentUser.Username;
            cs.ModifiedById = currentUser.Username;

            int Season_Id = csc.UpdateSeason(cs);

            mainContentSeason.Visible = false;
            PnlGridSeason.Visible = true;
            FillGridView();
            funClearData();
        }

        protected void gvSeason_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSeason.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((Label)((DropDownList)sender).Parent.FindControl("lblddlActionSeasonID")).Text;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                funClearData();
                int SeasonID = 0;
                int.TryParse(str, out SeasonID);

                LinkButton btn = sender as LinkButton;

                clsSeason cs = new clsSeason();
                clsSeasonController csc = new clsSeasonController();
                
                DataTable dt = new DataTable();

                dt = csc.GetSeasonDataBySeasonID(SeasonID);

                if (dt.Rows.Count > 0)
                {
                    hidRegID.Value = dt.Rows[0]["SeasonID"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["CountryID"].ToString();
                    txtSeasonTitle.Text = dt.Rows[0]["SeasonName"].ToString();
                    txtSeasonDescription.Text = dt.Rows[0]["SeasonDesc"].ToString();
                    txtSeasonStartDate.Text = dt.Rows[0]["StartDate"].ToString();

                    if (dt.Rows[0]["EndDate"].ToString() != "01/01/0001" && dt.Rows[0]["EndDate"].ToString() != "01/01/1900")
                    {
                        txtSeasonEndDate.Text = dt.Rows[0]["EndDate"].ToString();
                    }

                    txtSeasonLogoName.Text = dt.Rows[0]["SeasonLogoName"].ToString();

                    SeasonLogoImage.ImageUrl = dt.Rows[0]["SeasonLogoFile"].ToString();

                    string ufname = dt.Rows[0]["SeasonLogoFile"].ToString().Replace(" ", "");
                    SeasonLogoFile.ResolveUrl("ufname");

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

                    mainContentSeason.Visible = true;
                    PnlGridSeason.Visible = false;
                    btnUpdateSeason.Visible = true;
                    btnSaveSeason.Visible = false;
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "javascript:confirm('Are You Sure? Want To Delete.');", true);

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "javascript:Confirmation();", true);

                //int competition_Id = 0;

                //int.TryParse(str, out competition_Id);

                //CompRegInfo.DeleteCompetitionReg(competition_Id);

                //FillGridView();
            }
        }

    }
}
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
    public partial class frmLocation : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSportController spController = new clsSportController();
        clsLocation lClass = new clsLocation();
        clsLocationController lController = new clsLocationController();

        #region Variables

        int Location_ID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_LocationID.Value))
                {
                    int.TryParse(hdn_LocationID.Value, out id);
                }
                return id;
            }
        }

        #endregion

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillSport();
                FillLocationGridView();

                FillAllCountryList();
                FillAllCountryListUseToSearching();

            }
        }

        #endregion

        #region Grid Events

        protected void gvLocation_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridview = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnloc_CountryID = (HiddenField)e.Row.FindControl("hdnloc_CountryID");
                Label lblCountryName = (Label)e.Row.FindControl("lblCountryName");

                if (!string.IsNullOrEmpty(hdnloc_CountryID.Value))
                {
                    int countryId = 0;
                    int.TryParse(hdnloc_CountryID.Value, out countryId);

                    DataTable dtForCountry = lController.GetCountryByID(countryId);

                    if (dtForCountry.Rows.Count > 0)
                    {
                        lblCountryName.Text = dtForCountry.Rows[0]["CountryName"].ToString();
                    }
                }

                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvLocation, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";

            }
        }

        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;
            FillLocationGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_LocationID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnLocationID")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                pnlEntry.Visible = true;
                pnlList.Visible = false;
                btnSaveLocation.Visible = false;
                btnUpdateLocation.Visible = true;

                ClearData();

                LinkButton btn = sender as LinkButton;

                using (DataTable dt = lController.GetLocationByID(Location_ID))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Loc_LocationName"].ToString()))
                        {
                            txtLocationName.Text = dt.Rows[0]["Loc_LocationName"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Loc_LocationAddress"].ToString()))
                        {
                            txtLocationAddress.Text = dt.Rows[0]["Loc_LocationAddress"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Loc_City"].ToString()))
                        {
                            txtCity.Text = dt.Rows[0]["Loc_City"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Loc_State"].ToString()))
                        {
                            ddlState.Text = dt.Rows[0]["Loc_State"].ToString();
                        }
                        else
                        {
                            ddlState.SelectedValue = "0";
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Loc_ZipCode"].ToString()))
                        {
                            txtZipCode.Text = dt.Rows[0]["Loc_ZipCode"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Loc_Country"].ToString()))
                        {
                            ddlCountry.SelectedValue = dt.Rows[0]["Loc_Country"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["SportID"].ToString()))
                        {
                            ddlSport.SelectedValue = dt.Rows[0]["SportID"].ToString();
                        }
                    }
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                lController.DeleteLocation(Location_ID);
                FillLocationGridView();
                pnlEntry.Visible = false;
                pnlList.Visible = true;
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillLocationGridView();
        }

        protected void gvLocation_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLocation.EditIndex = e.NewEditIndex;
            this.BindGrid();

            Panel pnlEditable = gvLocation.Rows[gvLocation.EditIndex].FindControl("pnlEditable") as Panel;

            pnlEditable.Visible = true;

            gvLocation.EditRowStyle.ForeColor = System.Drawing.Color.White;
            gvLocation.EditRowStyle.BackColor = System.Drawing.Color.DarkSeaGreen;

            for (int i = 0; i < gvLocation.Columns.Count; i++)
            {
                gvLocation.Rows[gvLocation.EditIndex].Cells[i].ForeColor = System.Drawing.Color.White;
                gvLocation.Rows[gvLocation.EditIndex].Cells[i].BackColor = System.Drawing.Color.DarkSeaGreen;
            }

        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string name = (row.Cells[0].Controls[0] as TextBox).Text;

            int loc_id = 0;
            int.TryParse((row.Cells[1].Controls[1] as Label).Text, out loc_id);

            lController.UpdateLocationName(name, loc_id);

            gvLocation.EditIndex = -1;
            this.BindGrid();

        }

        protected void OnCancel(object sender, EventArgs e)
        {
            gvLocation.EditIndex = -1;
            this.BindGrid();
        }

        #endregion

        #region Methods

        private void FillAllCountryList()
        {
            DataTable dt = new DataTable();
            dt = lController.GetCountryList();
            ddlCountry.DataSource = dt;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("-- Select Country--", "0"));
        }

        private void FillAllCountryListUseToSearching()
        {
            DataTable dt = new DataTable();
            dt = lController.GetCountryList();
            ddlSearchByCountry.DataSource = dt;
            ddlSearchByCountry.DataTextField = "CountryName";
            ddlSearchByCountry.DataValueField = "CountryID";
            ddlSearchByCountry.DataBind();
            ddlSearchByCountry.Items.Insert(0, new ListItem("-- All Countries--", "0"));
        }

        private void FillLocationGridView()
        {
            using (DataTable dt = lController.FetchAllLocation(currentUser.Username,ddlSearchByCountry.SelectedValue,txtLocationNameSearch.Text))
            {
                gvLocation.DataSource = dt;
                gvLocation.DataBind();
            }
        }

        private void FillSport()
        {
            using (DataTable dt = spController.GetDataSport())
            {   
                if (dt.Rows.Count > 0)
                {
                    ddlSport.DataSource = dt;
                    ddlSport.DataTextField = "SportName";
                    ddlSport.DataValueField = "SportID";
                    ddlSport.DataBind();
                    ddlSport.Items.Insert(0, new ListItem("-- Select Sport --", "0"));
                    ddlSport.SelectedValue = "0";
                }
            }
        }

        private void ClearData()
        {
            ddlSport.ClearSelection();
            txtLocationName.Text = "";
            txtLocationAddress.Text = "";
            ddlCountry.ClearSelection();
            ddlState.ClearSelection();
            txtCity.Text = "";
            txtZipCode.Text = "";
        }

        #endregion

        #region Button Clicks

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillLocationGridView();
        }

        protected void btnSaveLocation_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "LocationSaveSuccessfully();", true);

            lClass.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            lClass.Loc_LocationName = txtLocationName.Text.Trim();
            lClass.Loc_LocationAddress = txtLocationAddress.Text.Trim();
            lClass.Loc_Country = ddlCountry.SelectedValue;
            if (ddlState.SelectedValue == "0")
            {
                lClass.Loc_State = "";
            }
            else
            {
                lClass.Loc_State = ddlState.SelectedValue;
            }

            lClass.Loc_ZipCode = txtZipCode.Text;
            lClass.Loc_City = txtCity.Text;
            lClass.PortalID = PortalId;
            lClass.CreatedById = currentUser.Username;
            lClass.ModifiedById = currentUser.Username;

            lController.InsertLocation(lClass);

            pnlList.Visible = true;
            pnlEntry.Visible = false;
            FillLocationGridView();
            ClearData();
        }

        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            ClearData();
            pnlEntry.Visible = true;
            pnlList.Visible = false;
            btnSaveLocation.Visible = true;
            btnUpdateLocation.Visible = false;
            FillSport();
        }

        protected void btnCloseLocation_Click(object sender, EventArgs e)
        {
            pnlList.Visible = false;
            pnlEntry.Visible = true;
            FillLocationGridView();
        }

        protected void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "LocationUpdateSuccessfully()", true);

            lClass.SportID = Convert.ToInt32(ddlSport.SelectedValue);
            lClass.Loc_LocationName = txtLocationName.Text.Trim();
            lClass.Loc_LocationAddress = txtLocationAddress.Text.Trim();
            lClass.Loc_Country = ddlCountry.SelectedValue;
            if (ddlState.SelectedValue == "0")
            {
                lClass.Loc_State = "";
            }
            else
            {
                lClass.Loc_State = ddlState.SelectedValue;
            }

            lClass.Loc_ZipCode = txtZipCode.Text;
            lClass.Loc_City = txtCity.Text;
            lClass.PortalID = PortalId;
            lClass.CreatedById = currentUser.Username;
            lClass.ModifiedById = currentUser.Username;

            lClass.Loc_LocationID = Location_ID;

            lController.UpdateLocation(lClass);

            pnlEntry.Visible = false;
            pnlList.Visible = true;
            FillLocationGridView();
            ClearData();
        }


        #endregion

    }
}
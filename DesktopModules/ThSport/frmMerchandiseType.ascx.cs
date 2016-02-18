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
    public partial class frmMerchandiseType : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSportController spController = new clsSportController();
        clsMerchandiseType mtClass = new clsMerchandiseType();
        clsMerchandiseTypeController mtController = new clsMerchandiseTypeController();

        #region MerchandiseType

        int MerchandiseType_ID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_MerchandiseTypeID.Value))
                {
                    int.TryParse(hdn_MerchandiseTypeID.Value, out id);
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
                FillMerchandiseTypeGridView();
            }
        }

        #endregion

        #region Grid Events



        protected void gvMerchandiseType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMerchandiseType.PageIndex = e.NewPageIndex;
            FillMerchandiseTypeGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_MerchandiseTypeID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnMerchandiseTypeID")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                pnlEntry.Visible = true;
                pnlList.Visible = false;
                btnSaveMerchandiseType.Visible = false;
                btnUpdateMerchandiseType.Visible = true;
                BindSport();
                ClearData();

                LinkButton btn = sender as LinkButton;

                using (DataTable dt = mtController.GetMerchandiseTypeByMerchandiseTypeId(MerchandiseType_ID))
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["MerchandiseType"].ToString()))
                        {
                            txtMerchandiseTypeName.Text = dt.Rows[0]["MerchandiseType"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["MerchandiseDesc"].ToString()))
                        {
                            txtMerchandiseTypeDescription.Text = dt.Rows[0]["MerchandiseDesc"].ToString();
                        }
                        ddlSport.SelectedValue = dt.Rows[0]["SportId"].ToString();
                    }
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                mtController.DeleteMerchandiseType(MerchandiseType_ID);
                FillMerchandiseTypeGridView();
                pnlEntry.Visible = false;
                pnlList.Visible = true;
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillMerchandiseTypeGridView();
        }


        protected void BindSport()
        {
            using (DataTable Sport_dt = spController.GetDataSport())
            {
                if (Sport_dt.Rows.Count > 0)
                {
                    ddlSport.DataSource = Sport_dt;
                    ddlSport.DataTextField = "SportName";
                    ddlSport.DataValueField = "SportID";
                    ddlSport.DataBind();
                }
                ddlSport.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

        #endregion

        #region Methods

        private void FillMerchandiseTypeGridView()
        {
            using (DataTable dt = mtController.GetAllMerchandiseType())
            {
                gvMerchandiseType.DataSource = dt;
                gvMerchandiseType.DataBind();
            }
        }

        private void ClearData()
        {

            txtMerchandiseTypeName.Text = "";
            txtMerchandiseTypeDescription.Text = "";

        }

        #endregion

        #region Button Clicks

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillMerchandiseTypeGridView();
        }

        protected void btnSaveMerchandiseType_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MerchandiseTypeSaveSuccessfully();", true);


                mtClass.MerchandiseType = txtMerchandiseTypeName.Text.Trim();
                mtClass.MerchandiseDesc = txtMerchandiseTypeDescription.Text;
                mtClass.PortalID = currentUser.PortalID;
                mtClass.CreatedById = currentUser.Username;
                mtClass.ModifiedById = currentUser.Username;
                mtClass.SportId = Convert.ToInt32(ddlSport.SelectedValue);
                mtController.InsertMerchandiseType(mtClass);

                pnlList.Visible = true;
                pnlEntry.Visible = false;
                FillMerchandiseTypeGridView();
                ClearData();
            }
        }

        protected void btnAddMerchandiseType_Click(object sender, EventArgs e)
        {
            ClearData();
            pnlEntry.Visible = true;
            pnlList.Visible = false;
            btnSaveMerchandiseType.Visible = true;
            btnUpdateMerchandiseType.Visible = false;
            BindSport();

        }

        protected void btnCloseMerchandiseType_Click(object sender, EventArgs e)
        {
            pnlList.Visible = true;
            pnlEntry.Visible = false;
            FillMerchandiseTypeGridView();
        }

        protected void btnUpdateMerchandiseType_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MerchandiseTypeUpdateSuccessfully()", true);

            mtClass.SportId = Convert.ToInt32(ddlSport.SelectedValue);

            mtClass.MerchandiseType = txtMerchandiseTypeName.Text.Trim();

            mtClass.MerchandiseDesc = txtMerchandiseTypeDescription.Text;

            mtClass.ModifiedById = currentUser.Username;

            mtClass.MerchandiseTypeId = MerchandiseType_ID;

            mtController.UpdateMerchandiseType(mtClass);

            pnlEntry.Visible = false;
            pnlList.Visible = true;
            FillMerchandiseTypeGridView();
            ClearData();
        }


        #endregion
    }
}
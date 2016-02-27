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


namespace DotNetNuke.Modules.ThSport
{
    public partial class frmMerchandise : PortalModuleBase
    {
        private readonly UserInfo currentUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

        clsSportController spController = new clsSportController();
        clsMerchandise mtClass = new clsMerchandise();
        clsMerchandiseController mtController = new clsMerchandiseController();

        #region Merchandise

        int Merchandise_ID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrEmpty(hdn_MerchandiseID.Value))
                {
                    int.TryParse(hdn_MerchandiseID.Value, out id);
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
                FillMerchandiseGridView();
            }
        }

        #endregion

        #region Grid Events



        protected void gvMerchandise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMerchandise.PageIndex = e.NewPageIndex;
            FillMerchandiseGridView();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_MerchandiseID.Value = ((HiddenField)((DropDownList)sender).Parent.FindControl("hdnMerchandiseID")).Value;

            string ddlSelectedValue = ((DropDownList)sender).SelectedValue;

            if (ddlSelectedValue == "Edit")
            {
                pnlEntry.Visible = true;
                pnlList.Visible = false;
                btnSaveMerchandise.Visible = false;
                btnUpdateMerchandise.Visible = true;
                BindMerchandiseType();
                ClearData();

                LinkButton btn = sender as LinkButton;

                using (DataTable dt = mtController.GetMerchandiseByMerchandiseId(Merchandise_ID))
                {
                    if (dt.Rows.Count > 0)
                    {

                        txtMerchandiseTitle.Text = dt.Rows[0]["MerchandiseTitle"].ToString();

                        txtMerchandiseDescription.Text = dt.Rows[0]["MerchandiseDesc"].ToString();
                        txtPrice.Text = dt.Rows[0]["MerchandisePrice"].ToString();

                        ddlMerchandiseType.SelectedValue = dt.Rows[0]["MerchandiseTypeId"].ToString();
                        if (dt.Rows[0]["ActiveFlagId"].ToString() == "1")
                        {
                            ChkIsActive.Checked = true;
                        }
                        if (dt.Rows[0]["ShowFlagId"].ToString() == "1")
                        {
                            ChkIsShow.Checked = true;
                        }

                    
                    }
                }
            }
            else if (ddlSelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "DeleteSuccessfully();", true);
                mtController.DeleteMerchandise(Merchandise_ID);
                FillMerchandiseGridView();
                pnlEntry.Visible = false;
                pnlList.Visible = true;
            }
        }

        #endregion

        #region Grid Editing Related Events

        protected void BindGrid()
        {
            FillMerchandiseGridView();
        }


        #endregion

        #region Methods

        private void FillMerchandiseGridView()
        {
            using (DataTable dt = mtController.GetAllMerchandise())
            {
                gvMerchandise.DataSource = dt;
                gvMerchandise.DataBind();
            }
        }

        private void ClearData()
        {

            txtMerchandiseTitle.Text = "";
            txtMerchandiseDescription.Text = "";

        }

        private void BindMerchandiseType()
        {
          
            using (  DataTable dt =new clsMerchandiseTypeController().GetAllMerchandiseType ())
            {
                if (dt.Rows.Count > 0)
                {
                    ddlMerchandiseType.DataSource =dt;
                    ddlMerchandiseType.DataTextField = "MerchandiseType";
                    ddlMerchandiseType.DataValueField = "MerchandiseTypeId";
                    ddlMerchandiseType.DataBind();
                }
                ddlMerchandiseType.Items.Insert(0, new ListItem("--Select--", "0"));
            }

           
        }

        #endregion

        #region Button Clicks

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void lbGo_Click(object sender, EventArgs e)
        {
            FillMerchandiseGridView();
        }

        protected void btnSaveMerchandise_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MerchandiseSaveSuccessfully();", true);


                if (!string.IsNullOrEmpty(ddlMerchandiseType.SelectedValue))
                {
                    mtClass.MerchandiseTypeId = Convert.ToInt32( ddlMerchandiseType.SelectedValue);
                
                }
                if (!string.IsNullOrEmpty(txtPrice.Text))
                {
                    mtClass.MerchandisePrice = Convert.ToInt32(txtPrice.Text);

                }
                mtClass.MerchandiseTitle = txtMerchandiseTitle.Text;
                mtClass.MerchandiseDesc = txtMerchandiseDescription.Text;
                mtClass.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
                mtClass.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);
                
                mtClass.PortalID = currentUser.PortalID;
                mtClass.CreatedById = currentUser.Username;
                mtClass.ModifiedById = currentUser.Username;

                mtController.InsertMerchandise(mtClass);

                pnlList.Visible = true;
                pnlEntry.Visible = false;
                FillMerchandiseGridView();
                ClearData();
            }
        }

        protected void btnAddMerchandise_Click(object sender, EventArgs e)
        {
            ClearData();
            pnlEntry.Visible = true;
            pnlList.Visible = false;
            btnSaveMerchandise.Visible = true;
            btnUpdateMerchandise.Visible = false;
            BindMerchandiseType();

        }

        protected void btnCloseMerchandise_Click(object sender, EventArgs e)
        {
            pnlList.Visible = true;
            pnlEntry.Visible = false;
            FillMerchandiseGridView();
        }

        protected void btnUpdateMerchandise_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "MerchandiseUpdateSuccessfully()", true);


            mtClass.MerchandiseId = Merchandise_ID;
            if (!string.IsNullOrEmpty(ddlMerchandiseType.SelectedValue))
            {
                mtClass.MerchandiseTypeId = Convert.ToInt32(ddlMerchandiseType.SelectedValue);

            }
            if (!string.IsNullOrEmpty(txtPrice.Text))
            {
                mtClass.MerchandisePrice = Convert.ToInt32(txtPrice.Text);

            }
            mtClass.MerchandiseTitle = txtMerchandiseTitle.Text;
            mtClass.MerchandiseDesc = txtMerchandiseDescription.Text;
            mtClass.ActiveFlagId = Convert.ToInt32(ChkIsActive.Checked);
            mtClass.ShowFlagId = Convert.ToInt32(ChkIsShow.Checked);

            mtClass.PortalID = currentUser.PortalID;
            mtClass.CreatedById = currentUser.Username;
            mtClass.ModifiedById = currentUser.Username;

            mtController.UpdateMerchandise(mtClass);

            pnlEntry.Visible = false;
            pnlList.Visible = true;
            FillMerchandiseGridView();
            ClearData();
        }


        #endregion

    }
}
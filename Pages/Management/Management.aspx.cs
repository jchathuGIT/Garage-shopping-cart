using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Management_Management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdProductTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Retrieve the Id of the product from the management page and allow to edit in the manage product page page
        //Get selected row
        GridViewRow row = grdProducts.Rows[e.NewEditIndex];
        
        //Get Id of selected products
        int rowId = Convert.ToInt32(row.Cells[1].Text);

        //Redirect user to manage product along with the selected rowId
        Response.Redirect("~/Pages/management/ManageProducts.aspx?id=" + rowId);

    }
}
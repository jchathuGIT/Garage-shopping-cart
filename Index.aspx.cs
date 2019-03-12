using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillPage();
    }

    private void FillPage()
    {
        ProductModel productModel = new ProductModel();
        List<Product> products = productModel.GetAllPrpducts();

        //Make sure the products exist in the DB
        if (products != null)
        {
            //Creating a new panel with an ImageButton and 2 labels for each Product
            foreach (Product product in products)
            {
                Panel productPanel = new Panel();
                ImageButton imageButton = new ImageButton();
                Label lblName = new Label();
                Label lblPrice = new Label();

                //Child Control's properties
                imageButton.ImageUrl = "~/Images/Products/" + product.Image;
                imageButton.CssClass = "productImage";
                imageButton.PostBackUrl = string.Format("~/Pages/Product.aspx?id={0}" , product.Id);

                lblName.Text = product.Name;
                lblName.CssClass = "productName";

                lblPrice.Text = "$" + product.Price;
                lblPrice.CssClass = "productPrice";

                //Add child controls to panel
                productPanel.Controls.Add(imageButton);
                productPanel.Controls.Add(new Literal { Text = "<br />" });
                productPanel.Controls.Add(lblName);
                productPanel.Controls.Add(new Literal { Text = "<br />" });
                productPanel.Controls.Add(lblPrice);

                //add dynamic panel to parent panel
                pnlProducts.Controls.Add(productPanel);

            }
        }
        else
        { 
            //no products found
            pnlProducts.Controls.Add(new Literal {Text = "No products found" });
        }
    }
}
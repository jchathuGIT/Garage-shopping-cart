using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
//using Models;


public partial class Pages_ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Get Id of the current logged in user
        string userId = User.Identity.GetUserId();

        //Display all items in user's cart.
        GetPurchasesInCart(userId);        

    }

    private void GetPurchasesInCart(string userId)
    {
        CartModel cartModel = new CartModel();
        double subTotal = 0;

        //Get all purchases for current user and display in table
        List<Cart> purchaseList = cartModel.GetOrdersInCart(userId);
        CreateShopTable(purchaseList, out subTotal);     //out parameter - allows the method to return a second return value

        //Add totals to webpage
        double vat = subTotal * 0.25;
        double totalAmount = subTotal + 15 + vat;

        litTotal.Text = "$ " + subTotal;
        litVat.Text = "$ " + vat;
        litTotalAmount.Text = "$ " + totalAmount;

    }

    private void CreateShopTable(List<Cart> purchaseList, out double subTotal)
    {
        subTotal = new double();
        ProductModel model = new ProductModel();
        
        foreach (Cart cart in purchaseList)
        {
            //Create HTML elements and fill values with database data
            Product product = model.GetProduct(cart.ProductID);

            //Create the Image button
            ImageButton btnImage = new ImageButton
            {
                ImageUrl = string.Format("~/Images/Products/{0}", product.Image),
                PostBackUrl = string.Format("~/Pages/Product.aspx?id={0}", product.Id)
            };

            //Create the delete link
            LinkButton lnkDelete = new LinkButton
            {
                PostBackUrl = string.Format("~/Pages/ShoppingCart.aspx?productId={0}", cart.ID),
                Text = "Delete Item",
                ID = "del" + cart.ID,
            };

            //Add an OnClick event
            lnkDelete.Click += Delete_Item;
            
            
            //Fill amount list with numbers 1-20
            int[] amount = Enumerable.Range(1, 20).ToArray();
            //Create the 'Quantity' dropdownlist
            DropDownList ddlAmount = new DropDownList
            {
                DataSource = amount,
                AppendDataBoundItems = true,
                AutoPostBack = true,        //when selecting a new value from the quantity, the page will automatically refreshes and calculate the total
                ID = cart.ID.ToString()
            };
            ddlAmount.DataBind();
            ddlAmount.SelectedValue = cart.Amount.ToString();
            ddlAmount.SelectedIndexChanged += ddlAmount_SelectedIndexChanged;

            //Create table to hold shopping cart details
            //Create HTML table with 2 rows
            Table table = new Table { CssClass = "CartTable" };
            TableRow row1 = new TableRow();
            TableRow row2 = new TableRow();

            //Create 6 cells for row1
            TableCell cell1_1 = new TableCell { RowSpan = 2, Width = 50 };
            TableCell cell1_2 = new TableCell
            {
                Text = string.Format("<h4>{0}</h4><br />{1}<br/>In Stock",
                    product.Name, "Item No:" + product.Id),
                HorizontalAlign = HorizontalAlign.Left,
                Width = 350,
            };
            TableCell cell1_3 = new TableCell { Text = "Unit Price<hr/>" };
            TableCell cell1_4 = new TableCell { Text = "Quantity<hr/>" };
            TableCell cell1_5 = new TableCell { Text = "Item Total<hr/>" };
            TableCell cell1_6 = new TableCell();

            //Create 6 cells for row2
            TableCell cell2_1 = new TableCell();
            TableCell cell2_2 = new TableCell { Text = "$ " + product.Price };
            TableCell cell2_3 = new TableCell();
            TableCell cell2_4 = new TableCell { Text = "$ " + Math.Round(Convert.ToDecimal(cart.Amount * product.Price), 2) };
            TableCell cell2_5 = new TableCell();
            TableCell cell2_6 = new TableCell();


            //Set custom controls
            cell1_1.Controls.Add(btnImage);
            cell1_6.Controls.Add(lnkDelete);
            cell2_3.Controls.Add(ddlAmount);

            //Add cells to rows
            row1.Cells.Add(cell1_1);
            row1.Cells.Add(cell1_2);
            row1.Cells.Add(cell1_3);
            row1.Cells.Add(cell1_4);
            row1.Cells.Add(cell1_5);
            row1.Cells.Add(cell1_6);

            row2.Cells.Add(cell2_1);
            row2.Cells.Add(cell2_2);
            row2.Cells.Add(cell2_3);
            row2.Cells.Add(cell2_4);
            row2.Cells.Add(cell2_5);
            row2.Cells.Add(cell2_6);

            //Add rows to table
            table.Rows.Add(row1);
            table.Rows.Add(row2);

            //Add table to shopping panel
            pnlShoppingCart.Controls.Add(table);

            //Add total amount of item in cart to subtotal
            subTotal += Convert.ToInt32(cart.Amount * product.Price);
        }

        //Add selected objects to Session
        Session[User.Identity.GetUserId()] = purchaseList;
        
    }

    private void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList selectedList = (DropDownList)sender;
        int cartId = Convert.ToInt32(selectedList.ID);
        int quantity = Convert.ToInt32(selectedList.SelectedValue);

        //Update purchase with new quantity and refresh page
        CartModel cartModel = new CartModel();
        cartModel.UpdateQuantity(cartId, quantity);
        Response.Redirect("~/Pages/ShoppingCart.aspx");

    }

    private void Delete_Item(object sender, EventArgs e)
    {
        //Get ID of product that has had its quantity dropdownlist changed.

        LinkButton selectedLink = (LinkButton)sender;
        string link = selectedLink.ID.Replace("del", "");
        int cartId = Convert.ToInt32(link);
        //DropDownList selectedList = (DropDownList)sender;
        //int cartId = Convert.ToInt32(selectedList.ID);
        //int quantity = Convert.ToInt32(selectedList.SelectedValue);

        //Update purchase with new quantity and refresh page
        CartModel cartModel = new CartModel();
        cartModel.DeleteCart(cartId);

        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }
}
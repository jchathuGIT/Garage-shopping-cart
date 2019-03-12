using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.IO;

public partial class Pages_Management_ManageProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            GetImages();
        //To add a check whether an Id parameter is present in the page_load method
            if (!String.IsNullOrWhiteSpace(Request.QueryString["id"])) 
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                FillPage(id);
            }
        }
    }

    private void FillPage(int id)
    { 
        //Get selected product from DB
        ProductModel productModel = new ProductModel();
        Product product = productModel.GetProduct(id);

        //Fill textboxes
        txtDescription.Text = product.Description;
        txtName.Text = product.Name;
        txtPrice.Text = product.Price.ToString();

        //To set dropdown values
        ddImage.SelectedValue = product.Image;
        ddType.SelectedValue = product.TypeId.ToString();
    }

    public void GetImages()
    {
        try
        {   //To get all filepaths
            string[] images = Directory.GetFiles(Server.MapPath("~/Images/Products/"));

            //Get all filenames and add to an array
            ArrayList imageList = new ArrayList();

            foreach (string image in images)
            {
                string imageName = image.Substring(image.LastIndexOf(@"\",StringComparison.Ordinal) + 1);
                imageList.Add(imageName);

                //To set the arraylist as the dropdown data sourse and refresh
                ddImage.DataSource = imageList;
                ddImage.AppendDataBoundItems = true;
                ddImage.DataBind();
            }
        }
        catch (Exception e)
        {

            lblResult.Text = e.ToString();
        }
    }

    private Product CreateProduct()
    {
        Product product = new Product();

        product.Name = txtName.Text;
        product.Price = Convert.ToInt32(txtPrice.Text);
        product.TypeId = Convert.ToInt32(ddType.SelectedValue);
        product.Description = txtDescription.Text;
        product.Image = ddImage.SelectedValue;

        return product;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductModel productModel = new ProductModel();
        Product product = CreateProduct();

        //check if the URL contains an ID parameter
        if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            //Id exist -> update existing row
            int id = Convert.ToInt32(Request.QueryString["id"]);
            lblResult.Text = productModel.UpdateProduct(id, product);

        }
        else
        { 
            //Id does not exist -> Create a new row
            lblResult.Text = productModel.InsertProduct(product);

        }

    }
}
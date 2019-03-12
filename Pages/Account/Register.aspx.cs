using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
//using Models;

public partial class Pages_Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            //UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();

            //Set ConnectionString to GarageConnectionString
            userStore.Context.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GarageDBConnectionString"].ConnectionString;
            
            var manager = new UserManager<IdentityUser>(userStore);   

            //Create new user and try to store in DB.
            IdentityUser user = new IdentityUser();   
            user.UserName = txtUserName.Text;
            //var user = manager.Find(txtUserName.Text, txtPassword.Text);

            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                try
                {   //Create user object
                    //DB will be created and will expand it automatically
                    IdentityResult result = manager.Create(user, txtPassword.Text);
                    if (result.Succeeded)
                    {
                        UserInformation info = new UserInformation
                        {
                            Address = txtAddress.Text,
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            //Guid = user.Id,
                            PostalCode = Convert.ToInt32(txtPostalCode.Text)
                        };

                        //UserInfoModel model = new UserInfoModel();
                        //model.InsertUserInformation(info);



                        //Store user in DB
                        var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                        //set to log in new user by cookie
                        var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                        //If succeedeed, log in the new user and redirect to homepage
                        authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                        Response.Redirect("~/Index.aspx");
                    }
                    else
                    {
                        litStatus.Text = result.Errors.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    litStatus.Text = ex.ToString();
                }
            }
            else
            {
                litStatus.Text = "Passwords must match!";
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartModel
/// </summary>
public class CartModel
{
    public string InsertCart(Cart cart)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            db.Carts.Add(cart);       //Will add the carts from the DB
            db.SaveChanges();       // Save changes to the db entity.

            return cart.DatePurchased + "Was successfully Inserted...";
        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }


    public string UpdateCart(int id, Cart cart)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            //To fetch carts from database
            Cart p = db.Carts.Find(id);   //find will return the entity with the given primary key values

            //To replace the data stored in object p by the data given in 'cart'.
            //We can actually update only the parameters in the 'carts' 
            p.DatePurchased = cart.DatePurchased;
            p.ClientID = cart.ClientID;
            p.Amount = cart.Amount;
            p.IsInCart = cart.IsInCart;
            p.ProductID = cart.ProductID;

            db.SaveChanges();
            return cart.DatePurchased + "was successfully updated...";

        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }



    public string DeleteCart(int id) // Delete from the primary key value
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            //First to find the objects to be deleted
            Cart cart = db.Carts.Find(id);

            db.Carts.Attach(cart);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return cart.DatePurchased + "was successfully Deleted...";
        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }

    public List<Cart> GetOrdersInCart(string clientId)
    {       //Get all items of the current user's shopping cart
        GarageDBEntities db = new GarageDBEntities();
        List<Cart> orders = (from x in db.Carts
                             where x.ClientID == clientId
                             && x.IsInCart
                             orderby x.DatePurchased descending
                             select x).ToList();
        return orders;
    }

    public int GetAmountOfOrders(string clientId)
    {       //Return the total amount of items in the shopping cart
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            int amount = (from x in db.Carts
                          where x.ClientID == clientId
                          && x.IsInCart
                          select x.Amount).Sum();

            return amount;
        }
        catch
        {
            return 0;
        }
    }

    public void UpdateQuantity(int id, int quantity)
    {       //Update the quantity of the selected product
        GarageDBEntities db = new GarageDBEntities();
        Cart p = db.Carts.Find(id);
        p.Amount = quantity;

        db.SaveChanges();
    }

    public void MarkOrdersAsPaid(List<Cart> carts)
    {       //List of cart objects to mark as paid
        GarageDBEntities db = new GarageDBEntities();

        if (carts != null)
        {
            foreach (Cart ct in carts)
            {
                Cart oldCart = db.Carts.Find(ct.ID);
                oldCart.DatePurchased = DateTime.Now;
                oldCart.IsInCart = false;
            }
            db.SaveChanges();
        }
    }


}
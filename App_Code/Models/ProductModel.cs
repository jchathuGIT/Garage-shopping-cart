using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductModel
/// </summary>
public class ProductModel
{
    public string InsertProduct(Product product)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            db.Products.Add(product);       //Will add the products from the DB
            db.SaveChanges();       // Save changes to the db entity.

            return product.Name + "Was successfully Inserted...";
        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }


    public string UpdateProduct(int id,Product product)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            //To fetch products from database
            Product p = db.Products.Find(id);   //find will return the entity with the given primary key values

            //To replace the data stored in object p by the data given in 'product'.
            //We can actually update only the parameters in the 'products' 
            p.Name = product.Name;
            p.Price = product.Price;
            p.TypeId = product.TypeId;
            p.Description = product.Description;
            p.Image = product.Image;

            db.SaveChanges();
            return product.Name + "was successfully updated...";

        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }



    public string DeleteProduct(int id) // Delete from the primary key value
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            //First to find the objects to be deleted
            Product product = db.Products.Find(id);

            db.Products.Attach(product);
            db.Products.Remove(product);
            db.SaveChanges();
            return product.Name + "was successfully Deleted...";


        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }

    public Product GetProduct(int id)
    {
        try
        {
            using (GarageDBEntities db = new GarageDBEntities())
            {
                Product product = db.Products.Find(id);
                return product;
            }
        }
        catch (Exception)
        {

            return null;
        }
    }

    public List<Product> GetAllPrpducts()
    {
        try
        {           //Using LINQ... To retrieve all data from the DB
            using (GarageDBEntities db = new GarageDBEntities())
            {
                List<Product> products = (from x in db.Products
                                          select x).ToList();
                return products; 
            }
        }
        catch (Exception)
        {
            
            return null;
        }
    }

    public List<Product> GetProductsByType(int typeId)
    {           //To return typeId of the input parameter of the selected type
        try
        {          
            using (GarageDBEntities db = new GarageDBEntities())
            {
                List<Product> products = (from x in db.Products  
                                          where x.TypeId == typeId
                                          select x).ToList();
                return products;
            }
        }
        catch (Exception)
        {

            return null;
        }
        
    }
}
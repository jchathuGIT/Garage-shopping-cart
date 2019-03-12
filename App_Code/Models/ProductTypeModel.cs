using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductTypeTypeModel
/// </summary>
public class ProductTypeTypeModel
{
    public string InsertProductType(ProductType productType)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            db.ProductTypes.Add(productType);       //Will add the productTypes from the DB
            db.SaveChanges();       // Save changes to the db entity.

            return productType.Name + "Was successfully Inserted...";
        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }


    public string UpdateProductType(int id, ProductType productType)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            //To fetch productTypes from database
            ProductType p = db.ProductTypes.Find(id);   //find will return the entity with the given primary key values

            //To replace the data stored in object p by the data given in 'productType'.
            //We can actually update only the parameters in the 'productTypes' 
            p.Name = productType.Name;

            db.SaveChanges();
            return productType.Name + "was successfully updated...";

        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }



    public string DeleteProductType(int id) // Delete from the primary key value
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            //First to find the objects to be deleted
            ProductType productType = db.ProductTypes.Find(id);

            db.ProductTypes.Attach(productType);
            db.ProductTypes.Remove(productType);
            db.SaveChanges();
            return productType.Name + "was successfully Deleted...";


        }
        catch (Exception e)
        {

            return "Error :" + e;
        }
    }
}
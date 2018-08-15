using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class ProductRepository : IRepository<Product>
    {
        DataContext ProductDB = new DataContext();

        public IEnumerable<Product> GetAll()
        {
            // TO DO : Code to get the list of all the records in database
            return ProductDB.Products;
        }

        public Product Get(int id)
        {
            // TO DO : Code to find a record in database
            return ProductDB.Products.Find(id);
        }

        public Product Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to save record into database
            ProductDB.Products.Add(item);
            ProductDB.SaveChanges();
            return item;
        }

        public bool Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database

            var products = ProductDB.Products.Single(a => a.Id == item.Id);
            products.Name = item.Name;
            products.Category = item.Category;
            products.Price = item.Price;
            products.Quantity = item.Quantity;
            ProductDB.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database

            Product products = ProductDB.Products.Find(id);
            ProductDB.Products.Remove(products);
            ProductDB.SaveChanges();

            return true;
        }
    }
}
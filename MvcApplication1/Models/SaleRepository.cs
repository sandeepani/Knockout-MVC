using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace MvcApplication1.Models
{
    public class SaleRepository : IRepository<Sale>
    {
        DataContext DB = new DataContext();

        public IEnumerable<Sale> GetAll()
        {
            // TO DO : Code to get the list of all the records in database
            return DB.Sales;
        }

        public Sale Get(int id)
        {
            // TO DO : Code to find a record in database
            return DB.Sales.Find(id);
        }

        public Sale Add(Sale item)
        {
            IRepository<Product> repository = new ProductRepository();
            using (var trans = new TransactionScope(
                           TransactionScopeOption.Required,
                           new TransactionOptions
                           {
                               IsolationLevel = IsolationLevel.ReadCommitted
                           },
                           EnterpriseServicesInteropOption.Automatic))
            {
                // TO DO : Code to save record into database
                DB.Sales.Add(item);
                DB.SaleDetails.AddRange(item.SaleDetails);
                DB.SaveChanges();
                foreach (var x in item.SaleDetails)
                {
                    var product = DB.Products.Single(y => y.Id == x.ProductId);
                    product.Quantity = product.Quantity - x.Quantity;
                    repository.Update(product);
                }
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            
            return item;
        }

        public bool Update(Sale item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database

            var Sales = DB.Sales.Single(a => a.Id == item.Id);
            Sales.CreatedDate = item.CreatedDate;
            Sales.GrossAmount = item.GrossAmount;
            Sales.SaleDetails = item.SaleDetails;
            DB.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            // TO DO : Code to remove the records from database

            Sale Sales = DB.Sales.Find(id);
            DB.Sales.Remove(Sales);
            DB.SaveChanges();

            return true;
        }
    }
}
using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class SaleController : Controller
    {

        static readonly IRepository<Sale> repository = new SaleRepository();
        static readonly IRepository<Product> prodRepository = new ProductRepository();

        public ActionResult Sales()
        {
            return View();
        }

        public JsonResult GetAllSales()
        {
            return Json(repository.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSale(Sale item)
        {
            item.CreatedDate = DateTime.Now;
            item = repository.Add(item);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditSale(int id, Sale Sale)
        {
            Sale.Id = id;
            if (repository.Update(Sale))
            {
                return Json(repository.GetAll(), JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        public JsonResult DeleteSale(int id)
        {

            if (repository.Delete(id))
            {
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = false }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAllProducts()
        {
            return Json(prodRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }

    }
}

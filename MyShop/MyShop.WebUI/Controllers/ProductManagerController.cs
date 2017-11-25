using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System.ComponentModel;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: Product
        public ActionResult Index()
        {

            List<Product> products = context.Collection().ToList();
            return View(products);
        }



        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
                return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, String Id)
        {
            Product producttoEdit = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    return View(product);

                }
                producttoEdit.Category = product.Category;
                producttoEdit.Description = product.Description;
                producttoEdit.Name = product.Name;
                producttoEdit.Image = product.Image;
                producttoEdit.Price = product.Price;
                context.Commit();
                return RedirectToAction("Index");
            }


        }


        public ActionResult Delete(string Id) {

            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);

            }

        }

        [ActionName("Delete")]
        [HttpPost]        
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }


    }
}
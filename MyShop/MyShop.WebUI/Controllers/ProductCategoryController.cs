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
    public class ProductCategoryController : Controller
    {
        InMemoryRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductCategory
        public ActionResult Index()
        {

            List<ProductCategory> productcategories = context.Collection().ToList();
            return View(productcategories);
        }



        public ActionResult Create()
        {
            ProductCategory productcategory = new ProductCategory();
            return View(productcategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {

            if (!ModelState.IsValid)
            {
                return View(productcategory);
            }
            else
            {
                context.Insert(productcategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(string Id)
        {
            ProductCategory productcategory = context.Find(Id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            else
                return View(productcategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory, String Id)
        {
            ProductCategory productcategorytoEdit = context.Find(Id);
            if (productcategorytoEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productcategory);

                }
                productcategorytoEdit.Category = productcategory.Category;
                //productcategorytoEdit.Description = product.Description;
                //productcategorytoEdit.Name = product.Name;
                //productcategorytoEdit.Image = product.Image;
                //productcategorytoEdit.Price = product.Price;
                context.Commit();
                return RedirectToAction("Index");
            }


        }


        public ActionResult Delete(string Id)
        {

            ProductCategory productcategoryToDelete = context.Find(Id);
            if (productcategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcategoryToDelete);

            }

        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productcategoryToDelete = context.Find(Id);
            if (productcategoryToDelete == null)
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
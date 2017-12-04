using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> productcategories;

        public ProductCategoryRepository()
        {
            productcategories = cache["productcategories"] as List<ProductCategory>;
            if (productcategories == null)
            {
                productcategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {

            cache["productcategories"] = productcategories;
        }

        public void Insert(ProductCategory pc)
        {

            productcategories.Add(pc);

        }

        public void Update(ProductCategory productcategory)
        {
            ProductCategory productcategoryToUpdate = productcategories.Find(pc => pc.Id == productcategory.Id);
            if (productcategoryToUpdate != null)
            {
                productcategory = productcategoryToUpdate;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productcategory = productcategories.Find(pc => pc.Id == Id);
            if (productcategory != null)
            {
                return productcategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }

        // returning a list of Queryable products
        public IQueryable<ProductCategory> Collection()
        {
            return productcategories.AsQueryable();
        }

        public void Delete(string Id)
        {

            ProductCategory productcategoryToDelete = productcategories.Find(pc => pc.Id == Id);
            if (productcategoryToDelete != null)
            {
                productcategories.Remove(productcategoryToDelete);

            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }

    }
}

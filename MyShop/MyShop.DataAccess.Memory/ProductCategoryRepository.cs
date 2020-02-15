using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.Memory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory pc)
        {
            productCategories.Add(pc);
        }

        public void Update(ProductCategory productCat)
        {
            ProductCategory productCatToUpdate = productCategories.Find(pc => pc.Id == productCat.Id);

            if (productCatToUpdate != null)
            {
                productCatToUpdate = productCat;
            }
            else
            {
                throw new Exception("Categoria de producto no encontrada");
            }


        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCat = productCategories.Find(pc => pc.Id == id);

            if (productCat != null)
            {
                return productCat;
            }
            else
            {
                throw new Exception("Categoria no encontrada");
            }
        }


        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Categoria no encontrada");
            }


        }
    }
}

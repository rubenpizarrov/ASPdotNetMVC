using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.Memory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> productCategoryContext;

        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            productCategoryContext = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create() 
        {
            ProductMangerViewModel viewModel = new ProductMangerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategoryContext.Collection();

            return View(viewModel);
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

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductMangerViewModel viewModel = new ProductMangerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategoryContext.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id) 
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(string id) 
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id) 
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}
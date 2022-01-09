using InventoryTracker.Models;
using InventoryTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Controllers
{
    public class InventoryController : Controller
    {
        public IActionResult Index()    //List of Inventory
        {
            ProductsDAO products = new ProductsDAO();

            return View(products.GetAllProducts());
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult ProcessAdd(ProductModel productModel)
        {
            SecurityService securityService = new SecurityService();
            ProductsDAO products = new ProductsDAO();

            if (securityService.IsValidProduct(productModel))
            {
                products.Add(productModel);
                return View("AddSuccess", productModel);
            }
            else
            {
                return View("AddFailure", productModel);
            }

        }

        public IActionResult SearchAR(string searchTerm)    
        {
            ProductsDAO products = new ProductsDAO();
            List<ProductModel> productList = products.SearchProducts(searchTerm);

            return View("Index", productList);
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult ShowDetails(int id)
        {
            ProductsDAO products = new ProductsDAO();
            ProductModel foundProduct = products.GetProductById(id);
            return View("Details",foundProduct);
        }

        public IActionResult Edit(int id)
        {
            ProductsDAO products = new ProductsDAO();
            ProductModel foundProduct = products.GetProductById(id);
            return View("Edit",foundProduct);
        }

        public IActionResult ProcessEdit(ProductModel productModel)
        {
            ProductsDAO products = new ProductsDAO();
            products.Update(productModel);
            return View("Index", products.GetAllProducts());
        }

        public IActionResult Delete(int Id)
        {
            ProductsDAO products = new ProductsDAO();
            ProductModel productModel = products.GetProductById(Id);
            products.Delete(productModel);
            return View("Index", products.GetAllProducts());
        }
    }
}

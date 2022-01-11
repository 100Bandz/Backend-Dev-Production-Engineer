using InventoryTracker.Models;
using InventoryTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public IActionResult ExportToCSV()
        {
            ProductsDAO products = new ProductsDAO();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Id,Name,Quantity,Price,Description,Date");
            foreach (ProductModel product in products.GetAllProducts())
            {
                stringBuilder.AppendLine($"{product.Id},{product.Name},{product.Quantity},{product.Price},{product.Description},{product.Date}");
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv","products.csv");
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

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult ProcessSearch(string searchTerm)    
        {
            ProductsDAO products = new ProductsDAO();
            List<ProductModel> productList = products.SearchProducts(searchTerm);

            if (productList.Count == 0)
            {
                return View("SearchFailure");
            }
            else
            {
                return View("Index", productList);
            }
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

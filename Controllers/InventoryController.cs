using InventoryTracker.Models;
using InventoryTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Controllers
{
    public class InventoryController : Controller
    {

        private readonly IConfiguration _configuration; //Setting up retrival of config values

        public InventoryController(IConfiguration config)
        {
            this._configuration = config;
        }

        public IActionResult Index()    //Action to return the entire inventory using the View "Index"
        {
            //string conn = _configuration.GetConnectionString("Default");
            var testserver = Environment.GetEnvironmentVariable("Server");
            var testdb = Environment.GetEnvironmentVariable("Database");
            var testuid = Environment.GetEnvironmentVariable("User ID");
            var testpwd = Environment.GetEnvironmentVariable("Password");

            string conn = string.Format("Server={0};Database={1};User Id={2};Password={3};", testserver, testdb, testuid, testpwd);

            ProductsDAO products = new ProductsDAO(conn);

            return View("Index",products.GetAllProducts());
        }

        public IActionResult ExportToCSV()  //Action to Export All data in the inventory to a CSV file
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Name,Quantity,Price,Description,Date");
            foreach (ProductModel product in products.GetAllProducts())
            {
                stringBuilder.AppendLine($"{product.Name},{product.Quantity},{product.Price},{product.Description},{product.Date}");
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv","products.csv");
        }

        public IActionResult Add()  //Action to Add by returning the "Add" View
        {
            return View("Add");
        }

        public IActionResult ProcessAdd(ProductModel productModel)  /*Action to process adding products by checking if a product of 
                                                                     *the same name was already present*/
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);

            if (products.IsValidProduct(productModel))
            {
                products.Add(productModel);
                return View("AddSuccess", productModel);
            }
            else
            {
                return View("AddFailure", productModel);
            }

        }

        public IActionResult Search()   //Action to Search by returning the "Search" View
        {
            return View("Search");
        }

        public IActionResult ProcessSearch(string searchTerm)   /*Action to process searching for a product by checking if the returned
                                                                 *list is empty or not*/
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);
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

        public IActionResult ProcessDetails(int id) /*Action to process showing the details of a product by first validating that
                                                     * the given id gives a productmodel object that isn't null*/
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);
            ProductModel foundProduct = products.GetProductById(id);
            if (foundProduct == null)
            {
                return View("DetailsFailure");
            }
            else
            {
                return View("Details", foundProduct);
            }
        }

        public IActionResult Edit(int id)   /*Action to Edit by returning the "Edit" View and the found product*/
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);
            ProductModel foundProduct = products.GetProductById(id);
            return View("Edit",foundProduct);
        }

        public IActionResult ProcessEdit(ProductModel productModel) /*Action to process editing a given product, updating it and then
                                                                     * returning the "Index" View*/
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);

            products.Update(productModel);
            return View("Index", products.GetAllProducts());

        }

        public IActionResult Delete(int Id) /*Action to Delete by returning the "Delete" View and the "Index" View*/
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);
            ProductModel productModel = products.GetProductById(Id);
            products.Delete(productModel);
            return View("Index",products.GetAllProducts());
        }

        public IActionResult ProcessDelete(ProductModel productModel)   /*Action to process deleting an item by allowing the user to
                                                                         * confirm their action as well as display the product's details*/
        {
            string conn = _configuration.GetConnectionString("Default");
            ProductsDAO products = new ProductsDAO(conn);
            products.Delete(productModel);
            return View("Index", products.GetAllProducts());
        }
    }
}

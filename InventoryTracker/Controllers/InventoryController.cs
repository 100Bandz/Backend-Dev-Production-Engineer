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
        public IActionResult Index()    //Action to return the entire inventory using the View "Index"
        {
            ProductsDAO products = new ProductsDAO();

            return View("Index",products.GetAllProducts());
        }

        public IActionResult ExportToCSV()  //Action to Export All data in the inventory to a CSV file
        {
            ProductsDAO products = new ProductsDAO();

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

        public IActionResult Search()   //Action to Search by returning the "Search" View
        {
            return View("Search");
        }

        public IActionResult ProcessSearch(string searchTerm)   /*Action to process searching for a product by checking if the returned
                                                                 *list is empty or not*/
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

        public IActionResult ProcessDetails(int id) /*Action to process showing the details of a product by first validating that
                                                     * the given id gives a productmodel object that isn't null*/
        {
            ProductsDAO products = new ProductsDAO();
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
            ProductsDAO products = new ProductsDAO();
            ProductModel foundProduct = products.GetProductById(id);
            return View("Edit",foundProduct);
        }

        public IActionResult ProcessEdit(ProductModel productModel) /*Action to process editing a given product, updating it and then
                                                                     * returning the "Index" View*/
        {
            ProductsDAO products = new ProductsDAO();
            products.Update(productModel);
            return View("Index", products.GetAllProducts());
        }

        public IActionResult Delete(int Id) /*Action to Delete by returning the "Delete" View and the "Index" View*/
        {
            ProductsDAO products = new ProductsDAO();
            ProductModel productModel = products.GetProductById(Id);
            products.Delete(productModel);
            return View("Delete",productModel);
        }

        public IActionResult ProcessDelete(ProductModel productModel)   /*Action to process deleting an item by allowing the user to
                                                                         * confirm their action as well as display the product's details*/
        {
            ProductsDAO products = new ProductsDAO();
            products.Delete(productModel);
            return View("Index", products.GetAllProducts());
        }
    }
}

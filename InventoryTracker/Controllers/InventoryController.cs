using InventoryTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Controllers
{
    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessAdd(ProductModel productModel)
        {
            if (productModel.Name == "Nico" && productModel.Quantity == 25)
            {
                return View("AddSuccess", productModel);
            }
            else
            {
                return View("AddFailure", productModel);
            }

        }

    }
}

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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessAdd(ProductModel productModel)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValidProduct(productModel))
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

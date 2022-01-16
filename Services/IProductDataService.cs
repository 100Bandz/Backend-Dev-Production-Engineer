using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Services
{
    interface IProductDataService   //Basic Interface that defines a contract for use by any class or struct
    {
        List<ProductModel> GetAllProducts();
        List<ProductModel> SearchProducts(string searchTerm);
        bool SearchName(string searchName);
        ProductModel GetProductById(int id);
        int Add(ProductModel product);
        int Delete(ProductModel product);
        int Update(ProductModel product);
    }
}

using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Services
{
    public class SecurityService
    {
        ProductsDAO productDAO = new ProductsDAO();

        public SecurityService()
        {

        }

        public bool IsValidProduct(ProductModel product)    //Checks if an item by the same name is already in the database
        {
            //bool isNullOrEmpty = productDAO.SearchProducts(product.Name)?.Any() != true;
            //return isNullOrEmpty;

            if (productDAO.SearchProducts(product.Name)?.Any() != true)
            {
                return true;
            }

            foreach (ProductModel item in productDAO.SearchProducts(product.Name)) // Loop through List with foreach
            {
                if (item.Name == product.Name)
                {
                    return false;
                }
            }
            return true;

        }
    }
}

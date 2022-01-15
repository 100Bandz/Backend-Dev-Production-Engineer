using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Services
{
    public class SecurityService    //A class for security and input/data validation
    {
        ProductsDAO productDAO = new ProductsDAO();
        public SecurityService()
        {

        }

        public bool IsValidProduct(ProductModel product)    /*Method that checks if an item by the same name is
                                                            *already in the database*/
        {
            if (productDAO.SearchName(product.Name))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}

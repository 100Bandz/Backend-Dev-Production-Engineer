using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Services
{
    public class ProductsDAO
    {
        string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Inventory;
             Integrated Security=True;" + "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public bool FindProductByNameAndPrice(ProductModel product)
        {
            string SQLQuery = "SELECT * FROM dbo.Products WHERE name = @name AND price = @price");

            SqlConnection connection = new SqlConnection(ConnectionString);
        }

    }
}

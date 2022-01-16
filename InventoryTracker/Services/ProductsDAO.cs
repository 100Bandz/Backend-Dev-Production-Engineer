using InventoryTracker.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Services
{
    public class ProductsDAO : IProductDataService  //A data access object class that inherets the IproductDataService Interface
    {
        private readonly string ConnectionString = "";  //Connection String to the SQL Server
        public ProductsDAO(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int Delete(ProductModel product) //Method to delete a given product from the database using an SQL query
        {
            int newIdNumber = -1;

            string SQLQuery = "DELETE FROM dbo.Product WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(SQLQuery, connection);
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Date", product.Date);

                    try
                    {
                        connection.Open();
                        newIdNumber = Convert.ToInt32(command.ExecuteScalar()); //Returns first column of the first row

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return newIdNumber;
        }

        public List<ProductModel> GetAllProducts()  //Method to Get all the products from the database using an SQL query
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string SQLQuery = "SELECT * FROM dbo.Product";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SQLQuery, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Quantity = (int)reader[2], 
                                            Price = (decimal)reader[3], Description = (string)reader[4], Date = (DateTime)reader[5] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return foundProducts;
        }

        public ProductModel GetProductById(int id)  /*Method to get a product from the database by using the given id 
                                                     * using an SQL query*/
        {
            ProductModel foundProduct = null;

            string SQLQuery = "SELECT * FROM dbo.Product WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProduct = new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Quantity = (int)reader[2], 
                                      Price = (decimal)reader[3], Description = (string)reader[4], Date = (DateTime)reader[5] };
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return foundProduct;
        }

        public int Add(ProductModel product)    //Method to add a given product to the database using an SQL query
        {
            string SQLQuery = "INSERT INTO dbo.Product(Name, Quantity, Price, Description, Date) VALUES (@Name, @Quantity, @Price, @Description, @Date)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SQLQuery, connection);

                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Date", product.Date);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return 1;
        }

        public List<ProductModel> SearchProducts(string searchTerm)
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string SQLQuery = "SELECT * FROM dbo.Product WHERE Name LIKE @Name";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@Name",'%' + searchTerm + '%');

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Quantity = (int)reader[2], Price = (decimal)reader[3], Description = (string)reader[4], Date = (DateTime)reader[5] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return foundProducts;
        }

        public bool SearchName(string searchName)   /*Method to search for a product from the database using 
                                                     * the given search name and an SQL query*/
        {
            ProductModel sameNameProduct = new ProductModel();

            string SQLQuery = "SELECT * FROM dbo.Product WHERE Name LIKE @Name";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@Name", searchName);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return false;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return true;
        }

        public int Update(ProductModel product) /*Method to update the values of a given product from the database 
                                                 * using an SQL query*/
        {
            int newIdNumber = -1;

            string SQLQuery = "UPDATE dbo.Product SET Name = @Name, Quantity = @Quantity, Price = @Price, Description = @Description, Date = @Date WHERE Id = @Id";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Date", product.Date);


                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return newIdNumber;
        }

        public bool IsValidProduct(ProductModel product)    /*Method that checks if an item by the same name is
                                                            *already in the database*/
        {
            if (SearchName(product.Name))
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

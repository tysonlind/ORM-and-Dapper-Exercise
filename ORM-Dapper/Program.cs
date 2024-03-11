using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            IDbConnection connection = new MySqlConnection(connectionString);

            var repo = new DapperDepartmentRepository(connection);

            Console.WriteLine("Do you want to add a new department?");

            var addDepartment = Console.ReadLine();

            if (addDepartment == "yes")
            {
                Console.WriteLine("Type a new Department name");

                var newDepartment = Console.ReadLine();

                repo.InsertDepartment(newDepartment);

                Console.WriteLine("department added");
            }

            var departments = repo.GetAllDepartments();

            Console.WriteLine("Do you want to delete a department?");

            var deleteDepartment = Console.ReadLine();

            if (deleteDepartment == "yes")
            {


                Console.WriteLine("Here's a list of the current departments:\n");

                foreach (var department in departments)
                {
                    Console.WriteLine(department.Name);
                }

                Console.WriteLine("\nWhich department do you want to delete?");

                var delDepartment = Console.ReadLine();

                repo.DeleteDepartment(delDepartment);

                Console.WriteLine("Department was deleted.");
            }

            departments = repo.GetAllDepartments();

            Console.WriteLine("Here is the final list of departments:\n");

            //List of departments
            foreach (var department in departments)
            {
                Console.WriteLine(department.Name);
            }

            var productRepo = new DapperProductRepository(connection);

            Console.WriteLine("Do you want to add a new product?");

            var addProduct = Console.ReadLine();

            while (addProduct == "yes")
            {
                Console.WriteLine("Type a new product name");

                var newProductName = Console.ReadLine();

                Console.WriteLine("Set a price (decimal)");

                var newProductPrice = double.Parse(Console.ReadLine());

                Console.WriteLine("Set a Category ID (integer)");

                var newProductCategoryID = int.Parse(Console.ReadLine());

                productRepo.CreateProduct(newProductName, newProductPrice, newProductCategoryID);

                Console.WriteLine($"product with name {newProductName}, price {newProductPrice}, and Category ID {newProductCategoryID} added. \n Would you like to add another?");

                var answer = Console.ReadLine();

                addProduct = answer;
            }

            Console.WriteLine("Would you like to see a list of all products?");

            var listProducts = Console.ReadLine();

            if (listProducts == "yes")
            {
                var products = productRepo.GetAllProducts();
                foreach (var product in products) { Console.WriteLine(product.Name); }
            }
            Console.WriteLine("Thank you for using our dapper database management system, goodbye.");
        }
    }
}

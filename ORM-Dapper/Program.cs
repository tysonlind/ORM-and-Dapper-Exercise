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

            var repo = new DepartmentRepository(connection);

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
        }
    }
}

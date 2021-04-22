using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NETProject
{
    public class EmployeeService
    {
        private readonly string connectionString = @"Data Source=.;Initial Catalog=AdoDB;Integrated Security=True";

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using(var command = new SqlCommand("SELECT * FROM Employees", connection))
                    {
                        using (var employeesReader = command.ExecuteReader())
                        {
                            while (employeesReader.Read())
                            {
                                var employee = new Employee()
                                {
                                    Id = employeesReader.GetInt32(0),
                                    FirstName = employeesReader.GetString(1),
                                    LastName = employeesReader.GetString(2),
                                    Salary = employeesReader.GetInt32(3)
                                };
                                employees.Add(employee);
                            }
                        }
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id}) {employee.FirstName} {employee.LastName}, {employee.Salary}");
            }
            return employees;
        }

        public void UpdateEmployee()
        {
            Console.WriteLine("Which employee you want to update? Select the id");

            GetAllEmployees();
           
            var employeeId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Give FirstName");
            var firstName = Console.ReadLine();
            Console.WriteLine("Give LastName");
            var lastName = Console.ReadLine();
            Console.WriteLine("Give Salary");
            var salary = Console.ReadLine();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    var queryString = "UPDATE Employees SET FirstName = @firstname, LastName = @lastname" +
                        " WHERE Id = @employeeId";

                    using (var command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@employeeId", employeeId));
                        command.Parameters.Add(new SqlParameter("@firstname", firstName));
                        command.Parameters.Add(new SqlParameter("@lastname", lastName));
                        command.Parameters.Add(new SqlParameter("@salary", salary));

                        var rowsUpdated = command.ExecuteNonQuery();
                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Updated Successfully");
                            Console.WriteLine($"{rowsUpdated} rows upadated successfully");
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
       
        public void CreateEmployees()
        {
            Console.WriteLine("Give FirstName:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Give LastName:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Give Salary:");
            var salary = Console.ReadLine();

            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var queryString = "INSERT INTO Employees(FirstName,LastName,Salary)" +
                        "VALUES (@firstname,@lastname,@salary)";

                    using (var command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@firstname", firstName));
                        command.Parameters.Add(new SqlParameter("@lastname", lastName));
                        command.Parameters.Add(new SqlParameter("@salary", salary));

                        var rowsInserted = command.ExecuteNonQuery();
                        if (rowsInserted > 0)
                        {
                            Console.WriteLine("Insertion Successfull");
                            Console.WriteLine($"{rowsInserted} rows inserted successfully.");
                        }
                    }    
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        public void DeleteEmployee()
        {
            Console.WriteLine("Which employee you want to delete? Select the Id");

            GetAllEmployees();

            var employeeId = Convert.ToInt32(Console.ReadLine());

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var queryString = "DELETE Employees WHERE Id = @employeeId";

                    using (var command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@employeeId", employeeId));

                        var rowsUpdated = command.ExecuteNonQuery();
                        if (rowsUpdated > 0)
                        {
                            Console.WriteLine("Deleted Successfully");
                            Console.WriteLine($"{rowsUpdated} rows deleted Successfully");
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}

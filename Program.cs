using System;

namespace ADO.NETProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeService = new EmployeeService();

            //employeeService.CreateEmployees();
            //employeeService.GetAllEmployees();
            //employeeService.UpdateEmployee();
            employeeService.DeleteEmployee();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            //Console.WriteLine(GetEmployeesFullInformation(context));
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
            //Console.WriteLine(AddNewAddressToEmployee(context));
            Console.WriteLine(GetEmployeesInPeriod(context));
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder employees = new StringBuilder();
            var employeesFullInformation = context.Employees.OrderBy(x => x.EmployeeId).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.MiddleName,
                x.JobTitle,
                x.Salary
            }).ToList();
            foreach(var employee in employeesFullInformation)
            {
                //Guy Gilbert R Production Technician 12500.00
                employees.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}"); 
            }
            return employees.ToString();
        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employees = context.Employees.Where(x => x.Salary > 50000).OrderBy(x => x.FirstName).Select(x => new
            {
                x.FirstName,
                x.Salary
            }).ToList();
            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }
            return stringBuilder.ToString();
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employees = context.Employees.Where(x => x.Department.Name == "Research and Development").OrderBy(x => x.Salary).ThenByDescending(x => x.FirstName).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Department,
                x.Salary
            });
            //Gigi Matthew from Research and Development - $40900.00
            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:F2}");
            }
            return stringBuilder.ToString();
        }
        //Do not invoke this method!!!
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Address newAddress = new Address() { TownId = 4, AddressText = "Vitoshka 15" };
            Employee toChangeName = context.Employees.FirstOrDefault(x => x.LastName == "Nakov");
            toChangeName.Address = newAddress;
            //If you want to invoke,make sure the line below is commented
            context.SaveChanges();
            var top10Addresses = context.Employees.OrderByDescending(x => x.AddressId).Take(10).Select(x => x.Address.AddressText).ToList();
            foreach(var address in top10Addresses)
            {
                stringBuilder.AppendLine(address);
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employees = context.Employees.Take(10).Select(x => new
            {
                EmployeeFirstName = x.FirstName,
                EmployeeLastName = x.LastName,
                ManagerFirstName = x.Manager.FirstName,
                ManagerLastName = x.Manager.LastName,
                Projects = x.Projects.Where(y => y.StartDate.Year >= 2001 && y.StartDate.Year <= 2003).Select(y => new
                {
                    y.Name,
                    y.StartDate,
                    y.EndDate
                }).ToList()
            }).ToList();
            //Guy Gilbert - Manager: Jo Brown
            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");
                foreach(var proj in employee.Projects)
                {
                    //"M/d/yyyy h:mm:ss tt" "not finished"
                    string endDateAsString;
                    if(proj.EndDate.HasValue)
                    {
                        endDateAsString = proj.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt");
                    }
                    else
                    {
                        endDateAsString = "not finished";
                    }
                    //--Half-Finger Gloves - 6/1/2002 12:00:00 AM - 6/1/2003 12:00:00 AM
                    stringBuilder.AppendLine($"--{proj.Name} - {proj.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {endDateAsString}");
                }
            }
            return stringBuilder.ToString().TrimEnd();
        }
    }
}

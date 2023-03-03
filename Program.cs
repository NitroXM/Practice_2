using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string taskschoise;
            bool v = true;
            while (v)
            {
                v = tasks();
            }
            bool tasks()
            {
                Console.WriteLine("Choose which task you want to execute: 1, 2, 3, 4");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "4":
                        Task4();
                        break;
                }
                Console.WriteLine("Do you want to choose tasks again? Y/N");
                taskschoise = Console.ReadLine();
                if (taskschoise == "Y")
                    return true;
                else
                    return false;
            }

            void Task1()
            {
                string filePath = "Students.txt"; // path to the file
                List<Student> students = new List<Student>();

                // Reading data from a file and adding it to the list
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        Student student = new Student
                        {
                            Name = data[0],
                            Grade1 = int.Parse(data[1]),
                            Grade2 = int.Parse(data[2]),
                            Grade3 = int.Parse(data[3])
                        };
                        students.Add(student);
                    }
                }

                // using a lambda expression to filter students by grades
                Console.WriteLine("Students with an average score of more than 4:");
                List<Student> filteredStudents = students.Where(s => s.AverageGrade() > 4).ToList();

                foreach (Student student in filteredStudents)
                {
                    Console.WriteLine("{0} - {1}", student.Name, student.AverageGrade());
                }
            }

            void Task2()
            {
                string filePath = "Employees.txt";

                List<Employee> employees = new List<Employee>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] employeeData = line.Split(',');
                        Employee employee = new Employee(employeeData[0], Convert.ToInt32(employeeData[1]), Convert.ToDecimal(employeeData[2]));
                        employees.Add(employee);
                    }
                }

                Console.WriteLine("Unsorted employees:");
                PrintEmployees(employees);

                Console.WriteLine("\nSorted by salary:");
                List<Employee> sortedEmployees = employees.OrderBy(e => e.Salary).ToList();
                PrintEmployees(sortedEmployees);

                static void PrintEmployees(List<Employee> employees)
                {
                    foreach (Employee employee in employees)
                    {
                        Console.WriteLine("{0}: {1}, {2:C}", employee.Name, employee.YearsOfExperience, employee.Salary);
                    }
                }
            }

            void Task3()
            {
                string filePath = "Products.txt";
                List<Product> products = ReadProductsFromFile(filePath);

                var groupedProducts = products.GroupBy(p => p.Category);

                foreach (var group in groupedProducts)
                {
                    Console.WriteLine($"Category: {group.Key}, Average Price: {group.Average(p => p.Price)}");

                    foreach (var product in group)
                    {
                        Console.WriteLine($"\tProduct: {product.Name}, Price: {product.Price}");
                    }
                }

                static List<Product> ReadProductsFromFile(string filePath)
                {
                    List<Product> products = new List<Product>();

                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] data = sr.ReadLine().Split(',');
                            Product product = new Product
                            {
                                Name = data[0],
                                Category = data[1],
                                Price = Convert.ToDouble(data[2])
                            };
                            products.Add(product);
                        }
                    }

                    return products;
                }

            }

            void Task4()
            {
                // creating a list of strings
                string[] names = { "Michael", "Jane", "Hannah", "Moe" };

                // creating a delegate that takes a string as input and returns its length
                Func<string, int> lengthDelegate = s => s.Length;

                // using a delegate to process a list of strings and display the results on the console
                Console.WriteLine("The length of each string in the array:");
                foreach (string name in names)
                {
                    Console.WriteLine("{0} - {1} symbols", name, lengthDelegate(name));
                }
            }
        }
    }
    class Student
    {
        public string Name { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public int Grade3 { get; set; }

        public double AverageGrade()
        {
            return (Grade1 + Grade2 + Grade3) / 3.0;
        }
    }

    class Employee
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal Salary { get; set; }

        public Employee(string name, int yearsOfExperience, decimal salary)
        {
            Name = name;
            YearsOfExperience = yearsOfExperience;
            Salary = salary;
        }
    }
    class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
}






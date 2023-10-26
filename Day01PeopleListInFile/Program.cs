using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01PeopleListInFile
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        private static int getMenuChoice()
        {
            Console.Write("What do you want to do?\n" +
                "1.Add person info\n" +
                "2.List persons info\n" +
                "3.Find a person by name\n" +
                "4.Find all persons younger than age\n" +
                "0.Exit\n" +
                "Choice: ");
            String line = Console.ReadLine();
            int choice;
            if (int.TryParse(line, out choice))  // asign a value to choice and return true or false
            { return choice; }
            else { return -1; }
        }
        private static void getMenuItemChoice()
        {
            while (true)
            {
                int choice = getMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddPersonInfo();
                        break;
                    case 2:
                        ListAllPersonInfo();
                        break;
                    case 3:
                        FindPersonByName();
                        break;
                    case 4:
                        FindPersonYoungerThan();
                        break;
                    case 0:
                        SavePersonToFile();
                        return;
                    default:
                        Console.WriteLine("No such option, try again");
                        break;
                }
                Console.WriteLine();
            }
        }
        public class Person
        {
            public string Name; // Name 2-100 characters long, not containing semicolons
            public int Age; // Age 0-150
            public string City; // City 2-100 characters long, not containing semicolons
            public Person(string name, int age, string city)
            {
                Name = name;
                Age = age;
                City = city;
            }
        }

        private static Person AddPersonInfo()
        {
            string name, city;
            int age;
            while (true)
            {
                Console.WriteLine("Enter name: ");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name) || name.Length < 2 || name.Length > 100)
                {
                    Console.WriteLine("Name must be 2-100 characters long.");
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Enter age: ");
                if (int.TryParse(Console.ReadLine(), out age) || age < 0 || age > 150)
                {
                    Console.WriteLine("Age must be 0-150.");
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Enter city: ");
                city = Console.ReadLine();
                if (string.IsNullOrEmpty(city) || city.Length < 2 || city.Length > 100)
                {
                    Console.WriteLine("City must be 2-100 characters long.");
                }
                else
                {
                    break;
                }
            }
            return new Person { Name = name, Age = age, City = city };
        }

        private static void ListAllPersonInfo() { }

        private static void FindPersonByName() { }
        private static void SavePersonToFile() { 
        
        
        
        }
        private static void FindPersonYoungerThan()
        { }
    }
}

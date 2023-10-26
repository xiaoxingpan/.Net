using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01PeopleListInFile
{
    internal class Program
    {
        const string DataFileName = @"..\..\people.txt";
        static List<Person> People = new List<Person>();  // to store all the person info
        static void Main(string[] args)
        {
            ReadAllPeopleFromFile();
            getMenuItemChoice();

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
        private static int getMenuChoiceLonger()
        {
            try
            {
                Console.Write("What do you want to do?\n" +
                       "1.Add person info\n" +
                       "2.List persons info\n" +
                       "3.Find and list a person by name\n" +
                       "4.Find and list persons younger than age\n" +
                       "0.Exit\n" + "Choice: ");
                string line = Console.ReadLine();
                int choice = int.Parse(line); // ex
                return choice;
            }
            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                return -1;
            }
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

        private static void AddPersonInfo()
        {
            Console.WriteLine("Adding a person.");
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
              
            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());  // ex FormatException, OverflowException

            Console.Write("Enter city: ");
            string city = Console.ReadLine();
              
            Person person = new Person(name, age, city);  // ex ArgumentException
            // if there is no constructer
            // Person person = new Person { Name = name , Age = age, City = city };

            People.Add(person);
            Console.WriteLine("Person added.");

        }

        private static void ListAllPersonInfo() 
        {
            Console.WriteLine("Listing all persons");
            foreach (Person person in People)
            {
                Console.WriteLine(person);
            }
        }

        private static void FindPersonByName() 
        {
            Console.Write("Enter partial person name: ");
            string searchStr = Console.ReadLine();
            var matchesList = People.Where(p => p.Name.Contains(searchStr)).ToList(); // LINQ, ToList() method is called after the query to materialize the results into a List<T>
            if (matchesList.Count > 0)
            {
                Console.WriteLine($"Found {matchesList.Count} results");
                foreach (Person p in matchesList)
                {
                    Console.WriteLine(p);
                }
            }
            else
            {
                Console.WriteLine("No matches found");
            }
        }

        private static void SavePersonToFile() 
        {
            try
            {
                List<string> linesList = new List<String>();
                foreach (Person person in People)
                {
                    linesList.Add($"{person.Name};{person.Age};{person.City}");
                }
                File.WriteAllLines(DataFileName, linesList); // ex IOException, SystemException, automatically inserts /n after each line
            }
            // catch (Exception ex) when (ex is IOException || ex is SystemException)
            catch (SystemException ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }


        }
        private static void FindPersonYoungerThan()
        {
            Console.Write("Enter maximum age: ");
            if(int.TryParse(Console.ReadLine(), out  int maxAge))
            {
                Console.WriteLine("Invalid input");
                return;
            }
            Console.WriteLine("Peple at that age or younger:");
            var youngerList = People.Where(p => (p.Age <= maxAge)); // Method syntax, returns an IEnumerable<T>
            var youngerList2 = from p in People where p.Age <= maxAge select p; // Query syntax
            foreach (Person p in youngerList)
            {
                Console.WriteLine(p);
            }
        }

        private static void ReadAllPeopleFromFile()
        {
            try
            {
                if (!File.Exists(DataFileName))
                {
                    return; // it's okay if the file does not exist yet
                }
                string[] linesArray = File.ReadAllLines(DataFileName); // ex IOException, SystemException
                foreach (string line in linesArray)
                {
                    try
                    {
                        string[] data = line.Split(';');
                        if (data.Length != 3)
                        {
                            throw new FormatException("Invalid number of items");
                            // or: Console.WriteLine("Error..."); continue;
                        }
                        string name = data[0];
                        int age = int.Parse(data[1]); // ex FormatException
                        string city = data[2];
                        Person person = new Person(name, age, city); // ex ArgumentException
                        People.Add(person);
                    }
                    catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
                    {
                        Console.WriteLine($"Error (skipping line): {ex.Message} in:\n  {line}");
                    }
                }
            }
            // catch (Exception ex) when (ex is IOException || ex is SystemException)
            catch (SystemException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }
    }
}

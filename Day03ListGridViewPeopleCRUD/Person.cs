using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03ListGridViewPeopleCRUD
{
    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // if want to validation in the setter
        //private int _age;
        //public int Age
        //{
        //    get
        //    {
        //        return _age;
        //    }
        //    set
        //    {
        //        if(!IsAgeValid(value, out string msg))
        //        {
        //            throw new ArgumentException(msg);
        //        }
        //    }
        //}

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public static bool IsNameValid(string name, out string error)
        { // Name must be 2-50 characters long, no semicolons
            if (name.Length < 2 || name.Length > 50 || name.Contains(";"))
            {
                error = "Name must be 2-50 characters long, no semicolons";
                return false;
            }
            error = null;
            return true; 
        }

        // can use both front-end and call back-end
        public static bool IsAgeValid(int age, out string error)
        { // age must be 0-150
            if (age < 0 || age > 150)
            {
                error = "Age must be 0-150";
                return false;
            }
            error = null;
            return true;
        }

        public override string ToString()
        {
            return $"{Name}, {Age}";
        }

        public static bool IsAgeValid(string strAge, out string error)
        {
            if (!int.TryParse(strAge, out int age))
            {
                error = "Age must be an integer value";
                return false;
            }
            if (age < 0 || age > 150)
            {
                error = "Age must be 0-150";
                return false;
            }
            error = null;
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(int id, string firstName, string lastName, int age)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
}

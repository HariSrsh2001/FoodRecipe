using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Company
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public Company(int id,string name ) { 
            this.Id = id;
            this.Name = name;
        }
        public  void DisplayCompanyName()
        {
            Console.WriteLine($"Id:{Id} ,Name:{Name}");        }
    }
}


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Company obj = new Company(24,"Tata");
            obj.DisplayCompanyName();

            Company newobj= new Company(44,"Nestle");
        
            newobj.DisplayCompanyName();
                                                                
        }
    }
}
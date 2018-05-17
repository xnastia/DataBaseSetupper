using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;//////
using Models;
namespace DataBaseSetupper
{
    class Program
    {
        const string DbConnectionString = @"Data Source=ANASTASIIA\SQLEXPRESS;Initial Catalog=NewBikeStore;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        static void Main(string[] args)
        {
            var db = new StoreDbContext(DbConnectionString);
            Console.WriteLine("Products Count: {0}", db.Products.Count());
            Console.ReadLine();
        }
    }
}

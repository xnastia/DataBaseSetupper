using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace DataBaseSetupper
{
    class Program
    {
        /*const string DbConnectionString = @"Data Source=ANASTASIIA\SQLEXPRESS;
                                  Initial Catalog=NewBikeStore;Integrated Security=True;
                                  Connect Timeout=15;  Encrypt=False;TrustServerCertificate=False";*/
       // Data Source=(localdb)\Projects;Initial Catalog=testconn;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False
        static void Main(string[] args)
        {
            
            Console.WriteLine("Please, enter data source");
            string data_source = Console.ReadLine();
           // if (data_source =="") {data_source=@"ANASTASIIA\SQLEXPRESS";}
            if (data_source == "") { data_source = @"(localdb)\Projects"; }
            Console.WriteLine("Please, enter initial catalog");
            string initial_catalog = Console.ReadLine();
            if (initial_catalog =="") { initial_catalog = "NewBikeStore"; }
            string conn = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False", data_source, initial_catalog);
            Console.WriteLine(conn);
            Console.ReadLine();
            var db = new StoreDbContext(conn);
            Console.WriteLine("Products Count: {0}", db.Products.Count());
            Console.ReadLine();
            User Petro = new User() { Name = "Petro", Email = "qwe@mail.ua", Password = "qweqwe" };
            User Ivan = new User() { Name = "Ivan", Email = "ewq@mail.ua", Password = "ewqewq" };
            UserRepository UR = new UserRepository(conn);
            UR.Create(Petro);
            UR.Create(Ivan);
            List<User> UL = new List<User>();
            UL = UR.GetUsers();
            foreach(User u in UL)
            {
                int id = u.Id;
                User U=UR.Get(id);
                string s = U.Name + "  " + U.Email + "  " + U.Password;
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}

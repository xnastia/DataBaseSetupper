using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseRepository
    {
        protected string connectionString;
        public BaseRepository() : this(GetConnectionString()) {    
            
        }
        public BaseRepository(string conn)
        {
            this.connectionString = conn;
        }
        public static string GetConnectionString() {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["ConnectionString"];
            return result;
        }
    }
}

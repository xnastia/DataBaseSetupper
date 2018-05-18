using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class BaseRepository
    {
        protected string connectionString;
        public BaseRepository(string conn)
        {
            this.connectionString = conn;
        }
    }
}

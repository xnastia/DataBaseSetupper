using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace Models
{
    public class VendorRepository : BaseRepository
    {
        public VendorRepository(string ConnectionString) : base(ConnectionString) { }
        public List<User> GetVendors()
        {
            List<User> vendors = new List<User>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                vendors = db.Query<User>("SELECT * FROM Vendors").ToList();
            }
            return vendors;
        }
        public User Get(int id)
        {
            User vendor = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                vendor = db.Query<User>("SELECT * FROM Vendors WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return vendor;
        }

        public User Create(User vendor)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Vendors (Name) VALUES(@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
                int vendorId = db.Query<int>(sqlQuery, vendor).FirstOrDefault();
                vendor.Id = vendorId;
            }
            return vendor;
        }

        public void Update(User vendor)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Vendors SET Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, vendor);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}

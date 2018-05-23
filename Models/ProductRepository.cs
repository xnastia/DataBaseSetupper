using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Models
{
    public class ProductRepository: BaseRepository
    {
        public ProductRepository(string ConnectionString) : base(ConnectionString) { }
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                products = db.Query<Product>("SELECT * FROM Products").ToList();
            }
            return products;
        }
        public Product Get(int id)
        {
            Product product = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                product = db.Query<Product>("SELECT * FROM Products WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return product;
        }

        public Product Create(Product product)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Products (Name, Description,Price,CategoryId, VendorId) VALUES(@Name, @Description,@Price,@CategoryId, @VendorId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int productId = db.Query<int>(sqlQuery, product).FirstOrDefault();
                product.Id = productId;
            }
            return product;
        }

        public void Update(Product product)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Products SET   Name = @Name, Description = @Description, Price=@Price, CategoryId=@CategoryId, VendorId=@VendorId WHERE Id = @Id";
                db.Execute(sqlQuery, product);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Products WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }

  }

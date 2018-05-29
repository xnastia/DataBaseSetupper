using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace Models
{
    public class CategoryRepository : BaseRepository
    {
        public CategoryRepository(string ConnectionString) : base(ConnectionString) { }
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                categories = db.Query<Category>("SELECT * FROM Categories").ToList();
            }
            return categories;
        }
        public Category Get(int id)
        {
            Category category = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                category = db.Query<Category>("SELECT * FROM Categories WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return category;
        }

        public Category Create(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Categories (Id, Name, ProductsCount) VALUES(@Id, @Name, @ProductsCount); SELECT CAST(SCOPE_IDENTITY() as int)";
                int userId = db.Query<int>(sqlQuery, category).FirstOrDefault();
                category.Id = userId;
            }
            return category;
        }

        public void Update(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, category);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM  Categories WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}

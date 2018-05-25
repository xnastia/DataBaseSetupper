using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace Models
{
    public class UserRepository : BaseRepository
    {
        public UserRepository() { }
        public UserRepository(string ConnectionString) : base(ConnectionString) { }        
        public virtual List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                users = db.Query<User>("SELECT * FROM Users").ToList();
            }
            return users;
        }
        public User Get(int id)
        {
            User user = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                user = db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return user;
        }
 
        public User Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Users (Name, Email, Password) VALUES(@Name, @Email,@Password); SELECT CAST(SCOPE_IDENTITY() as int)";
                int userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                user.Id = userId;
            }
            return user;
        }
 
        public void Update(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET Name = @Name, Email = @Email, Password=@Password WHERE Id = @Id";
                db.Execute(sqlQuery, user);
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

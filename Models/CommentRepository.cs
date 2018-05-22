using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace Models
{
    public class CommentRepository : BaseRepository
    {
        public CommentRepository(string ConnectionString) : base(ConnectionString) { }
        public List<User> GetComments()
        {
            List<User> comments = new List<User>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                comments = db.Query<User>("SELECT * FROM Comments").ToList();
            }
            return comments;
        }
        public User Get(int id)
        {
            User comment = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                comment = db.Query<User>("SELECT * FROM Comments WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return comment;
        }

        public User Create(User comment)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Comments (Message, UserName, Product) VALUES(@Message, @UserName, @Product); SELECT CAST(SCOPE_IDENTITY() as int)";
                int commentId = db.Query<int>(sqlQuery, comment).FirstOrDefault();
                comment.Id = commentId;
            }
            return comment;
        }

        public void Update(User comment)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Comments SET Message = @Message, UserName = @UserName, Product=@Product WHERE Id = @Id";
                db.Execute(sqlQuery, comment);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Comments WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}

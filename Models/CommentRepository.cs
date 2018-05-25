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
        public CommentRepository() { }
        public CommentRepository(string ConnectionString) : base(ConnectionString) { }
        public virtual List<Comment> GetComments(int ProductId)
        {
            List<Comment> comments = new List<Comment>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                comments = db.Query<Comment>("SELECT * FROM Comments WHERE ProductId=@ProductId",new { ProductId}).ToList();
            }
            return comments;
        }
        public virtual List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                comments = db.Query<Comment>("SELECT * FROM Comments WHERE ProductId=@ProductId").ToList();
            }
            return comments;
        }
        public Comment Get(int id)
        {
            Comment comment = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                comment = db.Query<Comment>("SELECT * FROM Comments WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return comment;
        }

        public Comment Create(Comment comment)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Comments (Message, UserName, ProductId, Rating) VALUES(@Message, @UserName, @ProductId,@Rating); SELECT CAST(SCOPE_IDENTITY() as int)";
                int commentId = db.Query<int>(sqlQuery, comment).FirstOrDefault();
                comment.Id = commentId;
            }
            return comment;
        }

        public void Update(Comment comment)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Comments SET Message = @Message, UserName = @UserName, ProductId=@ProductId, Rating=@Raiting WHERE Id = @Id";
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
        public double AverageRating(int ProductId)
        {
            double rating;
            using (IDbConnection db = new SqlConnection(connectionString))

            {
                 rating= db.Query <double>( "SELECT AVG(Rating) FROM Comments WHERE ProductId=@ProductId", new { ProductId }).FirstOrDefault();
            }
            return rating;
        }
        public static double AverageRating(CommentRepository cr, int ProductId)
        {
            return cr.GetComments(ProductId).Average(a => a.Rating);
       
        }

    }
}

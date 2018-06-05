using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace Models
{
    public class OrderItemRepository : BaseRepository
    {
        public OrderItemRepository() : base() { }
        public OrderItemRepository(string ConnectionString) : base(ConnectionString) { }
        public List<OrderItem> GetOrderItems()
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                orderItems = db.Query<OrderItem>("SELECT * FROM OrderItems").ToList();
            }
            return orderItems;
        }
        public OrderItem Get(int id)
        {
            OrderItem orderItem = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                orderItem = db.Query<OrderItem>("SELECT * FROM OrderItems WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return orderItem;
        }

        public OrderItem Create(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO OrderItems (OrderId, ProductId, Quantity) VALUES(@OrderId, @ProductId, @Quantity); SELECT CAST(SCOPE_IDENTITY() as int)";
                int orderItemId = db.Query<int>(sqlQuery, orderItem).FirstOrDefault();
                orderItem.Id = orderItemId;
            }
            return orderItem;
        }

        public void Update(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE OrderItems SET OrderId=@OrderId, ProductId=@ProductId, Quantity=@Quantity WHERE Id = @Id";
                db.Execute(sqlQuery, orderItem);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM OrderItems WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
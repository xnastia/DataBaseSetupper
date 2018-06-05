using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Models
{
    public class OrderRepository : BaseRepository
    {
        public OrderRepository() : base() { }
        public OrderRepository(string ConnectionString) : base(ConnectionString) { }
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                orders = db.Query<Order>("SELECT * FROM Orders").ToList();
            }
            return orders;
        }
        public Order Get(int id)
        {
            Order order = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                order = db.Query<Order>("SELECT * FROM Orders WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return order;
        }

        public Order Create(Order order)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Orders (Name, Phone,OrderDetails,Sum) VALUES(@Name,@Phone,@OrderDetails,@Sum); SELECT CAST(SCOPE_IDENTITY() as int)";
                int orderId = db.Query<int>(sqlQuery, order).FirstOrDefault();
                order.Id = orderId;
            }
            return order;
        }

        public void Update(Order order)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Products SET   Name = @Name, Phone = @Phone, OrderDetails=@OrderDetails, Sum=@Sum) WHERE Id = @Id";
                db.Execute(sqlQuery, order);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Orders WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }

}


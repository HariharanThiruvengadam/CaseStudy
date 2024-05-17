using EcommerceApplication.Exception;
using EcommerceApplication.Model;
using EcommerceApplication.Utility;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace EcommerceApplication.Repositories
{
    public class OrderProcessorRepository : IOrderProcessorRepository
    {
        internal SqlConnection sqlConnection = null;

        internal SqlCommand cmd = null;

        public OrderProcessorRepository()
        {
            sqlConnection = new SqlConnection(ConnectionUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public int CreateProduct(Product newProduct)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Insert into products OUTPUT INSERTED.product_id values(@Name,@Price,@Description,@stockQuantity)";
            cmd.Parameters.AddWithValue("@Name", newProduct.Name);
            cmd.Parameters.AddWithValue("@Price", newProduct.Price);
            cmd.Parameters.AddWithValue("@Description", newProduct.Description);
            cmd.Parameters.AddWithValue("@stockQuantity", newProduct.StockQuantity);
            int status = Convert.ToInt32(cmd.ExecuteScalar());
            sqlConnection.Close();
            return status;
        }

        public int CreateCustomer(Customer customer)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Insert into Customers OUTPUT INSERTED.customer_id values(@Name,@Email,@Password)";
            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Password", customer.Password);
            int status =  Convert.ToInt32(cmd.ExecuteScalar());
            sqlConnection.Close();
            return status;
        }

        public bool DeleteProduct(int productId)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "delete from products where product_id=@productId";
            cmd.Parameters.AddWithValue("@productId", productId);
            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }

        public bool DeleteCustomer(int customerId)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "delete from customers where customer_id=@customerId";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }

        public bool AddToCart(Cart cart)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "INSERT INTO cart VALUES (@customer_id, @product_id, @Quantity)";

            cmd.Parameters.AddWithValue("@customer_id", cart.CustomerId);
            cmd.Parameters.AddWithValue("@product_id", cart.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);

            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }

        public bool RemoveFromCart(int customerId, int productId)
        {
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Delete from cart where customer_id = @customerId and product_id = @productId";

            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@productId", productId);

            int status = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return status > 0 ? true : false;
        }

        public List<Product> getAllProducts()
        {
            List<Product> products = new List<Product>();
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Select * from products";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Product product = new Product
                {
                    ProductId = (int)reader["product_id"],
                    Name = Convert.ToString(reader["productName"]),
                    Price = (decimal)reader["price"],
                    Description = Convert.ToString(reader["Description"])
                };

                products.Add(product);
            }
            reader.Close();
            sqlConnection.Close();

            if (products.Count == 0)
            {
                throw new ProductNotFoundException("No products available!");
            }

            return products;
        }

        public List<Customer> getAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "Select * from customers";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Customer customer = new Customer
                {
                    CustomerId = (int)reader["customer_id"],
                    Name = Convert.ToString(reader["Name"]),
                };

                customers.Add(customer);
            }
            reader.Close();
            sqlConnection.Close();

            return customers;
        }

        public List<KeyValuePair<Cart, string>> GetAllFromCart(int customerId)
        {

            List<KeyValuePair<Cart, string>> products = new List<KeyValuePair<Cart, string>>();
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            cmd.CommandText = "SELECT COUNT(*) FROM Customers WHERE customer_id = @CustomerId";
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            int customerExists = Convert.ToInt32(cmd.ExecuteScalar());
            if (customerExists == 0)
            {
                throw new CustomerNotFoundException($"Customer with ID {customerId} not found.");
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT c.* , p.productName " +
                "FROM Cart c " +
                "JOIN Products p ON c.product_id = p.product_id " +
                "WHERE c.customer_id = @CustomerId";

            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cart cart = new Cart()
                {
                    CartId = (int)reader["cart_id"],
                    ProductId = (int)reader["product_id"],
                    Quantity = (int)reader["Quantity"]
                };

                string productName = Convert.ToString(reader["productName"]);

                products.Add(new KeyValuePair<Cart, string>(cart, productName));
            }
            reader.Close();
            sqlConnection.Close();

            if (products.Count == 0)
            {
                throw new ProductNotFoundException("No products found in the cart");
            }
            return products;
        }

        public (bool, decimal) PlaceOrder(Order order, List<KeyValuePair<Product, int>> products)
        {
            decimal totalAmount = 0;
            int status = 0;
            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;

            sqlConnection.Open();
            cmd.CommandText = "SELECT COUNT(*) FROM Customers WHERE customer_id = @CustomerId";
            cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            int customerExists = (int)cmd.ExecuteScalar();
            if (customerExists == 0)
            {
                throw new CustomerNotFoundException($"Customer with ID {order.CustomerId} not found.");
            }
            sqlConnection.Close();

            foreach (var item in products)
            {
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM Products WHERE product_id = @ProductId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ProductId", item.Key.ProductId);
                int productExists = (int)cmd.ExecuteScalar();
                if (productExists == 0)
                {
                    throw new ProductNotFoundException($"Product with ID {item.Key.ProductId} not found.");
                }
                sqlConnection.Close();
            }

            foreach (var item in products)
            {
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM cart WHERE product_id = @ProductId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ProductId", item.Key.ProductId);
                int productExists = (int)cmd.ExecuteScalar();
                if (productExists == 0)
                {
                    throw new ProductNotFoundException($"Product with ID {item.Key.ProductId} not found in the cart.");
                }
                sqlConnection.Close();
            }

            foreach (var item in products)
            {
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "Select Price from products where product_id = @ProductId";
                cmd.Parameters.AddWithValue("@ProductId", item.Key.ProductId);
                decimal price = (decimal)cmd.ExecuteScalar();
                totalAmount += price * item.Value;
                sqlConnection.Close();
            }

           // Console.WriteLine(totalAmount);
            order.TotalPrice = totalAmount;

            sqlConnection.Open();
            cmd.CommandText = "INSERT INTO Orders OUTPUT INSERTED.order_id VALUES (@customer_id,@OrderDate,@Price,@ShippingAddress)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@customer_id", order.CustomerId);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            cmd.Parameters.AddWithValue("@Price", order.TotalPrice);
            cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
            int orderId = (int)cmd.ExecuteScalar();
            sqlConnection.Close();

            if (orderId > 0)
            {
                foreach (var item in products)
                {
                    sqlConnection.Open();
                    cmd.CommandText = "INSERT INTO order_items VALUES (@orderId,@productId,@quantity)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@productId", item.Key.ProductId);
                    cmd.Parameters.AddWithValue("@quantity", item.Value);
                    status = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                foreach (var product in products)
                { 
                        this.RemoveFromCart(order.CustomerId, product.Key.ProductId.Value);
                   
                }
            }

           
            
            return (status > 0, totalAmount);
        }

        public (List<Order>, string) GetOrdersByCustomer(int customerId)
        {
            (List<Order>, string) orderDetails = (new List<Order>(), "");

            cmd.Parameters.Clear();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            cmd.CommandText = "SELECT COUNT(*) FROM Customers WHERE customer_id = @CustomerId";
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            int customerExists = (int)cmd.ExecuteScalar();
            if (customerExists == 0)
            {
                throw new CustomerNotFoundException($"Customer with ID {customerId} not found.");
            }
            sqlConnection.Close();

            cmd.Parameters.Clear();
            sqlConnection.Open();
            cmd.CommandText = "SELECT o.order_id, c.[Name], o.total_price as TotalPrice " +
                    "FROM Orders o " +
                     "JOIN Customers c ON c.customer_id = o.customer_id " +
                    "WHERE o.customer_id=@CustomerId";
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order();

                order.OrderId = Convert.ToInt32(reader["order_id"]);
                order.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);
                string customerName = Convert.ToString(reader["Name"]);
                orderDetails.Item1.Add(order);
                orderDetails.Item2 = customerName;

            }

            reader.Close();
            sqlConnection.Close();
            return orderDetails;
        }
    }
}

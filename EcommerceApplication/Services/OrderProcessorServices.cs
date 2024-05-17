using EcommerceApplication.Exception;
using EcommerceApplication.Model;
using EcommerceApplication.Repositories;

namespace EcommerceApplication.Services
{
    internal class OrderProcessorServices : IOrderProcessorServices
    {
        internal readonly IOrderProcessorRepository orderProcessorRepo;

        public OrderProcessorServices()
        {
            orderProcessorRepo = new OrderProcessorRepository();
        }

        public void CreateProduct()
        {

            Console.Write("Enter the product ID (leave blank if none): ");
            string productIdInput = Console.ReadLine();
            int? productId = null;
            if (!string.IsNullOrEmpty(productIdInput))
            {
                int parsedProductId;
                if (int.TryParse(productIdInput, out parsedProductId))
                {
                    productId = parsedProductId;
                }
                else
                {
                    Console.WriteLine("Invalid input for product ID. Setting it to null.");
                }
            }

            string name;
            while (true)
            {
                Console.Write("Enter the product name: ");
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name)) { break; }
                Console.WriteLine("Invalid input. Product name cannot be empty. Please enter a valid product name.");
            }

            Console.Write("Enter the price of the product: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number for the price.");
                Console.Write("Enter the price of the product: ");
            }

            string description;
            while (true)
            {
                Console.Write("Enter the description of the product: ");
                description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description)) { break; }
                Console.WriteLine("Invalid input. Please enter a valid string for the description.");
            }

            Console.Write("Enter the stock quantity: ");
            int stockQuantity;
            while (!int.TryParse(Console.ReadLine(), out stockQuantity))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the stock quantity.");
                Console.Write("Enter the stock quantity: ");
            }

            Product newProduct = new Product(productId, name, price, description, stockQuantity);

            int newProductId = orderProcessorRepo.CreateProduct(newProduct);

            if (newProductId >0)
            {
                Console.WriteLine($"Product created successfully. Product Id is {newProductId}");
            }
            else
            {
                Console.WriteLine("Failed to create product.");
            }
        }

        public void CreateCustomer()
        {
            Console.Write("Enter the customer ID (leave blank if none): ");
            string customerIdInput = Console.ReadLine();
            int? customerId = null;
            if (!string.IsNullOrEmpty(customerIdInput))
            {
                int parsedCustomerId;
                if (int.TryParse(customerIdInput, out parsedCustomerId))
                {
                    customerId = parsedCustomerId;
                }
                else
                {
                    Console.WriteLine("Invalid input for customer ID. Setting it to null.");
                }
            }

            string name;
            while (true)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name)) { break; }
                Console.WriteLine("Invalid input. Name cannot be empty. Please enter a valid name.");
            }

            string emailId;
            while (true)
            {
                Console.Write("Enter your email id: ");
                emailId = Console.ReadLine();
                if (!string.IsNullOrEmpty(emailId)) { break; }
                Console.WriteLine("Invalid input. Email Id cannot be empty. Please enter a valid email Id.");
            }

            string password;
            while (true)
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine();
                if (!string.IsNullOrEmpty(password)) { break; }
                Console.WriteLine("Invalid input. Password cannot be empty. Please enter a valid password.");
            }

            Customer newCustomer = new Customer(customerId, name, emailId, password);

            int newCustomerId = orderProcessorRepo.CreateCustomer(newCustomer);
            if (newCustomerId>0)
            {
                Console.WriteLine($"Added new customer! Customer ID is {newCustomerId}");
            }
            else
            {
                Console.WriteLine("Failed to add new customer");
            }
        }

        public void DeleteProduct()
        {
            List<Product> products = orderProcessorRepo.getAllProducts();

            foreach (var item in products)
            {
                Console.WriteLine($"Product Id: {item.ProductId}, Product Name: {item.Name}");
            }

            Console.WriteLine("Enter the product's ID which you want to delete:");
            int prodId;
            while (!int.TryParse(Console.ReadLine(), out prodId))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the product's ID.");
                Console.WriteLine("Enter the product's ID:");
            }

            bool status = orderProcessorRepo.DeleteProduct(prodId);
            if (status)
            {
                Console.WriteLine("Deleted the specified product");
            }
            else
            {
                Console.WriteLine("Failed to delete the specified product");
            }
        }

        public void DeleteCustomer()
        {
            /*List<Customer> customers = orderProcessorRepo.getAllCustomers();

            foreach (var item in customers)
            {
                Console.WriteLine($"Customer Id: {item.CustomerId}, Customer Name: {item.Name}");
            }*/
            Console.WriteLine("Enter the customer's ID which you want to delete:");

            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the customer's ID.");
                Console.WriteLine("Enter the customer's ID:");
            }

            bool status = orderProcessorRepo.DeleteCustomer(customerId);
            if (status)
            {
                Console.WriteLine("Deleted the specified customer");
            }
            else
            {
                throw new CustomerNotFoundException("Failed to delete the specified customer because customer id is not found!");
            }
        }

        public void AddToCart()
        {
           /* List<Customer> customers = orderProcessorRepo.getAllCustomers();

            foreach (var item in customers)
            {
                Console.WriteLine($"Customer Id: {item.CustomerId}, Customer Name: {item.Name}");
            }*/

            Cart cart = new Cart();

            while (true)
            {
                Console.Write("Enter Customer ID: ");
                string customerIdInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(customerIdInput))
                {
                    int parsedCustomerId;
                    if (int.TryParse(customerIdInput, out parsedCustomerId))
                    {
                        cart.CustomerId = parsedCustomerId;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for customer ID.");
                    }
                }
            }

            try
            {
                List<Product> products = orderProcessorRepo.getAllProducts();

                foreach (var item in products)
                {
                    Console.WriteLine($"Product Id: {item.ProductId}, Product Name: {item.Name}");
                }

                Console.WriteLine("Choose your product to add in the cart:");
                while (true)
                {
                    Console.Write("Enter Product ID: ");
                    string productIdInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(productIdInput))
                    {
                        int parsedProductId;
                        if (int.TryParse(productIdInput, out parsedProductId))
                        {
                            cart.ProductId = parsedProductId;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for Product ID.");
                        }
                    }
                }
                while (true)
                {
                    Console.Write("Enter Quantity: ");
                    string quantityInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(quantityInput))
                    {
                        int parsedQuantity;
                        if (int.TryParse(quantityInput, out parsedQuantity))
                        {
                            cart.Quantity = parsedQuantity;
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid input for quantity.");
                        }
                    }
                }

                bool status = orderProcessorRepo.AddToCart(cart);

                if (status)
                {
                    Console.WriteLine("Added to cart!!");
                }
                else
                {
                    Console.WriteLine("Failed to add to cart");
                }
            }
            catch (ProductNotFoundException pex)
            {
                Console.WriteLine(pex.Message);
            }
        }

        public void RemoveFromCart()
        {
           /* List<Customer> customers = orderProcessorRepo.getAllCustomers();

            foreach (var item in customers)
            {
                Console.WriteLine($"Customer Id: {item.CustomerId}, Customer Name: {item.Name}");
            }*/
            Console.WriteLine("Enter the customer id:");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the customer's ID.");
                Console.WriteLine("Enter the customer id:");
            }

            try
            {
                List<KeyValuePair<Cart, string>> cartItems = orderProcessorRepo.GetAllFromCart(customerId);

                foreach (var item in cartItems)
                {
                    Console.WriteLine($"Product Id :- {item.Key.ProductId}, Product Name:{item.Value}, Quantity:{item.Key.Quantity}");
                }

                Console.WriteLine("Enter the product id:");
                int productId;
                while (!int.TryParse(Console.ReadLine(), out productId))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the product ID.");
                    Console.WriteLine("Enter the product id:");
                }

                bool status = orderProcessorRepo.RemoveFromCart(customerId, productId);

                if (status)
                {
                    Console.WriteLine("Removed from cart!!");
                }
                else
                {
                    Console.WriteLine("Failed to remove from cart!!");
                }

            }
            catch (ProductNotFoundException pex)
            {
                Console.WriteLine(pex.Message);
            }
        }

        public void GetAllFromCart()
        {

          /*  List<Customer> customers = orderProcessorRepo.getAllCustomers();

            foreach (var item in customers)
            {
                Console.WriteLine($"Customer Id: {item.CustomerId}, Customer Name: {item.Name}");
            }*/
            Console.Write("Enter Customer ID: ");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the customer's ID.");
                Console.Write("Enter Customer ID: ");
            }

            try
            {
                List<KeyValuePair<Cart, string>> productsInCart = orderProcessorRepo.GetAllFromCart(customerId);

                if (productsInCart.Count > 0)
                {
                    Console.WriteLine("Products in the cart:");
                    foreach (var item in productsInCart)
                    {
                        Console.WriteLine($"Product Id:{item.Key.ProductId}Product Name: {item.Value}\t Product quantity:{item.Key.Quantity}");
                    }
                }
            }
            catch (ProductNotFoundException pex)
            {
                Console.WriteLine(pex.Message);
            }
            catch (CustomerNotFoundException cex)
            {
                Console.WriteLine(cex.Message);
            }
        }

        public void PlaceOrder()
        {
            try
            {
                List<KeyValuePair<Product, int>> items = new List<KeyValuePair<Product, int>>();

                Product product = new Product();
                Order order = new Order();
                string addMore;
                int quantity;

               /* List<Customer> customers = orderProcessorRepo.getAllCustomers();

                foreach (var item in customers)
                {
                    Console.WriteLine($"Customer Id: {item.CustomerId}, Customer Name: {item.Name}");
                }
*/
                while (true)
                {
                    Console.Write("Enter Customer ID: ");
                    string customerIdInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(customerIdInput))
                    {
                        int parsedCustomerId;
                        if (int.TryParse(customerIdInput, out parsedCustomerId))
                        {
                            order.CustomerId = parsedCustomerId;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for customer ID.");
                        }
                    }
                }

                while (true)
                {
                    Console.Write("Enter Shipping Address: ");
                    string shippingAddressInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(shippingAddressInput))
                    {
                        order.ShippingAddress = shippingAddressInput;
                        break;
                    }
                    Console.WriteLine("Invalid input. Shipping address cannot be empty. Please enter a valid shipping address.");
                }

                order.OrderDate = DateTime.Now;

                do
                {

                    List<KeyValuePair<Cart, string>> cartItems = orderProcessorRepo.GetAllFromCart(order.CustomerId);

                    foreach (var item in cartItems)
                    {
                        Console.WriteLine($"Product Id :- {item.Key.ProductId}, Product Name:{item.Value}, Quantity:{item.Key.Quantity}");
                    }

                    while (true)
                    {
                        Console.Write("Enter Product ID: ");
                        string productIdInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(productIdInput))
                        {
                            int parsedProductId;
                            if (int.TryParse(productIdInput, out parsedProductId))
                            {
                                product.ProductId = parsedProductId;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for Product ID.");
                            }
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter Quantity: ");
                        string quantityInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(quantityInput))
                        {
                            int parsedQuantity;
                            if (int.TryParse(quantityInput, out parsedQuantity))
                            {
                                quantity = parsedQuantity;
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Invalid input for quantity.");
                            }
                        }
                    }

                    KeyValuePair<Product, int> newKey = new KeyValuePair<Product, int>(product, quantity);
                    items.Add(newKey);

                    Console.Write("Do you want to add another product? (yes/no): ");
                    addMore = Console.ReadLine().ToLower();
                } while (addMore.Equals("yes"));

                var (success, TotalAmount) = orderProcessorRepo.PlaceOrder(order, items);

                if (success)
                {

                    Console.WriteLine($"Order placed successfully! Total amount: ${TotalAmount}");
                }
                else
                {
                    Console.WriteLine("Failed to place order.");
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);

            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetOrdersByCustomer()
        {

            try
            {
               /* List<Customer> customers = orderProcessorRepo.getAllCustomers();

                foreach (var item in customers)
                {
                    Console.WriteLine($"Customer Id: {item.CustomerId}, Customer Name: {item.Name}");
                }*/

                Console.Write("Enter Customer ID: ");
                int customerId;
                while (!int.TryParse(Console.ReadLine(), out customerId))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the customer's ID.");
                    Console.Write("Enter Customer ID: ");
                }

                (List<Order>, string) orders = orderProcessorRepo.GetOrdersByCustomer(customerId);

                if (orders.Item1.Count > 0)
                {
                    Console.WriteLine("Orders for Customer ID: " + customerId);
                    foreach (var order in orders.Item1)
                    {
                        Console.WriteLine($"Customer Id: {customerId}, Order Id: {order.OrderId}, Customer Name: {orders.Item2}, Total Amount: {order.TotalPrice}");
                    }
                }
                else
                {
                    throw new OrderNotFoundException("No orders found for this customer!");
                }
            }
            catch (OrderNotFoundException onfex)
            {
                Console.WriteLine(onfex.Message);
            }
            catch (CustomerNotFoundException cex)
            {
                Console.WriteLine(cex.Message);
            }
        }
    }
}

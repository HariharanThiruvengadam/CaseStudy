using EcommerceApplication.Services;

namespace EcommerceApplication
{
    internal class EcommerceApp
    {
        internal readonly IOrderProcessorServices _orderProcessorServices;

        public EcommerceApp()
        {
            _orderProcessorServices = new OrderProcessorServices();
        }

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nWELCOME TO E-COMMERCE APPLICATION");
                Console.WriteLine("\nHere are the options from which you can choose to perform:");

                Console.WriteLine("\nCustomer Management:");
                Console.WriteLine("  1. Register Customer");
                Console.WriteLine("  2. Delete Customer");

                Console.WriteLine("\nProduct Management:");
                Console.WriteLine("  3. Create Product");
                Console.WriteLine("  4. Delete Product");

                Console.WriteLine("\nShopping Cart:");
                Console.WriteLine("  5. Add to Cart");
                Console.WriteLine("  6. Remove from Cart");
                Console.WriteLine("  7. Get All Products from Cart");

                Console.WriteLine("\nOrder Management:");
                Console.WriteLine("  8. Place Order");
                Console.WriteLine("  9. Get Orders Ordered by Customer");

                Console.WriteLine("\nExit the Application:");
                Console.WriteLine("  10. Exit");

                Console.Write("\nSelect an Option: ");

                int userOption = int.Parse(Console.ReadLine());
                switch (userOption)
                {
                    case 1:
                        _orderProcessorServices.CreateCustomer();
                        break;
                    case 2:
                        _orderProcessorServices.DeleteCustomer();
                        break;
                       
                    case 3:
                        _orderProcessorServices.CreateProduct();
                        break;
                      
                    case 4:
                        _orderProcessorServices.DeleteProduct();
                        break;
                    case 5:
                        _orderProcessorServices.AddToCart();
                        break;
                    case 6:
                        _orderProcessorServices.RemoveFromCart();
                        break;
                    case 7:
                        _orderProcessorServices.GetAllFromCart();
                        break;
                    case 8:
                        _orderProcessorServices.PlaceOrder();
                        break;
                    case 9:
                        _orderProcessorServices.GetOrdersByCustomer();
                        break;
                    case 10:
                        Console.WriteLine("Thank you!!!");
                        break;
                }

                if (userOption == 10)
                {
                    break;
                }
                else if (userOption > 10)
                {
                    Console.WriteLine("Choose the correct given option");
                }
            }
        }
    }
}

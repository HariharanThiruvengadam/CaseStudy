using EcommerceApplication.Model;
using EcommerceApplication.Repositories;
using NUnit.Framework;

namespace NunitTesting
{
    public class Class1
    {
       readonly IOrderProcessorRepository orderRepo;

        public Class1()
        {
            orderRepo = new OrderProcessorRepository();
        }


        [Test]
        public void CreatedProduct()
        {
            Product product = new Product();

            product.Name = "Boots";
            product.StockQuantity = 10;
            product.Price = 2000.00m;
            product.Description = "Comfy and soft wearable";

            int status  = orderRepo.CreateProduct(product);

            Assert.That(status>0);
        }

        [Test]
        public void AddedToCart()
        {
            Cart cart = new Cart();

            cart.CustomerId = 2;
            cart.ProductId = 7;
            cart.Quantity =2;
          
            bool status = orderRepo.AddToCart(cart);

            Assert.That(status==true);
        }


        [Test]

        public void PlacedOrder()
        {
            Order order = new Order();
            order.CustomerId = 5;
            order.OrderDate = DateTime.Now;
            order.ShippingAddress = "Behal nagar";


            List<KeyValuePair<Product, int>> products = new List<KeyValuePair<Product, int>>()
            {
                new KeyValuePair<Product, int>(new Product{ProductId = 8},1),
                 new KeyValuePair<Product, int>(new Product{ProductId = 7},1)
            };


            (bool status, decimal totalAmount) = orderRepo.PlaceOrder(order, products);

            /* Assert.That(status == true);
             Assert.That("19999.99".Equals(totalAmount.ToString()));*/
            Assert.That(status == true);
            Assert.That("530000.00".Equals(totalAmount.ToString()));
        }

    }
}

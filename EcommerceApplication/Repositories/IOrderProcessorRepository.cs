using EcommerceApplication.Model;

namespace EcommerceApplication.Repositories
{
    public interface IOrderProcessorRepository
    {
        int CreateProduct(Product product);

        int CreateCustomer(Customer customer);

        bool DeleteProduct(int productId);

        bool DeleteCustomer(int customerId);

        bool AddToCart(Cart cart);

        bool RemoveFromCart(int customerId, int productId);

        (bool, decimal) PlaceOrder(Order order, List<KeyValuePair<Product, int>> products);

        (List<Order>, string) GetOrdersByCustomer(int customerId);

        List<Product> getAllProducts();

        List<Customer> getAllCustomers();

        List<KeyValuePair<Cart, string>> GetAllFromCart(int customerId);
    }
}

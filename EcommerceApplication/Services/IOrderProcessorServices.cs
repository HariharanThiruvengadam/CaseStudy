namespace EcommerceApplication.Services
{
    internal interface IOrderProcessorServices
    {
        void CreateProduct();

        void CreateCustomer();

        void DeleteProduct();

        void DeleteCustomer();

        void AddToCart();

        void RemoveFromCart();

        void GetAllFromCart();

        void PlaceOrder();

        void GetOrdersByCustomer();
    }
}

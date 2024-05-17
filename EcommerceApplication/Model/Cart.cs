namespace EcommerceApplication.Model
{
    public class Cart
    {
        private int? cartId; 
        public int? CartId 
        {
            get { return cartId; }
            set { cartId = value; }
        }

        private int customerId;
        public int CustomerId 
        {
            get { return customerId; }
            set { customerId = value; }
        }

        private int productId;
        public int ProductId  
        {
            get { return productId; }
            set { productId = value; }
        }

        private int quantity;
        public int Quantity  
        {
            get { return quantity; }
            set { quantity = value; }
        }


        public Cart()
        {
        }

        public Cart(int? cartId, int customerId, int productId, int quantity)
        {
            CartId = cartId;
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $" Cart-Id: {CartId}\nCustomer-Id: {CustomerId}\nProduct-Id: {ProductId}\nQuantity: {Quantity}\n";
        }
    }
}

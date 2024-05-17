namespace EcommerceApplication.Model
{
    public class OrderItems
    {
        private int orderItemId; 
        public int OrderItemId 
        {
            get { return orderItemId; }
            set { orderItemId = value; }
        }

        private int orderId; 
        public int OrderId  
        {
            get { return orderId; }
            set { orderId = value; }
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


        public OrderItems(int orderItemId, int orderId, int productId, int quantity)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public OrderItems()
        {
        }

        public override string ToString()
        {
            return $"Order Item ID: {OrderItemId}\nOrder ID: {OrderId}\nProduct ID: {ProductId}\nQuantity: {Quantity}\n";
        }
    }
}

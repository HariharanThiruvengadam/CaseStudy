namespace EcommerceApplication.Model
{
    public class Product
    {
        private int? productId; 
        public int? ProductId  
        {
            get { return productId; }
            set { productId = value; }
        }

        private string name; 
        public string Name  
        {
            get { return name; }
            set { name = value; }
        }

        private decimal price; 
        public decimal Price  
        {
            get { return price; }
            set { price = value; }
        }

        private string description; 
        public string Description  
        {
            get { return description; }
            set { description = value; }
        }

        private int stockQuantity; 
        public int StockQuantity  
        {
            get { return stockQuantity; }
            set { stockQuantity = value; }
        }


        public Product()
        {
        }

        public Product(int? product_id,
        string name,
        decimal price,
        string description,
        int stockQuantity)
        {
            ProductId = product_id;
            Name = name;
            Price = price;
            Description = description;
            StockQuantity = stockQuantity;
        }

        public override string ToString()
        {
            return $"Product ID: {ProductId}\nName: {Name}\nPrice: {Price:C}\nDescription: {Description}\nStock Quantity: {StockQuantity}\n";
        }
    }
}

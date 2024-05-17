namespace EcommerceApplication.Model
{
    public class Customer
    {
        private int? customerId;
        public int? CustomerId 
        {
            get { return customerId; }
            set { customerId = value; }
        }

        private string name; 
        public string Name  
        {
            get { return name; }
            set { name = value; }
        }

        private string email; 
        public string Email 
        {
            get { return email; }
            set { email = value; }
        }

        private string password;
        public string Password  
        {
            get { return password; }
            set { password = value; }
        }

        public Customer()
        {
        }

        public Customer(int? customer_id,
        string name,
        string email,
        string password)
        {
            CustomerId = customer_id;
            Name = name;
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            return $"Customer ID: {CustomerId}\nName: {Name}\nEmail: {Email}\n";
        }
    }
}

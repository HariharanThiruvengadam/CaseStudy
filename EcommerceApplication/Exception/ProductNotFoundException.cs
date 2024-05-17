using System;
namespace EcommerceApplication.Exception
{
	public class ProductNotFoundException:IOException
	{
		public ProductNotFoundException(string? message) : base(message)
        {
		}
	}
}


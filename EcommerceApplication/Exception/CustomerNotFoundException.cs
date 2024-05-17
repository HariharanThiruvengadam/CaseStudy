using System;
namespace EcommerceApplication.Exception
{
	public class CustomerNotFoundException:IOException
	{
		public CustomerNotFoundException(string? message) : base(message)
		{
		}
		
	}
}


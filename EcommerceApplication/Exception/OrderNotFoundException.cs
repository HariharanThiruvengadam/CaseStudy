using System;
namespace EcommerceApplication.Exception
{
	public class OrderNotFoundException:IOException
	{
		public OrderNotFoundException(string? message): base(message)
		{
		}
	}
}


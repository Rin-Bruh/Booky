﻿namespace Booky_API.Models.Dto
{
	public class RegisterationRequestDTO
	{
		public string UserName { get; set; }
		public string Name { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
	}
}

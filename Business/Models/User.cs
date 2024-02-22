using System.Collections.Generic;
using System;

namespace App3.Business.Models
{
	public enum UserRole
	{
		Admin,
		User,
		Customer,
		CustomerSupport,
		Technician
	}

	public class User
	{
		public int Id { get; set; }

		public string Name { get; set; } = String.Empty;

		public string Surname { get; set; } = String.Empty;

		public string Email { get; set; } = String.Empty;

		public string Phone { get; set; } = String.Empty;

		public string PasswordHash { get; set; } = String.Empty;

		public UserRole Role { get; set; }

		//public Address Address { get; set; } = null!;

		public ICollection<Device> Devices { get; set; } = new List<Device>();

		public bool IsActive { get; set; } = true;
	}
}

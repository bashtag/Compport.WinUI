using System;
using System.Collections.Generic;

namespace App3.Business.Models
{
	public class City
	{
		public int Id { get; set; }

		public string? CityName { get; set; } = string.Empty;

		public ICollection<District> Districts { get; } = new List<District>();
	}
}
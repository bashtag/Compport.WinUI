using System;
namespace App3.Business.Models
{
	public class District
	{
		public int Id { get; set; }

		public string ?DistrictName { get; set; } = string.Empty;

		public City City { get; set; } = null!;
	}
}
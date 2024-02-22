using System;

namespace App3.Business.Models
{
	public class ComputerModel
	{
		public int Id { get; set; }
		
		public string ModelNumber { get; set; } = String.Empty;
		
		public string BrandName { get; set; } = String.Empty;
		
		public string ModelName { get; set; } = String.Empty;
		
		public DateTime ReleaseDate { get; set; }
		
		public string Description { get; set; } = String.Empty;
		
		public string ProcessorDescription { get; set; } = String.Empty;
		
		public string RamDescription { get; set; } = String.Empty;
		
		public string StorageDescription { get; set; } = String.Empty;
		
		public string OperatingSystemDescription { get; set; } = String.Empty;
		
		public string GraphicsCardDescription { get; set; } = String.Empty;
		
		public string DisplayDescription { get; set; } = String.Empty;
		
		public string BatteryDescription { get; set; } = String.Empty;
		
		public string WifiCardDescription { get; set; } = String.Empty;
		
		public double Weight { get; set; } = 0.0;

		public bool IsActive { get; set; } = true;
	}
}

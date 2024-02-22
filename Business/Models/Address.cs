using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace App3.Business.Models;

public class Address
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public City City { get; set; } = null!;

	public District District { get; set; } = null!;

	public string Hood { get; set; } = string.Empty;

	public int ZipCode { get; set; }

	public string Description { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public string Phone { get; set; } = string.Empty;

	public bool IsActive { get; set; } = true;
}
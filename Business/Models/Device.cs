#nullable enable
using System;

namespace App3.Business.Models
{
    public class Device
    {
        public int Id
        {
            get; set;
        }

        public string SerialNumber { get; set; } = string.Empty;

        public int ComputerModelId
        {
            get; set;
        }

        public ComputerModel ComputerModel { get; set; } = null!;

        public DateTime? PurchaseDate { get; set; } = DateTime.Now;

        public int? UserId
        {
            get; set;
        }

        public User? User
        {
            get; set;
        }

        public bool IsActive { get; set; } = true;
    }
}
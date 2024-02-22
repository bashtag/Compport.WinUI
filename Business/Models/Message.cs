using System;

namespace App3.Business.Models
{
    public class Message
    {
		public int Id { get; set; }

		public User Sender { get; set; } = null!;

		public ChatRoom ChatRoom { get; set; } = null!;

		public string Content { get; set; } = string.Empty;
		public DateTime ?Timestamp { get; set; } = DateTime.Now;
		public bool IsActive { get; set; } = true;
	}
}
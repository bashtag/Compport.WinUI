using System;

using System.Collections.Generic;

namespace App3.Business.Models
{
	public class ChatRoom
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public ICollection<Message> Messages { get; } = new List<Message>();
		
		public bool IsActive { get; set; } = true;
	}
}
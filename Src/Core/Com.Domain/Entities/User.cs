using System;
using System.Collections.Generic;

namespace Com.Domain.Entities
{ 
    public class User : BaseEntity
    {
        public User()
        {
            Items = new List<Item>();
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Role { get; set; } = "User";
        public bool IsApproved { get; set; } = false;
        public List<Item> Items { get; set; }
        public Shop Shop { get; set; }
        public Guid ShopId { get; set; }
    }
}

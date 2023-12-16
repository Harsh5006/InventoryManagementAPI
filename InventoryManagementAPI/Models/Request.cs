using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventoryManagementAPI.Models
{
    public class Request
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public int quantity { get; set; }
        public string RequestStatus { get; set; }
    }
}

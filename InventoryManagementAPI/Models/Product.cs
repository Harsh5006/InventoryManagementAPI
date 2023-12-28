using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManagementAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int Quantity { get; set; }
        
        public Department Department { get; set; }
        [JsonIgnore]

        public ICollection<Request> Requests { get; set; }
    }
}

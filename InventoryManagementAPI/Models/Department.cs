using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

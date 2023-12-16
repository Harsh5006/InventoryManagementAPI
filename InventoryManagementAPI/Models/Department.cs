﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace InventoryManagementAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

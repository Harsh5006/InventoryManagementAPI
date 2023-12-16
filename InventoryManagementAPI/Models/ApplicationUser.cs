using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace InventoryManagementAPI.Models
{
    public class ApplicationUser
    {
        
        public string Name {  get; set; }
        public ICollection<Request> Requests { get; set; }

        [Key,ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}

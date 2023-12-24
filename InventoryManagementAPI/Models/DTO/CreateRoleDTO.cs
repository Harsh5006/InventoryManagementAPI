using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Models
{
    public class CreateRoleDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}

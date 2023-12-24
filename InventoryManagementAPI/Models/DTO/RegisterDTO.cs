using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Models
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(4,ErrorMessage = "Minimum Length for UserName is 4.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

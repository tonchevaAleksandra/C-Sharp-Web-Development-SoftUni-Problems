using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Models.Identity
{
    public class RegisterUserRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}

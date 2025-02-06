using System.ComponentModel.DataAnnotations;

namespace phonezone_backend.DTO.request
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace phonezone_backend.DTO.request
{
    public class RegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}

namespace phonezone_backend.DTO.request
{
    public class ResetPasswordRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}

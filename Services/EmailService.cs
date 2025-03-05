namespace phonezone_backend.Services
{
    using MailKit.Net.Smtp;
    using MimeKit;

    public class EmailService
    {
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUser = "phancongvinh020451@gmail.com";
        private readonly string smtpPass = "osii jndx uwgu ppbk\r\n";

        public void SendResetPasswordEmail(string toEmail, string resetLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PhoneZone", smtpUser));
            message.To.Add(new MailboxAddress(toEmail, toEmail));
            message.Subject = "Reset Your Password";
            message.Body = new TextPart("html")
            {
                Text = $"Click vào link sau để đặt lại mật khẩu: <a href='{resetLink}'>Reset Password</a>"
            };

            using var client = new SmtpClient();
            client.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(smtpUser, smtpPass);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}

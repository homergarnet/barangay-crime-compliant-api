using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using barangay_crime_complaint_api.Models;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;


namespace barangay_crime_compliant_api.Services
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public EmailService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }
        public async Task<string> SendForgotEmailAsync(string toEmail)
        {
            var emailExist = db.Users.Any(z => z.Email.Equals(toEmail));
            if (emailExist)
            {
                var emailQuery = db.Users.Where(z => z.Email == toEmail).FirstOrDefault();

                // User Claims
                var claims = new List<Claim>
                    {
                        new Claim("UserId", emailQuery.Id.ToString()),
                        new Claim("Username", emailQuery.Username.ToString()),
                        new Claim("FirstName", emailQuery.FirstName.ToString()),
                        new Claim("MiddleName", emailQuery.MiddleName.ToString()),
                        new Claim("LastName", emailQuery.LastName.ToString()),
                        new Claim("UserType", emailQuery.UserType.ToString()),

                    };

                // Encrypt credentials
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var auth = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(5), // Set to 5 minutes
                    signingCredentials: credentials);

                // Generate JWT
                var token = new JwtSecurityTokenHandler().WriteToken(auth);
                emailQuery.ForgotPasswordToken = token;
                db.SaveChanges();
                var WebUrl = configuration["WebUrl:Url"];
                var smtpServer = configuration["SmtpSettings:SmtpServer"];
                var smtpPort = int.Parse(configuration["SmtpSettings:SmtpPort"]);
                var smtpUsername = configuration["SmtpSettings:SmtpUsername"];
                var smtpPassword = configuration["SmtpSettings:SmtpPassword"];
                WebUrl = WebUrl + "?token=" + token + "&email=" + toEmail;
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Barangay Crime Management", smtpUsername)); // Change "Your Name" to your sender's name
                message.To.Add(new MailboxAddress(toEmail, toEmail)); // Change "Recipient's Name" as needed
                message.Subject = "Forgot Password";
                var bodyText = $@"
                    <p>Hello,</p>
                    <p>SCIREPORT: We received a request to reset your password. To reset your password, please click the link below:</p>
                    <p><a href='{WebUrl}'>Reset Password</a></p>
                    <p>you can also open this link for when reset password is not available {WebUrl}</p>
                    <p>If you did not request a password reset, you can safely ignore this email.</p>
                    <p>Best regards,<br/>SCIREPORT</p>
                ";
                var textPart = new TextPart("html")
                {
                    Text = bodyText
                };

                message.Body = textPart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, useSsl: true);
                    await client.AuthenticateAsync(smtpUsername, smtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(quit: true);
                }
                return "Email sent successfully";
            }
            else
            {
                return "email not found";
            }


        }

    }
}
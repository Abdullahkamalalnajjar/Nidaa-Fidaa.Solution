using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nidaa_Fidaa.Core.Twilio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WhatsAppService _whatsAppService;
        private static readonly Dictionary<string, string> OtpStore = new Dictionary<string, string>();

        public AuthController(WhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            var otp = GenerateOtp();
            var result = await _whatsAppService.SendOtpAsync(request.PhoneNumber, otp);

            if (result)
            {
                OtpStore[request.PhoneNumber] = otp; // Store OTP for later verification
                return Ok(new { Message = "OTP sent successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Failed to send OTP." });
            }
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            if (OtpStore.TryGetValue(request.PhoneNumber, out var storedOtp) && storedOtp == request.Otp)
            {
                // OTP is correct, generate a JWT token or any other session mechanism
                var token = GenerateJwtToken(request.PhoneNumber);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized(new { Error = "Invalid OTP." });
            }
        }

        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private string GenerateJwtToken(string phoneNumber)
        {
            // Implement JWT token generation logic here
            // Define token handler and key
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[32]; // مفتاح بطول 256 بت
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }


          //  var key = Encoding.ASCII.GetBytes("your_secret_key_here"); // Use a strong secret key

            // Define the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, phoneNumber),
                    new Claim(ClaimTypes.MobilePhone, phoneNumber)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create and return the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // You can also move these classes outside the controller if preferred.
        public class VerifyOtpRequest
        {
            public string PhoneNumber { get; set; }
            public string Otp { get; set; }
        }

        public class SendOtpRequest
        {
            public string PhoneNumber { get; set; }
        }
    }
}

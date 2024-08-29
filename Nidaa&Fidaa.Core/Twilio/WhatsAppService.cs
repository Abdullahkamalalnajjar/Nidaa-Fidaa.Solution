using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.Exceptions;

namespace Nidaa_Fidaa.Core.Twilio
{
    public class WhatsAppService
    {
        private readonly TwilioSettings _twilioSettings;

        public WhatsAppService(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
        }

        public async Task<bool> SendOtpAsync(string to, string otp)
        {
            try
            {
                var message = await MessageResource.CreateAsync(
                    body: $"Your OTP code is {otp}",
                    from: new PhoneNumber(_twilioSettings.WhatsAppFrom),
                    to: new PhoneNumber($"whatsapp:{to}")
                );

                if (message.ErrorCode != null)
                {
                    // Log the error code and message for debugging
                    Console.WriteLine($"Error sending OTP: {message.ErrorMessage} (Code: {message.ErrorCode})");
                    return false;
                }

                return true;
            }
            catch (ApiException ex)
            {
                // Log the exception details
                Console.WriteLine($"API Exception: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log any other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
    }
}


public class TwilioSettings
{
    public string AccountSid { get; set; }
    public string AuthToken { get; set; }
    public string WhatsAppFrom { get; set; }
}

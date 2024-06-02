using AzureSendgridSample.Common;
using Microsoft.AspNetCore.Mvc;

namespace AzureSendgridSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            var attachments = new List<EmailAttachment>();

            if (emailRequest.Attachments != null)
            {
                foreach (var attachment in emailRequest.Attachments)
                {
                    attachments.Add(new EmailAttachment
                    {
                        FileName = attachment.FileName,
                        Content = attachment.Content,
                        Type = attachment.Type
                    });
                }
            }

            await _emailService.SendEmailAsync(emailRequest.ToEmail, emailRequest.Subject,
                emailRequest.Message, emailRequest.HTMLMessage,
            attachments, emailRequest.CcEmail, emailRequest.BccEmail);
            return Ok("Email sent successfully.");
        }
    }
}

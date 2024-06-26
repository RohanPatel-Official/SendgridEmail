﻿using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureSendgridSample.Common
{

    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message, string htmlMessage,
            List<EmailAttachment> attachments = null, string ccEmail = null, string bccEmail = null);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message, string htmlMessage,
            List<EmailAttachment> attachments = null, string ccEmail = null, string bccEmail = null)
        {
            var client = new SendGridClient(_emailSettings.SendGridApiKey);
            var from = new EmailAddress("rohan.patel.dev@gmail.com", "Testing from local .Net");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, htmlMessage);

            if (!string.IsNullOrEmpty(ccEmail))
            {
                msg.AddCc(ccEmail);
            }

            if (!string.IsNullOrEmpty(bccEmail))
            {
                msg.AddBcc(bccEmail);
            }
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    var fileBytes = Convert.FromBase64String(attachment.Content);
                    msg.AddAttachment(attachment.FileName, Convert.ToBase64String(fileBytes));
                }
            }

            var response = await client.SendEmailAsync(msg);
        }


    }
}

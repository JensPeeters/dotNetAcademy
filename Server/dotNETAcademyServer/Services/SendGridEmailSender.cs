using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Threading.Tasks;

namespace dotNETAcademyServer.Services
{
    public class SendGridEmailSender
    {
        /// <summary>
        /// Aanroepen:
        /// SendGridEmailSender emailSender = new SendGridEmailSender();
        /// string mailmsg = "Mogelijk gemaakt door Davy";
        /// emailSender.SendEmailAsync("voorbeeld@domain", "Bedankt voor je bestelling!", mailmsg, 1).Wait();
        /// </summary>
        PDFGenerator pdfGenerator = new PDFGenerator();
        public async Task SendEmailAsync(string userEmail, string emailSubject, string message, int bestellingsId)
        {
            //var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@dotNetAcademy.be", "dotNetAcadamy"),
                Subject = emailSubject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(userEmail, ""));
            msg.SetFooterSetting(true,
                "<p style='color: grey;'>Dit is een automatische bevestiging van je bestelling.</p>",
                "Dit is een automatische bevestiging van je bestelling.");

            string fileName = "Factuur" + bestellingsId + ".pdf";
            pdfGenerator.GeneratePDF(fileName);
            byte[] pdfBytes = File.ReadAllBytes("./Bestellingen/" + fileName);
            string pdfBase64 = Convert.ToBase64String(pdfBytes);

            var bestelling = new Attachment()
            {
                Content = pdfBase64,
                Type = "application/pdf",
                Filename = fileName,
                Disposition = "inline",
                ContentId = "Factuur"
            };    
            msg.AddAttachment(bestelling);

            var response = await client.SendEmailAsync(msg);
        }
    }
}

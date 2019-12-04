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
        /// emailSender.SendEmailAsync("voorbeeld@domain", "Bedankt voor je bestelling!", mailmsg, 1, stream).Wait();
        /// </summary>
        PDFGenerator pdfGenerator = new PDFGenerator();
        public async Task SendEmailAsync(string userEmail, string emailSubject, string message, int? bestellingsId = null, MemoryStream stream = null)
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
                "<p style='color: grey;'>Dit is een automatische mail van dotNetAcademy™</p>",
                "Dit is een automatische mail van dotNetAcademy™");

            if(bestellingsId != null && stream != null)
            {
                msg.AddAttachment(CreateAttachment(bestellingsId.Value, stream));
            }  

            var response = await client.SendEmailAsync(msg);
        }
        public Attachment CreateAttachment(int bestellingsId, MemoryStream stream = null)
        {
            string fileName = "Factuur" + bestellingsId + ".pdf";
            ///--------
            /// Deze lijnen gebruik je wanneer je je document opslaat en hem uitleest om het document zo mee te sturen in de email
            //byte[] pdfBytes = File.ReadAllBytes("./Bestellingen/" + fileName);
            //byte[] pdfBytes = File.ReadAllBytes("factuur" + factuurId + ".pdf");
            //string pdfBase64 = Convert.ToBase64String(pdfBytes);
            ///--------
            byte[] buffer = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Flush();
            stream.Read(buffer, 0, (int)stream.Length);
            string pdfBase64 = Convert.ToBase64String(buffer);
            var bestelling = new Attachment()
            {
                Content = pdfBase64,
                Type = "application/pdf",
                Filename = fileName,
                Disposition = "inline",
                ContentId = "Factuur"
            };
            return bestelling;
        }
    }
}

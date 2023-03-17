using MercedesBenzModel;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzApiRecuperacion.Helpers
{
    public class MailService
    {

        public async Task<bool> SendGridMailAsync(Correo Correo, string Receptor, string NombreReceptor, string asunto, string html_content)
        {
            string mensaje = "Error en SendGridMailAsync";
            try
            {
                var from = new EmailAddress(Correo.Account, Correo.Name);
                var to = new EmailAddress(Receptor, NombreReceptor);
                var client = new SendGridClient(Correo.Key);
                var msg = MailHelper.CreateSingleEmail(from, to, asunto, string.Empty, html_content);

                Task response = client.SendEmailAsync(msg);
                response.Wait();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> SendNetMailAsync(Correo Correo, string Receptor, string NombreReceptor, string Asunto, string HtmlBody)
        {
            string mensaje = "Error en SendNetMailAsync";
            try
            {

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(Correo.Account, Correo.Name)
                };
                mailMessage.To.Add(new MailAddress(Receptor, NombreReceptor));
                mailMessage.Subject = Asunto;
                mailMessage.Body = HtmlBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;

                var smtpClient = new SmtpClient();
                smtpClient.Host = Correo.Host;
                smtpClient.Port = int.Parse(Correo.Puerto);
                smtpClient.EnableSsl = bool.Parse(Correo.Ssl);
                smtpClient.UseDefaultCredentials = bool.Parse(Correo.DefaultCredentials);

                var networkCredential = new NetworkCredential(Correo.Account, Correo.Password);
                smtpClient.Credentials = networkCredential;
                smtpClient.Send(mailMessage);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public string GenerarTokenRecuperacion(string cadena)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public string HtmlBodyRecuperacion(string UrlRecuperacion)
        {
            string body = "<div style='width:100%;'>";
            body += "<div style='margin:0px auto;width:600px;display:flex;height:30px;'></div>";
            body += "<div style='margin:0px auto;width:600px;background:#000000;background-color:#000000;color:#ffffff;'>";
            body += "<img src='https://multimedia.getresponse.com/getresponse-hilut/photos/a2282ddc-ec85-44bf-a069-339c8a6dae1f.jpg' style='width:100%'>";
            body += "</div>";
            body += "<div style='margin:0px auto;width:600px;display:flex;height:40px;'></div>";
            body += "<div style='margin:0px auto;width:600px;background:#000000;background-color:#000000;color:#ffffff;'>";
            body += "<div style='padding:0px 40px;'>";
            body += "<div style='display:flex;height:60px;'></div>";
            body += "<div style='display:flex;height:80px;align-items:end;'>";
            body += "<strong><span style='font-size:32px;font-family:Lato,Arial,sans-serif'>Recuperación de contraseña</span></strong>";
            body += "</div>";
            body += "<div style='display:flex;height:60px;'></div>";
            body += "<div style='display:flex;height:40px;align-items:end;'>";
            body += "<span style='font-size:14px;font-family:Tahoma'>Accede al siguiente link para recuperar tu contraseña de acceso</span>";
            body += "</div>";
            body += "<div style='display:flex;height:60px;'></div>";
            body += "<div style='display:flex;height:40px;padding:20px 0px;'>";
            body += $"<a href='{UrlRecuperacion}' style='text-decoration:none;background:#00a4e6;color:#ffffff;font-family:Arial;font-size:16px;padding:12px;'>Recuperar contraseña</a>";
            body += "</div>";
            body += "<div style='display:flex;height:60px;'></div>";
            body += "</div>";
            body += "</div>";
            body += "</div>";

            return body;
        }

    }
}

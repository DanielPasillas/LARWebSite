using System;
using System.Data.Entity;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace LARWebSite.Utilerias
{
    //We use this class for converting the comun url to a SEO Url.
    public static class StringHelpers
    {
        public static string ToSeoUrl(this string url)
        {
            // make the url lowercase
            string encodedUrl = (url ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
        //---------------------------
    }
    //---------------------------

    public class Utilerias
    {
        public bool SendEmailAsync(string emailFrom, string nombre, string mailbody)
        {

            try
            {

                string _bodyMessage = "Enviado por: <strong>" + nombre + "</strong><br>";
                _bodyMessage += "Email de contacto: <strong>"+ emailFrom +"</strong><br><br>";
                _bodyMessage += mailbody;


                MailMessage _sendMessage = new MailMessage(emailFrom, "info@laredcazaypesca.com.mx");
                _sendMessage.Subject = "La Red Caza y Pesca - WebSite Contacto";
                _sendMessage.Body = _bodyMessage;
                _sendMessage.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "relay-hosting.secureserver.net";
                smtp.Port = 25;
                smtp.EnableSsl = false;

                NetworkCredential _nc = new NetworkCredential("info@laredcazaypesca.com.mx", "Larwebsite.0518");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = _nc;
                smtp.Send(_sendMessage);

            }
            catch (System.Net.Mail.SmtpException)
            {
                throw;
            }

            return true;
        }
    }
}
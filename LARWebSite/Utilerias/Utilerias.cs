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
                MailMessage _sendMessage = new MailMessage(emailFrom, "asis.analytic@gmail.com");
                _sendMessage.Subject = "La Red Caza y Pesca - WebSite Contacto";
                _sendMessage.Body = mailbody;
                _sendMessage.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential _nc = new NetworkCredential("asis.analytic@gmail.com", "asis.analytics.333");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = _nc;
                smtp.Send(_sendMessage);

            }
            catch (System.Net.Mail.SmtpException)
            {

                throw;
            }


            return true;
            /*
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add("asis.analytic@gmail.com");

            msg.From = new MailAddress(emailFrom, nombre, System.Text.Encoding.UTF8);

            msg.Subject = "La Red Caza y Pesca WebSite - Contacto";

            msg.SubjectEncoding = System.Text.Encoding.UTF8;

            msg.Body = mailbody;

            msg.BodyEncoding = System.Text.Encoding.UTF8;

            msg.IsBodyHtml = false;

            //Aquí es donde se hace lo especial
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("asis.analytic@gmail.com", "asis.analytics.333");
            client.Port = 25;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail
            try
            {
                client.Send(msg);
            }
            catch (System.Net.Mail.SmtpException)
            {
                throw;
            }

            return true;
            */
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using crud1.Models;

namespace crud1.Util
{
    public class SendMail
    {
        public void send(List<Usuario> userList, Evento ev) {
            try {
                if (userList != null && userList.Count() > 0) {
                    foreach (Usuario u in userList) {
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("processoevoluasrpq@gmail.com");
                        mail.To.Add(u.Email);
                        mail.Subject = "Convite";
                        mail.Body = "Você está convidado para o evento: " + ev.Descricao;

                        // em caso de anexos
                        //mail.Attachments.Add(new Attachment(@"C:\teste.txt"));

                        using (var smtp = new SmtpClient("smtp.gmail.com"))
                        {
                            smtp.EnableSsl = true;
                            smtp.Port = 587;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;

                            smtp.Credentials = new NetworkCredential("processoevoluasrpq@gmail.com", "processo1234");
                            smtp.Send(mail);
                        }
                    }
                }
            } catch (Exception e) {
                throw e;
            }
        }
    }
}
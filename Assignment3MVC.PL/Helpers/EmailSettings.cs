using Assignment3MVC.DAL.Entities;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Assignment3MVC.PL.Helpers
{
	public class EmailSettings
	{
		public static void  SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("comboell@gmail.com", "yzubnuwasaprgoxv");
			client.Send("comboell@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}

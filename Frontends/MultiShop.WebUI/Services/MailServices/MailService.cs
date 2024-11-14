using MultiShop.DtoLayer.MailDtos;
using System.Net.Mail;

namespace MultiShop.WebUI.Services.MailServices
{
	public class MailService : IMailService
	{
		public bool SendMail(CreateMailSendDto mailSendDto)
		{
			if (mailSendDto != null)
			{
				MailMessage mail = new MailMessage();
				mail.To.Add(mailSendDto.To);
				mail.From= new MailAddress(mailSendDto.From);
				mail.Subject=mailSendDto.Subject;
				mail.Body=mailSendDto.Body;
				mail.IsBodyHtml=true;
				SmtpClient smtp = new SmtpClient();
				smtp.Host = "smtp.gmail.com";
				smtp.Port = 587;
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = new System.Net.NetworkCredential("Mail adresi", "Uygulama Şifresi oluşturduk");
				smtp.EnableSsl = true;
				smtp.Send(mail);
				return true;
			}
			else
				return false;
		}
	}
}

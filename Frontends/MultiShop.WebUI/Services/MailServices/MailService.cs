using Microsoft.Extensions.Configuration;
using MultiShop.DtoLayer.MailDtos;
using System.Net.Mail;

namespace MultiShop.WebUI.Services.MailServices
{
	public class MailService : IMailService
	{
		private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendMail(CreateMailSendDto mailSendDto)
		{
			string sendEmailAddress = _configuration["SendMailAddress"];
			string sendEmailPassword = _configuration["SendMailPassword"];
			mailSendDto.From = _configuration["SendMailAddress"];
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
				smtp.Credentials = new System.Net.NetworkCredential(sendEmailAddress, sendEmailPassword);
				smtp.EnableSsl = true;
				smtp.Send(mail);
				return true;
			}
			else
				return false;
		}
	}
}

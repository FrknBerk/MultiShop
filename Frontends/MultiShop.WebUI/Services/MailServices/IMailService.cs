using MultiShop.DtoLayer.MailDtos;

namespace MultiShop.WebUI.Services.MailServices
{
	public interface IMailService
	{
		bool SendMail(CreateMailSendDto mailSendDto);
	}
}

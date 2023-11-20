using backend.Application.Models;
using backend.DataAccess.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace backend.Application.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly ITemplateService _templateService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public EmailService(ITemplateService templateService, IConfiguration configuration, ILogger<EmailService> logger)
        {
            _templateService = templateService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<EmailModel> SendEmailToSubscriber(Subscriber subscriber)
        {
            var email = await CreateEmailSubscriber(subscriber);
            await SendEmail(email);
            return email;
        }

        public async Task<EmailModel> SendEmailToAdmin(AdminEmailModel admin)
        {
            var email = await CreateEmailAdmin(admin);
            await SendEmail(email);
            return email;
        }

        public async Task<EmailModel> SendEmailWithApiData(ApiResultModel apiModel, Subscriber subscriber)
        {
            var email = await CreateEmailFromApi(apiModel, subscriber);
            await SendEmail(email);
            _logger.LogInformation($"Sent email to {subscriber.Email}");
            return email;
        }

        private async Task<EmailModel> CreateEmailSubscriber(Subscriber subscriber)
        {
            Dictionary<string, string> wordsToReplace = new()
            {
                { "#{ID}#", subscriber.Id.ToString() },
                { "#{Name}#", subscriber.Name },
                {
                    "#{URL}#",
                    _configuration.GetValue<string>("Urls:UnsubscribeUrl")
                },
            };
            var messageContent = await _templateService.GetNewSubscriberHTML(typeof(EmailService).Assembly, wordsToReplace);

            var message = new EmailModel
            {
                From = _configuration.GetValue<string>("Smtp:FromAddress"),
                To = subscriber.Email,
                Subject = "Many thanks for your subscription!",
                Body =  new TextPart("html") {Text = messageContent }
            };
            return message;
        }
        private async Task<EmailModel> CreateEmailAdmin(AdminEmailModel admin)
        {
            Dictionary<string, string> wordsToReplace = new()
            {
                { "#{Name}#", admin.Username },
                { "#{Code}#", admin.Code }
            };
            var messageContent = await _templateService.GetAdminHTML(typeof(EmailService).Assembly, wordsToReplace);

            var message = new EmailModel
            {
                From = _configuration.GetValue<string>("Smtp:FromAddress"),
                To = admin.Email,
                Subject = "Login Request!",
                Body = new TextPart("html") {Text = messageContent }
            };
            return message;
        }

        private async Task<EmailModel> CreateEmailFromApi(ApiResultModel apiModel, Subscriber subscriber)
        {
            
            var companiesHtml = string.Empty;
            foreach (var company in apiModel.Result)
            {
                var wordsToReplaceCompany = new Dictionary<string, string>
                {
                    {"#{SYMBOL}#",company.Symbol},
                    { "#{LONGNAME}#", company.LongName },
                    {"#{REGULARMARKETPRICE}#", company.RegularMarketPrice.ToString()},
                    {"#{MARKETCAP}#",company.MarketCap.ToString()},
                    {"#{PREDICTION}#", company.PredictedPrice.ToString()}
                };

                var companyHtml =
                    await _templateService.GetCompanyHTML(typeof(EmailService).Assembly, wordsToReplaceCompany);
                
                companiesHtml += companyHtml;
            }

            var wordsToReplaceApi = new Dictionary<string, string>
            {
                {"#{ID}#", subscriber.Id.ToString()},
                {"#{NAME}#", subscriber.Name},
                {"#{COMPANIES}#", companiesHtml},
                {
                    "#{URL}#",
                    _configuration.GetValue<string>("Urls:UnsubscribeUrl")
                },
                {
                    "#{URLQ}#", _configuration.GetValue<string>("Urls:QuestionnaireUrl")
                }
            };
            var apiHtml = await _templateService.GetApiHTML(typeof(EmailService).Assembly, wordsToReplaceApi);

            var messageBodyBuilder = new BodyBuilder();
            
            foreach (var company in apiModel.Result)
            {
                var resource = "..\\backend.Application\\Utilities\\Images";
                var imagePath = $"{resource}\\{company.Symbol}.png";
                var image = await messageBodyBuilder.LinkedResources.AddAsync(imagePath);
                image.ContentId = company.Symbol;
            }

            messageBodyBuilder.HtmlBody = apiHtml;
            
            var message = new EmailModel
            {
                From = _configuration.GetValue<string>("Smtp:FromAddress"),
                To = subscriber.Email,
                Subject = "Here is the news for this week!!",
                Body =  messageBodyBuilder.ToMessageBody()
            };
            return message;
        }

        private async Task SendEmail(EmailModel email)
        {
            string host = _configuration.GetValue<string>("Smtp:Server");
            int port = _configuration.GetValue<int>("Smtp:Port");
            string fromAddress = _configuration.GetValue<string>("Smtp:FromAddress");
            string password = _configuration.GetValue<string>("Smtp:Password");

            MimeMessage emailMsg = ConfigureEmail(email);
            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(host, port, true);
                await client.AuthenticateAsync(fromAddress, password);
                await client.SendAsync(emailMsg);

                _logger.LogInformation("Email sent successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, DateTime.UtcNow.ToLongTimeString());
            }
            finally
            {
                await client.DisconnectAsync(true);
            }

        }

        private static MimeMessage ConfigureEmail(EmailModel emailMessage)
        {
            var email = new MimeMessage();

            email.To.Add(MailboxAddress.Parse(emailMessage.To));
            email.From.Add(MailboxAddress.Parse(emailMessage.From));
            email.Subject = emailMessage.Subject;
            email.Body = emailMessage.Body;
            
            return email;
        }
    }
}

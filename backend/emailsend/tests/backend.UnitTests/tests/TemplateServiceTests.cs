using backend.Application.Services;
using backend.Application.Services.Impl;
using FluentAssertions;

namespace backend.UnitTests.tests
{
    public class TemplateServiceTests
    {
        private readonly ITemplateService _templateService;
        public TemplateServiceTests()
        {
            _templateService = new TemplateService();
        }

        [Fact]
        public async Task GetSubscriberHTML_Returns_Subscriber_Email_Page()
        {
            // Arrange 
            Dictionary<string, string> wordsToReplace = new()
            {
                { "#{ID}#", Guid.NewGuid().ToString() },
                { "#{Name}#", "Vasilie" },
                {
                    "#{URL}#",
                    "http://localhost:4200/unsubscribe/"
                }
            };
            // Act
            var result = await _templateService.GetNewSubscriberHTML(typeof(TemplateService).Assembly,wordsToReplace);

            // Assert
            result.Should().NotBeNull();
            result.Contains(wordsToReplace["#{Name}#"]).Should().BeTrue();
        }

        [Fact]
        public async void GetCompanyHTML_Returns_Content()
        {
            // Arrange 
            Dictionary<string, string> wordsToReplace = new()
            {
                {"#{SYMBOL}#","NKE"},
                { "#{LONGNAME}#", "Nike Inc." },
                {"#{REGULARMARKETPRICE}#","231"},
                {"#{MARKETCAP}#","123456789"}
            };
            // Act
            var result = await _templateService.GetCompanyHTML(typeof(TemplateService).Assembly,wordsToReplace);

            // Assert
            result.Should().NotBeNull();
            result.Contains(wordsToReplace["#{SYMBOL}#"]).Should().BeTrue();
        }
        
        [Fact]
        public async void GetApiHTML_Returns_Content()
        {
            // Arrange 
            Dictionary<string, string> wordsToReplace = new()
            {
                {"#{ID}#", "21312312"},
                {"#{NAME}#", "Name"},
                {"#{COMPANIES}#", "companiesHtml"},
            };
            // Act
            var result = await _templateService.GetApiHTML(typeof(TemplateService).Assembly,wordsToReplace);

            // Assert
            result.Should().NotBeNull();
            result.Contains(wordsToReplace["#{ID}#"]).Should().BeTrue();
        }
        
        [Fact]
        public async void GetAdminHTML_Returns_Content()
        {
            // Arrange 
            Dictionary<string, string> wordsToReplace = new()
            {
                { "#{Name}#", "Username" },
                { "#{Code}#", "Code" }
            };
            // Act
            var result = await _templateService.GetAdminHTML(typeof(TemplateService).Assembly,wordsToReplace);

            // Assert
            result.Should().NotBeNull();
            result.Contains(wordsToReplace["#{Name}#"]).Should().BeTrue();
        }
    }
}

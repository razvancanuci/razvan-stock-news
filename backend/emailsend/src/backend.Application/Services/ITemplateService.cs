using System.Reflection;

namespace backend.Application.Services
{
    public interface ITemplateService
    {
        Task<string> GetNewSubscriberHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary);
        Task<string> GetAdminHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary);

        Task<string> GetApiHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary);

        Task<string> GetCompanyHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary);
    }
}

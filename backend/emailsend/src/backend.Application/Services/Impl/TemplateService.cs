using System.Reflection;

namespace backend.Application.Services.Impl
{
    public class TemplateService : ITemplateService
    {
        public async Task<string> GetNewSubscriberHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary)
        {
            var resourceName = assembly.GetManifestResourceNames()
           .SingleOrDefault(str => str.EndsWith("NewSubscriber.html"));
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();
            foreach (var word in wordsToReplaceDictionary)
                result = result.Replace(word.Key, word.Value);
            return result;
        }

        public async Task<string> GetAdminHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary)
        {
            var resourceName = assembly.GetManifestResourceNames()
                .SingleOrDefault(str => str.EndsWith("2Factor.html"));
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();
            foreach (var word in wordsToReplaceDictionary)
                result = result.Replace(word.Key, word.Value);
            return result;
        }

        public async Task<string> GetApiHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary)
        {
            var resourceName = assembly.GetManifestResourceNames()
                .SingleOrDefault(str => str.EndsWith("Api.html"));
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();
            foreach (var word in wordsToReplaceDictionary)
                result = result.Replace(word.Key, word.Value);
            return result;
        }

        public async Task<string> GetCompanyHTML(Assembly assembly, Dictionary<string, string> wordsToReplaceDictionary)
        {
            var resourceName = assembly.GetManifestResourceNames()
                .SingleOrDefault(str => str.EndsWith("Company.html"));
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();
            foreach (var word in wordsToReplaceDictionary)
                result = result.Replace(word.Key, word.Value);
            return result;
        }
    }
}

using backend.Application.Models;

namespace backend.API.Models
{
    public class MessagePublisher
    {
        public static TokenMessage Publish(string message)
        {
            return new TokenMessage { Message = message };
        }
        public static AdminEmailModel PublishEmailAdmin(string code, string email, string username) =>
            new AdminEmailModel
                { Code = code, Email = email, Username = username };
    }
}

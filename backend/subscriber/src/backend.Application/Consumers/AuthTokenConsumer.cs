using backend.Application.Models;
using backend.Application.Utilities;
using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Consumers
{
    [ExcludeFromCodeCoverage]
    public class AuthTokenConsumer : IConsumer<TokenMessage>
    {
        public async Task Consume(ConsumeContext<TokenMessage> context)
        {
            TokenCreator.WriteToken(context.Message.Message);
        }
    }
}

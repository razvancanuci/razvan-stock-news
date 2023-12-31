﻿using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class SubscriberAddedMessage
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}

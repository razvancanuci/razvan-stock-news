using MimeKit;

namespace backend.Application.Models
{
    public class EmailModel
    {
        public string? To { get; set; }

        public string? From { get; set; }

        public string? Subject { get; set; }

        public MimeEntity Body { get; set; }
    }
}

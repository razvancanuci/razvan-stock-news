namespace backend.IntegrationTests.Models
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; init; }

        public T Result { get; init; }

        public IEnumerable<ValidationError> Errors { get; init; }
    }

    public class ValidationError
    {
        public ValidationError(string? field, string message)
        {
            this.Field = field != string.Empty ? field : null;
            this.Message = message;
        }

        public string? Field { get; }

        public string Message { get; }
    }
}

﻿namespace backend.API.Models
{
    public class ApiResponse<T>
    {
        private ApiResponse(bool succeeded, T result, IEnumerable<ValidationError> errors)
        {
            this.Succeeded = succeeded;
            this.Result = result;
            this.Errors = errors;
        }

        public bool Succeeded { get; }

        public T Result { get; }

        public IEnumerable<ValidationError> Errors { get; }

        public static ApiResponse<T> Success(T result)
        {
            return new ApiResponse<T>(true, result, new List<ValidationError>());
        }

        public static ApiResponse<T> Fail(IEnumerable<ValidationError> errors)
        {
            return new ApiResponse<T>(false, default, errors);
        }
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


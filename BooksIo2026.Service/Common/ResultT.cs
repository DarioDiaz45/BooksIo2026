using BooksIo2026.Entities;

namespace BooksIo2026.Service.Common
{
    public class Result<T> where T : class
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T? Value { get; set; }
        public List<string> Errors { get; } = new();

        private Result(bool success, List<string> errors, T? value = null)
        {
            IsSuccess = success;
            Errors = errors;
            Value = value;
        }
        public static Result<T> Success(T? value = null)
        {
            return new Result<T>(true, new List<string>(), value);
        }
        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>(false, errors);

        }
        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, new List<string> { error });
        }
    }
}

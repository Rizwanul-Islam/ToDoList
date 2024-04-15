using FluentValidation.Results;

namespace ToDoService.Application.Exceptions;
public class BadRequestException : ApplicationException
{
    public BadRequestException(string message) : base(message)
    {

    }
}

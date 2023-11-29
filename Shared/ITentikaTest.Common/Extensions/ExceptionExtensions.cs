using System.Net;
using FluentValidation;
using FluentValidation.Results;
using ITentikaTest.Common.Responses;

namespace ITentikaTest.Common.Extensions;

public static class ExceptionExtensions
{
    public static ErrorResponse ToErrorResponse(this Exception exception)
    {
        return new ErrorResponse()
        {
            Code = (int)HttpStatusCode.InternalServerError,
            Message = exception.Message
        };
    }

    public static ErrorResponse ToErrorResponse(this ValidationException validationException)
    {
        return new ErrorResponse()
        {
            Code = (int)HttpStatusCode.BadRequest,
            FieldErrors = validationException.Errors.Select(x => new ErrorResponseFieldInfo()
            {
                FieldName = x.PropertyName,
                Message = x.ErrorMessage
            })
        };
    }
}
using FluentValidation.Results;

namespace ITentikaTest.Common.Validators;

public interface IModelValidator<T> where T : class
{
    void Check(T model);
    ValidationResult Validate(T model);
}
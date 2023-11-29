using FluentValidation;
using FluentValidation.Results;

namespace ITentikaTest.Common.Validators;

public class ModelValidator<T> : IModelValidator<T> where T : class
{
    private readonly IValidator<T> validator;

    public ModelValidator(IValidator<T> validator)
    {
        this.validator = validator;
    }

    public void Check(T model)
    {
        var result = Validate(model);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    public ValidationResult Validate(T model)
    {
        return validator.Validate(model);
    }
}
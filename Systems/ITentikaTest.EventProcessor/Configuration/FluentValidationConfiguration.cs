using System.Net;
using FluentValidation.AspNetCore;
using ITentikaTest.Common.Helpers;
using ITentikaTest.Common.Responses;
using ITentikaTest.Common.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ITentikaTest.EventProcessor.Configuration;

public static class FluentValidationConfiguration
{
    public static IMvcBuilder AddAppValidators(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
            options.InvalidModelStateResponseFactory = context =>
            {
                
                var fieldErrors = new List<ErrorResponseFieldInfo>();
                foreach (var (field, state) in context.ModelState)
                {
                    if (state.ValidationState == ModelValidationState.Invalid)
                        fieldErrors.Add(new ErrorResponseFieldInfo()
                        {
                            FieldName = field,
                            Message = string.Join(", ", state.Errors.Select(x => x.ErrorMessage))
                        });
                }

                var result = new BadRequestObjectResult(new ErrorResponse()
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "One or more validation errors occurred",
                    FieldErrors = fieldErrors
                });

                return result;
            }
        );

        builder.Services.AddFluentValidationAutoValidation();
        
        FluentValidatorHelper.Register(builder.Services);

        builder.Services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));
        
        return builder;
    }
}
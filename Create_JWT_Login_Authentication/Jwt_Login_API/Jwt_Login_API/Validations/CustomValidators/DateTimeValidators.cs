using FluentValidation;
using System;

namespace Jwt_Login_API.Validations.CustomValidators
{
    public static class DateTimeValidators
    {
        public static IRuleBuilderOptions<T, DateTime> AfterSunrise<T>(
            this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            var sunrise = TimeOnly.MinValue.AddHours(6);    // MinValue = 12h

            // Trả về này giờ thời gian clien tcung cấp
            //return ruleBuilder.Must(dateTime => TimeOnly.FromDateTime(dateTime) > sunrise)
            //                    .WithMessage("{PropertyName} must be after sunrise. You Provide time {PropertyValue}");

            // Trả về thời gian
            return ruleBuilder.Must((objectRoot, dateTime, context) =>
            {
                TimeOnly providedTime = TimeOnly.FromDateTime(dateTime);

                context.MessageFormatter.AppendArgument("Sunrise", sunrise);
                context.MessageFormatter.AppendArgument("ProvidedTime", providedTime);

                return providedTime > sunrise;
            }).WithMessage("{PropertyName} must be after {Sunrise}. You Provide time {ProvidedTime}");
        }
    }
}

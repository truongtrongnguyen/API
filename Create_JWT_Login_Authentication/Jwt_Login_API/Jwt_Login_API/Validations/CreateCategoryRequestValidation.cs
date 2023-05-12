using FluentValidation;
using Jwt_Login_API.Models;
using Jwt_Login_API.Validations.Commond;
using Jwt_Login_API.Validations.CustomValidators;

namespace Jwt_Login_API.Validations
{
    public class CreateCategoryRequestValidation : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidation()
        {
            RuleFor(x => x.Name).Length(3, 50).WithMessage("{PropertyName} phai dai tu 3 ky tu den 50 ");
            //RuleFor(x => x.DateCreate).Must(x => x > DateTime.UtcNow);
            //RuleFor(x => x.DateUpdate).GreaterThan(x => x.DateCreate);

            Include(new CreateCategoryDetailsValidation());
            // Dòng trên thay cho 2 dòng này
            //RuleFor(x => x.Descriptions).Must(NotEmpty)   
            //                    .WithMessage("Description phai dai tu 3 ky tu den 50 ");

            RuleFor(x => x.CreateCategoryDetails.DateCreate)
                                                .AfterSunrise();
            Include(new CreateCategoryDetails2Validation());
        }


    }


    // Hoặc ta có thể chuyển Class CreateCategoryDetails thành một validation riêng
    public class CreateCategoryDetailsValidation : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryDetailsValidation()
        {
            RuleFor(x => x.Descriptions).Must(NotEmpty)
                               .WithMessage("Description phai dai tu 3 ky tu den 50 ");
        }
        private bool NotEmpty(string description)
        {
            return description != null && description.Length > 4;
        }
    }

    public class CreateCategoryDetails2Validation : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryDetails2Validation()
        {
            RuleFor(x => x.CreateCategoryDetails.DateUpdate.Month).GreaterThan(5)
                               .WithMessage("Thang Update phai lon hon 5");
        }
    }
}

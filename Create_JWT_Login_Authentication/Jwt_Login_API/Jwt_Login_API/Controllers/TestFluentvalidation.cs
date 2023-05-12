using FluentValidation;
using FluentValidation.Results;
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Jwt_Login_API.Controllers
{
    public class TestFluentvalidation : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestFluentvalidation(AppDbContext context)
        {
            _context = context;
        }

        #region Setup validation sau khi thêm builder.Services.AddFluentValidation();

        [HttpPost("CreateCatetgory2")]
        public IActionResult CreateCategory2([FromForm] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = new CategoryMediator()
            {
                Name = request.Name,
                IsActive = request.IsActive,
                DateCreate = request.CreateCategoryDetails.DateCreate,
                DateUpdate = request.CreateCategoryDetails.DateUpdate,
                Descriptions = request.Descriptions
            };

            _context.CategoryMediators.Add(category);
            _context.SaveChanges();

            return Ok(category);
        }


        #endregion



        #region Setup validation thủ công khi chưa thêm builder.Services.AddFluentValidation();
        [HttpPost("CreateCatetgory")]
        public IActionResult CreateCategory(CreateCategoryRequest request,
                                    [FromServices] IValidator<CreateCategoryRequest> validator)
        {
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var modelStateDictionary = new ModelStateDictionary();

                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    modelStateDictionary.AddModelError(
                        failure.PropertyName,
                        failure.ErrorMessage);
                }
                return ValidationProblem(modelStateDictionary);
            }

            var category = new CategoryMediator()
            {
                Name = request.Name,
                IsActive = request.IsActive,
                DateCreate = request.CreateCategoryDetails.DateCreate,
                DateUpdate = request.CreateCategoryDetails.DateUpdate,
                Descriptions = request.Descriptions
            };

            _context.CategoryMediators.Add(category);
            _context.SaveChanges();

            return Ok(category);
        }
        #endregion
    }
}

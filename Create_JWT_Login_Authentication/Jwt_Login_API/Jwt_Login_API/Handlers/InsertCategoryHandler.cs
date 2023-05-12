using Jwt_Login_API.Commands;
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Models;
using MediatR;

namespace Jwt_Login_API.Handlers
{
    public class InsertCategoryHandler : IRequestHandler<InsertCategoryCommand, CategoryMediator>
    {
        private readonly AppDbContext _context;

        public InsertCategoryHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<CategoryMediator> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
        {
            var caterogy = new CategoryMediator()
            {
                Name = request.CategoryName
            };
            _context.CategoryMediators.Add(caterogy);
            Task.FromResult(_context.SaveChanges());
            return Task.FromResult(caterogy);
        }
    }
}

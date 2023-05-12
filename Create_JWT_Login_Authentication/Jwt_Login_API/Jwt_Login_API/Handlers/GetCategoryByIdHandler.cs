using Jwt_Login_API.Models;
using Jwt_Login_API.Queries;
using JWT_Login_Authentication.Models;
using MediatR;

namespace Jwt_Login_API.Handlers
{
    // Nên đưa ra một thư mục riêng là Handlers
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryMediator>
    {
        private readonly IMediator mediator;
        private readonly AppDbContext _context;

        public GetCategoryByIdHandler(IMediator mediator, AppDbContext context)
        {
            this.mediator = mediator;
            _context = context;
        }
        public async Task<CategoryMediator> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _context.CategoryMediators.FirstOrDefault(x => x.Id == request.Id);
            return result;
        }
    }
}

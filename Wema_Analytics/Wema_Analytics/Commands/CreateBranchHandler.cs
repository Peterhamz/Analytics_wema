using MediatR;
using System;
using Wema_Analytics.Data;
using Wema_Analytics.Entities;

namespace Wema_Analytics.Commands
{
    public record CreateBranch(string Code, string Name, string City, string Region) : IRequest<Branch>;


    public class CreateBranchHandler : IRequestHandler<CreateBranch, Branch>
    {
        private readonly WemaAnalyticsDbContext _context;
        public CreateBranchHandler(WemaAnalyticsDbContext context) => _context = context;

        public async Task<Branch> Handle(CreateBranch request, CancellationToken cancellationToken)
        {
            if (_context.Branches.Any(b => b.Code == request.Code && !b.IsDeleted))
                throw new InvalidOperationException("Branch code already exists");

            var branch = new Branch
            {
                Code = request.Code,
                Name = request.Name,
                City = request.City,
                Region = request.Region,
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };

            _context.Branches.Add(branch);
            await _context.SaveChangesAsync(cancellationToken);

            return branch;
        }
    }
}

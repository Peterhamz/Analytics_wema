using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Wema_Analytics.Data;
using Wema_Analytics.Entities;

namespace Wema_Analytics.Commands
{
    public record UpdateBranch(Guid Id, string Name, string City, string Region) : IRequest<Branch?>;

    public class UpdateBranchHandler : IRequestHandler<UpdateBranch, Branch?>
    {
        private readonly WemaAnalyticsDbContext _context;
        public UpdateBranchHandler(WemaAnalyticsDbContext context) => _context = context;

        public async Task<Branch?> Handle(UpdateBranch request, CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Id == request.Id && !b.IsDeleted, cancellationToken);
            if (branch is null) return null;

            branch.Name = request.Name;
            branch.City = request.City;
            branch.Region = request.Region;
            branch.UpdatedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return branch;
        }
    }
}

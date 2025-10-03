using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Wema_Analytics.Data;

namespace Wema_Analytics.Commands
{
    public record DeactivateBranch(Guid Id) : IRequest<bool>;

    public class DeactivateBranchHandler : IRequestHandler<DeactivateBranch, bool>
    {
        private readonly WemaAnalyticsDbContext _context;
        public DeactivateBranchHandler(WemaAnalyticsDbContext context) => _context = context;

        public async Task<bool> Handle(DeactivateBranch request, CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Id == request.Id && !b.IsDeleted, cancellationToken);
            if (branch is null) return false;

            branch.IsActive = false;
            branch.UpdatedAtUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

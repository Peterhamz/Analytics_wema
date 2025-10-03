using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Wema_Analytics.Data;

namespace Wema_Analytics.Commands
{
    public record DeleteBranch(Guid Id) : IRequest<bool>;

    public class DeleteBranchHandler : IRequestHandler<DeleteBranch, bool>
    {
        private readonly WemaAnalyticsDbContext _context;
        public DeleteBranchHandler(WemaAnalyticsDbContext context) => _context = context;

        public async Task<bool> Handle(DeleteBranch request, CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Id == request.Id && !b.IsDeleted, cancellationToken);
            if (branch is null) return false;

            branch.IsDeleted = true;
            branch.UpdatedAtUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

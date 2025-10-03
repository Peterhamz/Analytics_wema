using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Wema_Analytics.Data;
using Wema_Analytics.Entities;

namespace Wema_Analytics.Commands
{
    public record GetBranchById(Guid Id) : IRequest<Branch?>;

    public class GetBranchByIdHandler : IRequestHandler<GetBranchById, Branch?>
    {
        private readonly WemaAnalyticsDbContext _context;
        public GetBranchByIdHandler(WemaAnalyticsDbContext context) => _context = context;

        public async Task<Branch?> Handle(GetBranchById request, CancellationToken cancellationToken)
        {
            return await _context.Branches.FirstOrDefaultAsync(b => b.Id == request.Id && !b.IsDeleted, cancellationToken);
        }
    }
}

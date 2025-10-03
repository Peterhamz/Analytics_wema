using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Wema_Analytics.Data;
using Wema_Analytics.Entities;

namespace Wema_Analytics.Commands
{
    public record SearchBranches(string? City, string? Region) : IRequest<List<Branch>>;

    public class SearchBranchesHandler : IRequestHandler<SearchBranches, List<Branch>>
    {
        private readonly WemaAnalyticsDbContext _context;
        public SearchBranchesHandler(WemaAnalyticsDbContext context) => _context = context;

        public async Task<List<Branch>> Handle(SearchBranches request, CancellationToken cancellationToken)
        {
            var query = _context.Branches.Where(b => !b.IsDeleted).AsQueryable();

            if (!string.IsNullOrEmpty(request.City))
                query = query.Where(b => b.City.Contains(request.City));

            if (!string.IsNullOrEmpty(request.Region))
                query = query.Where(b => b.Region.Contains(request.Region));

            return await query.ToListAsync(cancellationToken);
        }
    }
}

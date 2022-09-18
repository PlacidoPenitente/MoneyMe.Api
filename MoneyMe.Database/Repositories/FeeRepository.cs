using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public sealed class FeeRepository : Repository<Fee, Database.Models.Fee>, IFeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FeeRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Fee> GetByNameAsync(string name)
        {
            var fee = await _context.Fees.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);

            return _mapper.Map<Fee>(fee);
        }
    }
}

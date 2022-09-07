using MoneyMe.Infrastructure.Database;
using System.Threading.Tasks;
using System;
using MoneyMe.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MoneyMe.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> task)
        {
            var result = await task();
            await CommitAsync();
            await DisposeAsync();

            return result;
        }

        public async Task ExecuteAsync(Func<Task> task)
        {
            await task();
            await CommitAsync();
            await DisposeAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}